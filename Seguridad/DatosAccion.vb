Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class DatosAccion

    Inherits Entidad

#Region "Variables"
    Private iAcciones As SortedList
    Private iConexion As accesoDatos
#End Region

#Region "Atributos"

    Public Property acciones() As SortedList
        Get
            Return iAcciones
        End Get
        Set(ByVal Value As SortedList)
            iAcciones = Value
        End Set
    End Property

#End Region

#Region "Métodos"

    Private Shared iInstancia As DatosAccion
    Private Shared iMutex As New System.Threading.Mutex()

    Public Shared Function getInstancia() As DatosAccion
        'Aca se evidencia la implementacion del patron Singleton: si no existe 
        'la unica instancia de este objeto la creamos, sino devolvemos la existente
        Try
            iMutex.WaitOne()
            If iInstancia Is Nothing Then
                iInstancia = New DatosAccion
            End If
            iMutex.ReleaseMutex()
            Return iInstancia
        Catch exception As exception
            If Not IsNothing(iMutex) Then iMutex.ReleaseMutex()
            Return Nothing
        End Try
    End Function

    Public Sub New()
        obtenerAcciones()
    End Sub


    Public Sub obtenerAcciones()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("paginaAsociada")
            iGeneradorSql.agregarTabla("accion")
            iGeneradorSql.agregarCondicionWhere("grabaLog=" & FuncionComun.booleanByte(True))
            iGeneradorSql.agregarOrden("paginaAsociada asc")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            iAcciones = New SortedList
            While iDataReader.Read
                iAcciones.Add(iDataReader.Item("paginaAsociada").ToString, iDataReader.Item("id").ToString)
            End While
        Catch excepcion As Exception
            Throw New AccionNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) AndAlso Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

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
