Public Class FormatoImagenVO

#Region "Atributos"

    Private iHeight As Integer
    Private iWidth As Integer

#End Region

#Region "Propiedades"

    Public Property height() As Integer
        Get
            Return iHeight
        End Get
        Set(ByVal Value As Integer)
            iHeight = Value
        End Set
    End Property
    Public Property width() As Integer
        Get
            Return iWidth
        End Get
        Set(ByVal Value As Integer)
            iWidth = Value
        End Set
    End Property

#End Region

End Class
