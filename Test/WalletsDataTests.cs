using Api.Wallet;
using Api.WebData.Developers;
using CardanoSharp.Wallet;
using Data.Wallet;

namespace Test
{
    public class WalletsDataTests
    {
        private IWalletData walletData;


        [SetUp]
        public void Setup()
        {
            walletData = new WalletData();

        }

        [Test]
        public async Task GetWalletData_ShouldReturnAllWallets()
        {
            // Arrange - no need for arrangement as data is hardcoded in the service

            // Act
            var result = await walletData.GetWalletData();

            // Assert
            Assert.IsNotNull(result);
            var walletList = result.ToList();
            Assert.AreEqual(5, walletList.Count);

            // Assuming you have a WalletExtension.Equals method or equivalent for comparison
            Assert.IsTrue(walletList.Contains(new WalletExtension { Key = "eternl", Name = "Eternl", Url = "https://eternl.io" }));
            Assert.IsTrue(walletList.Contains(new WalletExtension { Key = "nami", Name = "Nami", Url = "https://namiwallet.io" }));
            Assert.IsTrue(walletList.Contains(new WalletExtension { Key = "gerowallet", Name = "Gero", Url = "https://gerowallet.io" }));
            Assert.IsTrue(walletList.Contains(new WalletExtension { Key = "typhoncip30", Name = "Typhon", Url = "https://typhonwallet.io" }));
            Assert.IsTrue(walletList.Contains(new WalletExtension { Key = "flint", Name = "Flint", Url = "https://flint-wallet.com" }));
        }

    }
}