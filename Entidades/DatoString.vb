
Public Class DatoString : Inherits TipoDato

#Region "Metodos"

    Public Overrides Function isDatoString() As Boolean
        Return True
    End Function

    Public Overrides Function isDatoInteger() As Boolean
        Return False
    End Function

    Public Overrides Function isDatoLong() As Boolean
        Return False
    End Function

    Public Overrides Function isDatoDouble() As Boolean
        Return False
    End Function

    Public Overrides Function isDatoDate() As Boolean
        Return False
    End Function

    Public Overrides Function ToString() As String
        Return "DatoString"
    End Function

#End Region

End Class
