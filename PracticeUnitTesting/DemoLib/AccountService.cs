using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
    public class AccountService
    {
        private readonly IEmailSender _emailSender;
        public AccountService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public void CreateAccount(string username, string password,string email)
        {
            //// Code to insert in database using unit of work 

            _emailSender.Send(email);
        }
    }
}
