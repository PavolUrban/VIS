<%@ Page Language="C#"  MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Ponuka.aspx.cs" Inherits="TiperWebAppl.Ponuka" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:GridView ID="kurzovaPonuka" CellPadding="10" OnRowDataBound="KurzovaPonuka_RowDataBound" runat="server" OnSelectedIndexChanged="kurzovaPonuka_SelectedIndexChanged">
     
   </asp:GridView>
    

</asp:Content>