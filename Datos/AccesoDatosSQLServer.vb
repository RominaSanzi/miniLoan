Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class AccesoDatosSQLServer

#Region "Atributos"
    Dim iDatosConexionSQLServer As datosConexionSQLServer
    Dim iActiveTransaction As SqlTransaction
    Dim iConexion As SqlConnection
#End Region

#Region "Properties"
    Public Property ActiveTransaction() As SqlTransaction
        Get
            Return Me.iActiveTransaction
        End Get
        Set(ByVal Value As SqlTransaction)
            iActiveTransaction = Value
        End Set
    End Property
    Public Property conexion() As SqlConnection
        Get
            Return Me.iConexion
        End Get
        Set(ByVal Value As SqlConnection)
            iConexion = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Sub New(Optional ByVal eConexionExterna As Boolean = False)
        'creamos la conexion a la base
        Me.conexion = Me.obtenerConexion(eConexionExterna)
    End Sub

    Public Function obtenerConexion(ByVal eConexionExterna As Boolean) As SqlConnection
        Dim iConexion As New SqlConnection

        Try
            ' Abrimos la conexion: es importante que el stringConexion sea el mismo
            ' para todas las conexiones, de lo contrario no se esta utilizando el
            ' connection pooling que provee ADO.NET
            iConexion.ConnectionString = iDatosConexionSQLServer.getInstancia.stringConexionServicios

            iConexion.Open()

            'la devolvemos
            Return iConexion
        Catch ioe As InvalidOperationException
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        End Try
    End Function

    Public Function getDataSet(ByVal eSql As String, ByVal eTabla As String) As DataSet
        Dim iDataAdapter As SqlDataAdapter
        Dim iDataSet As DataSet
        Dim iCommand As SqlCommand

        Try

            loguear(eSql)

            If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
            iCommand = New SqlCommand(eSql, iConexion)
            If Not (IsNothing(Me.iActiveTransaction)) Then
                iCommand.Transaction = Me.ActiveTransaction
            End If
            iCommand.CommandText = eSql

            iDataAdapter = New SqlDataAdapter
            iDataAdapter.SelectCommand = iCommand

            iDataSet = New DataSet
            iDataAdapter.Fill(iDataSet, eTabla)

            Return iDataSet

        Catch ioe As InvalidOperationException
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            iCommand = Nothing
            iDataAdapter = Nothing
        End Try
    End Function

    Public Function getDataSet(ByVal eDataSet As DataSet, ByVal eSql As String, ByVal eTabla As String) As DataSet
        Dim iDataAdapter As SqlDataAdapter
        Dim iCommand As SqlCommand

        Try

            loguear(eSql)

            If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
            iCommand = New SqlCommand(eSql, iConexion)
            If Not (IsNothing(Me.iActiveTransaction)) Then
                iCommand.Transaction = Me.ActiveTransaction
            End If

            iCommand.CommandText = eSql
            iDataAdapter = New SqlDataAdapter
            iDataAdapter.SelectCommand = iCommand
            iDataAdapter.Fill(eDataSet, eTabla)

            Return eDataSet

        Catch ioe As InvalidOperationException
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            iDataAdapter = Nothing
            iCommand = Nothing
        End Try
    End Function

    Public Function getStoredProcedure(eDataSet As DataSet, ByVal eNombreStoredProcedure As String, eColeccion As Collection, eNombreTabla As String) As DataSet
        Dim iDataAdapter As SqlDataAdapter
        Dim iCommand As SqlCommand
        Dim i As Integer

        Try

            If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
            iCommand = New SqlCommand(eNombreStoredProcedure, iConexion)
            If Not (IsNothing(Me.iActiveTransaction)) Then
                iCommand.Transaction = Me.ActiveTransaction
            End If

            iCommand.CommandType = CommandType.StoredProcedure
            For i = 1 To eColeccion.Count
                iCommand.Parameters.Add(New SqlParameter(CType(eColeccion.Item(i), ParametrosSQLStoredProcedureVO).nombreParametro, CType(eColeccion.Item(i), ParametrosSQLStoredProcedureVO).valor))
            Next
            If IsNothing(eDataSet) Then eDataSet = New DataSet
            iDataAdapter = New SqlDataAdapter
            iDataAdapter.SelectCommand = iCommand
            iDataAdapter.Fill(eDataSet, eNombreTabla)

            Return eDataSet

        Catch ioe As InvalidOperationException
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            iDataAdapter = Nothing
            iCommand = Nothing
        End Try
    End Function

    Public Function getDataReader(ByVal eSql As String) As IDataReader
        Dim iCommand As SqlCommand

        Try

            loguear(eSql)

            If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
            iCommand = New SqlCommand(eSql, Me.iConexion)
            If Not (IsNothing(Me.iActiveTransaction)) Then
                iCommand.Transaction = Me.ActiveTransaction
            End If

            Return iCommand.ExecuteReader()
        Catch ioe As InvalidOperationException
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            'finalmente liberamos los objetos
            iCommand = Nothing
        End Try
    End Function

    Public Sub ejecutar(ByVal eSql As String)
        Dim iCommand As SqlCommand

        Try

            loguear(eSql)

            If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
            iCommand = New SqlCommand(eSql, Me.iConexion)
            If Not (IsNothing(Me.iActiveTransaction)) Then
                iCommand.Transaction = Me.ActiveTransaction
            End If

            iCommand.ExecuteNonQuery()

        Catch ioe As InvalidOperationException
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            'finalmente liberamos los objetos
            iCommand = Nothing
        End Try
    End Sub

    Public Function ejecutarInsert(ByVal eSql As String, eParametrosSQL As List(Of ParametroSQL)) As Long
        Dim iCommandSqlServer As SqlCommand
        Dim iSql As String
        Dim i As Integer

        Try

            If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()

            iCommandSqlServer = New SqlCommand

            iCommandSqlServer.CommandText = eSql
            iSql = eSql
            For i = 0 To eParametrosSQL.Count - 1
                iCommandSqlServer.Parameters.Add(New SqlParameter(eParametrosSQL(i).nombre, IIf(eParametrosSQL(i).valor.ToLower = "null", DBNull.Value, eParametrosSQL(i).valor.Replace("'", ""))))
                iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
            Next i

            iCommandSqlServer.CommandTimeout = 0
            loguear(iSql)

            iCommandSqlServer.Connection = Me.iConexion
            If Not (IsNothing(Me.ActiveTransaction)) Then
                iCommandSqlServer.Transaction = Me.ActiveTransaction
            End If

            If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

            Return CLng(iCommandSqlServer.ExecuteScalar())

        Catch ioe As InvalidOperationException
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            'finalmente liberamos los objetos
            iCommandSqlServer = Nothing
        End Try
    End Function

    Public Sub ejecutar(ByVal eSql As String, eParametrosSQL As List(Of ParametroSQL))
        Dim iCommandSqlServer As SqlCommand
        Dim iSql As String
        Dim i As Integer

        Try

            If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()

            iCommandSqlServer = New SqlCommand

            iCommandSqlServer.CommandText = eSql
            iSql = eSql
            For i = 0 To eParametrosSQL.Count - 1
                iCommandSqlServer.Parameters.Add(New SqlParameter(eParametrosSQL(i).nombre, IIf(eParametrosSQL(i).valor.ToLower = "null", DBNull.Value, eParametrosSQL(i).valor.Replace("'", ""))))
                iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
            Next i

            iCommandSqlServer.CommandTimeout = 0
            loguear(iSql)

            iCommandSqlServer.Connection = Me.iConexion
            If Not (IsNothing(Me.ActiveTransaction)) Then
                iCommandSqlServer.Transaction = Me.ActiveTransaction
            End If

            If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

            iCommandSqlServer.ExecuteNonQuery()

        Catch ioe As InvalidOperationException
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            'finalmente liberamos los objetos
            iCommandSqlServer = Nothing
        End Try
    End Sub

    Public Sub commit()
        If Not IsNothing(iActiveTransaction) AndAlso Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Open Then iActiveTransaction.Commit()
    End Sub

    Public Sub rollback()
        If Not IsNothing(iActiveTransaction) AndAlso Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Open Then iActiveTransaction.Rollback()
    End Sub

    Public Sub beginTransaction()
        Try
            If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Open Then iActiveTransaction = iConexion.BeginTransaction
        Catch exception As exception
            'LOGUEAMOS EL ERROR
            Throw exception
        End Try
    End Sub

    Public Sub cerrar()
        Try
            If Not IsNothing(conexion) AndAlso conexion.State <> ConnectionState.Closed Then
                conexion.Close()
                conexion.Dispose()
            End If
        Catch exception As ErrorConexionException
            Throw New ErrorConexionException(exception)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region "LOG"
    Public Sub loguear(ByVal eLog As String)
        If datosConexionSQLServer.getInstancia.loguear AndAlso datosConexionSQLServer.getInstancia.pathArchivoLog <> Nothing Then
            Dim iStreamWriter As System.IO.StreamWriter
            Dim iLog As String

            Try
                iLog = Now & vbTab
                If Not IsNothing(iConexion) Then
                    iLog &= "NS" & vbTab
                Else
                    iLog &= "S/C" & vbTab
                End If
                If Not IsNothing(iActiveTransaction) Then
                    iLog &= "T" & vbTab
                Else
                    iLog &= "S/T" & vbTab
                End If
                iLog &= eLog.Replace(vbNewLine, " ").Replace(vbTab, " ")

                iStreamWriter = New System.IO.StreamWriter(datosConexionSQLServer.getInstancia.pathArchivoLog & Format(Today, "ddMMyyyy") & ".xls", True)
                iStreamWriter.WriteLine(iLog)
            Catch ex As Exception
            Finally
                If Not IsNothing(iStreamWriter) Then iStreamWriter.Close()
                iStreamWriter = Nothing
            End Try
        End If
    End Sub

    Public Sub loguearError(ByVal eLog As String)
        If datosConexionSQLServer.getInstancia.pathArchivoLog <> Nothing Then
            Dim iStreamWriter As System.IO.StreamWriter
            Dim iLog As String

            Try
                iLog = Now & vbTab
                If Not IsNothing(iConexion) Then
                    iLog &= "NS" & vbTab
                Else
                    iLog &= "S/C" & vbTab
                End If
                If Not IsNothing(iActiveTransaction) Then
                    iLog &= "T" & vbTab
                Else
                    iLog &= "S/T" & vbTab
                End If
                iLog &= eLog.Replace(vbNewLine, " ").Replace(vbTab, " ")

                iStreamWriter = New System.IO.StreamWriter(datosConexionSQLServer.getInstancia.pathArchivoLog & "ERROR" & Format(Today, "ddMMyyyy") & ".xls", True)
                iStreamWriter.WriteLine(iLog)
                loguear("Error|" & eLog)
            Catch ex As Exception
            Finally
                If Not IsNothing(iStreamWriter) Then iStreamWriter.Close()
                iStreamWriter = Nothing
            End Try
        End If
    End Sub
#End Region

End Class
