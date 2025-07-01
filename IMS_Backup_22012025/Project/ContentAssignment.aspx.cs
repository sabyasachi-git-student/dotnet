using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Utils;
using DevExpress.Web;

public partial class Project_ContentAssignment : System.Web.UI.Page
{
    CustomTreeEditForm template = new CustomTreeEditForm();
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
            string parentId = ASPxTreeList1.FocusedNode["ContentID"].ToString();
            ASPxPageControl pc = ((ASPxTreeList)sender).FindEditFormTemplateControl("ASPxPageControl1") as ASPxPageControl;
            ASPxTextBox txt1 = pc.FindControl("ASPxTextBox1") as ASPxTextBox;
            ASPxTextBox txt2 = pc.FindControl("ASPxTextBox2") as ASPxTextBox;
            ASPxComboBox cmb1 = pc.FindControl("ASPxComboBox1") as ASPxComboBox;
            ASPxComboBox cmb2 = pc.FindControl("ASPxComboBox4") as ASPxComboBox;
            ASPxComboBox cmb3 = pc.FindControl("ASPxComboBox5") as ASPxComboBox;
            ASPxComboBox cmb4 = pc.FindControl("ASPxComboBox2") as ASPxComboBox;
            ASPxTextBox txt3 = pc.FindControl("ASPxTextBox3") as ASPxTextBox;
            string contentId;
            string contentName;
            string contentLink;
            string contentType;
            string contentPosition;
            string contentVisibility;
            if (pc.ActiveTabIndex == 0)
            {
                contentName = txt1.Text;
                contentLink = "";
                contentVisibility = "V";
                contentType = getContentType(cmb1.SelectedItem.Value.ToString());
                contentId = getContentId(cmb1.SelectedItem.Value.ToString());
                contentPosition = cmb2.SelectedItem.Text;
            }
            else
            {
                contentName = txt2.Text;
                contentLink = txt3.Text;
                contentVisibility = cmb4.SelectedItem.Value.ToString();
                contentType = "Page";
                contentId = getContentId("P");
                contentPosition = cmb3.SelectedItem.Text;
            }
            string blankPosition = "";
            for(int j=cmb2.Items.Count;j>0;j--)
            {
                string pos = DBAccess.FetchData(String.Format("Select count(*) from tbl_Contents where ParentContentID='{0}' and ContentPosition='{1}'", parentId, j)).Tables[0].Rows[0][0].ToString();                
                if(pos=="0")
                {
                    blankPosition=j.ToString();
                }
            }
            if (contentPosition !=blankPosition )
            {
                string replacedId = DBAccess.FetchData(String.Format("Select ContentId from tbl_Contents where ParentContentID='{0}' and ContentPosition='{1}'", parentId, contentPosition)).Tables[0].Rows[0][0].ToString();
                bool u = DBAccess.SaveData(String.Format("Update tbl_Contents set ContentPosition='{0}' where ContentId='{1}'", blankPosition, replacedId));
            }
            SqlDataSource1.InsertCommand = String.Format("Insert into tbl_Contents(ContentID,ContentName,ContentType,ContentLink,ContentVisibility,ParentContentID,ContentPosition) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", contentId, contentName, contentType, contentLink,contentVisibility, parentId,contentPosition);
            bool t = DBAccess.SaveData(String.Format("Insert into tbl_Access(ContentsId) values('{0}')", contentId));
            DataBind();
        }

    }
    protected string getContentId(string idIn)
    {
        Int32 count = Convert.ToInt32(DBAccess.FetchData(String.Format("Select Coalesce(Max(Convert(int,Right(ContentID,(LEN(ContentID)-1)))),'0') from tbl_Contents where ContentID like '{0}%'", idIn)).Tables[0].Rows[0][0]);
        string id = idIn + (count + 1);
        return id;
    }
    protected string getContentType(string contentins)
    {
        string type = "";
        switch (contentins)
        {
            case "P":
                type = "Page";
                break;
            case "S":
                type = "SubFolder";
                break;
            case "F":
                type = "Folder";
                break;
        }
        return type;
    }
    protected void ASPxTreeList1_CommandColumnButtonInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListCommandColumnButtonEventArgs e)
    {
        TreeListNode node = null;
        if (e.NodeKey != null)
        {
            node = ASPxTreeList1.FindNodeByKeyValue(e.NodeKey);
            if (e.ButtonType == TreeListCommandColumnButtonType.New)
            {
                if (node.GetValue("ContentType").ToString() == "Page")
                {
                    e.Visible = DefaultBoolean.False;
                }
            }
            if (e.ButtonType == TreeListCommandColumnButtonType.Edit || e.ButtonType == TreeListCommandColumnButtonType.Delete)
            {
                if (node.GetValue("ContentType").ToString() == "Root")
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
            string parentId = ASPxTreeList1.FocusedNode["ParentContentID"].ToString();
            ASPxPageControl pc = ((ASPxTreeList)sender).FindEditFormTemplateControl("ASPxPageControl1") as ASPxPageControl;
            ASPxTextBox txt1 = pc.FindControl("ASPxTextBox1") as ASPxTextBox;
            ASPxTextBox txt2 = pc.FindControl("ASPxTextBox2") as ASPxTextBox;
            ASPxComboBox cmb1 = pc.FindControl("ASPxComboBox1") as ASPxComboBox;
            ASPxComboBox cmb2 = pc.FindControl("ASPxComboBox3") as ASPxComboBox;
            ASPxComboBox cmb3 = pc.FindControl("ASPxComboBox2") as ASPxComboBox;
            e.NewValues["ContentID"] = getContentId(cmb1.SelectedItem.Value.ToString());
            string parentType = DBAccess.FetchData("Select ContentType from tbl_Contents where ContentId='"+e.NewValues["ParentContentID"]+"'").Tables[0].Rows[0][0].ToString();
            if ((parentType != "Root" && e.OldValues["ContentType"].ToString() == "Folder") || (parentType != "Folder" && e.OldValues["ContentType"].ToString() == "SubFolder")|| (parentType=="Page"))
            {
                throw new Exception("This Change Is Impossible");
            }
            else
            {
                string contentType;

                if (cmb2.SelectedItem.Text != e.OldValues["ContentPosition"].ToString())
                {
                    string replacedId = DBAccess.FetchData(String.Format("Select ContentId from tbl_Contents where ParentContentID='{0}' and ContentPosition='{1}'", parentId, cmb2.SelectedItem.Text)).Tables[0].Rows[0][0].ToString();
                    bool u = DBAccess.SaveData(String.Format("Update tbl_Contents set ContentPosition='{0}' where ContentId='{1}'", e.OldValues["ContentPosition"], replacedId));
                }
                if (parentId != e.NewValues["ParentContentID"].ToString())
                {
                    Int32 maxPos = Convert.ToInt32(DBAccess.FetchData("Select Coalesce(Max(ContentPosition),0) from tbl_Contents where ParentContentID='" + e.NewValues["ParentContentID"].ToString() + "'").Tables[0].Rows[0][0]);
                    //Int32 count = Convert.ToInt32(DBAccess.FetchData("Select Coalesce(Max(ContentPosition),0) from tbl_Contents where ParentContentID='" + e.NewValues["ParentContentID"].ToString() + "' and ").Tables[0].Rows[0][0]);
                    e.NewValues["ContentPosition"] = maxPos + 1;
                    Int32 premaxPos = Convert.ToInt32(DBAccess.FetchData("Select Max(ContentPosition) from tbl_Contents where ParentContentID='" + parentId + "'").Tables[0].Rows[0][0]);
                    if (Convert.ToInt32(e.OldValues["ContentPosition"]) != premaxPos)
                    {
                        bool u = DBAccess.SaveData("Update tbl_Contents set ContentPosition=(ContentPosition-1) where ParentContentID='" + parentId + "' and ContentPosition>" + e.OldValues["ContentPosition"] + "");
                    }
                }
                else
                {
                    e.NewValues["ContentPosition"] = cmb2.SelectedItem.Text;
                }
                e.NewValues["ContentType"] = contentType = getContentType(cmb1.SelectedItem.Value.ToString());
                if (contentType == "Page")
                {
                    e.NewValues["ContentVisibility"] = cmb3.SelectedItem.Value.ToString();
                }
                else
                {
                    e.NewValues["ContentVisibility"] = "V";
                }
                e.NewValues["ContentName"] = txt1.Text;
                e.NewValues["ContentLink"] = txt2 == null ? "" : txt2.Text;
            }
        }

    }
    protected void ASPxTreeList1_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        SqlDataSource1.DeleteCommand = String.Format("EXEC delTreeNode @ContentID='{0}'", e.Values["ContentID"]);
        bool d = DBAccess.SaveData(String.Format("Delete tbl_Access where ContentsId='{0}'", e.Values["ContentID"]));
        DataBind();
    }
}