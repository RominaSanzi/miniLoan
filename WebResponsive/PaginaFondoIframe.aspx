<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaginaFondoIframe.aspx.vb" MasterPageFile="~/BaseIncludes.Master" Inherits="di.financiera.webResponsive.PaginaFondoIframe" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>
<%@ MasterType TypeName="di.financiera.webResponsive.BaseIncludes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ClientIDMode="Static" ContentPlaceHolderID="Body" runat="server">
        <div class="main-panel" id="main-panel">
            <script>
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
                            </ul>
                        </div>
                    </div>
                </nav>
                <div class="panel-header panel-header-sm"></div>
            </div>
            <div class="content">
                <div class="" id="tablaDatos">

                    <div class="row align-items-end ">
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
  
</asp:Content>
