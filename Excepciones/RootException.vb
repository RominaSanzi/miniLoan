Imports System.Exception

Public Class RootException

    Inherits Exception

#Region "Variables"
    Protected iMensaje As String
    Private iException As exception
#End Region

#Region "Atributos"
    Public Property mensaje() As String
        Get
            Return iMensaje
        End Get
        Set(ByVal Value As String)
            iMensaje = Value
        End Set
    End Property

    Public Property originalCause() As exception
        Get
            Return iException
        End Get
        Set(ByVal Value As exception)
            iException = Value
        End Set
    End Property

#End Region

#Region "Metodos"

    Public Overrides Function ToString() As String
        Return iMensaje
    End Function

#End Region

#Region "Constructores"

    Sub New(ByVal eMensaje As String, ByVal eException As exception)
        iMensaje = eMensaje
        iException = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As exception)
        iException = eException
    End Sub

    Sub New()
    End Sub

#End Region

End Class
