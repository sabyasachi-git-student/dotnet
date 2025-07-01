using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DevExpress.Web;

public partial class Project_AddUser : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ASPxGridView3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string id = getUserGroupId();
        string name = e.NewValues["UserGroup"].ToString();
        SqlDataSource1.InsertCommand = String.Format("Insert Into tbl_UserGroups values('{0}','{1}')", id, name);
        bool i = DBAccess.SaveData(String.Format("Alter table tbl_Access add {0} int", name));
        bool u = DBAccess.SaveData(String.Format("Update tbl_Access set {0}=0", name));
        bool ii = DBAccess.SaveData(String.Format("Alter table tbl_BranchAccess add {0} int", name));
        bool uu = DBAccess.SaveData(String.Format("Update tbl_BranchAccess set {0}=0", name));
    }

    protected string getUserGroupId()
    {
        int serial = (int)DBAccess.FetchData("Select Coalesce(Max(Convert(int,Right(UserGroupId,(LEN(UserGroupId)-2)))),'0') from tbl_UserGroups where UserGroupId!='I'").Tables[0].Rows[0][0];
        int s = serial + 1;
        string ugi = "UG" + s;
        return ugi;
    }

    protected void ASPxGridView3_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string name = e.Values["UserGroup"].ToString();
        bool d = DBAccess.SaveData(String.Format("Alter table tbl_Access drop column {0}", name));
        bool dd = DBAccess.SaveData(String.Format("Alter table tbl_BranchAccess drop column {0}", name));
    }

    protected void ASPxGridView3_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            string ugid = ASPxGridView3.GetRowValues(e.VisibleIndex, ASPxGridView3.KeyFieldName).ToString();
            int count = (int)DBAccess.FetchData(String.Format("Select Count(*) from tbl_User where UserGroup='{0}'", ugid)).Tables[0].Rows[0][0];
            if (count > 0)
            {
                e.Visible = false;
            }
        }
    }

    protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string a = e.NewValues["UserGroup"].ToString();
        if (e.NewValues["UserGroup"].ToString() == "I")
        {
            e.NewValues["UserGroup"] = e.NewValues["UserName"];
            bool i = DBAccess.SaveData(String.Format("Alter table tbl_Access add {0} int", e.NewValues["UserName"]));
            bool u = DBAccess.SaveData(String.Format("Update tbl_Access set {0}=0", e.NewValues["UserName"]));
            bool ii = DBAccess.SaveData(String.Format("Alter table tbl_BranchAccess add {0} int", e.NewValues["UserName"]));
            bool uu = DBAccess.SaveData(String.Format("Update tbl_BranchAccess set {0}=0", e.NewValues["UserName"]));
        }
    }

    protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (e.OldValues["UserGroup"].ToString() == e.OldValues["UserName"].ToString())
        {
            if (e.OldValues["UserGroup"].ToString() != e.NewValues["UserGroup"].ToString())
            {
                bool d = DBAccess.SaveData(String.Format("Alter table tbl_Access drop column {0}", e.OldValues["UserName"]));
                bool dd = DBAccess.SaveData(String.Format("Alter table tbl_BranchAccess drop column {0}", e.OldValues["UserName"]));
            }
        }
        else if (e.OldValues["UserGroup"].ToString() != e.OldValues["UserName"].ToString())
        {
            if (e.NewValues["UserGroup"].ToString() == "I")
            {
                e.NewValues["UserGroup"] = e.NewValues["UserName"];
                bool i = DBAccess.SaveData(String.Format("Alter table tbl_Access add {0} int", e.NewValues["UserName"]));
                bool u = DBAccess.SaveData(String.Format("Update tbl_Access set {0}=0", e.NewValues["UserName"]));
                bool ii = DBAccess.SaveData(String.Format("Alter table tbl_BranchAccess add {0} int", e.NewValues["UserName"]));
                bool uu = DBAccess.SaveData(String.Format("Update tbl_BranchAccess set {0}=0", e.NewValues["UserName"]));
            }
        }
    }
    protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (e.Values["UserGroup"].ToString() == e.Values["UserName"].ToString())
        {
            bool d = DBAccess.SaveData(String.Format("Alter table tbl_Access drop column {0}", e.Values["UserName"]));
            bool dd = DBAccess.SaveData(String.Format("Alter table tbl_BranchAccess drop column {0}", e.Values["UserName"]));            
        }
    }
    protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ASPxGridView3.DataBind();
    }
    protected void ASPxGridView2_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (ASPxGridView2.IsEditing && e.Column.FieldName=="UserName" && !ASPxGridView2.IsNewRowEditing)
        {
            e.Editor.ReadOnly = true;
        }
    }
}