<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="di.financiera.webResponsive.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0'/>
    <title>Loan</title>

    <!-- Favicons -->
    <link rel="apple-touch-icon" href="assets/img/apple-touch-icon.png">
    <link rel="icon" href="assets/img/favicon.png">

    <%-- Yummy Login CSS --%>
    <link href="assets/yummy-login/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/yummy-login/common/css/source/main.css" rel="stylesheet" />

    <%-- Yummy Login JS --%>
    <script src="assets/yummy-login/jquery/jquery.min.js"></script>
    <script src="assets/yummy-login/html5-form-validation/dist/jquery.validation.min.js" type="text/javascript"></script>
    <script src="assets/yummy-login/bootstrap-show-password/bootstrap-show-password.min.js" type="text/javascript"></script>
    <script src="assets/yummy-login/gsap/src/minified/TweenMax.min.js"></script>
    <script src="assets/yummy-login/common/js/common.js" type="text/javascript"></script>

</head>

<body>
    <style>
        #myVideo {
            position: fixed;
            right: 0;
            bottom: 0;
            min-width: 100%;
            min-height: 100%;
        }

        .btn-primary {   
            border-radius: 80px!important;
              border-color: transparent!important;
              width: 100%!important;
              background: linear-gradient(125deg, #303698 0%, #5B247A 100%)!important;
              font-weight: 600!important;
        }

        .btn.btn-primary:hover:active, .btn.btn-primary:focus, .btn.btn-primary.active, .open > .btn.btn-primary:hover:active, .open > .btn.btn-primary:focus, .open > .btn.btn-primary.active { 
              border-radius: 80px!important;
              border-color: transparent!important;
              width: 100%!important;
              background: linear-gradient(125deg, #303698 0%, #5B247A 100%)!important;
              font-weight: 600!important;
              filter: brightness(0.75)!important;
        }
    </style>

    <video autoplay muted loop id="myVideo">
        <source src="assets/yummy-login/img/PuertoMadero.mp4" type="video/mp4">
    </video>

        <div class="page-content-inner">
            <%-- LOGO --%>
            <div class="single-page-block-header">
                <div class="row">
                    <div class="col-lg-4">
                        <div class="logo" style="max-height:100px;max-width:250px">
                            <a href="https://www.divisioninformatica.com/">
                                <img src="assets/yummy-login/img/logo.png" alt="Loan" style="filter:brightness(10)" />
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <%-- FIN LOGO --%>

            <div class="single-page-block">
                <div class="single-page-block-inner effect-3d-element" style="background-color: rgba(255, 255, 255, 0.5);">
                    <div class="blur-placeholder">
                    </div>
                    <asp:Panel runat="server" ID="Panel2" HorizontalAlign="Center">
                        <asp:Image ID="logo" runat="server" style="max-height:96px;max-width:238px"/>
                    </asp:Panel> 
                    <div class="single-page-block-form">
                        <br />
                        <h3 class="text-center" style="color: black; font-size:large" >
                            <i class="icmn-enter margin-right-10"></i>
                            Ingresar
                        </h3>
                        
                        <form runat="server" role="form" class="login-form">
                            <%-- CAMPOS LOGIN --%>
                            <div class="form-group">
                                <label class="sr-only" for="form-username">Username</label>
                                <cc1:TextBoxUp ID="txtLogin" name="form-username" Width="100%" runat="server" placeholder="Usuario..." class="form-username form-control" AutoComplete="False" required="required" title="Tenes que ingresar el usuario" TabIndex="10"></cc1:TextBoxUp>
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-password">Password</label>
                                <asp:TextBox ID="txtContrasenia" name="form-password" Width="100%" placeholder="Password..." class="form-password form-control" runat="server" style="text-transform:none" AutoComplete="False" TextMode="Password" required="required" title="Tenes que ingresar el Password" TabIndex="10"></asp:TextBox>
                            </div>

                            <%-- CAMPOS CAMBIAR PASS --%>
                            <div class="form-group">
                                <label class="sr-only" for="form-nuevapassword" visible="False">Nuevo password</label>
                                <asp:TextBox Visible="False" ID="txtNuevaContrasenia" name="form-nuevapassword" Width="100%" placeholder="Nuevo password..." class="form-password form-control" style="text-transform:none" runat="server" AutoComplete="False" TextMode="Password" TabIndex="10"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label class="sr-only " for="form-repetirnuevapassword" visible="False">Repetir nuevo password</label>
                                <asp:TextBox ID="txtRepetirContrasenia" Visible="False" name="form-repetirnuevapassword" Width="100%" placeholder="Repetir nuevo password..." class="form-password form-control" style="text-transform:none" runat="server" AutoComplete="False" TextMode="Password" TabIndex="10"></asp:TextBox>
                            </div>

                            <%-- OLVIDO CONTRASEÑA --%>
                            <div class="form-group">
                                <asp:LinkButton ID="btnOlvidoContrasenia" CssClass="pull-right" style="color: black;" runat="server" Width="100%" ToolTip="Olvidé mi contraseña" Text="Olvidé mi contraseña" TabIndex="20"></asp:LinkButton>
                                <div class="checkbox">
                                </div>
                            </div>
                            <%-- INGRESAR --%>
                            <div class="form-actions">
                                <asp:Button ID="btnIngresar" CssClass="btn btn-primary width-150" runat="server" Width="100%" ToolTip="Ingresar" Text="Login" TabIndex="10"></asp:Button>
                            </div>
                            
                        </form>
                    </div>
                </div>
            </div>
        </div>
    <script src="assets/js/plugins/sweetalert2.min.js"></script>
    <script src="assets/site/site.js"></script>
    <script>

        $(document).ready(function () {
            $(function () {

                // Form Validation
                $('#form-validation').validate({
                    submit: {
                        settings: {
                            inputContainer: '.form-group',
                            errorListClass: 'form-control-error',
                            errorClass: 'has-danger'
                        }
                    }
                });

                // Show/Hide Password
                $('.password').password({
                    eyeClass: '',
                    eyeOpenClass: 'icmn-eye',
                    eyeCloseClass: 'icmn-eye-blocked'
                });

                // Add class to body for change layout settings
                $('body').addClass('single-page single-page-inverse');

                // Set Background Image for Form Block
                function setImage() {
                    var imgUrl = $('.page-content-inner').css('background-image');

                    $('.blur-placeholder').css('background-image', imgUrl);
                };

                function changeImgPositon() {
                    var width = $(window).width(),
                            height = $(window).height(),
                            left = -(width - $('.single-page-block-inner').outerWidth()) / 2,
                            top = -(height - $('.single-page-block-inner').outerHeight()) / 2;


                    $('.blur-placeholder').css({
                        width: width,
                        height: height,
                        left: left,
                        top: top
                    });
                };

                setImage();
                changeImgPositon();

                $(window).on('resize', function () {
                    changeImgPositon();
                });

                // Mouse Move 3d Effect
                var rotation = function (e) {
                    var perX = (e.clientX / $(window).width()) - 0.5;
                    var perY = (e.clientY / $(window).height()) - 0.5;
                    TweenMax.to(".effect-3d-element", 0.4, { rotationY: 15 * perX, rotationX: 15 * perY, ease: Linear.easeNone, transformPerspective: 1000, transformOrigin: "center" })
                };

                if (!cleanUI.hasTouch) {
                    $('body').mousemove(rotation);
                }

                window.parent.parent.$("#loading").hide();
                window.parent.$("#loading").hide();
                $("#loading").hide();

            });
        });


    </script>

</body>

</html>