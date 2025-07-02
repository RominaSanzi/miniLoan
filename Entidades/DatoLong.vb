
Public Class DatoLong : Inherits TipoDato

#Region "Metodos"

    Public Overrides Function isDatoString() As Boolean
        Return False
    End Function

    Public Overrides Function isDatoInteger() As Boolean
        Return False
    End Function

    Public Overrides Function isDatoLong() As Boolean
        Return True
    End Function

    Public Overrides Function isDatoDouble() As Boolean
        Return False
    End Function

    Public Overrides Function isDatoDate() As Boolean
        Return False
    End Function

    Public Overrides Function ToString() As String
        Return "DatoLong"
    End Function

#End Region

End Class
