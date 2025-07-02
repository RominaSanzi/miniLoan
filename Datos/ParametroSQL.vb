
Public Class ParametroSQL

#Region "Variables"
    Private iNombre As String
    Private iValor As Object
    Private iNombreCampo As String
#End Region

#Region "Atributos"
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
    Public Property valor() As Object
        Get
            Return iValor
        End Get
        Set(ByVal Value As Object)
            iValor = Value
        End Set
    End Property
    Public Property nombreCampo() As String
        Get
            Return iNombreCampo
        End Get
        Set(ByVal Value As String)
            iNombreCampo = Value
        End Set
    End Property
#End Region

End Class
