<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Project_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Password Change</h3>
            </div>
            <br />


            <div align="center">
                <table cellspacing="15px" align="center">

                    <tr>
                        <td style="font-weight: bold;">Current Password :
                        <span style="color: red;">*</span></td>
                        <td colspan="6">
                            <asp:TextBox runat="server" ID="txtCurrentPassword" TextMode="Password" Height="40px" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">New Password :
                        <span style="color: red;">*</span></td>
                        <td colspan="6">
                            <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" Height="40px" Width="250px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                    </tr>

                    <tr>

                        <td colspan="12" align="center">
                            <asp:Button runat="server" Text="Change Password" Font-Bold="true" ID="btnChangePassword" OnClick="btnChangePassword_Click" CssClass="w3-btn w3-green" />
                            <asp:Button runat="server" Text="Clear" Font-Bold="true" ID="btnClear" OnClick="btnClear_Click" CssClass="w3-btn w3-orange"  />
                        </td>

                        <td align="center">&nbsp;</td>




                    </tr>

                </table>



            </div>


            <div align="center">

                <br />
                <div align="center">
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

