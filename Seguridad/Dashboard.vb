Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Collections.Generic
Public Class Dashboard

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
    Private iTiposTablero As List(Of TipoTablero)

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
    Public Property descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property
    Public Property tiposTablero() As List(Of TipoTablero)
        Get
            Return iTiposTablero
        End Get
        Set(ByVal Value As List(Of TipoTablero))
            iTiposTablero = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerDashboard() As Dashboard
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("Dashboard")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If iDescripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion=" & FuncionComun.nuloSiEsNothing(iDescripcion))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDescripcion = FuncionComun.vacioSiEsNulo(iDataReader.Item("descripcion").ToString)
                iDataReader.Close()

                iTiposTablero = obtenerTiposTableroDashboard()

                Return Me
            Else
                Throw New DashboardNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New DashboardNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerDashboardSoloIds() As Dashboard
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("Dashboard")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDescripcion = FuncionComun.vacioSiEsNulo(iDataReader.Item("descripcion").ToString)

                iDataReader.Close()

                Return Me
            Else
                Throw New DashboardNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New DashboardNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerDashboards() As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("Dashboard")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DashboardNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerDashboardsGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("Dashboard")
            If (descripcion <> Nothing OrElse descripcion <> "") Then iGeneradorSql.agregarCondicionWhere("descripcion like '" & descripcion & "%'")
            iGeneradorSql.agregarOrden("descripcion asc")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Dashboard")

        Catch excepcion As Exception
            Throw New DashboardNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Private Sub validarCrear()

        ' Validar campos obligatorios
        If descripcion = Nothing Then
            Throw New DashboardNoCreadoException("La descripción no puede ser nula")
        End If

        If IsNothing(tiposTablero) OrElse tiposTablero.Count = 0 Then
            Throw New DashboardNoCreadoException("Debe seleccionar al menos un tipo de tablero")
        End If

        ' Validar unicidad de campos
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("Dashboard")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & FuncionComun.nuloSiEsNothing(iDescripcion))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New DashboardNoCreadoException("El Dashboard ya existe en la base de datos")
            End If

        Catch excepcion As Exception
            Throw New DashboardNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("Dashboard")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

            crearTiposTableroDashboard()

        Catch excepcion As Exception
            Throw New DashboardNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Private Sub obtenerUltimoId()
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarTabla("Dashboard")
            iGeneradorSql.agregarColumna("max(id) as id")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iId = CLng(iDataReader.Item("id").ToString)
            Else
                Throw New DashboardNoCreadoException()
            End If

        Catch excepcion As Exception
            Throw New DashboardNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarEliminar()
        If id = Nothing Then
            Throw New DashboardNoEliminadoException("El Dashboard a eliminar es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarCondicionWhere("idDashboard=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New DashboardNoEliminadoException("No se puede eliminar el Dashboard porque se relaciona con un usuario")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New DashboardNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarEliminar()

            eliminarTiposTableroDashboard()

            iGeneradorSql.agregarTabla("Dashboard")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DashboardNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Private Sub validarModificar()

        ' Validar campos obligatorios
        If id = Nothing Then
            Throw New DashboardNoModificadoException("El Dashboard es inexistente")
        End If

        If IsNothing(tiposTablero) OrElse tiposTablero.Count = 0 Then
            Throw New DashboardNoModificadoException("Debe seleccionar al menos un tipo de tablero")
        End If

        ' Validar existencia del registro
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("Dashboard")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New DashboardNoModificadoException("El Dashboard a modificar es inexistente")
            End If

        Catch excepcion As Exception
            Throw New DashboardNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarTabla("Dashboard")
            iGeneradorSql.agregarSet("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

            crearTiposTableroDashboard()

        Catch excepcion As Exception
            Throw New DashboardNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

#Region "Tipos tablero Dashboard"
    Public Function obtenerTiposTableroDashboard() As List(Of TipoTablero)
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet
        Dim iTipoTablero As TipoTablero
        Dim iListaTipoTablero As List(Of TipoTablero)
        Dim i As Integer


        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("td.idtipoTablero")
            iGeneradorSql.agregarColumna("t.descripcion")

            iGeneradorSql.agregarTabla("tipoTablero t")
            iGeneradorSql.agregarTabla("DashboardTipoTablero td")

            iGeneradorSql.agregarCondicionWhere("td.idtipoTablero=t.id")
            iGeneradorSql.agregarCondicionWhere("td.idDashboard=" & id)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "TiposTablero")

            iListaTipoTablero = New List(Of TipoTablero)

            For i = 0 To iDataSet.Tables("TiposTablero").Rows.Count - 1
                iTipoTablero = New TipoTablero
                With iTipoTablero
                    .id = iDataSet.Tables("TiposTablero").Rows(i).Item("idtipoTablero")
                    .descripcion = iDataSet.Tables("TiposTablero").Rows(i).Item("descripcion")
                End With
                iListaTipoTablero.Add(iTipoTablero)
            Next

            Return iListaTipoTablero

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
            iTipoTablero = Nothing
            iListaTipoTablero = Nothing
        End Try

    End Function
    Public Sub crearTiposTableroDashboard()
        Dim iGeneradorSql As New GeneradorSql
        Dim i As Integer

        Try
            eliminarTiposTableroDashboard()

            iConexion = obtenerConexion()

            For i = 0 To iTiposTablero.Count - 1
                iGeneradorSql.agregarColumna("idTipoTablero")
                iGeneradorSql.agregarColumna("idDashboard")
                iGeneradorSql.agregarValue(iTiposTablero.Item(i).id)
                iGeneradorSql.agregarValue(iId)

                iGeneradorSql.agregarTabla("DashboardTipoTablero")

                iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Next

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub
    Public Sub eliminarTiposTableroDashboard()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarCondicionWhere("idDashboard=" & iId)
            iGeneradorSql.agregarTabla("DashboardTipoTablero")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub
#End Region

#Region "Usuario-Dashboard"
    Public Function obtenerDashboardCompletoPorUsuario(eUsuario As Usuario) As String
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataTable As DataTable
        Dim iJson As String

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar("ifnull(jsonTableroDinamico,''), CASE WHEN length(jsonTableroDinamico)>0 THEN ',' END ,ifnull(jsonTableroEstatico,'')") & " as jsonTableros")

            iGeneradorSql.agregarTabla("UsuarioDashboard")

            iGeneradorSql.agregarCondicionWhere("idUsuario=" & eUsuario.id)
            iGeneradorSql.agregarOrden("fecha desc")
            iGeneradorSql.agregarLimit("1")

            iDataTable = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Dashboard").Tables("Dashboard")

            iJson = "["

            If iDataTable.Rows.Count > 0 Then
                iJson &= iDataTable.Rows(0).Item("jsonTableros").ToString
            End If

            iJson &= "]"

            Return iJson

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataTable = Nothing
        End Try

    End Function
#End Region

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class
