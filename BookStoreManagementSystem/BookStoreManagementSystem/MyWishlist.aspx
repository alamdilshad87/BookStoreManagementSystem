<%@ Page Title="Wishlist" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="MyWishlist.aspx.cs"
    Inherits="BookStoreManagementSystem.MyWishlist" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<h3>❤️ Wishlist</h3>

<asp:GridView ID="gvWishlist" runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="WishlistId"
    OnRowCommand="gvWishlist_RowCommand"
    CssClass="table table-striped">

    <Columns>
        <asp:BoundField DataField="Book.Title" HeaderText="Book" />
        <asp:BoundField DataField="Book.Author" HeaderText="Author" />
        <asp:BoundField DataField="Book.Price" HeaderText="Price (₹)" />

        <asp:ButtonField Text="❌ Remove"
            CommandName="Remove"
            ButtonType="Button"
            ControlStyle-CssClass="btn btn-sm btn-danger" />
    </Columns>
</asp:GridView>




</asp:Content>
