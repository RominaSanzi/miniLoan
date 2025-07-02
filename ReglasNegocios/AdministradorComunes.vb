Imports di.financiera.excepciones
Imports di.financiera.entidades
Imports di.financiera.datos
Imports di.financiera.seguridad
Imports System.IO
Imports System.Configuration
Imports System.Collections.Generic
Imports di.financiera.utils
Imports ClosedXML.Excel
Public Class AdministradorComunes

#Region "Metodos"

#Region "Domicilio"

    Public Sub modificarDomicilio(eAccesoDatos As accesoDatos, ByVal eDomicilio As Domicilio)
        Try
            eDomicilio.accesoDatos = eAccesoDatos
            eDomicilio.modificar()
        Catch DomicilioNoModificadoException As DomicilioNoModificadoException
            Throw DomicilioNoModificadoException
        Catch Exception As Exception
            Throw New DomicilioNoModificadoException(Exception)
        End Try
    End Sub

    Public Function obtenerDomicilio(ByVal eDomicilio As Domicilio) As Domicilio
        Try
            Return eDomicilio.obtenerDomicilio
        Catch DomicilioNoEncontradoException As DomicilioNoEncontradoException
            Throw DomicilioNoEncontradoException
        Catch Exception As Exception
            Throw New DomicilioNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerDomicilio(eAccesoDatos As accesoDatos, ByVal eDomicilio As Domicilio) As Domicilio
        Try
            eDomicilio.accesoDatos = eAccesoDatos
            Return eDomicilio.obtenerDomicilio
        Catch DomicilioNoEncontradoException As DomicilioNoEncontradoException
            Throw DomicilioNoEncontradoException
        Catch Exception As Exception
            Throw New DomicilioNoEncontradoException(Exception)
        End Try
    End Function
    Public Function celularExistenteParaOtraPersona(eAccesoDatos As accesoDatos, eDocumento As Long, ByVal eTelefono As String) As Boolean
        Dim iDomicilio As New Domicilio

        Try
            iDomicilio.accesoDatos = eAccesoDatos
            Return iDomicilio.celularExistenteParaOtraPersona(eDocumento, eTelefono)
        Catch Exception As Exception
            Throw New DomicilioNoEncontradoException(Exception)
        Finally
            iDomicilio.accesoDatos = Nothing
            iDomicilio = Nothing
        End Try
    End Function

    Public Function obtenerDomicilioSoloIds(ByVal eDomicilio As Domicilio) As Domicilio
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return obtenerDomicilioSoloIds(iAccesoDatos, eDomicilio)
        Catch Exception As Exception
            Throw New DomicilioNoEncontradoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerDomicilioSoloIds(eAccesoDatos As accesoDatos, ByVal eDomicilio As Domicilio) As Domicilio
        Try
            eDomicilio.accesoDatos = eAccesoDatos
            Return eDomicilio.obtenerDomicilioSoloIds
        Catch Exception As Exception
            Throw New DomicilioNoEncontradoException(Exception)
        Finally
            eDomicilio.accesoDatos = Nothing
        End Try
    End Function

#End Region

#Region "Estado"
    Public Function obtenerEstados(ByVal eEstado As Estado) As Collection
        Try
            Return Estado.obtenerEstados
        Catch EstadoNoEncontradoException As Exception
            Throw New EstadoNoEncontradoException(EstadoNoEncontradoException)
        End Try
    End Function

#End Region

#Region "Datos Anexos"
    Public Sub crearDatoAnexo(ByVal eDatoAnexo As DatoAnexo)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            crearDatoAnexo(iAccesoDatos, eDatoAnexo)
        Catch Exception As Exception
            Throw New CartaNoCreadaException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearDatoAnexo(ByVal eAccesoDatos As accesoDatos, ByVal eDatoAnexo As DatoAnexo)
        Try
            eDatoAnexo.accesoDatos = eAccesoDatos
            eDatoAnexo.crear()
        Catch Exception As Exception
            Throw New CartaNoCreadaException(Exception)
        Finally
            eDatoAnexo.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarDatoAnexo(ByVal eDatoAnexo As DatoAnexo)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            modificarDatoAnexo(iAccesoDatos, eDatoAnexo)
        Catch Exception As Exception
            Throw New CartaNoCreadaException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarDatoAnexo(ByVal eAccesoDatos As accesoDatos, ByVal eDatoAnexo As DatoAnexo)
        Try
            eDatoAnexo.accesoDatos = eAccesoDatos
            eDatoAnexo.modificar()
        Catch Exception As Exception
            Throw New CartaNoCreadaException(Exception)
        Finally
            eDatoAnexo.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarDatoAnexo(ByVal eDatoAnexo As DatoAnexo)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            eliminarDatoAnexo(iAccesoDatos, eDatoAnexo)
        Catch Exception As Exception
            Throw New CartaNoCreadaException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarDatoAnexo(ByVal eAccesoDatos As accesoDatos, ByVal eDatoAnexo As DatoAnexo)
        Try
            eDatoAnexo.accesoDatos = eAccesoDatos
            eDatoAnexo.eliminar()
        Catch Exception As Exception
            Throw New CartaNoCreadaException(Exception)
        Finally
            eDatoAnexo.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerDatosAnexosPorEntidad(ByVal eAccesoDatos As accesoDatos, ByVal eDatoAnexo As DatoAnexo) As List(Of DatoAnexo)
        Try
            eDatoAnexo.accesoDatos = eAccesoDatos
            Return eDatoAnexo.obtenerDatosAnexosPorEntidad()
        Catch Exception As Exception
            Throw New CartaNoCreadaException(Exception)
        Finally
            eDatoAnexo.accesoDatos = Nothing
        End Try
    End Function

#End Region

#Region "Tipo Datos Anexos"

    Public Sub crearTipoDatoAnexo(ByVal eTipoDatoAnexo As TipoDatoAnexo)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            crearTipoDatoAnexo(eTipoDatoAnexo, iAccesoDatos)

        Catch Exception As Exception
            Throw New TipoDatoAnexoNoCreadoException(Exception)
        Finally
            eTipoDatoAnexo.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearTipoDatoAnexo(eTipoDatoAnexo As TipoDatoAnexo, eAccesoDatos As accesoDatos)
        Try
            eTipoDatoAnexo.accesoDatos = eAccesoDatos
            eTipoDatoAnexo.crear()
        Catch Exception As Exception
            Throw New TipoDatoAnexoNoCreadoException(Exception)
        Finally
            eTipoDatoAnexo.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarTipoDatoAnexo(ByVal eTipoDatoAnexo As TipoDatoAnexo)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            iAccesoDatos.beginTransaction()

            modificarTipoDatoAnexo(eTipoDatoAnexo, iAccesoDatos)

            iAccesoDatos.commit()

        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New TipoDatoAnexoNoModificadoException(Exception)
        Finally
            eTipoDatoAnexo.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarTipoDatoAnexo(eTipoDatoAnexo As TipoDatoAnexo, eAccesoDatos As accesoDatos)
        Try
            eTipoDatoAnexo.accesoDatos = eAccesoDatos
            eTipoDatoAnexo.modificar()
        Catch Exception As Exception
            Throw New TipoDatoAnexoNoModificadoException(Exception)
        Finally
            eTipoDatoAnexo.accesoDatos = Nothing
        End Try
    End Sub


    Public Function obtenerTipoDatoAnexo(ByVal eTipoDatoAnexo As TipoDatoAnexo) As TipoDatoAnexo
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return obtenerTipoDatoAnexo(eTipoDatoAnexo, iAccesoDatos)
        Catch Exception As Exception
            Throw New TipoDatoAnexoNoEncontradoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerTipoDatoAnexo(eTipoDatoAnexo As TipoDatoAnexo, eAccesoDatos As accesoDatos) As TipoDatoAnexo
        Try
            eTipoDatoAnexo.accesoDatos = eAccesoDatos
            Return eTipoDatoAnexo.obtenerTipoDatoAnexo()
        Catch Exception As Exception
            Throw New TipoDatoAnexoNoEncontradoException(Exception)
        Finally
            eTipoDatoAnexo.accesoDatos = Nothing
        End Try
    End Function

    Public Sub eliminarTipoDatoAnexo(eTipoDatoAnexo As TipoDatoAnexo)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            eliminarTipoDatoAnexo(eTipoDatoAnexo, iAccesoDatos)

        Catch Exception As Exception
            Throw New TipoDatoAnexoNoEliminadoException(Exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarTipoDatoAnexo(eTipoDatoAnexo As TipoDatoAnexo, eAccesoDatos As accesoDatos)
        Try
            eTipoDatoAnexo.accesoDatos = eAccesoDatos
            eTipoDatoAnexo.eliminar()
        Catch Exception As Exception
            Throw New TipoDatoAnexoNoEliminadoException(Exception)
        Finally
            eTipoDatoAnexo.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerTiposDatoasAnexosPorEntidad(eTipoDatoAnexo As TipoDatoAnexo) As IDataReader
        Try
            Return eTipoDatoAnexo.obtenerTiposDatosAnexosPorEntidad()
        Catch Exception As Exception
            Throw New TipoDatoAnexoNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerTipoDatoAnexos(eTipoDatoAnexo As TipoDatoAnexo, Optional eEsObligatorio As Boolean = False, Optional eEsRequeridoEnCarga As Boolean = False) As List(Of TipoDatoAnexo)
        Try
            Return eTipoDatoAnexo.obtenerTipoDatoAnexos(eEsObligatorio, eEsRequeridoEnCarga)
        Catch Exception As Exception
            Throw New TipoDatoAnexoNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerTiposDatosAnexosGrilla(eTipoDatoAnexo As TipoDatoAnexo) As DataSet
        Try
            Return eTipoDatoAnexo.obtenerTiposDatosAnexosGrilla
        Catch exception As Exception
            Throw New TipoDatoAnexoNoEncontradoException(exception)
        End Try
    End Function
#End Region

#Region "Estado Usuario"
    Public Function obtenerEstadoCivil(ByVal eEstadoUsuario As EstadoUsuario) As EstadoUsuario
        Try
            Return eEstadoUsuario.obtenerEstadoUsuario
        Catch EstadoUsuarioNoEncontradoException As EstadoUsuarioNoEncontradoException
            Throw EstadoUsuarioNoEncontradoException
        Catch Exception As Exception
            Throw New EstadoUsuarioNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerEstadosUsuario(ByVal eEstadoUsuario As EstadoUsuario) As IDataReader
        Try
            Return eEstadoUsuario.obtenerEstadosUsuario
        Catch EstadoUsuarioNoEncontradoException As EstadoUsuarioNoEncontradoException
            Throw EstadoUsuarioNoEncontradoException
        Catch Exception As Exception
            Throw New EstadoUsuarioNoEncontradoException(Exception)
        End Try
    End Function
#End Region

#Region "Boletin"

    Public Function obtenerDeudorBoletin(ByVal eConsultaBoletinVO As ConsultaBoletinVO, Optional ByVal eConexion As accesoDatos = Nothing, Optional ByVal eCodigoFinancieraNoBuscar As String = Nothing) As ConsultaBoletinVO
        Dim iDeudor As Deudor
        Dim i As Integer

        Try
            iDeudor = New Camara
            iDeudor.accesoDatos = eConexion
            iDeudor.documento = eConsultaBoletinVO.documento
            iDeudor.nombre = eConsultaBoletinVO.nombre
            Dim iCamara As Camara = iDeudor.obtenerDeudor(eCodigoFinancieraNoBuscar)
            eConsultaBoletinVO.documento = iCamara.documento
            eConsultaBoletinVO.nombre = iCamara.nombre
            eConsultaBoletinVO.financieras = iCamara.financieras

            iDeudor = New Veraz
            iDeudor.accesoDatos = eConexion
            iDeudor.documento = eConsultaBoletinVO.documento
            Dim iVeraz As Veraz = iDeudor.obtenerDeudor()
            eConsultaBoletinVO.importe = iVeraz.importe

            For i = 1 To eConsultaBoletinVO.financieras.Count
                If Trim(eConsultaBoletinVO.financieras.Item(i)) = iVeraz.financiera Then
                    iVeraz.financiera = Nothing
                End If
            Next i

            If iVeraz.financiera <> Nothing Then
                eConsultaBoletinVO.financieras.Add(iVeraz.financiera)
            End If

            If iCamara.mas Then eConsultaBoletinVO.financieras.Add("+")

            Return eConsultaBoletinVO

        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            If IsNothing(DeudorNoEncontradoException.originalCause) Then
                If iDeudor.isCamara Then
                    iDeudor = New Veraz
                    iDeudor.accesoDatos = eConexion
                    iDeudor.documento = eConsultaBoletinVO.documento
                    iDeudor.nombre = eConsultaBoletinVO.nombre
                    Dim iVeraz As Veraz = iDeudor.obtenerDeudor()
                    eConsultaBoletinVO.documento = iVeraz.documento
                    eConsultaBoletinVO.nombre = iVeraz.nombre
                    eConsultaBoletinVO.importe = iVeraz.importe
                    eConsultaBoletinVO.financieras = New Collection
                    eConsultaBoletinVO.financieras.Add(iVeraz.financiera)
                    Return eConsultaBoletinVO
                Else
                    Return eConsultaBoletinVO
                End If
            Else
                Throw New DeudorNoEncontradoException(DeudorNoEncontradoException)
            End If
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerDeudor(eAccesoDatos As accesoDatos, ByVal eDeudor As Deudor) As Deudor
        Try
            eDeudor.accesoDatos = eAccesoDatos
            Return eDeudor.obtenerDeudor()
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        Finally
            eDeudor.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerDeudor(ByVal eDeudor As Deudor) As Deudor
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            Return obtenerDeudor(iAccesoDatos, eDeudor)
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerDeudores(ByVal eDeudor As Deudor) As DataSet
        Try
            Return eDeudor.obtenerDeudores
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerDeudores(ByVal eAccesoDatos As accesoDatos, ByVal eDeudor As Deudor) As DataSet
        Try
            eDeudor.accesoDatos = eAccesoDatos
            Return eDeudor.obtenerDeudores
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        End Try
    End Function
#End Region

#Region "Veraz"
    Public Function obtenerVerazTarjeta(ByVal eDocumento As Long) As Boolean
        Dim iVeraz As New Veraz
        Try
            Return iVeraz.obtenerDeudorTarjeta(eDocumento)
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        Finally
            iVeraz = Nothing
        End Try
    End Function

    Public Function insertarVeraz(ByVal ePathArchivoVerazZipeado As String, ByVal eEliminarTodo As Boolean) As String
        Dim iVeraz As New Veraz
        Dim iAccesoDatos As accesoDatos
        Dim iResultado As String

        Try

            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            iVeraz.accesoDatos = iAccesoDatos
            iResultado = iVeraz.insertarVeraz(ePathArchivoVerazZipeado, eEliminarTodo)
            iVeraz.accesoDatos = Nothing
            iAccesoDatos.commit()

            Return iResultado

        Catch DeudorNoInsertadoException As DeudorNoInsertadoException
            iAccesoDatos.rollback()
            Throw DeudorNoInsertadoException
        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New DeudorNoInsertadoException(Exception)
        Finally
            iVeraz = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function


    Public Function obtenerVeraz(ByVal eDocumento As Long) As String
        Dim iVeraz As New Veraz
        Try
            Return iVeraz.obtenerDeudoresFinanciera(eDocumento)
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        Finally
            iVeraz = Nothing
        End Try
    End Function

#End Region

#Region "Camara"

    Public Function insertarCamara(ByVal ePathArchivoCamaraZipeado As String) As String
        Dim iCamara As New Camara
        Dim iAccesoDatos As accesoDatos
        Dim iResultado As String

        Try

            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            iCamara.accesoDatos = iAccesoDatos
            iResultado = iCamara.insertarCamara(ePathArchivoCamaraZipeado)
            iCamara.accesoDatos = Nothing
            iAccesoDatos.commit()

            Return iResultado
        Catch DeudorNoInsertadoException As DeudorNoInsertadoException
            iAccesoDatos.rollback()
            Throw DeudorNoInsertadoException
        Catch Exception As Exception
            iAccesoDatos.rollback()
            Throw New DeudorNoInsertadoException(Exception)
        Finally
            iCamara = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerCamaraTarjeta(ByVal eDocumento As Long) As Boolean
        Dim iCamara As New Camara
        Try
            Return iCamara.obtenerDeudorTarjeta(eDocumento)
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        Finally
            iCamara = Nothing
        End Try
    End Function
    Public Function obtenerCamara(ByVal eDocumento As Long) As String
        Dim iCamara As New Camara
        Try
            Return iCamara.obtenerDeudoresFinanciera(eDocumento)
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch Exception As Exception
            Throw New DeudorNoEncontradoException(Exception)
        Finally
            iCamara = Nothing
        End Try
    End Function

#End Region

#Region "Boletin Interno"
    Public Function obtenerBoletinInterno(ByVal eBoletinInterno As BoletinInterno) As BoletinInterno
        Try
            Return eBoletinInterno.obtenerBoletinInterno
        Catch BoletinInternoNoEncontradoException As BoletinInternoNoEncontradoException
            Throw BoletinInternoNoEncontradoException
        Catch Exception As Exception
            Throw New BoletinInternoNoEncontradoException(Exception)
        End Try
    End Function
#End Region

#Region "Sexo"
    Public Function obtenerSexo(ByVal eSexo As Sexo) As Sexo
        Try
            Return eSexo.obtenerSexo
        Catch SexoNoEncontradoException As SexoNoEncontradoException
            Throw SexoNoEncontradoException
        Catch Exception As Exception
            Throw New SexoNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerSexos(ByVal eSexo As Sexo, Optional ByVal eSinSexoGenerico As Boolean = False) As IDataReader
        Try
            Return eSexo.obtenerSexos(eSinSexoGenerico)
        Catch SexoNoEncontradoException As SexoNoEncontradoException
            Throw SexoNoEncontradoException
        Catch Exception As Exception
            Throw New SexoNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerSexosGrilla(ByVal eSexo As Sexo, Optional ByVal eSinSexoGenerico As Boolean = False) As DataSet
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            Return obtenerSexosGrilla(iAccesoDatos, eSexo, eSinSexoGenerico)
        Catch Exception As Exception
            Throw New SexoNoEncontradoException(Exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerSexosGrilla(eAccesoDatos As accesoDatos, ByVal eSexo As Sexo, Optional ByVal eSinSexoGenerico As Boolean = False) As DataSet

        Try
            eSexo.accesoDatos = eAccesoDatos
            Return eSexo.obtenerSexosGrilla(eSinSexoGenerico)
        Catch Exception As Exception
            Throw New SexoNoEncontradoException(Exception)
        Finally
            eSexo.accesoDatos = Nothing
        End Try
    End Function
#End Region

#Region "Cruce"
    Public Function cruceVerazBoletin(ByVal ePathArchivoZipeado As String) As String
        Dim iPathArchivoRecibido As String = ConfigurationManager.AppSettings("archivosRecibidos") & "ACRUZAR.TXT"
        Dim iPathArchivoGenerado As String = ConfigurationManager.AppSettings("archivosGenerados") & "CRUZADO.TXT"
        Dim iCantidadCruzados As Long
        Dim iLinea As String
        Dim iFechaInicio As Date
        Dim iFechaFin As Date
        Dim iArchivoACruzar As StreamReader
        Dim iArchivoCruzado As StreamWriter
        Dim iEncoding As New System.Text.ASCIIEncoding
        Dim iConsultaBoletinVO As ConsultaBoletinVO
        Dim iRespuesta As String
        Dim iMensaje As String
        Try

            'Deszipear el archivo
            iFechaInicio = Now

            FuncionComun.UnZip(ePathArchivoZipeado)
            iArchivoACruzar = New StreamReader(iPathArchivoRecibido)
            iArchivoCruzado = New StreamWriter(iPathArchivoGenerado)

            While Not iArchivoACruzar.Peek = -1
                iLinea = iArchivoACruzar.ReadLine()
                iConsultaBoletinVO = New ConsultaBoletinVO
                iConsultaBoletinVO.documento = CInt(Left(iLinea, 8))
                iCantidadCruzados += 1
                Try
                    obtenerDeudorBoletin(iConsultaBoletinVO)
                    iRespuesta = "SI"
                Catch DeudorNoEncontradoException As DeudorNoEncontradoException
                    iRespuesta = "NO"
                End Try
                iArchivoCruzado.WriteLine(Right("00000000" & iConsultaBoletinVO.documento, 8) & iRespuesta)
            End While
            iArchivoCruzado.Close()
            iFechaFin = Now
            iMensaje = "INICIO DEL PROCESO:" & iFechaInicio & vbNewLine
            iMensaje = iMensaje & "FIN DEL PROCESO:" & iFechaFin & vbNewLine
            iMensaje = iMensaje & "CANTIDAD DE REGISTROS PROCESADOS:" & iCantidadCruzados & vbNewLine

            Return iMensaje

        Catch exception As Exception
            Throw New BoletinCruceNoRealizadoException(exception)
        Finally

        End Try

    End Function
#End Region

#Region "Tipo Entidad"
    Public Function obtenerTipoEntidad(ByVal eTipoEntidad As TipoEntidad) As TipoEntidad
        Try
            Return eTipoEntidad.obtenerTipoEntidad
        Catch Exception As Exception
            Throw New TipoEntidadNoEncontradaException(Exception)
        End Try
    End Function

    Public Function obtenerTiposEntidad(ByVal eTipoEntidad As TipoEntidad) As IDataReader
        Try
            Return eTipoEntidad.obtenerTiposEntidades()
        Catch Exception As Exception
            Throw New TipoEntidadNoEncontradaException(Exception)
        End Try
    End Function

    Public Function obtenerTiposEntidadDatoAnexo(ByVal eTipoEntidad As TipoEntidad) As IDataReader
        Try
            Return eTipoEntidad.obtenerTiposEntidadDatoAnexo
        Catch Exception As Exception
            Throw New TipoEntidadNoEncontradaException(Exception)
        End Try
    End Function

    Public Function obtenerTiposEntidadTramite(ByVal eTipoEntidad As TipoEntidad) As IDataReader
        Try
            Return eTipoEntidad.obtenerTiposEntidadesTramite
        Catch Exception As Exception
            Throw New TipoEntidadNoEncontradaException(Exception)
        End Try
    End Function

    Public Function obtenerTiposEntidadInterfaceTemplate(ByVal eTipoEntidad As TipoEntidad) As IDataReader
        Try
            Return eTipoEntidad.obtenerTiposEntidadesInterfaceTemplate
        Catch Exception As Exception
            Throw New TipoEntidadNoEncontradaException(Exception)
        End Try
    End Function
#End Region

#Region "Dia"
    Public Function obtenerDia(ByVal eDia As Dia) As Dia
        Try
            Return eDia.obtenerDia
        Catch diasNoEncontradoException As DiasNoEncontradoException
            Throw diasNoEncontradoException
        Catch Exception As Exception
            Throw New DiasNoEncontradoException(Exception)
        End Try
    End Function

    Public Function obtenerDias(ByVal eDia As Dia) As IDataReader
        Try
            Return eDia.obtenerdias()
        Catch DiasNoEncontradoException As DiasNoEncontradoException
            Throw DiasNoEncontradoException
        Catch Exception As Exception
            Throw New DiasNoEncontradoException(Exception)
        End Try
    End Function
#End Region

#Region "Ente Pagador"

    '    Public Function obtenerEntesPagadoresDataReader(ByVal eEntePagador As EntePagador) As IDataReader
    '        Try
    '            Return eEntePagador.obtenerEntesPagadoresDataReader()
    '        Catch ex As Exception
    '            Throw New EntePagadorNoEncontradoException(ex)
    '        End Try
    '    End Function

    '    Public Function obtenerEntesPagadoresDataset(ByVal eEntePagador As EntePagador) As DataSet
    '        Try
    '            Return eEntePagador.obtenerEntesPagadoresDataset
    '        Catch exception As Exception
    '            Throw New EntePagadorNoEncontradoException(exception)
    '        End Try
    '    End Function

    '    Public Function obtenerEntePagador(ByVal eEntePagador As EntePagador, Optional eObtenerEmpleador As Boolean = True) As EntePagador
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos()
    '            Return obtenerEntePagador(iAccesoDatos, eEntePagador, eObtenerEmpleador)

    '        Catch exception As Exception
    '            Throw New EntePagadorNoEncontradoException(exception)
    '        End Try
    '    End Function

    '    Public Function obtenerEntePagador(ByVal eAccesoDatos As accesoDatos, ByVal eEntePagador As EntePagador, Optional eObtenerEmpleador As Boolean = True) As EntePagador
    '        Try
    '            eEntePagador.accesoDatos = eAccesoDatos
    '            Return eEntePagador.obtenerEntePagador(eObtenerEmpleador)
    '        Catch exception As Exception
    '            Throw New EntePagadorNoEncontradoException(exception)
    '        End Try
    '    End Function

    '    Public Function obtenerEntesPagadoresGrilla(ByVal eEntePagador As EntePagador) As DataSet
    '        Try
    '            Return eEntePagador.obtenerEntesPagadoresGrilla
    '        Catch exception As Exception
    '            Throw New ZonaNoEncontradaException(exception)
    '        End Try
    '    End Function

    '    Public Sub crearEntePagador(ByVal eEntePagador As EntePagador)
    '        Dim iAccesoDatos As accesoDatos
    '        Try

    '            iAccesoDatos = New accesoDatos()
    '            iAccesoDatos.beginTransaction()

    '            crearEntePagador(iAccesoDatos, eEntePagador)

    '            iAccesoDatos.commit()

    '        Catch exception As Exception
    '            iAccesoDatos.rollback()
    '            Throw New EntePagadorNoCreadoException(exception)
    '        Finally
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub crearEntePagador(ByVal eAccesoDatos As accesoDatos, ByVal eEntePagador As EntePagador)
    '        Try

    '            If eEntePagador.empleador.id = Nothing Then
    '                eEntePagador.empleador.accesoDatos = eAccesoDatos
    '                eEntePagador.empleador.crear()
    '                eEntePagador.empleador.accesoDatos = Nothing
    '            End If

    '            eEntePagador.accesoDatos = eAccesoDatos
    '            eEntePagador.crear()

    '        Catch exception As Exception
    '            Throw New EntePagadorNoCreadoException(exception)
    '        Finally
    '            eEntePagador.accesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub modificarEntePagador(ByVal eEntePagador As EntePagador)
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos()
    '            iAccesoDatos.beginTransaction()
    '            modificarEntePagador(iAccesoDatos, eEntePagador)
    '            iAccesoDatos.commit()
    '        Catch Exception As Exception
    '            iAccesoDatos.rollback()
    '            Throw New EntePagadorNoCreadoException(Exception)
    '        Finally
    '            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub modificarEntePagador(eAccesoDatos As accesoDatos, ByVal eEntePagador As EntePagador)
    '        Try
    '            eEntePagador.accesoDatos = eAccesoDatos
    '            eEntePagador.modificar()
    '        Catch Exception As Exception
    '            Throw New PoliticaComercialNoModificadaException(Exception)
    '        Finally
    '            eEntePagador.accesoDatos = Nothing
    '        End Try
    '    End Sub


    '    Public Sub eliminarEntePagador(ByVal eEntePagador As EntePagador)
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos()
    '            iAccesoDatos.beginTransaction()
    '            eliminarEntePagador(iAccesoDatos, eEntePagador)
    '            iAccesoDatos.commit()
    '        Catch Exception As Exception
    '            iAccesoDatos.rollback()
    '            Throw New EntePagadorNoCreadoException(Exception)
    '        Finally
    '            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub eliminarEntePagador(eAccesoDatos As accesoDatos, ByVal eEntePagador As EntePagador)
    '        Try
    '            eEntePagador.accesoDatos = eAccesoDatos
    '            eEntePagador.eliminar()
    '        Catch Exception As Exception
    '            Throw New PoliticaComercialNoModificadaException(Exception)
    '        Finally
    '            eEntePagador.accesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Function obtenerEntePagadorReporteExcel(ByVal eEntePagador As EntePagador) As String
    '        Dim iPathArchivo As String
    '        Dim iAccesoDatos As accesoDatos

    '        Try
    '            iAccesoDatos = New accesoDatos
    '            eEntePagador.accesoDatos = iAccesoDatos
    '            iPathArchivo = generarEntePagadorReporteExcel(iAccesoDatos, eEntePagador.obtenerEntesPagadoresDataset(False))
    '            Return iPathArchivo

    '        Catch Exception As Exception
    '            Throw New ReciboNoEncontradoException(Exception)
    '        Finally
    '            eEntePagador.accesoDatos = Nothing
    '            iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Function

    '    Public Function generarEntePagadorReporteExcel(ByVal eAccesoDatos As accesoDatos, ByVal eDataSet As DataSet) As String
    '        Dim iLibro As New XLWorkbook
    '        Dim iHoja As IXLWorksheet
    '        Dim iPathArchivoExcel As String
    '        Dim i As Integer

    '        Try

    '            iHoja = iLibro.Worksheets.Add("ENTE")

    '            iHoja.Cell(1, 1).Value = "PAGADOR"
    '            iHoja.Cell(1, 2).Value = "CUITPAG"
    '            iHoja.Cell(1, 3).Value = "EMPLEADOR"
    '            iHoja.Cell(1, 4).Value = "CUITEM"
    '            iHoja.Cell(1, 5).Value = "CATEGORIA"
    '            iHoja.Cell(1, 6).Value = "FECHADEBITO"
    '            iHoja.Cell(1, 7).Value = "FECHACOBRO"

    '            For i = 0 To eDataSet.Tables("EntesPagadores").Rows.Count - 1
    '                With eDataSet.Tables("EntesPagadores").Rows(i)

    '                    iHoja.Cell(i + 2, 1).Value = .Item("DESCRIPCION").ToString()
    '                    FuncionComun.obtenerEstilosExcel(iHoja.Cell(i + 2, 2).Style, FuncionComun.enumFormatoExcel.ESTILONNORMALTEXTO)
    '                    iHoja.Cell(i + 2, 2).Value = .Item("CUIT").ToString()
    '                    FuncionComun.obtenerEstilosExcel(iHoja.Cell(i + 2, 2).Style, FuncionComun.enumFormatoExcel.ESTILONNORMALTEXTO)
    '                    iHoja.Cell(i + 2, 3).Value = .Item("EMPLEADOR").ToString()
    '                    FuncionComun.obtenerEstilosExcel(iHoja.Cell(i + 2, 3).Style, FuncionComun.enumFormatoExcel.ESTILONNORMALTEXTO)
    '                    iHoja.Cell(i + 2, 4).Value = .Item("CUITEMPLEADOR").ToString()
    '                    FuncionComun.obtenerEstilosExcel(iHoja.Cell(i + 2, 4).Style, FuncionComun.enumFormatoExcel.ESTILONNORMALTEXTO)
    '                    iHoja.Cell(i + 2, 5).Value = .Item("CATEGORIAS").ToString()
    '                    FuncionComun.obtenerEstilosExcel(iHoja.Cell(i + 2, 4).Style, FuncionComun.enumFormatoExcel.ESTILONNORMALTEXTO)
    '                    iHoja.Cell(i + 2, 6).Value = FuncionComun.vacioSiEsCero(FuncionComun.ceroSiEsVacio(.Item("FECHADEBITO").ToString()))
    '                    FuncionComun.obtenerEstilosExcel(iHoja.Cell(i + 2, 4).Style, FuncionComun.enumFormatoExcel.ESTILONNORMALTEXTO)
    '                    iHoja.Cell(i + 2, 7).Value = FuncionComun.vacioSiEsNothing(.Item("FECHACOBRO"))
    '                    FuncionComun.obtenerEstilosExcel(iHoja.Cell(i + 2, 4).Style, FuncionComun.enumFormatoExcel.ESTILONNORMALTEXTO)
    '                End With
    '            Next


    '            iPathArchivoExcel = ConfigurationManager.AppSettings("archivosGenerados") & "ENTEPAGADOR.xlsx"
    '            iHoja.Columns().AdjustToContents()

    '            iLibro.SaveAs(iPathArchivoExcel, False)

    '            Return iPathArchivoExcel

    '        Catch excepcion As Exception
    '            Throw New EntePagadorNoEncontradoException(excepcion)
    '        Finally
    '            iLibro = Nothing
    '            iHoja = Nothing
    '        End Try
    '    End Function


    '#End Region

    '#Region "Empleador"
    '    Public Function obtenerEmpleadoresDataReader(ByVal eEmpleador As Empleador) As IDataReader
    '        Try
    '            Return eEmpleador.obtenerEmpleadoresDataReader()
    '        Catch ex As Exception
    '            Throw New EmpleadorNoEncontradoException(ex)
    '        End Try
    '    End Function

    '    Public Function obtenerEmpleador(eAccesoDatos As accesoDatos, ByVal eEmpleador As Empleador) As Empleador
    '        Try
    '            eEmpleador.accesoDatos = eAccesoDatos
    '            Return eEmpleador.obtenerEmpleador()
    '        Catch ex As Exception
    '            Throw New EmpleadorNoEncontradoException(ex)
    '        Finally
    '            eEmpleador.accesoDatos = Nothing
    '        End Try
    '    End Function

    '    Public Function obtenerEmpleador(ByVal eEmpleador As Empleador) As Empleador
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos
    '            Return obtenerEmpleador(iAccesoDatos, eEmpleador)
    '        Catch ex As Exception
    '            Throw New EmpleadorNoEncontradoException(ex)
    '        Finally
    '            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Function


    '    Public Function obtenerEmpleadoresGrilla(ByVal eEmpleador As Empleador) As DataSet
    '        Try
    '            Return eEmpleador.obtenerEmpleadoresGrilla()
    '        Catch ex As Exception
    '            Throw New EmpleadorNoEncontradoException(ex)
    '        End Try
    '    End Function

    '    Public Sub crearEmpleador(ByVal eEmpleador As Empleador)
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos()
    '            crearEmpleador(iAccesoDatos, eEmpleador)
    '        Catch ex As Exception
    '            Throw New EmpleadorNoCreadoException(ex)
    '        Finally
    '            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub crearEmpleador(ByVal eAccesoDatos As accesoDatos, ByVal eEmpleador As Empleador)
    '        Try
    '            eEmpleador.accesoDatos = eAccesoDatos
    '            eEmpleador.crear()
    '        Catch ex As Exception
    '            Throw New EmpleadorNoCreadoException(ex)
    '        Finally
    '            eEmpleador.accesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub modificarEmpleador(ByVal eEmpleador As Empleador)
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos
    '            eEmpleador.accesoDatos = iAccesoDatos
    '            eEmpleador.modificar()
    '            eEmpleador.accesoDatos = Nothing
    '        Catch ex As Exception
    '            Throw New EmpleadorNoModificadoException(ex)
    '        Finally
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub eliminarEmpleador(ByVal eEmpleador As Empleador)
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos
    '            eEmpleador.accesoDatos = iAccesoDatos
    '            eEmpleador.eliminar()
    '            eEmpleador.accesoDatos = Nothing
    '        Catch ex As Exception
    '            Throw New EmpleadorNoEliminadoException(ex)
    '        Finally
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '#End Region

    '#Region "Calendario Debitos"

    '    Public Function obtenerCalendarioAgrupado(ByVal eCalendarioVO As CalendarioVO) As DataSet
    '        Dim iAccesoDatos As accesoDatos
    '        Dim iCalendario As New Calendario

    '        Try

    '            iAccesoDatos = New accesoDatos()
    '            iCalendario.accesoDatos = iAccesoDatos

    '            Return iCalendario.obtenerCalendarioAgrupado(eCalendarioVO)

    '        Catch exception As Exception
    '            Throw New CalendarioNoEncontradoException(exception)
    '        Finally
    '            iCalendario.accesoDatos = Nothing
    '            iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Function

    '    Public Function obtenerCalendario(ByVal eCalendario As Calendario) As Calendario
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos()
    '            Return obtenerCalendario(iAccesoDatos, eCalendario)
    '        Catch EntePagadorNoEncontradoException As EntePagadorNoEncontradoException
    '            Throw EntePagadorNoEncontradoException
    '        Catch exception As Exception
    '            Throw New CalendarioNoEncontradoException(exception)
    '        End Try
    '    End Function

    '    Public Function obtenerCalendario(ByVal eAccesoDatos As accesoDatos, ByVal eCalendario As Calendario) As Calendario
    '        Try
    '            eCalendario.accesoDatos = eAccesoDatos
    '            Return eCalendario.obtenerCalendario
    '        Catch exception As Exception
    '            Throw New CalendarioNoEncontradoException(exception)
    '        End Try
    '    End Function

    '    Public Sub modificarCalendario(ByVal eCalendario As Calendario)
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos()
    '            iAccesoDatos.beginTransaction()
    '            modificarCalendario(iAccesoDatos, eCalendario)
    '            iAccesoDatos.commit()
    '        Catch Exception As Exception
    '            iAccesoDatos.rollback()
    '            Throw New CalendarioNoModificadoException(Exception)
    '        Finally
    '            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub modificarCalendario(eAccesoDatos As accesoDatos, ByVal eCalendario As Calendario)
    '        Try
    '            eCalendario.accesoDatos = eAccesoDatos
    '            eCalendario.modificar()
    '        Catch Exception As Exception
    '            Throw New CalendarioNoModificadoException(Exception)
    '        Finally
    '            eCalendario.accesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub crearCalendario(ByVal eCalendario As Calendario)
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos()
    '            iAccesoDatos.beginTransaction()
    '            crearCalendario(iAccesoDatos, eCalendario)
    '            iAccesoDatos.commit()
    '        Catch Exception As Exception
    '            iAccesoDatos.rollback()
    '            Throw New CalendarioNoCreadoException(Exception)
    '        Finally
    '            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub crearCalendario(eAccesoDatos As accesoDatos, ByVal eCalendario As Calendario)
    '        Try
    '            eCalendario.accesoDatos = eAccesoDatos
    '            eCalendario.crear()
    '        Catch Exception As Exception
    '            Throw New CalendarioNoCreadoException(Exception)
    '        Finally
    '            eCalendario.accesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub eliminarCalendario(ByVal eCalendario As Calendario)
    '        Dim iAccesoDatos As accesoDatos
    '        Try
    '            iAccesoDatos = New accesoDatos()
    '            iAccesoDatos.beginTransaction()
    '            eliminarCalendario(iAccesoDatos, eCalendario)
    '            iAccesoDatos.commit()
    '        Catch Exception As Exception
    '            iAccesoDatos.rollback()
    '            Throw New CalendarioNoEliminadoException(Exception)
    '        Finally
    '            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Sub eliminarCalendario(eAccesoDatos As accesoDatos, ByVal eCalendario As Calendario)
    '        Try
    '            eCalendario.accesoDatos = eAccesoDatos
    '            eCalendario.eliminar()
    '        Catch Exception As Exception
    '            Throw New CalendarioNoEliminadoException(Exception)
    '        Finally
    '            eCalendario.accesoDatos = Nothing
    '        End Try
    '    End Sub

    '    Public Function reporteCalendarioExcel(ByVal eCalendarioVO As CalendarioVO) As String
    '        Dim iAccesoDatos As New accesoDatos
    '        Dim iPathArchivo As String
    '        Dim iCalendario As New Calendario

    '        Try

    '            If IsNothing(eCalendarioVO.dataset) Then
    '                iCalendario.accesoDatos = iAccesoDatos
    '                eCalendarioVO.dataset = iCalendario.obtenerDatasetReporteExcel(eCalendarioVO)
    '                iCalendario.accesoDatos = Nothing
    '            End If

    '            iPathArchivo = generarReporteCalendarioExcel(iAccesoDatos, eCalendarioVO)

    '            Return iPathArchivo

    '        Catch Exception As Exception
    '            iAccesoDatos.rollback()
    '            EnviaMail.enviarMail(eCalendarioVO.usuario.mail, eCalendarioVO.nombreEnvio, Log.obtenerErrorAplicacion(Exception, Me.ToString).mensajeUsuario)
    '            Throw New CalendarioNoEncontradoException(Exception)
    '        Finally
    '            iAccesoDatos.cerrar()
    '            iAccesoDatos = Nothing
    '        End Try
    '    End Function

    '    Public Sub enviarReporteCalendarioExcel(ByVal eCalendarioVO As CalendarioVO, ByVal ePathArchivoZipeado As String)
    '        Dim iResultado As String

    '        Try
    '            iResultado = "PARAMETROS SELECCIONADOS" & vbNewLine
    '            iResultado &= vbNewLine
    '            iResultado &= "PERIODO"
    '            iResultado &= vbNewLine
    '            iResultado &= "FECHA DESDE: " & Format(eCalendarioVO.mesAnioDesde, "MM/yyyy") & vbNewLine
    '            iResultado &= "FECHA HASTA: " & Format(eCalendarioVO.mesAnioHasta, "MM/yyyy")

    '            EnviaMail.enviarMail(eCalendarioVO.usuario.mail, eCalendarioVO.nombreEnvio, iResultado, ePathArchivoZipeado)

    '        Catch Exception As Exception
    '            iResultado = Log.obtenerErrorAplicacion(Exception, Me.ToString).mensajeUsuario
    '            EnviaMail.enviarMail(eCalendarioVO.usuario.mail, eCalendarioVO.nombreEnvio, iResultado)
    '        Finally
    '            If File.Exists(ePathArchivoZipeado) Then File.Delete(ePathArchivoZipeado)
    '            iResultado = Nothing
    '        End Try
    '    End Sub

    '    Public Function generarReporteCalendarioExcel(ByVal eAccesoDatos As accesoDatos, ByVal eCalendarioVO As CalendarioVO) As String
    '        Dim iPathArchivoZipeado, iPathArchivo, iLinea, iSeparador As String
    '        Dim iColeccionArchivosAZipear As New Collection
    '        Dim iDataRow As DataRow
    '        Dim iArchivo As StreamWriter
    '        Dim iDataSet As DataSet
    '        Try

    '            iDataSet = eCalendarioVO.dataset

    '            ' +---------------------+
    '            ' |     ARCHIVO XLS     |
    '            ' +---------------------+
    '            iPathArchivo = ConfigurationManager.AppSettings("archivosGenerados") & "ReporteCalendario" & Format(Now, "yyyyMMdd") & ".xls"
    '            iArchivo = New StreamWriter(iPathArchivo, False, System.Text.Encoding.GetEncoding(1252))
    '            iSeparador = vbTab


    '            iLinea = "DIA" & vbTab & "FECHA" & vbTab & "HABIL" & vbTab & "FECHA DE MODIFICACION" & vbTab & "USUARIO MODIFICACION"

    '            iArchivo.WriteLine(iLinea)

    '            For Each iDataRow In iDataSet.Tables("DatasetCalendarioReporte").Rows

    '                iLinea = iDataRow.Item("dia").ToString & iSeparador
    '                iLinea &= Format(CDate(iDataRow.Item("fecha").ToString), "dd/MM/yy") & iSeparador
    '                iLinea &= iDataRow.Item("diahabil").ToString & iSeparador
    '                If iDataRow.Item("fechaModificacion").ToString = "" Then
    '                    iLinea &= Format(CDate(iDataRow.Item("fechaCreacion").ToString), "dd/MM/yy") & iSeparador
    '                    iLinea &= iDataRow.Item("usuarioCreacion").ToString
    '                Else
    '                    iLinea &= Format(CDate(iDataRow.Item("fechaModificacion").ToString), "dd/MM/yy") & iSeparador
    '                    iLinea &= iDataRow.Item("usuarioModificacion").ToString
    '                End If

    '                iArchivo.WriteLine(iLinea)
    '            Next

    '            iArchivo.Close()

    '            iPathArchivoZipeado = ConfigurationManager.AppSettings("archivosGenerados") & "CalendarioExcel" & Format(Now, "ddMMyyyy") & Format(Now, "hhmmss") & ".zip"
    '            iColeccionArchivosAZipear.Add(iPathArchivo)

    '            FuncionComun.zipearArchivos(iColeccionArchivosAZipear, iPathArchivoZipeado)

    '            Return iPathArchivoZipeado

    '        Catch exception As Exception
    '            Throw New CalendarioNoEncontradoException(exception)
    '        Finally
    '            iDataSet = Nothing
    '            iColeccionArchivosAZipear = Nothing
    '            If Not IsNothing(iArchivo) Then iArchivo.Close()
    '            iArchivo = Nothing
    '            If iPathArchivo <> Nothing AndAlso File.Exists(iPathArchivo) Then File.Delete(iPathArchivo)
    '        End Try
    '    End Function

    '    Public Function alertaCalendarioIncompleto(ByVal eCalendarioVO As CalendarioVO) As Boolean
    '        Dim iAdministradorFeriados As New AdministradorFeriados
    '        Dim iCalendario As New Calendario
    '        Dim iCalendarioDia As CalendarioDia


    '        'Esta funcion indica si debe enviar por mail el alerta de calendario incompleto el reporte calendario Excel
    '        'Evalua si hay algun dia habil incompleto en el mes, siempre y cuando no sea feriado ni sabado o domingo

    '        Try
    '            iCalendario.mes = eCalendarioVO.mesAnioDesde.Month
    '            iCalendario.anio = eCalendarioVO.mesAnioDesde.Year

    '            iCalendario = obtenerCalendario(iCalendario)

    '            For Each iCalendarioDia In iCalendario.diasCalendario
    '                Dim iFeriado As New Feriado
    '                iFeriado.fecha = iCalendarioDia.fecha

    '                If Not iAdministradorFeriados.esFeriado(iFeriado) AndAlso Format(CDate(iFeriado.fecha).DayOfWeek, "d") <> Dia.enumDias.SABADO AndAlso Format(CDate(iFeriado.fecha).DayOfWeek, "d") <> Dia.enumDias.DOMINGOVB AndAlso iCalendarioDia.diahabil = Nothing Then
    '                    Return True
    '                End If

    '            Next

    '            Return False

    '        Catch Exception As Exception
    '            Throw New CalendarioNoEncontradoException(Exception)
    '        Finally
    '            iAdministradorFeriados = Nothing
    '            iCalendario = Nothing
    '            iCalendarioDia = Nothing
    '        End Try
    '    End Function

#End Region

#Region "Pais"

    Public Sub crearPais(ByVal ePais As Pais)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            ePais.accesoDatos = iAccesoDatos
            ePais.crear()
        Catch Exception As Exception
            Throw New PaisNoCreadoException(Exception)
        Finally
            ePais.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerPaisGrilla(ByVal ePais As Pais) As DataSet
        Try
            Return ePais.obtenerPaisDataSet
        Catch Exception As Exception
            Throw New PaisNoEncontradoException(Exception)
        End Try
    End Function

    Public Sub eliminarPais(ByVal ePais As Pais)
        Dim iAccesoDatos As accesoDatos
        Try

            iAccesoDatos = New accesoDatos
            ePais.accesoDatos = iAccesoDatos
            ePais.eliminar()
        Catch Exception As Exception
            Throw New PaisNoEliminadoException(Exception)
        Finally
            ePais.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub
    Public Function obtenerPais(ByVal ePais As Pais) As Pais
        Try
            Return ePais.obtenerPais
        Catch Exception As Exception
            Throw New PaisNoEncontradoException(Exception)
        End Try
    End Function
    Public Function obtenerPaises(ByVal ePais As Pais) As IDataReader
        Try
            Return ePais.obtenerPaises
        Catch Exception As Exception
            Throw New PaisNoEncontradoException(Exception)
        End Try
    End Function
    Public Sub modificarPais(ByVal ePais As Pais)
        Dim iAccesoDatos As accesoDatos
        Try

            iAccesoDatos = New accesoDatos
            ePais.accesoDatos = iAccesoDatos
            ePais.modificar()

        Catch Exception As Exception
            Throw New PaisNoModificadoException(Exception)
        Finally
            ePais.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub
#End Region

#End Region

End Class
