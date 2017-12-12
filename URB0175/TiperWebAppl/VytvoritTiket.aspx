<%@ Page Language="C#"  MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="VytvoritTiket.aspx.cs" Inherits="TiperWebAppl.VytvoritTiket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div class="menuItem">
        <asp:DropDownList  ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList>
    </div>

    <div class="menuItem">
    <asp:DropDownList ID="DropDownList3" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList4" runat="server">
    </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" OnClick="vytvorTiket" Text="Button" />
  </div>
    

</asp:Content>