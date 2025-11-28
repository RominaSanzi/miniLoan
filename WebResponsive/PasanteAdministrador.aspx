<%@ Page Title="" Language="vb" EnableEventValidation="false" AutoEventWireup="True"
    MasterPageFile="~/BaseIncludes.master"
    CodeBehind="PasanteAdministrador.aspx.vb"
    Inherits="di.financiera.webResponsive.PasanteAdministrador" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>
<%@ MasterType TypeName="di.financiera.webResponsive.BaseIncludes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" ClientIDMode="Static" runat="server">
    <form id="formPrincipal" runat="server" onsubmit="cargando();">
        <div class="main-panel" id="main-panel">
            <script>
                validarMenu();
            </script>

            <!-- ░░░░ Barra flotante superior ░░░░ -->
            <div class="barra-flotante">
                <nav class="navbar navbar-expand-lg navbar-transparent bg-primary navbar-absolute">
                    <div class="container-fluid">
                        <div class="navbar-wrapper">
                            <div class="navbar-toggle">
                                <button type="button" class="navbar-toggler">
                                    <span class="navbar-toggler-bar bar1"></span>
                                    <span class="navbar-toggler-bar bar2"></span>
                                    <span class="navbar-toggler-bar bar3"></span>
                                </button>
                            </div>
                            <a class="navbar-brand"></a>
                        </div>

                        <button class="navbar-toggler" type="button" data-toggle="collapse"
                            data-target="#navigation" aria-controls="navigation-index"
                            aria-expanded="false" aria-label="Toggle navigation">
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                        </button>

                        <div class="collapse navbar-collapse justify-content-end" id="navigation">
                            <ul class="navbar-nav">
                                <li class="nav-item">
                                    <asp:LinkButton ID="btnAtras" CausesValidation="false" UseSubmitBehavior="false"
                                        ClientIDMode="Static" CssClass="nav-link" TabIndex="10050" runat="server"
                                        data-toggle='tooltip' ToolTip="Atras">
                                        <i class="far fa-arrow-alt-circle-left fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item">
                                    <asp:LinkButton ID="btnBuscar" ClientIDMode="Static" CssClass="nav-link"
                                        TabIndex="13" runat="server" data-toggle="tooltip" ToolTip="Buscar">
                                        <i class="fas fa-search fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item">
                                    <asp:LinkButton ID="btnAyuda" CausesValidation="false" UseSubmitBehavior="false"
                                        ClientIDMode="Static" CssClass="nav-link" TabIndex="14" runat="server"
                                        data-toggle='tooltip' ToolTip="Ayuda">
                                        <i class="fas fa-life-ring fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
                <div class="panel-header panel-header-sm"></div>
            </div>
            <!-- ░░░░ Fin barra flotante ░░░░ -->


            <div class="content">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <div class="row align-items-end">
                    <div class="card">
                        <div class="card-header card-header-icon card-header-info">
                            <h4 class="card-title">Administrador de pasante</h4>
                        </div>
                        <div class="card-body form-horizontal">
                            <asp:LinkButton ID="BtnAlta" TabIndex="1" CssClass="btn btn-primary"
                                runat="server" Text="Alta">
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body form-horizontal">
                            <div class="row align-items-end">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblDNI" runat="server"
                                            CssClass="bmd-label-floating" EnableViewState="False"
                                            ClientIDMode="Static">
                                            DNI
                                        </asp:Label>
                                        <cc1:IntegerBox ID="intDNI" UsaSeparadorPuntos="true"
                                            TabIndex="8" runat="server" CssClass="form-control"
                                            Width="100%" AutoComplete="False" MaxLength="8" ClientIDMode="Static"
                                            required="required">
                                        </cc1:IntegerBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblNombre" runat="server"
                                            CssClass="bmd-label-floating" EnableViewState="False"
                                            ClientIDMode="Static">
                                            Nombre
                                        </asp:Label>
                                        <asp:TextBox ID="txtNombre" TabIndex="1" runat="server"
                                            CssClass="form-control" Width="100%" MaxLength="40"
                                            AutoPostBack="false" AutoComplete="False" ClientIDMode="Static"
                                            required="required">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body form-horizontal">
                            <asp:DataGrid ID="grillaPasante" runat="server" CssClass="table table-striped table-bordered administrador" Width="100%" CellPadding="3" GridLines="none" AutoGenerateColumns="False" ClientIDMode="Static">
                                <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                                <AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
                                <ItemStyle CssClass="ItemStyle"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" CssClass="cabecera-grillas"></HeaderStyle>
                                <FooterStyle CssClass="FooterStyle"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="Documento" HeaderText="Documento"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="nombre" HeaderText="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="escuela" HeaderText="Escuela"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="">
                                        <ItemTemplate>
                                            <div class="row justify-content-end" style="padding-right: 15px;">
                                                <div style="margin: 2px; display: block;">
                                                    <asp:LinkButton ID="btnDetalles" CssClass="btn btn-round btn-success btn-icon btn-sm" data-toggle='tooltip' runat="server" title="Detalles" CommandName="Detalles"><i class="fas fa-info"></i></asp:LinkButton>
                                                </div>
                                                <div style="margin: 2px; display: <%Response.Write(Master.puedeAcceder("PasanteModificar.aspx"))%>;">
                                                    <asp:LinkButton ID="btnModificar" CssClass="btn btn-round btn-warning btn-icon btn-sm" data-toggle='tooltip' runat="server" title="Modificar" CommandName="Modificar"><i class="fas fa-pencil-alt"></i></asp:LinkButton>
                                                </div>
                                                <div style="margin: 2px; display: <%Response.Write(Master.puedeAcceder("PasanteEliminar.aspx"))%>;">
                                                    <asp:LinkButton ID="btnEliminar" OnClientClick="site.pedirConfirmacionEliminar(this);return false;" CssClass="btn btn-round btn-danger btn-icon btn-sm" data-toggle='tooltip' runat="server" title="Eliminar" CommandName="Eliminar"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
