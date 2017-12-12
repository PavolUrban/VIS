<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TiperWebAppl.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="main.css">
</head>
<body>
    <form class="loginWrap materialCard" id="form1" runat="server">
        <div ID="logintText">Prihláste sa prosím</div>
        <asp:TextBox ID="loginInput" AutoCompleteType="Disabled" runat="server" placeholder="Email"></asp:TextBox>
        <asp:TextBox ID="passwordInput" TextMode="Password" runat="server" placeholder="Heslo"></asp:TextBox>
        <asp:Button ID="confirmButton" runat="server" Text="Prihlásiť" OnClick="confirmButton_Click"/>
    </form>
</body>
</html>
