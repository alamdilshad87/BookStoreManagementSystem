<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="AdminAddBook.aspx.cs"
    Inherits="BookStoreManagementSystem.AdminAddBook" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<h3>Add Book (Admin)</h3>

<asp:TextBox ID="txtTitle" runat="server" CssClass="form-control mb-2" Placeholder="Title" />
<asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control mb-2" Placeholder="Author" />
<asp:TextBox ID="txtPrice" runat="server" CssClass="form-control mb-2" Placeholder="Price" />

<asp:Button Text="Add Book" runat="server"
    CssClass="btn btn-success"
    OnClick="AddBook_Click" />

</asp:Content>
