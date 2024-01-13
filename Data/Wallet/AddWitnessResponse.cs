using CardanoSharp.Wallet.Models.Transactions;

namespace Data.Wallet
{
	

	public class AddWitnessResponse
	{
		public AddWitnessRequest? Request { get; set; }

		public Transaction TxCbor { get; set; }
	}
}