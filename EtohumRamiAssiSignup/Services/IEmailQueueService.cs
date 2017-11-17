using System.Net.Mail;

namespace EtohumRamiAssiSignup.Services
{
    public interface IEmailQueueService
    {
        void SendMessageToQueue(MailMessage emailMessage);
        void ReceiveMessageFromQueue();
    }
}