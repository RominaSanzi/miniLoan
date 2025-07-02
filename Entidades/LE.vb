
Public Class LE : Inherits TipoDocumento

#Region "Metodos"

    Public Sub New()
        id = LE
        descripcion = "LE"
    End Sub

    Public Overrides Function isDNI() As Boolean
        Return False
    End Function

    Public Overrides Function isLC() As Boolean
        Return False
    End Function

    Public Overrides Function isLE() As Boolean
        Return True
    End Function

    Public Overrides Function isCI() As Boolean
        Return False
    End Function

    Public Overrides Function isPAS() As Boolean
        Return False
    End Function
    Public Overrides Function isCUIT() As Boolean
        Return False
    End Function
    Public Overrides Function ToString() As String
        Return "LE"
    End Function

#End Region

End Class
