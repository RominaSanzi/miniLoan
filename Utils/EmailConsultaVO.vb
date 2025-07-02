
Public Class EmailConsultaVO

#Region "Enumerado"
    Public Enum EnumTipoAccion
        TODOS = 0
        NOENVIADO = 1
        ENVIADO = 2
        CONERROR = 3
        CONERRORNOENVIADO = 4
    End Enum
#End Region

#Region "Variables"
    Private iFechaEnvioDesde As Date
    Private iFechaEnvioHasta As Date
    Private iDireccionDestino As String
    Private iAsunto As String
    Private iAccion As EnumTipoAccion
#End Region

#Region "Atributos"
    Public Property fechaEnvioDesde() As Date
        Get
            Return iFechaEnvioDesde
        End Get
        Set(ByVal Value As Date)
            iFechaEnvioDesde = Value
        End Set
    End Property
    Public Property fechaEnvioHasta() As Date
        Get
            Return iFechaEnvioHasta
        End Get
        Set(ByVal Value As Date)
            iFechaEnvioHasta = Value
        End Set
    End Property
    Public Property direccionDestino() As String
        Get
            Return iDireccionDestino
        End Get
        Set(ByVal Value As String)
            iDireccionDestino = Value
        End Set
    End Property
    Public Property asunto() As String
        Get
            Return iAsunto
        End Get
        Set(ByVal Value As String)
            iAsunto = Value
        End Set
    End Property
    Public Property accion() As EnumTipoAccion
        Get
            Return iAccion
        End Get
        Set(ByVal Value As EnumTipoAccion)
            iAccion = Value
        End Set
    End Property
#End Region

End Class
