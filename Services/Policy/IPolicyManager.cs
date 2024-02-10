using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.TransactionBuilding;

namespace Api.Services.Policy
{
    public interface IPolicyManager
    {
        IScriptAllBuilder GetPolicyScript();
        PrivateKey GetPrivateKey();
        PublicKey GetPublicKey();
    }

}
