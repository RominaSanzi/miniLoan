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
                    </div>
                </nav>
                <div class="panel-header panel-header-sm"></div>
            </div>

            <div class="content">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <div class="row align-items-end">
                    <div class="card">
                        <div class="card-header card-header-icon card-header-info">
                            <h4 class="card-title">Administrador de pasante</h4>
                        </div>
                        <div class="card-body form-horizontal">
                            <asp:LinkButton ID="BtnAlta" TabIndex="1" CssClass="btn btn-primary"
                                runat="server" Text="Alta"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
