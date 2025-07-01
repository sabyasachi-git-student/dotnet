using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using DevExpress.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;

public partial class Project_ReqStatusReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ddlStockInId_TextChanged(object sender, EventArgs e)
     {
        string Id = ddlRequisitionId.Text;

        DataTable Requisition = DBAccess.FetchDatatable("select * from tbl_RequisitionNOC where ReqNOCId='" + Id + "' and (Status6 is null or Status6 ='')");
        if (Requisition != null && Requisition.Rows.Count > 0)
        {
            TextBox1.BackColor = System.Drawing.Color.LightBlue;
        }
        
        DataTable RequisitionPerApprove = DBAccess.FetchDatatable("select distinct a.*, (select sum(qty) from tbl_RequisitionNOCDetails where ReqNOCId=a.ReqNOCId) from tbl_RequisitionNOC a join tbl_RequisitionNOCDetails b on a.ReqNOCId=b.ReqNOCId where ((select sum(qty) from tbl_RequisitionNOCDetails where ReqNOCId=a.ReqNOCId) -isnull((select sum(Qty) from tbl_RequisitionNOCDetailsApproval where ReqNOCId=b.ReqNOCId),0))>0   and  a.ReqNOCId='" + Id + "'");
        if (RequisitionPerApprove != null && RequisitionPerApprove.Rows.Count > 0)
        {
            TextBox1.BackColor = System.Drawing.Color.RosyBrown;
            TextBox2.BackColor = System.Drawing.Color.LightBlue;
            TextBox7.BackColor = System.Drawing.Color.Orange;
        }
        DataTable RequisitionPerApprove2 = DBAccess.FetchDatatable("select distinct a.*, (select sum(qty) from tbl_RequisitionNOCDetails where ReqNOCId=a.ReqNOCId) from tbl_RequisitionNOC a join tbl_RequisitionNOCDetails b on a.ReqNOCId=b.ReqNOCId where ((select sum(qty) from tbl_RequisitionNOCDetails where ReqNOCId=a.ReqNOCId) -isnull((select sum(Qty) from tbl_RequisitionNOCDetailsApproval where ReqNOCId=b.ReqNOCId),0))=0   and  a.ReqNOCId='" + Id + "'");
        if (RequisitionPerApprove2 != null && RequisitionPerApprove2.Rows.Count > 0)
        {
            TextBox1.BackColor = System.Drawing.Color.LightGreen;
            TextBox2.BackColor = System.Drawing.Color.LightBlue;
            TextBox7.BackColor = System.Drawing.Color.Orange;
        }

        //DataTable RequisitionApprove = DBAccess.FetchDatatable("select * from tbl_RequisitionNOC where ReqNOCId='" + Id + "' and  Status6 ='Approve'");
        //if (RequisitionApprove != null && RequisitionApprove.Rows.Count > 0)
        //{
        //    TextBox1.BackColor = System.Drawing.Color.LightGreen;
        //    TextBox2.BackColor = System.Drawing.Color.LightBlue;
        //    TextBox7.BackColor = System.Drawing.Color.Orange;
        //}

        DataTable RequisitionIssue = DBAccess.FetchDatatable("select ReqId from tbl_MaterialIssue where ReqId='" + Id + "'  and  ReqId LIKE '%ReqNOC%'");
        if (RequisitionIssue != null && RequisitionIssue.Rows.Count > 0)
        {
            //TextBox1.BackColor = System.Drawing.Color.LightGreen;
            TextBox2.BackColor = System.Drawing.Color.LightGreen;
            TextBox7.BackColor = System.Drawing.Color.LightGreen;
            //TextBox4.BackColor = System.Drawing.Color.LightGreen;
            TextBox14.BackColor = System.Drawing.Color.Orange;
        }
        else
        {
           // TextBox4.BackColor = System.Drawing.Color.LightGreen;
        }

        DataTable RequisitionShipment = DBAccess.FetchDatatable("select ReqId, IssueId from tbl_MaterialIssue where  IssueId in (Select IssueId from tbl_MaterialShipment) and ReqId='" + Id + "'  and  ReqId LIKE '%ReqNOC%'");
        if (RequisitionShipment != null && RequisitionShipment.Rows.Count > 0)
        {
            //TextBox1.BackColor = System.Drawing.Color.LightGreen;
            TextBox2.BackColor = System.Drawing.Color.LightGreen;
            TextBox7.BackColor = System.Drawing.Color.LightGreen;
            TextBox14.BackColor = System.Drawing.Color.LightGreen;            
            TextBox13.BackColor = System.Drawing.Color.Orange;
        }
        else
        {
           // TextBox4.BackColor = System.Drawing.Color.LightGreen;
        }

        DataTable RequisitionReject = DBAccess.FetchDatatable("select * from tbl_RequisitionNOC where ReqNOCId='" + Id + "' and  Status6 ='Reject'");
        if (RequisitionReject != null && RequisitionReject.Rows.Count > 0)
        {
            TextBox1.BackColor = System.Drawing.Color.Red;
            TextBox2.BackColor = System.Drawing.Color.Red;
        }


        DataTable RequisitionPro = DBAccess.FetchDatatable("select * from tbl_RequisitionProject where ReqProId='" + Id + "' ");
        if (RequisitionPro != null && RequisitionPro.Rows.Count > 0)
        {
            TextBox4.BackColor = System.Drawing.Color.LightGreen;
            TextBox7.BackColor = System.Drawing.Color.Orange;
        }

        DataTable ProRequisitionShipment = DBAccess.FetchDatatable("select ReqId, IssueId from tbl_MaterialIssue where  IssueId in (Select IssueId from tbl_MaterialShipment) and ReqId='" + Id + "'  and  ReqId LIKE '%ReqPro%'");
        if (ProRequisitionShipment != null && ProRequisitionShipment.Rows.Count > 0)
        {
            TextBox4.BackColor = System.Drawing.Color.LightGreen;
            TextBox14.BackColor = System.Drawing.Color.LightGreen;
            TextBox7.BackColor = System.Drawing.Color.LightGreen;
            TextBox13.BackColor = System.Drawing.Color.Orange;
        }

       

    }
   
}