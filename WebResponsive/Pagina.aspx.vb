Imports di.financiera.seguridad
Imports di.financiera.excepciones
Imports di.financiera.reglasnegocios

Partial Class Pagina
    Inherits PaginaLogueo

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No lo modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    <System.Web.Services.WebMethod()>
    Public Shared Sub borarrSessionControlesNiveles()
        HttpContext.Current.Session(HttpContext.Current.Session.SessionID & "controlesNivelesIdEmpresaGrupo") = ""
        HttpContext.Current.Session(HttpContext.Current.Session.SessionID & "controlesNivelesIdUnidadDeNegocios") = ""
    End Sub


    Public Shared Sub mantenerEstadoNiveles(ByVal eEmpresaGrupoValor As String, ByVal eUnidadDeNegociosValor As String)
        HttpContext.Current.Session(HttpContext.Current.Session.SessionID & "controlesNivelesIdEmpresaGrupo") = eEmpresaGrupoValor
        HttpContext.Current.Session(HttpContext.Current.Session.SessionID & "controlesNivelesIdUnidadDeNegocios") = eUnidadDeNegociosValor
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim iAdministradorUsuarios As New AdministradorUsuarios
            Dim iUsuario As Usuario
            Dim iPagina As String

            Try

                iUsuario = Session(Session.SessionID & "usuario")
                If IsNothing(iUsuario) OrElse IsNothing(Request) Then
                    Session(Session.SessionID & "ultimaExcepcion") = New UsuarioNoEncontradoException("La Sesión de usuario expiró, debera loguearse nuevamente al sistema")
                    Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                    Response.Redirect("Error.aspx", True)
                End If

                Try
                    iPagina = Request.Url.ToString
                    iPagina = iPagina.Substring(iPagina.LastIndexOf("/") + 1)
                    If iPagina.IndexOf("?") > 0 Then
                        iPagina = Left(iPagina, iPagina.IndexOf("?"))
                    End If
                    removerSessiones()
                Catch exception As Exception
                    iPagina = iPagina.Substring(iPagina.LastIndexOf("\") + 1)
                End Try

                'If Not iAdministradorUsuarios.puedeAcceder(iUsuario, iPagina) Then
                '    Session(Session.SessionID & "ultimaExcepcion") = New UsuarioNoEncontradoException("El usuario no esta habilitado para ver esta página")
                '    Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                '    Response.Redirect("Error.aspx", True)
                'End If

            Catch Exception As Exception
                If Not TypeOf (Exception) Is Threading.ThreadAbortException Then
                    Session(Session.SessionID & "ultimaExcepcion") = Exception
                    Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                    Response.Redirect("Error.aspx", True)
                End If
            Finally
                iAdministradorUsuarios = Nothing
            End Try
        End If
    End Sub

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
            Response.Redirect("Error.aspx", True)
        End Try
    End Sub

End Class
