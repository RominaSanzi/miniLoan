Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class TipoNivel

    Inherits Entidad

#Region "Constantes"
    Public Shared SUCURSAL As Integer = 1
    Public Shared UNIDADDENEGOCIOS As Integer = 2
    Public Shared EMPRESAGRUPO As Integer = 3
    Public Shared GRUPOEMPRESAS As Integer = 4
    Public Shared PUNTOVENTADGI As Integer = 5
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
    Public Property Descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerTipoNivel() As TipoNivel
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoNivel")
            If Id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & Id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Id = iDataReader.Item("id").ToString
                iDescripcion = iDataReader.Item("descripcion").ToString
                Return Me
            Else
                Throw New TipoNivelNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New TipoNivelNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerTiposNivel() As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoNivel")
            If Id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & Id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New TipoNivelNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

        Catch exception As exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class
