Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class TipoParametro

    Inherits Entidad

#Region "Constantes"
    Public Shared GENERICO As Integer = 1
    Public Shared CAJA As Integer = 2
    Public Shared MEDIOPAGO As Integer = 3
    Public Shared SERVICIO As Integer = 4
    Public Shared SMS As Integer = 5
    Public Shared TARJETA As Integer = 6
    Public Shared IMPRESION As Integer = 7
    Public Shared CONTABILIDAD As Integer = 8
    Public Shared NUMERO As Integer = 9
    Public Shared FACTURACION As Integer = 10
    Public Shared TIPOFACTURACION As Integer = 11
    Public Shared [INTERFACE] As Integer = 12
#End Region

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
    Private iConexion As accesoDatos

    Public Property id As Long
        Get
            Return iId
        End Get
        Set(value As Long)
            iId = value
        End Set
    End Property

    Public Property descripcion As String
        Get
            Return iDescripcion
        End Get
        Set(value As String)
            iDescripcion = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerTipoParametro() As TipoParametro
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoParametro")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDescripcion = FuncionComun.vacioSiEsNulo(iDataReader.Item("descripcion").ToString)
                Return Me
            Else
                Throw New TipoParametroNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New TipoParametroNoEncontradoException(excepcion)
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

    Public Function obtenerTiposParametro() As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoParametro")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New TipoParametroNoEncontradoException(excepcion)
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
