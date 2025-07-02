Public MustInherit Class CompraVenta

#Region " Constantes"
    Public Const COMPRA = 1
    Public Const VENTA = 2
#End Region

#Region " Variables "
    Private iId As Long
    Private iDescripcion As String
#End Region

#Region " Atributos "
    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
        End Set
    End Property

    Public Property descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property
#End Region

#Region " Metodos "

    MustOverride Function isCompra() As Boolean
    MustOverride Function isVenta() As Boolean

    Public Shared Function obtenerCompraVenta() As Collection
        Dim iColeccion As New Collection
        iColeccion.Add("COMPRA", COMPRA)
        iColeccion.Add("VENTA", VENTA)

        Return iColeccion
    End Function
#End Region

End Class
