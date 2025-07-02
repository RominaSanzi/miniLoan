Imports System.Exception

Public Class ErrorConexionException
    Inherits Exception

    Private iException As Exception
    Private iMensaje As String = "Ha ocurrido un error al conectar a la base de datos"

    Sub New(ByVal eMensaje As String)
        iMensaje = eMensaje
    End Sub

    Public Property originalCause() As Exception
        Get
            Return iException
        End Get
        Set(ByVal Value As Exception)
            iException = Value
        End Set
    End Property

    Sub New()
    End Sub

    Sub New(ByVal eException As Exception, ByVal eMensaje As String)
        iMensaje = eMensaje
        iException = eException
    End Sub

    Sub New(ByVal eException As Exception)
        iException = eException
    End Sub

    Public Overrides Function ToString() As String
        Return iMensaje
    End Function

End Class
