<%@ Page Title="Order Placed" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="OrderSuccess.aspx.cs"
    Inherits="BookStoreManagementSystem.OrderSuccess" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="card shadow text-center">
    <div class="card-body">
        <h2 class="text-success">✅ Order Placed Successfully</h2>
        <p class="mt-3">Thank you for shopping with us.</p>

        <a href="Orders.aspx" class="btn btn-primary">
            View My Orders
        </a>
    </div>
</div>

</asp:Content>
