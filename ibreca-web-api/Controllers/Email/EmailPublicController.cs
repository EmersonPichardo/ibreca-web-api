using ibreca_web_api.Controllers.Email.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ibreca_web_api.Controllers.Announcements
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailPublicController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> Send(EmailDataDto emailData)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("emersonthebest11@gmail.com", "kgcsdvgpwyqosnqw"),
                    EnableSsl = true
                };

                emailData.Message += $"\n\n" +
                    $"Nombre: {(string.IsNullOrWhiteSpace(emailData.Name) ? "Anónimo" : emailData.Name)}\n" +
                    $"Contacto: {(string.IsNullOrWhiteSpace(emailData.Contact) ? "Anónimo" : emailData.Contact)}\n" +
                    $"Correo: {(string.IsNullOrWhiteSpace(emailData.Email) ? "Anónimo" : emailData.Email)}";

                smtpClient.Send("emersonthebest11@gmail.com", "ibrca01@gmail.com", "Mensaje desde ibreca.org", emailData.Message);

                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
    }
}