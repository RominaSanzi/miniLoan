
Public Class ErrorAplicacionVO

#Region "Variables"
    Private iMensajeUsuario As String
    Private iMensajeOriginal As String
    Private iRootException As Boolean
    Private iLog As Log
#End Region

#Region "Atributos"
    Public Property rootException() As Boolean
        Get
            Return iRootException
        End Get
        Set(ByVal Value As Boolean)
            iRootException = Value
        End Set
    End Property
    Public Property mensajeUsuario() As String
        Get
            Return imensajeUsuario
        End Get
        Set(ByVal Value As String)
            imensajeUsuario = Value
        End Set
    End Property
    Public Property mensajeOriginal() As String
        Get
            Return iMensajeOriginal
        End Get
        Set(ByVal Value As String)
            iMensajeOriginal = Value
        End Set
    End Property
    Public Property log() As Log
        Get
            Return iLog
        End Get
        Set(ByVal Value As Log)
            iLog = Value
        End Set
    End Property
#End Region

End Class
