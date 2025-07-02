<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IndexIframe.aspx.vb" Inherits="di.financiera.webResponsive.IndexIframe" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>

<!DOCTYPE html>
<html style="overflow-x: hidden">

<head>
    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="assets/img/apple-icon.png">
    <link rel="icon" type="image/png" href="assets/img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Loan</title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no' name='viewport' />
    <link rel="stylesheet" href="assets/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/css.css" rel="stylesheet" />
    <link href="assets/css/now-ui-dashboard.css?v=1.4.1" rel="stylesheet" />
    <link href="assets/css/Site.css" rel="stylesheet" />
    <link href="assets/css/fontawesome/all.min.css" rel="stylesheet" />
    <script src="PopUpModal.js"></script>
    <script src="assets/js/core/jquery.min.js"></script>
  <style>

.search-box,.close-icon,.search-wrapper {
	position: relative;
	padding: 10px;
}


.close-icon {
	border:1px solid transparent;
	background-color: transparent;
	display: inline-block;
	vertical-align: middle;
  outline: 0;
  cursor: pointer;
}
.close-icon:after {
	content: "X";
	display: block;
	width: 20px;
	height: 20px;
	position: absolute;
	background-color: #FA9595;
	z-index:1;
	right: 35px;
	top: 0;
	bottom: 0;
	margin: auto;
	padding: 2px;
	border-radius: 50%;
	text-align: center;
	color: white;
	font-weight: normal;
	font-size: 20px;
	box-shadow: 0 0 2px #E50F0F;
	cursor: pointer;
}
/*.search-box:not(:valid) ~ .close-icon {
	display: none;
}*/
  </style>
</head>

<body>
    <script src="assets/js/plugins/bootstrap-notify.js"></script>
    <script type="text/javascript">
        function mostrarPendientesSincronizar(eMensaje) {
            console.log(eMensaje);
            color = 'info';

            $.notify({
                /*icon: "now-ui-icons ui-1_bell-53",*/
                message: eMensaje

            }, {
                    type: color,
                    timer: 16000,
                    placement: {
                        from: 'bottom',
                        align: 'right'
                    }
                });
        }
    </script>
    <div id="loading" class="cargando text-center loading">
        <img src="assets/img/Loan_Loading.gif" style="margin-top: -15%" />
    </div>

    <div id="modal" class="wrapper cargando text-center collapse">
        <div class="modal fade ps show" id="noticeModal" tabindex="-1" role="dialog" style="display: block;">
            <div class="modal-dialog modal-notice">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<h5 class="modal-title" id="modalTitulo">Operacion realizada</h5>--%>
                        <div class="swal2-icon swal2-success swal2-animate-success-icon" style="display: flex;">
                            <div class="swal2-success-circular-line-left" style="background-color: rgb(255, 255, 255);"></div>
                            <span class="swal2-success-line-tip"></span><span class="swal2-success-line-long"></span>
                            <div class="swal2-success-ring"></div>
                            <div class="swal2-success-fix" style="background-color: rgb(255, 255, 255);"></div>
                            <div class="swal2-success-circular-line-right" style="background-color: rgb(255, 255, 255);"></div>
                        </div>
                    </div>
                    <div class="modal-body">
                        <p id="modalMensaje">La operacion se realizo correctamente</p>
                    </div>
                    <div class="modal-footer justify-content-center">
                        <a id="aModal" class="btn btn-primary entrada-sin-label" href="DashboardIframe.aspx" target="basefrm">Aceptar</a>
                        <button id="btnModal" class="btn btn-primary entrada-sin-label" onclick="window.close()">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modalError" class="wrapper cargando text-center collapse">
        <div class="modal fade ps show" id="noticeModalError" tabindex="-1" role="dialog" style="display: block;">
            <div class="modal-dialog modal-notice">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<h5 class="modal-title" id="modalErrorTitulo">Operacion realizada</h5>--%>
                        <div class="swal2-icon swal2-warning swal2-animate-warning-icon" style="display: flex;"><span class="swal2-icon-text">!</span></div>
                    </div>
                    <div class="modal-body">
                        <p id="modalErrorMensaje">La operacion se realizo correctamente</p>
                    </div>
                    <div class="modal-footer justify-content-center">
                        <a id="aModalError" class="btn btn-primary entrada-sin-label" href="#" onclick="ocultarModalError();" >Aceptar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="wrapper ">
        <form runat="server">
            <div class="sidebar" data-color="blue">

                <div class="logo" style="text-align:center">
                    <a class="simple-text logo-normal" href="IndexIframe.aspx" target="basefrm">
                         <asp:Image ID="logo" runat="server"  style="max-height:96px;max-width:238px" /></a>
<span class="simple-text" style="color: #898FA3 ;font-size: x-small; text-align: center; padding: 0px; text-transform:unset"><% Response.Write(obtenerVersion())%></span>                </div>

                <div class="sidebar-wrapper" id="sidebar-wrapper">
                    <div class="user">
                        <div class="photo">
                            <img id="fotoPerfil" class="NO-CACHE" src="<% Response.Write(urlFotoPerfil())%>" />
                        </div>
                        <div class="info">
                            <a data-toggle="collapse" href="#collapseExample" class="collapsed">
                                <span style="color: black">
                                    <% Response.Write(usuario()) %>
                                    <b class="caret"></b>
                                </span>
                            </a>
                            <div class="clearfix"></div>
                            <div class="collapse" id="collapseExample">
                                <ul class="nav" style="text-align:center">
                                    <li>
                                        <a href="UsuarioModificarPerfil.aspx" onclick="marcarActivo(this);" target="basefrm">
                                            <span class="sidebar-mini-icon"></span>
                                            <span class="sidebar-normal">Modificar perfil</span>
                                        </a>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="btnSalir" runat="server" ClientIDMode="Static" CausesValidation="False">
                                            <span class="sidebar-mini-icon"></span>
                                            <span class="sidebar-normal">Cerrar sesión</span>
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div style="padding-top: 10px; margin-bottom: 0px!important; background-color: none!important; box-shadow: none!important;">
                        <div class="navbar navbar-expand justify-content" style="display: <% Response.Write(contarAccesosDirectos())%>; padding-top: 10px; margin-bottom: 0px!important; background-color: none!important; box-shadow: none!important;">
                            <ul class="navbar-nav">
                                <% Response.Write(armarAccesosDirectos()) %>
                            </ul>
                        </div>
                    </div>

                    <div class="nav form-group bmd-form-group" style="margin: 0px 15px;">
                           <input id="txtFiltroMenu" style="text-transform:unset"  placeholder="Buscar..." type="text" value="" class="form-control search-menu-box search-box">
                        <%--<button id="botonBorrar" ></button>--%>
                    </div>
                    <%--onkeyup="endAndStartTimer();"--%>
                    <ul class="nav" id="nav">
                        <li class="active" style="display: <%Response.Write(mostrarBotonResponsive())%>;" >
                            <a href="DashboardIframe.aspx" onclick="marcarActivo(this);" class="Nivel1" target="basefrm">
                                <i class="now-ui-icons media-2_sound-wave"></i>
                                <p>Dashboard</p>
                            </a>
                        </li>
                        <% Response.Write(armarArbol()) %>
                    </ul>

                </div>
            </div>
        </form>

        <div id="zoliframe">
            <iframe name="basefrm" src="<%Response.Write(mostrarDashBoard())%>" style="height: 100vh; width: 100%; overflow-x: hidden; margin-bottom: -15px;"></iframe>
        </div>
    </div>

    <script src="assets/js/core/jquery.min.js"></script>
    <script src="assets/js/core/popper.min.js"></script>
    <script src="assets/js/core/bootstrap.min.js"></script>
    <script src="assets/js/plugins/perfect-scrollbar.jquery.min.js"></script>
    <script src="assets/js/plugins/moment.min.js"></script>
    <script src="assets/js/plugins/bootstrap-switch.js"></script>
    <script src="assets/js/plugins/sweetalert2.min.js"></script>
    <script src="assets/js/plugins/jquery.validate.min.js"></script>
    <script src="assets/js/plugins/jquery.bootstrap-wizard.js"></script>
    <script src="assets/js/plugins/bootstrap-selectpicker.js"></script>
    <script src="assets/js/plugins/bootstrap-datetimepicker.js"></script>
    <script src="assets/js/plugins/jquery.dataTables.min.js"></script>
    <script src="assets/js/plugins/bootstrap-tagsinput.js"></script>
    <script src="assets/js/plugins/jasny-bootstrap.min.js"></script>
    <script src="assets/js/plugins/fullcalendar.min.js"></script>
    <script src="assets/js/plugins/jquery-jvectormap.js"></script>
    <script src="assets/js/plugins/nouislider.min.js"></script>
    <script src="assets/js/plugins/chartjs.min.js"></script>
    <script src="assets/js/plugins/bootstrap-notify.js"></script>
    <script src="assets/js/now-ui-dashboard.js?v=1.4.1" type="text/javascript"></script>
    <script src="assets/js/fontawesome/all.min.js"></script>
    <script src="assets/js/jquery.searchable-1.1.0.min.js"></script>
    <script src="assets/site/site.js"></script>

    <script>
        (function () {

            isWindows = navigator.platform.indexOf('Win') > -1 ? true : false;

            if (isWindows) {
                // if we are on windows OS we activate the perfectScrollbar function
                var ps = new PerfectScrollbar('.sidebar-wrapper');

                $('html').addClass('perfect-scrollbar-on');
            } else {
                $('html').addClass('perfect-scrollbar-off');
            }
        })();

        var timeout;
        var delay = 500;

        function filtrarMenuJSDos() {
            $('#nav').searchable({
                searchField: '#txtFiltroMenu',
                selector: 'li',
                childSelector: 'p',
                show: function (elem) {
                    elem.show();
                },
                hide: function (elem) {
                    elem.slideUp(50);
                }
            })
        }

        $('#txtFiltroMenu').keyup(function (e) {
            if (timeout) {
                clearTimeout(timeout);
            }
            timeout = setTimeout(function () {
                filtrarMenuJSDos();
            }, delay);
        });

        function marcarActivo(control) {
            $("li").each(function () {
                $(this).removeClass("active");
            })
            $(control).parent().addClass("active");
        }

        $("[target='basefrm']").on('click', function () {
            $("#loading").show();
        });

        $(document).ready(function () {
            //$('.NO-CACHE').attr('src', function () { return $(this).attr('src') + "?a=" + Math.random() });

            $('i').each(function (index, value) {
                $(this).attr("class", $(this).attr("class").replace("xxx", " "));
            });

        //$("#botonBorrar").on('click', function () {
        //    $('#txtFiltroMenu').val("");
        //    $("#txtFiltroMenu").keyup();
        //});
        })

        $("#aModal").on('click', function () {
            $("#modal").hide();
        });

                    $('.solonumeros').keypress(function (event) {
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            }); 


        function ocultarCargandoModal() {
            setTimeout(function () { $('#loading').hide() }, 500);
        }

        $('#txtFiltroMenu').keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
            }
        });
        
    </script>
</body>

</html>