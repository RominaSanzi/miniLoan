Imports System.Collections.Generic
Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports CarlosAg.ExcelXmlWriter
Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.seguridad
Imports di.financiera.utils

Public Class AdministradorUsuarios

#Region "Metodos"

#Region "Usuario"
    Public Sub modificarMailFotoPerfil(eAccesoDatos As accesoDatos, eUsuario As Usuario)
        Try

            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.modificarMailFotoPerfil()

        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarMailFotoPerfil(eUsuario As Usuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos

            modificarMailFotoPerfil(iAccesoDatos, eUsuario)

        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarAccesosDirectosUsuario(eAccesoDatos As accesoDatos, eUsuario As Usuario)
        Try

            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.modificarAccesosDirectos()

        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarAccesosDirectosUsuario(eUsuario As Usuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos

            eUsuario.accesoDatos = iAccesoDatos
            eUsuario.modificarAccesosDirectos()

        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerUsuariosMail(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario) As DataSet
        Try
            eUsuario.accesoDatos = eAccesoDatos
            Return eUsuario.obtenerUsuariosMail()
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Function

    Public Sub crearUsuario(ByVal eUsuario As Usuario, eRolesAutorizados As String(), eAccesosDirectos As List(Of AccesoDirecto))
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eUsuario.accesoDatos = iAccesoDatos
            crearUsuario(iAccesoDatos, eUsuario, eRolesAutorizados, eAccesosDirectos)
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New UsuarioNoCreadoException(exception)
        Finally
            eUsuario.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario, eRolesAutorizados As String(), eAccesosDirectos As List(Of AccesoDirecto))
        Dim iRol As Rol
        Dim i As Integer
        Try
            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.accesosDirectos = eAccesosDirectos
            eUsuario.crear()
            'Si es copia de usuario habilito los roles que tiene habilitado el usuario 
            If Not IsNothing(eRolesAutorizados) Then
                For i = 0 To eRolesAutorizados.Length - 1
                    iRol = New Rol
                    With iRol
                        .valor = True
                        .usuario = eUsuario
                        .descripcion = eRolesAutorizados(i)
                        .accesoDatos = eAccesoDatos
                        .habilitarRolPorDescripcion()
                        .accesoDatos = Nothing
                    End With
                Next
            End If

        Catch exception As Exception
            Throw New UsuarioNoCreadoException(exception)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarUsuario(ByVal eUsuario As Usuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eUsuario.accesoDatos = iAccesoDatos
            eliminarUsuario(iAccesoDatos, eUsuario)
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New UsuarioNoEliminadoException(exception)
        Finally
            eUsuario.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)
        Try
            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.eliminar()
        Catch exception As Exception
            Throw New UsuarioNoEliminadoException(exception)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarUsuario(ByVal eUsuario As Usuario, eUsuarioAccion As Usuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            eUsuario.accesoDatos = iAccesoDatos
            guardarCambiosUsuario(iAccesoDatos, eUsuario, eUsuarioAccion)
            eUsuario.modificar()
        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub guardarCambiosUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eUsuarioNuevo As Usuario, ByVal eUsuarioAccion As Usuario)
        Dim iUsuarioViejo As Usuario
        Dim iLogUsuario As LogUsuario
        Try
            '/// verifico si tengo que guardar el historial de domicilio
            iUsuarioViejo = New Usuario
            iUsuarioViejo.accesoDatos = eAccesoDatos
            iUsuarioViejo.id = eUsuarioNuevo.id
            iUsuarioViejo = obtenerUsuario(eAccesoDatos, iUsuarioViejo)
            If iUsuarioViejo.validaCambioDatos(eUsuarioNuevo) Then
                iLogUsuario = New LogUsuario
                With iLogUsuario
                    .usuarioModificador = eUsuarioAccion
                    .usuario = eUsuarioNuevo
                    .fecha = Today
                    .hora = Now.TimeOfDay
                    .loginActual = eUsuarioNuevo.login
                    .perfilesActuales = eUsuarioNuevo.perfiles
                    .estadoActual = eUsuarioNuevo.estado
                    .estadoUsuarioActual = eUsuarioNuevo.estadoUsuario

                    .loginAnterior = iUsuarioViejo.login
                    .perfilesAnteriores = iUsuarioViejo.perfiles
                    .estadoAnterior = iUsuarioViejo.estado
                    .estadoUsuarioAnterior = iUsuarioViejo.estadoUsuario

                    crearLogUsuario(eAccesoDatos, iLogUsuario)
                End With
            End If



        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            iUsuarioViejo = Nothing
            iLogUsuario = Nothing
        End Try
    End Sub

    Public Sub cambiarPasswordUsuario(ByVal eUsuario As Usuario)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            cambiarPasswordUsuario(iAccesoDatos, eUsuario)
        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub cambiarPasswordUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)
        Try

            validarCambioPassword(eAccesoDatos, eUsuario)

            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.cambiarPassword()
        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub
    Public Sub logoutUsuario(ByVal eUsuario As Usuario)
        Dim iAccesoDatos As accesoDatos
        Dim iRegistroUsuario As New RegistroUsuario

        Try
            iAccesoDatos = New accesoDatos
            iRegistroUsuario.usuario = eUsuario
            cerrarRegistroUsuario(iAccesoDatos, iRegistroUsuario)

        Catch Exception As Exception
            Throw Exception
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            iRegistroUsuario = Nothing
        End Try
    End Sub
    Public Sub modificarUsuarioPerfil(ByVal eUsuario As Usuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            modificarMailFotoPerfil(iAccesoDatos, eUsuario)
            modificarAccesosDirectosUsuario(iAccesoDatos, eUsuario)
            iAccesoDatos.commit()
        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            iAccesoDatos.rollback()
            Throw UsuarioNoModificadoException
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Function validarUsuario(ByVal eUsuario As Usuario, ByVal eIpAcceso As String(), Optional eLoginWeb As Boolean = False) As Usuario
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            Return validarUsuario(iAccesoDatos, eUsuario, eIpAcceso, eLoginWeb)

        Catch Exception As Exception
            '*** Si la excepcion es por haber errador el password valido la cantidad de intentos erroneos que tuvo ****
            If Not IsNothing(iAccesoDatos) AndAlso TypeOf (Exception) Is UsuarioClaveErroneaException Then validarCantidadIntentosIngresoPasswordPorLogin(iAccesoDatos, eUsuario)
            Throw Exception
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function validarUsuario(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario, ByVal eIpAcceso As String(), Optional eLoginWeb As Boolean = False) As Usuario
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iRegistroUsuario As New RegistroUsuario
        Dim iIpAccesoUsuario As String()
        Dim iParametro As Parametro
        Dim iVersion As New Version
        Dim iHabilitado As Boolean
        Dim iUltimaFechaEntrada As Date

        Try

            eUsuario.accesoDatos = eAccesoDatos

            'validar version sistema
            FuncionComun.obtenerComun(Today, True, eAccesoDatos)
            FuncionComun.verMarcaComun(eAccesoDatos)

            'Actualizo la version del sistema solo para WEB, sino valido la version
            If eLoginWeb Then
                iVersion.accesoDatos = eAccesoDatos
                iVersion.crear()
                iVersion.accesoDatos = Nothing
            Else
                FuncionComun.validarVersionSistema(eAccesoDatos)
            End If

            If eUsuario.id <> Nothing Then
                eUsuario = obtenerUsuario(eAccesoDatos, eUsuario)
            Else
                eUsuario = obtenerUsuarioPorNombreYPass(eAccesoDatos, eUsuario)

                If Not eUsuario.pedirCambioPassword Then

                    'Valido, si esta activado, la sesión por Usuario
                    If Not IsNothing(ConfigurationManager.AppSettings("loginUsuariosEstricto")) AndAlso CBool(ConfigurationManager.AppSettings("loginUsuariosEstricto")) Then
                        With iRegistroUsuario
                            .usuario = eUsuario
                            .fechaEntrada = Now
                        End With
                        validarSesionUsuario(eAccesoDatos, iRegistroUsuario)
                    End If

                    'Se crear el registro de usuario
                    With iRegistroUsuario
                        .usuario = eUsuario
                        .fechaEntrada = Now
                        If Not IsNothing(eIpAcceso) AndAlso Not IsNothing(eIpAcceso(0)) Then .ip = eIpAcceso(0)
                    End With
                    crearRegistroUsuario(eAccesoDatos, iRegistroUsuario) '''''''''
                End If

            End If

            iHabilitado = True

            If Not IsNothing(eUsuario) Then
                With eUsuario
                    'Primero me fijo si no está entrando desde una ip restringida
                    If .ipRestringida <> Nothing Then
                        iIpAccesoUsuario = Split(.ipRestringida, ".")
                        iHabilitado = Not (iIpAccesoUsuario(0) = eIpAcceso(0) And iIpAccesoUsuario(1) = eIpAcceso(1) And iIpAccesoUsuario(2) = eIpAcceso(2) And iIpAccesoUsuario(3) = eIpAcceso(3))
                    End If

                    If iHabilitado Then
                        'Si encontré en la base al usuario valido que pueda entrar en la fecha y hora y que tenga el medio de acceso habilitado
                        Select Case Today.DayOfWeek
                            Case DayOfWeek.Monday
                                iHabilitado = DateDiff(DateInterval.Second, .lunesHoraLoguinDesde, CDate(Format(Now, "HH:mm:ss"))) > 0 AndAlso
                                DateDiff(DateInterval.Second, CDate(Format(Now, "HH:mm:ss")), .lunesHoraLoguinHasta) > 0
                            Case DayOfWeek.Tuesday
                                iHabilitado = DateDiff(DateInterval.Second, .martesHoraLoguinDesde, CDate(Format(Now, "HH:mm:ss"))) > 0 AndAlso
                                DateDiff(DateInterval.Second, CDate(Format(Now, "HH:mm:ss")), .martesHoraLoguinHasta) > 0
                            Case DayOfWeek.Wednesday
                                iHabilitado = DateDiff(DateInterval.Second, .miercolesHoraLoguinDesde, CDate(Format(Now, "HH:mm:ss"))) > 0 AndAlso
                                DateDiff(DateInterval.Second, CDate(Format(Now, "HH:mm:ss")), .miercolesHoraLoguinHasta) > 0
                            Case DayOfWeek.Thursday
                                iHabilitado = DateDiff(DateInterval.Second, .juevesHoraLoguinDesde, CDate(Format(Now, "HH:mm:ss"))) > 0 AndAlso
                                DateDiff(DateInterval.Second, CDate(Format(Now, "HH:mm:ss")), .juevesHoraLoguinHasta) > 0
                            Case DayOfWeek.Friday
                                iHabilitado = DateDiff(DateInterval.Second, .viernesHoraLoguinDesde, CDate(Format(Now, "HH:mm:ss"))) > 0 AndAlso
                                DateDiff(DateInterval.Second, CDate(Format(Now, "HH:mm:ss")), .viernesHoraLoguinHasta) > 0
                            Case DayOfWeek.Saturday
                                iHabilitado = DateDiff(DateInterval.Second, .sabadoHoraLoguinDesde, CDate(Format(Now, "HH:mm:ss"))) > 0 AndAlso
                                DateDiff(DateInterval.Second, CDate(Format(Now, "HH:mm:ss")), .sabadoHoraLoguinHasta) > 0
                            Case DayOfWeek.Sunday
                                iHabilitado = DateDiff(DateInterval.Second, .domingoHoraLoguinDesde, CDate(Format(Now, "HH:mm:ss"))) > 0 AndAlso
                                DateDiff(DateInterval.Second, CDate(Format(Now, "HH:mm:ss")), .domingoHoraLoguinHasta) > 0
                        End Select
                    Else
                        Throw New UsuarioNoEncontradoException("El usuario está intentando ingresar desde una ip restringida")
                    End If
                    'Si está habilitado en cuanto horario reviso el medio de acceso
                    '**Ips de red local**
                    'Clase A 10.0.0.0 10.255.255.255 255.0.0.0 
                    'Clase B 172.16.0.0 172.31.255.255 255.255.0.0 
                    'Clase C 192.168.0.0 192.168.255.255 255.255.255.0 

                    If iHabilitado Then
                        Select Case .medioAccesoSistema.id
                            Case MedioAccesoSistema.REDLOCAL
                                'valido que la ip sea de una red local
                                Select Case eIpAcceso(0)
                                    Case 10
                                        ''todo el rango que empieza con 10 está habilitado
                                        iHabilitado = True
                                    Case 127
                                        'direccion de loopback
                                        iHabilitado = (eIpAcceso(1) = 0 And eIpAcceso(2) = 0 And eIpAcceso(3) = 1)
                                    Case 172
                                        'si empieza con 172 tiene que seguir entre 16 y 31
                                        iHabilitado = (eIpAcceso(1) >= 16 And eIpAcceso(1) >= 31)
                                    Case 192
                                        'si empieza con 192 y sigue con 168 -> está habilitado
                                        iHabilitado = (eIpAcceso(1) = 168)
                                    Case "::1"  ' ::1 is the IPv6 loopback address. Equivalent to 127.0.0.1 for IPv4.
                                        iHabilitado = True
                                    Case Else
                                        iHabilitado = False
                                End Select
                            Case MedioAccesoSistema.INTERNET
                                'valido que la ip sea de internet
                                Select Case eIpAcceso(0)
                                    Case 10
                                        iHabilitado = False
                                    Case 127
                                        iHabilitado = Not (eIpAcceso(1) = 0 And eIpAcceso(2) = 0 And eIpAcceso(3) = 1)
                                    Case 172
                                        iHabilitado = (eIpAcceso(1) < 16 And eIpAcceso(1) > 31)
                                    Case 192
                                        iHabilitado = (eIpAcceso(1) <> 168)
                                    Case Else
                                        iHabilitado = True
                                End Select
                            Case MedioAccesoSistema.IPACCESO
                                If .ipAcceso <> "" Then
                                    iIpAccesoUsuario = Split(.ipAcceso, ".")
                                    iHabilitado = (iIpAccesoUsuario(0) = eIpAcceso(0) And iIpAccesoUsuario(1) = eIpAcceso(1) And iIpAccesoUsuario(2) = eIpAcceso(2) And iIpAccesoUsuario(3) = eIpAcceso(3))
                                Else
                                    iHabilitado = False
                                End If
                            Case MedioAccesoSistema.TODOS
                                iHabilitado = True
                        End Select
                    Else
                        Throw New UsuarioNoEncontradoException("El usuario no está habilitado a entrar en el sistema en este día y horario")
                    End If
                End With
            End If

            If Not iHabilitado Then Throw New UsuarioNoEncontradoException("El usuario no está habilitado a entrar en el sistema con esa ip")

            '*** blanqueo la cantidad de intentos erroneos de ingreso de password porque la ingreso bien***
            vaciarCantidadIntentosIngreso(eAccesoDatos, eUsuario)

            '=== Valido la si permite vencimiento de contraseña y la cantidad de dias de vencimiento
            iParametro = New Parametro
            iParametro.descripcion = "HabilitarVencimientoContrasenia"
            iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, eUsuario.nivel)

            If FuncionComun.byteBoolean(iParametro.valor) Then

                iParametro = New Parametro
                iParametro.descripcion = "DiasCambioContraseña"
                iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, eUsuario.nivel)
                If eUsuario.fechaUltimoCambioContraseña.AddDays(iParametro.valor) <= Now.Date Then eUsuario.pedirCambioPassword = True
            End If

            '=== Valido el bloqueo de usuario por cantidad de dias ===

            iParametro = New Parametro
            iParametro.descripcion = "CantidadDiasBloqueoUsuario"
            iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, eUsuario.nivel)

            If FuncionComun.ceroSiEsNulo(iParametro.valor) <> 0 Then

                With iRegistroUsuario
                    .usuario = eUsuario
                    .fechaEntrada = Now
                End With
                iUltimaFechaEntrada = obtenerUltimaFechaInicioSesion(eAccesoDatos, iRegistroUsuario)

                If iUltimaFechaEntrada.AddDays(iParametro.valor) <= Now.Date Then
                    eUsuario.deshabilitarUsuario()
                    Throw New UsuarioNoEncontradoException("El usuario no está habilitado a entrar, se encuentra en estado bloqueado.")
                End If
            End If

            '=== Valido el bloqueo de usuario por cantidad de dias ===

            iParametro = New Parametro
            iParametro.descripcion = "CantidadDiasBajaUsuario"
            iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, eUsuario.nivel)

            If FuncionComun.ceroSiEsNulo(iParametro.valor) <> 0 Then

                With iRegistroUsuario
                    .usuario = eUsuario
                    .fechaEntrada = Now
                End With
                iUltimaFechaEntrada = obtenerUltimaFechaInicioSesion(eAccesoDatos, iRegistroUsuario)

                If iUltimaFechaEntrada.AddDays(iParametro.valor) <= Now.Date Then
                    eUsuario.bajaUsuario()
                    Throw New UsuarioNoEncontradoException("El usuario no está habilitado a entrar, se encuentra en estado de baja.")
                End If
            End If

            Return eUsuario

        Catch Exception As Exception
            '*** Si la excepcion es por haber errador el password valido la cantidad de intentos erroneos que tuvo ****
            If Not IsNothing(eAccesoDatos) AndAlso TypeOf (Exception) Is UsuarioClaveErroneaException Then validarCantidadIntentosIngresoPasswordPorLogin(eAccesoDatos, eUsuario)
            Throw Exception
        Finally
            If Not IsNothing(eUsuario) Then eUsuario.accesoDatos = Nothing
            iAdministradorParametros = Nothing
            iParametro = Nothing
        End Try
    End Function

    Public Sub modificarValidacionMail(ByVal eAccesoDatos As accesoDatos, ByVal eUsuarioAutogestion As UsuarioAutogestion)
        Try
            eUsuarioAutogestion.accesoDatos = eAccesoDatos
            eUsuarioAutogestion.modificarValidacionMail()

        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            eUsuarioAutogestion.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub validarCantidadIntentosIngresoPasswordPorLogin(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)
        Dim iGrupoEmpresas As New GrupoEmpresas
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iParametro As New Parametro

        Try

            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.sumarCantidadIntentosIngresoPasswordPorLogin()
            eUsuario.accesoDatos = Nothing

            If eUsuario.id <> Nothing Then
                iGrupoEmpresas.id = GrupoEmpresas.GRUPOEMPRESAS
                iParametro.descripcion = "CantidadReintentosIngresoPassword"
                iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, iGrupoEmpresas)
                If eUsuario.intentosIngresoPassword > iParametro.valor Then deshabilitarUsuario(eAccesoDatos, eUsuario)
            End If

        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            eUsuario.accesoDatos = Nothing
            iAdministradorParametros = Nothing
            iParametro = Nothing
        End Try
    End Sub

    Public Sub deshabilitarUsuario(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)

        Try
            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.deshabilitarUsuario()
        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub bajaUsuario(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)

        Try
            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.bajaUsuario()
        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub vaciarCantidadIntentosIngreso(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)

        Try
            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.vaciarCantidadIntentosIngreso()
        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Private Function obtenerUsuarioPorNombreYPass(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario) As Usuario
        Try

            eUsuario.accesoDatos = eAccesoDatos
            eUsuario = eUsuario.obtenerUsuarioPorNombreYPass
            eUsuario.accesoDatos = Nothing

            Return eUsuario

        Catch Exception As Exception
            Throw Exception
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerUsuarioPedirCambioPassword(ByVal eUsuario As Usuario) As Usuario
        Try
            Return eUsuario.obtenerUsuarioPedirCambioPassword
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerUsuarioSoloIds(eUsuario As Usuario, Optional eObtenerNivel As Boolean = False) As Usuario
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            Return obtenerUsuarioSoloIds(iAccesoDatos, eUsuario, eObtenerNivel:=eObtenerNivel)

        Catch ex As Exception
            Throw New UsuarioNoEncontradoException(ex)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try

    End Function

    Public Function obtenerUsuarioSoloIds(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario, Optional eObtenerNivel As Boolean = False) As Usuario
        Try
            eUsuario.accesoDatos = eAccesoDatos
            Return eUsuario.obtenerUsuarioSoloIds(eObtenerNivel:=eObtenerNivel)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerUsuario(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario) As Usuario
        Try
            eUsuario.accesoDatos = eAccesoDatos
            eUsuario = eUsuario.obtenerUsuario
            eUsuario.accesoDatos = Nothing

            Return eUsuario
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerUsuario(ByVal eUsuario As Usuario) As Usuario
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos

            Return obtenerUsuario(iAccesoDatos, eUsuario)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerUsuarios(ByVal eUsuario As Usuario) As IDataReader
        Try
            Return eUsuario.obtenerUsuarios()
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerUsuarios(ByVal eUsuario As Usuario, ByVal eNivel As Nivel) As IDataReader
        Try
            Return eUsuario.obtenerUsuarios(eNivel)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerUsuariosGrilla(ByVal eUsuariosGrillaVO As UsuariosGrillaVO) As DataSet
        Dim iUsuario As New Usuario
        Try
            Return iUsuario.obtenerUsuariosGrilla(eUsuariosGrillaVO)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            iUsuario = Nothing
        End Try
    End Function

    Public Function obtenerUsuariosReporte(ByVal eUsuariosReporteVO As UsuariosReporteVO) As String
        Dim iUsuario As New Usuario
        Try
            Return iUsuario.obtenerUsuariosReporte(eUsuariosReporteVO)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            iUsuario = Nothing
        End Try
    End Function
    Public Sub activarUsuario(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)
        Try

            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.activarUsuario()

        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarCelularValidado(eAccesoDatos As accesoDatos, ByVal eUsuarioAutogestion As UsuarioAutogestion)
        Try

            eUsuarioAutogestion.accesoDatos = eAccesoDatos
            eUsuarioAutogestion.modificarCelularValidado()

        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            eUsuarioAutogestion.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarCBUValidado(eAccesoDatos As accesoDatos, ByVal eUsuarioAutogestion As UsuarioAutogestion)
        Try

            eUsuarioAutogestion.accesoDatos = eAccesoDatos
            eUsuarioAutogestion.modificarCBUValidado()

        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            eUsuarioAutogestion.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarIdentidadValidada(eAccesoDatos As accesoDatos, ByVal eUsuarioAutogestion As UsuarioAutogestion)
        Try

            eUsuarioAutogestion.accesoDatos = eAccesoDatos
            eUsuarioAutogestion.modificarIdentidadValidada()

        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            eUsuarioAutogestion.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarPaginaDireccionar(eAccesoDatos As accesoDatos, ByVal eUsuarioAutogestion As UsuarioAutogestion)
        Try

            eUsuarioAutogestion.accesoDatos = eAccesoDatos
            eUsuarioAutogestion.modificarPaginaDireccionar()

        Catch Exception As Exception
            Throw New UsuarioNoModificadoException(Exception)
        Finally
            eUsuarioAutogestion.accesoDatos = Nothing
        End Try
    End Sub

    'Public Function generarToken(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario) As String
    '    Dim iRandom(1) As Integer
    '    Dim iCounter As Integer
    '    Dim iRandNumber As New Random
    '    Dim iParametro As New Parametro
    '    Dim iAdministradorParametros As New AdministradorParametros
    '    Dim iAdministradorSMS As New AdministradorSMS
    '    Dim iEnvioSMSVO As New EnvioSMSVO

    '    Try

    '        eUsuario.accesoDatos = eAccesoDatos

    '        'si no tiene token, o ya esta vencido genero uno nuevo, sino devuelvo el existente
    '        If eUsuario.token = Nothing OrElse eUsuario.fechaCaducidadToken = Nothing OrElse eUsuario.fechaCaducidadToken < Now Then

    '            'Genero el token
    '            '=================================================================================================
    '            For iCounter = 0 To 1
    '                iRandom(iCounter) = iRandNumber.Next(100)
    '            Next iCounter
    '            Array.Sort(iRandom)

    '            iParametro.descripcion = "DuracionTokenDobleFactor"
    '            iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, eUsuario.nivel)
    '            eUsuario.fechaCaducidadToken = Now.AddMinutes(iParametro.valor)

    '            eUsuario.token = (iRandom(0) & iRandom(1)).ToString.PadRight(4, "0")

    '            eUsuario.modificarToken()

    '        End If

    '        With iEnvioSMSVO
    '            .telefono = eUsuario.telefonoCelularCodigoArea & eUsuario.telefonoCelularCaracteristica & eUsuario.telefonoCelularNumero
    '            .mensaje = "Su token para ingresar es: " & eUsuario.token
    '            .datosReemplazar.Add("TOKEN", eUsuario.token)
    '            .tipoMensajeEnvioSMS = FuncionComun.enumTipoMensajeEnvioSMS.RECUPEROCONTRASENIA
    '            .nivel = eUsuario.nivel
    '            .tipoEnvioSMS = FuncionComun.enumTipoEnvioSMS.SINDEFINIR
    '        End With

    '        iAdministradorSMS.envioPuntualSMS(eAccesoDatos, iEnvioSMSVO)

    '        Return eUsuario.token

    '    Catch Exception As Exception
    '        Throw New UsuarioNoModificadoException(Exception)
    '    Finally
    '        iEnvioSMSVO = Nothing
    '        iAdministradorSMS = Nothing
    '        iAdministradorParametros = Nothing
    '        eUsuario.accesoDatos = Nothing
    '    End Try
    'End Function
#End Region

#Region "Menu"

    ''//makeMenu('TYPE','TEXT','LINK','TARGET', 'END (THE LAST MENU)')
    Private STRING_NODO As String = "makeMenu('<nivel>','<nombre>','<url>','basefrm')"
    Private iCabecera As String = "preLoadBackgrounds(level0_regular,level0_round,level1_regular,level1_round,level1_sub,level1_sub_round,level1_round2,level2_regular,level2_round)"
    Private iPie As String = "onload=SlideMenuInit;"
    Private iCodigo As String

    Private STRING_NODOTOP As String
    Private STRING_NODOSUB As String
    Private STRING_NODOHIJO As String
    Private STRING_NODOHIJO2 As String
    Private STRING_NODOCIERRE As String
    Private STRING_NODOCIERRETOP As String
    Private STRING_ICONO As String


    Public Function obtenerMenu(ByVal ePerfiles As List(Of Perfil)) As Menu
        Dim iAccesoDatos As New accesoDatos()
        Dim iPerfil As New Perfil

        Try
            iPerfil.accesoDatos = iAccesoDatos
            Return iPerfil.obtenerMenu(ePerfiles, Menu.EnumTipoSistema.LOAN)
        Catch exception As Exception
            Throw New MenuNoEncontradoException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            iPerfil.accesoDatos = Nothing
        End Try

    End Function

    Public Function obtenerMenuResponsivo(ByVal ePerfiles As List(Of Perfil)) As Menu
        Dim iAccesoDatos As New accesoDatos()
        Dim iPerfil As New Perfil

        Try
            iPerfil.accesoDatos = iAccesoDatos
            Return iPerfil.obtenerMenuResponsivo(ePerfiles)
        Catch exception As Exception
            Throw New MenuNoEncontradoException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            iPerfil.accesoDatos = Nothing
        End Try

    End Function

    Public Function presentarMenu(ByVal ePerfiles As List(Of Perfil)) As String
        Dim iMenu As Menu

        Try
            iMenu = obtenerMenu(ePerfiles)
            iCodigo = mostrarMenuCompuesto(iMenu)
            If iCodigo <> Nothing Then iCodigo = iCabecera & vbNewLine & iCodigo & vbNewLine & iPie

            Return iCodigo

        Catch exception As Exception
            Throw New MenuNoEncontradoException(exception)
        End Try
    End Function

    Private Function mostrarMenuCompuesto(ByVal eMenu As Menu) As String
        Dim iMenu As MenuCompuesto
        Dim iHijo As Menu

        Try
            iMenu = eMenu
            For Each iHijo In iMenu.menues
                If iHijo.isMenuCompuesto Then
                    iCodigo = iCodigo & STRING_NODO.Replace("<nombre>", iHijo.nombre).Replace("<url>", "").Replace("<nivel>", iHijo.nivel) & vbNewLine
                    mostrarMenuCompuesto(iHijo)
                Else
                    Dim iMenuSimple As MenuSimple
                    iMenuSimple = iHijo
                    iCodigo = iCodigo & STRING_NODO.Replace("<nombre>", iMenuSimple.nombre).Replace("<url>", iMenuSimple.accion.paginaAsociada).Replace("<nivel>", iMenuSimple.nivel) & vbNewLine
                End If
            Next
            Return iCodigo
        Catch exception As Exception
            Throw New MenuNoEncontradoException(exception)
        End Try
    End Function

    Public Function puedeAcceder(ByVal eUsuario As Usuario, ByVal eNombrePagina As String) As Boolean
        Try
            Return eUsuario.puedeAcceder(eNombrePagina)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        End Try
    End Function

    Public Function puede(ByVal eUsuario As Usuario, ByVal eRol As String) As Boolean
        Try
            Return eUsuario.puede(eRol)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        End Try
    End Function

    Public Function presentarMenuResponsivo(ByVal ePerfiles As List(Of Perfil)) As String
        Dim iMenu As MenuCompuesto
        Dim iTextInfo As TextInfo = New CultureInfo("es-AR", False).TextInfo

        Try

            STRING_ICONO = "<i class='now-ui-icons <icono>'></i>"

            STRING_NODOTOP = "<li>" & vbNewLine
            STRING_NODOTOP &= "  <a data-toggle='collapse' href='#c<id>' class='<classnivel> '><p><conicono><nombre><b class='caret'></b></p><p style='display: none;'><oculto></p></a>" & vbNewLine
            STRING_NODOTOP &= "  <div class='collapse ' id='c<id>'><ul class='nav sangria'>" & vbNewLine

            STRING_NODOSUB = "  <li>" & vbNewLine
            STRING_NODOSUB &= "  <a data-toggle='collapse' href='#c<id>' class='<classnivel> '><p><conicono><nombre><b class='caret'></b></p><p style='display: none;'><oculto></p></a>" & vbNewLine
            STRING_NODOSUB &= "  <div class='collapse ' id='c<id>'><ul class='nav sangria'>" & vbNewLine

            STRING_NODOHIJO = "    <li>" & vbNewLine
            STRING_NODOHIJO &= "    <a href='<url>' target='basefrm' data-toggle='tooltip' title='<tooltip>' onclick='marcarActivo(this);' class='<classnivel> '><span class='sidebar-mini-icon'><nombreCorto></span><span class='sidebar-normal'><nombre></span><p style='display: none;'><oculto></p></a>" & vbNewLine
            STRING_NODOHIJO &= "    </li>" & vbNewLine

            STRING_NODOHIJO2 = "    <li>" & vbNewLine
            STRING_NODOHIJO2 &= "    <a href='<url>' target='basefrm' data-toggle='tooltip' title='<tooltip>' onclick='marcarActivo(this);' class='<classnivel> '><span class='sidebar-mini-icon'><nombreCorto></span><span class='sidebar-normal'><nombre></span><p style='display: none;'><oculto></p></a>" & vbNewLine
            STRING_NODOHIJO2 &= "    </li>" & vbNewLine

            STRING_NODOCIERRE = "  </ul>" & vbNewLine
            STRING_NODOCIERRE &= "  </div>" & vbNewLine
            STRING_NODOCIERRE &= "  </li>" & vbNewLine

            iMenu = obtenerMenuResponsivo(ePerfiles)

            ' se agrega un <p> oculto que es la concatenacion de todo el arbol hasta el nodo para la busqueda
            For Each iHijo As Menu In iMenu.menues
                If iHijo.isMenuCompuesto AndAlso tieneMenuFinal(iHijo) Then
                    iCodigo = iCodigo & STRING_NODOTOP.Replace("<nombreCorto>", Left(iHijo.nombre, 1)).Replace("<nombre>", iHijo.nombre).Replace("<id>", iHijo.id).Replace("<classnivel>", "Nivel1").Replace("<oculto>", iTextInfo.ToTitleCase(iHijo.nombre))
                    If iHijo.icono <> Nothing Then
                        iCodigo = iCodigo.Replace("<conicono>", STRING_ICONO).Replace("<icono>", iHijo.icono)
                    Else
                        iCodigo = iCodigo.Replace("<conicono>", "")
                    End If

                    For Each iHijo2 As Menu In CType(iHijo, MenuCompuesto).menues

                        If iHijo2.isMenuCompuesto AndAlso tieneMenuFinal(iHijo2) Then
                            iCodigo = iCodigo & STRING_NODOSUB.Replace("<nombreCorto>", Left(iHijo2.nombre, 1)).Replace("<nombre>", iHijo2.nombre).Replace("<id>", iHijo2.id).Replace("<oculto>", iTextInfo.ToTitleCase(iHijo.nombre & " " & iHijo2.nombre)).Replace("<classnivel>", "Nivel2") '.Replace(" ", String.Empty))
                            If iHijo2.icono <> Nothing Then
                                iCodigo = iCodigo.Replace("<conicono>", STRING_ICONO).Replace("<icono>", iHijo2.icono)
                            Else
                                iCodigo = iCodigo.Replace("<conicono>", "")
                            End If
                            For Each iHijo3 As Menu In CType(iHijo2, MenuCompuesto).menues

                                If iHijo3.isMenuCompuesto AndAlso tieneMenuFinal(iHijo3) Then
                                    iCodigo = iCodigo & STRING_NODOSUB.Replace("<nombreCorto>", Left(iHijo3.nombre, 1)).Replace("<nombre>", iHijo3.nombre).Replace("<id>", iHijo3.id).Replace("<oculto>", iTextInfo.ToTitleCase(iHijo.nombre & " " & iHijo2.nombre & " " & iHijo3.nombre)).Replace("<classnivel>", "Nivel3") '.Replace(" ", String.Empty))
                                    If iHijo3.icono <> Nothing Then
                                        iCodigo = iCodigo.Replace("<conicono>", STRING_ICONO).Replace("<icono>", iHijo3.icono)
                                    Else
                                        iCodigo = iCodigo.Replace("<conicono>", "")
                                    End If
                                    For Each iHijo4 As Menu In CType(iHijo3, MenuCompuesto).menues
                                        If iHijo4.isMenuCompuesto Then
                                            iCodigo = iCodigo & STRING_NODOSUB.Replace("<nombreCorto>", Left(iHijo4.nombre, 1)).Replace("<nombre>", iHijo4.nombre).Replace("<id>", iHijo4.id).Replace("<oculto>", iTextInfo.ToTitleCase(iHijo.nombre & " " & iHijo2.nombre & " " & iHijo3.nombre)).Replace("<classnivel>", "Nivel3") '.Replace(" ", String.Empty))
                                            If iHijo4.icono <> Nothing Then
                                                iCodigo = iCodigo.Replace("<conicono>", STRING_ICONO).Replace("<icono>", iHijo4.icono)
                                            Else
                                                iCodigo = iCodigo.Replace("<conicono>", "")
                                            End If
                                        Else
                                            Dim iMenuSimple As MenuSimple
                                            iMenuSimple = iHijo4
                                            iCodigo = iCodigo & STRING_NODOHIJO.Replace("<nombreCorto>", Left(iMenuSimple.nombre, 1)).Replace("<nombre>", iMenuSimple.nombre).Replace("<url>", iMenuSimple.accion.paginaAsociada).Replace("<oculto>", iTextInfo.ToTitleCase(iHijo.nombre & " " & iHijo2.nombre & " " & iHijo3.nombre & " " & iHijo4.nombre)).Replace("<classnivel>", "Nivel4") '.Replace(" ", String.Empty))
                                            If iHijo4.icono <> Nothing Then
                                                iCodigo = iCodigo.Replace("<conicono>", STRING_ICONO).Replace("<icono>", iHijo4.icono)
                                            Else
                                                iCodigo = iCodigo.Replace("<conicono>", "")
                                            End If
                                            If iHijo4.tooltip <> Nothing Then
                                                iCodigo = iCodigo.Replace("<tooltip>", iHijo4.tooltip)
                                            Else
                                                iCodigo = iCodigo.Replace("<tooltip>", "")
                                            End If
                                        End If
                                        If iHijo4.isMenuCompuesto Then iCodigo = iCodigo & STRING_NODOCIERRE
                                    Next

                                ElseIf iHijo3.isMenuSimple() Then
                                    Dim iMenuSimple As MenuSimple
                                    iMenuSimple = iHijo3
                                    iCodigo = iCodigo & STRING_NODOHIJO.Replace("<nombreCorto>", Left(iMenuSimple.nombre, 1)).Replace("<nombre>", iMenuSimple.nombre).Replace("<url>", iMenuSimple.accion.paginaAsociada).Replace("<oculto>", iTextInfo.ToTitleCase(iHijo.nombre & " " & iHijo2.nombre & " " & iHijo3.nombre)).Replace("<classnivel>", "Nivel3") '.Replace(" ", String.Empty))
                                    If iHijo3.icono <> Nothing Then
                                        iCodigo = iCodigo.Replace("<conicono>", STRING_ICONO).Replace("<icono>", iHijo3.icono)
                                    Else
                                        iCodigo = iCodigo.Replace("<conicono>", "")
                                    End If
                                    If iHijo3.tooltip <> Nothing Then
                                        iCodigo = iCodigo.Replace("<tooltip>", iHijo3.tooltip)
                                    Else
                                        iCodigo = iCodigo.Replace("<tooltip>", "")
                                    End If
                                End If
                                If iHijo3.isMenuCompuesto AndAlso tieneMenuFinal(iHijo3) Then iCodigo = iCodigo & STRING_NODOCIERRE

                            Next

                        ElseIf iHijo2.isMenuSimple() Then
                            Dim iMenuSimple As MenuSimple
                            iMenuSimple = iHijo2
                            iCodigo = iCodigo & STRING_NODOHIJO.Replace("<nombreCorto>", Left(iMenuSimple.nombre, 1)).Replace("<nombre>", iMenuSimple.nombre).Replace("<url>", iMenuSimple.accion.paginaAsociada).Replace("<oculto>", iTextInfo.ToTitleCase(iHijo.nombre & " " & iHijo2.nombre)).Replace("<classnivel>", "Nivel2") '.Replace(" ", String.Empty))
                            If iHijo2.icono <> Nothing Then
                                iCodigo = iCodigo.Replace("<conicono>", STRING_ICONO).Replace("<icono>", iHijo2.icono)
                            Else
                                iCodigo = iCodigo.Replace("<conicono>", "")
                            End If
                            If iHijo2.tooltip <> Nothing Then
                                iCodigo = iCodigo.Replace("<tooltip>", iHijo2.tooltip)
                            Else
                                iCodigo = iCodigo.Replace("<tooltip>", "")
                            End If
                        End If
                        If iHijo2.isMenuCompuesto AndAlso tieneMenuFinal(iHijo2) Then iCodigo = iCodigo & STRING_NODOCIERRE
                    Next

                ElseIf iHijo.isMenuSimple() Then
                    Dim iMenuSimple As MenuSimple
                    iMenuSimple = iHijo
                    iCodigo = iCodigo & STRING_NODOHIJO.Replace("<nombreCorto>", Left(iMenuSimple.nombre, 1)).Replace("<nombre>", iMenuSimple.nombre).Replace("<url>", iMenuSimple.accion.paginaAsociada).Replace("<classnivel>", "Nivel1")
                    If iHijo.icono <> Nothing Then
                        iCodigo = iCodigo.Replace("<conicono>", STRING_ICONO).Replace("<icono>", iHijo.icono)
                    Else
                        iCodigo = iCodigo.Replace("<conicono>", "")
                    End If
                    If iHijo.tooltip <> Nothing Then
                        iCodigo = iCodigo.Replace("<tooltip>", iHijo.tooltip)
                    Else
                        iCodigo = iCodigo.Replace("<tooltip>", "")
                    End If
                End If
                If tieneMenuFinal(iHijo) Then iCodigo = iCodigo & STRING_NODOCIERRE
            Next

            Return iCodigo

        Catch exception As Exception
            Throw New MenuNoEncontradoException(exception)
        End Try
    End Function

    Private Function tieneMenuFinal(eMenu As MenuCompuesto) As Boolean
        For Each iHijo As Menu In eMenu.menues
            If iHijo.isMenuCompuesto Then
                For Each iHijo2 As Menu In CType(iHijo, MenuCompuesto).menues
                    If iHijo2.isMenuCompuesto Then
                        For Each iHijo3 As Menu In CType(iHijo2, MenuCompuesto).menues
                            If iHijo3.isMenuCompuesto Then
                                For Each iHijo4 As Menu In CType(iHijo3, MenuCompuesto).menues
                                    If iHijo4.isMenuCompuesto Then

                                    Else
                                        Return True
                                    End If
                                Next
                            Else
                                Return True
                            End If
                        Next
                    Else
                        Return True
                    End If
                Next
            Else
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub crearMenuConStored(eScript As String)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos()
            Menu.crearConStored(iAccesoDatos, eScript)

        Catch exception As Exception
            Throw New MenuNoEncontradoException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try

    End Sub

    Public Sub crearMenu(ByVal eMenu As Menu)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            crearMenu(iAccesoDatos, eMenu)
        Catch exception As Exception
            Throw New MenuNoCreadoException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearMenu(ByVal eAccesoDatos As accesoDatos, ByVal eMenu As Menu)
        Try
            eMenu.accesoDatos = eAccesoDatos
            If eMenu.id = Nothing Then eMenu.id = FuncionComun.generarId
            eMenu.crear()
        Catch exception As Exception
            Throw New MenuNoCreadoException(exception)
        Finally
            eMenu.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarMenu(ByVal eMenu As Menu)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            modificarMenu(iAccesoDatos, eMenu)
        Catch exception As Exception
            Throw New MenuNoModificadoException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarMenu(ByVal eAccesoDatos As accesoDatos, ByVal eMenu As Menu)
        Try
            eMenu.accesoDatos = eAccesoDatos
            eMenu.modificar()
        Catch exception As Exception
            Throw New MenuNoModificadoException(exception)
        Finally
            eMenu.accesoDatos = Nothing
        End Try
    End Sub
#End Region

#Region "Logo"
    Public Function obtenerLogo(eNivel As Nivel) As String
        Dim iAdministradorNiveles As New AdministradorNiveles
        Dim iAccesodatos As accesoDatos
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iParametro As New Parametro

        Try
            iAccesodatos = New accesoDatos

            If IsNothing(eNivel) Then
                eNivel = New GrupoEmpresas
                eNivel.id = GrupoEmpresas.GRUPOEMPRESAS
            End If

            iParametro.descripcion = "nombreLogo"
            iParametro = iAdministradorParametros.obtenerParametro(iAccesodatos, iParametro, eNivel)
            Return iParametro.valor.ToString()

        Catch exception As Exception
            Return Nothing
        Finally
            If Not IsNothing(iAccesodatos) Then iAccesodatos.cerrar()
            iAccesodatos = Nothing
            iAdministradorNiveles = Nothing
            iAdministradorParametros = Nothing
            iParametro = Nothing
        End Try
    End Function

    Public Sub validarExistenciaLogo(eNivel As Nivel)
        Try
            Dim iNombre As String = obtenerLogo(eNivel)
            If iNombre <> Nothing Then
                Dim iPath As String = ConfigurationManager.AppSettings("archivosImagenes") & iNombre & ".PNG"
                File.Delete(iPath)
            End If
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub eliminarMenu(ByVal eMenu As Menu)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            eliminarMenu(iAccesoDatos, eMenu)
        Catch exception As Exception
            Throw New MenuNoEliminadoException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarMenu(eAccesoDatos As accesoDatos, ByVal eMenu As Menu)
        Try
            eMenu.accesoDatos = eAccesoDatos
            eMenu.eliminar()

        Catch exception As Exception
            Throw New MenuNoEliminadoException(exception)
        Finally
            eMenu.accesoDatos = Nothing
        End Try
    End Sub


#End Region

#Region "Perfil"

    Public Function perfilHabilitadoConsulta() As String
        Dim iPerfil As New Perfil
        Try
            Return iPerfil.perfilHabilitadoConsulta
        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)
        End Try
    End Function

    Public Sub crearPerfil(ByVal ePerfil As Perfil)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            ePerfil.accesoDatos = iAccesoDatos
            ePerfil.crear()
        Catch exception As Exception
            Throw New PerfilNoCreadoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            ePerfil.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarPerfil(ByVal ePerfil As Perfil)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            iAccesoDatos.beginTransaction()
            ePerfil.accesoDatos = iAccesoDatos
            ePerfil.eliminar()
            iAccesoDatos.commit()
        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New PerfilNoEliminadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            ePerfil.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub asignarPerfil(ByVal ePerfil As Perfil)
        Try
            ePerfil.asignarPerfiles()
        Catch Exception As Exception
            Throw New PerfilNoEliminadoException(Exception)
        End Try
    End Sub

    Public Sub asignarPerfil(ByVal eAccesoDatos As accesoDatos, ByVal ePerfil As Perfil)
        Try
            ePerfil.accesoDatos = eAccesoDatos
            ePerfil.asignarPerfiles()
        Catch Exception As Exception
            Throw New PerfilNoEliminadoException(Exception)
        End Try
    End Sub

    Public Sub asignarPerfiles(ByVal eAccesoDatos As accesoDatos, ByVal ePerfil As Perfil, ByVal eLista As List(Of Long))
        Try
            ePerfil.accesoDatos = eAccesoDatos
            ePerfil.asignarPerfilesLista(eLista)
        Catch Exception As Exception
            Throw New PerfilNoEliminadoException(Exception)
        End Try
    End Sub

    Public Sub modificarPerfiles(ByVal ePerfil As Perfil, ByVal ePerfilesSeleccionados As String, ByVal ePerfilesNoSeleccionados As String, ByVal ePerfilesSeleccionadosColeccion As Collection, ByVal eFinal As Boolean)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            iAccesoDatos.beginTransaction()
            ePerfil.accesoDatos = iAccesoDatos
            ePerfil.modificar(ePerfilesSeleccionados, ePerfilesNoSeleccionados, ePerfilesSeleccionadosColeccion, eFinal)
            iAccesoDatos.commit()
        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New PerfilNoModificadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            ePerfil.accesoDatos = Nothing
        End Try
    End Sub

    'Public Function obtenerArbolReporteBI(Optional eConNodosFinales As Boolean = True) As Object
    '    '================================================================================================
    '    'ESTE MÉTODO OBTIENE EL MENÚ DE REPORTING PARA QUE EL USUARIO PUEDA MODIFICAR
    '    '================================================================================================
    '    Dim iAdministradorParametros As New AdministradorParametros

    '    Dim iAccesoDatos As accesoDatos

    '    Dim iMenu As New MenuCompuesto
    '    Dim iParametro As New Parametro
    '    Dim iGrupoEmpresas As New GrupoEmpresas


    '    Dim iListaItems As List(Of Object)

    '    Try
    '        iAccesoDatos = New accesoDatos()

    '        iGrupoEmpresas.id = GrupoEmpresas.GRUPOEMPRESAS

    '        '---------------
    '        'OBTENEMOS EL ID DE MENÚ INICIAL DEL ARBOL PARA MOSTRARLO A PARTIR DE AHÍ
    '        '---------------
    '        iParametro.descripcion = "ReporteBIIdMenu"
    '        iParametro = iAdministradorParametros.obtenerParametro(iAccesoDatos, iParametro, iGrupoEmpresas)
    '        iMenu.id = iParametro.valor

    '        iListaItems = obtenerArbolPerfilesConCabecera(iAccesoDatos, Nothing, False, False, iMenu)

    '        '---------------
    '        'AGREGAMOS LOS DATOS DE REPORTE A CADA ITEM
    '        '---------------
    '        Return obtenerArbolReporteBIRecursiva(iAccesoDatos, iListaItems, eConNodosFinales)

    '    Catch Exception As Exception
    '        Throw New PerfilNoEncontradoException(Exception)

    '    Finally
    '        If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
    '        iAccesoDatos = Nothing
    '        iAdministradorParametros = Nothing
    '        iParametro = Nothing
    '        iGrupoEmpresas = Nothing
    '    End Try
    'End Function

    'Public Function obtenerArbolReporteBIRecursiva(ByRef eAccesoDatos As accesoDatos, ByRef eListaItems As List(Of Object), Optional eConNodosFinales As Boolean = True) As Object
    '    Dim iAdministradorReportesBI As New AdministradorReportesBI
    '    Dim iLista As New List(Of Object)
    '    Dim iItemReporte As Object

    '    Dim iReporteBI As ReporteBI
    '    Dim iReportID, iIdReporteBI, iDescripcionReporteBI, iIdConexionReporteBI As String

    '    Try

    '        If Not IsNothing(eListaItems) Then
    '            For Each iItem As Object In eListaItems
    '                If (iItem.final AndAlso eConNodosFinales) OrElse Not iItem.final Then
    '                    If iItem.final = True Then
    '                        '---------------
    '                        'OBTENEMOS LOS DATOS DEL REPORTE A PARTIR DEL IDMENU
    '                        '---------------
    '                        iReporteBI = New ReporteBI
    '                        iReporteBI.menu = New MenuCompuesto
    '                        iReporteBI.menu.id = iItem.id

    '                        iReporteBI = iAdministradorReportesBI.obtenerReporteBI(eAccesoDatos, iReporteBI)

    '                        iReportID = iReporteBI.reportID
    '                        iIdReporteBI = iReporteBI.id
    '                        iDescripcionReporteBI = iReporteBI.descripcion
    '                        iIdConexionReporteBI = iReporteBI.conexionReporteBI.id
    '                    Else
    '                        iReporteBI = Nothing
    '                        iReportID = ""
    '                        iIdReporteBI = ""
    '                        iDescripcionReporteBI = ""
    '                        iIdConexionReporteBI = ""
    '                    End If

    '                    iItemReporte = New With {
    '                        .id = iItem.id,
    '                        .text = iItem.text,
    '                        .asignado = iItem.asignado,
    '                        .final = iItem.final,
    '                        .esSubMenu = iItem.esSubMenu,
    '                        .grabaLog = iItem.grabaLog,
    '                        .idAccion = iItem.idAccion,
    '                        .paginaAsociada = iItem.paginaAsociada,
    '                        .nombreAccion = iItem.nombreAccion,
    '                        .checkedFieldName = iItem.checkedFieldName,
    '                        .reportID = iReportID,
    '                        .idReporteBI = iIdReporteBI,
    '                        .descripcionReporteBI = iDescripcionReporteBI,
    '                        .idConexionReporte = iIdConexionReporteBI,
    '                        .children = obtenerArbolReporteBIRecursiva(eAccesoDatos, iItem.children, eConNodosFinales)
    '                         }

    '                    iLista.Add(iItemReporte)
    '                End If
    '            Next

    '        End If

    '        Return iLista

    '    Catch Exception As Exception
    '        Throw New PerfilNoEncontradoException(Exception)
    '    Finally
    '    End Try
    'End Function
    Public Function obtenerArbolPerfilesConCabecera(ByVal ePerfiles As List(Of Perfil), Optional ByVal eEsDetalles As Boolean = False, Optional eEsAsignadoPerfil As Boolean = False, Optional eMenu As Menu = Nothing) As Object
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()

            Return obtenerArbolPerfilesConCabecera(iAccesoDatos, ePerfiles, eEsDetalles, eEsAsignadoPerfil, eMenu)

        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)

        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerArbolPerfilesConCabecera(ByVal eAccesoDatos As accesoDatos, ByVal ePerfiles As List(Of Perfil), Optional ByVal eEsDetalles As Boolean = False, Optional eEsAsignadoPerfil As Boolean = False, Optional eMenu As Menu = Nothing) As Object
        Dim iDataset As DataSet
        Dim iLista As New List(Of Object)
        Dim iDatarow As DataRow
        Dim iItem As Object
        Dim iPerfil As New Perfil

        Try
            iPerfil.accesoDatos = eAccesoDatos

            iDataset = obtenerArbolPerfil(eAccesoDatos, ePerfiles, eEsAsignadoPerfil, eMenu)

            iDatarow = iDataset.Tables("ArbolPerfil").Rows(0)

            iItem = New With {
                    .id = iDatarow.Item("id"),
                        .text = iDatarow.Item("nombre").Substring(0, 1).ToUpper() + iDatarow.Item("nombre").Substring(1).ToLower(),
                    .asignado = FuncionComun.byteBoolean(iDatarow.Item("asignado")),
                    .final = FuncionComun.byteBoolean(iDatarow.Item("final")),
                    .esSubMenu = FuncionComun.byteBoolean(FuncionComun.ceroSiEsNulo(iDatarow.Item("esSubMenu"))),
                    .grabaLog = FuncionComun.byteBoolean(FuncionComun.ceroSiEsNulo(iDatarow.Item("grabaLog"))),
                    .idAccion = FuncionComun.nothingSiEsNulo(iDatarow.Item("idAccion")),
                    .paginaAsociada = FuncionComun.vacioSiEsNulo(iDatarow.Item("paginaAsociada")),
                    .nombreAccion = FuncionComun.vacioSiEsNulo(iDatarow.Item("nombreAccion")),
                    .checkedFieldName = .asignado AndAlso .final,
                    .children = obtenerArbolPerfilesConCabeceraRecursiva(iDataset, ePerfiles, eEsDetalles, iDatarow.Item("id"), iItem)
                        }


            If eEsDetalles Then
                If Not IsNothing(iItem) AndAlso iItem.asignado Then
                    iLista.Add(iItem)
                End If
            Else
                iLista.Add(iItem)
            End If

            Return iLista

        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)
        Finally
        End Try
    End Function

    Public Function obtenerArbolPerfilesConCabeceraRecursiva(ByVal eDataset As DataSet, ByVal ePerfiles As List(Of Perfil), Optional ByVal eEsDetalles As Boolean = False, Optional eIdPadre As Long = 0, Optional eItem As Object = Nothing) As Object
        Dim iLista As New List(Of Object)
        Dim iItem As Object
        Dim iDatarowsHijos() As DataRow

        Try

            iDatarowsHijos = eDataset.Tables("ArbolPerfil").Select("idPadre=" & eIdPadre, "nombre ASC")

            For Each iDataRow As DataRow In iDatarowsHijos
                If FuncionComun.ceroSiEsNulo(iDataRow.Item("idPadre")) = eIdPadre Then

                    iItem = New With {
                        .id = iDataRow.Item("id"),
                        .text = iDataRow.Item("nombre").Substring(0, 1).ToUpper() + iDataRow.Item("nombre").Substring(1).ToLower(),
                        .asignado = FuncionComun.byteBoolean(iDataRow.Item("asignado")),
                        .final = FuncionComun.byteBoolean(iDataRow.Item("final")),
                        .esSubMenu = FuncionComun.byteBoolean(FuncionComun.ceroSiEsNulo(iDataRow.Item("esSubMenu"))),
                        .grabaLog = FuncionComun.byteBoolean(FuncionComun.ceroSiEsNulo(iDataRow.Item("grabaLog"))),
                        .idAccion = FuncionComun.nothingSiEsNulo(iDataRow.Item("idAccion")),
                        .paginaAsociada = FuncionComun.vacioSiEsNulo(iDataRow.Item("paginaAsociada")),
                        .nombreAccion = FuncionComun.vacioSiEsNulo(iDataRow.Item("nombreAccion")),
                    .checkedFieldName = .asignado AndAlso .final,
                            .children = Nothing
                    }

                    If Not iItem.final Then
                        iItem.children = obtenerArbolPerfilesConCabeceraRecursiva(eDataset, ePerfiles, eEsDetalles, iDataRow.Item("id"), iItem)
                    End If

                    If eEsDetalles Then
                        If Not IsNothing(iItem) AndAlso iItem.asignado Then
                            iLista.Add(iItem)
                        End If
                    Else
                        iLista.Add(iItem)
                    End If
                End If
            Next

            Return iLista

        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)
        Finally
        End Try
    End Function


    Public Sub altaPerfil(ByVal ePerfil As Perfil, eListaIds As List(Of Long))
        Dim iAccesoDatos As accesoDatos
        Dim iListaFinal As New List(Of Long)

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()

            ePerfil.accesoDatos = iAccesoDatos

            crearPerfil(ePerfil)

            For Each eId As Long In eListaIds
                If (Not iListaFinal.Contains(eId)) Then
                    iListaFinal.Add(eId)
                End If
                ePerfil.menu = New MenuCompuesto()
                ePerfil.menu.id = eId

                ePerfil.menu.accesoDatos = iAccesoDatos
                ePerfil.menu = CType(ePerfil.menu, MenuCompuesto).obtenerMenuCompuesto()
                If (Not iListaFinal.Contains(ePerfil.menu.idPadre)) Then
                    iListaFinal.Add(ePerfil.menu.idPadre)
                End If
                While ePerfil.menu.idPadre <> Nothing
                    ePerfil.menu.id = ePerfil.menu.idPadre
                    ePerfil.menu.accesoDatos = iAccesoDatos
                    ePerfil.menu = CType(ePerfil.menu, MenuCompuesto).obtenerMenuCompuesto()

                    If (ePerfil.menu.idPadre <> Nothing AndAlso Not iListaFinal.Contains(ePerfil.menu.idPadre)) Then
                        iListaFinal.Add(ePerfil.menu.idPadre)
                    End If
                End While
            Next

            asignarPerfiles(iAccesoDatos, ePerfil, iListaFinal)

            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New PerfilNoCreadoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarPerfil(ByVal ePerfil As Perfil, eListaIds As List(Of Long))
        Dim iAccesoDatos As accesoDatos
        Dim iListaFinal As New List(Of Long)

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()

            ePerfil.accesoDatos = iAccesoDatos

            vaciarPerfil(iAccesoDatos, ePerfil)

            For Each eId As Long In eListaIds
                If (Not iListaFinal.Contains(eId)) Then
                    iListaFinal.Add(eId)
                End If
                ePerfil.menu = New MenuCompuesto()

                ePerfil.menu.id = eId

                ePerfil.menu.accesoDatos = iAccesoDatos
                ePerfil.menu = CType(ePerfil.menu, MenuCompuesto).obtenerMenuCompuesto()

                If (Not iListaFinal.Contains(ePerfil.menu.idPadre)) And ePerfil.menu.idPadre <> Nothing Then
                    iListaFinal.Add(ePerfil.menu.idPadre)
                End If

                While ePerfil.menu.idPadre <> Nothing
                    ePerfil.menu.id = ePerfil.menu.idPadre
                    ePerfil.menu.accesoDatos = iAccesoDatos
                    ePerfil.menu = CType(ePerfil.menu, MenuCompuesto).obtenerMenuCompuesto()

                    If (ePerfil.menu.idPadre <> Nothing AndAlso Not iListaFinal.Contains(ePerfil.menu.idPadre)) Then
                        iListaFinal.Add(ePerfil.menu.idPadre)
                    End If
                End While
            Next

            asignarPerfiles(iAccesoDatos, ePerfil, iListaFinal)

            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New PerfilNoModificadoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub vaciarPerfil(ByVal ePerfil As Perfil)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            iAccesoDatos.beginTransaction()
            ePerfil.accesoDatos = iAccesoDatos
            ePerfil.vaciar()
            iAccesoDatos.commit()
        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New PerfilNoEliminadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            ePerfil.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub vaciarPerfil(ByVal eAccesoDatos As accesoDatos, ByVal ePerfil As Perfil)
        Try
            ePerfil.accesoDatos = eAccesoDatos
            ePerfil.vaciar()
        Catch Exception As Exception
            Throw New PerfilNoEliminadoException(Exception)
        Finally
            ePerfil.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerPerfilesGrilla(ByVal ePerfil As Perfil) As DataSet
        Try
            Return ePerfil.obtenerPerfilesGrilla
        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerArbolPerfil(ByVal eAccesoDatos As accesoDatos, ByVal ePerfiles As List(Of Perfil), Optional eEsAsignadoPerfil As Boolean = False, Optional eMenu As Menu = Nothing) As DataSet
        Dim iPerfil As New Perfil
        Try
            iPerfil.accesoDatos = eAccesoDatos
            Return iPerfil.obtenerArbolPerfil(ePerfiles, eEsAsignadoPerfil, eMenu)
        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)
            iPerfil.accesoDatos = Nothing
            iPerfil = Nothing
        End Try
    End Function


    Public Function obtenerPerfiles(ByVal ePerfil As Perfil) As IDataReader
        Try
            Return ePerfil.obtenerPerfiles
        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerPerfil(ByVal ePerfil As Perfil) As Perfil
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return obtenerPerfil(iAccesoDatos, ePerfil)
        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerPerfil(ByVal eAccesoDatos As accesoDatos, ByVal ePerfil As Perfil) As Perfil
        Try
            ePerfil.accesoDatos = eAccesoDatos
            Return ePerfil.obtenerPerfil
        Catch Exception As Exception
            Throw New PerfilNoEncontradoException(Exception)
        Finally
            ePerfil.accesoDatos = Nothing
        End Try
    End Function
#End Region

#Region "Log"
    Public Sub crearLog(ByVal eLog As Log)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            crearLog(iAccesoDatos, eLog)
        Catch Exception As Exception
            Throw New LogNoCreadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearLog(eAccesoDatos As accesoDatos, ByVal eLog As Log)
        Try
            eLog.accesoDatos = eAccesoDatos
            eLog.crear()
        Catch Exception As Exception
            Throw New LogNoCreadoException(Exception)
        Finally
            eLog.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerLogs(ByVal eLog As Log, ByVal eLogVO As LogVO) As DataSet
        Try
            Return eLog.obtenerLogs(eLogVO)
        Catch Exception As Exception
            Throw New LogNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerLog(ByVal eLog As Log) As Log
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            eLog.accesoDatos = iAccesoDatos
            Return eLog.obtenerLog()
        Catch Exception As Exception
            Throw New LogNoEncontradoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            eLog.accesoDatos = Nothing
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Sub eliminarLog(ByVal eLog As Log, ByVal eLogVO As LogVO)
        Try
            eLog.eliminar(eLogVO)
        Catch Exception As Exception
            Throw New LogNoEncontradoException(Exception)
        End Try
    End Sub
#End Region

#Region "Log Entidad Usuario"

    Public Sub crearLogEntidadUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eLogEntidadUsuario As LogEntidadUsuario)

        Try

            eLogEntidadUsuario.accesoDatos = eAccesoDatos
            eLogEntidadUsuario.crear()

        Catch exception As Exception
            Throw New LogNoCreadoException(exception)
        Finally
            eLogEntidadUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearLogEntidadUsuario(ByVal eLogEntidadUsuario As LogEntidadUsuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            crearLogEntidadUsuario(iAccesoDatos, eLogEntidadUsuario)

        Catch Exception As Exception
            Throw New LogNoCreadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarLogEntidadUsuario(ByVal eLogEntidadUsuario As LogEntidadUsuario)
        Try
            eLogEntidadUsuario.eliminar()
        Catch Exception As Exception
            Throw New LogNoEncontradoException(Exception)
        End Try
    End Sub

    Public Function obtenerLogsEntidad(ByVal eLogsEntidadVO As LogsEntidadVO) As DataSet
        Dim iLogEntidadUsuario As New LogEntidadUsuario
        Try
            Return iLogEntidadUsuario.obtenerLogsEntidad(eLogsEntidadVO)
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            iLogEntidadUsuario = Nothing
        End Try
    End Function

    Public Function obtenerLogsEntidad(ByVal eAccesoDatos As accesoDatos, ByVal eLogsEntidadVO As LogsEntidadVO) As DataSet

        Dim iLogEntidadUsuario As New LogEntidadUsuario
        Try
            iLogEntidadUsuario.accesoDatos = eAccesoDatos
            Return iLogEntidadUsuario.obtenerLogsEntidad(eLogsEntidadVO)
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            iLogEntidadUsuario = Nothing
        End Try
    End Function


    Public Function obtenerSolicitudesConcretadasPolitica(ByVal eAccesoDatos As accesoDatos, ByVal eLogsEntidadVO As LogsEntidadVO, ByVal eIdPoliticaComercial As Long) As DataSet

        Dim iLogEntidadUsuario As New LogEntidadUsuario
        Try
            iLogEntidadUsuario.accesoDatos = eAccesoDatos
            Return iLogEntidadUsuario.obtenerSolicitudesConcretadasPolitica(eLogsEntidadVO, eIdPoliticaComercial)
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            iLogEntidadUsuario = Nothing
        End Try
    End Function

#End Region

#Region "Accion"
    Public Function obtenerAcciones(ByVal eAccion As Accion) As IDataReader
        Try
            Return eAccion.obtenerAcciones()
        Catch Exception As Exception
            Throw New AccionNoEncontradaException(Exception)
        End Try
    End Function

    Public Sub crearAccion(ByVal eAccion As Accion)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            crearAccion(iAccesoDatos, eAccion)
        Catch exception As Exception
            Throw New AccionNoCreadaException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearAccion(ByVal eAccesoDatos As accesoDatos, ByVal eAccion As Accion)
        Try
            eAccion.accesoDatos = eAccesoDatos
            If eAccion.id = Nothing Then eAccion.id = FuncionComun.generarId
            eAccion.crear()
        Catch exception As Exception
            Throw New AccionNoCreadaException(exception)
        Finally
            eAccion.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarAccion(ByVal eAccion As Accion)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            eliminarAccion(iAccesoDatos, eAccion)
        Catch exception As Exception
            Throw New AccionNoEliminadaException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarAccion(eAccesoDatos As accesoDatos, ByVal eAccion As Accion)
        Try
            eAccion.accesoDatos = eAccesoDatos
            eAccion.eliminar()

        Catch exception As Exception
            Throw New AccionNoEliminadaException(exception)
        Finally
            eAccion.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarAccion(ByVal eAccion As Accion)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            modificarAccion(iAccesoDatos, eAccion)
        Catch exception As Exception
            Throw New AccionNoModificadaException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarAccion(ByVal eAccesoDatos As accesoDatos, ByVal eAccion As Accion)
        Try
            eAccion.accesoDatos = eAccesoDatos
            eAccion.modificar
        Catch exception As Exception
            Throw New AccionNoModificadaException(exception)
        Finally
            eAccion.accesoDatos = Nothing
        End Try
    End Sub

#End Region

#Region "Rol"

    Public Sub copiarRolesUsuario(eRolCopiarVO As RolCopiarVO)
        Dim iRol As New Rol
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            iRol.accesoDatos = iAccesoDatos
            iRol.copiarRolesUsuario(eRolCopiarVO)
            iAccesoDatos.commit()

        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New RolNoEncontradoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            iRol = Nothing
        End Try
    End Sub
    Public Function rolHabilitadoConsulta() As String
        Dim iRol As New Rol
        Try
            Return iRol.rolHabilitadoConsulta
        Catch Exception As Exception
            Throw New RolNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerRolesUsuario(ByVal eRol As Rol) As DataSet
        Try
            Return eRol.obtenerRolesHabilitar
        Catch exception As Exception
            Throw New RolNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerRolesUsuarioPorTitulo(ByVal eRol As Rol, ByVal eTitulo As String) As DataSet
        Try
            Return eRol.obtenerRolesHabilitarPorTitulo(eTitulo)
        Catch exception As Exception
            Throw New RolNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerRolesUsuarioFila(ByVal eRol As Rol) As DataSet
        Try
            Return eRol.obtenerRolesUsuarioFila
        Catch exception As Exception
            Throw New RolNoEncontradoException(exception)
        End Try
    End Function

    Public Sub habilitarRol(ByVal eColeccion As Collection)
        Dim iAccesoDatos As accesoDatos
        Dim i As Integer
        Dim iRol As Rol

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            For i = 1 To eColeccion.Count
                iRol = eColeccion.Item(i)
                iRol.accesoDatos = iAccesoDatos
                iRol.habilitarRol()
                iRol.accesoDatos = Nothing
                iRol = Nothing
            Next
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RolNoHabilitadoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            iRol = Nothing
        End Try
    End Sub
#End Region

#Region "Varios"
    Public Function obtenerFechaProcesoConConexion(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario, ByVal eNivel As Nivel, Optional ByVal eFechaVencimiento As Boolean = False, Optional ByVal eFechaProceso As Date = Nothing) As Date
        Dim iAdministradorParametros As AdministradorParametros
        Dim iParametro As Parametro
        Dim iFechaProceso As Date
        Try
            If eFechaVencimiento Then
                iAdministradorParametros = New AdministradorParametros
                iParametro = New Parametro
                iParametro.descripcion = "diasDeVencimiento"
                If eFechaProceso <> Nothing Then
                    iFechaProceso = eFechaProceso
                Else
                    iFechaProceso = obtenerFechaProceso(eAccesoDatos, eUsuario, eNivel)
                End If
                iFechaProceso = iAdministradorParametros.obtenerParametroDiasDeVencimeinto(eAccesoDatos, iParametro, eNivel, iFechaProceso)
                Return iFechaProceso
            Else
                Return obtenerFechaProceso(eAccesoDatos, eUsuario, eNivel)
            End If

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            iAdministradorParametros = Nothing
        End Try
    End Function

    Public Function obtenerFechaProceso(ByVal eUsuario As Usuario, ByVal eNivel As Nivel, Optional ByVal eFechaVencimiento As Boolean = False) As Date
        Dim iAccesoDatos As accesoDatos
        Dim iAdministradorParametros As AdministradorParametros
        Dim iParametro As Parametro
        Dim iFechaProceso As Date
        Try
            iAccesoDatos = New accesoDatos
            If eFechaVencimiento Then
                iAdministradorParametros = New AdministradorParametros
                iParametro = New Parametro
                iParametro.descripcion = "diasDeVencimiento"
                iFechaProceso = obtenerFechaProceso(iAccesoDatos, eUsuario, eNivel)
                iFechaProceso = iAdministradorParametros.obtenerParametroDiasDeVencimeinto(iAccesoDatos, iParametro, eNivel, iFechaProceso)
                Return iFechaProceso
            Else
                Return obtenerFechaProceso(iAccesoDatos, eUsuario, eNivel)
            End If

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerFechaProceso(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario, ByVal eNivel As Nivel) As Date
        Dim iParametro As Parametro
        Dim iAdministradorParametros As AdministradorParametros
        Try
            If eUsuario.rolesAutorizados.BinarySearch(eUsuario.rolesAutorizados, "tomaFechaProceso") > 0 Then
                iAdministradorParametros = New AdministradorParametros
                iParametro = New Parametro
                iParametro.descripcion = "fechaProceso"
                iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, eNivel)
                Return Format(CDate(iParametro.valor), "dd/MM/yyyy")
            Else
                Return Today
            End If
        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            iParametro = Nothing
            iAdministradorParametros = Nothing
        End Try
    End Function

#End Region

#Region "Medio Acceso Sistema"
    Public Function obtenerMedioAccesoSistema(ByVal eMedioAccesoSistema As MedioAccesoSistema) As MedioAccesoSistema
        Try
            Return eMedioAccesoSistema.obtenerMedioAccesoSistema
        Catch exception As Exception
            Throw New MedioAccesoSistemaNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerMediosAccesoSistema(ByVal eMedioAccesoSistema As MedioAccesoSistema) As IDataReader
        Try
            Return eMedioAccesoSistema.obtenerMediosAccesoSistema
        Catch exception As Exception
            Throw New MedioAccesoSistemaNoEncontradoException(exception)
        End Try
    End Function
#End Region

#Region "Tipo Usuario Tarea"

    Public Sub crearTipoUsuarioTarea(ByVal eTipoUsuarioTarea As TipoUsuarioTarea)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eTipoUsuarioTarea.accesoDatos = iAccesoDatos
            crearTipoUsuarioTarea(iAccesoDatos, eTipoUsuarioTarea)
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(exception)
        Finally
            eTipoUsuarioTarea.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearTipoUsuarioTarea(ByVal eAccesoDatos As accesoDatos, ByVal eTipoUsuarioTarea As TipoUsuarioTarea)
        Try
            eTipoUsuarioTarea.accesoDatos = eAccesoDatos
            eTipoUsuarioTarea.crear()
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            eTipoUsuarioTarea.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarTipoUsuarioTarea(ByVal eTipoUsuarioTarea As TipoUsuarioTarea)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eTipoUsuarioTarea.accesoDatos = iAccesoDatos
            eliminarTipoUsuarioTarea(iAccesoDatos, eTipoUsuarioTarea)
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(exception)
        Finally
            eTipoUsuarioTarea.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarTipoUsuarioTarea(ByVal eAccesoDatos As accesoDatos, ByVal eTipoUsuarioTarea As TipoUsuarioTarea)
        Try
            eTipoUsuarioTarea.accesoDatos = eAccesoDatos
            eTipoUsuarioTarea.eliminar()
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            eTipoUsuarioTarea.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarTipoUsuarioTarea(ByVal eTipoUsuarioTarea As TipoUsuarioTarea)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            eTipoUsuarioTarea.accesoDatos = iAccesoDatos
            eTipoUsuarioTarea.modificar()
        Catch RootException As RootException
            Throw RootException
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eTipoUsuarioTarea.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerTipoUsuarioTarea(ByVal eTipoUsuarioTarea As TipoUsuarioTarea) As TipoUsuarioTarea
        Dim iAccesoDatos As accesoDatos

        Try

            iAccesoDatos = New accesoDatos
            eTipoUsuarioTarea.accesoDatos = iAccesoDatos
            Return eTipoUsuarioTarea.obtenerTipoUsuarioTarea

        Catch Exception As Exception
            Throw New RootException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eTipoUsuarioTarea.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerTiposUsuarioTarea(ByVal eTipoUsuarioTarea As TipoUsuarioTarea) As IDataReader
        Try
            Return eTipoUsuarioTarea.obtenerTiposUsuarioTarea()
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function

    Public Function obtenerTiposUsuarioTareaDataSet(ByVal eTipoUsuarioTarea As TipoUsuarioTarea) As DataSet
        Try
            Return eTipoUsuarioTarea.obtenerTiposUsuarioTareaDataSet()
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function

#End Region

#Region "Punto venta Dgi"

    Public Sub crearPuntoVentaDgi(ByVal ePuntoVentaDgi As PuntoVentaDgi)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos

            iAccesoDatos.beginTransaction()
            crearPuntoVentaDgi(iAccesoDatos, ePuntoVentaDgi)
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(exception)
        Finally
            ePuntoVentaDgi.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing

        End Try
    End Sub

    Public Sub crearPuntoVentaDgi(ByVal eAccesoDatos As accesoDatos, ByVal ePuntoVentaDgi As PuntoVentaDgi)
        Dim iParametro As New Parametro
        Dim iAdministradorParametros As New AdministradorParametros

        Try

            ePuntoVentaDgi.accesoDatos = eAccesoDatos
            ePuntoVentaDgi.crear()
            ePuntoVentaDgi.accesoDatos = Nothing

            iAdministradorParametros.crearParametrosDesdeParametroBase(eAccesoDatos, ePuntoVentaDgi)

        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            ePuntoVentaDgi.accesoDatos = Nothing
            iParametro = Nothing
            iAdministradorParametros = Nothing
        End Try
    End Sub

    Public Function obtenerPuntosVentaDgi(ByVal ePuntoVentaDgi As PuntoVentaDgi) As DataSet
        Try
            Return ePuntoVentaDgi.obtenerNivelesDataSet()
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function

    Public Function obtenerPuntoVentaDgi(ByVal ePuntoVentaDgi As PuntoVentaDgi) As Nivel
        Try
            Return ePuntoVentaDgi.obtenerNivel()
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function
    Public Function obtenerPuntoVentaDgi(ByVal eAccesoDatos As accesoDatos, ByVal ePuntoVentaDgi As PuntoVentaDgi) As Nivel
        Try
            ePuntoVentaDgi.accesoDatos = eAccesoDatos
            Return ePuntoVentaDgi.obtenerNivel()
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function

    Public Sub modificarPuntoVentaDgi(ByVal ePuntoVentaDgi As PuntoVentaDgi)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos

            iAccesoDatos.beginTransaction()

            modificarPuntoVentaDgi(iAccesoDatos, ePuntoVentaDgi)

            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(exception)
        Finally
            ePuntoVentaDgi.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarPuntoVentaDgi(ByVal eAccesoDatos As accesoDatos, ByVal ePuntoVentaDgi As PuntoVentaDgi)

        Try
            ePuntoVentaDgi.accesoDatos = eAccesoDatos
            ePuntoVentaDgi.modificar()

        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            ePuntoVentaDgi.accesoDatos = Nothing
        End Try

    End Sub


    Public Sub eliminariPuntoVentaDgi(ByVal ePuntoVentaDgi As PuntoVentaDgi)
        Dim iAccesoDatos As accesoDatos
        Dim iParametro As New Parametro
        Dim iAdministradorParametros As New AdministradorParametros

        Try
            iAccesoDatos = New accesoDatos

            iAccesoDatos.beginTransaction()

            iParametro.nombreTabla = "parametroFacturacion"
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, ePuntoVentaDgi)

            iParametro.nombreTabla = "parametro"
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, ePuntoVentaDgi)

            eliminariPuntoVentaDgi(iAccesoDatos, ePuntoVentaDgi)

            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(exception)
        Finally
            ePuntoVentaDgi.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try

    End Sub


    Public Sub eliminariPuntoVentaDgi(ByVal eAccesoDatos As accesoDatos, ByVal ePuntoVentaDgi As PuntoVentaDgi)

        Try
            ePuntoVentaDgi.accesoDatos = eAccesoDatos
            ePuntoVentaDgi.eliminar()

        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            ePuntoVentaDgi.accesoDatos = Nothing
        End Try

    End Sub

#End Region

#Region "Sector Autorización"

    Public Sub crearSectorAutorizacion(ByVal eSectorAutorizacion As SectorAutorizacion)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eSectorAutorizacion.accesoDatos = iAccesoDatos
            crearSectorAutorizacion(iAccesoDatos, eSectorAutorizacion)
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(exception)
        Finally
            eSectorAutorizacion.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearSectorAutorizacion(ByVal eAccesoDatos As accesoDatos, ByVal eSectorAutorizacion As SectorAutorizacion)
        Try
            eSectorAutorizacion.accesoDatos = eAccesoDatos
            eSectorAutorizacion.crear()
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            eSectorAutorizacion.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarSectorAutorizacion(ByVal eSectorAutorizacion As SectorAutorizacion)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eSectorAutorizacion.accesoDatos = iAccesoDatos
            eliminarSectorAutorizacion(iAccesoDatos, eSectorAutorizacion)
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(exception)
        Finally
            eSectorAutorizacion.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarSectorAutorizacion(ByVal eAccesoDatos As accesoDatos, ByVal eSectorAutorizacion As SectorAutorizacion)
        Try
            eSectorAutorizacion.accesoDatos = eAccesoDatos
            eSectorAutorizacion.eliminar()
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            eSectorAutorizacion.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarSectorAutorizacion(ByVal eSectorAutorizacion As SectorAutorizacion)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            eSectorAutorizacion.accesoDatos = iAccesoDatos
            eSectorAutorizacion.modificar()
        Catch RootException As RootException
            Throw RootException
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eSectorAutorizacion.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerSectorAutorizacion(ByVal eSectorAutorizacion As SectorAutorizacion) As SectorAutorizacion
        Dim iAccesoDatos As accesoDatos

        Try

            iAccesoDatos = New accesoDatos
            eSectorAutorizacion.accesoDatos = iAccesoDatos
            Return obtenerSectorAutorizacion(iAccesoDatos, eSectorAutorizacion.obtenerSectorAutorizacion)

        Catch Exception As Exception
            Throw New RootException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eSectorAutorizacion.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerSectorAutorizacion(ByVal eAccesoDatos As accesoDatos, ByVal eSectorAutorizacion As SectorAutorizacion) As SectorAutorizacion
        Try
            eSectorAutorizacion.accesoDatos = eAccesoDatos
            Return eSectorAutorizacion.obtenerSectorAutorizacion
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function

    Public Function obtenerSectoresAutorizacion(ByVal eSectorAutorizacion As SectorAutorizacion) As IDataReader
        Try
            Return eSectorAutorizacion.obtenerSectoresAutorizacion()
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function

    Public Function obtenerSectoresAutorizacionGrilla(ByVal eSectorAutorizacion As SectorAutorizacion) As DataSet
        Try
            Return eSectorAutorizacion.obtenerSectoresAutorizacionGrilla()
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function

#End Region

#Region "Log Usuario"
    Public Sub crearLogUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eLogUsuario As LogUsuario)

        Try

            eLogUsuario.accesoDatos = eAccesoDatos
            eLogUsuario.crear()

        Catch exception As Exception
            Throw New LogNoCreadoException(exception)
        Finally
            eLogUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearLogUsuario(ByVal eLogUsuario As LogUsuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            crearLogUsuario(iAccesoDatos, eLogUsuario)

        Catch Exception As Exception
            Throw New LogNoCreadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerLogUsuarioReporte(ByVal eLogUsuarioReporteVO As LogUsuarioReporteVO) As String
        Dim iLogUsuario As New LogUsuario
        Dim iPathArchivo As String
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos

            iLogUsuario.accesoDatos = iAccesoDatos
            eLogUsuarioReporteVO.dataSet = iLogUsuario.obtenerLogUsuarioReporte(eLogUsuarioReporteVO)
            iLogUsuario.accesoDatos = Nothing

            iPathArchivo = obtenerLogUsuarioReporteExcel(iAccesoDatos, eLogUsuarioReporteVO)

            Return iPathArchivo

        Catch Exception As Exception
            Throw New RootException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            iLogUsuario = Nothing
        End Try
    End Function

    Public Function obtenerLogUsuarioReporteExcel(ByVal eAccesoDatos As accesoDatos, ByVal eLogUsuarioReporteVO As LogUsuarioReporteVO) As String
        ' Dim iAdministradorSolicitudes As New AdministradorSolicitudes
        Dim iPathArchivo As String
        Dim iColeccionArchivosAZipear As New Collection
        Dim iArchivo As StreamWriter
        Dim iLibro As New Workbook
        Dim iRow As WorksheetRow
        Dim iDataRow As DataRow
        Dim iHoja As Worksheet

        Dim iTitulo, iTituloArchivo As String

        Try

            iTituloArchivo = "REPORTELOGUSUARIOS"

            iPathArchivo = ConfigurationManager.AppSettings("archivosGenerados") & iTituloArchivo & Format(Now, "ddMMyyyy") & Format(Now, "hhmmss") & ".xls"

            FuncionComun.agregarEstilosExcel(iLibro)

            iHoja = iLibro.Worksheets.Add("RESULTADO")

            iRow = iHoja.Table.Rows.Add()

            'AGREGO LOS ENCABEZADOS
            iRow = iHoja.Table.Rows.Add()
            iTitulo = "REPORTE LOG USUARIOS"
            iRow.Cells.Add(New WorksheetCell(iTitulo, "EstiloTitulo"))
            iRow.Cells(0).MergeAcross = 10
            iRow = iHoja.Table.Rows.Add()
            If eLogUsuarioReporteVO.fechaDesde <> Nothing Then iRow.Cells.Add(New WorksheetCell("FECHA DESDE:   " & Format(eLogUsuarioReporteVO.fechaDesde, "dd/MM/yyyy"), "EstiloSubTitulo"))
            iRow = iHoja.Table.Rows.Add()
            If eLogUsuarioReporteVO.fechaHasta <> Nothing Then iRow.Cells.Add(New WorksheetCell("FECHA HASTA: " & Format(eLogUsuarioReporteVO.fechaHasta, "dd/MM/yyyy"), "EstiloSubTitulo"))
            iRow = iHoja.Table.Rows.Add()
            iRow = iHoja.Table.Rows.Add()

            'AGREGO LOS REGISTROS
            iRow = iHoja.Table.Rows.Add()

            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))
            iHoja.Table.Columns.Add(New WorksheetColumn(100))

            iRow.Cells.Add(New WorksheetCell("ID", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("USUARIO", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("FECHA MODIF", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("HORA MODIF", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("USUARIO MODIF", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("ESTADO ANTERIOR", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("ESTADO ACTUAL", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("ESTADO USUARIO ANTERIOR", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("ESTADO USUARIO ACTUAL", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("PERFIL ANTERIOR", "EstiloTituloAzul"))
            iRow.Cells.Add(New WorksheetCell("PERFIL ACTUAL", "EstiloTituloAzul"))

            For Each iDataRow In eLogUsuarioReporteVO.dataSet.Tables("LogUsuarioReporte").Rows
                iRow = iHoja.Table.Rows.Add()

                With iDataRow
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(FuncionComun.ceroSiEsNulo(.Item("id"))), DataType.Number, "ENN"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("usuario")), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(Format(.Item("fecha"), "dd/MM/yyyy")), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("hora").ToString), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("usuarioModificador")), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("estadoAnterior")), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("estadoActual")), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("estadoUsuarioAnterior")), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("estadoUsuarioActual")), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("perfilAnterior")), DataType.String, "ENT"))
                    iRow.Cells.Add(New WorksheetCell(FuncionComun.vacioSiEsNulo(.Item("perfilActual")), DataType.String, "ENT"))

                End With

            Next

            iRow = iHoja.Table.Rows.Add()

            iLibro.Save(iPathArchivo)

            Return iPathArchivo

        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            'iAdministradorSolicitudes = Nothing
            iColeccionArchivosAZipear = Nothing
            iArchivo = Nothing
            iLibro = Nothing
            iHoja = Nothing
            iRow = Nothing
        End Try
    End Function

#End Region

#Region "Genera Contrasenia"
    Public Sub modificarSoloGeneraContrasenia(eUsuario As Usuario)
        Dim iAccesoDatos As accesoDatos

        Try

            iAccesoDatos = New accesoDatos
            modificarSoloGeneraContrasenia(iAccesoDatos, eUsuario)

        Catch ex As Exception
            Throw New UsuarioNoModificadoException(ex)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarSoloGeneraContrasenia(eAccesoDatos As accesoDatos, eUsuario As Usuario)

        Try
            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.modificarSoloGeneraContrasenia()

        Catch ex As Exception
            Throw New UsuarioNoModificadoException(ex)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try
    End Sub

    'Public Sub enviarGeneraContrasenia(eUsuarioGeneraContrasenia As UsuarioGeneraContraseniaVO, eUsuario As Usuario)
    '    Dim iAccesoDatos As accesoDatos

    '    Try

    '        iAccesoDatos = New accesoDatos
    '        enviarGeneraContrasenia(iAccesoDatos, eUsuarioGeneraContrasenia, eUsuario)

    '    Catch ex As Exception
    '        Dim iResultado As String
    '        iResultado = Log.obtenerErrorAplicacion(ex, Me.ToString).mensajeUsuario
    '        EnviaMail.enviarMail(eUsuario.mail, "GENERAR CONTRASEÑA", iResultado)
    '        Throw New UsuarioGeneraContraseniaNoEnviadaException(ex)
    '    Finally
    '        iAccesoDatos.cerrar()
    '        iAccesoDatos = Nothing
    '    End Try
    'End Sub

    'Public Sub enviarGeneraContrasenia(eAccesoDatos As accesoDatos, eUsuarioGeneraContrasenia As UsuarioGeneraContraseniaVO, eUsuario As Usuario)
    '    Dim iAdministradorParametros As New AdministradorParametros
    '    Dim iAdministradorNiveles As New AdministradorNiveles
    '    Dim iEncriptador As New Encriptador("tokengeneracontraseniausuario")
    '    Dim iParametro As New Parametro
    '    Dim iMensaje, iUrlParametro As String
    '    Dim iDiasVencimiento As Integer
    '    Dim iUrl, iUrlForm, iUrlToken As String
    '    Dim iComillas As String = Chr(34)

    '    Try

    '        'Obtengo el nivel del usuario
    '        eUsuario.nivel = iAdministradorNiveles.obtenerNivel(eAccesoDatos, eUsuario.nivel)

    '        ' --- CALCULO FECHA VENCIMIENTO ---
    '        iParametro.descripcion = "DiasVencimientoGeneraContraseniaUsuario"
    '        iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, eUsuario.nivel)
    '        iDiasVencimiento = iParametro.valor

    '        eUsuarioGeneraContrasenia.fechaVencimientoGeneraContrasenia = Today.AddDays(iDiasVencimiento)

    '        ' --- GENERAR TOKEN ---
    '        With eUsuarioGeneraContrasenia
    '            iUrlParametro = String.Join("|", {
    '                    FuncionComun.vacioSiEsNulo(.usuarioId),
    '                    FuncionComun.vacioSiEsNulo(.usuarioLogin),
    '                    FuncionComun.vacioSiEsNulo(.usuarioMail),
    '                    FuncionComun.vacioSiEsNulo(.fechaVencimientoGeneraContrasenia)  ' NOTE: Necesario para que el token varie del anterior en caso de ser reenviado
    '                    })
    '        End With
    '        iUrlToken = iEncriptador.encriptar(iUrlParametro)

    '        iUrlForm = "LoginCambioContrasenia.aspx"
    '        iUrl = eUsuarioGeneraContrasenia.urlBase & "/" & iUrlForm & "?parametro='" & iUrlToken & "'"

    '        ' --- ENVIAR DE MAIL ---
    '        iMensaje = "<p>(Este es un mensaje de correo electrónico automático, no lo respondas)</p>" & vbNewLine & vbNewLine
    '        iMensaje &= "<p>Hola " & eUsuarioGeneraContrasenia.usuarioNombre & " ! Bienvenido al sistema LOAN,</p>" & vbNewLine & vbNewLine
    '        iMensaje &= "<p>Para poder acceder activa tu cuenta pulsando en el siguiente enlace:</p>" & vbNewLine & vbNewLine
    '        iMensaje &= "<p><a href=" & iComillas & iUrl & iComillas & ">Activar cuenta</a></p>" & vbNewLine & vbNewLine
    '        iMensaje &= "<p>Login: <b>" & eUsuarioGeneraContrasenia.usuarioLogin & "</b></p>" & vbNewLine & vbNewLine
    '        iMensaje &= "<p>Personaliza tu contraseña para completar la activación.</p>" & vbNewLine & vbNewLine
    '        iMensaje &= "<p>Ten en cuenta que el código caducará en " & Trim(Str(iDiasVencimiento)) & " día(s)." & vbNewLine
    '        iMensaje &= "Transcurrido este tiempo, tendrás que volver a pedirnos que cambiemos tus datos.</p>"

    '        EnviaMail.enviarMail(eUsuarioGeneraContrasenia.usuarioMail, "GENERAR CONTRASEÑA", iMensaje, True)

    '        ' --- MARCAR USUARIO ---
    '        With eUsuario
    '            .generaContrasenia = True
    '            .fechaVencimientoGeneraContrasenia = eUsuarioGeneraContrasenia.fechaVencimientoGeneraContrasenia
    '            .tokenGeneraContrasenia = iUrlToken
    '            .estadoTokenGeneraContrasenia = New Alta
    '        End With

    '        modificarSoloGeneraContrasenia(eAccesoDatos, eUsuario)

    '    Catch ex As Exception
    '        Dim iResultado As String
    '        iResultado = Log.obtenerErrorAplicacion(ex, Me.ToString).mensajeUsuario
    '        EnviaMail.enviarMail(eUsuario.mail, "GENERAR CONTRASEÑA", iResultado)
    '        Throw New UsuarioGeneraContraseniaNoEnviadaException(ex)
    '    Finally
    '        iEncriptador = Nothing
    '        iParametro = Nothing
    '        iAdministradorParametros = Nothing
    '        iAdministradorNiveles = Nothing
    '    End Try
    'End Sub

    'Public Function decodificarTokenGeneraContrasenia(eTokenParametro As String, eUsuarioGeneraContraseniaVO As UsuarioGeneraContraseniaVO) As UsuarioGeneraContraseniaVO
    '    ' @eTokenParametro : parámetro recibido por la página
    '    ' @eUsuarioGeneraContraseniaVO : objeto a popular
    '    Dim iEncriptador As New Encriptador("tokengeneracontraseniausuario")
    '    Dim iTokenString As String
    '    Dim iTokenVector As String()
    '    Dim iSeparador As String = "|"

    '    Try
    '        If IsNothing(eTokenParametro) Then Throw New UsuarioGeneraContraseniaNoGeneradaException("URL no valida!")

    '        eTokenParametro = eTokenParametro.Replace(" ", "+")  ' NOTE: Reemplazo porque los caracteres "+" que genera el encriptador no los reconoce el QueryString
    '        eTokenParametro = eTokenParametro.Replace("'", "")  ' NOTE: Quito las comillas simples que le habiamos puesto al momento de enviar el token por mail.
    '        iTokenString = iEncriptador.desencriptar(eTokenParametro)
    '        iTokenVector = Strings.Split(iTokenString, iSeparador)
    '        With eUsuarioGeneraContraseniaVO
    '            .usuarioId = iTokenVector.GetValue(0)
    '            .usuarioLogin = iTokenVector.GetValue(1)
    '            .usuarioMail = iTokenVector.GetValue(2)
    '            .fechaVencimientoGeneraContrasenia = iTokenVector.GetValue(3)
    '            .tokenGeneraContrasenia = eTokenParametro
    '        End With

    '        Return eUsuarioGeneraContraseniaVO

    '    Catch ex As Exception
    '        Throw New UsuarioGeneraContraseniaNoEnviadaException(ex)
    '    Finally
    '        iTokenVector = Nothing
    '        iEncriptador = Nothing
    '    End Try

    'End Function

    'Public Function obtenerUsuarioGeneraContrasenia(eUsuarioGeneraContraseniaVO As UsuarioGeneraContraseniaVO) As Usuario
    '    Dim iUsuario As Usuario
    '    Dim iTokenVencido As Boolean

    '    Try
    '        ' --- CHEQUEAR VENCIMIENTO TOKEN ---
    '        iTokenVencido = (Today > eUsuarioGeneraContraseniaVO.fechaVencimientoGeneraContrasenia)
    '        If iTokenVencido Then
    '            Throw New UsuarioGeneraContraseniaNoGeneradaException("Enlace de activacion vencido! Debera solicitar un nuevo enlace al Administrador")
    '        End If

    '        ' --- OBTENER USUARIO ---
    '        With eUsuarioGeneraContraseniaVO
    '            iUsuario = New Usuario
    '            iUsuario.id = .usuarioId
    '            iUsuario.login = .usuarioLogin
    '            iUsuario = obtenerUsuarioSoloIds(iUsuario)
    '        End With
    '        If IsNothing(iUsuario) Then
    '            Throw New UsuarioGeneraContraseniaNoGeneradaException("Usuario no valido!")
    '        End If

    '        ' --- CHEQUEA TOKEN ---
    '        If eUsuarioGeneraContraseniaVO.tokenGeneraContrasenia <> iUsuario.tokenGeneraContrasenia Then
    '            Throw New UsuarioGeneraContraseniaNoGeneradaException("Token no valido!")
    '        End If

    '        If Not iUsuario.estadoTokenGeneraContrasenia.isAlta Then
    '            Throw New UsuarioGeneraContraseniaNoGeneradaException("Token utilizado anteriormente!")
    '        End If

    '        Return iUsuario

    '    Catch ex As Exception
    '        Throw New UsuarioGeneraContraseniaNoGeneradaException(ex)
    '    Finally
    '        iUsuario = Nothing
    '    End Try

    'End Function

    Public Sub cambiarPasswordUsuarioGeneraContrasenia(ByVal eUsuario As Usuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            cambiarPasswordUsuarioGeneraContrasenia(iAccesoDatos, eUsuario)

        Catch ex As Exception
            Throw New UsuarioNoModificadoException(ex)
        Finally
            If Not IsNothing(iAccesoDatos) Then
                iAccesoDatos.cerrar()
                iAccesoDatos = Nothing
            End If
        End Try

    End Sub

    Public Sub cambiarPasswordUsuarioGeneraContrasenia(ByVal eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)

        Try

            validarCambioPassword(eAccesoDatos, eUsuario)

            eUsuario.accesoDatos = eAccesoDatos
            eUsuario.cambiarPasswordUsuarioGeneraContrasenia()

        Catch ex As Exception
            Throw New UsuarioNoModificadoException(ex)
        Finally
            eUsuario.accesoDatos = Nothing
        End Try

    End Sub


    Public Sub validarCambioPassword(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario)
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iRegistroUsuario As New RegistroUsuario
        Dim iParametro As Parametro
        Dim iGrupoEmpresas As New GrupoEmpresas
        Try

            iGrupoEmpresas.id = GrupoEmpresas.GRUPOEMPRESAS

            iParametro = New Parametro
            iParametro.descripcion = "CantidadDiasLimiteCambioContrasenia"
            iParametro = iAdministradorParametros.obtenerParametro(eAccesoDatos, iParametro, iGrupoEmpresas)
            If FuncionComun.ceroSiEsNulo(iParametro.valor) <> 0 AndAlso eUsuario.fechaUltimoCambioContraseña.AddDays(iParametro.valor) <= Now.Date Then
                Throw New UsuarioGeneraContraseniaNoGeneradaException("El Usuario supera la cantidad de dias para cambiar la contraseña.")
            End If


        Catch Exception As Exception
            Throw Exception
        Finally
            If Not IsNothing(eUsuario) Then eUsuario.accesoDatos = Nothing
            iAdministradorParametros = Nothing
            iParametro = Nothing
        End Try

    End Sub

#End Region

#Region "Dashboard"
    Public Function obtenerDashboard(ByVal eDashboard As Dashboard) As Dashboard
        Try
            Return eDashboard.obtenerDashboard
        Catch exception As Exception
            Throw New DashboardNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerDashboard(eAccesoDatos As accesoDatos, ByVal eDashboard As Dashboard) As Dashboard
        Try
            eDashboard.accesoDatos = eAccesoDatos
            Return eDashboard.obtenerDashboard

        Catch exception As Exception
            Throw New DashboardNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerDashboards(ByVal eDashboard As Dashboard) As IDataReader
        Try
            Return eDashboard.obtenerDashboards()
        Catch exception As Exception
            Throw New DashboardNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerDashboardsGrilla(ByVal eDashboard As Dashboard) As DataSet
        Try
            Return eDashboard.obtenerDashboardsGrilla()
        Catch exception As Exception
            Throw New DashboardNoEncontradoException(exception)
        End Try
    End Function

    Public Sub crearDashboard(ByVal eDashboard As Dashboard)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            eDashboard.accesoDatos = iAccesoDatos
            crearDashboard(iAccesoDatos, eDashboard)
        Catch Exception As Exception
            Throw New DashboardNoCreadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eDashboard.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearDashboard(eAccesoDatos As accesoDatos, ByVal eDashboard As Dashboard)
        Try
            eDashboard.accesoDatos = eAccesoDatos
            eDashboard.crear()
        Catch Exception As Exception
            Throw New DashboardNoCreadoException(Exception)
        Finally
            eDashboard.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarDashboard(ByVal eDashboard As Dashboard)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            eDashboard.accesoDatos = iAccesoDatos
            eDashboard.eliminar()
        Catch Exception As Exception
            Throw New DashboardNoEliminadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eDashboard.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarDashboard(ByVal eDashboard As Dashboard)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            eDashboard.accesoDatos = iAccesoDatos
            eDashboard.modificar()
        Catch Exception As Exception
            Throw New DashboardNoModificadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eDashboard.accesoDatos = Nothing
        End Try
    End Sub

#End Region

#Region "Tipo Tablero"

    Public Function obtenerTipoTablero(ByVal eTipoTablero As TipoTablero) As TipoTablero
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            eTipoTablero.accesoDatos = iAccesoDatos
            Return eTipoTablero.obtenerTipoTablero

        Catch Exception As Exception
            Throw New RootException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eTipoTablero.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerTiposTablero(ByVal eTipoTablero As TipoTablero) As IDataReader
        Try
            Return eTipoTablero.obtenerTiposTableros()
        Catch Exception As Exception
            Throw New RootException(Exception)
        End Try
    End Function

    Public Function obtenerDashboardCompletoPorUsuario(ByVal eUsuario As Usuario, eFecha As Date) As String
        Dim iAccesoDatos As accesoDatos

        Try

            iAccesoDatos = New accesoDatos
            Return obtenerDashboardCompletoPorUsuario(iAccesoDatos, eUsuario, eFecha)

        Catch exception As Exception
            Throw New DashboardNoEncontradoException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerDashboardCompletoPorUsuario(eAccesoDatos As accesoDatos, ByVal eUsuario As Usuario, eFecha As Date) As String
        'ESTE MÉTODO DEVUELVE UN JSON COMPLETO CON TODOS LOS TABLEROS DEL USUARIO
        Dim iDashBoard As New Dashboard
        Dim iAcumuladores As String
        'Dim iTablerosDashboardVO As List(Of TableroDashboardVO)
        Dim iJson As String

        Try
            ''PRIMERO GENERAMOS DINAMICOS DE ESE USUARIO POR SI CAMBIÓ ALGÚN VALOR
            'generarTablerosDinamicosPorUsuario(eAccesoDatos, eUsuario, eFecha)

            ''LUEGO RECUPERAMOS LOS TABLEROS ALMACENADOS
            'iDashBoard.accesoDatos = eAccesoDatos
            'iJson = iDashBoard.obtenerDashboardCompletoPorUsuario(eUsuario)

            'iCoelsaSantanderTransferenciaLogin = JsonConvert.DeserializeObject(Of CoelsaSantanderTransferenciaLogin)(iJsonRespuestaLogin)

            iAcumuladores = "<div class=" & """row""" & ">                                                                         " &
                            "    <div class=" & """col-md-12""" & ">                                                               " &
                            "        <div class=" & """card card-stats""" & ">                                                     " &
                            "            <div class=" & """card-body""" & ">                                                       " &
                            "                <div class=" & """row""" & ">                                                         " &
                            "                    <div class=" & """col-md-1""" & ">                                                " &
                            "                    </div>                                                                            " &
                            "                    <div class=" & """col-md-2""" & ">                                                " &
                            "                        <div class=" & """statistics""" & ">                                          " &
                            "                            <div class=" & """info""" & ">                                            " &
                            "	                            <div class=" & """icon icon-primary""" & ">                            " &
                            "		                            <i class=" & """now-ui-icons arrows-1_cloud-download-93""" & "></i>" &
                            "	                            </div>                                                                 " &
                            "	                            <h3 class=" & """info-title""" & ">325</h3>                            " &
                            "	                            <h6 class=" & """stats-title""" & ">Ingresadas</h6>                    " &
                            "                            </div>                                                                    " &
                            "                        </div>                                                                        " &
                            "                    </div>                                                                            " &
                            "                    <div class=" & """col-md-2""" & ">                                                " &
                            "                        <div class=" & """statistics""" & ">                                          " &
                            "                            <div class=" & """info""" & ">                                            " &
                            "	                            <div class=" & """icon icon-warning""" & ">                            " &
                            "		                            <i class=" & """now-ui-icons media-1_button-pause""" & "></i>      " &
                            "	                            </div>                                                                 " &
                            "	                            <h3 class=" & """info-title""" & ">154</h3>                            " &
                            "	                            <h6 class=" & """stats-title""" & ">Pendientes</h6>                    " &
                            "                            </div>                                                                    " &
                            "                        </div>                                                                        " &
                            "                    </div>                                                                            " &
                            "                    <div class=" & """col-md-2""" & ">                                                " &
                            "                        <div class=" & """statistics""" & ">                                          " &
                            "                            <div class=" & """info""" & ">                                            " &
                            "	                            <div class=" & """icon icon-success""" & ">                            " &
                            "		                            <i class=" & """now-ui-icons media-1_button-play""" & "></i>       " &
                            "	                            </div>                                                                 " &
                            "	                            <h3 class=" & """info-title""" & ">67</h3>                             " &
                            "	                            <h6 class=" & """stats-title""" & ">Aprobadas</h6>                     " &
                            "                            </div>                                                                    " &
                            "                        </div>                                                                        " &
                            "                    </div>                                                                            " &
                            "                    <div class=" & """col-md-2""" & ">                                                " &
                            "                        <div class=" & """statistics""" & ">                                          " &
                            "                            <div class=" & """info""" & ">                                            " &
                            "	                            <div class=" & """icon icon-info""" & ">                               " &
                            "		                            <i class=" & """now-ui-icons ui-1_check""" & "></i>                " &
                            "	                            </div>                                                                 " &
                            "	                            <h3 class=" & """info-title""" & ">48</h3>                             " &
                            "	                            <h6 class=" & """stats-title""" & ">Concretadas</h6>                   " &
                            "                            </div>                                                                    " &
                            "                        </div>                                                                        " &
                            "                    </div>                                                                            " &
                            "                    <div class=" & """col-md-2""" & ">                                                " &
                            "                        <div class=" & """statistics""" & ">                                          " &
                            "                            <div class=" & """info""" & ">                                            " &
                            "	                            <div class=" & """icon icon-danger""" & ">                             " &
                            "		                            <i class=" & """now-ui-icons ui-1_simple-remove""" & "></i>        " &
                            "	                            </div>                                                                 " &
                            "	                            <h3 class=" & """info-title""" & ">123</h3>                            " &
                            "	                            <h6 class=" & """stats-title""" & ">Rechazadas</h6>                    " &
                            "                            </div>                                                                    " &
                            "                        </div>                                                                        " &
                            "                    </div>                                                                            " &
                            "                    <div class=" & """col-md-1""" & ">                                                " &
                            "                    </div>                                                                            " &
                            "                </div>                                                                                " &
                            "            </div>                                                                                    " &
                            "        </div>                                                                                        " &
                            "    </div>                                                                                            " &
                            "</div>                                                                                                "

            iAcumuladores &= "<div class=" & """row""" & ">                                                                      " &
                            "    <div class=" & """col-lg-6 col-md-6""" & ">                                                     " &
                            "        <div class=" & """card card-chart""" & ">                                                   " &
                            "            <div class=" & """card-header""" & ">                                                   " &
                            "                <h5 class=" & """card-category""" & ">Ranking de Ventas por punto de venta</h5>     " &
                            "                <h2 class=" & """card-title""" & ">12 M</h2>                                        " &
                            "            </div>                                                                                  " &
                            "            <div class=" & """card-body""" & ">                                                     " &
                            "                <div class=" & """chart-area""" & ">                                                " &
                            "                    <canvas id=" & """activeUsers""" & "></canvas>                                  " &
                            "                </div>                                                                              " &
                            "                <div class=" & """table-responsive""" & ">                                          " &
                            "                    <table class=" & """table""" & ">                                               " &
                            "                        <tbody>                                                                     " &
                            "                            <tr>                                                                    " &
                            "	                            <td>                                                                 " &
                            "		                            <div class=" & """flag""" & ">                                   " &
                            "			                            <img src=" & """../assets/img/apple-icon.png""" & ">         " &
                            "		                            </div>                                                           " &
                            "	                            </td>                                                                " &
                            "	                            <td>ALMAGRO</td>                                              " &
                            "	                            <td class=" & """text-right""" & ">7                                 " &
                            "	                            </td>                                                                " &
                            "	                            <td class=" & """text-right""" & ">58.33%                            " &
                            "	                            </td>                                                                " &
                            "                            </tr>                                                                   " &
                            "                            <tr>                                                                    " &
                            "	                            <td>                                                                 " &
                            "		                            <div class=" & """flag""" & ">                                   " &
                            "			                            <img src=" & """../assets/img/apple-icon.png""" & ">         " &
                            "		                            </div>                                                           " &
                            "	                            </td>                                                                " &
                            "	                            <td>BELGRANO</td>                                                   " &
                            "	                            <td class=" & """text-right""" & ">4.3                               " &
                            "	                            </td>                                                                " &
                            "	                            <td class=" & """text-right""" & ">35.84%                            " &
                            "	                            </td>                                                                " &
                            "                            </tr>                                                                   " &
                            "                            <tr>                                                                    " &
                            "	                            <td>                                                                 " &
                            "		                            <div class=" & """flag""" & ">                                   " &
                            "			                            <img src=" & """../assets/img/apple-icon.png""" & ">         " &
                            "		                            </div>                                                           " &
                            "	                            </td>                                                                " &
                            "	                            <td>CONSTITUCION</td>                                                " &
                            "	                            <td class=" & """text-right""" & ">0.5                               " &
                            "	                            </td>                                                                " &
                            "	                            <td class=" & """text-right""" & ">4.16%                             " &
                            "	                            </td>                                                                " &
                            "                            </tr>                                                                   " &
                            "                            <tr>                                                                    " &
                            "	                            <td>                                                                 " &
                            "		                            <div class=" & """flag""" & ">                                   " &
                            "			                            <img src=" & """../assets/img/apple-icon.png""" & ">         " &
                            "		                            </div>                                                           " &
                            "	                            </td>                                                                " &
                            "	                            <td>MENDOZA</td>                                                     " &
                            "	                            <td class=" & """text-right""" & ">0.2                               " &
                            "	                            </td>                                                                " &
                            "	                            <td class=" & """text-right""" & ">1.67%                             " &
                            "	                            </td>                                                                " &
                            "                            </tr>                                                                   " &
                            "                        </tbody>                                                                    " &
                            "                    </table>                                                                        " &
                            "                </div>                                                                              " &
                            "            </div>                                                                                  " &
                            "        </div>                                                                                      " &
                            "    </div>                                                                                          " &
                            "    <div class=" & """col-lg-4 col-md-6""" & " style=" & """display: none""" & ">                   " &
                            "        <div class=" & """card card-chart""" & ">                                                   " &
                            "            <div class=" & """card-header""" & ">                                                   " &
                            "                <h5 class=" & """card-category""" & ">Summer Email Campaign</h5>                    " &
                            "                <h2 class=" & """card-title""" & ">55,300</h2>                                      " &
                            "                <div class=" & """dropdown""" & ">                                                  " &
                            "                    <button type=" & """button""" & " class=" & """btn btn-round dropdown-toggle btn-outline-default btn-icon no-caret""" & " data-toggle=" & """dropdown""" & "> " &
                            "                        <i class=" & """now-ui-icons loader_gear""" & "></i>                                                                                                      " &
                            "                    </button>                                                                                                                                                     " &
                            "                    <div class=" & """dropdown-menu dropdown-menu-right""" & ">                                                                                                   " &
                            "                        <a class=" & """dropdown-item""" & " href=" & """#""" & ">Action</a>                                                                                      " &
                            "                        <a class=" & """dropdown-item""" & " href=" & """#""" & ">Another action</a>                                                                              " &
                            "                        <a class=" & """dropdown-item""" & " href=" & """#""" & ">Something else here</a>                                                                         " &
                            "                        <a class=" & """dropdown-item text-danger""" & " href=" & """#""" & ">Remove Data</a>                                                                     " &
                            "                    </div>                                                                                                                                                        " &
                            "                </div>                                                                                                                                                            " &
                            "            </div>                                                                                                                                                                " &
                            "            <div class=" & """card-body""" & ">                                                                                                                                   " &
                            "                <div class=" & """chart-area""" & ">                                                                                                                              " &
                            "                    <canvas id=" & """emailsCampaignChart""" & "></canvas>                                                                                                        " &
                            "                </div>                                                                                                                                                            " &
                            "                <div class=" & """card-progress""" & ">                                                                                                                           " &
                            "                    <div class=" & """progress-container""" & ">                                                                                                                  " &
                            "                        <span class=" & """progress-badge""" & ">Delivery Rate</span>                                                                                             " &
                            "                        <div class=" & """progress""" & ">                                                                                                                        " &
                            "                            <div class=" & """progress-bar""" & " role=" & """progressbar""" & " aria-valuenow=" & """60""" & " aria-valuemin=" & """0""" & " aria-valuemax=""" & " 100""" & " style=" & """width: 90%;""" & ">                      " &
                            "	                            <span class=" & """progress-value""" & ">90%</span>                                                                                                                                                                   " &
                            "                            </div>                                                                                                                                                                                                                   " &
                            "                        </div>                                                                                                                                                                                                                       " &
                            "                    </div>                                                                                                                                                                                                                           " &
                            "                    <div class=" & """progress-container progress-success""" & ">                                                                                                                                                                    " &
                            "                        <span class=" & """progress-badge""" & ">Open Rate</span>                                                                                                                                                                    " &
                            "                        <div class=" & """progress""" & ">                                                                                                                                                                                           " &
                            "                            <div class=" & """progress-bar progress-bar-warning""" & " role=" & """progressbar""" & " aria-valuenow=" & """60""" & " aria-valuemin=" & """0""" & " aria-valuemax=" & """100""" & " style=" & """width: 60%;""" & ">  " &
                            "	                            <span class=" & """progress-value""" & ">60%</span>                                                                                                                                                                   " &
                            "                            </div>                                                                                                                                                                                                                   " &
                            "                        </div>                                                                                                                                                                                                                       " &
                            "                    </div>                                                                                                                                                                                                                           " &
                            "                    <div class=" & """progress-container progress-info""" & ">                                                                                                                                                                       " &
                            "                        <span class=" & """progress-badge""" & ">Click Rate</span>                                                                                                                                                                   " &
                            "                        <div class=" & """progress""" & ">                                                                                                                                                                                           " &
                            "                            <div class=" & """progress-bar progress-bar-warning""" & " role=" & """progressbar""" & " aria-valuenow=" & """60""" & " aria-valuemin=" & """0""" & " aria-valuemax=" & """100""" & " style=" & """width: 12%;""" & ">  " &
                            "	                            <span class=" & """progress-value""" & ">12%</span>                                                                                                                                                                   " &
                            "                            </div>                                                                                                                                                                                                                   " &
                            "                        </div>                                                                                                                                                                                                                       " &
                            "                    </div>                                                                                                                                                                                                                           " &
                            "                    <div class=" & """progress-container progress-warning""" & ">                                                                                                                                                                    " &
                            "                        <span class=" & """progress-badge""" & ">Hard Bounce</span>                                                                                                                                                                  " &
                            "                        <div class=" & """progress""" & ">                                                                                                                                                                                           " &
                            "                            <div class=" & """progress-bar progress-bar-warning""" & " role=" & """progressbar""" & " aria-valuenow=" & """60""" & " aria-valuemin=" & """0""" & " aria-valuemax=" & """100""" & " style=" & """width: 5%;""" & ">   " &
                            "	                            <span class=" & """progress-value""" & ">5%</span>                                                                                                                                                                    " &
                            "                            </div>                                                                                                                                                                                                                   " &
                            "                        </div>                                                                                                                                                                                                                       " &
                            "                    </div>                                                                                                                                                                                                                           " &
                            "                    <div class=" & """progress-container progress-danger""" & ">                                                                                                                                                                     " &
                            "                        <span class=" & """progress-badge""" & ">Spam Report</span>                                                                                                                                                                  " &
                            "                        <div class=" & """progress""" & ">                                                                                                                                                                                           " &
                            "                            <div class=" & """progress-bar progress-bar-warning""" & " role=" & """progressbar""" & " aria-valuenow=" & """60""" & " aria-valuemin=" & """0""" & " aria-valuemax=" & """100""" & " style=" & """width: 0.11%;""" & ">" &
                            "	                            <span class=" & """progress-value""" & ">0.11%</span>              " &
                            "                            </div>                                                                " &
                            "                        </div>                                                                    " &
                            "                    </div>                                                                        " &
                            "                </div>                                                                            " &
                            "            </div>                                                                                " &
                            "            <div class=" & """card-footer""" & ">                                                 " &
                            "                <div class=" & """stats""" & ">                                                   " &
                            "                    <i class=" & """now-ui-icons arrows-1_refresh-69""" & "></i>Just Updated      " &
                            "                </div>                                                                            " &
                            "            </div>                                                                                " &
                            "        </div>                                                                                    " &
                            "    </div>                                                                                        " &
                            "    <div class=" & """col-lg-6 col-md-12""" & ">                                                  " &
                            "        <div class=" & """card card-chart""" & ">                                                 " &
                            "            <div class=" & """card-header""" & ">                                                 " &
                            "                <h5 class=" & """card-category""" & ">Ranking Morosidad por punto de venta</h5>   " &
                            "                <h2 class=" & """card-title""" & ">3 M</h2>                                       " &
                            "            </div>                                                                                " &
                            "            <div class=" & """card-body""" & ">                                                   " &
                            "                <div class=" & """chart-area""" & ">                                              " &
                            "                    <canvas id=" & """activeCountries""" & "></canvas>                            " &
                            "                </div>                                                                            " &
                            "                <div class=" & """table-responsive""" & ">                                        " &
                            "                    <table class=" & """table""" & ">                                             " &
                            "                        <tbody>                                                                   " &
                            "                            <tr>                                                                  " &
                            "	                            <td>VILLA BALLESTER</td>                                                   " &
                            "	                            <td class=" & """text-right""" & ">1.9                             " &
                            "	                            </td>                                                              " &
                            "	                            <td class=" & """text-right""" & ">63.33%                          " &
                            "	                            </td>                                                              " &
                            "                            </tr>                                                                 " &
                            "                            <tr>                                                                  " &
                            "	                            <td>LAFERRERE</td>                                              " &
                            "	                            <td class=" & """text-right""" & ">0.4                             " &
                            "	                            </td>                                                              " &
                            "	                            <td class=" & """text-right""" & ">13.34%                          " &
                            "	                            </td>                                                              " &
                            "                            </tr>                                                                 " &
                            "                            <tr>                                                                  " &
                            "	                            <td>SAN MARTIN</td>                                              " &
                            "	                            <td class=" & """text-right""" & ">0.4                             " &
                            "	                            </td>                                                              " &
                            "	                            <td class=" & """text-right""" & ">13.34%                          " &
                            "	                            </td>                                                              " &
                            "                            </tr>                                                                 " &
                            "                            <tr>                                                                  " &
                            "	                            <td>MATADEROS</td>                                           " &
                            "	                            <td class=" & """text-right""" & ">0.3                             " &
                            "	                            </td>                                                              " &
                            "	                            <td class=" & """text-right""" & ">10.00%                          " &
                            "	                            </td>                                                              " &
                            "                            </tr>                                                                 " &
                            "                        </tbody>                                                                  " &
                            "                    </table>                                                                      " &
                            "                </div>                                                                            " &
                            "            </div>                                                                                " &
                            "        </div>                                                                                    " &
                            "    </div>                                                                                        " &
                            "</div>                                                                                            "

            iAcumuladores &= "<div class=" & """row""" & ">                                                                      " &
                           "    <div class=" & """col-lg-12 col-md-12""" & ">                                                    " &
                           "        <div class=" & """card card-chart""" & ">                                                    " &
                           "            <div class=" & """card-header""" & ">                                                    " &
                           "                <h5 class=" & """card-category""" & ">Mapa</h5>                                      " &
                           "            </div>                                                                                   " &
                           "            <div class=" & """card-body""" & ">                                                      " &
                           "                <div id=" & """map""" & " style=" & """width: 100%; height: 500px;""" & "></div>     " &
                           "            </div>                                                                                   " &
                           "        </div>                                                                                       " &
                           "        <div class=" & """card-footer""" & "></div>                                                  " &
                           "    </div>                                                                                           " &
                           "</div>                                                                                               "

            Return iAcumuladores

        Catch exception As Exception
            Throw New DashboardNoEncontradoException(exception)
        Finally
            iDashBoard.accesoDatos = Nothing
        End Try
    End Function
#End Region

#Region "Tablero Acumulador"
    Public Sub generarTableros(ByRef eAccesoDatos As accesoDatos)
        Dim iTableroAcumulador As New TipoTableroAcumulador

        Try
            iTableroAcumulador.accesoDatos = eAccesoDatos
            iTableroAcumulador.generarTableros()

        Catch Exception As Exception
            Throw New RootException(Exception)
        Finally
            iTableroAcumulador.accesoDatos = Nothing
            iTableroAcumulador = Nothing
        End Try
    End Sub

    Public Sub generarTablerosDinamicosPorUsuario(ByRef eAccesoDatos As accesoDatos, eUsuario As Usuario, eFecha As Date)
        Dim iTableroAcumulador As New TipoTableroAcumulador

        Try

            iTableroAcumulador.accesoDatos = eAccesoDatos
            iTableroAcumulador.generarTablerosDinamicosPorUsuario(eUsuario, eFecha)

        Catch Exception As Exception
            Throw New RootException(Exception)
        Finally
            iTableroAcumulador.accesoDatos = Nothing
            iTableroAcumulador = Nothing
        End Try
    End Sub

    Public Sub generarTablerosEstaticosPorUsuario(ByRef eAccesoDatos As accesoDatos, eUsuario As Usuario, eFecha As Date)
        Dim iTableroAcumulador As New TipoTableroAcumulador

        Try
            iTableroAcumulador.accesoDatos = eAccesoDatos
            iTableroAcumulador.generarTablerosEstaticosPorUsuario(eUsuario, eFecha)

        Catch Exception As Exception
            Throw New RootException(Exception)
        Finally
            iTableroAcumulador.accesoDatos = Nothing
            iTableroAcumulador = Nothing
        End Try
    End Sub

#End Region

#Region "Registro Usuario"

    Public Sub crearRegistroUsuario(ByVal eRegistroUsuario As RegistroUsuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos()
            iAccesoDatos.beginTransaction()
            eRegistroUsuario.accesoDatos = iAccesoDatos
            crearRegistroUsuario(iAccesoDatos, eRegistroUsuario)
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New UsuarioNoCreadoException(exception)
        Finally
            eRegistroUsuario.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearRegistroUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eRegistroUsuario As RegistroUsuario)
        Try
            eRegistroUsuario.accesoDatos = eAccesoDatos
            eRegistroUsuario.crear()

        Catch exception As Exception
            Throw New UsuarioNoCreadoException(exception)
        Finally
            eRegistroUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarRegistroUsuario(ByVal eRegistroUsuario As RegistroUsuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eRegistroUsuario.accesoDatos = iAccesoDatos
            eliminarRegistroUsuario(iAccesoDatos, eRegistroUsuario)
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New UsuarioNoEliminadoException(exception)
        Finally
            eRegistroUsuario.accesoDatos = Nothing
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarRegistroUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eRegistroUsuario As RegistroUsuario)
        Try
            eRegistroUsuario.accesoDatos = eAccesoDatos
            eRegistroUsuario.eliminar()
        Catch exception As Exception
            Throw New UsuarioNoEliminadoException(exception)
        Finally
            eRegistroUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarRegistroUsuario(ByVal eRegistroUsuario As RegistroUsuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            eRegistroUsuario.accesoDatos = iAccesoDatos
            eRegistroUsuario.modificar()
        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eRegistroUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub cerrarRegistroUsuario(ByVal eRegistroUsuario As RegistroUsuario)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            cerrarRegistroUsuario(iAccesoDatos, eRegistroUsuario)

        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            eRegistroUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub cerrarRegistroUsuario(eAccesoDatos As accesoDatos, ByVal eRegistroUsuario As RegistroUsuario)

        Try
            eRegistroUsuario.accesoDatos = eAccesoDatos
            eRegistroUsuario.cerrar()

        Catch UsuarioNoModificadoException As UsuarioNoModificadoException
            Throw UsuarioNoModificadoException
        Finally
            eRegistroUsuario.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerRegistroUsuario(ByVal eRegistroUsuario As RegistroUsuario) As RegistroUsuario
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return obtenerRegistroUsuario(iAccesoDatos, eRegistroUsuario)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerRegistroUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eRegistroUsuario As RegistroUsuario) As RegistroUsuario
        Try
            eRegistroUsuario.accesoDatos = eAccesoDatos
            Return eRegistroUsuario.obtenerRegistroUsuario()
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            eRegistroUsuario.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerRegistrosUsuariosGrilla(ByVal eRegistrosUsuariosGrillaVO As RegistrosUsuariosGrillaVO) As DataSet
        Dim iRegistroUsuario As New RegistroUsuario
        Try
            Return iRegistroUsuario.obtenerRegistrosUsuariosGrilla(eRegistrosUsuariosGrillaVO)
        Catch Exception As Exception
            Throw New UsuarioNoEncontradoException(Exception)
        Finally
            iRegistroUsuario = Nothing
        End Try
    End Function

    Public Sub validarSesionUsuario(ByVal eAccesoDatos As accesoDatos, ByVal eRegistroUsuario As RegistroUsuario)
        Try
            eRegistroUsuario.accesoDatos = eAccesoDatos
            eRegistroUsuario.validarSesionUsuario()

        Catch exception As Exception
            Throw New UsuarioNoCreadoException(exception)
        Finally
            eRegistroUsuario.accesoDatos = Nothing
        End Try
    End Sub
    Public Function obtenerUltimaFechaInicioSesion(ByVal eAccesoDatos As accesoDatos, ByVal eRegistroUsuario As RegistroUsuario) As Date
        Try
            eRegistroUsuario.accesoDatos = eAccesoDatos
            Return eRegistroUsuario.obtenerUltimaFechaInicioSesion()

        Catch exception As Exception
            Throw New UsuarioNoCreadoException(exception)
        Finally
            eRegistroUsuario.accesoDatos = Nothing
        End Try
    End Function

#End Region

#Region "Icono"
    Public Function obtenerIcono(ByVal eIcono As Icono) As Icono
        Try
            Return eIcono.obtenerIcono
        Catch exception As Exception
            Throw New IconoNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerIcono(eAccesoDatos As accesoDatos, ByVal eIcono As Icono) As Icono
        Try
            eIcono.accesoDatos = eAccesoDatos
            Return eIcono.obtenerIcono

        Catch exception As Exception
            Throw New IconoNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerIconosLista(ByVal eIcono As Icono) As IDataReader
        Try
            Return eIcono.obtenerIconosLista()
        Catch exception As Exception
            Throw New IconoNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerIconosGrilla(ByVal eIcono As Icono) As DataSet
        Try
            Return eIcono.obtenerIconosGrilla()
        Catch exception As Exception
            Throw New IconoNoEncontradoException(exception)
        End Try
    End Function
#End Region

#Region "Token Servicio"
    Public Function obtenerTokenServicio(eTokenServicioVO As TokenServicioVO) As TokenServicioVO

        Try

            Return TokenServicio.obtenerTokenServicio(eTokenServicioVO)

        Catch ex As Exception
            Throw New UsuarioNoModificadoException(ex)
        Finally

        End Try
    End Function

    Public Function crearTokenServicio(eTokenServicioVO As TokenServicioVO) As TokenServicioVO

        Try

            Return TokenServicio.crearTokenServicio(eTokenServicioVO)

        Catch ex As Exception
            Throw New UsuarioNoModificadoException(ex)
        Finally

        End Try
    End Function
#End Region

#End Region

End Class
