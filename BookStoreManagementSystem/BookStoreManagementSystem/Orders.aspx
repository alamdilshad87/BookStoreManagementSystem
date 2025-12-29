<%@ Page Title="My Orders" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Orders.aspx.cs"
    Inherits="BookStoreManagementSystem.Orders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<h3>📦 My Orders</h3>

<asp:Repeater ID="rptOrders" runat="server">
    <ItemTemplate>
        <div class="card mb-3 shadow-sm">
            <div class="card-header">
                <strong>Order #<%# Eval("OrderId") %></strong>
                <span class="float-end">
                    <%# Eval("OrderDate", "{0:dd MMM yyyy}") %>
                </span>
            </div>

            <div class="card-body">
                <p><strong>Total:</strong> ₹<%# Eval("TotalAmount") %></p>

                <ul>
                    <%# GetOrderItems((int)Eval("OrderId")) %>
                </ul>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

</asp:Content>
