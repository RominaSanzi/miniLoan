Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class RegistrosUsuariosGrillaVO

    Inherits Entidad

#Region "Variables"
    Private iFechaEntradaDesde As Date
    Private iFechaEntradaHasta As Date
    Private iFechaSalidaDesde As Date
    Private iFechaSalidaHasta As Date
    Private iUsuarios As Collection
    Private iPerfiles As Collection
#End Region

#Region "Atributos"
    Public Property usuarios() As Collection
        Get
            Return iUsuarios
        End Get
        Set(ByVal Value As Collection)
            iUsuarios = Value
        End Set
    End Property

    Public Property perfiles() As Collection
        Get
            Return iPerfiles
        End Get
        Set(ByVal Value As Collection)
            iPerfiles = Value
        End Set
    End Property

    Public Property fechaEntradaDesde() As Date
        Get
            Return iFechaEntradaDesde
        End Get
        Set(ByVal Value As Date)
            iFechaEntradaDesde = Value
        End Set
    End Property

    Public Property fechaEntradaHasta() As Date
        Get
            Return iFechaEntradaHasta
        End Get
        Set(ByVal Value As Date)
            iFechaEntradaHasta = Value
        End Set
    End Property

    Public Property fechaSalidaDesde() As Date
        Get
            Return iFechaSalidaDesde
        End Get
        Set(ByVal Value As Date)
            iFechaSalidaDesde = Value
        End Set
    End Property

    Public Property fechaSalidaHasta() As Date
        Get
            Return iFechaSalidaHasta
        End Get
        Set(ByVal Value As Date)
            iFechaSalidaHasta = Value
        End Set
    End Property

#End Region

End Class
