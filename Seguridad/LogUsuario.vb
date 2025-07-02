Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Collections.Generic

Public Class LogUsuario

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iUsuario As Usuario
    Private iUsuarioModificador As Usuario
    Private iFecha As Date
    Private iHora As TimeSpan
    Private iLoginAnterior As String
    Private iLoginActual As String
    Private iEstadoAnterior As Estado
    Private iEstadoActual As Estado
    Private iEstadoUsuarioAnterior As EstadoUsuario
    Private iEstadoUsuarioActual As EstadoUsuario
    Private iPerfilesAnteriores As List(Of Perfil)
    Private iPerfilesActuales As List(Of Perfil)

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

    Public Property usuarioModificador() As Usuario
        Get
            Return iUsuarioModificador
        End Get
        Set(ByVal Value As Usuario)
            iUsuarioModificador = Value
        End Set
    End Property
    Public Property estadoAnterior() As Estado
        Get
            Return iEstadoAnterior
        End Get
        Set(ByVal Value As Estado)
            iEstadoAnterior = Value
        End Set
    End Property
    Public Property estadoActual() As Estado
        Get
            Return iEstadoActual
        End Get
        Set(ByVal Value As Estado)
            iEstadoActual = Value
        End Set
    End Property
    Public Property estadoUsuarioAnterior() As EstadoUsuario
        Get
            Return iEstadoUsuarioAnterior
        End Get
        Set(ByVal Value As EstadoUsuario)
            iEstadoUsuarioAnterior = Value
        End Set
    End Property
    Public Property estadoUsuarioActual() As EstadoUsuario
        Get
            Return iEstadoUsuarioActual
        End Get
        Set(ByVal Value As EstadoUsuario)
            iEstadoUsuarioActual = Value
        End Set
    End Property
    Public Property perfilesAnteriores() As List(Of Perfil)
        Get
            Return iPerfilesAnteriores
        End Get
        Set(ByVal Value As List(Of Perfil))
            iPerfilesAnteriores = Value
        End Set
    End Property
    Public Property perfilesActuales() As List(Of Perfil)
        Get
            Return iperfilesActuales
        End Get
        Set(ByVal Value As List(Of Perfil))
            iperfilesActuales = Value
        End Set
    End Property

    Public Property loginAnterior As String
        Get
            Return iLoginAnterior
        End Get
        Set(value As String)
            iLoginAnterior = value
        End Set
    End Property

    Public Property loginActual As String
        Get
            Return iLoginActual
        End Get
        Set(value As String)
            iLoginActual = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Private Sub validarCrear()

        If IsNothing(usuario) Then
            Throw New LogNoCreadoException("El usuario no puede ser nulo")
        End If

        If IsNothing(iUsuarioModificador) Then
            Throw New LogNoCreadoException("El usuario modificador no puede ser nulo")
        End If

        Dim iGeneradorSql As New GeneradorSql
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

            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("id=" & usuarioModificador.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New LogNoCreadoException("El usuario modificador es inexistente")
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
        Dim iGeneradorSql As New GeneradorSql
        Dim iIdsListaPerfilesActuales, iIdsListaPerfilesAnteriores As String

        Try
            iConexion = obtenerConexion()

            validarCrear()

            For Each iPerfil As Perfil In iPerfilesActuales
                iIdsListaPerfilesActuales &= iPerfil.id & ","
            Next
            iIdsListaPerfilesActuales = Left(iIdsListaPerfilesActuales, iIdsListaPerfilesActuales.Length - 1)

            For Each iPerfil As Perfil In iPerfilesAnteriores
                iIdsListaPerfilesAnteriores &= iPerfil.id & ","
            Next
            iIdsListaPerfilesAnteriores = Left(iIdsListaPerfilesAnteriores, iIdsListaPerfilesAnteriores.Length - 1)

            iGeneradorSql.agregarTabla("logUsuario")

            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("idUsuarioModificador")
            iGeneradorSql.agregarColumna("fecha")
            iGeneradorSql.agregarColumna("hora")
            iGeneradorSql.agregarColumna("loginAnterior")
            iGeneradorSql.agregarColumna("loginActual")
            iGeneradorSql.agregarColumna("idEstadoAnterior")
            iGeneradorSql.agregarColumna("idEstadoActual")
            iGeneradorSql.agregarColumna("idEstadoUsuarioAnterior")
            iGeneradorSql.agregarColumna("idEstadoUsuarioActual")
            iGeneradorSql.agregarColumna("idPerfilesAnteriores")
            iGeneradorSql.agregarColumna("idPerfilesActuales")

            iGeneradorSql.agregarValue(usuario.id)
            iGeneradorSql.agregarValue(usuarioModificador.id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Today))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(Now))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iLoginAnterior))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iLoginActual))
            If IsNothing(estadoAnterior) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(IIf(iEstadoAnterior.isAlta, Estado.ALTA, Estado.BAJA))
            End If
            If IsNothing(estadoActual) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(IIf(estadoActual.isAlta, Estado.ALTA, Estado.BAJA))
            End If
            If IsNothing(estadoUsuarioAnterior) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(estadoUsuarioAnterior.id)
            End If
            If IsNothing(estadoUsuarioActual) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(estadoUsuarioActual.id)
            End If
            iGeneradorSql.agregarValue(iIdsListaPerfilesAnteriores)
            iGeneradorSql.agregarValue(iIdsListaPerfilesActuales)

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

    Public Function obtenerLogUsuarioReporte(ByVal eLogUsuarioReporteVO As LogUsuarioReporteVO) As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTablaPrincipal("logUsuario l")
            iGeneradorSql.agregarTablaConJoin("estadousuario euant", "l.idEstadoUsuarioAnterior=euant.id", True)
            iGeneradorSql.agregarTablaConJoin("estadousuario euact", "l.idEstadoUsuarioActual=euact.id", True)
            iGeneradorSql.agregarTablaConJoin("perfil pant", "l.idPerfilesAnteriores=pant.id", True)
            iGeneradorSql.agregarTablaConJoin("perfil pact", "l.idPerfilesActuales= pact.id", True)
            iGeneradorSql.agregarTablaConJoin("usuario u", "u.id=l.idUsuario")
            iGeneradorSql.agregarTablaConJoin("usuario um", "um.id=l.idUsuarioModificador")

            iGeneradorSql.agregarColumna("l.id")
            iGeneradorSql.agregarColumna("l.fecha")
            iGeneradorSql.agregarColumna("l.hora")
            iGeneradorSql.agregarColumna("u.nombre As usuario")
            iGeneradorSql.agregarColumna("um.nombre As usuarioModificador")
            iGeneradorSql.agregarColumna("case When l.idEstadoAnterior=" & Estado.ALTA & " Then 'ALTA' else case when l.idEstadoAnterior=" & Estado.BAJA & " then 'BAJA' else '' end end as estadoAnterior")
            iGeneradorSql.agregarColumna("case when l.idEstadoActual=" & Estado.ALTA & " then 'ALTA' else case when l.idEstadoActual=" & Estado.BAJA & " then 'BAJA' else '' end end as estadoActual")
            iGeneradorSql.agregarColumna("euant.descripcion as estadoUsuarioAnterior")
            iGeneradorSql.agregarColumna("euact.descripcion as estadoUsuarioActual")
            iGeneradorSql.agregarColumna("group_concat(pant.nombre) as perfilAnterior")
            iGeneradorSql.agregarColumna("group_concat(pact.nombre) as perfilActual")

            With eLogUsuarioReporteVO
                If .fechaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
                If .fechaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
                If Not IsNothing(.estado) Then iGeneradorSql.agregarCondicionWhere(IIf(.estado.id = Estado.ALTA, "l.idEstadoActual=" & Estado.ALTA, "l.idEstadoActual=" & Estado.BAJA))
                If .nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("u.nombre like '" & .nombre & "%'")
                If .login <> Nothing Then iGeneradorSql.agregarCondicionWhere("u.login like '" & .login & "%'")
            End With
            iGeneradorSql.agregarOrden("l.id")

            iGeneradorSql.agregarGroupBy("l.id")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "LogUsuarioReporte")

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
    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            If Not IsNothing(iUsuario) Then iUsuario.dispose()
            If Not IsNothing(iUsuarioModificador) Then iUsuarioModificador.dispose()

        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class

