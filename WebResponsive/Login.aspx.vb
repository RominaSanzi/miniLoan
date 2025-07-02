Imports di.financiera.seguridad
Imports di.financiera.excepciones
Imports di.financiera.reglasnegocios
Imports di.financiera.entidades
Imports di.financiera.datos
Imports di.financiera.utils
Imports System.Web.Security
Imports System.Reflection

Partial Class Login
    Inherits System.Web.UI.Page

#Region "AlertCustom"
    Private Sub alertCustom(ByVal eMensaje As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "alertCustom", "document.addEventListener('DOMContentLoaded', function(event) { site.showSwal(); Swal.fire({title: 'LOAN',html: '" & eMensaje & "',buttonsStyling: false,confirmButtonClass: 'btn btn-info'}) });", True)
    End Sub

    Private Sub modalError(ByVal eMensaje As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "modalError", "document.addEventListener('DOMContentLoaded', function(event) { site.showSwal(); Swal.fire({title: 'LOAN',html: '" & eMensaje & "',type: 'warning',buttonsStyling: false,confirmButtonClass: 'btn btn-info'}) });", True)
    End Sub
#End Region

#Region "Ayuda"
    Private Sub ShowPopUp(ByVal ePaginaPopUp As String, ByVal eWidth As Integer, ByVal eHeight As Integer, Optional ByVal eResizable As String = "no", Optional ByVal eScrollBars As String = "no", Optional ByVal eStatus As String = "no")
        Dim abrir As String = "<SCRIPT language='javascript'> " & vbNewLine &
                    "var window_width = " & eWidth & ";" &
                    "var window_height = " & eHeight & ";" &
                    "var newfeatures= 'resizable=yes,scrollbars=yes';" &
                    "var window_top = (screen.height-window_height)/2;" &
                    "var window_left = (screen.width-window_width)/2;" &
                    "window.open('" & ePaginaPopUp & "', 'Ayuda','width=' + window_width + ',height=' + window_height + ',top=' + window_top + ',left=' + window_left + ',features=' + newfeatures + '');" &
                    "</SCRIPT>"
        RegisterStartupScript("abrir", abrir)
    End Sub
    Public Function hipervinculo1() As String
        If Not txtNuevaContrasenia.Visible Then
            Return "<a href=" & """http://www.divinf.com.ar" & """><img src=" & """imagenes/di.jpg" & """  border=" & """0" & """ align=" & """top" & """ onmouseover=" & """this.src='imagenes/di_rollover.jpg';" & """ onmouseout=" & """this.src='imagenes/di.jpg';" & """></a>"
        Else
            Return Nothing
        End If
    End Function
    Public Function hipervinculo2() As String
        If txtNuevaContrasenia.Visible Then
            Return "<a href=" & """http://www.divinf.com.ar" & """><img src=" & """imagenes/di2.jpg" & """  border=" & """0" & """ align=" & """top" & """ onmouseover=" & """this.src='imagenes/di2_rollover.jpg';" & """ onmouseout=" & """this.src='imagenes/di2.jpg';" & """></a>"
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Metodos"
    Private Function obtenerIpUsuario() As String
        Return CStr(Request.ServerVariables("REMOTE_HOST")) & " | " & CStr(Request.ServerVariables("REMOTE_ADDR"))
    End Function
    Private Sub login()
        Dim iUsuario As Usuario
        Dim iAdministradorUsuarios As New AdministradorUsuarios

        Try

            If IsNothing(Session(Session.SessionID & "usuario")) Then
                iUsuario = New Usuario
                iUsuario.login = txtLogin.Text
                iUsuario.password = txtContrasenia.Text
                'iUsuario.nivel = New GrupoEmpresas
                'iUsuario.nivel.id = 1
                iUsuario = iAdministradorUsuarios.validarUsuario(iUsuario, Split(Request.UserHostAddress, "."), True)
                If iUsuario.pedirCambioPassword Then
                    txtNuevaContrasenia.Visible = True
                    txtRepetirContrasenia.Visible = True
                    txtLogin.Visible = False
                    txtContrasenia.Visible = False
                    btnIngresar.Text = "Confirmar"
                    SetFocus(txtNuevaContrasenia)
                    iUsuario.pedirCambioPassword = False
                    Session(Session.SessionID & "usuario") = iUsuario
                Else
                    Session("masterpage") = "~/HijoConMenu.master"
                    Session(Session.SessionID & "usuario") = iUsuario
                    FormsAuthentication.SignOut()
                    If Page.IsValid Then FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, False)
                End If
            ElseIf Len(txtNuevaContrasenia.Text) > 0 AndAlso Len(txtRepetirContrasenia.Text) > 0 AndAlso txtNuevaContrasenia.Text = txtRepetirContrasenia.Text Then
                iUsuario = Session(Session.SessionID & "usuario")
                iUsuario.password = txtNuevaContrasenia.Text
                iUsuario.pedirCambioPassword = False
                ' iAdministradorUsuarios.cambiarPasswordUsuario(iUsuario)
                Session(Session.SessionID & "usuario") = iUsuario
                FormsAuthentication.SignOut()
                If Page.IsValid Then FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, False)
            ElseIf Not (txtNuevaContrasenia.Text = txtRepetirContrasenia.Text) Then
                txtNuevaContrasenia.Text = ""
                txtRepetirContrasenia.Text = ""
                alertCustom("Las nuevas contraseñas ingresadas son distintas")
                SetFocus(txtNuevaContrasenia)
            Else
                Session.RemoveAll()
                FormsAuthentication.SignOut()
                SetFocus(txtLogin)
            End If

        Catch UsuarioNoEncontradoException As UsuarioNoEncontradoException
            alertCustom(FuncionComun.obtenerMotivoOriginal(UsuarioNoEncontradoException).Replace("'", "").Replace(vbNewLine, ""))
        Catch Exception As Exception
            modalError("Ocurrió un error")
            FuncionComun.loguearErrores(FuncionComun.obtenerMotivoOriginal(Exception).Replace("'", "").Replace(vbNewLine, ""))
            SetFocus(txtLogin)
        Finally
            iUsuario = Nothing
            iAdministradorUsuarios = Nothing
        End Try
    End Sub

    Private Sub autologin()
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        Dim iEncriptador As New Encriptador("*|Front*|")
        Dim iUsuario As seguridad.Usuario
        Dim iParametros() As String
        Dim iParametro As String

        Try

            iParametro = iEncriptador.desencriptar(Request.QueryString.Item("front").Replace(" ", "+"))

            iParametros = Split(iParametro, "|")
            If iParametros.Length > 1 Then Session(Session.SessionID & "FrontPaginaDireccionar") = iParametros(1)

            iUsuario = New seguridad.Usuario
            iUsuario.id = iParametros(0)
            iUsuario = iAdministradorUsuarios.validarUsuario(iUsuario, Split(Request.UserHostAddress, "."))
            Session(Session.SessionID & "usuario") = iUsuario

            FormsAuthentication.SignOut()
            FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, False)

        Catch Exception As Exception
            SetFocus(txtLogin)
        Finally
            iAdministradorUsuarios = Nothing
            iEncriptador = Nothing
            iUsuario = Nothing
        End Try
    End Sub

    Private Sub mostrarLogo()

        Dim iAdministradorNiveles As New AdministradorNiveles
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        Dim iImagenNombre, iCarpeta As String
        Dim iUsuario As New seguridad.Usuario

        Dim iServerPath As String() = ConfigurationManager.AppSettings("archivosImagenes").Split("\")

        Try
            iCarpeta = iServerPath(iServerPath.Length - 2)

            iImagenNombre = iAdministradorUsuarios.obtenerLogo(Nothing)

            If iImagenNombre <> Nothing AndAlso System.IO.File.Exists(ConfigurationManager.AppSettings("archivosImagenes") & iImagenNombre & ".png") Then
                logo.ImageUrl = "~/" & iCarpeta & "/" & iImagenNombre & ".png"
            Else
                logo.ImageUrl = "~/" & iCarpeta & "/logodefault.png"
            End If


        Catch exception As Exception 'ERROR .CLOSE
            logo.ImageUrl = "~/" & iCarpeta & "/logodefault.png"
        Finally
            iAdministradorNiveles = Nothing
            iAdministradorUsuarios = Nothing
        End Try

    End Sub
#End Region

#Region "Botones"
    Protected Sub btnIngresar_Click1(sender As Object, e As EventArgs) Handles btnIngresar.Click
        login()
    End Sub
    Protected Sub btnOlvidoContrasenia_Click(sender As Object, e As EventArgs) Handles btnOlvidoContrasenia.Click
        Response.Redirect("LoginRecuperoContrasenia.aspx", True)
    End Sub
#End Region

#Region "Pagina"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then



            mostrarLogo()
            Session.RemoveAll()
            FormsAuthentication.SignOut()
            If Not IsNothing(Request.QueryString.Item("front")) Then
                autologin()
            Else
                SetFocus(txtLogin)
            End If
        End If
    End Sub
#End Region

End Class