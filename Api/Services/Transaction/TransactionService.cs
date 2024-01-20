using Api.Services.Policy;
using CardanoSharp.Koios.Client;
using CardanoSharp.Wallet.CIPs.CIP2;
using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Extensions.Models.Transactions.TransactionWitnesses;
using CardanoSharp.Wallet.Models;
using CardanoSharp.Wallet.Models.Addresses;
using CardanoSharp.Wallet.Models.Transactions;
using CardanoSharp.Wallet.TransactionBuilding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardanoSharpAsset = CardanoSharp.Wallet.Models.Asset;

namespace Api.Services.Transaction;


public class TransactionService : ITransactionService
{
    private readonly IAddressClient _addressClient;
    private readonly INetworkClient _networkClient;
    private readonly IEpochClient _epochClient;
    private readonly IPolicyManager _policyManager;

    private readonly string _sendPaymentToAddress;

    public TransactionService(IConfiguration config, IPolicyManager policyManager, IAddressClient addressClient, INetworkClient networkClient, IEpochClient epochClient)
    {
        _policyManager = policyManager;
        _addressClient = addressClient;
        _networkClient = networkClient;
        _epochClient = epochClient;
    }

    public async Task<CardanoSharp.Wallet.Models.Transactions.Transaction> BuildTransaction(string fromAddress, string toAddress, int value)
    {
        try
        {
           
            
            //0. Prep           
            var scriptPolicy = _policyManager.GetPolicyScript();

            //1. Get UTxOs
            var utxos = await GetUtxos(fromAddress);


            ///2. Create the Body
            var transactionBody = TransactionBodyBuilder.Create;
            ulong amountToTransfer = (ulong)value;

            transactionBody.AddOutput(toAddress.ToAddress().GetBytes(), amountToTransfer);

            var coinSelection = ((TransactionBodyBuilder)transactionBody).UseRandomImprove(utxos, fromAddress);
            foreach (var i in coinSelection.Inputs)
            {
                transactionBody.AddInput(i.TransactionId, i.TransactionIndex);
            }

          

            if (coinSelection.ChangeOutputs is not null && coinSelection.ChangeOutputs.Any())
            {
                foreach (var output in coinSelection.ChangeOutputs)
                    transactionBody.AddOutput(new Address(fromAddress), output.Value.Coin);

            }



            var ppResponse = await _epochClient.GetProtocolParameters();
            var protocolParameters = ppResponse.Content.FirstOrDefault();
         
            var blockSummaries = (await _networkClient.GetChainTip()).Content;
            var ttl = 1000 + (uint)blockSummaries.First().AbsSlot;
            transactionBody.SetTtl(ttl);
            

            var witnessSet = TransactionWitnessSetBuilder.Create
                            .AddVKeyWitness(_policyManager.GetPublicKey(), _policyManager.GetPrivateKey())
                            .MockVKeyWitness(2);
            
            var transaction = TransactionBuilder.Create;
            transaction.SetBody(transactionBody);
            transaction.SetWitnesses(witnessSet);
            

            //get a draft transaction to calculate fee
            var draft = transaction.Build();
           

            var fee = draft.CalculateFee(protocolParameters.MinFeeA, protocolParameters.MinFeeB);
            draft.TransactionBody.TransactionOutputs.Last().Value.Coin -= fee;
            transactionBody.SetFee(fee);
            try
            {
                transactionBody.RemoveFeeFromChange();
            }
            catch (Exception ex)
            {
                Console.WriteLine("this transction can fail if is divided by 0");
                Console.WriteLine(ex.Message);
            }

            transaction.SetBody(transactionBody);
            var rawTx = transaction.Build();
            var mockWitnesses = rawTx.TransactionWitnessSet.VKeyWitnesses.Where(x => x.IsMock);
            foreach (var mw in mockWitnesses)
                rawTx.TransactionWitnessSet.VKeyWitnesses.Remove(mw);

            return rawTx;
        }
        catch (Exception ex)
        {
            Console.WriteLine("build transaction'failed");
            Console.WriteLine(ex.Message);
        }

        return null;
      
    }


    public async Task<CardanoSharp.Wallet.Models.Transactions.Transaction> SignTransaction(string transactionCbor, string witness)
    {
        try
        {
            var tx= transactionCbor.HexToByteArray().DeserializeTransaction();
            var vKeyWitnesses = witness.HexToByteArray().DeserializeTransactionWitnessSet();
            foreach (var vkeyWitness in vKeyWitnesses.VKeyWitnesses)
                tx.TransactionWitnessSet.VKeyWitnesses.Add(vkeyWitness);

            tx.TransactionWitnessSet.VKeyWitnesses.Add(new VKeyWitness()
            {
                VKey = _policyManager.GetPublicKey(),
                SKey = _policyManager.GetPrivateKey()
            });
            return tx;
        }
        catch (Exception ex)
        {
            Console.WriteLine("build transaction'failed");
            Console.WriteLine(ex.Message);
        }

        return null;

    }

    private async Task<List<Utxo>> GetUtxos(string address)
    {
        try
        {
            var addressBulkRequest = new AddressBulkRequest { Addresses = new List<string> { address } };
            var addressResponse = await _addressClient.GetAddressInformation(addressBulkRequest);
            var addressInfo = addressResponse.Content;
            var utxos = new List<Utxo>();

            foreach (var ai in addressInfo.SelectMany(x => x.UtxoSets))
            {
                if (ai is null) continue;
                var utxo = new Utxo()
                {
                    TxIndex = ai.TxIndex,
                    TxHash = ai.TxHash,
                    Balance = new Balance()
                    {
                        Lovelaces = ulong.Parse(ai.Value)
                    }
                };

                var assetList = new List<CardanoSharpAsset>();
                foreach (var aa in ai.AssetList)
                {
                    assetList.Add(new CardanoSharpAsset()
                    {
                        Name = aa.AssetName,
                        PolicyId = aa.PolicyId,
                        Quantity = long.Parse(aa.Quantity)
                    });
                }

                utxo.Balance.Assets = assetList;
                utxos.Add(utxo);
            }

            return utxos;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}