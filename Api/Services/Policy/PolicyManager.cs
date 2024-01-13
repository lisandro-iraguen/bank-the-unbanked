using CardanoSharp.Wallet;
using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Derivations;
using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.TransactionBuilding;
using CardanoSharp.Wallet.Utilities;

namespace Api.Services.Policy;


public class PolicyManager : IPolicyManager
{
    private readonly PrivateKey _privateKey;
    private readonly PublicKey _publicKey;

    public PolicyManager()
    {
        var mnemonic = new MnemonicService().Restore("lunch layer hold omit fit select erase toy mixture cruel wonder nose grunt refuse private");
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