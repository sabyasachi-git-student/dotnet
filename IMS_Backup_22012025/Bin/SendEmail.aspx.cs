using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class Project_SendEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SendEmail(object sender, EventArgs e)
    {
        string to = txtTo.Text;
        string from = txtEmail.Text;
        string subject = txtSubject.Text;
        //string body = "<Html> <body> <br> '" + txtBody.Text + "'  <br>  </body></Html>";

      
        

        using (MailMessage mm = new MailMessage(txtEmail.Text, txtTo.Text))
        {
            mm.Subject = txtSubject.Text;
            string body = txtBody.Text + "<br/><img src=cid:signature>";
            mm.Body = body;
            //string path = Server.MapPath(".").ToString();
            string path = Request.PhysicalApplicationPath.ToString()+"Images\\signature.png";
            LinkedResource myimage = new LinkedResource(path);
           
            AlternateView htmlMail = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            myimage.ContentId = "signature";
            htmlMail.LinkedResources.Add(myimage);
            mm.AlternateViews.Add(htmlMail);

           if (fuAttachment.HasFile)
            {
                string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
            }
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
           //smtp.Host = "smtp.mail.yahoo.com";
            //smtp.Host = "mail.sapiencebs.com";
           smtp.Host = "mail.vishalprofin.in";
           smtp.EnableSsl = false;//
           txtPassword.Text = "vppl123";
            NetworkCredential NetworkCred = new NetworkCredential(txtEmail.Text,"vppl123");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            //smtp.Port = 25;//port for yahoo.com
            smtp.Port = 26;//port for sapience and vishal
          // smtp.Port = 465;
            mm.CC.Add(new MailAddress("vishal@vishalprofin.in"));
            smtp.Send(mm);
            txtSubject.Text = "";
            txtEmail.Text = "";
            txtBody.Text = "";
            txtTo.Text = "";
            
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
        }
    }
    
}