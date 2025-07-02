
Public Class Baja : Inherits Estado

#Region "Metodos"

    Public Overrides Function isBaja() As Boolean
        Return True
    End Function

    Public Overrides Function isAlta() As Boolean
        Return False
    End Function

    Public Overrides Function ToString() As String
        Return "Baja"
    End Function

    Public Sub New()
        id = Estado.BAJA
        descripcion = "BAJA"
    End Sub

#End Region

End Class