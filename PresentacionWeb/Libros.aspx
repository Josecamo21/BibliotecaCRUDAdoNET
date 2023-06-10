<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Libros.aspx.cs" Inherits="PresentacionWeb.Libros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Libros</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
            <div class="card-header text-center">
                <h2>Gestion de Libros</h2>
            </div>

            <%if (Session["_Err"] != null) {%>
                <% = Session["_Err"] %>

                <div class="alert alert-warning" role="alert">

                    <button type="button" class="btn-close" aria-label="Close" data-bs-dismiss="alert"></button>
                </div>

            <%}%>
            

            <%if (Session["_Exito"]!=null) {%>

                <div class="alert alert-primary" role="alert">
                    <% = Session["_Exito"] %>
                    
                    <button type="button" class="btn-close" aria-label="Close" data-bs-dismiss="alert"></button>
                </div>

            <%}%>




                <asp:GridView ID="dgvLibros" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="6" ForeColor="#333333" GridLines="None" PageSize="15" Width="100%" EmptyDataText="No se han encontrado Libros en la Base de Datos" OnPageIndexChanging="dgvLibros_PageIndexChanging">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="claveLibro" HeaderText="Clave Libro" />
                        <asp:BoundField DataField="titulo" HeaderText="Título" />
                        <asp:BoundField DataField="claveAutor" HeaderText="Clave Autor" />
                        <asp:BoundField DataField="autor" HeaderText="Autor" />
                        <asp:BoundField DataField="claveCategoria" HeaderText="Clave Categoría" />
                        <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("claveLibro").ToString() %>' OnCommand="lnkEliminar_Command">Eliminar</asp:LinkButton>
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
