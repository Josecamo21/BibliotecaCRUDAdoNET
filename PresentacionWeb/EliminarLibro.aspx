<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="EliminarLibro.aspx.cs" Inherits="PresentacionWeb.EliminarLibro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Eliminar Libro</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Eliminar Libro</h3>
    <hr />
    <asp:TextBox ID="txtCLibro" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="txtCAutor" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="txtApPaterno" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="txtApMaterno" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="txtCCategoria" runat="server"></asp:TextBox><br />
    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox><br />
    <%--<h4>Título: <% %></h4>--%>
</asp:Content>
