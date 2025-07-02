Public Class LineaParrafoVO

#Region "Variables"
    Private iLinea As String
    Private iNegrita As Boolean
    Private iTamanioFuente As Single
#End Region

#Region "Atributos"
    Public Property linea() As String
        Get
            Return iLinea
        End Get
        Set(ByVal Value As String)
            iLinea = Value
        End Set
    End Property

    Public Property negrita() As Boolean
        Get
            Return iNegrita
        End Get
        Set(ByVal Value As Boolean)
            iNegrita = Value
        End Set
    End Property

    Public Property tamanioFuente() As Single
        Get
            Return iTamanioFuente
        End Get
        Set(ByVal Value As Single)
            iTamanioFuente = Value
        End Set
    End Property
#End Region
    
End Class
