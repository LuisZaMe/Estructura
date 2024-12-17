using Estructura.Common.Response;
using Estructura.Core.Utilities;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.DAL.Utilities
{
    public class SendMail : ISendMail
    {
        private readonly Core.ConfigurationReflection.APIConfig Config;
        public SendMail(IOptions<Core.ConfigurationReflection.APIConfig> _options)
        {
            this.Config=_options.Value;
        }
        public async Task<GenericResponse<bool>> SendSingle(string to, string body, string subject)
        {
            var unhandledError = new GenericResponse<bool>()
            {
                ErrorMessage = "Unhandled error",
                Response = false,
                StatusCode = System.Net.HttpStatusCode.OK,
                Sucess = false
            };

            try
            {
                string from = Config.Environment.EmailFrom;
                string host = Config.Environment.EmailHost;
                string pass = Config.Environment.EmailPass;
                int port = Config.Environment.EmailPort;
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse(from));
                mail.To.Add(MailboxAddress.Parse(to));
                mail.Subject = subject;
                mail.Body = new TextPart(TextFormat.Plain) { Text = body };

                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                    smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    smtp.Authenticate(from, pass);
                    smtp.Send(mail);
                    smtp.Disconnect(true);
                }

                return new GenericResponse<bool>()
                {
                    Response = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Sucess = true
                };
            }
            catch (Exception exc)
            {
                unhandledError.ErrorMessage = exc.Message;
            }
            return unhandledError;
        }
    }
}
