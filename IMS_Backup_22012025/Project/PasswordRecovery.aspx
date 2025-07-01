<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="Project_PasswordRecovery"  Title="Password Recovery"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style5
        {
            width: 100%;
        }
        .style6
        {
        }
        .style7
        {
        }
        .style8
        {
            width: 583px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Font-Bold="True" 
                    Theme="BlackGlass" Font-Size="Large" Font-Underline="False" 
                    Text="Error : Sorry Invalid Request!!!:(" Visible="False">
                </dx:ASPxLabel>
    <table class="style5" style="background-color: #FFFFFF" runat="server" id="recpw">
        <tr>
            <td class="style6" align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6" align="center" colspan="2">
                <dx:ASPxLabel ID="ASPxLabel14" runat="server" Font-Bold="True" 
                    Theme="BlackGlass" Font-Size="Large" Font-Underline="True" 
                    Text="User Password Recovery">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="style6" align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6" align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" class="style8">
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="True" Text="Your Id :" 
                    Theme="BlackGlass">
                </dx:ASPxLabel>
            </td>
            <td align="left">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="True" 
                    Theme="BlackGlass">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td align="right" class="style8">
                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Font-Bold="True" 
                    Text="Your Password :" Theme="BlackGlass">
                </dx:ASPxLabel>
            </td>
            <td align="left">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Font-Bold="True" 
                    Theme="BlackGlass">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7" align="center" colspan="2">
                <dx:ASPxButton ID="ASPxButton1" runat="server" onclick="ASPxButton1_Click" 
                    Text="Send Credentials To My Mail Id" Theme="BlackGlass" Width="276px">
                </dx:ASPxButton>
                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Font-Bold="True" 
                    Text="* Mail With Your Credentials Sent Successfully!!!" Theme="BlackGlass" 
                    Visible="False" ForeColor="Green">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

