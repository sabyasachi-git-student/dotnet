using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;

public partial class Project_ProjectWiseStockBarcodeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter2.WriteXlsToResponse(new XlsExportOptions());
    }
    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter2.WriteXlsxToResponse(new XlsxExportOptions());
    }
}