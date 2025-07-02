Imports di.financiera.datos

Public MustInherit Class Deudor

    Inherits Entidad


#Region "Variables"
    Private iId As Long
    Private iDocumento As Long
    Private iNombre As String
#End Region

#Region "Atributos"
    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
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

    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    MustOverride Function obtenerDeudor(Optional ByVal eCodigoFinancieraNoBuscar As String = Nothing) As Deudor
    MustOverride Function obtenerDeudores() As DataSet
    MustOverride Function obtenerDeudorTarjeta(ByVal eDocumento As Long) As Boolean
    MustOverride Function obtenerDeudoresFinanciera(ByVal eDocumento As Long) As String
    MustOverride Function isVeraz() As Boolean
    MustOverride Function isCamara() As Boolean
#End Region

End Class
