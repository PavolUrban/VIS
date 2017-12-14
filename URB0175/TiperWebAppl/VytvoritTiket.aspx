<%@ Page Language="C#"  MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="VytvoritTiket.aspx.cs" Inherits="TiperWebAppl.VytvoritTiket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div style="width:100%;">  
        <div class="leftSide">
            <div class="menuItems">
                <asp:DropDownList  ID="DropDownList1" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server">
                </asp:DropDownList>
            </div>

            <div class="menuItems">
                <asp:DropDownList ID="DropDownList3" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList4" runat="server">
                </asp:DropDownList>
            </div>
    
            <div class="menuItems">
                <asp:DropDownList ID="DropDownList5" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList6" runat="server">
                </asp:DropDownList>
            </div>
            <div class="menuItems">
                 <asp:Label ID="Label3" runat="server" Text="Celkový kurz"></asp:Label>
                </div>
            <div class="menuItems">
                <asp:Button ID="Button1" runat="server" OnClick="vytvorTiket" Text="Prepočítať" />
                </div>
            <div class="menuItems">
                <asp:TextBox ID="TextBox1" runat="server" TextMode="Number"></asp:TextBox>
                </div>
            <div class="menuItems">
                <asp:Label ID="Label4" runat="server" Text="Možná výhra"></asp:Label>
                </div>
            </div>
        
    
        <div class="rightSide">
   
            <asp:Label ID="Label5" runat="server" Text="Stav účtu"></asp:Label>
        </div>
   
    </div>
    
    
    
    

</asp:Content>