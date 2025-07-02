Public Class ParametrosSQLStoredProcedureVO

#Region "Variables"
    Private iNombreParametro As String
    Private iValor As Object
#End Region

#Region "Atributos"
    Public Property nombreParametro As String
        Get
            Return iNombreParametro
        End Get
        Set(value As String)
            iNombreParametro = value
        End Set
    End Property
    Public Property valor As Object
        Get
            Return iValor
        End Get
        Set(value As Object)
            iValor = value
        End Set
    End Property
#End Region

End Class
