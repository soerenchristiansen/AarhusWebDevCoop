using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult ContactForm()
        {
            var model = new ContactModel();
            return PartialView("ContactForm", model);
        }

        [HttpPost]
        public ActionResult ContactForm(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Comment");

                comment.SetValue("name", model.Name);
                comment.SetValue("email", model.Email);
                comment.SetValue("subject", model.Subject);
                comment.SetValue("message", model.Message);

                Services.ContentService.Save(comment);

                var sb = new StringBuilder();
                sb.AppendFormat("<p>Message: {0}</p>", model.Message);
                sb.AppendFormat("<p>Subject: {0}</p>", model.Subject);
                sb.AppendFormat("<p>Email: {0}</p>", model.Email);

                var message = new MailMessage();

                //message.To.Add(new MailAddress(ConfigurationManager.AppSettings["ContactAddress"]));
                message.Subject = model.Subject;
                message.From = new MailAddress(model.Email, model.Name);
                message.Body = sb.ToString();
                message.IsBodyHtml = true;
                TempData["success"] = true;

                //using (var smtp = new SmtpClient())
                //{
                    
                //    await smtp.SendMailAsync(message);
                //}

                return RedirectToCurrentUmbracoPage();
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}