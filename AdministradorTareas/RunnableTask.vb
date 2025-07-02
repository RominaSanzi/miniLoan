
Public MustInherit Class RunnableTask

#Region "Variables"
    Private iParametros As Object
    Private iId As String
    Private iHoraInicio As String
#End Region

#Region "Atributos"
    Public Property parametros() As Object
        Get
            Return iParametros
        End Get
        Set(ByVal Value As Object)
            iParametros = Value
        End Set
    End Property

    Public Property id As String
        Get
            Return iId
        End Get
        Set(value As String)
            iId = value
        End Set
    End Property

    Public Property horaInicio As String
        Get
            Return iHoraInicio
        End Get
        Set(value As String)
            iHoraInicio = value
        End Set
    End Property
#End Region

#Region "Metodos"
    MustOverride Function execute() As Object
#End Region

End Class
