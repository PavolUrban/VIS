<%@ Page Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="TiperoveTikety.aspx.cs" Inherits="TiperWebAppl.TiperoveTikety" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>

    <asp:GridView ID="MojTikety" CellPadding="10" OnRowDataBound="MojTikety_RowDataBound" runat="server" OnSelectedIndexChanged="MojTikety_SelectedIndexChanged">
     
   </asp:GridView>
    
<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Filtrovať" />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Overiť výhernosť" />
    
</asp:Content>
