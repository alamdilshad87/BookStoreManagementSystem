<%@ Page Title="Dashboard" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="BookStoreManagementSystem.Dashboard" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="container">

    <!-- Welcome -->
    <div class="mb-4 text-center">
        <h2 class="fw-bold">Welcome 👋</h2>
        <asp:Label ID="lblUser"
            runat="server"
            CssClass="fs-4 text-primary fw-semibold" />
        <p class="text-muted mt-2">
            Manage your books, cart, and wishlist from here.
        </p>
    </div>

    <!-- Dashboard Cards -->
    <div class="row g-4">

        <!-- Books -->
        <div class="col-md-3">
            <div class="card shadow-sm text-center h-100">
                <div class="card-body">
                    <h5 class="card-title">📚 Books</h5>
                    <p class="card-text">Browse available books</p>
                    <a href="Books.aspx" class="btn btn-primary w-100">
                        View Books
                    </a>
                </div>
            </div>
        </div>

        <!-- Cart -->
        <div class="col-md-3">
            <div class="card shadow-sm text-center h-100">
                <div class="card-body">
                    <h5 class="card-title">🛒 Cart</h5>
                    <p class="card-text">Items you added</p>
                    <a href="Cart.aspx" class="btn btn-success w-100">
                        View Cart
                    </a>
                </div>
            </div>
        </div>

        <!-- Wishlist -->
        <div class="col-md-3">
            <div class="card shadow-sm text-center h-100">
                <div class="card-body">
                    <h5 class="card-title">❤️ Wishlist</h5>
                    <p class="card-text">Saved books</p>
                    <a href="MyWishlist.aspx" class="btn btn-danger w-100">
                        View Wishlist
                    </a>
                </div>
            </div>
        </div>

        <!-- Admin (hidden for users) -->
        <asp:Panel ID="pnlAdmin" runat="server" Visible="false"
            CssClass="col-md-3">
            <div class="card shadow-sm text-center h-100 border-warning">
                <div class="card-body">
                    <h5 class="card-title">🛠 Admin</h5>
                    <p class="card-text">Manage books</p>
                    <a href="Admin/Books.aspx" class="btn btn-warning w-100">
                        Admin Panel
                    </a>
                </div>
            </div>
        </asp:Panel>

    </div>
</div>

</asp:Content>
