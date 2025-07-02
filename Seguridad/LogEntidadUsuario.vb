Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class LogEntidadUsuario

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iUsuario As usuario
    Private iEntidad As Object
    Private iTipoEntidad As TipoEntidad
    Private iFecha As Date
    Private iHora As TimeSpan
    Private iObservacion As String
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

    Public Property usuario() As usuario
        Get
            Return iUsuario
        End Get
        Set(ByVal Value As usuario)
            iUsuario = Value
        End Set
    End Property

    Public Property tipoEntidad() As TipoEntidad
        Get
            Return iTipoEntidad
        End Get
        Set(ByVal Value As TipoEntidad)
            iTipoEntidad = Value
        End Set
    End Property

    Public Property entidad() As Object
        Get
            Return iEntidad
        End Get
        Set(ByVal Value As Object)
            iEntidad = Value
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

    Public Property hora() As TimeSpan
        Get
            Return iHora
        End Get
        Set(ByVal Value As TimeSpan)
            iHora = Value
        End Set
    End Property

    Public Property observacion As String
        Get
            Return iObservacion
        End Get
        Set(value As String)
            iObservacion = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Private Sub validarCrear()

        If IsNothing(usuario) Then
            Throw New LogNoCreadoException("El usuario no puede ser nulo")
        End If

        If IsNothing(tipoEntidad) Then
            Throw New LogNoCreadoException("El tipo de entidad no puede ser nulo")
        End If

        If IsNothing(entidad) Then
            Throw New LogNoCreadoException("La entidad no puede ser nula")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

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

            iGeneradorSql.agregarTabla("tipoentidad")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarCondicionWhere("id=" & tipoEntidad.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If Not iDataReader.Read Then
                Throw New LogNoCreadoException("El tipo de entidad es inexistente")
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
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("logEntidadUsuario")

            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("idTipoEntidad")
            iGeneradorSql.agregarColumna("idEntidad")
            iGeneradorSql.agregarColumna("fecha")
            iGeneradorSql.agregarColumna("hora")
            iGeneradorSql.agregarColumna("Observacion")

            iGeneradorSql.agregarValue(usuario.id)
            iGeneradorSql.agregarValue(tipoEntidad.id)
            iGeneradorSql.agregarValue(entidad.id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Today))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(Now))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iObservacion))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New LogNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("logEntidadUsuario")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

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

    Public Sub eliminarLogsPorUsuario()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("logEntidadUsuario")
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

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            If Not IsNothing(iUsuario) Then iUsuario.dispose()
            If Not IsNothing(iTipoEntidad) Then iTipoEntidad.dispose()

        Catch exception As exception
            Throw New RootException(exception)
        End Try
    End Sub

    Public Function obtenerLogsEntidad(ByVal eLogsEntidadVO As LogsEntidadVO) As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iTiposEntidad As String
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            If eLogsEntidadVO.tiposEntidad.Count > 0 Then
                iTiposEntidad = "("
                For i = 1 To eLogsEntidadVO.tiposEntidad.Count
                    iTiposEntidad &= eLogsEntidadVO.tiposEntidad.Item(i).id & ","
                Next
                iTiposEntidad = Left(iTiposEntidad, Len(iTiposEntidad) - 1) & ")"
            End If

            iGeneradorSql.agregarTabla("logEntidadUsuario l")
            iGeneradorSql.agregarTabla("tipoEntidad te")
            iGeneradorSql.agregarTabla("usuario u")

            iGeneradorSql.agregarColumna("l.fecha")
            iGeneradorSql.agregarColumna("l.hora")
            iGeneradorSql.agregarColumna("te.descripcion as tipoEntidad")
            iGeneradorSql.agregarColumna("u.nombre as usuario")

            iGeneradorSql.agregarCondicionWhere("te.id=l.idTipoEntidad")
            iGeneradorSql.agregarCondicionWhere("u.id=l.idUsuario")

            If Len(iTiposEntidad) > 0 Then iGeneradorSql.agregarCondicionWhere("te.id in " & iTiposEntidad)
            If Not IsNothing(eLogsEntidadVO.entidad) Then iGeneradorSql.agregarCondicionWhere("l.idEntidad=" & eLogsEntidadVO.entidad.id)
            If Not IsNothing(eLogsEntidadVO.usuario) Then iGeneradorSql.agregarCondicionWhere("u.id = " & eLogsEntidadVO.usuario.id)


            If eLogsEntidadVO.fechaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.fecha>=" & FuncionComun.nuloSiEsNothing(eLogsEntidadVO.fechaDesde))
            If eLogsEntidadVO.fechaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.fecha<=" & FuncionComun.nuloSiEsNothing(eLogsEntidadVO.fechaHasta))

            If eLogsEntidadVO.horaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.hora>='" & Format(eLogsEntidadVO.horaDesde, "HH:mm:ss") & "'")
            If eLogsEntidadVO.horaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.hora<='" & Format(eLogsEntidadVO.horaHasta, "HH:mm:ss") & "'")


            iGeneradorSql.agregarOrden("l.fecha")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "LogsEntidad")

        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerSolicitudesConcretadasPolitica(ByVal eLogsEntidadVO As LogsEntidadVO, ByVal eIdPoliticaComercial As Long) As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("logEntidadUsuario l")
            iGeneradorSql.agregarTabla("tipoEntidad te")
            iGeneradorSql.agregarTabla("usuario u")
            iGeneradorSql.agregarTabla("solicitud s")
            iGeneradorSql.agregarTabla("solicitudPendiente sp")

            iGeneradorSql.agregarColumna("l.fecha")
            iGeneradorSql.agregarColumna("l.hora")
            iGeneradorSql.agregarColumna("te.descripcion as tipoEntidad")
            iGeneradorSql.agregarColumna("u.nombre as usuario")

            iGeneradorSql.agregarCondicionWhere("te.id=l.idTipoEntidad")
            iGeneradorSql.agregarCondicionWhere("u.id=l.idUsuario")

            iGeneradorSql.agregarCondicionWhere("te.id = " & TipoEntidad.SOLICITUD)
            iGeneradorSql.agregarCondicionWhere("l.idEntidad=s.id")
            iGeneradorSql.agregarCondicionWhere("sp.id = s.idSolicitudPendiente")
            iGeneradorSql.agregarCondicionWhere("sp.idPoliticaComercial= " & eIdPoliticaComercial)

            iGeneradorSql.agregarCondicionWhere("s.idEstado= " & Estado.ALTA)


            If Not IsNothing(eLogsEntidadVO.usuario) Then iGeneradorSql.agregarCondicionWhere("u.id = " & eLogsEntidadVO.usuario.id)


            If eLogsEntidadVO.fechaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.fecha>=" & FuncionComun.nuloSiEsNothing(eLogsEntidadVO.fechaDesde))
            If eLogsEntidadVO.fechaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.fecha<=" & FuncionComun.nuloSiEsNothing(eLogsEntidadVO.fechaHasta))

            If eLogsEntidadVO.horaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.hora<='" & eLogsEntidadVO.horaHasta.ToString & "'")
            If eLogsEntidadVO.horaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.hora>='" & eLogsEntidadVO.horaDesde.ToString & "'")


            iGeneradorSql.agregarOrden("l.fecha")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "LogsEntidad")

        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function


#End Region

End Class
