Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class TipoJurisdiccionSello
    Inherits Entidad

#Region "Constantes"
    Public Shared CLIENTE As Integer = 1
    Public Shared COMERCIO As Integer = 2
    Public Shared SUCURSAL As Integer = 3
#End Region

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
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
#End Region

#Region "Metodos"

    Public Function obtenerTipoJurisdiccionSello() As TipoJurisdiccionSello

        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("TipoJurisdiccionSello")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                id = iDataReader.Item("id").ToString
                iDescripcion = iDataReader.Item("descripcion").ToString
                Return Me
            Else
                Throw New RootException()
            End If

        Catch excepcion As Exception
            Throw New RootException(excepcion)
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

    Public Function obtenerTiposJurisdiccionSello() As IDataReader

        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("TipoJurisdiccionSello")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

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
