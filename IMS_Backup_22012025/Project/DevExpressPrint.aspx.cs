using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;


public partial class Project_DevExpressPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string BranchId = "";
        if (Session["BranchId"] != null)
        {
            BranchId = Session["BranchId"].ToString();
        }
        if (Request.QueryString["AP"] != null && Request.QueryString["MInvoiceNo"] != null)
        {
            int PrintCount = 0;
            try
            {
                PrintCount = Convert.ToInt32(Request.QueryString["AP"]);
            }
            catch { PrintCount = 1; }
            string InvoiceNo = Request.QueryString["MInvoiceNo"].ToString();
            XtraReport report = null;
            report = new xrFinalInvoice();
            report.ExportOptions.Pdf.ShowPrintDialogOnOpen = true;
            report.Parameters[0].Visible = false;
            report.Parameters[1].Visible = false;
            report.Parameters[2].Visible = false;
            report.Parameters[0].Value = InvoiceNo;
            report.Parameters[1].Value = BranchId;
            report.Parameters[2].Value = PrintCount;
            ASPxDocumentViewer1.Report = report;
        }
        if (Request.QueryString["InvoiceNo"] != null)
        {
            string InvoiceNo = Request.QueryString["InvoiceNo"].ToString();          
            XtraReport report = null;
            report = new xrInvoice();
            report.ExportOptions.Pdf.ShowPrintDialogOnOpen = true;
            report.Parameters[0].Visible = false;
            report.Parameters[1].Visible = false;
            report.Parameters[2].Visible = false;
            report.Parameters[0].Value = InvoiceNo;
            report.Parameters[1].Value = BranchId;
            report.Parameters[2].Value = "ORIGINAL FOR RECIPIENT";
            ASPxDocumentViewer1.Report = report;
        }
        if (Request.QueryString["PINo"] != null)
        {
            string PINo = Request.QueryString["PINo"].ToString();
            XtraReport report = null;
            report = new xrProformaInvoice();
            report.ExportOptions.Pdf.ShowPrintDialogOnOpen = true;
            report.Parameters[0].Visible = false;
            report.Parameters[1].Visible = false;
            report.Parameters[0].Value = PINo;
            report.Parameters[1].Value = BranchId;
            ASPxDocumentViewer1.Report = report;
        }

        if (Request.QueryString["MetShipId"] != null)
        {

            string MetShipId = Request.QueryString["MetShipId"].ToString();
            XtraReport report = null;
            report = new XtraChallan();
            report.ExportOptions.Pdf.ShowPrintDialogOnOpen = true;
            report.Parameters[0].Visible = false;
            report.Parameters[0].Value = MetShipId;

            ASPxDocumentViewer1.Report = report;
        }

        if (Request.QueryString["MetShipId1"] != null)
        {

            string MetShipId1 = Request.QueryString["MetShipId1"].ToString();
            XtraReport report = null;
            report = new XtraGatePass();
            report.ExportOptions.Pdf.ShowPrintDialogOnOpen = true;
            report.Parameters[0].Visible = false;
            report.Parameters[0].Value = MetShipId1;

            ASPxDocumentViewer1.Report = report;
        }

        if (Request.QueryString["EInvoiceNo"] != null && Request.QueryString["EmailId"] != null)
        {
            string UserId = "";
            if (Session["UserId"] != null)
            {
                UserId = Session["UserId"].ToString();
            }
            string FromMail = "noreply@ironandsteel.co.in";

            string ToMail = Request.QueryString["EmailId"].ToString();           
            
            string InvoiceNo = Request.QueryString["EInvoiceNo"].ToString();
            XtraReport report = null;
            report = new xrInvoice();
            report.ExportOptions.Pdf.ShowPrintDialogOnOpen = true;
            report.Parameters[0].Visible = false;
            report.Parameters[1].Visible = false;
            report.Parameters[2].Visible = false;
            report.Parameters[0].Value = InvoiceNo;
            report.Parameters[1].Value = BranchId;
            report.Parameters[2].Value = "ORIGINAL FOR RECIPIENT";
           
            MemoryStream mem = new MemoryStream();
            report.ExportToPdf(mem);

            using (MailMessage mm = new MailMessage(FromMail, ToMail))
            {
                try
                {
                    mm.Subject = "Invoice From Shree Ji Steel Corporation.";
                    mm.Body = "Please Download The Attachment.";

                    mm.IsBodyHtml = true;

                    mem.Seek(0, System.IO.SeekOrigin.Begin);
                    Attachment att = new Attachment(mem, "Invoice.pdf", "application/pdf");
                    // Create a new message and attach the PDF report to it.

                    mm.Attachments.Add(att);
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "palm.arvixe.com";
                    NetworkCredential credential = new NetworkCredential();
                    credential.UserName = "mailtest@sapiencebs.com";
                    credential.Password = "sapience9";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = credential;
                    smtp.Port = 26;
                    smtp.EnableSsl = false;
                    smtp.Send(mm);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('MAIL SENT SUCCESSFULLY'); window.location='InvoiceReport.aspx';", true);
                    //Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage","alert('MAIL SENT SUCCESSFULLY'); window.onunload = CloseWindow();");
                }
                catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('MAIL NOT SENT'); window.location='InvoiceReport.aspx';", true); }

            }
        }
    }
    


}