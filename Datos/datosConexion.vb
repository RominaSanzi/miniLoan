Imports System.Configuration

Public Class datosConexion

#Region "Variables"

    Private Shared iStringConexion As String
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

    Private Shared iInstancia As datosConexion
    Private Shared iMutex As New System.Threading.Mutex()

    Public Shared Function getInstancia() As datosConexion
        'Aca se evidencia la implementacion del patron Singleton: si no existe 
        ' la unica instancia de este objeto la creamos, sino devolvemos la existente
        iMutex.WaitOne()
        If iInstancia Is Nothing Then
            iInstancia = New datosConexion()
        End If

        iMutex.ReleaseMutex()

        'recuperamos el stringConexion
        ' llenarClase()
        Return iInstancia
    End Function

    Public Sub New()
        llenarClase()
    End Sub

    Private Shared Sub llenarClase()

        iStringConexion = ConfigurationManager.AppSettings("stringConexion")
        If Not iStringConexion.Contains("Password=") Then
            iStringConexion = "Username=loan;Password=*loan9501422*;" & iStringConexion
        End If
        If Not iStringConexion.Contains(";Convert Zero Datetime=True;respect binary flags=false;Allow User Variables=True") Then
            iStringConexion = iStringConexion & ";Convert Zero Datetime=True;respect binary flags=false;Allow User Variables=True"
        End If
        iPathArchivoLog = ConfigurationManager.AppSettings("archivosGenerados") & "LogConexiones"
        iLoguear = ConfigurationManager.AppSettings("loguearConexiones").ToLower = "true"
    End Sub

#End Region

End Class

