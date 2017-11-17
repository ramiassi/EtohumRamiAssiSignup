using System.Net.Mail;

namespace EthumRamiAssiSignup.Services
{
    public interface IEmailQueueService
    {
        void SendMessageToQueue(MailMessage emailMessage);
        void ReceiveMessageFromQueue();
    }
}