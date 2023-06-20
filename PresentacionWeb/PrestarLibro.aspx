<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="PrestarLibro.aspx.cs" Inherits="PresentacionWeb.PrestarLibro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Prestamo Libro</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="card-header text-center">
        <h2>Prestamo de Libros</h2>
        </div>
        <asp:TextBox ID="txtClaveUsuario" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtUsuario" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtDireccion" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtPrestamo" runat="server" Enabled="False"></asp:TextBox>
        <asp:GridView runat="server" ID="dgvClientes" AutoGenerateColumns="False" CellPadding="6" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" EmptyDataText="No se han encontrado Usuarios en la Base de Datos" OnPageIndexChanging="dgvClientes_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Seleccionar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSeleccionar" runat="server" CommandArgument='<%# Eval("claveUsuario").ToString() %>' OnCommand="lnkSeleccionar_Command"><i class="fa-solid fa-check"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="claveUsuario" HeaderText="Clave Cliente" />
                <asp:BoundField DataField="cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="email" HeaderText="Correo" />
                <asp:BoundField DataField="direccion" HeaderText="Direccion" />
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

        <br /> <br /> <hr /> <br />

        <asp:TextBox ID="txtClaveEjemplar" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtClaveLibro" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtTitulo" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtCondicion" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtEstado" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtEditorial" runat="server" Enabled="False"></asp:TextBox>
        <asp:TextBox ID="txtPaginas" runat="server" Enabled="False"></asp:TextBox>
        <asp:GridView ID="dgvEjemplares" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="6" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgvEjemplares_PageIndexChanging" Width="100%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Seleccionar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSeleccionar" runat="server" CommandArgument='<%# Eval("claveEjemplar").ToString() %>' OnCommand="lnkSeleccionar_Command1"><i class="fa-solid fa-check"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="claveEjemplar" HeaderText="Ejemplar" />
                <asp:BoundField DataField="claveLibro" HeaderText="Clave Libro" />
                <asp:BoundField DataField="titulo" HeaderText="Titulo" />
                <asp:BoundField DataField="claveCondicion" HeaderText="Condicion" />
                <asp:BoundField DataField="claveEstado" HeaderText="Estado" />
                <asp:BoundField DataField="claveEditorial" HeaderText="Editorial" />
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
        
        <br /> <br /> <hr /> <br />

        <asp:Button ID="btnPrestar" runat="server" Text="Realizar Prestamo" OnClick="btnPrestar_Click" />
    </div>
</asp:Content>
