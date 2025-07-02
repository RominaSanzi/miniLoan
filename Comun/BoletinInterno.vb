Imports di.financiera.Datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades

Public Class BoletinInterno

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iIdPersona As Long
    Private iCalificacion As String
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

    Public Property idPersona() As Long
        Get
            Return iIdPersona
        End Get
        Set(ByVal Value As Long)
            iIdPersona = Value
        End Set
    End Property

    Public Property calificacion() As String
        Get
            Return iCalificacion
        End Get
        Set(ByVal Value As String)
            iCalificacion = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerBoletinInterno() As BoletinInterno
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As iDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("idPersona")
            iGeneradorSql.agregarColumna("calificacion")
            iGeneradorSql.agregarTabla("boletinInterno")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If idPersona <> Nothing Then iGeneradorSql.agregarCondicionWhere("idPersona=" & idPersona)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                id = iDataReader.Item("id").ToString
                idPersona = iDataReader.Item("idPersona").ToString
                iCalificacion = iDataReader.Item("calificacion").ToString
                Return Me
            Else
                Throw New BoletinInternoNoEncontradoException()
            End If
        Catch excepcion As Exception
            Throw New BoletinInternoNoEncontradoException(excepcion)
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
