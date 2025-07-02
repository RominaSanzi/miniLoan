Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class TipoProceso

    Inherits Entidad

#Region "Constantes"
    Public Shared RAPIPAGO As Integer = 1
    Public Shared PAGOFACIL As Integer = 2
    Public Shared BAPRO As Integer = 3
    Public Shared DEBITO As Integer = 4
    Public Shared COBRANZAINTERFACE As Integer = 5
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

    Public Function obtenerTipoProceso() As TipoProceso

        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("tipoProceso")
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
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function



#End Region

End Class

