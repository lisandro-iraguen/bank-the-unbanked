using CardanoSharp.Wallet;
using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Derivations;
using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.TransactionBuilding;
using CardanoSharp.Wallet.Utilities;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
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
        //return "muffin brisk logic desk spot chase equal hen evil casual hat neck enemy since chief upon anxiety love stuff tent luggage chaos put winter";

        string clientId = "0167353a-c78e-48f9-9464-d94fe6ba3d0b";
        string clientSecret = "xGU8Q~B0of6PB2aAU24L-FlLoEfbrpjz~thdnbm.";//this should be changed in appplication.json, it must use clientSecretId, not the client secret itself
        string tenantId = "c8da0640-1d20-4d34-94de-5d62b0832499";

        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);


        string keyVaultUrl = "https://bank-the-unbanked-kv.vault.azure.net/";
        var client = new SecretClient(new Uri(keyVaultUrl), credential);
        string secretName = "nemonic";
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