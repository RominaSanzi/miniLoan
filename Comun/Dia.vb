Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Public Class Dia
    Inherits Entidad

#Region "Constantes"
    Public Enum enumDias
        LUNES = 1
        MARTES = 2
        MIERCOLES = 3
        JUEVES = 4
        VIERNES = 5
        SABADO = 6
        DOMINGO = 7
        DOMINGOVB = 0
    End Enum

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

    Public Function obtenerDia() As Dia
        Dim iDataReader As iDataReader

        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("dia")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDescripcion = FuncionComun.vacioSiEsNulo(iDataReader.Item("descripcion").ToString)
                Return Me
            Else
                Throw New DiasNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New DiasNoEncontradoException(excepcion)
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

    Public Function obtenerdias() As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As iDataReader
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("dia")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            Return iDataReader
        Catch excepcion As Exception
            Throw New DiasNoEncontradoException(excepcion)
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
