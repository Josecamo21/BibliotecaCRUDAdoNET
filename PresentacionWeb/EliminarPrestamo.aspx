<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="EliminarPrestamo.aspx.cs" Inherits="PresentacionWeb.EliminarPrestamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="container">
        <div class="card-header">
            <h3>Eliminar Prestamo</h3>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Titulo: <% = ViewState["_titulo"] %></h4>
            <h6 class="card-title">Codigo Prestamo: <% = Session["_clavePrestamo"] %></h6>
            <h6 class="card-title">Usuario: <% = ViewState["_usuario"] %></h6>
            <h6 class="card-title">Fecha Prestamo: <% = ViewState["_fechaPrestamo"] %></h6>
            <h6 class="card-title">Fecha Devolucion: <% = ViewState["_fechaDevolucion"] %></h6>

            <hr />
            <p class="card-text">El prestamo se eliminaría permanentemente! Confirma que desea eliminarlo?</p>
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-warning" OnClick="btnCancelar_Click" />
        </div>
    </div>
</asp:Content>
