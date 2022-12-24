using Autofac.Extras.Moq;
using Moq;
using DemoLib;
using NUnit.Framework;
using Shouldly;

namespace DemoLib.Tests
{
    public class AccountServiceTest
    {
        private AutoMock _mock;
        private Mock<IEmailSender> _emailSender;
        private AccountService _accountService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _emailSender = _mock.Mock<IEmailSender>();           //// By this create a mock instance of a virtual Class that implement IEmailSender;
            _accountService = _mock.Create<AccountService>();    //// By this create a mock instance of AccountService Class;
        }

        [TearDown]
        public void Teardown()
        {
            _emailSender.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }


        [Test,Category("UnitTest")]
        public void CreateAccount_ValidEmail_SendsEmail()
        {
            string username = "sajjadhossain";
            string password = "1234";
            string email = "info@devskill.com";

            _emailSender.Setup(x => x.Send(email)).Verifiable();

            _accountService.CreateAccount(username, password, email);

            _emailSender.VerifyAll();
        }



        [Test, Category("UnitTest")]
        public void GetCampaignReport_ValidCampaignName_SendsEmail()
        {
            string campaignName = "";

            string email = "sajjad@gmail.com";

            _emailSender.Setup(x => x.GetEmailSeen(campaignName)).Returns(5).Verifiable();
            _emailSender.Setup(x => x.Send(email)).Verifiable();

            _accountService.GetCampaignReport(campaignName);

            _emailSender.VerifyAll();
        }
    }
}