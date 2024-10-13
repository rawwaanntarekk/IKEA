using LinkDev.IKEA.DAL.Models.Mails;


namespace LinkDev.IKEA.PL.Helpers
{
    public interface IMailSettings
    {
        void SendEmail(Email email);
    }
}
