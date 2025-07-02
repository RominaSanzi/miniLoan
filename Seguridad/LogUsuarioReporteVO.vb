
Imports di.financiera.entidades

Public Class LogUsuarioReporteVO

#Region "Variables"
    Private iNombre As String
    Private iLogin As String
    Private iFechaDesde As Date
    Private iFechaHasta As Date
    Private iEstado As Estado

    Private iDataSet As DataSet
#End Region

#Region "Atributos"
    Public Property dataSet() As DataSet
        Get
            Return iDataSet
        End Get
        Set(ByVal Value As DataSet)
            iDataSet = Value
        End Set
    End Property
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
    Public Property login() As String
        Get
            Return iLogin
        End Get
        Set(ByVal Value As String)
            iLogin = Value
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

    Public Property estado() As Estado
        Get
            Return iEstado
        End Get
        Set(ByVal Value As Estado)
            iEstado = Value
        End Set
    End Property
#End Region

End Class
