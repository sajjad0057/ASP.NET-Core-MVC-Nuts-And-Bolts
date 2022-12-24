namespace DemoLib
{
    public interface IEmailSender
    {
        void Send(string email);
        int GetEmailSeen(string campaignName);
    }
}