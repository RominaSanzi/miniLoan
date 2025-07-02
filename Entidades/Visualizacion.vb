Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Collections.ObjectModel
Imports System.Collections.Generic

Public Class Visualizacion

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iLabel As String
    Private iPais As Pais
    Private iPagina As String
    Private iTexto As String
    Private iConexion As accesoDatos
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

    Public Property label() As String
        Get
            Return iLabel
        End Get
        Set(ByVal Value As String)
            iLabel = Value
        End Set
    End Property

    Public Property pais() As Pais
        Get
            Return iPais
        End Get
        Set(ByVal Value As Pais)
            iPais = Value
        End Set
    End Property

    Public Property pagina() As String
        Get
            Return iPagina
        End Get
        Set(ByVal Value As String)
            iPagina = Value
        End Set
    End Property

    Public Property texto() As String
        Get
            Return iTexto
        End Get
        Set(ByVal Value As String)
            iTexto = Value
        End Set
    End Property

#End Region

#Region "Metodos"

    Public Function obtenerVisualizacionesPorPagina(ByVal ePagina As String) As List(Of Visualizacion)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet As DataSet
        Dim iListVisualizacion As New List(Of Visualizacion)
        Dim iVisualizacion As Visualizacion

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("label")
            iGeneradorSql.agregarColumna("idPais")
            iGeneradorSql.agregarColumna("pagina")
            iGeneradorSql.agregarColumna("texto")
            iGeneradorSql.agregarCondicionWhere("pagina=" & FuncionComun.vacioSiEsNulo(ePagina))

            iGeneradorSql.agregarTabla("visualizacion")


            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "visualizacion")

            For Each iDataRow As DataRow In iDataSet.Tables("visualizacion").Rows
                iVisualizacion = New Visualizacion

                With iVisualizacion
                    .id = iDataRow.Item("id")
                    .label = iDataRow.Item("label")
                    .iPais = New Pais
                    .iPais.id = iDataRow.Item("idPais")
                    .pagina = iDataRow.Item("pagina")
                    .texto = iDataRow.Item("texto")
                End With

                iListVisualizacion.Add(iVisualizacion)
                iVisualizacion = Nothing
            Next

            Return iListVisualizacion

        Catch excepcion As Exception
            Throw New VisualizacionNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
            iListVisualizacion = Nothing
        End Try

    End Function

#End Region

End Class
