Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.seguridad

Public Class Icono
    Inherits Entidad

#Region "Variables"

    Private iId As Long
    Private iIcono As String

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

    Public Property icono() As String
        Get
            Return iIcono
        End Get
        Set(ByVal Value As String)
            iIcono = Value
        End Set
    End Property
#End Region

#Region "Metodos"



    Public Function obtenerIconosGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("icono")

            iGeneradorSql.agregarTabla("Icono")


            iGeneradorSql.agregarLimit("1000")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Icono")

        Catch excepcion As Exception
            Throw New IconoNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerIcono() As Icono
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("icono")

            iGeneradorSql.agregarTabla("icono")

            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
                iIcono = FuncionComun.nothingSiEsNulo(iDataReader.Item("icono"))

                iDataReader.Close()

                Return Me
            Else
                Throw New IconoNoEncontradoException
            End If
        Catch BocaNoEncontradoException As IconoNoEncontradoException
            Throw BocaNoEncontradoException
        Catch excepcion As Exception
            Throw New IconoNoEncontradoException(excepcion)
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

    Public Function obtenerIconosLista() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Icono")

            iGeneradorSql.agregarTabla("Icono")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New IconoNoEncontradoException(excepcion)
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
