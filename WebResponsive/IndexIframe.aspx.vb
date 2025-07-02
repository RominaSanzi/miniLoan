Imports System.IO
Imports System.Reflection
Imports di.financiera.reglasnegocios
Imports di.financiera.seguridad

Public Class IndexIframe
    Inherits System.Web.UI.Page

#Region "Metodos"

    Private Sub abrirCarteleraModal()
        Dim abrirCarteleraModal As String = "abrirPopUpSinPostback('IntranetCarteleraPopUpConsulta.aspx',this.id,630,440,this.id);"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "abrirCarteleraModal", abrirCarteleraModal, True)
    End Sub

    'Private Sub mostrarCartelera()
    '    'Dim iAdministradorExternos As AdministradorExternos
    '    Try
    '        'If Not IsNothing(Session(Session.SessionID & "Usuario")) AndAlso Session(Session.SessionID & "Usuario").rolesAutorizados.BinarySearch(Session(Session.SessionID & "Usuario").rolesAutorizados, "mostrarIntraneCarteleraAlInicio") > 0 Then
    '        '    iAdministradorExternos = New AdministradorExternos
    '        '    If iAdministradorExternos.poseeIntranetCarteleraAMostrar(Session(Session.SessionID & "Usuario").perfiles) Then abrirCarteleraModal()
    '        'End If
    '    Catch Exception As Exception
    '        If Not TypeOf (Exception) Is Threading.ThreadAbortException Then
    '            Session(Session.SessionID & "ultimaExcepcion") = Exception
    '            Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
    '            Response.Redirect("Error.aspx", True)
    '        End If
    '    Finally
    '        'iAdministradorExternos = Nothing
    '    End Try
    'End Sub

    Private Sub mostrarLogo()
        Dim iUsuario As Usuario
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        Dim iImagenNombre, iCarpeta As String

        Dim iServerPath As String() = ConfigurationManager.AppSettings("archivosImagenes").Split("\")

        Try

            iCarpeta = iServerPath(iServerPath.Length - 2)

            iUsuario = Session(Session.SessionID & "usuario")

            If Not IsNothing(iUsuario) Then
                iImagenNombre = iAdministradorUsuarios.obtenerLogo(iUsuario.nivel)

                If iImagenNombre <> Nothing AndAlso File.Exists(ConfigurationManager.AppSettings("archivosImagenes") & iImagenNombre & ".png") Then
                    logo.ImageUrl = "~/" & iCarpeta & "/" & iImagenNombre & ".png"
                Else
                    logo.ImageUrl = "~/" & iCarpeta & "/logodefault.png"
                End If
            End If


        Catch exception As Exception
            logo.ImageUrl = "~/" & iCarpeta & "/logodefault.png"
        Finally
            iUsuario = Nothing
            iAdministradorUsuarios = Nothing
        End Try

    End Sub

    Private Sub mostrarMensajeFacturacion()
        'Dim iAdministradorComprobantes As New AdministradorComprobantes
        Dim iMensaje As String

        'Try
        '    If IsNothing(Session(Session.SessionID & "usuario")) Then Return

        '    iMensaje = iAdministradorComprobantes.obtenerMensajesFacturacion(Session(Session.SessionID & "usuario"))
        '    If iMensaje <> Nothing Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pendientes", "mostrarPendientesSincronizar('" & iMensaje & "');", True)
        '    End If

        'Catch Exception As Exception

        'Finally
        '    iAdministradorComprobantes = Nothing
        'End Try
    End Sub

    Private Sub logout()
        Dim iAdministradorUsuarios As New AdministradorUsuarios

        Try
            iAdministradorUsuarios.logoutUsuario(CType(Session(Session.SessionID & "usuario"), Usuario))
        Catch Exception As Exception
        Finally
            iAdministradorUsuarios = Nothing
        End Try

        Session.Abandon()
        Session.RemoveAll()
        FormsAuthentication.SignOut()
        Response.Write("<script>")
        Response.Write("window.open('login.aspx?ReturnUrl=Index.aspx','_top')")
        Response.Write("</script>")
    End Sub

    Public Function mostrarDashBoard() As String
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        Dim iUsuario As Usuario

        Try
            iUsuario = CType(Session(Session.SessionID & "usuario"), Usuario)

            Return "DashboardIframe.aspx"

        Catch exception As Exception
        Finally
            iAdministradorUsuarios = Nothing
            iUsuario = Nothing
        End Try
    End Function

    Public Function armarArbol() As String
        Dim iAdministradorUsuarios As New AdministradorUsuarios()
        Dim iUsuario As Usuario = Session(Session.SessionID & "usuario")
        Dim iArbolPerfil As String = Session(Session.SessionID & "ArbolPerfil")

        Try
            If Not IsNothing(iUsuario) AndAlso iArbolPerfil <> Nothing Then
                Return iArbolPerfil
            ElseIf Not IsNothing(iUsuario) Then
                iArbolPerfil = iAdministradorUsuarios.presentarMenuResponsivo(iUsuario.perfiles)
                Session(Session.SessionID & "ArbolPerfil") = iArbolPerfil
                Return iArbolPerfil
            Else
                Response.Redirect("login.aspx", True)
            End If


        Catch exception As Exception

        Finally
            iAdministradorUsuarios = Nothing
            iUsuario = Nothing
        End Try
    End Function

    Public Function armarAccesosDirectos() As String
        Dim iAdministradorUsuarios As New AdministradorUsuarios()
        Dim iUsuario As Usuario = Session(Session.SessionID & "usuario")
        Dim iComillas As String = Chr(34)
        Dim iAccesos As String
        Dim i As Integer
        Dim iPagina As String
        Try

            If Not IsNothing(iUsuario) Then
                For i = 0 To iUsuario.accesosDirectos.Count - 1
                    With iUsuario.accesosDirectos(i)
                        iPagina = .pagina
                        If iPagina.IndexOf("?") > 0 Then iPagina = Left(iPagina, iPagina.IndexOf("?"))

                        If Not IsNothing(iUsuario) AndAlso iAdministradorUsuarios.puedeAcceder(iUsuario, iPagina) Then
                            If .abrirModal Then
                                'iAccesos &= "<input type=" & iComillas & "image" & iComillas & " name=" & iComillas & .descripcion.ToLower & iComillas & " id=" & iComillas & .descripcion.ToLower & iComillas & " tabindex=" & iComillas & i & iComillas & " title=" & iComillas & .descripcion.ToLower & iComillas & " onClick=" & iComillas & "abrirPopUpSinPostback('" & .pagina & "',this.id,1000,600,this.id);" & iComillas & " src=" & iComillas & "imagenes/" & .imagen & iComillas & " border=" & iComillas & "0" & iComillas & " />"
                                iAccesos &= "<li class=""nav-item"" style=""margin-top:0px!important"" data-toggle='tooltip' title='" & .descripcion.ToLower & "' ToolTip='" & .descripcion.ToLower & "' ><a style='cursor:pointer;' href='javascript:void(0);'  Class=""nav-link"" target='basefrm' onclick=""abrirPopUpSinPostback('" & .pagina & "',this.id,1000,600,this.id);ocultarCargandoModal();"">" & .imagen & "</a></li>"
                            Else
                                'iAccesos &= "<A href=" & .pagina & " target=" & iComillas & "basefrm" & iComillas & "><IMG title=" & iComillas & .descripcion.ToLower & iComillas & " alt=" & iComillas & .descripcion.ToLower & iComillas & " src=" & iComillas & "imagenes/" & .imagen & iComillas & " border=" & iComillas & "0" & iComillas & "></A><IMG src=" & iComillas & "imagenes/blanco.gif" & iComillas & " border=" & iComillas & "0" & iComillas & " style=" & iComillas & "WIDTH: 5px; HEIGHT: 16px" & iComillas & ">"
                                iAccesos &= "<li class=""nav-item"" style=""margin-top:0px!important"" data-toggle='tooltip' title='" & .descripcion.ToLower & "' ToolTip='" & .descripcion.ToLower & "' ><a href = '" & .pagina & "' Class=""nav-link""     target='basefrm' onclick='cargando();'>" & .imagen & "</a></li>"
                            End If
                        End If
                    End With
                Next


                '    ' data-toggle='tooltip' es el estilo del resto, pero se oculta tras el nav TODO
                '    If iUsuario.usuarioComercio.Booleano Then
                '        If iAdministradorUsuarios.puedeAcceder(iUsuario, "ClienteBusquedaComercio.aspx") Then
                '        End If
                '    Else
                '        If iAdministradorUsuarios.puedeAcceder(iUsuario, "ClienteBusqueda.aspx") Then
                '            iAccesoDirectos = "<li class=""nav-item"" style=""margin-top:0px!important"" data-toggle='tooltip' title='Cliente Busqueda' ToolTip='Cliente Busqueda' ><a href = 'ClienteBusqueda.aspx'  Class=""nav-link""    style=""margin-top:0px""   target='basefrm' onclick='cargando();'><i Class='fa fa-search-plus fa-2x'></i></a></li>"
                '        End If
                '    End If

                '    If iAdministradorUsuarios.puedeAcceder(iUsuario, "SolicitudAltaPorPasos0.aspx") Then
                '        iAccesoDirectos &= "<li class=""nav-item"" style=""margin-top:0px!important"" data-toggle='tooltip' title='Solicitud alta por pasos' ToolTip='Solicitud por pasos' ><a href = 'SolicitudAltaPorPasos0.aspx'  Class=""nav-link""    style=""margin-top:0px""   target='basefrm' onclick='cargando();'><i Class='fas fa-hand-holding-usd fa-2x'></i></a></li>"
                '    End If

                '    If iAdministradorUsuarios.puedeAcceder(iUsuario, "CalculadorCuotas.aspx") Then
                '        iAccesoDirectos &= "<li class=""nav-item"" style=""margin-top:0px!important"" data-toggle='tooltip' title='Calculador de cuotas' ToolTip='Calculador de cuotas' ><a href = 'CalculadorCuotas.aspx'  Class=""nav-link""    style=""margin-top:0px""   target='basefrm' onclick='cargando();'><i Class='fas fa-calculator fa-2x'></i></a></li>"
                '    End If

                '    If iAdministradorUsuarios.puedeAcceder(iUsuario, "SolicitudPendienteBandejaEntrada.aspx") Then
                '        iAccesoDirectos &= "<li class=""nav-item"" style=""margin-top:0px!important"" data-toggle='tooltip' title='Bandeja solicitud pendiente' ToolTip='Bandeja solicitud pendiente' ><a href = 'SolicitudPendienteBandejaEntrada.aspx'  Class=""nav-link""    style=""margin-top:0px""   target='basefrm' onclick='cargando();'><i Class='fas fa-inbox fa-2x'></i></a></li>"
                '    End If

                '    If iAdministradorUsuarios.puedeAcceder(iUsuario, "ConvenioAlta.aspx") Then
                '        iAccesoDirectos &= "<li class=""nav-item"" style=""margin-top:0px!important"" data-toggle='tooltip' title='Alta de convenio' ToolTip='Alta de convenio' ><a href = 'ConvenioAlta.aspx'  Class=""nav-link""    style=""margin-top:0px""   target='basefrm' onclick='cargando();'><i Class='fas fa-handshake fa-2x'></i></a></li>"
                '    End If


                Return iAccesos
            Else
                Response.Redirect("login.aspx", True)
            End If

        Catch exception As Exception

        Finally
            iAdministradorUsuarios = Nothing
            iUsuario = Nothing
        End Try
    End Function

    Public Function mostrarBotonResponsive() As String
        Dim iAdministradorUsuarios As New AdministradorUsuarios()
        Dim iUsuario As Usuario = Session(Session.SessionID & "usuario")

        Try
            If Not IsNothing(iUsuario.dashboard) Then
                Return "block"
            Else
                Return "none"
            End If

        Catch exception As Exception

        Finally
            iAdministradorUsuarios = Nothing
            iUsuario = Nothing
        End Try
    End Function

    Public Function contarAccesosDirectos() As String
        Dim iAdministradorUsuarios As New AdministradorUsuarios()
        Dim iUsuario As Usuario = Session(Session.SessionID & "usuario")

        Try

            If Not IsNothing(iUsuario) Then
                If Not IsNothing(iUsuario.accesosDirectos) AndAlso iUsuario.accesosDirectos.Count > 0 Then
                    Return "block"
                Else
                    Return "none"
                End If
            Else
                Return "none"
            End If

        Catch exception As Exception

        Finally
            iAdministradorUsuarios = Nothing
            iUsuario = Nothing
        End Try
    End Function

    Public Function usuario() As String
        Dim iUsuario As Usuario = Session(Session.SessionID & "usuario")

        Try
            If Not IsNothing(iUsuario) Then
                Return iUsuario.nombre.Split(" ").ElementAt(0)
            Else
                Return Nothing
            End If

        Catch exception As Exception
            Session(Session.SessionID & "ultimaExcepcion") = exception
            Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
            Response.Redirect("PaginaError.aspx", True)
        Finally
            iUsuario = Nothing
        End Try
    End Function

    Public Function temaElegido() As String
        Dim iUsuario As Usuario = Session(Session.SessionID & "usuario")

        Try
            'If Not IsNothing(iUsuario) Then
            '    If (iUsuario.temaElegido = "") Then
            '        iUsuario.temaElegido = "skin-blue"
            '    End If
            '    Return iUsuario.temaElegido
            'Else
            '    Return Nothing
            'End If

            Return Nothing

        Catch exception As Exception
            Session(Session.SessionID & "ultimaExcepcion") = exception
            Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
            Response.Redirect("PaginaError.aspx", True)
        Finally
            iUsuario = Nothing
        End Try
    End Function


    Public Function urlFotoPerfil() As String
        Dim iUsuario As Usuario = Session(Session.SessionID & "usuario")

        Try
            If Not IsNothing(iUsuario) Then
                'si esta vacio devuelvo la que viene x defecto

                If iUsuario.fotoPerfil <> Nothing Then
                    'Return ConfigurationManager.AppSettings("fotosPerfil") & iUsuario.id & ".jpg"
                    'Return "fotosperfiles/" & iUsuario.id & ".jpg"
                    Return iUsuario.fotoPerfil
                Else
                    Return "assets/img/arg.png"
                End If
            End If

            Return Nothing

        Catch exception As Exception
        Finally
            iUsuario = Nothing
        End Try
    End Function

    Public Function obtenerVersion() As String
        Return "Versión: " & System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString & " - Fecha: " & CType(AssemblyCopyrightAttribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly, GetType(AssemblyCopyrightAttribute)), AssemblyCopyrightAttribute).Copyright
    End Function
#End Region

#Region "Web Metod"

    <System.Web.Services.WebMethod()>
    Public Shared Function filtrarArbol(ByVal filtro As String) As String
        Dim iAdministradorUsuarios As New AdministradorUsuarios()
        Dim iUsuario As Usuario = HttpContext.Current.Session(HttpContext.Current.Session.SessionID & "usuario")
        Dim iArbolPerfil As String = HttpContext.Current.Session(HttpContext.Current.Session.SessionID & "ArbolPerfil")

        Try
            If IsNothing(iUsuario) Then
                Return ""
            Else
                iArbolPerfil = iAdministradorUsuarios.presentarMenuResponsivo(iUsuario.perfiles)
                Return iArbolPerfil
            End If

        Catch exception As Exception

        Finally
            iAdministradorUsuarios = Nothing
            iUsuario = Nothing
        End Try
    End Function

#End Region

#Region "Botones"
    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        logout()
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(Session(Session.SessionID & "Usuario")) AndAlso Array.BinarySearch(Session(Session.SessionID & "Usuario").rolesAutorizados, "validarComprobantesPendientesSincronizacion") > 0 Then
            mostrarMensajeFacturacion()
        End If
        'mostrarCartelera()
        mostrarLogo()
    End Sub

End Class