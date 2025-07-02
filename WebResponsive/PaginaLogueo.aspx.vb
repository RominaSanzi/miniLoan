Imports di.financiera.entidades
Imports di.financiera.seguridad

Partial Class PaginaLogueo
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
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
            Dim iPagina As String = Request.Url.ToString
            iPagina = iPagina.Substring(iPagina.LastIndexOf("/") + 1)
            If IsNothing(Session(Session.SessionID & "usuario")) Then
                FuncionComun.loguearPaginas(iPagina, "NOTHING")
            Else
                FuncionComun.loguearPaginas(iPagina, Session(Session.SessionID & "usuario").login.ToString.ToUpper)
            End If
        End If
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Try
            VisualizacionesSingleton.visualizacionControlesPorPaginaYPais(CType(MyBase.Session(MyBase.Session.SessionID & "usuario"), seguridad.Usuario).paisVisualizacion, Right(Request.Url.LocalPath, Len(Request.Url.LocalPath) - InStrRev(Request.Url.LocalPath, "/")), MyBase.Page)
        Catch ex As Exception
        End Try

        MyBase.Render(writer)
    End Sub

End Class
