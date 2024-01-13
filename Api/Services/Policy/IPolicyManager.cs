using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.TransactionBuilding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Policy
{
    public interface IPolicyManager
    {
        IScriptAllBuilder GetPolicyScript();
        PrivateKey GetPrivateKey();
        PublicKey GetPublicKey();
    }

}
