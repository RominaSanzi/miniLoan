Imports di.financiera.reglasnegocios
Imports di.financiera.seguridad
Imports di.financiera.entidades
Imports Newtonsoft.Json

Partial Class DashboardIframe
    Inherits PaginaLogueo

#Region "Dashboard"
    Private Sub obtenerDatosDashBoard()
        Dim iAdministradorUsuarios As New AdministradorUsuarios

        Try

            graficos.InnerHtml = iAdministradorUsuarios.obtenerDashboardCompletoPorUsuario(CType(MyBase.Session(MyBase.Session.SessionID & "usuario"), seguridad.Usuario), Today)

        Catch exception As Exception
        Finally
            iAdministradorUsuarios = Nothing
        End Try
    End Sub

#End Region

#Region "Pagina"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            obtenerDatosDashBoard()
        End If
    End Sub
#End Region

End Class