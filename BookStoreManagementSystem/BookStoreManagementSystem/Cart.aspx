<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Cart.aspx.cs"
    Inherits="BookStoreManagementSystem.CartPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<h3>My Cart</h3>

<asp:GridView ID="gvCart" runat="server"
    CssClass="table table-bordered"
    AutoGenerateColumns="False">

    <Columns>
        <asp:BoundField DataField="Book.Title" HeaderText="Book" />
        <asp:BoundField DataField="Book.Price" HeaderText="Price" />
        <asp:BoundField DataField="Quantity" HeaderText="Qty" />
    </Columns>

</asp:GridView>

</asp:Content>
