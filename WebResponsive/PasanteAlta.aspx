<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/BaseIncludes.master"
    CodeBehind="PasanteAlta.aspx.vb" Inherits="di.financiera.webResponsive.PasanteAlta" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>
<%@ MasterType TypeName="di.financiera.webResponsive.BaseIncludes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">

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
                                    <asp:LinkButton ID="btnAceptar" ClientIDMode="Static" CssClass="nav-link"
                                        TabIndex="13" runat="server" data-toggle='tooltip' ToolTip="Aceptar">
                                        <i class="far fa-check-circle fa-2x"></i>
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

                <!-- ░░░░ Validaciones ░░░░ -->
                <asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" runat="server"
                    EnableTheming="True" ShowMessageBox="False" ShowSummary="True" HeaderText="Atención:"
                    CssClass="col-xs-11 col-sm-6 alert alert-danger alert-with-icon animated fadeInDown alert-dismissable"
                    role="alert" data-notify-position="top-left"
                    Style="display: inline-block; margin: 15px auto; position: fixed; transition: all 0.5s ease-in-out;
                    z-index: 1031; bottom: 20px; right: 20px;" />

                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" runat="server"
                    EnableTheming="True" ShowMessageBox="False" ShowSummary="True" HeaderText="Atención:"
                    CssClass="col-xs-11 col-sm-6 alert alert-danger alert-with-icon animated fadeInDown alert-dismissable"
                    role="alert" data-notify-position="top-left"
                    Style="display: inline-block; margin: 15px auto; position: fixed; transition: all 0.5s ease-in-out;
                    z-index: 1031; bottom: 20px; right: 20px;" />

                <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>

                <!-- ░░░░ Contenido principal ░░░░ -->
                <div id="tablaContenedora">
                    <div class="row align-items-end">

                        <!-- ░░░░ Primera tarjeta (card) ░░░░ -->
                        <div class="card">
                            <div class="card-header card-header-icon">
                                <h4 class="card-title">
                                    <asp:Label ID="lblTitulo" runat="server" ClientIDMode="Static">
                                        Alta de pasante
                                    </asp:Label>
                                </h4>
                            </div>

                            <div class="card-header card-header-icon card-header-info">
                                <h4 class="card-title">
                                    <asp:Label ID="lblDatosPersonales" runat="server" ClientIDMode="Static">
                                        Datos personales
                                    </asp:Label>
                                </h4>
                            </div>

                            <!-- ░░░░ Formulario interno ░░░░ -->
                            <div class="card-body form-horizontal">

                                <div class="row align-items-end">
                                    <!-- Tipo de documento -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblTipoDNI" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Tipo de documento
                                            </asp:Label>
                                            <asp:DropDownList ID="lstTipoDNI" TabIndex="5" runat="server"
                                                CssClass="form-control input-requerido" Width="100%" AutoPostBack="false"
                                                ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <!-- Número de documento -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblDNI" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Número de documento
                                            </asp:Label>
                                            <cc1:IntegerBox ID="intDNI" UsaSeparadorPuntos="true"
                                                TabIndex="8" runat="server" CssClass="form-control input-requerido"
                                                Width="100%" AutoComplete="False" MaxLength="8" ClientIDMode="Static"
                                                required="required">
                                            </cc1:IntegerBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end">
                                    <!-- Nombre y apellido -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblNombreApellido" runat="server"
                                                CssClass="bmd-label-floating " EnableViewState="False"
                                                ClientIDMode="Static">
                                                Nombre y Apellido
                                            </asp:Label>
                                            <asp:TextBox ID="txtNombreApellido" TabIndex="1" runat="server"
                                                CssClass="form-control input-requerido" Width="100%" MaxLength="40"
                                                AutoPostBack="false" AutoComplete="False" ClientIDMode="Static"
                                                required="required">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <!-- Fecha de nacimiento -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblFechaNacimiento" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Fecha de nacimiento
                                            </asp:Label>
                                            <cc1:DateBox ID="dtbFechaNacimiento" TabIndex="6" runat="server"
                                                CssClass="form-control fecha datepicker input-requerido"
                                                AutoComplete="False" ClientIDMode="Static" Width="100%" required="required">
                                            </cc1:DateBox>
                                        </div>
                                    </div>
                                </div>

                                <%-- Email --%>
                                <div class="row align-items-end">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblEmail" runat="server" CssClass="bmd-label-floating">
                                                Email
                                            </asp:Label>
                                            <asp:TextBox ID="txtEmail" runat="server"
                                                CssClass="form-control" Width="100%"
                                                MaxLength="150" TextMode="Email" required="required">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <%-- Cuil --%>
                                <div class="row align-items-end">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblCUIL" runat="server" CssClass="bmd-label-floating">
                                                CUIL
                                            </asp:Label>
                                            <cc1:IntegerBox ID="intCUIL" UsaSeparadorPuntos="false"
                                                TabIndex="8" runat="server" CssClass="form-control input-requerido"
                                                Width="100%" AutoComplete="False" MaxLength="11" ClientIDMode="Static"
                                                required="required">
                                            </cc1:IntegerBox>
                                        </div>
                                    </div>
                                </div>
                                <%-- Sexo --%>
                                <div class="row align-items-end">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblSexo" runat="server" CssClass="bmd-label-floating">
                                                Sexo
                                            </asp:Label>
                                            <asp:DropDownList ID="lstSexo" TabIndex="5" runat="server"
                                                CssClass="form-control input-requerido" Width="100%" AutoPostBack="false"
                                                ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div> <!-- fin card-body -->
                        </div> <!-- fin card -->

                        <!-- ░░░░ Parte dooossssss ░░░░ -->
                        <div class="card">

                            <div class="card-header card-header-icon card-header-info">
                                <h4 class="card-title">
                                    <asp:Label ID="lblDomicilio" runat="server" ClientIDMode="Static">
                                        Domicilio
                                    </asp:Label>
                                </h4>
                            </div>

                            <!-- ░░░░ Formulario interno ░░░░ -->
                            <div class="card-body form-horizontal">

                                <div class="row align-items-end">
                                    <!-- Calle -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblCalle" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Calle 
                                            </asp:Label>
                                            <asp:TextBox ID="txtCalle" TabIndex="1" runat="server"
                                                CssClass="form-control input-requerido" Width="100%" MaxLength="40"
                                                AutoPostBack="false" AutoComplete="False" ClientIDMode="Static"
                                                required="required">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <!-- Numero -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblNumero" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Número
                                            </asp:Label>
                                            <cc1:IntegerBox ID="intNumero" UsaSeparadorPuntos="false"
                                                TabIndex="8" runat="server" CssClass="form-control input-requerido"
                                                Width="100%" AutoComplete="False" MaxLength="12" ClientIDMode="Static"
                                                required="required">
                                            </cc1:IntegerBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end">
                                    <!-- Piso -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblPiso" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Piso
                                            </asp:Label>
                                            <cc1:IntegerBox ID="intPiso" UsaSeparadorPuntos="false"
                                                TabIndex="8" runat="server" CssClass="form-control"
                                                Width="100%" AutoComplete="False" MaxLength="12" ClientIDMode="Static"
                                                required="required">
                                            </cc1:IntegerBox>
                                        </div>
                                    </div>

                                    <!-- Codigo Postal -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblCodigoPostal" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Código Postal
                                            </asp:Label>
                                            <cc1:IntegerBox ID="intCodigoPostal" UsaSeparadorPuntos="false"
                                                TabIndex="8" runat="server" CssClass="form-control input-requerido"
                                                Width="100%" AutoComplete="False" MaxLength="12" ClientIDMode="Static"
                                                required="required">
                                            </cc1:IntegerBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end">
                                    <!-- Localidad -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblLocalidad" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Localidad
                                            </asp:Label>
                                            <asp:DropDownList ID="lstLocalidad" TabIndex="5" runat="server"
                                                CssClass="form-control input-requerido" Width="100%" AutoPostBack="false"
                                                ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <!-- Barrio -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblBarrio" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Barrio
                                            </asp:Label>
                                            <asp:DropDownList ID="lstBarrio" TabIndex="5" runat="server"
                                                CssClass="form-control input-requerido" Width="100%" AutoPostBack="false"
                                                ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div> <!-- fin card-body -->
                        </div> <!-- fin card -->

                        <!-- ░░░░ Terceraaaaaaaaaa ░░░░ -->
                        <div class="card">

                            <div class="card-header card-header-icon card-header-info">
                                <h4 class="card-title">
                                    <asp:Label ID="lblDatosPasantia" runat="server" ClientIDMode="Static">
                                        Datos pasantía
                                    </asp:Label>
                                </h4>
                            </div>

                            <!-- ░░░░ Formulario interno ░░░░ -->
                            <div class="card-body form-horizontal">

                                <div class="row align-items-end">
                                    <!-- Legajo Escuela -->
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblLegajoEscuela" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Legajo de escuela
                                            </asp:Label>
                                            <cc1:IntegerBox ID="intLegajoEscuela" UsaSeparadorPuntos="false"
                                                TabIndex="8" runat="server" CssClass="form-control input-requerido"
                                                Width="100%" AutoComplete="False" MaxLength="12" ClientIDMode="Static"
                                                required="required">
                                            </cc1:IntegerBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end">
                                    <!-- Fecha de Inicio -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblFechaInicio" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Fecha de inicio de pasantía
                                            </asp:Label>
                                            <cc1:DateBox ID="dtbFechaInicio" TabIndex="6" runat="server"
                                                CssClass="form-control fecha datepicker input-requerido"
                                                AutoComplete="False" ClientIDMode="Static" Width="100%" required="required">
                                            </cc1:DateBox>
                                        </div>
                                    </div>

                                    <!-- Fecha de finalizacion -->
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblFechaFinalizacion" runat="server"
                                                CssClass="bmd-label-floating" EnableViewState="False"
                                                ClientIDMode="Static">
                                                Fecha de finalización de pasantía
                                            </asp:Label>
                                            <cc1:DateBox ID="dtbFechaFinalizacion" TabIndex="6" runat="server"
                                                CssClass="form-control fecha datepicker input-requerido"
                                                AutoComplete="False" ClientIDMode="Static" Width="100%" required="required">
                                            </cc1:DateBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end">
                                    <!-- Escuela -->
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblEscuela" runat="server" CssClass="bmd-label-floating">
                                                Escuela
                                            </asp:Label>
                                            <asp:DropDownList ID="lstEscuela" TabIndex="5" runat="server"
                                                CssClass="form-control input-requerido" Width="100%" AutoPostBack="false"
                                                ClientIDMode="Static">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div> <!-- fin card-body -->
                        </div> <!-- fin card -->

                    </div> <!-- fin row -->
                </div> <!-- fin tablaContenedora -->

            </div> <!-- fin content -->
        </div> <!-- fin main-panel -->
    </form>
</asp:Content>
