
Public Class LogVO

#Region "Variables"
    Private iId As Long
    Private iFechaDesde As Date
    Private iFechaHasta As Date
    Private iUsuario As Usuario
    Private iAccion As Accion
    Private iNivel As Nivel
    Private iDetalle As String
    Private iColeccionUsuarios As Collection
#End Region

#Region "Atributos"
    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
        End Set
    End Property
    Public Property usuario() As Usuario
        Get
            Return iUsuario
        End Get
        Set(ByVal Value As Usuario)
            iUsuario = Value
        End Set
    End Property

    Public Property accion() As Accion
        Get
            Return iAccion
        End Get
        Set(ByVal Value As Accion)
            iAccion = Value
        End Set
    End Property

    Public Property fechaDesde() As Date
        Get
            Return iFechaDesde
        End Get
        Set(ByVal Value As Date)
            iFechaDesde = Value
        End Set
    End Property

    Public Property fechaHasta() As Date
        Get
            Return iFechaHasta
        End Get
        Set(ByVal Value As Date)
            iFechaHasta = Value
        End Set
    End Property

    Public Property nivel() As Nivel
        Get
            Return iNivel
        End Get
        Set(ByVal Value As Nivel)
            iNivel = Value
        End Set
    End Property
    Public Property detalle() As String
        Get
            Return iDetalle
        End Get
        Set(ByVal Value As String)
            iDetalle = Value
        End Set
    End Property
    Public Property coleccionUsuarios() As Collection
        Get
            Return iColeccionUsuarios
        End Get
        Set(ByVal Value As Collection)
            iColeccionUsuarios = Value
        End Set
    End Property
#End Region

End Class
