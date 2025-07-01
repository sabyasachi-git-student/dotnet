using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Net;

public partial class Project_PasswordRecovery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (url.IndexOf("?") > 0)
            {
                string fs = Request.QueryString["fs"];
                try
                {
                    DataTable dt = DBAccess.FetchData(String.Format("Select UserName,Password from tbl_User where ForgetString='{0}'", fs)).Tables[0];
                    ASPxLabel10.Text = dt.Rows[0][0].ToString();
                    ASPxLabel12.Text = dt.Rows[0][1].ToString();
                    bool j = DBAccess.SaveData(String.Format("Update tbl_User set ForgetString = NULL where ForgetString ='{0}'", fs));
                    ASPxLabel15.Visible = false;
                    recpw.Visible = true;
                }
                catch (Exception ex)
                {
                    ASPxLabel15.Visible = true;
                    recpw.Visible = false;
                }
            }
        }
    }
    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        bool sent = false;
        if (sent == false)
        {
            try
            {
                const string SendersAddress = "";
                string ReceiversAddress = DBAccess.FetchData(String.Format("Select MailId from tbl_User where UserName='{0}'", ASPxLabel10.Text)).Tables[0].Rows[0][0].ToString();
                const string SendersPassword = "";
                const string subject = "Software Password Recovery";
                string body = String.Format("Your UserId : {0}\n Your Password : {1}", ASPxLabel10.Text, ASPxLabel12.Text);
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(SendersAddress, SendersPassword),
                    Timeout = 30000
                };
                MailMessage message = new MailMessage(SendersAddress, ReceiversAddress, subject, body);
                smtp.Send(message);
                sent = true;
                ASPxLabel13.Visible = true;
            }
            catch (Exception ex)
            {
                sent = false;
                ASPxLabel13.Visible = false;
            }
        }
    }
}