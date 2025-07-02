<%@ Page Title="" Language="vb" AutoEventWireup="True" MasterPageFile="~/BaseIncludes.master" CodeBehind="PaginaError.aspx.vb" Inherits="di.financiera.webResponsive.PaginaError" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>
<%@ MasterType TypeName="di.financiera.webResponsive.BaseIncludes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" ClientIDMode="Static" runat="server">
    <form id="form" runat="server" onsubmit="cargando();">
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
                                    <asp:LinkButton ID="btnAtras" CausesValidation="false" UseSubmitBehavior="false" ClientIDMode="Static" CssClass="nav-link" TabIndex="0" runat="server" data-toggle='tooltip' ToolTip="Atrás"><i class="far fa-arrow-alt-circle-left fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item">
                                    <asp:LinkButton ID="btnAyuda" CausesValidation="false" UseSubmitBehavior="false" ClientIDMode="Static" CssClass="nav-link" TabIndex="6" runat="server" data-toggle='tooltip' ToolTip="Ayuda"><i class="fas fa-life-ring fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
                <div class="panel-header panel-header-sm"></div>
            </div>
            <div class="content">
                <div class="" id="tablaGeneral">

                    <div class="row align-items-end ">
                        <div class="card">
                            <div class="card-header card-header-icon card-header-info">
                                <h4 class="card-title">
                                    <%--<asp:Label ID="lblMotivoExcepcion" runat="server" CssClass="subtitulo" ClientIDMode="Static">Motivo</asp:Label>--%></h4>
                            </div>
                            <div class="card-body form-horizontal">
                                <div class="row justify-content-center">
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="lblMotivoDetalle" runat="server" CssClass="label" ClientIDMode="Static"></asp:Label>

                                        </div>
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
</asp:Content>
