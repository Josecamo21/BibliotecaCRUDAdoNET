<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="MantenimientoLibro.aspx.cs" Inherits="PresentacionWeb.MantenimientoLibro" %>
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Clave del Libro Necesario" ControlToValidate="txtClaveLibro" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtClaveLibro" runat="server" CssClass="form-control"></asp:TextBox>
            </div><!-- fin columa 1-->

            <!--columa 2-->
            <div class="col-2">
                <asp:Label ID="Label2" runat="server" Text="Titulo:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitulo" ErrorMessage="Titulo Necesario" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
            </div><!-- fin columa 2-->

            <!--columa 3-->
            <div class="col-4">
                <asp:TextBox ID="txtIdAutor" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="Autor:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIdAutor" ErrorMessage="Autor Necesario" ForeColor="Red">*</asp:RequiredFieldValidator>
                <div class="input-group mb-3">
                    <input type="button" id="btnModalAutor" class="btn btn-outline-primary" value="Buscar" data-bs-toggle="modal" data-bs-target="#modalAutor" />
                    <asp:TextBox ID="txtAutor" runat="server" ReadOnly="true" CssClass="form-control" aria-describedby="btnModalAutor"></asp:TextBox>
                </div>
            </div><!-- fin columa 3-->


            <!--columa 4-->
            <div class="col-4">
                <asp:TextBox ID="txtIdCategoria" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="Categoria:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIdCategoria" ErrorMessage="Categoria Necesaria" ForeColor="Red">*</asp:RequiredFieldValidator>
                <div class="input-group mb-3">
                    <input type="button" id="btnModalCategoria" class="btn btn-outline-primary" value="Buscar" data-bs-toggle="modal" data-bs-target="#modalCategorias" />
                    <asp:TextBox ID="txtCategoria" runat="server" ReadOnly="true" CssClass="form-control" aria-describedby="btnModalCategoria"></asp:TextBox>
                </div>
            </div><!-- fin columa 4-->
            <hr />
            <div class="card-header">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-warning" OnClick="btnCancelar_Click" ValidationGroup="Cancelar" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </div>
        </div>

            <!-- Modal Autores-->
            <div class="modal fade" id="modalAutor" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Buscar Autor</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <asp:TextBox ID="txtFiltroAutor" runat="server"></asp:TextBox>
                      <asp:Button ID="btnFiltroAutor" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnFiltroAutor_Click" />
                      <asp:GridView ID="dgvAutores" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnPageIndexChanging="dgvAutores_PageIndexChanging1">
                          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                          <Columns>
                              <asp:TemplateField HeaderText="Seleccionar">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="lnkSelecccionar" runat="server" CommandArgument='<%# Eval("claveAutor").ToString() %>' ToolTip="Seleccionar" OnCommand="lnkSelecccionar_Command"><i class="fa-solid fa-check"></i></asp:LinkButton>
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
            </div> <!--Fin Modal Autores-->

            <!-- Modal Categorias-->
            <div class="modal fade" id="modalCategorias" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel1">Buscar Categoria</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                      <asp:TextBox ID="txtFiltroCategorias" runat="server"></asp:TextBox>
                      <asp:Button ID="btnFiltroCategorias" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnFiltroCategorias_Click" />
                      <asp:GridView ID="dgvCategorias" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnPageIndexChanging="dgvCategorias_PageIndexChanging">
                          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                          <Columns>
                              <asp:TemplateField HeaderText="Seleccionar">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="lnkSeleccionar" runat="server" CommandArgument='<%# Eval("claveCategoria").ToString() %>' ToolTip="Seleccionar" OnCommand="lnkSeleccionar_Command"><i class="fa-solid fa-check"></i></asp:LinkButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="claveCategoria" HeaderText="Clave Categoria" />
                              <asp:BoundField DataField="descripcion" HeaderText="Categoria" />
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
            </div> <!--Fin Modal Categorias-->
    </div>
    
    
</asp:Content>
