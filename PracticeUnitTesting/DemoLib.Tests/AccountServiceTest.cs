using Autofac.Extras.Moq;
using Moq;
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
        public void Sum_ValidNumbers_ValidSum()
        {
            string username = "sajjadhossain";
            string password = "1234";
            string email = "info@devskill.com";

            _emailSender.Setup(x => x.Send(email)).Verifiable();

            _accountService.CreateAccount(username, password, email);

            _emailSender.VerifyAll();
        }
    }
}