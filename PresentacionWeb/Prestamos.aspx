<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Prestamos.aspx.cs" Inherits="PresentacionWeb.Prestamos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Prestamos</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
            <div class="card-header text-center">
                <h2>Gestion de Prestamos</h2>
        </div>
            <hr />
            <asp:TextBox ID="txtFiltroLibro" runat="server"></asp:TextBox>
            <asp:Button ID="btnFiltroLibro" runat="server" Text="Buscar" CssClass="btn btn-primary" />
            <asp:Button style="float:right" ID="btnNuevoPrestamo" runat="server" Text="Agregar Prestamo" CssClass="btn btn-outline-primary" OnClick="btnNuevoPrestamo_Click" />
            <hr />  
            <br />
            <%if (Session["_Err"] != null) {%>
                <div class="alert alert-danger" role="alert">

                    <%= Session["_Err"]%>
                    <button type="button" class="btn-close" aria-label="Close" data-bs-dismiss="alert"></button>

                </div>
            <%}%>
            

            <%if (Session["_Exito"] != null) {%>
                <div class="alert alert-success" role="alert">

                    <%= Session["_Exito"] %>
                    <button type="button" class="btn-close" aria-label="Close" data-bs-dismiss="alert"></button>
                    
                </div>

            <%}%>




                <asp:GridView ID="dgvPrestamos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="6" ForeColor="#333333" GridLines="None" PageSize="15" Width="100%" EmptyDataText="No se han encontrado Prestamos en la Base de Datos" OnPageIndexChanging="dgvPrestamos_PageIndexChanging" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="clavePrestamo" HeaderText="Clave Prestamo" />
                        <asp:BoundField DataField="claveEjemplar" HeaderText="Clave Ejemplar" Visible="False" />
                        <asp:BoundField DataField="titulo" HeaderText="Ejemplar" />
                        <asp:BoundField DataField="claveUsuario" HeaderText="Usuario" Visible="False" />
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="fechaPrestamo" HeaderText="Fecha Prestamo" />
                        <asp:BoundField DataField="fechaDevolucion" HeaderText="Fecha Devolucion" />
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("clavePrestamo").ToString() %>' ToolTip="Eliminar" OnCommand="lnkEliminar_Command"><i class="fa-solid fa-trash-can"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkModificar" runat="server" CommandArgument='<%# Eval("clavePrestamo").ToString() %>' ToolTip="Modificar" OnCommand="lnkModificar_Command"><i class="fa-regular fa-pen-to-square"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
</asp:Content>
