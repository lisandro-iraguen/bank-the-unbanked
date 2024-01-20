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
        string keyVaultUrl =  _configuration["KeyVolt"];

        // Create a new SecretClient using the Key Vault URL and default Azure credentials
        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

        // Retrieve a secret by name
        string secretName = _configuration["keyVoltScretName"];
        KeyVaultSecret secret = client.GetSecret(secretName);

        // Access the secret value

        var mnemonic = new MnemonicService().Restore(secret.Value);
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

    public IScriptAllBuilder GetPolicyScript() =>
        ScriptAllBuilder.Create
            .SetScript(NativeScriptBuilder.Create.SetKeyHash(
                HashUtility.Blake2b224(_publicKey.Key)));

    public PrivateKey GetPrivateKey() => _privateKey;
    public PublicKey GetPublicKey() => _publicKey;
}