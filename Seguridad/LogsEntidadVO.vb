
Public Class LogsEntidadVO

#Region "Variables"
    Private iUsuario As usuario
    Private iTiposEntidad As Collection
    Private iEntidad As Object
    Private iFechaDesde As Date
    Private iFechaHasta As Date
    Private iHoraDesde As TimeSpan
    Private iHoraHasta As TimeSpan
#End Region

#Region "Atributos"
    Public Property usuario() As usuario
        Get
            Return iUsuario
        End Get
        Set(ByVal Value As usuario)
            iUsuario = Value
        End Set
    End Property

    Public Property tiposEntidad() As Collection
        Get
            Return iTiposEntidad
        End Get
        Set(ByVal Value As Collection)
            iTiposEntidad = Value
        End Set
    End Property

    Public Property entidad() As Object
        Get
            Return iEntidad
        End Get
        Set(ByVal Value As Object)
            iEntidad = Value
        End Set
    End Property

    Public Property fechaDesde As Date
        Get
            Return iFechaDesde
        End Get
        Set(value As Date)
            iFechaDesde = value
        End Set
    End Property

    Public Property fechaHasta As Date
        Get
            Return iFechaHasta
        End Get
        Set(value As Date)
            iFechaHasta = value
        End Set
    End Property

    Public Property horaDesde As TimeSpan
        Get
            Return iHoraDesde
        End Get
        Set(value As TimeSpan)
            iHoraDesde = value
        End Set
    End Property

    Public Property horaHasta As TimeSpan
        Get
            Return iHoraHasta
        End Get
        Set(value As TimeSpan)
            iHoraHasta = value
        End Set
    End Property
#End Region

End Class
