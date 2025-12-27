<%@ Page Title="Register" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Register.aspx.cs"
    Inherits="BookStoreManagementSystem.Register" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="row justify-content-center">
    <div class="col-md-5">
        <div class="card shadow">
            <div class="card-body">

                <h3 class="text-center mb-4">Create Account</h3>

                <div class="mb-3">
                    <label>Full Name</label>
                    <asp:TextBox ID="txtName" runat="server"
                        CssClass="form-control" />
                </div>

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

                <asp:Button ID="btnRegister"
                    runat="server"
                    Text="Register"
                    CssClass="btn btn-success w-100"
                    OnClick="Register_Click" />

                <asp:Label ID="lblMessage"
                    runat="server"
                    CssClass="text-danger mt-2 d-block text-center" />

                <p class="text-center mt-3">
                    Already have an account?
                    <a href="Login.aspx">Login</a>
                </p>

            </div>
        </div>
    </div>
</div>

</asp:Content>
