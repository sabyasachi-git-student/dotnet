﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxTreeList;
using System.Web.UI;
using DevExpress.Web;
/// <summary>
/// Summary description for CustomBranchEditForm
/// </summary>
public class CustomBranchEditForm:ITemplate
{
    private ASPxTreeList _TreeList;
    public ASPxTreeList Tree
    {
        get
        {
            return _TreeList;
        }
        set
        {
            _TreeList = value;
        }
    }
    public void InstantiateIn(Control container)
    {
        int level = 0;
        ASPxPageControl pc = new ASPxPageControl();
        pc.ID = "ASPxPageControl1";
        pc.TabPages.Add("Edit Informations");

        if (!_TreeList.IsNewNodeEditing)
        {
            ASPxLabel bname = new ASPxLabel();
            bname.Text = "Branch Name :";

            ASPxTextBox txtbName = new ASPxTextBox();
            txtbName.ID = "ASPxTextBox1";
            txtbName.Text = (container as TreeListEditFormTemplateContainer).GetValue("BranchName").ToString();
            pc.TabPages[0].Controls.Add(bname);
            pc.TabPages[0].Controls.Add(txtbName);

            ASPxLabel badd = new ASPxLabel();
            badd.Text = "Branch Address :";

            ASPxTextBox txtbAdd = new ASPxTextBox();
            txtbAdd.ID = "ASPxTextBox2";
            txtbAdd.Text = (container as TreeListEditFormTemplateContainer).GetValue("BranchAddress").ToString();
            pc.TabPages[0].Controls.Add(badd);
            pc.TabPages[0].Controls.Add(txtbAdd);

            ASPxLabel bcontact = new ASPxLabel();
            bcontact.Text = "Contact Person :";

            ASPxTextBox txtcontact = new ASPxTextBox();
            txtcontact.ID = "ASPxTextBox3";
            txtcontact.Text = (container as TreeListEditFormTemplateContainer).GetValue("ContactPerson").ToString();
            pc.TabPages[0].Controls.Add(bcontact);
            pc.TabPages[0].Controls.Add(txtcontact);

            ASPxLabel bcontactno = new ASPxLabel();
            bcontactno.Text = "Contact No :";

            ASPxTextBox txtcontactNo = new ASPxTextBox();
            txtcontactNo.ID = "ASPxTextBox4";
            txtcontactNo.Text = (container as TreeListEditFormTemplateContainer).GetValue("ContactNo").ToString();
            pc.TabPages[0].Controls.Add(bcontactno);
            pc.TabPages[0].Controls.Add(txtcontactNo);

            ASPxLabel bpriority = new ASPxLabel();
            bpriority.Text = "Priority :";

            ASPxComboBox cmbPriority = new ASPxComboBox();
            cmbPriority.ID = "ASPxComboBox1";
            cmbPriority.Items.Add("High");
            cmbPriority.Items.Add("Medium");
            cmbPriority.Items.Add("Low");
            cmbPriority.SelectedIndex = cmbPriority.Items.IndexOfText((container as TreeListEditFormTemplateContainer).GetValue("Priority").ToString());

            pc.TabPages[0].Controls.Add(bpriority);
            pc.TabPages[0].Controls.Add(cmbPriority);

            ASPxTreeListTemplateReplacement upd = new ASPxTreeListTemplateReplacement { ReplacementType = TreeListEditFormTemplateReplacementType.UpdateButton, ID = "Update" };
            pc.TabPages[0].Controls.Add(upd);

            ASPxTreeListTemplateReplacement can = new ASPxTreeListTemplateReplacement { ReplacementType = TreeListEditFormTemplateReplacementType.CancelButton, ID = "Cancel" };
            pc.TabPages[0].Controls.Add(can);
            
            
            container.Controls.Add(pc);


        }
        else
        {
            string parent = (container as TreeListEditFormTemplateContainer).Parent.ToString();
            ASPxPageControl pc1 = new ASPxPageControl();
            pc1.ID = "ASPxPageControl1";
            pc1.TabPages.Add("Add New Branch");


            ASPxLabel bname = new ASPxLabel();
            bname.Text = "Branch Name :";

            ASPxTextBox txtbName = new ASPxTextBox();
            txtbName.ID = "ASPxTextBox5";
            pc1.TabPages[0].Controls.Add(bname);
            pc1.TabPages[0].Controls.Add(txtbName);

            ASPxLabel badd = new ASPxLabel();
            badd.Text = "Branch Address :";

            ASPxTextBox txtbAdd = new ASPxTextBox();
            txtbAdd.ID = "ASPxTextBox6";
            pc1.TabPages[0].Controls.Add(badd);
            pc1.TabPages[0].Controls.Add(txtbAdd);

            ASPxLabel bcontact = new ASPxLabel();
            bcontact.Text = "Contact Person :";

            ASPxTextBox txtcontact = new ASPxTextBox();
            txtcontact.ID = "ASPxTextBox7";
            pc1.TabPages[0].Controls.Add(bcontact);
            pc1.TabPages[0].Controls.Add(txtcontact);

            ASPxLabel bcontactno = new ASPxLabel();
            bcontactno.Text = "Contact No :";

            ASPxTextBox txtcontactNo = new ASPxTextBox();
            txtcontactNo.ID = "ASPxTextBox8";
            pc1.TabPages[0].Controls.Add(bcontactno);
            pc1.TabPages[0].Controls.Add(txtcontactNo);

            ASPxLabel bpriority = new ASPxLabel();
            bpriority.Text = "Priority :";

            ASPxComboBox cmbPriority = new ASPxComboBox();
            cmbPriority.ID = "ASPxComboBox2";
            cmbPriority.Items.Add("High");
            cmbPriority.Items.Add("Medium");
            cmbPriority.Items.Add("Low");

            pc1.TabPages[0].Controls.Add(bpriority);
            pc1.TabPages[0].Controls.Add(cmbPriority);


            ASPxTreeListTemplateReplacement upd1 = new ASPxTreeListTemplateReplacement { ReplacementType = TreeListEditFormTemplateReplacementType.UpdateButton, ID = "Update1" };
            pc1.TabPages[0].Controls.Add(upd1);

            ASPxTreeListTemplateReplacement can1 = new ASPxTreeListTemplateReplacement { ReplacementType = TreeListEditFormTemplateReplacementType.CancelButton, ID = "Cancel1" };
            pc1.TabPages[0].Controls.Add(can1);

            container.Controls.Add(pc1);
        }

    }
}