<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Railtel Corporation</title>
    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../plugins/fontawesome-free/css/all.min.css">
    <!-- icheck bootstrap -->
    <link rel="stylesheet" href="../../plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/adminlte.min.css">
    <link rel="stylesheet" href="style.css">
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server">
        <div class="container">
            <header>Login Form</header>
            <div class="input-field">
                <asp:TextBox ID="txtUserName" class="form-control" runat="server" placeholder="Username"></asp:TextBox>
            </div>
            <div class="input-field">
                <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
            </div>
            <div class="button">
           
                    <asp:Button ID="btnLogin" class="btn btn-primary btn-block" runat="server" Text="Login" OnClick="btnLogin_Click" ViewStateMode="Disabled" />

            </div>
            <div class="button">
                
                    <asp:Button ID="btnResLogin" class="btn btn-primary btn-block" runat="server" Text="Login With Responsive" Visible="false" OnClick="btnResLogin_Click" ViewStateMode="Disabled" />

            </div>

           
        </div>
        <!-- jQuery -->
        <script>
            var input = document.querySelector('.pswrd');
            var show = document.querySelector('.show');
            show.addEventListener('click', active);
            function active() {
                if (input.type === "password") {
                    input.type = "text";
                    show.style.color = "#1DA1F2";
                    show.textContent = "HIDE";
                } else {
                    input.type = "password";
                    show.textContent = "SHOW";
                    show.style.color = "#111";
                }
            }
        </script>
    </form>
</body>
</html>