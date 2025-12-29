<%@ Page Title="Cart" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Cart.aspx.cs"
    Inherits="BookStoreManagementSystem.CartPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<h3>🛒 My Cart</h3>

<asp:GridView ID="gvCart" runat="server"
    CssClass="table table-bordered"
    AutoGenerateColumns="False"
    DataKeyNames="CartId"
    OnRowDeleting="gvCart_RowDeleting">

    <Columns>
        <asp:BoundField DataField="Book.Title" HeaderText="Book" />

        <asp:BoundField DataField="Book.Price"
            HeaderText="Price (₹)"
            DataFormatString="{0:N2}" />

        <asp:BoundField DataField="Quantity"
            HeaderText="Qty" />

        <%-- Item Total --%>
        <asp:TemplateField HeaderText="Total (₹)">
            <ItemTemplate>
                <%# (Convert.ToDecimal(Eval("Book.Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("N2") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
</asp:GridView>


<div class="text-end mt-3">
    <h4>
        Grand Total: ₹
        <asp:Label ID="lblGrandTotal"
            runat="server"
            CssClass="fw-bold" />
    </h4>
</div>



<asp:Button ID="btnPlaceOrder"
    runat="server"
    Text="Place Order"
    CssClass="btn btn-primary mt-3"
    OnClick="PlaceOrder_Click" />


</asp:Content>
