Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class TipoPersona

    Inherits Entidad

#Region "Constantes"
    Public Shared CLIENTE = 1
    Public Shared ADICIONAL = 2
    Public Shared GARANTE = 3
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

    Public Function obtenerTipoPersona() As TipoPersona

        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoPersona")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion=" & descripcion)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Id = iDataReader.Item("id").ToString
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

    Public Function obtenerTiposPersona() As IDataReader

        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoPersona")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
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
