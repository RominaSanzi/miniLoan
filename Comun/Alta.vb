Public Class Alta : Inherits Estado

#Region "Metodos"

    Public Overrides Function isBaja() As Boolean
        Return False
    End Function

    Public Overrides Function isAlta() As Boolean
        Return True
    End Function

    Public Overrides Function ToString() As String
        Return "Alta"
    End Function

    Public Sub New()
        id = Estado.ALTA
        descripcion = "ALTA"
    End Sub

#End Region

End Class