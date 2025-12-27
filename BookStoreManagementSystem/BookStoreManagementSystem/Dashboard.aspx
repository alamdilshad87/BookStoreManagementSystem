<%@ Page Title="Dashboard" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="BookStoreManagementSystem.Dashboard" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="card shadow">
    <div class="card-body text-center">

        <h2>Welcome 👋</h2>

        <asp:Label ID="lblUser"
            runat="server"
            CssClass="fw-bold fs-4" />

        <hr />

        <p>Start exploring books and managing your orders.</p>

        <a href="Books.aspx" class="btn btn-primary">
            Go to Books
        </a>

    </div>
</div>

</asp:Content>
