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


            var epoc = new ProtocolParameters();
            epoc.EpochNo = 8;
            var expectedParameters = new ProtocolParameters[] { epoc };
            var apiResponse = new ApiResponse<ProtocolParameters[]>(new HttpResponseMessage(System.Net.HttpStatusCode.OK), expectedParameters, null);
            epochClient.Setup(x => x.GetProtocolParameters(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
               .ReturnsAsync(apiResponse);

            transactionService = new TransactionService(configuration.Object, policyManager.Object, addressClient.Object, networkClient.Object, epochClient.Object);
        }


    }
}