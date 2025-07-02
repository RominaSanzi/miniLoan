Imports di.financiera.entidades
Imports di.financiera.seguridad
Imports di.financiera.utils
Imports di.financiera.reglasnegocios
Imports System.IO
Imports System.Configuration
Imports System.Collections.Generic

Public Class frmBarraTareasProcesosAutomaticos

    Private WithEvents iNotificacion As New NotifyIcon()
    Private iMenuContextual As New ContextMenu()
    Private iTipoProceso As FuncionComun.enumProcesoAutomatico
    Private iOpcion As String
    Private iOpcion2 As String
    Private iOpcion3 As String

#Region "Metodos"

    Private Sub ejecutarProceso()
        Try

            generarProcesoAutomatico()

        Catch exception As Exception
            'MsgBox(Log.obtenerErrorAplicacion(exception, Me.ToString).mensajeUsuario, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub generarProcesoAutomatico()
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        Dim iAdministradorProcesosAutomaticos As New AdministradorProcesosAutomaticos
        Dim iMonitoreoProcesos As New MonitoreoProcesos
        Dim iParametro As New Parametro
        Dim iUsuario As New di.financiera.seguridad.Usuario
        Dim iNombreProceso, iObservacion As String
        Dim iInicio, iFin As Date


        Try

            With iMonitoreoProcesos
                .Fecha = Today
                .FechaInicio = Now
                .Estado = "PROCESANDO"
            End With

            iInicio = Now
            iUsuario.id = ConfigurationManager.AppSettings("usuarioDefecto")
            iUsuario = iAdministradorUsuarios.obtenerUsuario(iUsuario)

            Select Case iTipoProceso
                Case FuncionComun.enumProcesoAutomatico.PROCESOAUTOMATICO

                    iNombreProceso = "PROCESOAUTOMATICO"
                    iMonitoreoProcesos.Proceso = iNombreProceso
                    iAdministradorProcesosAutomaticos.crearMonitoreoprocesos(iMonitoreoProcesos)

                    FuncionComun.loguearProcesoWindows(Format(iInicio, "dd/MM/yyyy HH:mm:ss") & vbTab & "" & vbTab & "" & vbTab & iNombreProceso & vbTab & "INICIO PROCESO" & vbTab)
                    iAdministradorProcesosAutomaticos.procesoAutomatico(iUsuario)

                    iMonitoreoProcesos.FechaFin = Now
                    iMonitoreoProcesos.Estado = "FINALIZADO"
                    iAdministradorProcesosAutomaticos.modificarEstadoMonitoreoprocesos(iMonitoreoProcesos)

            End Select

            iFin = Now
            iObservacion = "OK"

            Environment.ExitCode = 0

        Catch exception As Exception

            Environment.ExitCode = 1

            If Not IsNothing(iMonitoreoProcesos) Then
                iMonitoreoProcesos.FechaFin = Now
                iMonitoreoProcesos.Estado = "ERROR"
                iMonitoreoProcesos.Mensaje = FuncionComun.obtenerMotivoOriginal(exception, True)
                iAdministradorProcesosAutomaticos.modificarEstadoMonitoreoprocesos(iMonitoreoProcesos)
            End If

            iFin = Now
            iObservacion = Log.obtenerErrorAplicacion(exception, Me.ToString).mensajeUsuario
            EnviaMail.enviarMail(iUsuario.mail, "Proceso Automatico", Log.obtenerErrorAplicacion(exception, Me.ToString).mensajeUsuario)
        Finally
            FuncionComun.loguearProcesoWindows(Format(iInicio, "dd/MM/yyyy HH:mm:ss") & vbTab & Format(iFin, "dd/MM/yyyy HH:mm:ss") & vbTab & FuncionComun.diferenciaTiempo(iInicio, iFin) & vbTab & iNombreProceso & vbTab & iObservacion & vbTab)
            iAdministradorProcesosAutomaticos = Nothing
            iAdministradorParametros = Nothing
            iAdministradorUsuarios = Nothing
            iMonitoreoProcesos = Nothing
            iParametro = Nothing
            iUsuario = Nothing
        End Try
    End Sub

    Private Sub salir()
        Me.Close()
    End Sub

#End Region

    Private Sub frmBarraTareas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        iMenuContextual.MenuItems.Add("&Restaurar", New EventHandler(AddressOf Restaurar_Click))
        iMenuContextual.MenuItems(0).DefaultItem = True
        iMenuContextual.MenuItems.Add("-")

        With iNotificacion
            .Icon = Me.Icon
            .ContextMenu = Me.iMenuContextual
            .Text = ".:: Sistema de Procesos automaticos ::."
            .Visible = True
            iNotificacion.BalloonTipText = ".:: Sistema de Procesos automaticos ::."
            iNotificacion.ShowBalloonTip(3000)
        End With
        ejecutarProceso()
        salir()
    End Sub

    Private Sub Restaurar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles iNotificacion.DoubleClick
        Show()
        WindowState = FormWindowState.Normal
        Activate()
    End Sub

    Public Sub New(eTipoProceso As FuncionComun.enumProcesoAutomatico, Optional eOpcion As String = "", Optional eOpcion2 As String = "", Optional eOpcion3 As String = "")

        iTipoProceso = eTipoProceso
        iOpcion = eOpcion
        iOpcion2 = eOpcion2
        iOpcion3 = eOpcion3
        InitializeComponent()
    End Sub
End Class