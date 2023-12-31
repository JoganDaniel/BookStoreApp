﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using Experimental.System.Messaging;

namespace BookStoreCommon.Model
{
    public class MSMQ
    {
        MessageQueue messageQueue = new MessageQueue();

        public void sendData2Queue(String token, string email)
        {
            messageQueue.Path = @".\private$\token";
            if (!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += (sender, e) => MessageQ_ReceiveCompleted(sender, e, email);  //Delegate
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }
        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e, string email)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string subject = "Book Store App Reset Link";
                string body = "https://localhost:5002/api/User/ResetPassword"+token;
                var SMTP = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("shrey0683@gmail.com", "dtijkisinohllanp"),
                    EnableSsl = true

                };
                SMTP.Send("shrey0683@gmail.com", "jogandaniel7@gmail.com", subject, body);
                messageQueue.BeginReceive();
            }
            catch (MessageQueueException)
            {
                throw;
            }
        }
    }
}
