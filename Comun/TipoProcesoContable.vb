Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class TipoProcesoContable
    Inherits Entidad

#Region "Constantes"
    Public Shared COBRANZA As Integer = 1
    Public Shared DEVENGAMIENTO As Integer = 2
    Public Shared SOLICITUD As Integer = 3
    Public Shared LIQUIDACION As Integer = 4
    Public Shared ASIENTOCOMPRA As Integer = 5
    Public Shared MOVIMIENTO As Integer = 6
    Public Shared FORMAPAGOSOLICITUD As Integer = 7
    Public Shared ASIENTOPAGO As Integer = 8
    Public Shared COMISION As Integer = 9
    Public Shared DEVENGAMIENTOALORIGEN As Integer = 10
    Public Shared DEVENGAMIENTODIARIO As Integer = 11
    Public Shared CUOTASNOCOBRADAS As Integer = 12
    Public Shared DEVENGAMIENTOCOMISION As Integer = 13
    Public Shared DEVENGAMIENTOAJUSTE As Integer = 14
    Public Shared IVADEBITO As Integer = 15
    Public Shared PREVISION As Integer = 16
    Public Shared PRECANCELACIONES As Integer = 17
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

    Public Function obtenerTipoProceso() As TipoProcesoContable

        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoProcesoContable")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                id = iDataReader.Item("id").ToString
                iDescripcion = iDataReader.Item("descripcion").ToString
                Return Me
            Else
                Throw New TipoProcesoNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New TipoProcesoNoEncontradoException(excepcion)
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

    Public Function obtenerTipoProcesoIDataReader() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoProcesoContable")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New TipoProcesoNoEncontradoException(exception)
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

