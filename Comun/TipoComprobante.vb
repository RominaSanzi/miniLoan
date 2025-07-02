Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades

Public MustInherit Class TipoComprobante

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iCodigo As Integer
    Private iDescripcion As String
#End Region

#Region "Constantes"
    Public Const FACTURAA As Integer = 1
    Public Const FACTURAB As Integer = 2
    Public Const FACTURAAPUNTUAL As Integer = 3
    Public Const FACTURABPUNTUAL As Integer = 4
    Public Const NOTADECREDITOB As Integer = 5
    Public Const NOTADEDEBITOB As Integer = 6
    Public Const NOTADECREDITOA As Integer = 7
    Public Const NOTADEDEBITOA As Integer = 8
    Public Const FACTURABPUNTUALMOSTRADOR As Integer = 9
    Public Const NOTADECREDITOBMOSTRADOR As Integer = 10
    Public Const NOTADEDEBITOBMOSTRADOR As Integer = 11
    Public Const FACTURAC As Integer = 12
    Public Const NOTADECREDITOC As Integer = 13
    Public Const NOTADEDEBITOC As Integer = 14
    Public Const FACTURARGMIPYMES As Integer = 15
    Public Const NOTADECREDITORGMIPYMES As Integer = 16
    Public Const NOTADEDEBITORGMIPYMES As Integer = 17
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

    Public Property codigo() As Integer
        Get
            Return iCodigo
        End Get
        Set(ByVal Value As Integer)
            iCodigo = Value
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

#Region "Metodos"

    MustOverride Function isFacturaA() As Boolean
    MustOverride Function isFacturaB() As Boolean
    MustOverride Function isFacturaC() As Boolean
    MustOverride Function isFacturaPuntualA() As Boolean
    MustOverride Function isFacturaPuntualBMostrador() As Boolean
    MustOverride Function isFacturaPuntualB() As Boolean
    MustOverride Function isNotaDeCreditoB() As Boolean
    MustOverride Function isNotaDeDebitoB() As Boolean
    MustOverride Function isNotaDeCreditoA() As Boolean
    MustOverride Function isNotaDeDebitoA() As Boolean
    MustOverride Function isNotaDeCreditoBMostrador() As Boolean
    MustOverride Function isNotaDeDebitoBMostrador() As Boolean
    MustOverride Function isNotaDeCreditoC() As Boolean
    MustOverride Function isNotaDeDebitoC() As Boolean
    MustOverride Function isFacturaRGMiPymes() As Boolean
    MustOverride Function isNotaDeCreditoRGMiPymes() As Boolean
    MustOverride Function isNotaDeDebitoRGMiPymes() As Boolean

    Public Shared Function obtenerTiposComprobante() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(FACTURAA, "FACTURA A")
        iSortedList.Add(FACTURAB, "FACTURA B")
        iSortedList.Add(FACTURAAPUNTUAL, "FACTURA A PUNTUAL")
        iSortedList.Add(FACTURABPUNTUAL, "FACTURA B PUNTUAL")
        iSortedList.Add(NOTADECREDITOB, "NOTA DE CREDITO B")
        iSortedList.Add(NOTADEDEBITOB, "NOTA DE DEBITO B")
        iSortedList.Add(NOTADECREDITOA, "NOTA DE CREDITO A")
        iSortedList.Add(NOTADEDEBITOA, "NOTA DE DEBITO A")
        iSortedList.Add(FACTURABPUNTUALMOSTRADOR, "FACTURA B PUNTUAL MOSTRADOR")
        iSortedList.Add(NOTADECREDITOBMOSTRADOR, "NOTA DE CREDITO B MOSTRADOR")
        iSortedList.Add(NOTADEDEBITOBMOSTRADOR, "NOTA DE DEBITO B MOSTRADOR")
        iSortedList.Add(FACTURARGMIPYMES, "FACTURA RG MiPyMEs")
        iSortedList.Add(NOTADECREDITORGMIPYMES, "NOTA DE CREDITO RG MiPyMEs")
        iSortedList.Add(NOTADEDEBITORGMIPYMES, "NOTA DE DEBITO RG MiPyMEs")
        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteEspeciales() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(FACTURAB, "FACTURA")
        iSortedList.Add(NOTADECREDITOB, "NOTA DE CREDITO")
        iSortedList.Add(NOTADEDEBITOB, "NOTA DE DEBITO")
        iSortedList.Add(FACTURARGMIPYMES, "FACTURA RG MiPyMEs")
        iSortedList.Add(NOTADECREDITORGMIPYMES, "NOTA DE CREDITO RG MiPyMEs")
        iSortedList.Add(NOTADEDEBITORGMIPYMES, "NOTA DE DEBITO RG MiPyMEs")
        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteNcNd() As SortedList
        Dim iSortedList As New SortedList

        iSortedList.Add(NOTADECREDITOB, "NOTA DE CREDITO B")
        iSortedList.Add(NOTADEDEBITOB, "NOTA DE DEBITO B")
        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteNcNdMostrador() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(NOTADECREDITOBMOSTRADOR, "NOTA DE CREDITO B")
        iSortedList.Add(NOTADEDEBITOBMOSTRADOR, "NOTA DE DEBITO B")
        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteNcNdA() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(NOTADECREDITOA, "NOTA DE CREDITO A")
        iSortedList.Add(NOTADEDEBITOA, "NOTA DE DEBITO A")
        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteSinNcNd() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(FACTURAA, "FACTURA A")
        iSortedList.Add(FACTURAB, "FACTURA B")
        iSortedList.Add(FACTURAAPUNTUAL, "FACTURA A PUNTUAL")
        iSortedList.Add(FACTURABPUNTUAL, "FACTURA B PUNTUAL")
        iSortedList.Add(FACTURABPUNTUALMOSTRADOR, "FACTURA B PUNTUAL MOSTRADOR")

        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteSinNcNdA() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(FACTURAA, "FACTURA A")
        iSortedList.Add(FACTURAAPUNTUAL, "FACTURA A PUNTUAL")

        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteSinNcNdB() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(FACTURAB, "FACTURA B")
        iSortedList.Add(FACTURABPUNTUAL, "FACTURA B PUNTUAL")
        iSortedList.Add(FACTURABPUNTUALMOSTRADOR, "FACTURA B PUNTUAL MOSTRADOR")
        Return iSortedList
    End Function


    Public Shared Function obtenerTiposComprobanteProveedorInscripto() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(FACTURAA, "FACTURA A")
        iSortedList.Add(FACTURAB, "FACTURA B")
        iSortedList.Add(NOTADECREDITOA, "NOTA DE CREDITO A")
        iSortedList.Add(NOTADECREDITOB, "NOTA DE CREDITO B")
        iSortedList.Add(NOTADEDEBITOA, "NOTA DE DEBITO A")
        iSortedList.Add(NOTADEDEBITOB, "NOTA DE DEBITO B")
        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteProveedorNoInscripto() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(FACTURAC, "FACTURA C")
        iSortedList.Add(NOTADECREDITOC, "NOTA DE CREDITO C")
        iSortedList.Add(NOTADEDEBITOC, "NOTA DE DEBITO C")
        Return iSortedList
    End Function

    Public Shared Function obtenerTiposComprobanteProveedor() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(FACTURAA, "FACTURA A")
        iSortedList.Add(FACTURAB, "FACTURA B")
        iSortedList.Add(NOTADECREDITOA, "NOTA DE CREDITO A")
        iSortedList.Add(NOTADECREDITOB, "NOTA DE CREDITO B")
        iSortedList.Add(NOTADEDEBITOA, "NOTA DE DEBITO A")
        iSortedList.Add(NOTADEDEBITOB, "NOTA DE DEBITO B")
        iSortedList.Add(FACTURAC, "FACTURA C")
        iSortedList.Add(NOTADECREDITOC, "NOTA DE CREDITO C")
        iSortedList.Add(NOTADEDEBITOC, "NOTA DE DEBITO C")
        Return iSortedList
    End Function

#End Region

End Class