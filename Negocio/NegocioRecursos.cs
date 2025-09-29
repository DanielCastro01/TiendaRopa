using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Negocio
{
    public class NegocioRecursos
    {


        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("juancitotwt@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smpt = new SmtpClient()
                {
                    Credentials = new NetworkCredential("juancitotwt@gmail.com", "kfimyeeibsaequps"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smpt.Send(mail);
                resultado = true;

            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }


        public static string ConvertirEnBase64(string ruta, out bool conversion)
        {
            string texto = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                texto = Convert.ToBase64String(bytes);
            }
            catch
            {
                conversion = false;
            }

            return texto;
        }

        public static string ConvertirSha64(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}
