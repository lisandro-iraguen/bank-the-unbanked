using Api.Services.Policy;
using Api.Services.Transaction;
using Api.Services.Wallet;
using CardanoSharp.Koios.Client;
using CardanoSharp.Koios.Client.Contracts;
using Microsoft.Extensions.Configuration;
using Moq;
using Refit;
using System.Net;

namespace Test
{
    public class TransactionTests
    {
        private ITransactionService transactionService;


        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IConfiguration>();
            var policyManager = new Mock<IPolicyManager>();
            var addressClient = new Mock<IAddressClient>();
            var networkClient = new Mock<INetworkClient>();
            var epochClient = new Mock<IEpochClient>();


           
        }

    }
}