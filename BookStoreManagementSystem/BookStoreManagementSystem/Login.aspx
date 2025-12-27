<%@ Page Title="Login" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="BookStoreManagementSystem.Login" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="row justify-content-center">
    <div class="col-md-4">
        <div class="card shadow">
            <div class="card-body">

                <h3 class="text-center mb-4">Login</h3>

                <div class="mb-3">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label>Password</label>
                    <asp:TextBox ID="txtPassword" runat="server"
                        TextMode="Password"
                        CssClass="form-control" />
                </div>

                <asp:Button ID="btnLogin"
                    runat="server"
                    Text="Login"
                    CssClass="btn btn-dark w-100"
                    OnClick="Login_Click" />

                <asp:Label ID="lblMessage"
                    runat="server"
                    CssClass="text-danger mt-2 d-block text-center" />

                <p class="text-center mt-3">
                    New user?
                    <a href="Register.aspx">Register</a>
                </p>

            </div>
        </div>
    </div>
</div>

</asp:Content>
