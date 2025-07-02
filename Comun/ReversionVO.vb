Public Class ReversionVO

#Region "Variables"

    Private iFechaDesde As Date
    Private iFechaHasta As Date
    Private iNumeroCredito As Long
    Private iDocumento As Integer

#End Region

#Region "Atributos"

    Public Property fechaDesde() As Date
        Get
            Return iFechaDesde
        End Get
        Set(ByVal Value As Date)
            iFechaDesde = Value
        End Set
    End Property
    Public Property fechaHasta() As Date
        Get
            Return iFechaHasta
        End Get
        Set(ByVal Value As Date)
            iFechaHasta = Value
        End Set
    End Property
    Public Property numeroCredito() As Long
        Get
            Return iNumeroCredito
        End Get
        Set(ByVal Value As Long)
            iNumeroCredito = Value
        End Set
    End Property

    Public Property documento() As Integer
        Get
            Return iDocumento
        End Get
        Set(ByVal Value As Integer)
            iDocumento = Value
        End Set
    End Property

#End Region

End Class
