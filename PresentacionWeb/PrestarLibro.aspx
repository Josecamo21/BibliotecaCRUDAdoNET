<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="PrestarLibro.aspx.cs" Inherits="PresentacionWeb.PrestarLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Prestamo Libro</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card-header text-center">
            <h2>Prestamo de Libros</h2>
        </div>

        <div class="row mt-3">
            <!--columa 1-->
            <div class="col-2">
                <asp:Label ID="Label1" runat="server" Text="Clave Prestamo:" CssClass="form-label"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Clave del Libro Necesario" ControlToValidate="txtClaveLibro" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                <asp:TextBox ID="txtClavePrestamo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <!-- fin columa 1-->

            <!--columa 2-->
            <div class="col-4">
                <asp:TextBox ID="txtIdUsuario" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="Usuario:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIdUsuario" ErrorMessage="Usuario Necesario" ForeColor="Red">*</asp:RequiredFieldValidator>
                <div class="input-group mb-3">
                    <input type="button" id="btnModalPrestamo" class="btn btn-outline-primary" value="Buscar" data-bs-toggle="modal" data-bs-target="#modalUsuario" />
                    <asp:TextBox ID="txtUsuario" runat="server" ReadOnly="true" CssClass="form-control" aria-describedby="btnModalPrestamo"></asp:TextBox>
                </div>
            </div>
            <!-- fin columa 2-->


            <!--columa 3-->
            <div class="col-4">
                <asp:TextBox ID="txtIdEjemplar" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="Ejemplar:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIdEjemplar" ErrorMessage="Ejemplar Necesaria" ForeColor="Red">*</asp:RequiredFieldValidator>
                <div class="input-group mb-3">
                    <input type="button" id="btnModalEjemplar" class="btn btn-outline-primary" value="Buscar" data-bs-toggle="modal" data-bs-target="#modalEjemplar" />
                    <asp:TextBox ID="txtEjemplar" runat="server" ReadOnly="true" CssClass="form-control" aria-describedby="btnModalEjemplar"></asp:TextBox>
                </div>
            </div>
            <!-- fin columa 3-->


            <!--columa 4-->
            <div class="col-2">
                <asp:Label ID="Label5" runat="server" Text="Fecha Prestamo:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechaPrestamo" ErrorMessage="Titulo Necesario" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtFechaPrestamo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
            <!-- fin columa 4-->


            <!--columa 5-->
            <div class="col-2">
                <asp:Label ID="Label6" runat="server" Text="Fecha Devolucion:" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFechaDevolucion" ErrorMessage="Fecha Necesario" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
            <!-- fin columa 5-->

            <hr />
            <div class="card-header">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-warning" ValidationGroup="Cancelar" OnClick="btnCancelar_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </div>
        </div>
        <br />
        <hr />
        <br />
        <br />
        <br />



        <!-- Modal Usuarios-->
        <div class="modal fade" id="modalUsuario" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel1">Buscar Usuario</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtFiltroAutor" runat="server"></asp:TextBox>
                        <asp:Button ID="btnFiltroAutor" runat="server" Text="Buscar" CssClass="btn btn-primary" />
                        <asp:GridView runat="server" ID="dgvClientes" AutoGenerateColumns="False" CellPadding="6" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" EmptyDataText="No se han encontrado Usuarios en la Base de Datos" OnPageIndexChanging="dgvClientes_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSeleccionar" runat="server" CommandArgument='<%# Eval("claveUsuario").ToString() %>' OnCommand="lnkSeleccionar_Command"><i class="fa-solid fa-check"></i>dsa</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="claveUsuario" HeaderText="Clave Cliente" />
                                <asp:BoundField DataField="cliente" HeaderText="Cliente" />
                                <%--<asp:BoundField DataField="email" HeaderText="Correo" />
                                <asp:BoundField DataField="direccion" HeaderText="Direccion" />--%>
                                <asp:BoundField DataField="claveEstado" HeaderText="Prestamos" />
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
        <!--Fin Modal Usuarios-->



        <!-- Modal Ejemplares-->
        <div class="modal fade" id="modalEjemplar" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel1">Buscar Usuario</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="Buscar" CssClass="btn btn-primary" />
                        <asp:GridView ID="dgvEjemplares" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="6" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgvEjemplares_PageIndexChanging" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSeleccionar" runat="server" CommandArgument='<%# Eval("claveEjemplar").ToString() %>' OnCommand="lnkSeleccionar_Command1"><i class="fa-solid fa-check"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="claveEjemplar" HeaderText="Ejemplar" />
                                <%--<asp:BoundField DataField="claveLibro" HeaderText="Clave Libro" />--%>
                                <asp:BoundField DataField="titulo" HeaderText="Titulo" />
                                <%--<asp:BoundField DataField="claveCondicion" HeaderText="Condicion" />--%>
                                <asp:BoundField DataField="claveEstado" HeaderText="Estado" />
                                <%--<asp:BoundField DataField="claveEditorial" HeaderText="Editorial" />--%>
                                <asp:BoundField DataField="numeroPaginas" HeaderText="Paginas" />
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
        <!--Fin Modal Ejemplares-->
    </div>
</asp:Content>
