Imports System.Data.OleDb

Public Class AccesoDatosAccess

#Region "Atributos"
    Private iActiveTransaction As OleDbTransaction
    Private iConexion As OleDbConnection
    Private iDatosConexionAccess As datosConexionAccess
#End Region

#Region "Propiedades"
    Public Property activeTransaction() As OleDbTransaction
        Get
            Return iActiveTransaction
        End Get
        Set(ByVal Value As OleDbTransaction)
            iActiveTransaction = Value
        End Set
    End Property

    Public Property conexion() As OleDbConnection
        Get
            Return iConexion
        End Get
        Set(ByVal Value As OleDbConnection)
            iConexion = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Sub New(ePathArchivo As String)
        Try
            iConexion = obtenerConexion(ePathArchivo)
            iConexion.ReleaseObjectPool()

        Catch exception As ErrorConexionException
            Throw New ErrorConexionException(exception, "Se ha producido un error en la conexión a la base de datos.")
        End Try
    End Sub

    Public Function obtenerConexion(ePathArchivo As String) As OleDbConnection
        Dim iConexion As New OleDbConnection

        Try
            iConexion.ConnectionString = datosConexionAccess.getInstancia().stringConexion & ePathArchivo
            iConexion.Open()
            Return iConexion
        Catch exception As Exception
            Throw New ErrorConexionException(exception, "Se ha producido un error en la conexión a la base de datos.")
        End Try
    End Function

    Public Function getDataSet(ByVal eSql As String, ByVal eTabla As String, ePathArchivo As String) As DataSet
        Dim iDataAdapter As OleDbDataAdapter
        Dim iDataSet As DataSet

        Try

            iDataAdapter = New OleDbDataAdapter(eSql, datosConexionAccess.getInstancia().stringConexion & ePathArchivo)

            iDataSet = New DataSet()

            iDataAdapter.Fill(iDataSet, eTabla)
            Return iDataSet
        Catch exception As Exception
            Throw New ErrorConexionException(exception, "Se ha producido un error en la conexión a la base de datos.")
        Finally
            iDataAdapter = Nothing
        End Try
    End Function

    Public Function getDataSet(ByVal eDataSet As DataSet, ByVal eSql As String, ByVal eTabla As String, ePathArchivo As String) As DataSet
        Dim iDataAdapter As OleDbDataAdapter

        Try
            iDataAdapter = New OleDbDataAdapter(eSql, datosConexionAccess.getInstancia().stringConexion & ePathArchivo)

            iDataAdapter.Fill(eDataSet, eTabla)
            Return eDataSet
        Catch exception As Exception
            Throw New ErrorConexionException(exception, "Se ha producido un error en la conexión a la base de datos.")
        Finally
            iDataAdapter = Nothing
        End Try
    End Function

    Public Function getDataReader(ByVal eSql As String) As IDataReader
        Dim iCommand As OleDbCommand

        Try
            iCommand = New OleDbCommand(eSql, iConexion)
            If Not (IsNothing(iActiveTransaction)) Then
                iCommand.Transaction = activeTransaction
            End If

            Return iCommand.ExecuteReader()
        Catch exception As Exception
            Throw New ErrorConexionException(exception, "Se ha producido un error en la conexión a la base de datos.")
        Finally
            iCommand = Nothing
        End Try
    End Function

    Public Sub ejecutar(ByVal eSql As String)
        Dim iCommand As OleDbCommand

        Try
            iCommand = New OleDbCommand(eSql, iConexion)

            If Not (IsNothing(iActiveTransaction)) Then
                iCommand.Transaction = activeTransaction
            End If
            iCommand.ExecuteNonQuery()

        Catch exception As Exception
            Throw exception
        Finally
            iCommand = Nothing
        End Try
    End Sub

    Public Sub beginTransaction()
        Try
            iActiveTransaction = iConexion.BeginTransaction()
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub rollBack()
        iActiveTransaction.Rollback()
    End Sub

    Public Sub commit()
        iActiveTransaction.Commit()
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
        If Not IsNothing(iConexion) Then
            iConexion = Nothing
        End If
    End Sub

#End Region

End Class

