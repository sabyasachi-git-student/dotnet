﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
//using WebSite1;

public partial class Account_Manage : System.Web.UI.Page
{
    protected string SuccessMessage
    {
        get;
        private set;
    }

    protected bool CanRemoveExternalLogins
    {
        get;
        private set;
    }

    //private bool HasPassword(UserManager manager)
    //{
    //    var user = manager.FindById(User.Identity.GetUserId());
    //    return (user != null && user.PasswordHash != null);
    //}

    protected void Page_Load()
    {
        //if (!IsPostBack)
        //{
        //    // Determine the sections to render
        //    UserManager manager = new UserManager();
        //    if (HasPassword(manager))
        //    {
        //        changePasswordHolder.Visible = true;
        //    }
        //    else
        //    {
        //        setPassword.Visible = true;
        //        changePasswordHolder.Visible = false;
        //    }
        //    CanRemoveExternalLogins = manager.GetLogins(User.Identity.GetUserId()).Count() > 1;

        //    // Render success message
        //    var message = Request.QueryString["m"];
        //    if (message != null)
        //    {
        //        // Strip the query string from action
        //        Form.Action = ResolveUrl("~/Account/Manage");

        //        SuccessMessage =
        //            message == "ChangePwdSuccess" ? "Your password has been changed."
        //            : message == "SetPwdSuccess" ? "Your password has been set."
        //            : message == "RemoveLoginSuccess" ? "The account was removed."
        //            : String.Empty;
        //        successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
        //    }
        //}
    }

    protected void ChangePassword_Click(object sender, EventArgs e)
    {
        //if (IsValid)
        //{
        //    UserManager manager = new UserManager();
        //    IdentityResult result = manager.ChangePassword(User.Identity.GetUserId(), CurrentPassword.Text, NewPassword.Text);
        //    if (result.Succeeded)
        //    {
        //        Response.Redirect("~/Account/Manage?m=ChangePwdSuccess");
        //    }
        //    else
        //    {
        //        AddErrors(result);
        //    }
        //}
    }

    protected void SetPassword_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            // Create the local login info and link the local account to the user
            //UserManager manager = new UserManager();
            //IdentityResult result = manager.AddPassword(User.Identity.GetUserId(), password.Text);
            //if (result.Succeeded)
            //{
                //Response.Redirect("~/Account/Manage?m=SetPwdSuccess");
            //}
            //else
            //{
              //  AddErrors(result);
            //}
        }
    }

    //public IEnumerable<UserLoginInfo> GetLogins()
    //{
    //    //string str="Hello";
    //    //UserManager manager = new UserManager();
    //    //var accounts = manager.GetLogins(User.Identity.GetUserId());
    //    //CanRemoveExternalLogins = accounts.Count() > 1 || HasPassword(manager);
    //    //return accounts;
    //    //return str ;
    //}

    //public void RemoveLogin(string loginProvider, string providerKey)
    //{
    //    UserManager manager = new UserManager();
    //    var result = manager.RemoveLogin(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
    //    var msg = result.Succeeded
    //        ? "?m=RemoveLoginSuccess"
    //        : String.Empty;
    //    Response.Redirect("~/Account/Manage" + msg);
    //}

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error);
        }
    }
}