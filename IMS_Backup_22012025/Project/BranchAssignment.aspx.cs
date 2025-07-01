using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Utils;

public partial class Project_BranchAssignment : System.Web.UI.Page
{
    CustomBranchEditForm template = new CustomBranchEditForm();
    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxTreeList1.Templates.EditForm = template;
        template.Tree = ASPxTreeList1;
        if (!IsPostBack)
        {
            DataBind();
            ASPxTreeList1.ExpandToLevel(2);
        }
    }
    protected void ASPxTreeList1_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (ASPxTreeList1.FocusedNode != null)
        {
            string parentId = ASPxTreeList1.FocusedNode["BranchId"].ToString();
            string branchId = getBranchId();
            ASPxPageControl pc = ((ASPxTreeList)sender).FindEditFormTemplateControl("ASPxPageControl1") as ASPxPageControl;
            ASPxTextBox txt1 = pc.FindControl("ASPxTextBox5") as ASPxTextBox;
            ASPxTextBox txt2 = pc.FindControl("ASPxTextBox6") as ASPxTextBox;
            ASPxTextBox txt3 = pc.FindControl("ASPxTextBox7") as ASPxTextBox;
            ASPxTextBox txt4 = pc.FindControl("ASPxTextBox8") as ASPxTextBox;
            ASPxComboBox cmb1 = pc.FindControl("ASPxComboBox2") as ASPxComboBox;

            SqlDataSource1.InsertCommand = String.Format("Insert Into tbl_Branch(BranchId,BranchName,BranchAddress,ContactPerson,ContactNo,Priority,Parentid) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", branchId, txt1.Text, txt2.Text, txt3.Text, txt4.Text, cmb1.SelectedItem.Text, parentId);
            bool t = DBAccess.SaveData(String.Format("Insert into tbl_BranchAccess(BranchIds) values('{0}')", branchId));
            string password = "12345",usergroup="UG3",mailid="",ForgetString="";
            bool user = DBAccess.SaveData("insert into tbl_User values('" + branchId + "','" + password + "','" + usergroup + "','" + mailid + "','"+ForgetString +"')");
            
            
            DataBind();
        }
    }
    protected string getBranchId()
    {
        Int32 serial = (Int32)DBAccess.FetchData("Select Coalesce(Max(Convert(int,Right(BranchId,(LEN(BranchId)-2)))),'0') from tbl_Branch where BranchId!='Main'").Tables[0].Rows[0][0];
        Int32 s = serial + 1;
        string brid = "GD" + s;
        return brid;
    }
    protected void ASPxTreeList1_CommandColumnButtonInitialize(object sender, TreeListCommandColumnButtonEventArgs e)
    {
        TreeListNode node = null;
        if (e.NodeKey != null)
        {
            node = ASPxTreeList1.FindNodeByKeyValue(e.NodeKey);
            if (e.ButtonType == TreeListCommandColumnButtonType.Delete)
            {
                string branch = node.GetValue("BranchName").ToString();
                string parent = DBAccess.FetchData(String.Format("Select Coalesce(ParentId,'0') from tbl_Branch where BranchName='{0}'", branch)).Tables[0].Rows[0][0].ToString();
                string branchId = DBAccess.FetchData(String.Format("Select BranchId from tbl_Branch where BranchName='{0}'", branch)).Tables[0].Rows[0][0].ToString();
                int count = 0;
                //int count = (Int32)DBAccess.FetchData(String.Format("Select Count(*) from tbl_Consignment where brfrom='{0}'", branchId)).Tables[0].Rows[0][0];
                //count =count + (Int32)DBAccess.FetchData(String.Format("Select Count(*) from tbl_Consignment where brto='{0}'", branchId)).Tables[0].Rows[0][0];
                if (parent == "0" || count>0)
                {
                    e.Visible = DefaultBoolean.False;
                }
            }
            
        }
    }
    protected void ASPxTreeList1_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (ASPxTreeList1.FocusedNode != null)
        {
            string parentId = ASPxTreeList1.FocusedNode["ParentId"].ToString();
            ASPxPageControl pc = ((ASPxTreeList)sender).FindEditFormTemplateControl("ASPxPageControl1") as ASPxPageControl;
            ASPxTextBox txt1 = pc.FindControl("ASPxTextBox1") as ASPxTextBox;
            ASPxTextBox txt2 = pc.FindControl("ASPxTextBox2") as ASPxTextBox;
            ASPxTextBox txt3 = pc.FindControl("ASPxTextBox3") as ASPxTextBox;
            ASPxTextBox txt4 = pc.FindControl("ASPxTextBox4") as ASPxTextBox;
            ASPxComboBox cmb1 = pc.FindControl("ASPxComboBox1") as ASPxComboBox;

            e.NewValues["BranchName"] = txt1.Text;
            e.NewValues["BranchAddress"] = txt2.Text;
            e.NewValues["ContactPerson"] = txt3.Text;
            e.NewValues["ContactNo"] = txt4.Text;
            e.NewValues["Priority"] = cmb1.SelectedItem.Text;
        }
    }
    protected void ASPxTreeList1_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        SqlDataSource1.DeleteCommand = String.Format("Delete tbl_Branch where BranchId='{0}'", e.Values["BranchId"]);
        bool d = DBAccess.SaveData(String.Format("Delete tbl_BranchAccess where BranchIds='{0}'", e.Values["BranchID"]));
        DataBind();
    }
}