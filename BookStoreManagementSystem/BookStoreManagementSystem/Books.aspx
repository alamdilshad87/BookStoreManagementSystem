<%@ Page Title="Books" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Books.aspx.cs"
    Inherits="BookStoreManagementSystem.Books" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<h3>Available Books</h3>

<asp:GridView ID="gvBooks" runat="server"
    CssClass="table table-striped"
    AutoGenerateColumns="False"
    DataKeyNames="BookId"
    OnRowCommand="gvBooks_RowCommand">

    <Columns>
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="Author" HeaderText="Author" />
        <asp:BoundField DataField="Price" HeaderText="Price" />

        <asp:ButtonField Text="Add to Cart"
            CommandName="Cart"
            ControlStyle-CssClass="btn btn-sm btn-primary" />

        <asp:ButtonField Text="Wishlist ❤️"
            CommandName="Wish"
            ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
    </Columns>

</asp:GridView>

</asp:Content>
