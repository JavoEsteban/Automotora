
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;


namespace Automotriz.Models

{
    public class Correo
    {
        public bool EnviarMail(string destinatario, string asunto, Vehiculos vehiculo)
        {
            try
            {
                string FromEmail = "thinkagiledevelopment@gmail.com"; //mail origen, se necesitan las credenciales posteriormente!
                string ToEmail = destinatario; //mail destino

                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress(FromEmail);

                //Configurando cliente SMTP
                SmtpClient SMTP = new SmtpClient();
                SMTP.Port = 587;
                SMTP.EnableSsl = true;
                SMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                SMTP.UseDefaultCredentials = false;

                //Credenciales del correo de origen
                SMTP.Credentials = new NetworkCredential("thinkagiledevelopment@gmail.com", "thinkagile0132"); //Credenciales correo hackmonkeys, solo desarrollo.
                SMTP.Host = "smtp.gmail.com";

                mail.To.Add(new MailAddress(ToEmail));

                MailAddress cc = new MailAddress("mbulos@automotrizlarrain.cl");
                MailAddress cc2 = new MailAddress("aruggeri@automotrizlarrain.cl");
                MailAddress cc3 = new MailAddress("cgorlitz@automotrizlarrain.cl");
                MailAddress cc4 = new MailAddress("mortiz@automotrizlarrain.cl");
                MailAddress cco = new MailAddress("javier.gomez@hackmonkeys.cl");
                mail.CC.Add(cc);
                mail.CC.Add(cc2);
                mail.CC.Add(cc3);
                mail.CC.Add(cc4);
                mail.Bcc.Add(cco);


                mail.Subject = asunto; //Asunto del mail

                //Creando body HTML
                //string stringEmail = "";

                //stringEmail = "<Table style=' background-color:#299be8;color:black;width: 100%; padding-bottom: 35px;' >";
                //stringEmail = stringEmail + "<tr>";
                //stringEmail = stringEmail + "<td align = 'center' valign='top' colspan='3' style=' color:white;'>";
                //stringEmail = stringEmail + "<img src = 'https://www.automotrizlarrain.cl/assets/img/logoblc.png' height='48' width='142' style='margin: 26px 20px 20px 15px;' />";

                //stringEmail = stringEmail + "</td>";

                //stringEmail = stringEmail + "</tr>";


                //stringEmail = stringEmail + "<tr>";

                //stringEmail = stringEmail + "<td align = 'center' colspan='3'>";
                //stringEmail = stringEmail + "<h3>Nuevo vehículo ingresado a preparación</h3>";
                //stringEmail = stringEmail + "</td>";

                //stringEmail = stringEmail + "</tr>";

                //stringEmail = stringEmail + "<tr>";
                //stringEmail = stringEmail + "<td align = 'center' colspan='3' style='margin-right: 139px'><label>Vehículo:" + vehiculo.PATENTE + " </label></td>";
                //stringEmail = stringEmail + "</tr>";

                //stringEmail = stringEmail + "<tr>";
                //stringEmail = stringEmail + "<td align = 'center' colspan='3'><label>Estado: disponible para su preparación</label></td>";
                //stringEmail = stringEmail + "</tr>";

                //stringEmail = stringEmail + "<tr>";
                //stringEmail = stringEmail + "<td align = 'center' colspan='3' style='color:white;'><label>Fecha: "+vehiculo.FECHA_INGRESO+"</label></td>";
                //stringEmail = stringEmail + "</tr>";

                //stringEmail = stringEmail + " </table>";


                string StrHtml = "<Table>";
                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td align = 'center' valign='top' colspan='2'>";

                StrHtml = StrHtml + "<img src = 'https://fotos.subefotos.com/4213c9eba19093a846493db28d6f016eo.png'height='65' width='180' ' />";
                StrHtml = StrHtml + "<br/>";

                StrHtml = StrHtml + "<h3> NOTIFICACIÓN NUEVO VEHÍCULO INGRESADO</h3>";

                StrHtml = StrHtml + "</td>";
                StrHtml = StrHtml + "</tr>";
                StrHtml = StrHtml + "</tr>";

                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td><span>PATENTE </span></td>";
                StrHtml = StrHtml + "<td><b><label>" + vehiculo.PATENTE.ToUpper() + "</label><b></td>";
                StrHtml = StrHtml + "</tr>";



                StrHtml = StrHtml + "<tr>";
                StrHtml = StrHtml + "<td><span> FECHA INGRESO</span></td>";
                StrHtml = StrHtml + "<td><Label>" + vehiculo.FECHA_INGRESO + "</label> </td>";
                StrHtml = StrHtml + "</tr>";

                StrHtml = StrHtml + " </table>";

                //Crear archivo PDF

                //var routeDir = "/ArchivosGenerados/";
                //DirectoryInfo dir = Directory.CreateDirectory(routeDir);

                //var Renderer = new IronPdf.HtmlToPdf();
                //var PDF = Renderer.RenderHtmlAsPdf(stringEmail);
                //var nombreDoc = "NombreArchivoGenerado.pdf";

                //PDF.SaveAs("/ArchivosGenerados/" + nombreDoc);

                //Attachment archivoAttach = new Attachment(routeDir + nombreDoc);
                //archivoAttach.Name = nombreDoc;


                //mail.Attachments.Add(archivoAttach); //Adjuntando archivo generado en el mail

                mail.Body = StrHtml;
                mail.IsBodyHtml = true;

                SMTP.Send(mail);

                //controlar el retorno! a modo de prueba se configura como true
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}