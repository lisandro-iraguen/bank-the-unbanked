﻿using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CardanoSharp.Wallet;
using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Derivations;
using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.TransactionBuilding;
using CardanoSharp.Wallet.Utilities;
using Microsoft.Extensions.Configuration;
namespace Api.Services.Policy;


public class PolicyManager : IPolicyManager
{
    private readonly PrivateKey _privateKey;
    private readonly PublicKey _publicKey;

    private readonly IConfiguration _configuration;

    
    public PolicyManager(IConfiguration configuration)
    {
        _configuration = configuration;
        string nmonic = GetNmonicFromAPI();

        var mnemonic = new MnemonicService().Restore(nmonic);
        IIndexNodeDerivation paymentNode1 = mnemonic.GetMasterNode()
            .Derive(PurposeType.PolicyKeys)
            .Derive()
            .Derive(0)
            .Derive(RoleType.ExternalChain)
            .Derive(0);
        paymentNode1.SetPublicKey();
        _privateKey = paymentNode1.PrivateKey;
        _publicKey = paymentNode1.PublicKey;
    }

    private string GetNmonicFromAPI()
    {
        string clientId = _configuration["clientId"];
        string clientSecret = _configuration["clientSecret"]; 
        string tenantId = _configuration["tenantId"];

        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);


        string keyVaultUrl = _configuration["KeyVolt"]; 
        var client = new SecretClient(new Uri(keyVaultUrl), credential);
        string secretName = _configuration["keyVoltScretName"];
        KeyVaultSecret secret = client.GetSecret(secretName);
        string nmonic = secret.Value;
        return nmonic;
    }

    public IScriptAllBuilder GetPolicyScript() =>
        ScriptAllBuilder.Create
            .SetScript(NativeScriptBuilder.Create.SetKeyHash(
                HashUtility.Blake2b224(_publicKey.Key)));

    public PrivateKey GetPrivateKey() => _privateKey;
    public PublicKey GetPublicKey() => _publicKey;
}