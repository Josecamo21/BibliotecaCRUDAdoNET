<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="EliminarLibro.aspx.cs" Inherits="PresentacionWeb.EliminarLibro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Eliminar Libro</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container">
        <div class="card-header">
            <h3>Eliminar Libros</h3>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Titulo: <% = ViewState["_titulo"] %></h4>
            <h6 class="card-title">Codigo Libro: <% = Session["_claveLibro"] %></h6>
            <h6 class="card-title">Autor: <% = ViewState["_autor"] %></h6>
            <h6 class="card-title">Categoria: <% = ViewState["_categoria"] %></h6>
            <hr />
            <p class="card-text">El libró se eliminaría permanentemente! Confirma que desea eliminarlo?</p>
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-warning" OnClick="btnCancelar_Click" />
        </div>
    </div>
</asp:Content>
