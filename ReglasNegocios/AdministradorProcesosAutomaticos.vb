Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.seguridad
Imports di.financiera.utils

Public Class AdministradorProcesosAutomaticos

#Region "Configuracion Procesos Automaticos"

    Public Function obtenerConfiguracionProcesosAutomaticos(ByVal eConfiguracionProcesosAutomaticos As ConfiguracionProcesosAutomaticos, Optional eObtenerEntidades As Boolean = True) As ConfiguracionProcesosAutomaticos
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return obtenerConfiguracionProcesosAutomaticos(iAccesoDatos, eConfiguracionProcesosAutomaticos, eObtenerEntidades)
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerConfiguracionProcesosAutomaticos(ByVal eAccesoDatos As accesoDatos, ByVal eConfiguracionProcesosAutomaticos As ConfiguracionProcesosAutomaticos, Optional eObtenerEntidades As Boolean = True) As ConfiguracionProcesosAutomaticos
        Try
            eConfiguracionProcesosAutomaticos.accesoDatos = eAccesoDatos
            Return eConfiguracionProcesosAutomaticos.obtenerConfiguracionProcesosAutomaticos(eObtenerEntidades)
        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Function

    Public Sub crearConfiguracionProcesosAutomaticos(ByVal eConfiguracionProcesosAutomaticos As ConfiguracionProcesosAutomaticos)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eConfiguracionProcesosAutomaticos.accesoDatos = iAccesoDatos
            crearConfiguracionProcesosAutomaticos(iAccesoDatos, eConfiguracionProcesosAutomaticos)
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(exception)
        Finally
            eConfiguracionProcesosAutomaticos.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearConfiguracionProcesosAutomaticos(ByVal eAccesoDatos As accesoDatos, ByVal eConfiguracionProcesosAutomaticos As ConfiguracionProcesosAutomaticos)
        Try
            eConfiguracionProcesosAutomaticos.accesoDatos = eAccesoDatos
            eConfiguracionProcesosAutomaticos.crear()
        Catch Exception As Exception
            Throw New RootException(Exception)
        Finally
            eConfiguracionProcesosAutomaticos.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarConfiguracionProcesosAutomaticos(ByVal eConfiguracionProcesosAutomaticos As ConfiguracionProcesosAutomaticos)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eConfiguracionProcesosAutomaticos.accesoDatos = iAccesoDatos
            eConfiguracionProcesosAutomaticos.modificar()
            iAccesoDatos.commit()

        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(Exception)
        Finally
            eConfiguracionProcesosAutomaticos.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerConfiguracionProcesosAutomaticosParaProceso(eAccesoDatos As accesoDatos) As DataSet
        Dim iConfiguracionProcesosAutomaticos As New ConfiguracionProcesosAutomaticos
        Try
            iConfiguracionProcesosAutomaticos.accesoDatos = eAccesoDatos
            Return iConfiguracionProcesosAutomaticos.obtenerConfiguracionProcesosAutomaticosParaProceso()
        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            iConfiguracionProcesosAutomaticos = Nothing
        End Try
    End Function

    Public Sub eliminarConfiguracionProcesosAutomaticos(ByVal eConfiguracionProcesosAutomaticos As ConfiguracionProcesosAutomaticos)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eConfiguracionProcesosAutomaticos.accesoDatos = iAccesoDatos
            eConfiguracionProcesosAutomaticos.eliminar()
            iAccesoDatos.commit()

        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New RootException(Exception)
        Finally
            eConfiguracionProcesosAutomaticos.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub
#End Region

#Region "Monitoreo de procesos"
    Public Function obtenerMonitoreoProcesos(eMonitoreoprocesos As MonitoreoProcesos) As MonitoreoProcesos
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return obtenerMonitoreoProcesos(iAccesoDatos, eMonitoreoprocesos)
        Catch exception As Exception
            Throw New MonitoreoProcesosNoEncontradoException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerMonitoreoProcesos(eAccesoDatos As accesoDatos, eMonitoreoprocesos As MonitoreoProcesos) As MonitoreoProcesos
        Try
            eMonitoreoprocesos.accesoDatos = eAccesoDatos
            Return eMonitoreoprocesos.obtenerMonitoreoprocesos
        Catch exception As Exception
            Throw New MonitoreoProcesosNoEncontradoException(exception)
        End Try
    End Function

    Public Sub crearMonitoreoprocesos(eMonitoreoprocesos As MonitoreoProcesos)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            crearMonitoreoprocesos(iAccesoDatos, eMonitoreoprocesos)
        Catch Exception As Exception
            Throw New MonitoreoProcesosNoCreadoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearMonitoreoprocesos(eAccesoDatos As accesoDatos, eMonitoreoprocesos As MonitoreoProcesos)
        Try
            eMonitoreoprocesos.accesoDatos = eAccesoDatos
            eMonitoreoprocesos.crear()
        Catch Exception As Exception
            Throw New MonitoreoProcesosNoCreadoException(Exception)
        Finally
            eMonitoreoprocesos.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarMonitoreoprocesos(eMonitoreoprocesos As MonitoreoProcesos)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            modificarMonitoreoprocesos(iAccesoDatos, eMonitoreoprocesos)
        Catch Exception As Exception
            Throw New MonitoreoProcesosNoModificadoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarMonitoreoprocesos(eAccesoDatos As accesoDatos, eMonitoreoprocesos As MonitoreoProcesos)

        Try
            eMonitoreoprocesos.accesoDatos = eAccesoDatos
            eMonitoreoprocesos.modificar()
        Catch Exception As Exception
            Throw New MonitoreoProcesosNoModificadoException(Exception)
        Finally
            eMonitoreoprocesos.accesoDatos = Nothing
        End Try
    End Sub
    Public Sub modificarEstadoMonitoreoprocesos(eMonitoreoprocesos As MonitoreoProcesos)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            modificarEstadoMonitoreoprocesos(iAccesoDatos, eMonitoreoprocesos)
        Catch Exception As Exception
            Throw New MonitoreoProcesosNoModificadoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarEstadoMonitoreoprocesos(eAccesoDatos As accesoDatos, eMonitoreoprocesos As MonitoreoProcesos)

        Try
            eMonitoreoprocesos.accesoDatos = eAccesoDatos
            eMonitoreoprocesos.modificarEstado()
        Catch Exception As Exception
            Throw New MonitoreoProcesosNoModificadoException(Exception)
        Finally
            eMonitoreoprocesos.accesoDatos = Nothing
        End Try
    End Sub
#End Region

#Region "Proceso sincronizacion facturacion"
    Public Sub procesoAutomatico(eUsuario As seguridad.Usuario)
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iAdministradorNiveles As New AdministradorNiveles
        Dim iAccesoDatos As accesoDatos
        Dim iParametro As New Parametro
        Dim iParametrosFacturacionElectronica, iResultado As String

        Try
            iAccesoDatos = New accesoDatos

            iParametro.descripcion = "parametroFacturacionElectronica"
            iParametro = iAdministradorParametros.obtenerParametro(iParametro, New GrupoEmpresas)
            iParametrosFacturacionElectronica = iParametro.valor

            'Logica del Proceso

        Catch Exception As Exception
            EnviaMail.enviarMail(eUsuario.mail, "PROCESO AUTOMATICO", iResultado)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAdministradorParametros = Nothing
            iParametrosFacturacionElectronica = Nothing
            iAdministradorNiveles = Nothing
            iParametro = Nothing
        End Try
    End Sub

#End Region

End Class