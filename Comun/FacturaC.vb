Public Class FacturaC : Inherits TipoComprobante

#Region "Metodos"

    Public Sub New()
        id = FACTURAC
        descripcion = "FACTURA C"
    End Sub

    Public Overrides Function isFacturaA() As Boolean
        Return False
    End Function
    Public Overrides Function isNotaDeCreditoC() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeDebitoC() As Boolean
        Return False
    End Function

    Public Overrides Function isFacturaC() As Boolean
        Return True
    End Function


    Public Overrides Function isFacturaB() As Boolean
        Return False
    End Function

    Public Overrides Function isFacturaPuntualA() As Boolean
        Return False
    End Function

    Public Overrides Function isFacturaPuntualB() As Boolean
        Return False
    End Function

    Public Overrides Function isFacturaPuntualBMostrador() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeCreditoB() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeDebitoB() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeCreditoA() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeDebitoA() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeDebitoBMostrador() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeCreditoBMostrador() As Boolean
        Return False
    End Function

    Public Overrides Function isFacturaRGMiPymes() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeCreditoRGMiPymes() As Boolean
        Return False
    End Function

    Public Overrides Function isNotaDeDebitoRGMiPymes() As Boolean
        Return False
    End Function

    Public Overrides Function ToString() As String
        Return "FACTURA C"
    End Function
#End Region

End Class

