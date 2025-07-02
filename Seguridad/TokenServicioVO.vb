
Public Class TokenServicioVO

#Region "Variables"
    Private iFechaVencimiento As Date
    Private iPalabraClave As String
    Private iIdUsuario As Long
    Private iToken As String
    Private iLogin As String
    Private iPassword As String
#End Region

#Region "Atributos"
    Public Property fechaVencimiento As Date
        Get
            Return iFechaVencimiento
        End Get
        Set(value As Date)
            iFechaVencimiento = value
        End Set
    End Property

    Public Property palabraClave As String
        Get
            Return iPalabraClave
        End Get
        Set(value As String)
            iPalabraClave = value
        End Set
    End Property

    Public Property idUsuario As Long
        Get
            Return iIdUsuario
        End Get
        Set(value As Long)
            iIdUsuario = value
        End Set
    End Property

    Public Property token As String
        Get
            Return iToken
        End Get
        Set(value As String)
            iToken = value
        End Set
    End Property

    Public Property login As String
        Get
            Return iLogin
        End Get
        Set(value As String)
            iLogin = value
        End Set
    End Property

    Public Property password As String
        Get
            Return iPassword
        End Get
        Set(value As String)
            iPassword = value
        End Set
    End Property
#End Region

End Class
