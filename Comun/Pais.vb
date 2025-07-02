Imports di.financiera.entidades
Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.IO

Public Class Pais
    Inherits Entidad

#Region "Atributos"

    Private iId As Long
    Private iCodigo As String
    Private iNombre As String
    Private iHabilitado As Boolean
    Private iGeolocalizacion As String
    Private iConexion As accesoDatos

#End Region

#Region "Propiedades"

    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
        End Set
    End Property
    Public Property codigo() As String
        Get
            Return iCodigo
        End Get
        Set(ByVal Value As String)
            iCodigo = Value
        End Set
    End Property
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
    Public Property habilitado() As Boolean
        Get
            Return iHabilitado
        End Get
        Set(ByVal Value As Boolean)
            iHabilitado = Value
        End Set
    End Property
    Public Property geolocalizacion As String
        Get
            Return iGeolocalizacion
        End Get
        Set(value As String)
            iGeolocalizacion = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Public Function obtenerPais() As Pais
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("habilitado")

            iGeneradorSql.agregarTabla("Pais")

            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo='" & iCodigo & "'")
            If iNombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("nombre like '%" & iNombre & "%'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iCodigo = iDataReader.Item("codigo").ToString
                iNombre = iDataReader.Item("nombre").ToString
                iHabilitado = FuncionComun.byteBoolean(iDataReader.Item("Habilitado").ToString)

                iDataReader.Close()

                Return Me
            Else
                Throw New PaisNoEncontradoException
            End If
        Catch PaisNoEncontradoException As PaisNoEncontradoException
            Throw PaisNoEncontradoException
        Catch excepcion As Exception
            Throw New PaisNoEncontradoException(excepcion)
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

    Public Function obtenerIdPais() As Long
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")

            iGeneradorSql.agregarTabla("Pais")

            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo='" & iCodigo & "'")
            If iNombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("nombre like '%" & iNombre & "%'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString

                iDataReader.Close()

                Return iId
            Else
                Throw New PaisNoEncontradoException
            End If
        Catch PaisNoEncontradoException As PaisNoEncontradoException
            Throw PaisNoEncontradoException
        Catch excepcion As Exception
            Throw New PaisNoEncontradoException(excepcion)
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

    Public Function obtenerPaisSoloId() As Pais
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")

            iGeneradorSql.agregarTabla("Pais")

            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo='" & iCodigo & "'")
            If iNombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("nombre like '%" & iNombre & "%'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString

                iDataReader.Close()

                Return Me
            Else
                Throw New PaisNoEncontradoException
            End If
        Catch PaisNoEncontradoException As PaisNoEncontradoException
            Throw PaisNoEncontradoException
        Catch excepcion As Exception
            Throw New PaisNoEncontradoException(excepcion)
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

    Public Function obtenerPaisDataSet() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("case when habilitado=" & FuncionComun.booleanByte(True) & " then 'SI' else 'NO' end as habilitado")

            iGeneradorSql.agregarTabla("Pais")

            If codigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo like '" & codigo & "%'")
            If nombre <> Nothing And nombre <> "" Then iGeneradorSql.agregarCondicionWhere("nombre like '" & iNombre & "%'")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Pais")

        Catch excepcion As Exception
            Throw New PaisNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerPaises() As IDataReader
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("nombre as descripcion")
            iGeneradorSql.agregarTabla("pais")

            iGeneradorSql.agregarOrden("nombre")
            iGeneradorSql.agregarCondicionWhere("habilitado=" & FuncionComun.booleanByte(True))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            Return iDataReader
        Catch excepcion As Exception
            Throw New PaisNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Private Sub validarCrear()
        If codigo = Nothing Then
            Throw New PaisNoCreadoException("El c�digo no puede ser nulo")
        Else
            Dim iGeneradorSql As New GeneradorSql
            Dim iDataReader As IDataReader

            Try
                iConexion = obtenerConexion()
                iGeneradorSql.agregarTabla("Pais")
                iGeneradorSql.agregarColumna("codigo")
                iGeneradorSql.agregarCondicionWhere("codigo='" & codigo & "'")

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    Throw New PaisNoCreadoException("El c�digo de Pais ya existe")
                End If
                iDataReader.Close()

            Catch excepcion As Exception
                Throw New PaisNoCreadoException(excepcion)
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
        End If
    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("Pais")

            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("habilitado")

            iGeneradorSql.agregarValue("'" & codigo & "'")
            iGeneradorSql.agregarValue("'" & nombre & "'")
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(habilitado))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New PaisNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarModificar()
        If id = Nothing Then
            Throw New PaisNoModificadoException("El Pais es inexistente")
        End If

        If codigo = Nothing Then
            Throw New PaisNoModificadoException("El c�digo de Pais es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Pais")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If Not iDataReader.Read Then
                Throw New PaisNoModificadoException("El Pais es inexistente")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("Pais")
            iGeneradorSql.agregarCondicionWhere("codigo='" & codigo & "'")
            iGeneradorSql.agregarCondicionWhere("id<>" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New PaisNoEliminadoException("El codigo del pais ya existe")
            End If
            iDataReader.Close()


        Catch excepcion As Exception
            Throw New PaisNoModificadoException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("nombre='" & nombre & "'")
            iGeneradorSql.agregarSet("codigo='" & codigo & "'")
            iGeneradorSql.agregarSet("habilitado=" & FuncionComun.booleanByte(habilitado))

            iGeneradorSql.agregarTabla("Pais")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New PaisNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarEliminar()
        If id = Nothing Then
            Throw New PaisNoEliminadoException("El Pais es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Pais")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If Not iDataReader.Read Then
                Throw New PaisNoEliminadoException("El Pais es inexistente")
            End If

            iDataReader.Close()

            'iGeneradorSql.agregarTabla("provincia")
            'iGeneradorSql.agregarColumna("id")
            'iGeneradorSql.agregarCondicionWhere("idPais=" & id)
            'iGeneradorSql.agregarLimit("1")

            'iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            'If iDataReader.Read Then
            '    Throw New BarrioNoEliminadoException("El pais tiene anexas provincias")
            'End If
            'iDataReader.Close()


        Catch excepcion As Exception
            Throw New PaisNoEliminadoException(excepcion)
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
    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("Pais")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New PaisNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub


    Public Function obtenerPaisesParaGeolocalizar() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("pais pa")

            iGeneradorSql.agregarColumna("pa.Id")
            iGeneradorSql.agregarColumna("pa.nombre as pais")


            iGeneradorSql.agregarCondicionWhere("pa.geolocalizacion is null")

            ' iGeneradorSql.agregarLimit("100")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "paises")

        Catch exception As Exception
            Throw New ComercioNoEncontradoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Sub modificarGeolocalizacion()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarSet("geolocalizacion='" & geolocalizacion & "'")

            iGeneradorSql.agregarTabla("pais")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New ComercioNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
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