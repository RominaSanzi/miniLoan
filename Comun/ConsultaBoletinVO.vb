
Public Class ConsultaBoletinVO

#Region "Variables"
    Private iNombre As String
    Private iDocumento As Long
    Private iImporte As Double
    Private iFinancieras As Collection
#End Region

#Region "Atributos"
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
    Public Property documento() As Long
        Get
            Return iDocumento
        End Get
        Set(ByVal Value As Long)
            iDocumento = Value
        End Set
    End Property
    Public Property importe() As Double
        Get
            Return iImporte
        End Get
        Set(ByVal Value As Double)
            iImporte = Value
        End Set
    End Property
    Public Property financieras() As Collection
        Get
            Return iFinancieras
        End Get
        Set(ByVal Value As Collection)
            iFinancieras = Value
        End Set
    End Property
#End Region

End Class