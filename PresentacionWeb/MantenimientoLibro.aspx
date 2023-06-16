﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="MantenimientoLibro.aspx.cs" Inherits="PresentacionWeb.MantenimientoLibro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mantenimiento Libros</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card-header">
            <h3>Mantenimiento de Libros</h3>
        </div>
        <br />
        <div class="row mt-3">
             <!--columa 1-->
            <div class="col-2">
                <asp:Label ID="Label1" runat="server" Text="Clave Libro:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtClaveLibro" runat="server" CssClass="form-control"></asp:TextBox>
            </div><!-- fin columa 1-->

            <!--columa 2-->
            <div class="col-2">
                <asp:Label ID="Label2" runat="server" Text="Titulo:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
            </div><!-- fin columa 2-->

            <!--columa 3-->
            <div class="col-4">
                <asp:TextBox ID="txtIdAutor" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="Autor:" CssClass="form-label"></asp:Label>
                <div class="input-group mb-3">
                    <input type="button" id="btnModalAutor" class="btn btn-outline-primary" value="Buscar" data-bs-toggle="modal" data-bs-target="#modalAutor" />
                    <asp:TextBox ID="txtAutor" runat="server" ReadOnly="true" CssClass="form-control" aria-describedby="btnModalAutor"></asp:TextBox>
                </div>
            </div><!-- fin columa 3-->


            <!--columa 4-->
            <div class="col-4">
                <asp:TextBox ID="txtIdCategoria" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="Categoria:" CssClass="form-label"></asp:Label>
                <div class="input-group mb-3">
                    <input type="button" id="btnModalCategoria" class="btn btn-outline-primary" value="Buscar" data-bs-toggle="modal" data-bs-target="#XXXXXX" />
                    <asp:TextBox ID="txtCategoria" runat="server" ReadOnly="true" CssClass="form-control" aria-describedby="btnModalCategoria"></asp:TextBox>
                </div>
            </div><!-- fin columa 4-->

        </div>

            <!-- Modal -->
            <div class="modal fade" id="modalAutor" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Buscar Autor</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <asp:TextBox ID="txtFiltroAutor" runat="server"></asp:TextBox>
                      <asp:Button ID="btnFiltroAutor" runat="server" Text="Buscar" CssClass="btn btn-primary" />
                      <asp:GridView ID="dgvAutores" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                          <Columns>
                              <asp:TemplateField>
                                  <ItemTemplate>
                                      <asp:LinkButton ID="lnkSelecccionar" runat="server" CommandArgument='<%# Eval("claveAutor").ToString() %>' ToolTip="Seleccionar"><i class="fa-solid fa-check"></i>Select</asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="claveAutor" HeaderText="Clave Autor" />
                              <asp:BoundField DataField="apPaterno" HeaderText="Apellido 1" />
                              <asp:BoundField DataField="apMaterno" HeaderText="Apellido 2" />
                              <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                          </Columns>
                          <EditRowStyle BackColor="#999999" />
                          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                          <SortedAscendingCellStyle BackColor="#E9E7E2" />
                          <SortedAscendingHeaderStyle BackColor="#506C8C" />
                          <SortedDescendingCellStyle BackColor="#FFFDF8" />
                          <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                      </asp:GridView>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>

                  </div>
                </div>
              </div>
            </div>
    </div>

</asp:Content>