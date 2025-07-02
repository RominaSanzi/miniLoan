Imports System.Configuration

Public Class datosConexionSQLServer

#Region "Variables"

    Private Shared iStringConexion As String
    Private Shared iStringConexionServicios As String
    Private Shared iLoguear As String
    Private Shared iPathArchivoLog As String

#End Region

#Region "Atributos"

    Public Property stringConexion() As String
        Get
            Return iStringConexion
        End Get
        Set(ByVal Value As String)
            iStringConexion = Value
        End Set
    End Property

    Public Property stringConexionServicios() As String
        Get
            Return iStringConexionServicios
        End Get
        Set(ByVal Value As String)
            iStringConexionServicios = Value
        End Set
    End Property

    Public Property loguear() As Boolean
        Get
            Return iLoguear
        End Get
        Set(ByVal Value As Boolean)
            iLoguear = Value
        End Set
    End Property

    Public Property pathArchivoLog() As String
        Get
            Return iPathArchivoLog
        End Get
        Set(ByVal Value As String)
            iPathArchivoLog = Value
        End Set
    End Property

#End Region

#Region "Métodos"

    Private Shared iInstancia As datosConexionSQLServer
    Private Shared iMutex As New System.Threading.Mutex

    Public Shared Function getInstancia() As datosConexionSQLServer
        'Aca se evidencia la implementacion del patron Singleton: si no existe 
        ' la unica instancia de este objeto la creamos, sino devolvemos la existente
        iMutex.WaitOne()
        If iInstancia Is Nothing Then
            iInstancia = New datosConexionSQLServer
        End If

        iMutex.ReleaseMutex()

        'recuperamos el stringConexion
        llenarClase()
        Return iInstancia
    End Function

    Public Sub New()
        llenarClase()
    End Sub

    Private Shared Sub llenarClase()
        iStringConexion = ConfigurationManager.AppSettings("stringConexionSQL")
        iStringConexionServicios = ConfigurationManager.AppSettings("stringConexionSQLServicios")
        iPathArchivoLog = ConfigurationManager.AppSettings("archivosGenerados") & "LogConexionesSQLSereverServicio"
        iLoguear = ConfigurationManager.AppSettings("loguearConexiones").ToLower = "true"
    End Sub

#End Region

End Class
