Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Imports System.Configuration

Public Class RegistroUsuario

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iFechaEntrada As Date
    Private iFechaSalida As Date
    Private iUsuario As Usuario
    Private iIp As String

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

    Public Property fechaEntrada() As Date
        Get
            Return iFechaEntrada
        End Get
        Set(ByVal Value As Date)
            iFechaEntrada = Value
        End Set
    End Property

    Public Property fechaSalida() As Date
        Get
            Return iFechaSalida
        End Get
        Set(ByVal Value As Date)
            iFechaSalida = Value
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

    Public Property ip() As String
        Get
            Return iIp
        End Get
        Set(ByVal Value As String)
            iIp = Value
        End Set
    End Property

#End Region

#Region "Metodos"

    Private Sub validarCrear()

        If IsNothing(usuario) Then
            Throw New LogNoCreadoException("El usuario no puede ser nulo")
        End If

    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("RegistroUsuario")

            iGeneradorSql.agregarColumna("fechaEntrada")
            iGeneradorSql.agregarColumna("fechaSalida")
            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("ip")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingFechaHora(fechaEntrada))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingFechaHora(fechaSalida))
            iGeneradorSql.agregarValue(usuario.id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(ip))

            'iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

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

    Public Sub validarSesionUsuario()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("RegistroUsuario")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iUsuario.id)
            iGeneradorSql.agregarCondicionWhere("fechaSalida is null")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoCreadoException("El usuario tiene una sesión abierta, deberá espera a que finalice para poder ingresar")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New LogNoCreadoException(excepcion)
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

    Public Function obtenerUltimaFechaInicioSesion() As Date
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iUltimaFechaEntrada As Date
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("MAX(fechaEntrada) as ultimaFechaEntrada")
            iGeneradorSql.agregarTabla("RegistroUsuario")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iUsuario.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iUltimaFechaEntrada = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaFechaEntrada").ToString)
            End If
            iDataReader.Close()

            Return iUltimaFechaEntrada

        Catch excepcion As Exception
            Throw New LogNoCreadoException(excepcion)
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
    End Function

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("RegistroUsuario")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete)

        Catch excepcion As Exception
            Throw New LogNoEliminadoException(excepcion)
        Finally
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

            iGeneradorSql.agregarTabla("RegistroUsuario")

            iGeneradorSql.agregarSet("fechaEntrada=" & FuncionComun.nuloSiEsNothingFechaHora(fechaEntrada))
            iGeneradorSql.agregarSet("fechaSalida=" & FuncionComun.nuloSiEsNothingFechaHora(fechaSalida))

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate)

        Catch exception As Exception
            Throw New UsuarioNoModificadoException
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarCerrar()
        If (IsNothing(iUsuario) OrElse iUsuario.id = Nothing) AndAlso id = Nothing Then
            Throw New UsuarioNoModificadoException("No hay datos para cerrar sesión")
        End If
    End Sub

    Public Sub cerrar()
        Dim iGeneradorSql As New GeneradorSql
        Try

            validarCerrar()

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("RegistroUsuario")
            iGeneradorSql.agregarSet("fechaSalida=" & FuncionComun.nuloSiEsNothingFechaHora(Now))
            If Not IsNothing(iUsuario) AndAlso iUsuario.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idUsuario=" & iUsuario.id)
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            iGeneradorSql.agregarCondicionWhere("fechaSalida is null")

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New UsuarioNoModificadoException
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerRegistroUsuario() As RegistroUsuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("fechaEntrada")
            iGeneradorSql.agregarColumna("fechaSalida")
            iGeneradorSql.agregarColumna("ip")

            iGeneradorSql.agregarTabla("usuario")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iFechaEntrada = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaEntrada").ToString)
                iFechaSalida = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaSalida").ToString)
                iIp = iDataReader.Item("ip").ToString
                iUsuario = New Usuario
                iUsuario.id = iDataReader.Item("idUsuario").ToString

                iDataReader.Close()

                iUsuario.accesoDatos = iConexion
                iUsuario = iUsuario.obtenerUsuario
                iUsuario.accesoDatos = Nothing

                Return Me
            Else
                Throw New UsuarioNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
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

    Public Function obtenerRegistrosUsuariosGrilla(ByVal eRegistrosUsuariosGrillaVO As RegistrosUsuariosGrillaVO) As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iUsuarios, iPerfiles As String
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("ru.id")
            iGeneradorSql.agregarColumna("u.nombre")
            iGeneradorSql.agregarColumna("ru.fechaEntrada")
            iGeneradorSql.agregarColumna("ru.fechaSalida")
            iGeneradorSql.agregarColumna("ru.ip")
            iGeneradorSql.agregarColumna("case when ru.fechasalida is null then convert(" & FuncionComun.diferenciaHoras("'" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "'", "ru.fechaEntrada") & ",char) " &
                                         "else convert(" & FuncionComun.diferenciaHoras("ru.fechaSalida", "ru.fechaEntrada") & ",char) end as tiempoSesion")
            iGeneradorSql.agregarTabla("registroUsuario ru" &
                                        " inner join usuario u on u.id=ru.idUsuario")

            With eRegistrosUsuariosGrillaVO
                If .fechaEntradaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("ru.fechaEntrada>=" & FuncionComun.nuloSiEsNothing(.fechaEntradaDesde))
                If .fechaEntradaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("ru.fechaEntrada<=" & FuncionComun.nuloSiEsNothing(.fechaEntradaHasta))
                If .fechaSalidaDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("ru.fechaSalida>=" & FuncionComun.nuloSiEsNothing(.fechaSalidaDesde))
                If .fechaSalidaHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("ru.fechaSalida<=" & FuncionComun.nuloSiEsNothing(.fechaSalidaHasta))
                If Not IsNothing(.usuarios) AndAlso .usuarios.Count > 0 Then
                    iUsuarios = "("
                    For i = 1 To .usuarios.Count
                        iUsuarios &= .usuarios.Item(i).id & ","
                    Next
                    iUsuarios = Left(iUsuarios, Len(iUsuarios) - 1) & ")"
                    iGeneradorSql.agregarCondicionWhere("ru.idUsuario in " & iUsuarios)
                End If
                If Not IsNothing(.perfiles) AndAlso .perfiles.Count > 0 Then
                    iPerfiles = "("
                    For i = 1 To .perfiles.Count
                        iPerfiles &= .perfiles.Item(i).id & ","
                    Next
                    iPerfiles = Left(iPerfiles, Len(iPerfiles) - 1) & ")"
                    iGeneradorSql.agregarCondicionWhere("u.idPerfil in " & iPerfiles)
                End If
            End With

            iGeneradorSql.agregarOrden("ru.fechaEntrada desc")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "RegistrosUsuariosGrilla")

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
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

        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub

#End Region

End Class

