<%@ Page Title="" Language="vb" EnableEventValidation="false" AutoEventWireup="True" MasterPageFile="~/BaseIncludes.master" CodeBehind="ComprobanteCompraAdministrador.aspx.vb" Inherits="di.financiera.webResponsive.ComprobanteCompraAdministrador" %>

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
            <div class="barra-flotante">
                <nav class="navbar navbar-expand-lg navbar-transparent  bg-primary  navbar-absolute">
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
                        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigation" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                            <span class="navbar-toggler-bar navbar-kebab"></span>
                        </button>
                        <div class="collapse navbar-collapse justify-content-end" id="navigation">
                            <ul class="navbar-nav">
                                <li class="nav-item">
                                    <asp:LinkButton ID="btnBuscar" ClientIDMode="Static" CssClass="nav-link" TabIndex="9" runat="server" data-toggle='tooltip' ToolTip="Buscar"><i class="fas fa-search fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item">
                                    <asp:LinkButton ID="btnAyuda" CausesValidation="false" UseSubmitBehavior="false" ClientIDMode="Static" CssClass="nav-link" TabIndex="10" runat="server" data-toggle='tooltip' ToolTip="Ayuda"><i class="fas fa-life-ring fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
                <div class="panel-header panel-header-sm"></div>
            </div>
            <div class="content">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
                <div class="" id="tablaGenerica">

                    <div class="row align-items-end ">
                        <div class="card">
                            <div class="card-header card-header-icon card-header-info">
                                <h4 class="card-title">Administrador de comprobante compra</h4>
                            </div>
                            <div class="card-body form-horizontal">
                                <div style="display: <%Response.Write(Master.puedeAcceder("ComprobanteCompraCarga.aspx"))%>;">
                                    <asp:LinkButton ID="btnAlta" TabIndex="1" CssClass="btn btn-primary" runat="server" Text="Alta"></asp:LinkButton>
                                </div>
                                <%--<div class="row justify-content-center">
                                    <ajaxToolkit:CascadingDropDown ID="ccdGrupoEmpresa" runat="server" Category="GrupoEmpresa" EnableViewState="true" ClientIDMode="Static"
                                        viewstatemode="Enabled"
                                        LoadingText="[Cargando grupo de empresas...]"
                                        ServicePath="ServiciosAJAX/ComboNiveles.asmx"
                                        ServiceMethod="ObtenerGrupoEmpresas"
                                        BehaviorID="ccdGrupoEmpresa" 
                                        UseContextKey="true"
                                        TargetControlID="lstGrupoEmpresas">
                                    </ajaxToolkit:CascadingDropDown>
                                    <ajaxToolkit:CascadingDropDown ID="ccdEmpresaGrupo" runat="server" Category="EmpresaGrupo"  EnableViewState="true" ClientIDMode="Static"
                                        viewstatemode="Enabled"
                                        LoadingText="[Cargando empresas del grupo...]"
                                        BehaviorID="ccdEmpresaGrupo" 
                                        ParentControlID="lstGrupoEmpresas"
                                        ServicePath="ServiciosAJAX/ComboNiveles.asmx"
                                        ServiceMethod="ObtenerEmpresasGrupoVacio"
                                        UseContextKey="true"
                                        TargetControlID="lstEmpresaGrupo">
                                    </ajaxToolkit:CascadingDropDown>
                                    <ajaxToolkit:CascadingDropDown ID="ccdUnidadDeNegocios" runat="server" Category="UnidadDeNegocios" EnableViewState="true" ClientIDMode="Static"
                                        viewstatemode="Enabled"
                                        BehaviorID="ccdUnidadDeNegocios" 
                                        LoadingText="[Cargando unidades de negocio...]"
                                        ParentControlID="lstEmpresaGrupo"
                                        UseContextKey="true"
                                        ServicePath="ServiciosAJAX/ComboNiveles.asmx"
                                        ServiceMethod="ObtenerUnidadDeNegociosPorEmpresGrupoVacio"
                                        TargetControlID="lstUnidadDeNegocios">
                                    </ajaxToolkit:CascadingDropDown>
                                    <ajaxToolkit:CascadingDropDown ID="ccdSucursal" runat="server" Category="Sucursal" EnableViewState="true" ClientIDMode="Static"
                                        viewstatemode="Enabled"
                                        BehaviorID="ccdSucursal" 
                                        LoadingText="[Cargando Sucursales...]"
                                        ParentControlID="lstUnidadDeNegocios"
                                        ServicePath="ServiciosAJAX/ComboNiveles.asmx"
                                        ServiceMethod="ObtenerSucursalesPorUnidadDeNegocioVacio"
                                        UseContextKey="true"
                                        TargetControlID="lstSucursal">
                                    </ajaxToolkit:CascadingDropDown>
                                </div>--%>

                                <div class="row align-items-end ">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblCodigo" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Código</asp:Label>
                                            <cc1:IntegerBox ID="intCodigo" TabIndex="5" runat="server" AutoComplete="False" CssClass="form-control" ClientIDMode="Static" Width="100%"></cc1:IntegerBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblNombre" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Nombre</asp:Label>
                                            <cc1:TextBoxUp ID="txtNombre" TabIndex="6" runat="server" AutoComplete="False" CssClass="form-control" Width="100%" ClientIDMode="Static"></cc1:TextBoxUp>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end ">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblTipoFactura" runat="server" EnableViewState="False" CssClass="bmd-label-floating" ClientIDMode="Static">Tipo de factura</asp:Label>
                                            <asp:DropDownList ID="lstTipoFactura" TabIndex="7" runat="server" CssClass="form-control" Width="100%" AutoPostBack="false" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblFormaPago" runat="server" EnableViewState="False" CssClass="bmd-label-floating" ClientIDMode="Static">Forma de pago</asp:Label>
                                            <asp:DropDownList ID="lstFormaPago" TabIndex="8" runat="server" CssClass="form-control" Width="100%" AutoPostBack="false" ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-end ">
                                    <div class="col-md-12 table-responsive">
                                        <asp:DataGrid ID="grillaComprobanteConsulta" runat="server" CssClass="table table-striped table-bordered administrador" Width="100%" CellPadding="3" GridLines="none" AutoGenerateColumns="False" ClientIDMode="Static">
                                            <SelectedItemStyle CssClass="SelectedItemStyle"></SelectedItemStyle>
                                            <AlternatingItemStyle CssClass="AlternatingItemStyle"></AlternatingItemStyle>
                                            <ItemStyle CssClass="ItemStyle"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" CssClass="cabecera-grillas"></HeaderStyle>
                                            <FooterStyle CssClass="FooterStyle"></FooterStyle>
                                            <Columns>
                                                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="codigo" HeaderText="Código"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="razonSocial" HeaderText="Razón social"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yy}"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="descripcion" HeaderText="Tipo"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="numeroFactura" HeaderText="N&#250;mero factura"></asp:BoundColumn>

                                                <asp:TemplateColumn HeaderText="">
                                                    <ItemTemplate>
                                                        <div class="row justify-content-end" style="padding-right: 15px;">
                                                            <div style="margin: 2px; display: block;">
                                                                <asp:LinkButton ID="btnDetalles" CssClass="btn btn-round btn-success btn-icon btn-sm" data-toggle='tooltip' runat="server" title="Detalles" CommandName="Detalles"><i class="fas fa-info"></i></asp:LinkButton>
                                                            </div>
                                                            <div style="margin: 2px; display: <%Response.Write(Master.puedeAcceder("ComprobanteCompraModificar.aspx"))%>;">
                                                                <asp:LinkButton ID="btnModificar" CssClass="btn btn-round btn-warning btn-icon btn-sm" data-toggle='tooltip' runat="server" title="Modificar" CommandName="Modificar"><i class="fas fa-pencil-alt"></i></asp:LinkButton>
                                                            </div>
                                                            <div style="margin: 2px; display: <%Response.Write(Master.puedeAcceder("ComprobanteCompraEliminar.aspx"))%>;">
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
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="assets/site/niveles.js"></script>
    <script src="assets/js/plugins/jquery.dataTables.min.js"></script>
    <script src="assets/js/plugins/datatables/buttons.colVis.min.js"></script>
    <script src="assets/js/plugins/datatables/buttons.html5.min.js"></script>
    <script src="assets/js/plugins/datatables/buttons.print.min.js"></script>
    <script src="assets/js/plugins/datatables/dataTables.buttons.min.js"></script>
    <script src="assets/js/plugins/datatables/dataTables.responsive.min.js"></script>
    <script src="assets/js/plugins/datatables/dataTables.Select.min.js"></script>
    <script src="assets/js/plugins/datatables/dataTables.tableTools.js"></script>
    <script src="assets/js/plugins/datatables/jszip.min.js"></script>
    <script src="assets/js/plugins/datatables/pdfmake.min.js"></script>
    <script src="assets/js/plugins/datatables/responsive.bootstrap.min.js"></script>
    <script src="assets/js/plugins/datatables/vfs_fonts.js"></script>
    <script src="assets/js/tablas-index-es.js"></script>
</asp:Content>
