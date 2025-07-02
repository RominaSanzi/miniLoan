Imports System.IO
Imports di.financiera.excepciones
Imports di.financiera.reglasnegocios
Imports di.financiera.seguridad
Imports di.financiera.entidades

Public Class BaseIncludes
    Inherits System.Web.UI.MasterPage

#Region "Ayuda"
    Private Sub ShowPopUp(ByVal ePaginaPopUp As String, ByVal eWidth As Integer, ByVal eHeight As Integer, Optional ByVal eResizable As String = "no", Optional ByVal eScrollBars As String = "no", Optional ByVal eStatus As String = "no")
        Dim abrir As String = "var window_width = " & eWidth & ";" &
                    "var window_height = " & eHeight & ";" &
                    "var newfeatures= 'resizable=yes,scrollbars=yes';" &
                    "var window_top = (screen.height-window_height)/2;" &
                    "var window_left = (screen.width-window_width)/2;" &
                    "<iframe width=""560"" height=""315"" src=""https://www.youtube.com/embed/TfrVjDxIlsA?start=4"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>" &
                    "window.open('" & ePaginaPopUp & "', 'Ayuda','width=' + window_width + '%,height=' + window_height + '%,top=' + window_top + ',left=' + window_left + ',features=' + newfeatures + '');"

        ScriptManager.RegisterStartupScript(Me, GetType(Page), "abrir", abrir, True)
    End Sub

    Public Sub ayuda(ByVal ePaginaPopUp As String, Optional eWidth As Integer = 750, Optional eHeight As Integer = 400)
        Dim iPaginaAyuda As String
        Try
            iPaginaAyuda = "Ayuda/" & Left(ePaginaPopUp, Len(ePaginaPopUp) - 4) & "htm"
            ShowPopUp(iPaginaAyuda, eWidth, eHeight)

            iPaginaAyuda = "modalAyuda('https://www.youtube.com/embed/TfrVjDxIlsA?start=4', '" & iPaginaAyuda & "');"
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "paginaAyuda", iPaginaAyuda, True)

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "modal"
    Public Sub modal(ePagina As String, eTitulo As String, eMensaje As String)
        Dim modal As String

        If ePagina = Nothing AndAlso Not IsNothing(Session(Session.SessionID & "ultimaPagina")) Then
            ePagina = Session(Session.SessionID & "ultimaPagina")
        ElseIf ePagina = Nothing AndAlso IsNothing(Session(Session.SessionID & "ultimaPagina")) Then
            ePagina = ConfigurationManager.AppSettings("ultimaPaginaPorDefecto")
        End If

        Dim ePaginaCerrar As String = Request.UrlReferrer.AbsoluteUri
        ePaginaCerrar = Split(ePaginaCerrar, "/")(Split(ePaginaCerrar, "/").Length - 1)
        If ePaginaCerrar.Contains("?") Then
            ePaginaCerrar = ePaginaCerrar.Split("?")(0)
        End If

        eMensaje = eMensaje.Replace(vbNewLine, "\n").Replace("'", "")
        modal = "modalPopUp('" & ePagina & "', '" & eTitulo & "', '" & eMensaje & "', '" & ePaginaCerrar & "');"

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "modal", modal, True)

    End Sub

    Public Sub modalPopUpRedireccionar(ePagina As String, eTitulo As String, eMensaje As String)
        Dim modal As String

        If ePagina = Nothing AndAlso Not IsNothing(Session(Session.SessionID & "ultimaPagina")) Then
            ePagina = Session(Session.SessionID & "ultimaPagina")
        ElseIf ePagina = Nothing AndAlso IsNothing(Session(Session.SessionID & "ultimaPagina")) Then
            ePagina = ConfigurationManager.AppSettings("ultimaPaginaPorDefecto")
        End If

        Dim ePaginaCerrar As String = Request.UrlReferrer.AbsoluteUri
        ePaginaCerrar = Split(ePaginaCerrar, "/")(Split(ePaginaCerrar, "/").Length - 1)
        If ePaginaCerrar.Contains("?") Then
            ePaginaCerrar = ePaginaCerrar.Split("?")(0)
        End If

        eMensaje = eMensaje.Replace(vbNewLine, "\n").Replace("'", "")
        modal = "modalPopUpRedireccionar('" & ePagina & "', '" & eTitulo & "', '" & eMensaje & "', '" & ePaginaCerrar & "');"

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "modal", modal, True)

    End Sub

    Public Sub modalError(ePagina As String, eTitulo As String, eMensaje As String)
        Dim modal As String
        Dim iExcepcion As Exception
        Dim iErrorAplicacionVO As ErrorAplicacionVO

        Try

            If IsNothing(Session(Session.SessionID & "usuario")) Then
                eMensaje = "La sesión de usuario expiró, debera loguearse nuevamente al sistema"
            Else
                iExcepcion = Session(Session.SessionID & "ultimaExcepcion")

                If Not IsNothing(iExcepcion) Then
                    iErrorAplicacionVO = Log.obtenerErrorAplicacion(iExcepcion, Request.UrlReferrer.ToString, CType(Session(Session.SessionID & "usuario"), seguridad.Usuario))
                    eMensaje = iErrorAplicacionVO.mensajeUsuario
                End If
            End If

            If ePagina = Nothing AndAlso Not IsNothing(Session(Session.SessionID & "ultimaPagina")) Then
                ePagina = Session(Session.SessionID & "ultimaPagina")
            ElseIf ePagina = Nothing AndAlso IsNothing(Session(Session.SessionID & "ultimaPagina")) Then
                ePagina = ConfigurationManager.AppSettings("ultimaPaginaPorDefecto")
            End If

            eMensaje = eMensaje.Replace(vbNewLine, "\n").Replace("'", "")

            Dim ePaginaCerrar As String = Request.UrlReferrer.AbsoluteUri
            ePaginaCerrar = Split(ePaginaCerrar, "/")(Split(ePaginaCerrar, "/").Length - 1)
            If ePaginaCerrar.Contains("?") Then
                ePaginaCerrar = ePaginaCerrar.Split("?")(0)
            End If

            modal = "document.addEventListener('DOMContentLoaded', function(event) { modalErrorPopUp('" & ePagina & "', '" & eTitulo & "', '" & eMensaje & "', '" & ePaginaCerrar & "') });;"

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "modalError", modal, True)

        Catch exception As Exception
            'Si hay error lo controlo pero no hago nada
        Finally
            iExcepcion = Nothing
            'removerSessiones() --> comento porque sigue en la misma página y si remueve las sessiones x hace cagada
        End Try
    End Sub

    Private Sub loguearAccion(ByVal iException As String)
        Dim iLog As New Log()
        Dim iAdministradorUsuarios As New AdministradorUsuarios()
        Dim iDetalle As String
        Try
            iLog.fecha = Now
            iLog.usuario = Session(Session.SessionID & "usuario")
            iLog.accion = New Accion
            iDetalle = "ORIGEN:" & Right(Session(Session.SessionID & "ultimaPagina"), Len(Session(Session.SessionID & "ultimaPagina")) - InStrRev(Session(Session.SessionID & "ultimaPagina"), "/")) & vbNewLine
            iDetalle = iDetalle & "EXCEPCION:" & iException
            iLog.detalle = iDetalle.Replace("'", "")
            iLog.accion.id = DatosAccion.getInstancia.acciones.Item("Error.aspx")

            iAdministradorUsuarios.crearLog(iLog)

        Catch exception As Exception
            Throw New LogNoCreadoException
        Finally
            iAdministradorUsuarios = Nothing
            iLog = Nothing
        End Try
    End Sub
#End Region

#Region "descarga"
    Public Sub descargarArchivo(ePathImagen As String, eNombreImagen As String)
        Dim iDescargar As String
        Dim iImagenBase64 As String

        Try

            iImagenBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(ePathImagen))
            iDescargar = "descargar('" & iImagenBase64 & "', '" & eNombreImagen & "');"

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "descargar", iDescargar, True)

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Metodos"
    'Public Function armarArbol() As String
    '    Dim iAdministradorUsuarios As New AdministradorUsuarios()
    '    Dim iUsuario As Usuario = Session(Session.SessionID & "usuario")
    '    Dim iArbolPerfil As String = Session(Session.SessionID & "ArbolPerfil")
    '    Dim iIdPerfil As String = Session(Session.SessionID & "idPerfil")

    '    Try
    '        If Not IsNothing(iUsuario) AndAlso iArbolPerfil <> Nothing AndAlso iUsuario.perfil.id = iIdPerfil Then 'AndAlso txtFiltro.Value = iFiltro Then
    '            Return iArbolPerfil
    '        ElseIf Not IsNothing(iUsuario) Then
    '            '    iFiltro = txtFiltro.Value
    '            iArbolPerfil = iAdministradorUsuarios.presentarMenuResponsivo(iUsuario.perfil)
    '            Session(Session.SessionID & "ArbolPerfil") = iArbolPerfil
    '            Session(Session.SessionID & "idPerfil") = iUsuario.perfil.id
    '            Return iArbolPerfil
    '        Else
    '            Response.Redirect("login.aspx", True)
    '        End If

    '    Catch exception As Exception

    '    Finally
    '        iAdministradorUsuarios = Nothing
    '        iUsuario = Nothing
    '    End Try
    'End Function

    Public Function usuario() As String
        Dim iUsuario As seguridad.Usuario = Session(Session.SessionID & "usuario")

        Try
            If Not IsNothing(iUsuario) Then
                Return iUsuario.nombre
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
        Dim iUsuario As seguridad.Usuario = Session(Session.SessionID & "usuario")

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

    Public Function bordeTemaElegido() As String
        Dim iUsuario As seguridad.Usuario = Session(Session.SessionID & "usuario")
        Dim borde As String = "primary"

        Try

            'If Not IsNothing(iUsuario) Then
            '    If (iUsuario.temaElegido = "") Then
            '        iUsuario.temaElegido = "skin-blue"
            '    End If
            '    Select Case iUsuario.temaElegido
            '        Case "skin-blue", "skin-purple"
            '            borde = "primary"
            '        Case "skin-blue-light", "skin-purple-light"
            '            borde = "info"
            '        Case "skin-yellow", "skin-yellow-light"
            '            borde = "warning"
            '        Case "skin-green", "skin-green-light"
            '            borde = "success"
            '        Case "skin-red", "skin-red-light"
            '            borde = "danger"
            '        Case Else
            '            borde = "default"

            '    End Select

            '    Return borde
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
        Dim iUsuario As seguridad.Usuario = Session(Session.SessionID & "usuario")

        Try
            If Not IsNothing(iUsuario) Then
                If iUsuario.fotoPerfil <> Nothing Then
                    'Return ConfigurationManager.AppSettings("fotosPerfil") & iUsuario.id & ".jpg"
                    'Return "fotosperfiles/" & iUsuario.id & ".jpg"
                    Return iUsuario.fotoPerfil
                Else
                    Return "assets/img/arg.png"
                End If
            Else

            End If

            Return Nothing

        Catch exception As Exception
        Finally
            iUsuario = Nothing
        End Try
    End Function

    Public Function puedeAcceder(ByVal ePaginaCompleta As String) As String
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        Dim iUsuario As seguridad.Usuario

        Try

            iUsuario = Session(Session.SessionID & "usuario")

            Try
                ePaginaCompleta = ePaginaCompleta.Substring(ePaginaCompleta.LastIndexOf("/") + 1)
                If ePaginaCompleta.IndexOf("?") > 0 Then
                    ePaginaCompleta = Left(ePaginaCompleta, ePaginaCompleta.IndexOf("?"))
                End If
                removerSessiones()
            Catch exception As Exception
                ePaginaCompleta = ePaginaCompleta.Substring(ePaginaCompleta.LastIndexOf("\") + 1)
            End Try

            If False Then
                Return "none"
            End If

            Return "block"

        Catch Exception As Exception
            Return ""
        Finally
            iAdministradorUsuarios = Nothing
        End Try
    End Function

    Private Sub removerSessiones()
        Dim iObjetoEnSession As Object
        Dim iColeccionARemover As New Collection
        Dim i As Integer

        Try
            For Each iObjetoEnSession In Session.Contents
                If Mid(iObjetoEnSession, Len(Session.SessionID) + 1, 1) = "x" And
                    iObjetoEnSession <> "usuario" And
                    iObjetoEnSession <> "ultimaPagina" And
                    iObjetoEnSession <> "ultimaExcepcion" Then

                    iColeccionARemover.Add(iObjetoEnSession)

                End If
            Next

            For i = 1 To iColeccionARemover.Count
                Session.Remove(iColeccionARemover.Item(i))
            Next i

        Catch Exception As Exception
            Session(Session.SessionID & "ultimaExcepcion") = Exception
            Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
            Response.Redirect("PaginaError.aspx", True)
        End Try
    End Sub

    Public Shared Function controlAHtml(ByVal eControl As Object) As String
        Dim iStringBuilder As StringBuilder
        Dim iStringWriter As StringWriter
        Dim iHtmlTextWriter As HtmlTextWriter

        Try
            iStringBuilder = New StringBuilder()
            iStringWriter = New StringWriter(iStringBuilder)
            iHtmlTextWriter = New HtmlTextWriter(iStringWriter)

            eControl.RenderControl(iHtmlTextWriter)

            Return iStringBuilder.ToString()

        Catch ex As Exception
            Return Nothing
        Finally
            iStringBuilder = Nothing
            iStringWriter = Nothing
            iHtmlTextWriter = Nothing
        End Try

    End Function




#End Region

#Region "AlertCustom"
    Public Sub alertCustom(ByVal eMensaje As String)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "alertCustom", "document.addEventListener('DOMContentLoaded', function(event) { site.showSwal(); Swal.fire({ type: 'info', html: '" & eMensaje & "', buttonsStyling: false,confirmButtonClass: 'btn btn-info btn-round btn-block min-width-200'}) });", True)
    End Sub

    Public Sub alertExito(ByVal eMensaje As String)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "alertCustom", "document.addEventListener('DOMContentLoaded', function(event) { site.showSwal(); Swal.fire({ type: 'success', html: '" & eMensaje & "', buttonsStyling: false,confirmButtonClass: 'btn btn-info btn-round btn-block min-width-200'}) });", True)
    End Sub
    Public Sub alertyRedireccionar(ByVal eMensaje As String, ByVal eUrl As String)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "alertCustom", "document.addEventListener('DOMContentLoaded', function(event) { site.showSwal(); Swal.fire({ type: 'success', html: '" & eMensaje & "', buttonsStyling: false,confirmButtonClass: 'btn btn-info btn-round btn-block min-width-200'}).then(function() {window.location='" & eUrl & "';}); });", True)
    End Sub
#End Region

#Region "Botones"
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class