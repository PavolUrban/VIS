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
       
  </div>
    
     <div class="menuItem">
    <asp:DropDownList ID="DropDownList5" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList6" runat="server">
    </asp:DropDownList>
       </div>
    
    <asp:Label ID="Label3" runat="server" Text="Celkový kurz"></asp:Label>
     <asp:Button ID="Button1" runat="server" OnClick="vytvorTiket" Text="Prepočítať" />
    
    

    
    
    
    

    <asp:TextBox ID="TextBox1" runat="server" TextMode="Number"></asp:TextBox>
    <asp:Label ID="Label4" runat="server" Text="Možná výhra"></asp:Label>
    <asp:Label ID="Label5" runat="server" Text="Stav účtu"></asp:Label>
    
    

    
    
    
    

</asp:Content>