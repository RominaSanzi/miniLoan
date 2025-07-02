Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Configuration

Public Class accesoDatos

#Region "Atributos"
    Dim iDatosConexion As datosConexion
    Dim iDatosConexionSQLServer As datosConexionSQLServer
    Dim iActiveTransaction As MySqlTransaction
    Dim iActiveTransactionSQLServer As SqlClient.SqlTransaction
    Dim iConexion As MySqlConnection
    Dim iConexionSQLServer As SqlClient.SqlConnection
    Dim iBaseDeDatos As String
    Dim iLoadLocal As String
#End Region

#Region "Properties"
    Public Property activeTransaction() As MySqlTransaction
        Get
            Return Me.iActiveTransaction
        End Get
        Set(ByVal Value As MySqlTransaction)
            iActiveTransaction = Value
        End Set
    End Property
    Public Property conexion() As MySqlConnection
        Get
            Return Me.iConexion
        End Get
        Set(ByVal Value As MySqlConnection)
            iConexion = Value
        End Set
    End Property
    Public Property activeTransactionSQLServer As SqlTransaction
        Get
            Return iActiveTransactionSQLServer
        End Get
        Set(value As SqlTransaction)
            iActiveTransactionSQLServer = value
        End Set
    End Property
    Public Property conexionSQLServer As SqlConnection
        Get
            Return iConexionSQLServer
        End Get
        Set(value As SqlConnection)
            iConexionSQLServer = value
        End Set
    End Property
    Public Property baseDeDatos As String
        Get
            Return iBaseDeDatos
        End Get
        Set(value As String)
            iBaseDeDatos = value
        End Set
    End Property
#End Region

#Region "Metodos"

#Region "Conexión"
    Public Sub New(Optional eStringConexion As String = "")
        iBaseDeDatos = ConfigurationManager.AppSettings("baseDeDatos")
        iLoadLocal = ConfigurationManager.AppSettings("LoadLocal")
        'creamos la conexion a la base
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            Me.conexionSQLServer = Me.obtenerConexionSQLServer
        Else
            Me.conexion = Me.obtenerConexion(eStringConexion)
        End If

        loguear("ABRE CONEXION")
    End Sub

    Public Function obtenerConexion(Optional eStringConexion As String = "") As MySqlConnection
        Dim iConexion As New MySqlConnection
        Try
            ' Abrimos la conexion: es importante que el stringConexion sea el mismo
            ' para todas las conexiones, de lo contrario no se esta utilizando el
            ' connection pooling que provee ADO.NET
            If Len(eStringConexion) > 0 Then
                iConexion.ConnectionString = eStringConexion
            Else
                iConexion.ConnectionString = iDatosConexion.getInstancia.stringConexion
            End If
            Try
                If Not iConexion.Ping() Then iConexion.Open()
            Catch ex As Exception
                System.Threading.Thread.Sleep(1000)
                If Not iConexion.Ping() Then iConexion.Open()
            End Try
            'la devolvemos
            Return iConexion
        Catch ioe As InvalidOperationException
            loguearError("Open|" & ioe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            loguearError("Open|" & oe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            loguearError("Open|" & e.Message)
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        End Try
    End Function


    Public Function obtenerConexionSQLServer() As SqlConnection
        Dim iConexion As New SqlConnection

        Try
            ' Abrimos la conexion: es importante que el stringConexion sea el mismo
            ' para todas las conexiones, de lo contrario no se esta utilizando el
            ' connection pooling que provee ADO.NET
            iConexion.ConnectionString = iDatosConexionSQLServer.getInstancia.stringConexion

            iConexion.Open()

            'la devolvemos
            Return iConexion
        Catch ioe As InvalidOperationException
            loguearError("Open|" & ioe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            loguearError("Open|" & oe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            loguearError("Open|" & e.Message)
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        End Try
    End Function
    Public Sub commit()
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            If Not IsNothing(iActiveTransactionSQLServer) AndAlso Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Open Then iActiveTransactionSQLServer.Commit()
        Else
            If Not IsNothing(iActiveTransaction) AndAlso Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Open Then iActiveTransaction.Commit()
        End If
        loguear("CT")
    End Sub

    Public Sub rollback()
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            If Not IsNothing(iActiveTransactionSQLServer) AndAlso Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Open Then iActiveTransactionSQLServer.Rollback()
        Else
            If Not IsNothing(iActiveTransaction) AndAlso Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Open Then iActiveTransaction.Rollback()
        End If
        loguear("RT")
    End Sub

    Public Sub beginTransaction()
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Open Then iActiveTransactionSQLServer = iConexionSQLServer.BeginTransaction
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Open Then iActiveTransaction = iConexion.BeginTransaction()
            End If
            loguear("AT")
        Catch exception As Exception
            'LOGUEAMOS EL ERROR
            Throw exception
        End Try
    End Sub

    Public Sub cerrar()
        Dim iIDConexion As String
        Try
            If Not IsNothing(conexionSQLServer) AndAlso conexionSQLServer.State <> ConnectionState.Closed Then
                conexionSQLServer.Close()
                conexionSQLServer.Dispose()
                loguear("CIERRA CONEXION")
            End If
            If Not IsNothing(conexion) AndAlso conexion.State <> ConnectionState.Closed Then
                iIDConexion = conexion.ServerThread & vbTab
                conexion.Close() 'ERROR .close
                conexion.Dispose()
                loguear(iIDConexion & "CIERRA CONEXION")
            End If
        Catch exception As ErrorConexionException
            Throw New ErrorConexionException(exception)
        End Try
    End Sub
#End Region

#Region "Metodos sin parámetros"
    Public Function getDataSetSinParametros(ByVal eSql As String, ByVal eTabla As String) As DataSet
        Dim iDataAdapter As MySqlDataAdapter
        Dim iDataAdapterSqlServer As SqlDataAdapter
        Dim iDataSet As DataSet
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()
                iCommandSqlServer = New SqlCommand(eSql, iConexionSQLServer)
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If
                iCommandSqlServer.CommandText = eSql

                iDataAdapterSqlServer = New SqlDataAdapter
                iDataAdapterSqlServer.SelectCommand = iCommandSqlServer

                iDataSet = New DataSet
                iDataAdapterSqlServer.Fill(iDataSet, eTabla)
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
                iCommand = New MySqlCommand(eSql, iConexion)
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If
                iCommand.CommandText = eSql

                iDataAdapter = New MySqlDataAdapter
                iDataAdapter.SelectCommand = iCommand

                iDataSet = New DataSet
                iDataAdapter.Fill(iDataSet, eTabla)
            End If

            loguear(eSql)

            Return iDataSet

        Catch ioe As InvalidOperationException
            loguearError(eSql & "|" & ioe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            loguearError(eSql & "|" & oe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            loguearError(eSql & "|" & e.Message)
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            iCommand = Nothing
            iDataAdapter = Nothing
        End Try
    End Function

    Public Function getDataSetSinParametros(ByVal eDataSet As DataSet, ByVal eSql As String, ByVal eTabla As String) As DataSet
        Dim iDataAdapter As MySqlDataAdapter
        Dim iCommand As MySqlCommand
        Dim iDataAdapterSqlServer As SqlDataAdapter
        Dim iCommandSqlServer As SqlCommand
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()
                iCommandSqlServer = New SqlCommand(eSql, iConexionSQLServer)
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                iCommandSqlServer.CommandText = eSql
                iDataAdapterSqlServer = New SqlDataAdapter
                iDataAdapterSqlServer.SelectCommand = iCommandSqlServer
                iDataAdapterSqlServer.Fill(eDataSet, eTabla)
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
                iCommand = New MySqlCommand(eSql, iConexion)
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                iCommand.CommandText = eSql
                iDataAdapter = New MySqlDataAdapter
                iDataAdapter.SelectCommand = iCommand
                iDataAdapter.Fill(eDataSet, eTabla)
            End If


            loguear(eSql)

            Return eDataSet

        Catch ioe As InvalidOperationException
            loguearError(eSql & "|" & ioe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            loguearError(eSql & "|" & oe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            loguearError(eSql & "|" & e.Message)
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            iDataAdapter = Nothing
            iCommand = Nothing
        End Try
    End Function

    Public Function getDataReaderSinParametros(ByVal eSql As String) As IDataReader
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()

                iCommandSqlServer = New SqlCommand(eSql, Me.iConexionSQLServer)
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                loguear(eSql)

                Return (iCommandSqlServer.ExecuteReader())
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()

                iCommand = New MySqlCommand(eSql, Me.iConexion)
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                loguear(eSql)

                Return (iCommand.ExecuteReader())
            End If


            'Dim iComand2 As New MySqlCommand("set net_write_timeout=99999; set net_read_timeout=99999;", Me.iConexion)
            'iComand2.ExecuteNonQuery()
        Catch ioe As InvalidOperationException
            loguearError(eSql & "|" & ioe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            loguearError(eSql & "|" & oe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            loguearError(eSql & "|" & e.Message)
            'Logueamos el error
            Throw New ErrorConexionException(e, e.Message)
        Finally
            'finalmente liberamos los objetos
            iCommand = Nothing
        End Try
    End Function

    Public Sub ejecutarSinParametros(ByVal eSql As String)
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()
                iCommandSqlServer = New SqlCommand(eSql, Me.iConexionSQLServer)
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                loguear(eSql)

                iCommandSqlServer.ExecuteNonQuery()
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
                iCommand = New MySqlCommand(eSql, Me.iConexion)
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                loguear(eSql)

                iCommand.ExecuteNonQuery()
            End If
        Catch ioe As InvalidOperationException
            loguearError(eSql & "|" & ioe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            loguearError(eSql & "|" & oe.Message)
            'Logueamos el error
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            loguearError(eSql & "|" & e.Message)
            Throw New ErrorConexionException(e, e.Message)
        Finally
            'finalmente liberamos los objetos
            iCommandSqlServer = Nothing
            iCommand = Nothing
        End Try
    End Sub

    Public Function ejecutarInsertSinParametros(ByVal eSql As String) As Long
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()
                eSql &= ";SELECT  SCOPE_IDENTITY();"
                iCommandSqlServer = New SqlCommand(eSql, Me.iConexionSQLServer)
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                loguear(eSql)

                Return CLng(iCommandSqlServer.ExecuteScalar())
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
                eSql &= ";SELECT LAST_INSERT_ID() as UltimoId;"
                iCommand = New MySqlCommand(eSql, Me.iConexion)
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                loguear(eSql)

                Return CLng(iCommand.ExecuteScalar())
            End If



        Catch ioe As InvalidOperationException
            'Logueamos el error
            loguearError(eSql & "|" & ioe.Message)
            Throw New ErrorConexionException(ioe, "La conexion ya esta abierta")
        Catch oe As OleDb.OleDbException
            'Logueamos el error
            loguearError(eSql & "|" & oe.Message)
            Throw New ErrorConexionException(oe, "Se ha producido un error de nivel de conexión al abrir la conexión.")
        Catch e As Exception
            'Logueamos el error
            loguearError(eSql & "|" & e.Message)
            Throw New ErrorConexionException(e, e.Message)
        Finally
            'finalmente liberamos los objetos
            iCommand = Nothing
        End Try
    End Function
#End Region

#Region "Metodos con parámetros"
    Public Function getDataSet(ByVal eSql As String, eParametrosSQL As List(Of ParametroSQL), ByVal eTabla As String) As DataSet
        Dim iDataAdapter As MySqlDataAdapter
        Dim iDataAdapterSqlServer As SqlDataAdapter
        Dim iDataSet As DataSet
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Dim iSqlParametro As SqlParameter
        Dim iSql As String
        Dim i As Integer
        Dim iObjeto As Object
        Dim iSqlDbType As SqlDbType
        Dim iFechaInicio As Date = Now
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()
                iCommandSqlServer = New SqlCommand()
                iCommandSqlServer.CommandText = eSql
                iSql = eSql

                For i = 0 To eParametrosSQL.Count - 1
                    iSqlDbType = obtenerTipoDatoSQL(eParametrosSQL(i).valor.Replace("'", ""), eParametrosSQL(i).nombreCampo)
                    If iSqlDbType = SqlDbType.Date Then
                        iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, iSqlDbType)
                        iSqlParametro.Value = eParametrosSQL(i).valor.Replace("'", "")
                    Else
                        iObjeto = eParametrosSQL(i).valor.Replace("'", "")
                        iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, iObjeto)
                    End If
                    iCommandSqlServer.Parameters.Add(iSqlParametro)
                    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre & ")", eParametrosSQL(i).valor & ")")
                    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre & " ", eParametrosSQL(i).valor & " ")
                Next i


                'For i = 0 To eParametrosSQL.Count - 1

                '    iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, obtenerTipoDatoSQL(eParametrosSQL(i).valor.Replace("'", "")))
                '    If iSqlParametro.SqlDbType = SqlDbType.VarChar Then
                '        iSqlParametro.Value = eParametrosSQL(i).valor.Replace("'", "")
                '    Else
                '        iSqlParametro.Value = eParametrosSQL(i).valor.Replace("'", "")
                '    End If

                '    iCommandSqlServer.Parameters.Add(iSqlParametro)
                '    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre & ")", eParametrosSQL(i).valor & ")")
                '    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre & " ", eParametrosSQL(i).valor & " ")
                'Next i

                iCommandSqlServer.CommandTimeout = 0
                loguear(iSql)

                iCommandSqlServer.Connection = Me.iConexionSQLServer
                'iCommandSqlServer.Prepare()
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                iDataAdapterSqlServer = New SqlDataAdapter
                iDataAdapterSqlServer.SelectCommand = iCommandSqlServer

                iDataSet = New DataSet()
                iDataAdapterSqlServer.Fill(iDataSet, eTabla)

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
                iCommand = New MySqlCommand()
                iCommand.CommandText = eSql

                iSql = eSql
                For i = 0 To eParametrosSQL.Count - 1
                    iCommand.Parameters.Add(New MySqlParameter(eParametrosSQL(i).nombre, eParametrosSQL(i).valor.Replace("'", "")))
                    iSql = iSql.Replace("?" & eParametrosSQL(i).nombre & ")", eParametrosSQL(i).valor & ")")
                    iSql = iSql.Replace("?" & eParametrosSQL(i).nombre & " ", eParametrosSQL(i).valor & " ")
                Next i

                iCommand.CommandTimeout = 0
                loguear(iSql)

                iCommand.Connection = Me.iConexion
                '      iCommand.Prepare()
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                iDataAdapter = New MySqlDataAdapter()
                iDataAdapter.SelectCommand = iCommand

                iDataSet = New DataSet()
                iDataAdapter.Fill(iDataSet, eTabla)

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()



            End If

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
            loguear(iSql, iFechaInicio, Now)
            iCommand = Nothing
            iDataAdapter = Nothing
        End Try
    End Function

    Public Function getDataSet(ByVal eDataSet As DataSet, ByVal eSql As String, eParametrosSQL As List(Of ParametroSQL), ByVal eTabla As String) As DataSet
        Dim iDataAdapter As MySqlDataAdapter
        Dim iDataAdapterSqlServer As SqlDataAdapter
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Dim iSqlParametro As SqlParameter
        Dim iSql As String
        Dim i As Integer
        Dim iObjeto As Object
        Dim iSqlDbType As SqlDbType
        Dim iFechaInicio As Date = Now
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()

                iCommandSqlServer = New SqlCommand

                iCommandSqlServer.CommandText = eSql
                iSql = eSql
                For i = 0 To eParametrosSQL.Count - 1
                    iSqlDbType = obtenerTipoDatoSQL(eParametrosSQL(i).valor.Replace("'", ""), eParametrosSQL(i).nombre)
                    If iSqlDbType = SqlDbType.Date Then
                        iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, iSqlDbType)
                        iSqlParametro.Value = eParametrosSQL(i).valor.Replace("'", "")
                    Else
                        iObjeto = eParametrosSQL(i).valor.Replace("'", "")
                        iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, iObjeto)
                    End If
                    iCommandSqlServer.Parameters.Add(iSqlParametro)
                    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                Next i


                'For i = 0 To eParametrosSQL.Count - 1
                '    iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, obtenerTipoDatoSQL(eParametrosSQL(i).valor.Replace("'", "")))
                '    iSqlParametro.Value = eParametrosSQL(i).valor.Replace("'", "")
                '    iCommandSqlServer.Parameters.Add(iSqlParametro)
                '    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                'Next i

                iCommandSqlServer.CommandTimeout = 0
                loguear(iSql)

                iCommandSqlServer.Connection = Me.iConexionSQLServer
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                iDataAdapterSqlServer = New SqlDataAdapter
                iDataAdapterSqlServer.SelectCommand = iCommandSqlServer
                iDataAdapterSqlServer.Fill(eDataSet, eTabla)

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()

                iCommand = New MySqlCommand()

                iCommand.CommandText = eSql
                iSql = eSql
                For i = 0 To eParametrosSQL.Count - 1
                    iCommand.Parameters.Add(New MySqlParameter(eParametrosSQL(i).nombre, eParametrosSQL(i).valor.Replace("'", "")))
                    iSql = iSql.Replace("?" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                Next i

                iCommand.CommandTimeout = 0
                loguear(iSql)

                iCommand.Connection = Me.iConexion
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                iDataAdapter = New MySqlDataAdapter()
                iDataAdapter.SelectCommand = iCommand
                iDataAdapter.Fill(eDataSet, eTabla)

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

            End If

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
            loguear(iSql, iFechaInicio, Now)
            iDataAdapter = Nothing
            iCommand = Nothing
        End Try
    End Function

    Public Function getDataReader(ByVal eSql As String, eParametrosSQL As List(Of ParametroSQL)) As IDataReader
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Dim iSqlParametro As SqlParameter
        Dim iSql As String
        Dim i As Integer
        Dim iObjeto As Object
        Dim iSqlDbType As SqlDbType
        Dim iFechaInicio As Date = Now
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()
                iCommandSqlServer = New SqlCommand
                iCommandSqlServer.CommandText = eSql
                iSql = eSql

                For i = 0 To eParametrosSQL.Count - 1
                    iSqlDbType = obtenerTipoDatoSQL(eParametrosSQL(i).valor.Replace("'", ""), eParametrosSQL(i).nombre)
                    If iSqlDbType = SqlDbType.Date Then
                        iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, iSqlDbType)
                        iSqlParametro.Value = eParametrosSQL(i).valor.Replace("'", "")
                    Else
                        iObjeto = eParametrosSQL(i).valor.Replace("'", "")
                        iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, iObjeto)
                    End If
                    iCommandSqlServer.Parameters.Add(iSqlParametro)
                    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                Next i

                'For i = 0 To eParametrosSQL.Count - 1
                '    iSqlParametro = New SqlParameter(eParametrosSQL(i).nombre, obtenerTipoDatoSQL(eParametrosSQL(i).valor.Replace("'", "")))
                '    iSqlParametro.Value = eParametrosSQL(i).valor.Replace("'", "")
                '    iCommandSqlServer.Parameters.Add(iSqlParametro)
                '    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                'Next i

                iCommandSqlServer.CommandTimeout = 0
                loguear(iSql)

                iCommandSqlServer.Connection = Me.iConexionSQLServer
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

                Return iCommandSqlServer.ExecuteReader()
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()

                iCommand = New MySqlCommand()

                iCommand.CommandText = eSql
                iSql = eSql
                For i = 0 To eParametrosSQL.Count - 1
                    iCommand.Parameters.Add(New MySqlParameter(eParametrosSQL(i).nombre, eParametrosSQL(i).valor.Replace("'", "")))
                    iSql = iSql.Replace("?" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                Next i

                iCommand.CommandTimeout = 0
                Console.WriteLine(iSql)  ' DEBUG
                loguear(iSql)

                iCommand.Connection = Me.iConexion
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                'If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()


                Return iCommand.ExecuteReader()
            End If
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
            loguear(iSql, iFechaInicio, Now)
            'finalmente liberamos los objetos
            iCommand = Nothing
        End Try
    End Function

    Public Function ejecutarInsert(ByVal eSql As String, eParametrosSQL As List(Of ParametroSQL)) As Long
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Dim iSql As String
        Dim i As Integer
        Dim iFechaInicio As Date = Now
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()
                eSql &= ";SELECT  SCOPE_IDENTITY() ;"

                iCommandSqlServer = New SqlCommand

                iCommandSqlServer.CommandText = eSql
                iSql = eSql
                For i = 0 To eParametrosSQL.Count - 1
                    iCommandSqlServer.Parameters.Add(New SqlParameter(eParametrosSQL(i).nombre, IIf(eParametrosSQL(i).valor.ToLower = "null", DBNull.Value, eParametrosSQL(i).valor.Replace("'", ""))))
                    iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                Next i

                iCommandSqlServer.CommandTimeout = 0
                loguear(iSql)

                iCommandSqlServer.Connection = Me.iConexionSQLServer
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

                Return CLng(iCommandSqlServer.ExecuteScalar())
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
                eSql &= ";SELECT LAST_INSERT_ID() as UltimoId;"

                iCommand = New MySqlCommand()

                iCommand.CommandText = eSql
                iSql = eSql
                For i = 0 To eParametrosSQL.Count - 1
                    iCommand.Parameters.Add(New MySqlParameter(eParametrosSQL(i).nombre, IIf(eParametrosSQL(i).valor.ToLower = "null", DBNull.Value, eParametrosSQL(i).valor.Replace("'", ""))))
                    iSql = iSql.Replace("?" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                Next i

                iCommand.CommandTimeout = 0
                loguear(iSql)

                iCommand.Connection = Me.iConexion
                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()
                Dim idsd As String
                idsd = iCommand.ExecuteScalar()
                Return idsd
            End If


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
            loguear(iSql, iFechaInicio, Now)
            'finalmente liberamos los objetos
            iCommand = Nothing
        End Try
    End Function

    Public Sub ejecutar(ByVal eSql As String, Optional eParametrosSQL As List(Of ParametroSQL) = Nothing)
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Dim iSql As String
        Dim i As Integer
        Dim iFechaInicio As Date = Now
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()

                iCommandSqlServer = New SqlCommand

                iCommandSqlServer.CommandText = eSql
                iSql = eSql
                If Not IsNothing(eParametrosSQL) Then
                    For i = 0 To eParametrosSQL.Count - 1
                        iCommandSqlServer.Parameters.Add(New SqlParameter(eParametrosSQL(i).nombre, IIf(eParametrosSQL(i).valor.ToLower = "null", DBNull.Value, eParametrosSQL(i).valor.Replace("'", ""))))
                        iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                    Next i
                End If

                iCommandSqlServer.CommandTimeout = 0
                loguear(iSql)

                iCommandSqlServer.Connection = Me.iConexionSQLServer
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

                iCommandSqlServer.ExecuteNonQuery()

            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()

                iCommand = New MySqlCommand()

                iCommand.CommandText = eSql
                iSql = eSql
                If Not IsNothing(eParametrosSQL) Then
                    For i = 0 To eParametrosSQL.Count - 1
                        iCommand.Parameters.Add(New MySqlParameter(eParametrosSQL(i).nombre, IIf(eParametrosSQL(i).valor.ToLower = "null", DBNull.Value, eParametrosSQL(i).valor.Replace("'", ""))))
                        iSql = iSql.Replace("?" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                    Next i
                End If

                iCommand.CommandTimeout = 0
                loguear(iSql)

                iCommand.Connection = Me.iConexion

                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()
                Dim iiid As String
                iiid = iCommand.ExecuteNonQuery()
            End If


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
            loguear(iSql, iFechaInicio, Now)
            'finalmente liberamos los objetos
            iCommand = Nothing
        End Try
    End Sub

    Public Sub ejecutarLoad(ByVal eSql As String, Optional eParametrosSQL As List(Of ParametroSQL) = Nothing)
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Dim iSql As String
        Dim i As Integer
        Dim iFechaInicio As Date = Now
        Try
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()

                iCommandSqlServer = New SqlCommand

                iCommandSqlServer.CommandText = eSql
                iSql = eSql
                If Not IsNothing(eParametrosSQL) Then
                    For i = 0 To eParametrosSQL.Count - 1
                        iCommandSqlServer.Parameters.Add(New SqlParameter(eParametrosSQL(i).nombre, IIf(eParametrosSQL(i).valor.ToLower = "null", DBNull.Value, eParametrosSQL(i).valor.Replace("'", ""))))
                        iSql = iSql.Replace("@" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                    Next i
                End If

                iCommandSqlServer.CommandTimeout = 0
                loguear(iSql)

                iCommandSqlServer.Connection = Me.iConexionSQLServer
                If Not (IsNothing(Me.iActiveTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

                iCommandSqlServer.ExecuteNonQuery()

            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()

                iCommand = New MySqlCommand()
                'REEMPLAZO SENTENCIA LOAD
                eSql = eSql.Replace("LOAD DATA INFILE", "LOAD DATA " & iLoadLocal & " INFILE")

                iCommand.CommandText = eSql
                iSql = eSql
                If Not IsNothing(eParametrosSQL) Then
                    For i = 0 To eParametrosSQL.Count - 1
                        iCommand.Parameters.Add(New MySqlParameter(eParametrosSQL(i).nombre, IIf(eParametrosSQL(i).valor.ToLower = "null", DBNull.Value, eParametrosSQL(i).valor.Replace("'", ""))))
                        iSql = iSql.Replace("?" & eParametrosSQL(i).nombre, eParametrosSQL(i).valor)
                    Next i
                End If

                iCommand.CommandTimeout = 0
                loguear(iSql)

                iCommand.Connection = Me.iConexion

                If Not (IsNothing(Me.iActiveTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If

                If Not IsNothing(eParametrosSQL) Then eParametrosSQL.Clear()

                iCommand.ExecuteNonQuery()

            End If


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
            loguear(iSql, iFechaInicio, Now)
            'finalmente liberamos los objetos
            iCommand = Nothing
        End Try
    End Sub

    Public Sub ejecutarStoredProcedure(ByVal eNombreStoredProcedure As String, eListaParametros As List(Of ParametrosSQLStoredProcedureVO))
        Dim iDataAdapter As SqlDataAdapter
        Dim iCommand As MySqlCommand
        Dim iCommandSqlServer As SqlCommand
        Dim i As Integer
        Dim iSql As String

        Try
            iSql = eNombreStoredProcedure

            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
                If Not IsNothing(iConexionSQLServer) AndAlso iConexionSQLServer.State = ConnectionState.Closed Then iConexionSQLServer.Open()
                iCommandSqlServer = New SqlCommand(eNombreStoredProcedure, iConexionSQLServer)
                If Not (IsNothing(Me.activeTransactionSQLServer)) Then
                    iCommandSqlServer.Transaction = Me.activeTransactionSQLServer
                End If
                iCommandSqlServer.CommandType = CommandType.StoredProcedure

                If Not IsNothing(eListaParametros) Then
                    For i = 0 To eListaParametros.Count - 1
                        iCommandSqlServer.Parameters.Add(New SqlParameter(eListaParametros(i).nombreParametro, IIf(eListaParametros(i).valor.ToLower = "null", DBNull.Value, eListaParametros(i).valor.Replace("'", ""))))
                        iSql &= "@" & eListaParametros(i).nombreParametro & "=" & eListaParametros(i).valor.ToString
                    Next i
                End If

                iCommandSqlServer.CommandTimeout = 0
                iCommandSqlServer.ExecuteNonQuery()
            Else
                If Not IsNothing(iConexion) AndAlso iConexion.State = ConnectionState.Closed Then iConexion.Open()
                iCommand = New MySqlCommand(eNombreStoredProcedure, iConexion)
                If Not (IsNothing(Me.activeTransaction)) Then
                    iCommand.Transaction = Me.activeTransaction
                End If
                iCommand.CommandType = CommandType.StoredProcedure

                If Not IsNothing(eListaParametros) Then
                    For i = 0 To eListaParametros.Count - 1
                        iCommand.Parameters.Add(New MySqlParameter(eListaParametros(i).nombreParametro, IIf(eListaParametros(i).valor.ToLower = "null", DBNull.Value, eListaParametros(i).valor.Replace("'", ""))))
                        iSql &= "@" & eListaParametros(i).nombreParametro & "=" & eListaParametros(i).valor.ToString
                    Next i
                End If

                iCommand.CommandTimeout = 0
                iCommand.ExecuteNonQuery()
            End If

            If Not IsNothing(eListaParametros) Then eListaParametros.Clear()

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
            iCommandSqlServer = Nothing
            iCommand = Nothing
        End Try
    End Sub
#End Region

#Region "MetodosAuxiliares"
    Private Function obtenerTipoDatoSQL(eObjeto As Object, eNombreCampo As String) As SqlDbType
        If IsNumeric(eObjeto) Then
            If eObjeto.ToString.Contains(".") OrElse eObjeto.ToString.Contains(",") Then
                eObjeto = CDbl(eObjeto)
            Else
                If Math.Abs(CLng(eObjeto)) > 2147483648 Then
                    eObjeto = CLng(eObjeto)
                Else
                    eObjeto = CInt(eObjeto)
                End If
            End If
        ElseIf IsDate(eObjeto) Then
            eObjeto = CDate(eObjeto)
        End If

        If TypeOf (eObjeto) Is DateTime Then
            Return SqlDbType.DateTime
        ElseIf TypeOf (eObjeto) Is Date OrElse (eNombreCampo.Contains("fecha") AndAlso eObjeto.ToString() = "null") Then
            Return SqlDbType.Date
        ElseIf TypeOf (eObjeto) Is String AndAlso Len(eObjeto) < 8000 Then
            Return SqlDbType.VarChar
        ElseIf TypeOf (eObjeto) Is String AndAlso Len(eObjeto) > 8000 Then
            Return SqlDbType.Text
        ElseIf TypeOf (eObjeto) Is Long Then
            Return SqlDbType.BigInt
        ElseIf TypeOf (eObjeto) Is Integer Then
            Return SqlDbType.Int
        ElseIf TypeOf (eObjeto) Is Double Then
            Return SqlDbType.Decimal
        Else
            Return Nothing
        End If
        'Return SqlDbType.Variant
    End Function


#End Region

#Region "LOG"
    Public Sub loguear(ByVal eLog As String)
        Debug.WriteLine(eLog)
        If datosConexion.getInstancia.loguear AndAlso datosConexion.getInstancia.pathArchivoLog <> Nothing Then
            Dim iStreamWriter As System.IO.StreamWriter
            Dim iLog As String

            Try
                iLog = Now & vbTab
                If Not IsNothing(iActiveTransaction) Then
                    iLog &= "T" & vbTab
                Else
                    iLog &= "S/T" & vbTab
                End If
                If Not IsNothing(iConexion) AndAlso Not eLog.Contains("CIERRA CONEXION") Then
                    Try
                        iLog &= iConexion.ServerThread & vbTab
                    Catch ex As Exception
                    End Try
                End If
                iLog &= eLog.Replace(vbNewLine, " ").Replace(vbTab, " ")

                iStreamWriter = New System.IO.StreamWriter(datosConexion.getInstancia.pathArchivoLog & Format(Today, "ddMMyyyy") & ".xls", True)
                iStreamWriter.WriteLine(iLog)
            Catch ex As Exception
            Finally
                If Not IsNothing(iStreamWriter) Then iStreamWriter.Close()
                iStreamWriter = Nothing
            End Try
        End If
    End Sub

    Public Sub loguearError(ByVal eLog As String)
        If datosConexion.getInstancia.pathArchivoLog <> Nothing Then
            Dim iStreamWriter As System.IO.StreamWriter
            Dim iLog As String

            Try
                iLog = Now & vbTab
                If Not IsNothing(iConexion) Then
                    iLog &= iConexion.ServerThread & vbTab
                Else
                    iLog &= "S/C" & vbTab
                End If
                If Not IsNothing(iActiveTransaction) Then
                    iLog &= "T" & vbTab
                Else
                    iLog &= "S/T" & vbTab
                End If
                iLog &= eLog.Replace(vbNewLine, " ").Replace(vbTab, " ")

                iStreamWriter = New System.IO.StreamWriter(datosConexion.getInstancia.pathArchivoLog & "ERROR" & Format(Today, "ddMMyyyy") & ".xls", True)
                iStreamWriter.WriteLine(iLog)
                loguear("Error|" & eLog)
            Catch ex As Exception
            Finally
                If Not IsNothing(iStreamWriter) Then iStreamWriter.Close()
                iStreamWriter = Nothing
            End Try
        End If
    End Sub

    Public Sub loguear(ByVal eLog As String, ByVal eFechaInicio As Date, eFechaFin As Date)
        If DateDiff(DateInterval.Second, eFechaInicio, eFechaFin) > 10 Then
            Dim iStreamWriter As System.IO.StreamWriter
            Dim iLog As String

            Try
                iLog = Now & vbTab
                iLog &= diferenciaTiempo(eFechaInicio, eFechaFin) & vbTab
                If Not IsNothing(iConexion) Then
                    iLog &= iConexion.ServerThread & vbTab
                Else
                    iLog &= "S/C" & vbTab
                End If
                If Not IsNothing(iActiveTransaction) OrElse Not IsNothing(iActiveTransactionSQLServer) Then
                    iLog &= "T" & vbTab
                Else
                    iLog &= "S/T" & vbTab
                End If
                iLog &= eLog.Replace(vbNewLine, " ").Replace(vbTab, " ")

                iStreamWriter = New System.IO.StreamWriter(datosConexion.getInstancia.pathArchivoLog & "LOW" & Format(Now, "ddMMyyyy") & ".xls", True)
                iStreamWriter.WriteLine(iLog)
            Catch ex As Exception
            Finally
                If Not IsNothing(iStreamWriter) Then iStreamWriter.Close()
                iStreamWriter = Nothing
            End Try
        End If
    End Sub

    Private Function diferenciaTiempo(ByVal eFechaInicio As Date, ByVal eFechafin As Date) As String
        Dim iHoras, iMinutos, iSegundos As Long

        Try
            iHoras = DateDiff(DateInterval.Hour, eFechaInicio, eFechafin)
            iMinutos = DateDiff(DateInterval.Minute, eFechaInicio, eFechafin) - (iHoras * 60)
            iSegundos = DateDiff(DateInterval.Second, eFechaInicio, eFechafin) - (iHoras * 3600) - (iMinutos * 60)

            Return Right("00" & iHoras, 2) & ":" & Right("00" & iMinutos, 2) & ":" & Right("00" & iSegundos, 2)

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#End Region

End Class

