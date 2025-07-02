Public Class datosConexionAccess

#Region "Variables"
    Private Shared iStringConexion As String
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

#End Region

#Region "Métodos"

    Private Shared iInstancia As datosConexionAccess
    Private Shared iMutex As New System.Threading.Mutex()

    Public Shared Function getInstancia() As datosConexionAccess
        iMutex.WaitOne()
        If iInstancia Is Nothing Then
            iInstancia = New datosConexionAccess()
        End If

        iMutex.ReleaseMutex()

        Return iInstancia
    End Function

    Public Sub New()
        llenarClase()
    End Sub

    Private Shared Sub llenarClase()

        iStringConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="

    End Sub

#End Region

End Class
