using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Messaging;
using System.Net.Mail;
using System.Diagnostics;

namespace EtohumRamiAssiSignup.Services
{
    /**
     MSMQ stands for Microsoft Message Queuing is one of the most reliable way to sending and receiving messages
     from one system to another system; both system can be located at different geographical locations. 
     The benefit of MSMQ is that the receiving application doesn't have to run at the time of sending the message.
     Messages are stored in the queue of the system (Microsoft Operating System) and once the receiving application runs,
     it starts peeking the messages one by one.
         **/
    public class EmailQueueService : IEmailQueueService
    {
        const string queueName = @".\private$\TestQueue";
        public void SendMessageToQueue(MailMessage emailMessage)
        {
            // check if queue exists, if not create it
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))
            {
                //Create message queue instance 
                msMq = MessageQueue.Create(queueName);
            }

            else
            {
                // use the existed queue
                msMq = new MessageQueue(queueName);
            }

            try
            {
                // Create new instance of Message object
                Message msmqMsg = new Message();

                // Assign email to the Message body
                msmqMsg.Body = emailMessage;

                // Recoverable indicates message is guaranteed 
                msmqMsg.Recoverable = true;

                // Formatter to serialize the object,I’m using binary serialization
                msmqMsg.Formatter = new BinaryMessageFormatter();

                //If the Message queue does not exists at specified 
                //location create it
                if (!MessageQueue.Exists(queueName))
                {
                    msMq = MessageQueue.Create(queueName);
                }

                //  Set Formatter to serialize the object. 
                msMq.Formatter = new XmlMessageFormatter();

                //Send the message object in the created queue
                msMq.Send(msmqMsg);
            }

            catch (MessageQueueException ex)
            {
                Debug.Write(ex.ToString());
            }

            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }

            finally
            {
                // It is good practice to close the queue to save resources
                msMq.Close();
            }

            // Log success message sending result
            Debug.WriteLine("Message sent ......");
        }

        public void ReceiveMessageFromQueue()
        {
            // check if queue exists, if not create it
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))
            {
                //Create message queue instance 
                msMq = MessageQueue.Create(queueName);
            }

            else
            {
                // use the existed queue
                msMq = new MessageQueue(queueName);
            }

            try
            {
                msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(MailMessage) });

                var message = (MailMessage)msMq.Receive().Body;

                Debug.WriteLine("From: " + message.From + ", Body: " + message.Body);

                Debug.WriteLine(message.Body.ToString());
            }
            catch (MessageQueueException ex)

            {
                Debug.Write(ex.ToString());
            }

            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }

            finally
            {
                // It is good practice to close the queue to save resources
                msMq.Close();
            }

            // Log success message receive result
            Debug.WriteLine("Message received ......");
        }
    }
}