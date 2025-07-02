Imports System.Collections.Generic
Imports di.financiera.entidades
Imports di.financiera.seguridad

Public Class ParametroVO

#Region "Variables"

    Private iNivel As Nivel
    Private iTipoParametro As TipoParametro
    Private iParametro As List(Of Parametro)
    Private iParametroEliminar As List(Of Parametro)
    Private iParametroCrear As List(Of Parametro)
    Private iDataSet As DataSet

#End Region

#Region "Atributos"

    Public Property nivel() As Nivel
        Get
            Return iNivel
        End Get
        Set(ByVal Value As Nivel)
            iNivel = Value
        End Set
    End Property
    Public Property parametro() As List(Of Parametro)
        Get
            Return iParametro
        End Get
        Set(ByVal Value As List(Of Parametro))
            iParametro = Value
        End Set
    End Property
    Public Property parametroEliminar() As List(Of Parametro)
        Get
            Return iParametroEliminar
        End Get
        Set(ByVal Value As List(Of Parametro))
            iParametroEliminar = Value
        End Set
    End Property
    Public Property parametroCrear() As List(Of Parametro)
        Get
            Return iParametroCrear
        End Get
        Set(ByVal Value As List(Of Parametro))
            iParametroCrear = Value
        End Set
    End Property

    Public Property dataSet() As DataSet
        Get
            Return iDataSet
        End Get
        Set(ByVal Value As DataSet)
            iDataSet = Value
        End Set
    End Property

    Public Property tipoParametro As TipoParametro
        Get
            Return iTipoParametro
        End Get
        Set(value As TipoParametro)
            iTipoParametro = value
        End Set
    End Property

#End Region

End Class
