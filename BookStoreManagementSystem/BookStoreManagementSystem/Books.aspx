<%@ Page Title="Books" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Books.aspx.cs"
    Inherits="BookStoreManagementSystem.Books" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<h3>📚 Available Books</h3>

<asp:GridView ID="gvBooks" runat="server"
    CssClass="table table-striped"
    AutoGenerateColumns="False"
    DataKeyNames="BookId"
    OnRowCommand="gvBooks_RowCommand">

    <Columns>
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="Author" HeaderText="Author" />
        <asp:BoundField DataField="Price" HeaderText="Price (₹)" />

        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button runat="server"
                    Text="Add to Cart"
                    CssClass="btn btn-sm btn-primary"
                    CommandName="Cart"
                    CommandArgument="<%# Container.DataItemIndex %>" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button runat="server"
                    Text="❤️ Wishlist"
                    CssClass="btn btn-sm btn-outline-danger"
                    CommandName="Wish"
                    CommandArgument="<%# Container.DataItemIndex %>" />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>




</asp:Content>
