<%@ Page Title="" Language="vb" AutoEventWireup="True" MasterPageFile="~/BaseIncludes.master" CodeBehind="ComprobanteCompraAlta.aspx.vb" Inherits="di.financiera.webResponsive.ComprobanteCompraAlta" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>
<%@ MasterType TypeName="di.financiera.webResponsive.BaseIncludes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript" src="js/funciones.js"></script>
    <script type="text/javascript" src="js/funcionesAjax.js"></script>
    <script type="text/javascript">
        function redondeo(num, decimals) {
            var sign = num >= 0 ? 1 : -1;
            return (Math.round((num * Math.pow(10, decimals)) + (sign * 0.001)) / Math.pow(10, decimals)).toFixed(decimals);
        }

        function importe(value) {
            var res = 0.0;
            try {
                if (trim(value) != '')
                    res = parseFloat(value.replace(",", "."));
                return res;
            } catch (e) {
                alert(e.message)
            }
        }

        function setMontoNetoGravadoTotal(value) {
            document.getElementById('dcbMontoNetoGravadoTotal').value = value
        }

        function setImporteMontoIVA(value) {
            document.getElementById('dcbImporteMontoIVA').value = value
        }

        function setMontoTotal(value) {
            document.getElementById('dcbMontoTotal').value = value
        }

        function calcularMontoTotal() {
            var total_neto_gravado = 0.0;
            var tota_iva = 0.0;
            var total_percepciones = 0.0
            var total_retenciones = 0.0
            var total = 0.0;

            try {
                // Netos Gravados
                total_neto_gravado = 0.0;
                total_neto_gravado += importe(document.getElementById('dcbImporteNetoGravado105').value);
                total_neto_gravado += importe(document.getElementById('dcbImporteNetoGravado21').value);
                total_neto_gravado += importe(document.getElementById('dcbImporteNetoGravado27').value);
                //total_neto_gravado += importe(document.getElementById('dcbImporteMontoIVA').value);

                // Precepciones
                total_percepciones = 0.0;
                total_percepciones += importe(document.getElementById('dcbMontoPercepIVA').value);
                total_percepciones += importe(document.getElementById('dcbMontoPercepIIBB').value);

                // Retenciones
                total_retenciones = 0.0;
                total_retenciones += importe(document.getElementById('dcbMontoRetenSicreb').value);
                total_retenciones += importe(document.getElementById('dcbMontoRetenIIBB').value);
                total_retenciones += importe(document.getElementById('dcbMontoRetenIVA').value);
                total_retenciones += importe(document.getElementById('dcbMontoRetenSuss').value);
                total_retenciones += importe(document.getElementById('dcbMontoRetenGanancias').value);

                // Total Neto Gravado
                setMontoNetoGravadoTotal(redondeo(total_neto_gravado, 2));

                // Total IVA
                total_iva = calculaMontoIVA();
                setImporteMontoIVA(redondeo(total_iva, 2));

                // Total General
                total = 0.0
                total += total_neto_gravado
                total += total_iva
                total += importe(document.getElementById('dcbMontoNoGravado').value);
                total += total_percepciones;
                total -= total_retenciones;
                setMontoTotal(redondeo(total, 2));

            } catch (e) {
                alert(e.message);
            }

        }

        function calculaMontoIVA() {
            var iva105 = 0.0;
            var iva21 = 0.0;
            var iva27 = 0.0;
            var res = 0.0;

            try {
                iva105 = redondeo(importe(document.getElementById('dcbImporteNetoGravado105').value) * 0.105, 2);
                iva21 = redondeo(importe(document.getElementById('dcbImporteNetoGravado21').value) * 0.21, 2);
                iva27 = importe(document.getElementById('dcbImporteNetoGravado27').value) * 0.27;

                res = parseFloat(iva105) + parseFloat(iva21) + parseFloat(iva27)
                return res;

            } catch (e) {
                alert(e.message);
            }
        }

        function limpiarImportes() {
            try {
                document.getElementById('dcbImporteNetoGravado105').value = 0.0;
                document.getElementById('dcbImporteNetoGravado21').value = 0.0;
                document.getElementById('dcbImporteNetoGravado27').value = 0.0;

                setMontoNetoGravadoTotal(0.0);
                setImporteMontoIVA(0.0);

                document.getElementById('dcbMontoNoGravado').value = 0.0;
                document.getElementById('dcbMontoPercepIVA').value = 0.0;
                document.getElementById('dcbMontoPercepIIBB').value = 0.0;
                document.getElementById('dcbMontoRetenSicreb').value = 0.0;
                document.getElementById('dcbMontoRetenIIBB').value = 0.0;
                document.getElementById('dcbMontoRetenIVA').value = 0.0;
                document.getElementById('dcbMontoRetenSuss').value = 0.0;
                document.getElementById('dcbMontoRetenGanancias').value = 0.0;

                setMontoTotal(0.0);

            } catch (e) {
                alert(e.message);
            }
        }

        function listaContiene(lista, valor) {
            var res = false;
            res = lista.indexOf(valor) > -1;
            return res;
        }

        function seleccionarAsientosModelos(option) {
            if (option.value == 0) {
                PageMethods.borrarSessionAsientos();
            } else {
                PageMethods.seleccionarSessionAsientos(option.value);
            }
        }

    </script>
</asp:Content>
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
                                    <asp:LinkButton ID="btnAtras" CausesValidation="false" UseSubmitBehavior="false" ClientIDMode="Static" CssClass="nav-link" TabIndex="10050" runat="server" data-toggle='tooltip' ToolTip="Atras"><i class="far fa-arrow-alt-circle-left fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item">
                                    <asp:LinkButton ID="btnAceptar" ClientIDMode="Static" CssClass="nav-link" TabIndex="13" runat="server" data-toggle='tooltip' ToolTip="Aceptar"><i class="far fa-check-circle fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item">
                                    <asp:LinkButton ID="btnAyuda" CausesValidation="false" UseSubmitBehavior="false" ClientIDMode="Static" CssClass="nav-link" TabIndex="14" runat="server" data-toggle='tooltip' ToolTip="Ayuda"><i class="fas fa-life-ring fa-2x"></i>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
                <div class="panel-header panel-header-sm"></div>
            </div>
            <div class="content">
                <asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" runat="server" EnableTheming="True" ShowMessageBox="False" ShowSummary="True" HeaderText="Atención:" CssClass="col-xs-11 col-sm-6 alert alert-danger alert-with-icon animated fadeInDown alert-dismissable" role="alert" data-notify-position="top-left" Style="display: inline-block; margin: 15px auto; position: fixed; transition: all 0.5s ease-in-out; z-index: 1031; bottom: 20px; right: 20px;" />
                <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>
                <div class="" id="tablaContenedora">
                    <div class="row align-items-end ">
                        <div class="card">
                            <div class="card-header card-header-icon">
                                <h4 class="card-title">
                                    <asp:Label ID="lblTitulo" runat="server" CssClass="" ClientIDMode="Static">Alta de comprobante compra</asp:Label>
                                </h4>
                            </div>
                            <div class="card-header card-header-icon card-header-info">
                                <h4 class="card-title">
                                    <asp:Label ID="lbdatos" runat="server" CssClass="" ClientIDMode="static">datos</asp:Label></h4>
                            </div>
                            <div class="card-body form-horizontal">
                                <div class="row align-items-end ">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblCodigoProveedor" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Código de proveedor</asp:Label>
                                            <asp:TextBox ID="txtCodigoProveedor" TabIndex="1" runat="server" CssClass="form-control input-requerido" Width="100%" MaxLength="8" AutoPostBack="true" autocomplete="False" ClientIDMode="Static" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none;">
                                        <asp:RequiredFieldValidator CssClass="validator" ID="valtxtCodigoProveedor" runat="server" ControlToValidate="txtCodigoProveedor" ErrorMessage="Código de proveedor es un campo requerido." ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblRazonSocial" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Razón social</asp:Label>
                                            <cc1:TextBoxUp ID="txtRazonSocial" TabIndex="2" runat="server" CssClass="form-control" Width="100%" AutoComplete="False" AutoPostBack="true" MaxLength="50" ClientIDMode="Static"></cc1:TextBoxUp>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end ">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblCUIT" runat="server" EnableViewState="False" CssClass="bmd-label-floating" ClientIDMode="Static">Cuit</asp:Label>
                                            <cc1:IntegerBox ID="txtCUIT" UsaSeparadorPuntos="false" TabIndex="3" runat="server" CssClass="form-control" Width="100%" MaxLength="11" AutoComplete="False" AutoPostBack="true" ClientIDMode="Static"></cc1:IntegerBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblTipoIVA" runat="server" EnableViewState="False" CssClass="bmd-label-floating" ClientIDMode="Static">Tipo de IVA</asp:Label>
                                            <cc1:TextBoxUp ID="txtTipoIVA" TabIndex="4" runat="server" CssClass="form-control" Width="100%" AutoComplete="False" Enabled="False" ClientIDMode="Static"></cc1:TextBoxUp>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end ">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblTipoFactura" runat="server" EnableViewState="False" CssClass="etiqueta" ClientIDMode="Static">Tipo de factura</asp:Label>
                                            <asp:DropDownList ID="lstTipoFactura" TabIndex="5" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblFecha" runat="server" CssClass="etiqueta" EnableViewState="False" ClientIDMode="Static">Fecha de comprobante</asp:Label>
                                            <cc1:DateBox ID="dtbFecha" TabIndex="6" runat="server" CssClass="form-control fecha datepicker" AutoComplete="False" ClientIDMode="Static" Width="100%" required="required"></cc1:DateBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end ">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:RequiredFieldValidator CssClass="validator" ID="valdtbFecha" runat="server" ControlToValidate="dtbFecha" ErrorMessage="Fecha es un campo requerido." ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
                                            <asp:Label ID="lblFechaContable" runat="server" CssClass="etiqueta" EnableViewState="False" ClientIDMode="Static">Fecha contable *</asp:Label>
                                            <cc1:DateBox ID="dtbFechaContable" TabIndex="7" runat="server" CssClass="form-control fecha datepicker" AutoComplete="False" ClientIDMode="Static" Width="100%" required="required"></cc1:DateBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:RequiredFieldValidator CssClass="validator" ID="valdtbFechaContable" runat="server" ControlToValidate="dtbFechaContable" ErrorMessage="Fecha contable es un campo requerido." ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblNumeroFactura" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Número de factura</asp:Label>
                                            <cc1:IntegerBox ID="intNumeroFactura" UsaSeparadorPuntos ="false" TabIndex="8" runat="server" CssClass="form-control input-requerido" Width="100%" AutoComplete="False" MaxLength="12" ClientIDMode="Static" required="required"></cc1:IntegerBox>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none;">
                                        <asp:RequiredFieldValidator ID="valintNumeroFactura" CssClass="validator" runat="server" ControlToValidate="intNumeroFactura" ErrorMessage="Número de factura es un campo requerido." ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row align-items-end ">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblFormaPago" runat="server" EnableViewState="False" CssClass="etiqueta" ClientIDMode="Static">Forma de pago</asp:Label>
                                            <asp:DropDownList ID="lstFormaPago" TabIndex="9" runat="server" CssClass="form-control" Width="100%" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblCondicionCompra" runat="server" EnableViewState="False" CssClass="etiqueta" ClientIDMode="Static">Condición de compra</asp:Label>
                                            <asp:DropDownList ID="lstCondicionCompra" TabIndex="10" runat="server" CssClass="form-control" Width="100%" AutoPostBack="false" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row align-items-end ">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblTerminoDePago" runat="server" EnableViewState="False" CssClass="etiqueta" ClientIDMode="Static">Término de pago</asp:Label>
                                            <asp:DropDownList ID="lstTerminoPago" TabIndex="11" runat="server" CssClass="form-control" Width="100%" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblAsientosModelos" runat="server" CssClass="etiqueta" EnableViewState="False" ClientIDMode="Static">Asientos modelos</asp:Label>
                                            <asp:DropDownList ID="lstAsientosModelos" TabIndex="12" runat="server" CssClass="form-control" Width="100%" AutoPostBack="false" onchange="seleccionarAsientosModelos(this);" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    <div class="card">
                        <div class="card-header card-header-icon card-header-info">
                            <h4 class="card-title">
                                <asp:Label ID="lblnetos" runat="server" CssClass="" ClientIDMode="static">neto gravado</asp:Label></h4>
                        </div>
                        <div class="card-body form-horizontal">
                            <div class="row align-items-end ">
                                <div class="col-md">
                                    <div class="form-group">
                                        <asp:Label ID="lblImporteNetoGrav105" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Neto gravado 10.5</asp:Label>
                                        <cc1:DecimalBox ID="dcbImporteNetoGravado105" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                                <div class="col-md">
                                    <div class="form-group">
                                        <asp:Label ID="lblImporteNetoGrav21" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Neto gravado 21</asp:Label>
                                        <cc1:DecimalBox ID="dcbImporteNetoGravado21" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                                <div class="col-md">
                                    <div class="form-group">
                                        <asp:Label ID="lblImporteNetoGrav27" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Neto gravado 27</asp:Label>
                                        <cc1:DecimalBox ID="dcbImporteNetoGravado27" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row align-items-end ">
                                <div class="col-md">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoNetoGravadoTotal" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Neto gravado total</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoNetoGravadoTotal" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ReadOnly="true" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                        <input id="inpMontoNetoGravadoTotal" runat="server" type="hidden" clientidmode="Static" width="100%" cssclass="form-control">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header card-header-icon card-header-info">
                            <h4 class="card-title">
                                <asp:Label ID="lbliva" runat="server" CssClass="" ClientIDMode="static">iva</asp:Label></h4>
                        </div>
                        <div class="card-body form-horizontal">
                            <div class="row align-items-end ">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoIVA" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Monto iva</asp:Label>
                                        <cc1:DecimalBox ID="dcbImporteMontoIVA" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ReadOnly="true" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <input id="inpImporteMontoIVA" runat="server" type="hidden" clientidmode="Static" width="100%" cssclass="form-control">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header card-header-icon card-header-info">
                            <h4 class="card-title">
                                <asp:Label ID="lblnogravado" runat="server" CssClass="" ClientIDMode="static">neto no gravado</asp:Label></h4>
                        </div>
                        <div class="card-body form-horizontal">
                            <div class="row align-items-end ">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoNoGravado" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Monto no gravado</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoNoGravado"  UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header card-header-icon card-header-info">
                            <h4 class="card-title">
                                <asp:Label ID="lblpercepciones" runat="server" CssClass="" ClientIDMode="static">percepciones</asp:Label></h4>
                        </div>
                        <div class="card-body form-horizontal">
                            <div class="row align-items-end ">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoPercepIVA" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">monto percepción iva</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoPercepIVA" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoPercepIIBB" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">monto percepción iibb</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoPercepIIBB"  UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header card-header-icon card-header-info">
                            <h4 class="card-title">
                                <asp:Label ID="lbltituloretenciones" runat="server" CssClass="" ClientIDMode="static">retenciones</asp:Label></h4>
                        </div>
                        <div class="card-body form-horizontal">
                            <div class="row align-items-end ">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoRetenSicreb" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">monto retención sicreb</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoRetenSicreb" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoRetenIIBB" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">monto retención iibb</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoRetenIIBB" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row align-items-end ">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoRetenIVA" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">monto retención iva</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoRetenIVA" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoRetenSuss" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">monto retención suss</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoRetenSuss"  UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoRetenGanancias" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">monto retención ganancias</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoRetenGanancias" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onblur="if(validarSiNumero(this))calcularMontoTotal();" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header card-header-icon card-header-info">
                            <h4 class="card-title">
                                <asp:Label ID="lbltitulototal" runat="server" CssClass="" ClientIDMode="static">total</asp:Label></h4>
                        </div>
                        <div class="card-body form-horizontal">
                            <div class="row align-items-end ">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblMontoTotal" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Monto total</asp:Label>
                                        <cc1:DecimalBox ID="dcbMontoTotal" UsaSeparadorPuntos ="false" runat="server" CssClass="form-control" AutoComplete="False" onchange="setMontoTotal(this.value);" ReadOnly="true" ClientIDMode="Static" Width="100%"></cc1:DecimalBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <input id="inpMontoTotal" UsaSeparadorPuntos ="false" runat="server" type="hidden" clientidmode="Static" width="100%" cssclass="form-control">
                                    </div>
                                </div>
                            </div>

                            <div class="row align-items-end ">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblDetalle" runat="server" CssClass="bmd-label-floating" EnableViewState="False" ClientIDMode="Static">Detalle</asp:Label>
                                        <cc1:TextBoxUp ID="txtDetalle" runat="server" CssClass="form-control" Width="100%" MaxLength="300" TextMode="MultiLine" AutoComplete="False" ClientIDMode="Static"></cc1:TextBoxUp>
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
