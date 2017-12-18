<%@ Page Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Vysledky.aspx.cs" Inherits="TiperWebAppl.Vysledky" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:GridView ID="VysledkyZapasov" CellPadding="10" OnRowDataBound="VysledkyZapasov_RowDataBound" runat="server" OnSelectedIndexChanged="VysledkyZapasov_SelectedIndexChanged">
     
   </asp:GridView>
    
   
    
</asp:Content>
