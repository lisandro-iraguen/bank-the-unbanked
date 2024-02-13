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
    public class PolicyManagerTests
    {
        private IPolicyManager policyManager;


        [SetUp]
        public void Setup()
        {

            var configuration = new Mock<IConfiguration>();
            policyManager = new PolicyManager(configuration.Object);
        }


    }
}