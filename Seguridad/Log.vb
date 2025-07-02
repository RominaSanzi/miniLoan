Imports System.Configuration
Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class Log

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iFecha As Date
    Private iUsuario As Usuario
    Private iAccion As Accion
    Private iDetalle As String
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

    Public Property usuario() As Usuario
        Get
            Return iUsuario
        End Get
        Set(ByVal Value As Usuario)
            iUsuario = Value
        End Set
    End Property

    Public Property accion() As Accion
        Get
            Return iAccion
        End Get
        Set(ByVal Value As Accion)
            iAccion = Value
        End Set
    End Property

    Public Property fecha() As Date
        Get
            Return iFecha
        End Get
        Set(ByVal Value As Date)
            iFecha = Value
        End Set
    End Property

    Public Property detalle() As String
        Get
            Return iDetalle
        End Get
        Set(ByVal Value As String)
            iDetalle = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function obtenerLog() As Log
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("log")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("fecha")
            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("idAccion")
            iGeneradorSql.agregarColumna("detalle")


            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id")
                iDetalle = iDataReader.Item("Detalle").ToString
                iFecha = FuncionComun.nothingSiEsVacio(iDataReader.Item("fecha").ToString)
                iUsuario = New Usuario
                iUsuario.id = FuncionComun.ceroSiEsVacio(iDataReader.Item("idUsuario").ToString)
                iAccion = New Accion
                iAccion.id = FuncionComun.ceroSiEsVacio(iDataReader.Item("idAccion").ToString)

                iDataReader.Close()

                iUsuario.accesoDatos = iConexion
                iUsuario = iUsuario.obtenerUsuarioSoloIds
                iUsuario.accesoDatos = Nothing

                iAccion.accesoDatos = iConexion
                iAccion = iAccion.obtenerAccion
                iAccion.accesoDatos = Nothing

                Return Me
            Else
                Throw New LogNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw New LogNoEncontradoException(excepcion)
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
    Public Function obtenerLogs(ByVal eLogVO As LogVO) As DataSet
        Dim iGeneradorSql As New GeneradorSql()
        Dim iStringUsuarios As String
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            If Not IsNothing(eLogVO.coleccionUsuarios) Then
                For i = 1 To eLogVO.coleccionUsuarios.Count
                    iStringUsuarios = iStringUsuarios & eLogVO.coleccionUsuarios.Item(i).id & ","
                Next
            End If

            iGeneradorSql.agregarColumna("l.id")
            iGeneradorSql.agregarColumna(FuncionComun.sqlFormatoFecha("l.fecha", entidades.FuncionComun.enumFormatoFecha.DDMMYYYYHHMMSS) & " as fecha")
            iGeneradorSql.agregarColumna("l.detalle")
            iGeneradorSql.agregarColumna("u.nombre as usuario")
            iGeneradorSql.agregarColumna("a.nombre as accion")

            iGeneradorSql.agregarTabla("log l")
            iGeneradorSql.agregarTabla("usuario u")
            iGeneradorSql.agregarTabla("accion a")

            iGeneradorSql.agregarCondicionWhere("l.idUsuario = u.id")
            iGeneradorSql.agregarCondicionWhere("l.idAccion = a.id")
            iGeneradorSql.agregarLimit("100")

            If eLogVO.fechaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere(FuncionComun.sqlFormatoFecha("l.fecha", entidades.FuncionComun.enumFormatoFecha.YYYYMMDD) & ">='" & Format(eLogVO.fechaDesde, "yyyy/MM/dd") & "'")
            If eLogVO.fechaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere(FuncionComun.sqlFormatoFecha("l.fecha", entidades.FuncionComun.enumFormatoFecha.YYYYMMDD) & " <='" & Format(eLogVO.fechaHasta, "yyyy/MM/dd") & "'")
            If Not IsNothing(eLogVO.usuario) Then iGeneradorSql.agregarCondicionWhere("l.idUsuario=" & eLogVO.usuario.id)
            If Not IsNothing(eLogVO.accion) Then iGeneradorSql.agregarCondicionWhere("l.idAccion=" & eLogVO.accion.id)
            If Not IsNothing(eLogVO.nivel) Then iGeneradorSql.agregarCondicionWhere("u.idNivel=" & eLogVO.nivel.id)
            If eLogVO.detalle <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.detalle like '%" & eLogVO.detalle & "%'")
            If Not IsNothing(eLogVO.coleccionUsuarios) Then iGeneradorSql.agregarCondicionWhere("u.id in (" & Left(iStringUsuarios, iStringUsuarios.Length - 1) & ")")
            If eLogVO.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.id=" & eLogVO.id)

            iGeneradorSql.agregarOrden("L.FECHA desc")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Logs")

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub validarCrear()

        If fecha = Nothing Then
            Throw New LogNoCreadoException("La fecha no puede ser nula")
        End If

        If IsNothing(usuario) Then
            Throw New LogNoCreadoException("El usuario no puede ser nulo")
        End If

        If IsNothing(accion) Then
            Throw New LogNoCreadoException("La accion no puede ser nula")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("id=" & usuario.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New LogNoCreadoException("El usuario es inexistente")
            End If

            iDataReader.Close()

            iGeneradorSql.agregarTabla("accion")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("id=" & accion.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New LogNoCreadoException("La accion es inexistente")
            End If

            iDataReader.Close()

        Catch excepcion As Exception
            Throw New LogNoCreadoException(excepcion)
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

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("log")
            iGeneradorSql.agregarColumna("fecha")
            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("idAccion")
            iGeneradorSql.agregarColumna("detalle")

            iGeneradorSql.agregarValue("'" & Format(fecha, "yyyy/MM/dd HH:mm:ss") & "'")
            iGeneradorSql.agregarValue(usuario.id)
            iGeneradorSql.agregarValue(accion.id)
            iGeneradorSql.agregarValue("'" & detalle & "'")

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New LogNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub eliminarLogsPorUsuario()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("log")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iUsuario.id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New LogNoEliminadoException(excepcion)
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

    Public Sub eliminar(ByVal eLogVo As LogVO)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iUsuarios As String
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            If Not IsNothing(eLogVo.nivel) Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarTabla("usuario")
                iGeneradorSql.agregarCondicionWhere("idNivel =" & eLogVo.nivel.id)

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                While iDataReader.Read
                    iUsuarios = iUsuarios & iDataReader.Item("id").ToString & ","
                End While

                iUsuarios = Left(iUsuarios, Len(iUsuarios) - 1)
                If iUsuarios <> Nothing Then iGeneradorSql.agregarCondicionWhere("idUsuario in(" & iUsuarios & ")")
                iDataReader.Close()
            End If

            iGeneradorSql.agregarTabla("log")

            If eLogVo.fechaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("fecha >='" & Format(eLogVo.fechaDesde, "yyyy/MM/dd") & "'")
            If eLogVo.fechaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("fecha <='" & Format(eLogVo.fechaHasta, "yyyy/MM/dd") & "'")
            If Not IsNothing(eLogVo.usuario) Then iGeneradorSql.agregarCondicionWhere("idUsuario=" & eLogVo.usuario.id)
            If Not IsNothing(eLogVo.accion) Then iGeneradorSql.agregarCondicionWhere("idAccion=" & eLogVo.accion.id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New LogNoEliminadoException(excepcion)
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

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            If Not IsNothing(iUsuario) Then iUsuario.dispose()
            If Not IsNothing(iAccion) Then iAccion.dispose()

        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub

#End Region

#Region "Log Errores"

    Public Shared Function obtenerErrorAplicacion(ByVal eExcepcion As Exception, eOrigen As String, Optional eUsuario As Usuario = Nothing, Optional eConexion As accesoDatos = Nothing) As ErrorAplicacionVO
        Dim iErrorAplicacionVO As New ErrorAplicacionVO

        Try

            If Not IsNothing(eExcepcion) Then
                If TypeOf (eExcepcion) Is RootException Then

                    Dim iExcepcionAplicacion As RootException = eExcepcion
                    iErrorAplicacionVO.mensajeUsuario = iExcepcionAplicacion.ToString
                    iErrorAplicacionVO.mensajeOriginal = iExcepcionAplicacion.ToString
                    iErrorAplicacionVO.rootException = True

                    While Not IsNothing(iExcepcionAplicacion.originalCause)
                        If TypeOf (iExcepcionAplicacion.originalCause) Is RootException Then
                            iExcepcionAplicacion = iExcepcionAplicacion.originalCause
                            iErrorAplicacionVO.mensajeUsuario = iExcepcionAplicacion.ToString
                            iErrorAplicacionVO.mensajeOriginal = iExcepcionAplicacion.ToString
                            iErrorAplicacionVO.rootException = True
                        Else
                            iErrorAplicacionVO.mensajeOriginal = iExcepcionAplicacion.originalCause.ToString
                            iErrorAplicacionVO.log = loguearError(eUsuario, eOrigen, iErrorAplicacionVO.mensajeOriginal, eConexion)
                            Try
                                iErrorAplicacionVO.log = loguearError(eUsuario, eOrigen, iErrorAplicacionVO.mensajeOriginal, eConexion)
                                iErrorAplicacionVO.mensajeUsuario = "Lo sentimos, existe un inconveniente en la aplicación. Vuelva a intentar la operación o comuníquese con nuestro help desk – código de referencia: " & iErrorAplicacionVO.log.id
                            Catch ex As Exception
                                iErrorAplicacionVO.mensajeUsuario = "Lo sentimos, existe un inconveniente en la aplicación. Vuelva a intentar la operación o comuníquese con nuestro help desk"
                            End Try
                            iErrorAplicacionVO.rootException = False
                            iExcepcionAplicacion.originalCause = Nothing
                        End If
                    End While

                Else
                    iErrorAplicacionVO.mensajeOriginal = eExcepcion.ToString
                    Try
                        iErrorAplicacionVO.log = loguearError(eUsuario, eOrigen, iErrorAplicacionVO.mensajeOriginal, eConexion)
                        iErrorAplicacionVO.mensajeUsuario = "Lo sentimos, existe un inconveniente en la aplicación. Vuelva a intentar la operación o comuníquese con nuestro help desk – código de referencia: " & iErrorAplicacionVO.log.id
                    Catch ex As Exception
                        iErrorAplicacionVO.mensajeUsuario = "Lo sentimos, existe un inconveniente en la aplicación. Vuelva a intentar la operación o comuníquese con nuestro help desk"
                    End Try
                    iErrorAplicacionVO.rootException = False
                End If
            End If

            Return iErrorAplicacionVO

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function loguearError(eUsuario As Usuario, eOrigen As String, eError As String, eConexion As accesoDatos) As Log
        Dim iLog As New Log()
        Dim iDatosAccion As DatosAccion

        Try
            With iLog
                If IsNothing(eUsuario) Then
                    .usuario = New Usuario
                    .usuario.id = CLng(ConfigurationManager.AppSettings("usuarioDefecto"))
                Else
                    .usuario = eUsuario
                End If
                .accion = New Accion()
                .accion.id = iDatosAccion.getInstancia.acciones.Item("Error.aspx")

                .detalle = "ORIGEN:" & eOrigen & vbNewLine
                .detalle &= "ERROR:" & eError & vbNewLine

                .id = .crearLogError(.usuario, .accion, .detalle, eConexion)
            End With

            Return iLog

        Catch exception As Exception
            Throw New LogNoCreadoException()
        Finally
            iLog = Nothing
        End Try
    End Function

    Private Shared Sub validarCrearLogError(eUsuario As Usuario, eAccion As Accion, eDetalle As String)

        If IsNothing(eUsuario) Then
            Throw New LogNoCreadoException("El usuario no puede ser nulo")
        End If

        If IsNothing(eAccion) Then
            Throw New LogNoCreadoException("La accion no puede ser nula")
        End If

        If eDetalle = Nothing Then
            Throw New LogNoCreadoException("El detalle no puede ser nulo")
        End If

    End Sub

    Public Shared Function crearLogError(eUsuario As Usuario, eAccion As Accion, eDetalle As String, eConexion As accesoDatos) As Long
        Dim iConexion As accesoDatos
        Dim iGeneradorSql As New GeneradorSql

        Try
            validarCrearLogError(eUsuario, eAccion, eDetalle)

            If IsNothing(eConexion) Then
                iConexion = New accesoDatos
            Else
                iConexion = eConexion
            End If


            iGeneradorSql.agregarTabla("log")
            iGeneradorSql.agregarColumna("fecha")
            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("idAccion")
            iGeneradorSql.agregarColumna("detalle")

            iGeneradorSql.agregarValue("'" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "'")
            iGeneradorSql.agregarValue(eUsuario.id)
            iGeneradorSql.agregarValue(eAccion.id)
            iGeneradorSql.agregarValue("'" & eDetalle.Replace("'", "´") & "'")

            Return iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
        Finally
            If (IsNothing(eConexion)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

#End Region

End Class
