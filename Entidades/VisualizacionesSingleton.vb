Imports di.financiera.datos
Imports di.financiera.utils
Imports di.financiera.excepciones
Imports System.Collections.Generic
Imports System.Web.UI
Imports System.Configuration

Public Class VisualizacionesSingleton
    Inherits Entidad

#Region "Variables"

    Private Shared iVisualizaciones As DataSet

#End Region

#Region "Atributos"
    Public Property visualizaciones() As DataSet
        Get
            Return iVisualizaciones
        End Get
        Set(ByVal Value As DataSet)
            iVisualizaciones = Value
        End Set
    End Property

#End Region

    Private Shared iInstancia As VisualizacionesSingleton
    Private Shared iMutex As New System.Threading.Mutex()

    Public Shared Function getInstancia() As VisualizacionesSingleton
        Try
            iMutex.WaitOne()
            If iInstancia Is Nothing Then
                iInstancia = New VisualizacionesSingleton()
            End If

            Return iInstancia
        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        Finally
            iMutex.ReleaseMutex()
        End Try
    End Function

    Public Sub New()
        Try
            obtenerVisualizaciones()

        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Sub

    Public Shared Sub obtenerVisualizaciones()
        Dim iGeneradorSql As New GeneradorSql()
        Dim iConexion As accesoDatos

        Try

            iConexion = New accesoDatos

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("label")
            iGeneradorSql.agregarColumna("idPais")
            iGeneradorSql.agregarColumna("pagina")
            iGeneradorSql.agregarColumna("texto")

            iGeneradorSql.agregarTabla("visualizacion")

            iVisualizaciones = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "visualizaciones")

        Catch excepcion As Exception
            FuncionComun.loguearPaginas("ERROR VISUALIZACION:" & FuncionComun.obtenerMotivoOriginal(excepcion), "VISUALIZACION")
        Finally
            If Not IsNothing(iConexion) Then iConexion.cerrar()
            iConexion = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Shared Function obtenerVisualizacionesPorPaisYPagina(ByVal ePais As Pais, ByVal ePagina As String) As DataSet
        Dim iDataSet As DataSet
        Dim iResult() As DataRow
        Dim iFiltros As String

        Try

            iDataSet = VisualizacionesSingleton.getInstancia.visualizaciones

            iFiltros = "idPais=" & ePais.id & " and (pagina='" & ePagina & "' or pagina='')"

            iResult = iDataSet.Tables("Visualizaciones").Select(iFiltros)

            Dim iDataSetNuevo As New DataSet
            iDataSetNuevo.Tables.Add(iDataSet.Tables("Visualizaciones").Clone)
            FuncionComun.copiarDataRow(iDataSetNuevo, "Visualizaciones", iResult)

            Return iDataSetNuevo

        Catch excepcion As Exception
            FuncionComun.loguearPaginas("ERROR VISUALIZACION:" & FuncionComun.obtenerMotivoOriginal(excepcion), "VISUALIZACION")
        Finally
            iResult = Nothing
            iDataSet = Nothing
        End Try
    End Function

    Public Shared Sub visualizacionControlesPorPaginaYPais(ByVal ePais As Pais, eUrl As String, ePagina As System.Web.UI.Page)
        Dim iControl As System.Web.UI.Control
        Dim iVisualizaciones As DataSet

        Try
            'FuncionComun.loguearPaginas("VISUALIZACION PAIS:" & ePais.id & "- URL:" & eUrl, "VISUALIZACION")

            iVisualizaciones = VisualizacionesSingleton.obtenerVisualizacionesPorPaisYPagina(ePais, eUrl)
            If Not IsNothing(iVisualizaciones) AndAlso Not IsNothing(iVisualizaciones.Tables("Visualizaciones")) Then
            For Each iDataRow As DataRow In iVisualizaciones.Tables("Visualizaciones").Rows
                Try
                    iControl = ePagina.Form.FindControl(iDataRow.Item("label").ToString)
                    If Not IsNothing(iControl) Then
                        CType(iControl, System.Web.UI.WebControls.Label).Text = iDataRow.Item("texto").ToString
                    End If
                Catch exception As Exception
                End Try
            Next
            End If


            If Not IsNothing(ePagina.Form) AndAlso Not IsNothing(ePagina.Form.Controls) Then

                For Each iControlLabel As Control In ePagina.Form.Controls
                    cambiarTextoControles(iControlLabel)
                    If Not IsNothing(iControlLabel.Controls) Then
                        For Each iControlLabelHijo As Control In iControlLabel.Controls
                            cambiarTextoControles(iControlLabelHijo)
                            If Not IsNothing(iControlLabelHijo.Controls) Then
                                For Each iControlLabelHijo2 As Control In iControlLabelHijo.Controls
                                    cambiarTextoControles(iControlLabelHijo)
                                    If Not IsNothing(iControlLabelHijo2.Controls) Then
                                        For Each iControlLabelHijo3 As Control In iControlLabelHijo2.Controls
                                            cambiarTextoControles(iControlLabelHijo3)
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If

        Catch exception As Exception
            FuncionComun.loguearPaginas("ERROR VISUALIZACION:" & FuncionComun.obtenerMotivoOriginal(exception), "VISUALIZACION")
        Finally
            iControl = Nothing
            'iVisualizaciones = Nothing
        End Try
    End Sub

    Private Shared Sub cambiarTextoControles(eControlLabel As Object)
        Try
            If TypeOf (eControlLabel) Is System.Web.UI.WebControls.Label Then
                If CType(eControlLabel, System.Web.UI.WebControls.Label).Text.Contains("$") Then
                    Select Case ConfigurationManager.AppSettings("Region")
                        Case "ARGENTINA"

                        Case "URUGUAY"

                        Case "PARAGUAY"
                            CType(eControlLabel, System.Web.UI.WebControls.Label).Text = CType(eControlLabel, System.Web.UI.WebControls.Label).Text.Replace("$", "Gs")
                        Case "COLOMBIA"

                    End Select
                Else
                    Select Case ConfigurationManager.AppSettings("Region")
                        Case "ARGENTINA"

                        Case "URUGUAY"

                        Case "PARAGUAY"

                        Case "COLOMBIA"
                            If CType(eControlLabel, System.Web.UI.WebControls.Label).Text.ToUpper = "CALLE" Then
                                CType(eControlLabel, System.Web.UI.WebControls.Label).Text = CType(eControlLabel, System.Web.UI.WebControls.Label).Text.Replace("Calle", "Dirección")
                            End If
                            If CType(eControlLabel, System.Web.UI.WebControls.Label).Text.ToUpper = "CALLE A NORMALIZAR" Then
                                CType(eControlLabel, System.Web.UI.WebControls.Label).Text = CType(eControlLabel, System.Web.UI.WebControls.Label).Text.Replace("Calle a normalizar", "Dirección a normalizar")
                            End If
                            If CType(eControlLabel, System.Web.UI.WebControls.Label).Text.ToUpper = "SELECCIONAR CALLE" Then
                                CType(eControlLabel, System.Web.UI.WebControls.Label).Text = CType(eControlLabel, System.Web.UI.WebControls.Label).Text.Replace("Seleccionar calle", "Seleccionar dirección")
                            End If
                    End Select
                End If
            End If

        Catch exception As Exception
        End Try
    End Sub


    Public Shared Function visualizacionMenuPorPais(ByVal ePais As Pais, eUrl As String, eCodigoMenu As String) As String
        Dim iControl As System.Web.UI.Control
        Dim iVisualizaciones As DataSet

        Try
            'FuncionComun.loguearPaginas("VISUALIZACION PAIS:" & ePais.id & "- URL:" & eUrl, "VISUALIZACION")

            iVisualizaciones = VisualizacionesSingleton.obtenerVisualizacionesPorPaisYPagina(ePais, eUrl)
            For Each iDataRow As DataRow In iVisualizaciones.Tables("Visualizaciones").Rows
                Try
                    eCodigoMenu = eCodigoMenu.Replace("'" & iDataRow.Item("label").ToString & "'", "'" & iDataRow.Item("texto").ToString & "'")
                Catch exception As Exception
                End Try
            Next

            Return eCodigoMenu
 
        Catch exception As Exception
            FuncionComun.loguearPaginas("ERROR VISUALIZACION:" & FuncionComun.obtenerMotivoOriginal(exception), "VISUALIZACION")
        Finally
            iControl = Nothing
            iVisualizaciones = Nothing
        End Try
    End Function

End Class
