Imports System.Configuration
Imports di.financiera.excepciones
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports di.financiera.datos
Imports di.financiera.utils
Imports FormulaFinanciera
Imports CarlosAg.ExcelXmlWriter
Imports ClosedXML.Excel
Imports System.Security.Cryptography
Imports System.Text
Imports System.Collections.Generic
Imports System.Net.Mail
Imports System.Web.UI
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports System.Reflection
Imports System.Web.UI.WebControls

Public Class FuncionComun

#Region "Enumerados"

    Public Const GRUPOEMPRESA As Integer = 1
    Public Shared iBaseDeDatos As String = ConfigurationSettings.AppSettings("baseDeDatos")
    Public Const ROJO = "#d9534f"
    Public Const AZUL = "#0275d8"
    Public Const VERDE = "#5cb85c"
    Public Const CELESTE = "#5bc0de"
    Public Const NARANJA = "#f0ad4e"


    Public Enum enumFormatoFecha
        DDMMYY
        DDMMYYYY
        YYYYMMDD
        YYYYMMDDGUION
        YYMMDD
        MMYY
        MMYYYY
        YYYYMM
        YYMM
        MMYYSINSEPARADOR
        YYYYMMDDHHMMSS
        YYMMDDHHMMSS
        YYMMDDHHMM
        YYYYMMDDHHMM
        DDMMYYHHMM
        DDMMYYYYHHMM
        DDMMYYHHMMSS
        DDMMYYYYHHMMSS
        DD
        MM
        MMDD
        YY
        YYYY
        HHMM
        HHMMSS
        MMDDYYYY
    End Enum

    Public Enum enumFormatoFechaInterfaces
        DDMMYY = 1
        YYMMDD
        DDMMYYYY
        YYYYMMDD
        DD_MM_YYYY
        YYYY_MM_DD
        DD_MM_YY
        YY_MM_DD
        JULIANA
    End Enum

    Public Enum enumFecha
        DIA
        MES
        AÑO
        SEMANA
    End Enum

    Public Enum enumConceptoCalculoCFT
        INTERES = 1
        INTERESGASTOS = 2
        INTERESGASTOSSEGUROSELLADO = 3
    End Enum

    Public Enum enumEnvioRPPMCPF
        RAPIPAGO = 1
        PAGOMISCUENTAS = 2
        PAGOFACIL = 3
        RAPIPAGOGIRE = 4
        PAGOMISCUENTAS2 = 5
        PAGOMISCUENTASSIRO = 6
        REDLINK = 7
        BAPRO = 8
        PAGOMISCUENTASFRAVEGA = 9
    End Enum

    Public Enum enumTipoArchivoPagoMisCuentas
        DISENOCSV = 1
        DISENOCANTIDADCARACTERES = 2
        DISENO029 = 3
    End Enum

    Public Enum enumDiseñosPagoFacil
        SINDISEÑO = 0
        DISENOI042019 = 1
    End Enum

    Public Enum enumPeriodoAnualFormulas
        OPCION36530 = 1 'TOMA COMO DATO LA DIVISION DE 365 / 30
        OPCION12 = 2 'TOMA COMO DATO 12
    End Enum
    Public Enum enumTipoInteresPeriodoRegular
        SINCALCULO = 0 'NO CALCULA
        CALCULOEXPONENCIAL = 1 'FORMULA BST
        CALCULODIRECTO = 2 'CALCULO BACS
    End Enum

    Public Enum enumSentido
        IZQUIERDA = 1
        DERECHA = 2
    End Enum
    Public Enum enumTipoReporteReversion
        RESUMEN = 1
        DETALLADO = 2
    End Enum

    Public Enum EnumConvenio
        CONVENIO = 1
        IMPUTACIONCONVENIO = 2
        SOLICITUD = 3
    End Enum

    Public Enum enumTipoRedondeo
        SINREDONDEO = 0
        MONTOSUPERIOR = 1
        MONTOINFERIOR = 2
        MONTOMEDIO = 3
        MONTOPERZONALIZADO = 4
        MONTOPROPORCIONALALADECENA = 5
    End Enum

    Public Enum enumProceso
        ARCHIVOEMBOZADORA = 10
        STOCK = 11
        ARCHIVOESBOZADORA = 13
    End Enum

    Public Enum enumTipoImputacionMigracionExterna
        IMPUTACIONPORCUOTA = 1
        IMPUTACIONPORMONTO = 2
    End Enum

    Public Enum enumSistemaExterno
        NOINFORMA = 0
        BST = 1
        BACS = 2
    End Enum

    Public Enum enumTipoArchivoInserccionCampania
        sinAnexo = 1
        conAnexo = 2
    End Enum

    Public Enum enumTipoFacturacion
        AUTOMATICA = 1
        MANUAL = 2
    End Enum

    Public Enum enumEstadoSolicitud
        LIQUIDADO = 1
        ANULADO = 2
    End Enum

    Public Enum enumTipoInteres
        TASADIRECTA = 0
        INTERESFRANCES = 1
        INTERESFRANCESCONSEGURO = 2
        SINDEFINIR = 3
    End Enum

    Public Enum enumOrdenImputacionCobranza
        PROPORCIONAL = 1
        ORDEN1 = 2 'GastoCobranza + Interes punitorio + Interes Compensatorio + (Gastos,Seguro, sellado, impuestos, segurovida) + interés + Capital) 
        PROPORCIONALTOTAL = 3
        ORDENPARAMETRIZADO = 4
    End Enum

    Public Enum enumTipoCapital
        FINANCIADO = 0
        NETO = 1
    End Enum

    Public Enum enumFormatoCodigoBarra
        FORMATOCODIGOBARRA1 = 1 ' CODIGO BARRA 1 (36 DIG.)
        FORMATOCODIGOBARRA2 = 2 ' CODIGO BARRA 2 (32 DIG.)
        FORMATOCODIGOBARRA3 = 3 ' CODIGO BARRA 4 (44 DIG.)
        FORMATOCODIGOBARRA4 = 4 ' CODIGO BARRA 3 (50 DIG.)
        FORMATOCODIGOBARRA5 = 5 ' CODIGO BARRA 5 (42 DIG.)
        FORMATOCODIGOBARRA6 = 6 ' CODIGO BARRA 6 (48 DIG.)
        FORMATOCODIGOBARRA7 = 7 ' CODIGO BARRA 7 (56 DIG.)
        FORMATOCODIGOBARRA8 = 8 ' CODIGO BARRA 8 (42 DIG.)
        FORMATOCODIGOBARRA9 = 9 ' CODIGO BARRA 9 (54 DIG.)
        FORMATOCODIGOBARRA10 = 10 ' CODIGO BARRA 10 (42 DIG. ONLINE)
        FORMATOCODIGOBARRA11 = 11 ' CODIGO BARRA 11 (46 DIG.)
        FORMATOCODIGOBARRA12 = 12 ' CODIGO BARRA 16 CORTO CORTO 
    End Enum

    Public Enum enumNivelParametroNumeroCredito
        GRUPOEMPRESA = 1
        EMPRESAGRUPO = 2
        UNIDADDENEGOCIOS = 3
        SUCURSAL = 4
        COMERCIO = 5
    End Enum

    Public Enum enumAgrupacionContabilidad
        SIAGRUPAR = 0
        PORMOVIMIENTO = 1
        PORFECHA = 2
        MES = 3
    End Enum

    Public Enum enumNumeracionCuentaContable
        PUNTO = 1
        LIBRE = 2
    End Enum

    Public Enum enumCamposLiquidacion
        CAPITALPEDIDOYGASTOS = 1
        CAPITALNETO = 2
    End Enum

    Public Enum enumCamposLiquidacionArchivo
        TXT = 1
        EXCEL = 2
    End Enum
    Public Enum enumTipoSaldoARefinanciacion
        RESUMENACTUAL = 1
        SALDOTOTAL = 2
    End Enum

    Public Enum enumTipoCancelacion
        CANCELACIONTIPO1 = 1
        CANCELACIONTIPO2 = 2
        CANCELACIONTIPO3 = 3
    End Enum
    Public Enum enumTipoSolicitud
        COMUN = 1
        TARJETA = 4
    End Enum

    Public Enum enumOpcionEvalucaionSU
        SINRECIBO = 1
        CONRECIBO = 2
    End Enum

    Public Enum EnumTipoCartera
        TODOS = 1
        CARTERAPROPIA = 2
        CARTERACEDIDA = 3
    End Enum

    Public Enum EnumLiquidados
        TODOS = 1
        SI = 2
        NO = 3
    End Enum

    Public Enum enumProcesoAutomatico
        PROCESOAUTOMATICO = 1
    End Enum
    Public Enum enumOrdenCalculadorCuotas
        CANTIDADCUOTASASC = 1
        CANTIDADCUOTASDESC = 2
        TIPOPLANCANTIDADCUOTASASC = 3
        TIPOPLANCANTIDADCUOTASDESC = 4
    End Enum

    Public Enum enumOrdenResumenDeuda
        FECHALEVANTECREDITOYCUOTA = 1
        FECHAVENCIMIENTOCREDITO = 2
    End Enum

    Public Enum enumFechaPrimerVencimiento
        FECHALEVANTE = 1
        FECHASOLICITUD = 2
    End Enum

    Public Enum enumTipoCalculoVentaCartera
        COLUMBIA = 1
        BST = 2
        ICBC
    End Enum

    Public Enum enumPaginaARedireccionarSolicitudPasoAPaso
        SOLICITUDALTAPORPASOS = 1
        SOLICITUDALTAPORPASOSCD = 2
        SOLICITUDALTAPORPASOSCDSU = 3
        SOLICITUDALTAPORPASOSBACS = 4
    End Enum

    Public Enum enumPaginaADireccionarSolicitudPasos
        INICIO = 1
        CLIENTEMODIFICAR = 2
        VALIDARIDENTIDAD = 3
        OPERACIONREALIZADA = 4
        SELECCIONPLAN = 5
        DOCUMENTACIONAUTOGESTIONADA = 6
        TOKENIZACIONTARJETA = 7
    End Enum
    Public Enum enumPaginaSolicituAltaPorPasos
        SOLICITUDALTAPORPASOS0
        SOLICITUDALTAPORPASOS1
        SOLICITUDALTAPORPASOS2
        SOLICITUDALTAPORPASOSCLIENTEMODIFICAR
        SOLICITUDALTAPORPASOSSELECCIONTARJETA
        SOLICITUDALTAPORPASOSSELECCIONCREDITO
        SOLICITUDALTAPORPASOSFORMAPAGO
        SOLICITUDALTAPORPASOSVALIDADORIDENTIDADPREGUNTAS
        SOLICITUDALTAPORPASOSVALIDADORIDENTIDADAUTORIZACION
        SOLICITUDALTAPORPASOSSELECCIONPRODUCTO
        SOLICITUDALTAPORPASOSSELECCIONPRODUCTOCOMPRADO
        SOLICITUDALTAPORPASOSTARJETADEBITO
        SOLICITUDALTAPORPASOSSELECCIONDEBITOPORTARJETA
        SOLICITUDALTAPORPASOSSELECCIONDEBITO
        SOLICITUDALTAPORPASOSDOCUMENTACIONAUTOGESTIONADA
        SOLICITUDALTAPORPASOSALTATARJETA
        SOLICITUDALTAPORPASOSAUTORIZACION
        SOLICITUDALTAPORPASOSCAMPANIAMARKETING
        SOLICITUDALTAPORPASOSDATOSANEXOSPRODUCTOCOMPRADO
        SOLICITUDALTAPORPASOSENROLAMIENTOPRODUCTOCOMPRADO
        SOLICITUDALTAPORPASOSFIRMADIGITAL
    End Enum

    Public Enum enumProcesoCierreContable
        REFUNDICION = 1
        CIERREPATRIMONIAL = 2
    End Enum

    Public Enum enumTipoControlRequisitosSolicitud
        ENTRADASOLICITUD = 1
        REMITOCONTROL = 2
    End Enum

    Public Enum enumTipoInteresPLanes
        PORCENTAJEINTERES = 1
        TEM = 2
        TNA = 3
    End Enum

    Public Enum enumTipoNumeracionRecibo
        SECUENCIALGRUPOEMPRESA = 1
        CANTIDADPAGOS = 2
        SECUENCIALEMPRESAGRUPO = 3
        SECUENCIALUNIDADNEGOCIOS = 4
        SECUENCIALSUCURSAL = 5
    End Enum

    Public Enum enumCategoriaTipoCliente
        PORCANTIDADCREDITOS = 1
        PORCANTIDADCREDITOSCANCELADOS = 2
    End Enum

    Public Enum enumTipoTelefonoFijo
        PARTICULAR = 0
        LABORAL = 1
    End Enum

    Public Enum enumTipoTelefono
        CELULAR = 0
        FIJO = 1
        SINDEFINIR = 2
    End Enum

    Public Enum enumParametrosFacturacionElectronica
        GRUPOEMPRESA = 1
        PUNTOVENTA = 2
        EMPRESAGRUPO = 3
        SUCURSAL = 4
    End Enum

    Public Enum enumProveedorFacturacionElectronica
        AFIP = 1
        RONDANET = 2
    End Enum

    Public Enum enumTipoDocumentoFacturacion
        DOCUMENTO = 1
        CUIL = 2
    End Enum

    Public Enum enumFormatoExcel
        ESTILONNORMALNUMEROAZUL
        ESTILONNORMALPESOS
        ESTILONNORMALPORCENTAJE
        ESTILONNORMALPORCENTAJEAZUL
        ESTILONNORMALTEXTO
        ESTILONNORMALTEXTOAZUL
        ESTILONNORMALTEXTOROJO
        ESTILOGRISPESOS
        ESTILONNORMALDECIMAL
        ESTILONNORMALFECHA
        ESTILONNORMALNUMERO
        ESTILONNORMALNUMEROROJO
        ESTILOSUBTITULO
        ESTILOSUBTITULOGRIS
        ESTILOTITULO
        ESTILOTITULOAMARILLO
        ESTILOTITULOAZULSINBORDE
        ESTILOTITULOGRIS
        ESTILOTITULOGRISPESOS
        ESTILOTITULOGRISCLAROPESOS
        ESTILOTITULOGRISPORCENTAJE
        ESTILOTITULOGRISNUMERO
        ESTILOTITULONARANJA
        ESTILOTITULOROJO
        ESTILOTITULOVERDE
        ESTILOTITULOVERDESINBORDE
        ESTILOTITULOVIOLETA
        ESTILOTITULOAZUL
        ESTILOTITULOCENTRADO
        ESTILOTITULOAZULPESOS
        ESTILOTITULOAZULDECIMAL
    End Enum

    Public Enum enumTipoProcesoDocumentacion
        PORARCHIVO = 1
        PORDIRECTORIO = 2
        PORDIRECTORIOARCHIVO = 3
    End Enum

    Public Enum enumTipoCobranzaServicio
        SERVICIO = 1
        COBRANZA = 2
    End Enum

    Public Enum enumPlanComercialAsignacionMasivaExcelTipoProceso
        ASIGNACION = 1
        DESASIGNACION = 2
    End Enum

    Public Enum enumTipoEnvioSMS
        SINDEFINIR = 0
        SMSSTART = 1
        INFOBIP = 2
        SMSMASIVOS = 3
        TELEPROM = 4
        UCONTACT = 5
        ASISTECLICK = 6
        AWS = 7
    End Enum

    Public Enum enumTipoMensajeEnvioSMS
        SINDEFINIR = 0
        VALIDACIONDOCUMENTO = 1
        VALIDACIONCELULAR = 2
        DOCUMENTACIONAUTOGESTIONADA = 3
        TOKENIZACIONTARJETA = 4
        CAMBIOCONTRASENIA = 5
        TOKENMEDIOPAGO = 6
        PINAPP = 7
        AUTORIZACIONTARJETA = 8
        HABILITACIONTARJETA = 9
        VENCIMIENTOCUOTA = 10
        RECUPEROCONTRASENIA = 11
        EMISIONTARJETA = 12
        LINKPAGOMEDIOPAGO = 13
        SOLICITUDALTA = 14
        AUTORIZACIONCONSULTAINFORMACIONCLIENTE = 15
        FIRMADIGITAL = 16
    End Enum

    Public Enum enumProcesoAnulacionMasivaRecibos
        DESMARCARRECIBO = 1
        DESMARCARCUENTACORRIENTE = 2
        DESMARCARAMBOS = 3
    End Enum

    Public Enum enumProducto
        PRESTAMO = 1
        CONVENIO = 2
        TRAMITE = 3
        RESUMENTARJETA = 4
        RECIBOCUOTASOCIAL = 5
    End Enum

    Public Enum enumTiempoCliente
        AÑOS = 0
        MESES = 1
    End Enum

    Public Enum enumTipoConsultaNosis
        XML = 1
        API = 2
    End Enum

    Public Enum enumCancelacionAnticipada
        NOVALIDA = 1
        PRESTAMOAPAGAR = 2
        TODOSLOSPRESTAMOS = 3
    End Enum

    Public Enum enumBaseCalculoPunitorios
        SALDOCUOTA = 1
        SALDOCAPITAL = 2
        SALDOCAPITALSEGURO = 3
    End Enum

    Public Enum EnumTipoSistema
        Loan = 1
    End Enum

    Public Enum EnumRenaperTipoApi
        API1 = 1
        API2 = 2
    End Enum

    Public Enum EnumSituacionesBCRA
        SITUACIONBCRA1 = 1
        SITUACIONBCRA2 = 2
        SITUACIONBCRA3 = 3
        SITUACIONBCRA4 = 4
        SITUACIONBCRA5 = 5
        SITUACIONBCRA6 = 6
    End Enum

    Public Enum EnumDelimitadorDecimal
        SINDELIMITADOR = 1
        PUNTO = 2
        COMA = 3
    End Enum

    Public Enum EnumDelimitadorColumna
        SINDELIMITADOR = 1
        PUNTOYCOMA = 2
        PUNTO = 3
        COMA = 4
        PYPE = 5
        ESPACIO = 6
    End Enum

    Public Enum EnumSiNo
        NO = 0
        SI = 1
    End Enum

    Public Enum EnumInformarCobranza
        NOINFORMAR = 0
        ANDLIRA = 1
    End Enum

    Public Enum EnumTodosSiNo
        TODOS = -1
        NO = 0
        SI = 1
    End Enum

    Public Enum EnumTipoCalculo
        CALCULOTRADICIONAL = 1
        CALCULOPROPORCIONAL = 2
    End Enum

#End Region

#Region "Metodos"
    Public Shared Function generarCaseSqlPorEnumerado(ByVal eNombreCampo As String, ByVal eEnumerado As Array) As String
        Dim iCase As String

        Try
            iCase = "case "
            For i As Integer = 0 To eEnumerado.Length - 1
                iCase &= " when " & eNombreCampo & "=" & CInt(eEnumerado.GetValue(i)) & " then '" & eEnumerado(i).ToString.Replace("_", " ") & "'"
            Next
            iCase &= " end"

            Return iCase

        Catch excepcion As Exception
            Return ""
        End Try
    End Function

    Public Shared Sub validarSqlInjection(ByRef eObjeto As Object)
        Dim iXML As XmlSerializer
        Dim eXML As New StringWriter
        Dim iTexto As String

        If Not IsNothing(eObjeto) Then
            For Each prop As PropertyInfo In eObjeto.GetType().GetProperties()

                If Not prop.PropertyType.FullName.ToUpper.Contains("SYSTEM") Then
                    validarSqlInjection(prop.GetValue(eObjeto))
                End If

                If prop.PropertyType.Name = "String" Then
                    Dim iValorOriginal As Object = prop.GetValue(eObjeto)

                    If CType(iValorOriginal, String) <> Nothing Then

                        If CType(iValorOriginal, String).ToUpper.Contains("SELECT ") OrElse CType(iValorOriginal, String).ToUpper.Contains("UPDATE ") OrElse CType(iValorOriginal, String).ToUpper.Contains("INSERT ") OrElse CType(iValorOriginal, String).ToUpper.Contains("DELETE ") OrElse CType(iValorOriginal, String).ToUpper.Contains("TRUNCATE ") OrElse CType(iValorOriginal, String).ToUpper.Contains("CREATE ") OrElse CType(iValorOriginal, String).ToUpper.Contains("DROP ") OrElse CType(iValorOriginal, String).ToUpper.Contains("ALTER TABLE") OrElse CType(iValorOriginal, String).ToUpper.Contains("SLEEP(") OrElse CType(iValorOriginal, String).ToUpper.Contains("HAVING ") OrElse CType(iValorOriginal, String).ToUpper.Contains("GROUP BY ") Then

                            Try
                                iXML = New XmlSerializer(eObjeto.GetType)
                                iXML.Serialize(eXML, eObjeto)
                                iTexto = eXML.ToString
                            Catch ex As Exception
                                iTexto = CType(iValorOriginal, String)
                            End Try

                            FuncionComun.loguearInjection(iTexto)
                            Throw New RootException("No se pudo procesar el request")
                        End If

                        If prop.SetMethod <> Nothing Then
                            Dim iValorModificado As Object = CType(iValorOriginal, String).Replace(";", "").Replace("'", "").Replace("`", "").Replace("&gt", "").Replace("&lt", "")
                            prop.SetValue(eObjeto, Convert.ChangeType(iValorModificado, prop.PropertyType), Nothing)
                        End If
                    End If
                End If
            Next
        End If
    End Sub
    Public Shared Function generarCadenaAleatoria(Optional ByVal eCantidadDigitos As Integer = 8) As String
        Dim iStringDatos As String = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789"
        Dim iStringBuilder As New StringBuilder
        Dim iIndiceBusqueda As Integer
        Dim iRandom As New Random

        Try

            For i As Integer = 1 To eCantidadDigitos
                iIndiceBusqueda = iRandom.Next(0, 33)
                iStringBuilder.Append(iStringDatos.Substring(iIndiceBusqueda, 1))
            Next

            Return "A" & iStringBuilder.ToString & "1"

        Catch exception As Exception
            Throw New UsuarioNoCreadoException(exception)
        Finally
            iStringBuilder = Nothing
            iRandom = Nothing
        End Try
    End Function

    Public Shared Function generarCadenaAleatoriaNumero(Optional ByVal eCantidadDigitos As Integer = 8) As String
        Dim iStringDatos As String = "1234567890"
        Dim iStringBuilder As New StringBuilder
        Dim iIndiceBusqueda As Integer
        Dim iRandom As New Random

        Try

            For i As Integer = 1 To eCantidadDigitos
                iIndiceBusqueda = iRandom.Next(0, 9)
                iStringBuilder.Append(iStringDatos.Substring(iIndiceBusqueda, 1))
            Next

            Return iStringBuilder.ToString

        Catch exception As Exception
            Throw New UsuarioNoCreadoException(exception)
        Finally
            iStringBuilder = Nothing
            iRandom = Nothing
        End Try
    End Function

    Public Shared Sub generarCSVGenerico(ByVal eDataTable As DataTable, ByVal ePathArchivo As String)
        Dim iArchivo As StreamWriter
        Dim iDataRow As DataRow
        Dim iDataColumn As DataColumn
        Dim iDataCell As Object
        Dim iLinea As String
        Try

            iArchivo = New StreamWriter(ePathArchivo, False, System.Text.Encoding.GetEncoding(1252))
            iLinea = ""
            For Each iDataColumn In eDataTable.Columns
                iLinea &= iDataColumn.Caption & ";"
            Next
            iArchivo.WriteLine(Left(iLinea, iLinea.Length - 1))
            'Vamos por los Campos
            For Each iDataRow In eDataTable.Rows
                iLinea = ""
                For Each iDataCell In iDataRow.ItemArray
                    iLinea &= iDataCell.ToString.Replace(" 00:00:00", "") & ";"
                Next
                iArchivo.WriteLine(Left(iLinea, iLinea.Length - 1))
            Next
            iArchivo.Close()
        Catch exception As Exception
            Throw exception
        Finally
            iDataRow = Nothing
            iDataColumn = Nothing
            iDataCell = Nothing
        End Try
    End Sub

    Public Shared Function generarExcelGenerico(ByVal eDataTable As DataTable) As Workbook
        Dim iDataRow As DataRow
        Dim iDataColumn As DataColumn
        Dim iDataCell As Object
        Dim iLibro As New Workbook
        Dim iRow As WorksheetRow
        Dim iHoja As Worksheet
        Try

            FuncionComun.agregarEstilosExcel(iLibro)
            iHoja = iLibro.Worksheets.Add(eDataTable.TableName)

            'AGREGO LOS ENCABEZADOS
            iRow = iHoja.Table.Rows.Add()
            For Each iDataColumn In eDataTable.Columns
                iHoja.Table.Columns.Add(New WorksheetColumn())
                iRow.Cells.Add(New WorksheetCell(iDataColumn.Caption, "EstiloTituloAzul"))
                iHoja.Table.Columns(iDataColumn.Ordinal).AutoFitWidth = True
            Next

            'Vamos por los Campos
            For Each iDataRow In eDataTable.Rows
                iRow = iHoja.Table.Rows.Add()
                For Each iDataCell In iDataRow.ItemArray
                    iRow.Cells.Add(New WorksheetCell(iDataCell.ToString))
                Next
            Next

            'For Each iDataColumn In eDataTable.Columns
            ' iHoja.Table.Columns(5).AutoFitWidth
            'Next

            Return iLibro

        Catch ex As Exception
            Return Nothing
        Finally
            iDataRow = Nothing
            iDataColumn = Nothing
            iDataCell = Nothing
            iLibro = Nothing
            iRow = Nothing
            iHoja = Nothing
        End Try


    End Function

    Public Shared Sub agregarEstilosExcel(ByRef eLibro As Workbook)
        Dim iEstiloTitulo, iEstiloGrisPesos, iENF, iEstiloTituloRojo, iEstiloTituloNaranja, iENN, iENP, iENTA, iENT, iENNA, iEstiloNormalPorcentaje, iEstiloNormalDecimal, iEstiloSubTitulo, iEstiloTituloAzul, iEstiloTituloAzulSinBorde As WorksheetStyle
        Dim iEstiloNormalTexto, iEstiloNormalNumero, iEstiloNormalFecha, iEstiloTotal As WorksheetStyle
        Try

            iEstiloTitulo = eLibro.Styles.Add("EstiloTitulo")
            With iEstiloTitulo
                .Font.FontName = "Tahoma"
                .Font.Size = 14
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.Single
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "White"
                .Name = "EstiloTitulo"
                .Interior.Color = "Black"
                .Interior.Pattern = StyleInteriorPattern.Solid
            End With

            iEstiloSubTitulo = eLibro.Styles.Add("EstiloSubTitulo")
            With iEstiloSubTitulo
                .Font.FontName = "Tahoma"
                .Font.Size = 12
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Left
                .Font.Color = "Black"
                .Name = "EstiloSubTitulo"
                .Interior.Color = "White"
                .Interior.Pattern = StyleInteriorPattern.Solid
            End With

            iEstiloTituloAzul = eLibro.Styles.Add("EstiloTituloAzul")
            With iEstiloTituloAzul
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "White"
                .Name = "EstiloTituloAzul"
                .Interior.Color = "Blue"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
            End With

            iEstiloTituloAzulSinBorde = eLibro.Styles.Add("EstiloTituloAzulSinBorde")
            With iEstiloTituloAzulSinBorde
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "White"
                .Name = "EstiloTituloAzulSinBorde"
                .Interior.Color = "Blue"
                .Interior.Pattern = StyleInteriorPattern.Solid
            End With

            iEstiloTituloNaranja = eLibro.Styles.Add("EstiloTituloNaranja")
            With iEstiloTituloNaranja
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "White"
                .Name = "EstiloTituloNaranja"
                .Interior.Color = "Orange"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
            End With

            iEstiloTituloRojo = eLibro.Styles.Add("EstiloTituloRojo")
            With iEstiloTituloRojo
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "White"
                .Name = "EstiloTituloRojo"
                .Interior.Color = "Red"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
            End With

            iEstiloTitulo = eLibro.Styles.Add("EstiloTituloVerde")
            With iEstiloTitulo
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "White"
                .Name = "EstiloTituloVerde"
                .Interior.Color = "Green"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
            End With

            iEstiloTitulo = eLibro.Styles.Add("EstiloTituloVerdeSinBorde")
            With iEstiloTitulo
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "White"
                .Name = "EstiloTituloVerdeSinBorde"
                .Interior.Color = "Green"
                .Interior.Pattern = StyleInteriorPattern.Solid
            End With

            iEstiloTitulo = eLibro.Styles.Add("EstiloTituloAmarillo")
            With iEstiloTitulo
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "Gray"
                .Name = "EstiloTituloAmarillo"
                .Interior.Color = "Yellow"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
            End With

            iEstiloTitulo = eLibro.Styles.Add("EstiloTituloVioleta")
            With iEstiloTitulo
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "White"
                .Name = "EstiloTituloVioleta"
                .Interior.Color = "Violet"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
            End With

            iENN = eLibro.Styles.Add("ENN")
            With iENN
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Font.Color = "Black"
                .Name = "ENN"
                .NumberFormat = "_(0_);_( \(0\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
            End With

            iEstiloNormalDecimal = eLibro.Styles.Add("END")
            With iEstiloNormalDecimal
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Font.Color = "Black"
                .Name = "END"
                .NumberFormat = "_(#,##0.00_);_(\(#,##0.00\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
            End With

            iENF = eLibro.Styles.Add("ENF")
            With iENF
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "Black"
                .Name = "ENF"
            End With

            iEstiloGrisPesos = eLibro.Styles.Add("EstiloGrisPesos")
            With iEstiloGrisPesos
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Name = "EstiloGrisPesos"
                .Font.Color = "White"
                .Interior.Color = "Gray"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .NumberFormat = "_($ #,##0.00_);_($ \(#,##0.00\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
            End With

            iEstiloTitulo = eLibro.Styles.Add("EstiloTituloGris")
            With iEstiloTitulo
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Left
                .Name = "EstiloTituloGris"
                .Font.Color = "White"
                .Interior.Color = "Gray"
                .Interior.Pattern = StyleInteriorPattern.Solid
            End With

            iEstiloTitulo = eLibro.Styles.Add("EstiloSubTituloGris")
            With iEstiloTitulo
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Left
                .Name = "EstiloSubTituloGris"
                .Font.Color = "White"
                .Interior.Color = "DarkGray"
                .Interior.Pattern = StyleInteriorPattern.Solid
            End With


            iENP = eLibro.Styles.Add("ENP")
            With iENP
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Font.Color = "Black"
                .Name = "ENP"
                .NumberFormat = "_($ #,##0.00_);_($ \(#,##0.00\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
            End With

            iEstiloNormalPorcentaje = eLibro.Styles.Add("ENPO")
            With iEstiloNormalPorcentaje
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Font.Color = "Black"
                .Name = "ENPO"
                .NumberFormat = "_(#,##0.00%_);_(% \(#,##0.00\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
            End With

            iENT = eLibro.Styles.Add("ENT")
            With iENT
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Left
                .Font.Color = "Black"
                .Name = "ENT"
            End With

            iEstiloNormalPorcentaje = eLibro.Styles.Add("ENPOA")
            With iEstiloNormalPorcentaje
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Font.Color = "White"
                .Name = "EstiloTituloAzul"
                .Interior.Color = "Blue"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
                .Name = "ENPOA"
                .NumberFormat = "_(% #,##0.00_);_(% \(#,##0.00\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
            End With

            iENNA = eLibro.Styles.Add("ENNA")
            With iENNA
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Font.Color = "White"
                .Name = "EstiloTituloAzul"
                .Interior.Color = "Blue"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
                .Name = "ENNA"
                .NumberFormat = "_(0_);_( \(0\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
            End With

            iENTA = eLibro.Styles.Add("ENTA")
            With iENTA
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Left
                .Font.Color = "White"
                .Name = "EstiloTituloAzul"
                .Interior.Color = "Blue"
                .Interior.Pattern = StyleInteriorPattern.Solid
                .Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "White")
                .Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "White")
                .Name = "ENTA"
            End With

            iEstiloNormalNumero = eLibro.Styles.Add("EstiloNormalNumero")
            With iEstiloNormalNumero
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Font.Color = "Black"
                .Name = "EstiloNormalNumero"
            End With

            iEstiloNormalFecha = eLibro.Styles.Add("EstiloNormalFecha")
            With iEstiloNormalFecha
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Center
                .Font.Color = "Black"
                .Name = "EstiloNormalFecha"
            End With

            iEstiloNormalTexto = eLibro.Styles.Add("EstiloNormalTexto")
            With iEstiloNormalTexto
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = False
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Left
                .Font.Color = "Black"
                .Name = "EstiloNormalTexto"
            End With

            iEstiloTotal = eLibro.Styles.Add("EstiloTotal")
            With iEstiloTotal
                .Font.FontName = "Tahoma"
                .Font.Size = 10
                .Font.Bold = True
                .Font.Underline = UnderlineStyle.None
                .Alignment.Horizontal = StyleHorizontalAlignment.Right
                .Font.Color = "White"
                .Name = "EstiloTotal"
                .Interior.Color = "Black"
                .Interior.Pattern = StyleInteriorPattern.Solid
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Shared Sub combinarRangoCeldasIgualesHorizontal(eRango As IXLRange)
        ' Recibe una hoja y un rango de celdas
        ' Recorre las celdas combinando las celdas (horizontalmente) contiguas iguales
        ' Ejemplo aplicado sobre la primer fila:
        ' +--------+--------+--------+--------+--------+    +--------+--------+--------+--------+--------+
        ' | Lunes  | Lunes  | Lunes  | Martes | Martes | -> |          Lunes           |     Martes      |
        ' +--------+--------+--------+--------+--------+    +--------+--------+--------+--------+--------+
        ' | Plan A | Plan B | Plan C | Plan D | Plan E |    | Plan A | Plan B | Plan C | Plan D | Plan E |
        ' +--------+--------+--------+--------+--------+    +--------+--------+--------+--------+--------+
        Dim iRng As IXLRangeRow
        Dim iColumnas As Integer
        Dim iCeldaActual, iCeldaProxima As String
        Dim i, j As Integer

        iColumnas = eRango.ColumnCount
        For Each iRng In eRango.Rows
            For i = 1 To iColumnas - 1
                For j = i + 1 To iColumnas
                    iCeldaActual = iRng.AsRange.Cell(iRng.RowNumber, i).Value
                    iCeldaProxima = iRng.AsRange.Cell(iRng.RowNumber, j).Value
                    If iCeldaActual <> iCeldaProxima Then
                        Exit For
                    End If
                Next j
                eRango.Range(eRango.Cell(iRng.RowNumber, i), eRango.Cell(iRng.RowNumber, j - 1)).Merge()
                i = j - 1
            Next i
        Next

    End Sub

    Public Shared Sub combinarRangoCeldasIgualesVertical(eRango As IXLRange)
        ' Recibe una hoja y un rango de celdas
        ' Recorre las celdas combinando las celdas (verticalmente) contiguas iguales
        ' Ejemplo aplicado sobre la primer columna:
        ' +--------+--------+    +--------+--------+
        ' | Lunes  | Plan A |    |        | Plan A |
        ' +--------+--------+    |        +--------+
        ' | Lunes  | Plan B |    | Lunes  | Plan B |
        ' +--------+--------+    |        +--------+
        ' | Lunes  | Plan C | -> |        | Plan C |
        ' +--------+--------+    +--------+--------+
        ' | Martes | Plan D |    | Martes | Plan D |
        ' +--------+--------+    |        +--------+
        ' | Martes | Plan E |    |        | Plan E |
        ' +--------+--------+    +--------+--------+
        Dim iRng As IXLRangeColumn
        Dim iFilas As Integer
        Dim iCeldaActual, iCeldaProxima As String
        Dim i, j As Integer

        iFilas = eRango.RowCount
        For Each iRng In eRango.Columns
            For i = 1 To iFilas - 1
                For j = i + 1 To iFilas
                    iCeldaActual = iRng.AsRange.Cell(i, iRng.ColumnNumber).Value
                    iCeldaProxima = iRng.AsRange.Cell(j, iRng.ColumnNumber).Value
                    If iCeldaActual <> iCeldaProxima Then
                        Exit For
                    End If
                Next j
                eRango.Range(eRango.Cell(i, iRng.ColumnNumber), eRango.Cell(j - 1, iRng.ColumnNumber)).Merge()
                i = j - 1
            Next i
        Next

    End Sub

    Public Shared Function removerUltimoCaracter(ByVal eCadena As String, Optional ByVal eCaracter As String = ",") As String
        Try
            If eCadena.Length > 0 Then
                eCadena = RTrim(eCadena)
                If eCadena.Substring(eCadena.Length - 1) = eCaracter Then
                    eCadena = eCadena.Substring(0, eCadena.Length - 1)
                End If
                'Right(
            End If
            Return eCadena

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function soloNumerico(ByVal eDato As String) As String
        Dim i As Integer
        Dim iElemento As String
        Dim iDatoSalida As String = ""

        Try

            For i = 1 To Len(eDato)
                iElemento = Mid(eDato, i, 1)
                If IsNumeric(iElemento) Then
                    iDatoSalida = iDatoSalida & iElemento
                End If
            Next

            Return iDatoSalida

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function soloNumericoYComa(ByVal eDato As String) As String
        Dim i As Integer
        Dim iElemento As String
        Dim iDatoSalida As String = ""

        Try

            For i = 1 To Len(eDato)
                iElemento = Mid(eDato, i, 1)
                If IsNumeric(iElemento) Or iElemento = "," Then
                    iDatoSalida = iDatoSalida & iElemento
                End If
            Next

            Return iDatoSalida

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function sqlDiferenciaMeses(ByVal eFechaDesde As String, ByVal eFechaHasta As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return "datediff(month," & eFechaDesde & "," & eFechaHasta & ") "
        Else
            'MySQL
            Return "timestampdiff(month," & eFechaDesde & "," & eFechaHasta & ") "
        End If
    End Function

    Public Shared Function sqlCortarTextoEnCantidadCaracteres(ByVal eCampo As String, ByVal eCantidadCaracteres As String, eSentido As FuncionComun.enumSentido) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return IIf(eSentido = enumSentido.IZQUIERDA, "LEFT", "RIGHT") & "(" & eCampo & "," & eCantidadCaracteres & ")"
        Else
            'MySQL
            Return IIf(eSentido = enumSentido.IZQUIERDA, "LEFT", "RIGHT") & "(" & eCampo & "," & eCantidadCaracteres & ")"
        End If
    End Function

    Public Shared Function sqlPosicionCaracter(ByVal eCampo As String, ByVal eCaracterBuscado As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return "CHARINDEX('" & eCaracterBuscado & "'," & eCampo & ")"
        Else
            'MySQL
            Return "INSTR(" & eCampo & ",'" & eCaracterBuscado & "')"
        End If
    End Function

    Public Shared Function sqlDiferenciaHoras(ByVal ePrimerCampo As String, ByVal eSegundoCampo As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return "datediff(day," & eSegundoCampo & "," & ePrimerCampo & ")"
        Else
            'MySQL
            Return "timediff(" & ePrimerCampo & "," & eSegundoCampo & ")"
        End If
    End Function

    Public Shared Function diferenciaTiempo(ByVal eFechaInicio As Date, ByVal eFechafin As Date) As String
        Dim iHoras, iMinutos, iSegundos As Long

        Try
            iHoras = Int(DateDiff("s", eFechaInicio, eFechafin) / 3600)
            iMinutos = Int((DateDiff("s", eFechaInicio, eFechafin) - (iHoras * 3600)) / 60)
            iSegundos = Int(DateDiff("s", eFechaInicio, eFechafin) - (iHoras * 3600) - (iMinutos * 60))

            Return Right("00" & iHoras, 2) & ":" & Right("00" & iMinutos, 2) & ":" & Right("00" & iSegundos, 2)

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Private Shared Sub marcaComun(Optional eAccesoDatos As accesoDatos = Nothing)
        Dim iConexion As accesoDatos
        Dim iGeneradorSql As New GeneradorSql
        Try
            If IsNothing(eAccesoDatos) Then
                iConexion = New accesoDatos
            Else
                iConexion = eAccesoDatos
            End If
            iGeneradorSql.agregarSet("valor=" & FuncionComun.booleanByte(True))
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'marcaComun'")
            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New ComunException("Consulte a soporte tecnico")
        Finally
            If Not IsNothing(iConexion) AndAlso IsNothing(eAccesoDatos) Then iConexion.cerrar()
            iConexion = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Shared Sub verMarcaComun(eAccesoDatos As accesoDatos)
        Dim iConexion As accesoDatos
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            If eAccesoDatos Is Nothing Then
                iConexion = New accesoDatos
            Else
                iConexion = eAccesoDatos
            End If

            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'marcaComun'")
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                If byteBoolean(iDataReader.Item("valor").ToString) Then Throw New ComunException("Consulte a soporte tecnico")
            End If

        Catch exception As Exception
            Throw New ComunException("Consulte a soporte tecnico")
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            If Not IsNothing(iConexion) AndAlso IsNothing(eAccesoDatos) Then iConexion.cerrar()
            iConexion = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Shared Sub validarVersionSistema(eAccesoDatos As accesoDatos, Optional eSistemaOrigen As String = Nothing)
        Dim iConexion As accesoDatos
        Dim iVersion As New Version

        Try

            If Not Debugger.IsAttached Then

                If eAccesoDatos Is Nothing Then
                    iConexion = New accesoDatos
                Else
                    iConexion = eAccesoDatos
                End If

                iVersion.accesoDatos = iConexion
                iVersion = iVersion.obtenerVersion()
                iVersion.accesoDatos = Nothing

                If iVersion.numero <> Version.NUMEROVERSION OrElse iVersion.fecha <> Version.FECHAVERSION Then
                    iVersion.enviarEmail(iVersion, "Version sistema", "La version utilizada del sistema es " & Version.NUMEROVERSION & " : " & Version.FECHAVERSION & ". La version registrada en base es " & iVersion.numero & " : " & iVersion.fecha & IIf(eSistemaOrigen <> Nothing, ", Sistema origen:" & eSistemaOrigen, ""))
                    Throw New ComunException("La version del sistema difiere del asentado en base de datos")
                End If

            End If

        Catch exception As Exception
            loguearProcesoVersion(Format(Now, "dd/MM/yyyy HH:mm:ss") & vbTab & "PROBLEMAS VERSION" & vbTab & "LA VERSION DEL SISTEMA DIFIERE DEL ASENTADO EN BASE DE DATOS" & vbTab)
            Throw New ComunException("La version del sistema difiere del asentado en base de datos")
        Finally
            If Not IsNothing(iConexion) AndAlso IsNothing(eAccesoDatos) Then iConexion.cerrar()
            iConexion = Nothing
            iVersion = Nothing
        End Try
    End Sub

    Public Shared Function validarSoloNumerico(ByVal eDato As String) As String
        Dim i As Integer
        Dim iElemento As String
        Dim iDatoSalida As String = ""

        Try

            For i = 1 To Len(eDato)
                iElemento = Mid(eDato, i, 1)
                If IsNumeric(iElemento) Then
                    iDatoSalida = iDatoSalida & iElemento
                Else
                    Return False
                End If
            Next

            Return True

        Catch exception As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function validarSoloNumericoYComa(ByVal eDato As String) As Boolean
        Dim i As Integer
        Dim iElemento As String

        Try

            For i = 1 To Len(eDato)
                iElemento = Mid(eDato, i, 1)
                If Not (IsNumeric(iElemento) OrElse iElemento = ",") Then
                    Return False
                End If
            Next

            Return True

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function vacioSiEsNulo(ByVal eObjeto As Object) As String

        If IsDBNull(eObjeto) Or eObjeto Is Nothing Then
            Return ""
        Else
            Return Trim(eObjeto)
        End If

    End Function

    Public Shared Function obtenerPosicionTramaSalida(ByVal eRegistroParametrizado As String(), ByVal eRegistro As String(), eTramaSalida As String) As String
        Dim i As Integer = 0

        Try
            For Each RegistroParametrizado As String In eRegistroParametrizado
                If RegistroParametrizado = eTramaSalida Then
                    Return IIf(eRegistro(i).ToString().ToUpper = "NULL", "", eRegistro(i).ToString())
                End If
                i += 1
            Next

            Return ""

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Shared Function ceroDecimal(ByVal eNumero As String) As String
        If eNumero <> vbNullString Then
            ceroDecimal = Format(CDbl(eNumero), "0.00")
        Else
            ceroDecimal = "0,00"
        End If
    End Function

    Public Shared Function ceroSiEsNulo(ByVal eObjeto As Object) As Double

        If IsNothing(eObjeto) OrElse IsDBNull(eObjeto) OrElse eObjeto = Nothing Then
            Return 0
        Else
            Return CDbl(eObjeto)
        End If

    End Function

    Public Shared Function ceroSiEsVacio(ByVal eCadena As String) As Double

        If Trim(eCadena) = "" Then
            Return 0
        Else
            Return CDbl(Replace(eCadena, ".", ","))
        End If

    End Function

    Public Shared Function vacioSiEsCero(ByVal eNumero As Double) As String
        Try

            If eNumero = Nothing Then
                Return ""
            Else
                Return eNumero.ToString
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function nuloSiEsNothing(ByVal eObject As Object) As String
        If IsNothing(eObject) OrElse eObject = Nothing Then
            Return "null"
        Else
            If TypeOf (eObject) Is Double Then
                eObject = "'" & eObject & "'"
                Return Replace(eObject, ",", ".")
            ElseIf TypeOf (eObject) Is Date Then
                Return "'" & Format(eObject, "yyyy-MM-dd") & "'"
            Else
                Return "'" & eObject & "'"
            End If
        End If
    End Function

    Public Shared Function nuloSiEsVacio(ByVal eString As String) As String
        If Len(eString) = 0 Then
            Return "\N"
        Else
            Return eString
        End If
    End Function

    Public Shared Function ceroSiEsNothing(ByVal eObject As Object) As String
        If eObject = Nothing Then
            Return "0"
        Else
            If TypeOf (eObject) Is Double Then
                eObject = "'" & eObject & "'"
                Return Replace(eObject, ",", ".")
            ElseIf TypeOf (eObject) Is Date Then
                Return "'" & Format(eObject, "yyyy/MM/dd") & "'"
            Else
                Return "'" & eObject & "'"
            End If
        End If
    End Function

    Public Shared Function hoySiEsVacio(ByVal eObject As Object) As String

        Try
            If eObject = Nothing Then
                Return "'" & Format(Today, "yyyy-MM-dd") & "'"
            Else
                Return "'" & Format(eObject, "yyyy-MM-dd") & "'"
            End If
        Catch exception As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function nothingSiEsVacio(ByVal eCadena As String) As Date

        Try
            If eCadena = "" Then
                Return Nothing
            Else
                Return CDate(eCadena)
            End If
        Catch exception As Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function validarFecha(ByVal eCadena As String) As Boolean

        Try
            If eCadena = "" OrElse IsDate(eCadena) Then
                Return True
            Else
                Return False
            End If
        Catch exception As Exception
            Return True
        End Try

    End Function

    Public Shared Function nothingSiEsNulo(ByVal eObject As Object) As Object
        If IsDBNull(eObject) OrElse eObject = Nothing Then
            Return Nothing
        Else
            Return eObject
        End If
    End Function

    Public Shared Function byteBoolean(ByVal eNumero As Byte) As Boolean
        Return eNumero = 1
    End Function

    Public Shared Function booleanByte(ByVal eBoolean As Boolean) As Byte
        Return IIf(eBoolean, 1, 0)
    End Function

    Public Shared Function vacioSiEsNothing(ByVal eObject As Object) As String
        Try
            If eObject = Nothing Then
                Return ""
            Else
                Select Case TypeName(eObject)
                    Case "Date"
                        Return Format(CType(eObject, Date), "dd/MM/yyyy")
                    Case Else
                        Return eObject.ToString
                End Select
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function formatoMoneda(ByVal eNumero As Double) As String
        Try

            If eNumero = Nothing Then
                Return Format(0, FuncionComun.obtenerSignoMoneda & " #,##0.00")
            Else
                Return Format(eNumero, FuncionComun.obtenerSignoMoneda & " #,##0.00")
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function obtenerDatosListaSiNo() As Collection
        Dim iColeccion As New Collection
        iColeccion.Add("SI")
        iColeccion.Add("NO")
        Return iColeccion
    End Function


    Public Shared Function obtenerPlazoLiquidacion() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add("TODOS", -1)
        iSortedList.Add("0", 0)
        iSortedList.Add("30", 30)
        iSortedList.Add("60", 60)
        iSortedList.Add("90", 90)

        Return iSortedList
    End Function

    Public Shared Function obtenerDatosListaSiNoTodos() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add("TODOS", -1)
        iSortedList.Add("SI", 1)
        iSortedList.Add("NO", 2)
        Return iSortedList
    End Function

    Public Shared Function obtenerDatosVersionDocumento() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(0, "ORIGINAL")
        iSortedList.Add(1, "DUPLICADO")
        iSortedList.Add(2, "TRIPLICADO")
        iSortedList.Add(3, "CUADRIPLICADO")
        iSortedList.Add(4, "QUINTUPLICADO")
        iSortedList.Add(5, "SEXTUPLICADO")

        Return iSortedList
    End Function

    Public Shared Function obtenerSignos() As SortedList
        Dim iSortedList As New SortedList
        iSortedList.Add(">", 1)
        iSortedList.Add(">=", 2)
        iSortedList.Add("=", 3)
        iSortedList.Add("<=", 4)
        iSortedList.Add("<", 5)
        Return iSortedList
    End Function

    Public Shared Function obtenerDelimitadorDecimal() As Collection
        Dim iColeccion As New Collection
        iColeccion.Add("SIN DELIMITADOR", EnumDelimitadorDecimal.SINDELIMITADOR)
        iColeccion.Add("PUNTO (.)", EnumDelimitadorDecimal.PUNTO)
        iColeccion.Add("COMA (,)", EnumDelimitadorDecimal.COMA)
        Return iColeccion
    End Function

    Public Shared Function obtenerDelimitadorColumnas() As Collection
        Dim iColeccion As New Collection
        iColeccion.Add("SIN DELIMITADOR", EnumDelimitadorColumna.SINDELIMITADOR)
        iColeccion.Add("PUNTO Y COMA (;)", EnumDelimitadorColumna.PUNTOYCOMA)
        iColeccion.Add("PUNTO (.)", EnumDelimitadorColumna.PUNTO)
        iColeccion.Add("COMA (,)", EnumDelimitadorColumna.COMA)
        iColeccion.Add("PYPE (|)", EnumDelimitadorColumna.PYPE)
        iColeccion.Add("ESPACIO", EnumDelimitadorColumna.ESPACIO)
        Return iColeccion
    End Function


    Public Shared Function delimitadorDecimal(eValor As String) As String
        Dim iDelimitador As String
        Try
            Select Case CInt(eValor)
                Case EnumDelimitadorDecimal.SINDELIMITADOR
                    iDelimitador = ""
                Case EnumDelimitadorDecimal.PUNTO
                    iDelimitador = "."
                Case EnumDelimitadorDecimal.COMA
                    iDelimitador = ","
                Case Else
                    iDelimitador = ""
            End Select

            Return iDelimitador

        Catch ex As Exception
            Throw New Exception
        End Try

    End Function

    Public Shared Function delimitadorColumnas(eValor As String) As String
        Dim iDelimitador As String
        Try
            Select Case CInt(eValor)
                Case EnumDelimitadorColumna.SINDELIMITADOR
                    iDelimitador = ""
                Case EnumDelimitadorColumna.PUNTOYCOMA
                    iDelimitador = ";"
                Case EnumDelimitadorColumna.PUNTO
                    iDelimitador = "."
                Case EnumDelimitadorColumna.COMA
                    iDelimitador = ","
                Case EnumDelimitadorColumna.PYPE
                    iDelimitador = "|"
                Case EnumDelimitadorColumna.ESPACIO
                    iDelimitador = vbTab
                Case Else
                    iDelimitador = ""
            End Select

            Return iDelimitador

        Catch ex As Exception
            Throw New Exception
        End Try
    End Function

    Public Shared Function numeroALetras(ByVal eMonto As Double) As String
        Dim iLongitud As Integer
        Dim iCentavos As String
        Dim iCadena As String
        Dim iUnidad As Integer
        Dim iDecena As Integer
        Dim iCentena As Integer
        Dim iUnidadDeMil As Integer
        Dim iDecenaDeMil As Integer
        Dim iCentenaDeMil As Integer
        Dim iUnidadDeMillon As Integer
        Dim iMontoString As String

        iMontoString = CStr(Format(eMonto, "0.00"))
        iLongitud = Len(iMontoString) - 3
        iCentavos = "CON " + Right(iMontoString, 2) + " CENTAVOS"
        iCadena = ""

        'Cifras de 1 a 9.
        If iLongitud = 1 Then
            iUnidad = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 99.
        If iLongitud = 2 Then
            iUnidad = Val(Mid(iMontoString, 2, 1))
            iDecena = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 999.
        If iLongitud = 3 Then
            iUnidad = Val(Mid(iMontoString, 3, 1))
            iDecena = Val(Mid(iMontoString, 2, 1))
            iCentena = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 9999.
        If iLongitud = 4 Then
            iUnidad = Val(Mid(iMontoString, 4, 1))
            iDecena = Val(Mid(iMontoString, 3, 1))
            iCentena = Val(Mid(iMontoString, 2, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 99999.
        If iLongitud = 5 Then
            iUnidad = Val(Mid(iMontoString, 5, 1))
            iDecena = Val(Mid(iMontoString, 4, 1))
            iCentena = Val(Mid(iMontoString, 3, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 2, 1))
            iDecenaDeMil = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 999999.
        If iLongitud = 6 Then
            iUnidad = Val(Mid(iMontoString, 6, 1))
            iDecena = Val(Mid(iMontoString, 5, 1))
            iCentena = Val(Mid(iMontoString, 4, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 3, 1))
            iDecenaDeMil = Val(Mid(iMontoString, 2, 1))
            iCentenaDeMil = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 9999999.
        If iLongitud = 7 Then
            iUnidad = Val(Mid(iMontoString, 7, 1))
            iDecena = Val(Mid(iMontoString, 6, 1))
            iCentena = Val(Mid(iMontoString, 5, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 4, 1))
            iDecenaDeMil = Val(Mid(iMontoString, 3, 1))
            iCentenaDeMil = Val(Mid(iMontoString, 2, 1))
            iUnidadDeMillon = Val(Mid(iMontoString, 1, 1))
        End If


        'Cifras de 1 a 99999999.
        If iLongitud = 8 Then
            iUnidad = Val(Mid(iMontoString, 8, 1))
            iDecena = Val(Mid(iMontoString, 7, 1))
            iCentena = Val(Mid(iMontoString, 6, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 5, 1))
            iDecenaDeMil = Val(Mid(iMontoString, 4, 1))
            iCentenaDeMil = Val(Mid(iMontoString, 3, 1))
            iUnidadDeMillon = Val(Mid(iMontoString, 1, 2))
        End If

        'Genera los millones.
        If iUnidadDeMillon = 1 Then iCadena = "UN MILLON "
        If iUnidadDeMillon = 2 Then iCadena = "DOS MILLONES "
        If iUnidadDeMillon = 3 Then iCadena = "TRES MILLONES "
        If iUnidadDeMillon = 4 Then iCadena = "CUATRO MILLONES "
        If iUnidadDeMillon = 5 Then iCadena = "CINCO MILLONES "
        If iUnidadDeMillon = 6 Then iCadena = "SEIS MILLONES "
        If iUnidadDeMillon = 7 Then iCadena = "SIETE MILLONES "
        If iUnidadDeMillon = 8 Then iCadena = "OCHO MILLONES "
        If iUnidadDeMillon = 9 Then iCadena = "NUEVE MILLONES "

        If iUnidadDeMillon > 9 Then
            iCadena = numeroALetrasEntero(iUnidadDeMillon) & " MILLONES "
        End If

        'Genera los cientos redondos.
        If iCentenaDeMil = 1 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "CIEN MIL "
        If iCentenaDeMil = 2 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "DOSCIENTOS MIL "
        If iCentenaDeMil = 3 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "TRECIENTOS MIL "
        If iCentenaDeMil = 4 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "CUATROCIENTOS MIL "
        If iCentenaDeMil = 5 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "QUINIENTOS MIL "
        If iCentenaDeMil = 6 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "SEICIENTOS MIL "
        If iCentenaDeMil = 7 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "SETECIENTOS MIL "
        If iCentenaDeMil = 8 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "OCHOCIENTOS MIL "
        If iCentenaDeMil = 9 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "NOVECIENTOS MIL "

        'Genera los cientos parciales.
        If iCentenaDeMil = 1 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "CIENTO "
        If iCentenaDeMil = 2 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "DOSCIENTOS "
        If iCentenaDeMil = 3 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "TRESCIENTOS "
        If iCentenaDeMil = 4 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "CUATROCIENTOS "
        If iCentenaDeMil = 5 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "QUINIENTOS "
        If iCentenaDeMil = 6 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "SEISCIENTOS "
        If iCentenaDeMil = 7 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "SETECIENTOS "
        If iCentenaDeMil = 8 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "OCHOCIENTOS "
        If iCentenaDeMil = 9 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "NOVECIENTOS "

        'Genera las decenas de mil redondas.
        If iDecenaDeMil = 1 And iUnidadDeMil = 0 Then iCadena = iCadena + "DIEZ MIL "
        If iDecenaDeMil = 2 And iUnidadDeMil = 0 Then iCadena = iCadena + "VEINTE MIL "
        If iDecenaDeMil = 3 And iUnidadDeMil = 0 Then iCadena = iCadena + "TREINTA MIL "
        If iDecenaDeMil = 4 And iUnidadDeMil = 0 Then iCadena = iCadena + "CUARENTA MIL "
        If iDecenaDeMil = 5 And iUnidadDeMil = 0 Then iCadena = iCadena + "CINCUENTA MIL "
        If iDecenaDeMil = 6 And iUnidadDeMil = 0 Then iCadena = iCadena + "SESENTA MIL "
        If iDecenaDeMil = 7 And iUnidadDeMil = 0 Then iCadena = iCadena + "SETENTA MIL "
        If iDecenaDeMil = 8 And iUnidadDeMil = 0 Then iCadena = iCadena + "OCHENTA MIL "
        If iDecenaDeMil = 9 And iUnidadDeMil = 0 Then iCadena = iCadena + "NOVENTA MIL "

        'Genera las decenas de mil parciales.
        If iDecenaDeMil = 1 And iUnidadDeMil > 5 Then iCadena = iCadena + "DIECI"
        If iDecenaDeMil = 2 And iUnidadDeMil > 0 Then iCadena = iCadena + "VEINTI"
        If iDecenaDeMil = 3 And iUnidadDeMil > 0 Then iCadena = iCadena + "TREINTA Y "
        If iDecenaDeMil = 4 And iUnidadDeMil > 0 Then iCadena = iCadena + "CUARENTA Y "
        If iDecenaDeMil = 5 And iUnidadDeMil > 0 Then iCadena = iCadena + "CINCUENTA Y "
        If iDecenaDeMil = 6 And iUnidadDeMil > 0 Then iCadena = iCadena + "SESENTA Y "
        If iDecenaDeMil = 7 And iUnidadDeMil > 0 Then iCadena = iCadena + "SETENTA Y "
        If iDecenaDeMil = 8 And iUnidadDeMil > 0 Then iCadena = iCadena + "OCHENTA Y "
        If iDecenaDeMil = 9 And iUnidadDeMil > 0 Then iCadena = iCadena + "NOVENTA Y "

        If iUnidadDeMil = 1 And iDecenaDeMil = 1 Then iCadena = iCadena + "ONCE MIL "
        If iUnidadDeMil = 2 And iDecenaDeMil = 1 Then iCadena = iCadena + "DOCE MIL "
        If iUnidadDeMil = 3 And iDecenaDeMil = 1 Then iCadena = iCadena + "TRECE MIL "
        If iUnidadDeMil = 4 And iDecenaDeMil = 1 Then iCadena = iCadena + "CATORCE MIL "
        If iUnidadDeMil = 5 And iDecenaDeMil = 1 Then iCadena = iCadena + "QUINCE MIL "
        If iUnidadDeMil = 6 And iDecenaDeMil = 1 Then iCadena = iCadena + "SEIS MIL "
        If iUnidadDeMil = 7 And iDecenaDeMil = 1 Then iCadena = iCadena + "SIETE MIL "
        If iUnidadDeMil = 8 And iDecenaDeMil = 1 Then iCadena = iCadena + "OCHO MIL "
        If iUnidadDeMil = 9 And iDecenaDeMil = 1 Then iCadena = iCadena + "NUEVE MIL "

        If iUnidadDeMil = 1 And iDecenaDeMil <> 1 Then iCadena = iCadena + "UN MIL "
        If iUnidadDeMil = 2 And iDecenaDeMil <> 1 Then iCadena = iCadena + "DOS MIL "
        If iUnidadDeMil = 3 And iDecenaDeMil <> 1 Then iCadena = iCadena + "TRES MIL "
        If iUnidadDeMil = 4 And iDecenaDeMil <> 1 Then iCadena = iCadena + "CUATRO MIL "
        If iUnidadDeMil = 5 And iDecenaDeMil <> 1 Then iCadena = iCadena + "CINCO MIL "
        If iUnidadDeMil = 6 And iDecenaDeMil <> 1 Then iCadena = iCadena + "SEIS MIL "
        If iUnidadDeMil = 7 And iDecenaDeMil <> 1 Then iCadena = iCadena + "SIETE MIL "
        If iUnidadDeMil = 8 And iDecenaDeMil <> 1 Then iCadena = iCadena + "OCHO MIL "
        If iUnidadDeMil = 9 And iDecenaDeMil <> 1 Then iCadena = iCadena + "NUEVE MIL "

        If iCentena = 1 And (iDecena > 0 Or iUnidad > 0) Then iCadena = iCadena + "CIENTO "
        If iCentena = 1 And (iDecena = 0 And iUnidad = 0) Then iCadena = iCadena + "CIEN "

        If iCentena = 2 Then iCadena = iCadena + "DOSCIENTOS "
        If iCentena = 3 Then iCadena = iCadena + "TRESCIENTOS "
        If iCentena = 4 Then iCadena = iCadena + "CUATROCIENTOS "
        If iCentena = 5 Then iCadena = iCadena + "QUINIENTOS "
        If iCentena = 6 Then iCadena = iCadena + "SEISCIENTOS "
        If iCentena = 7 Then iCadena = iCadena + "SETECIENTOS "
        If iCentena = 8 Then iCadena = iCadena + "OCHOCIENTOS "
        If iCentena = 9 Then iCadena = iCadena + "NOVECIENTOS "

        If iDecena = 1 And iUnidad = 0 Then iCadena = iCadena + "DIEZ "
        If iDecena = 1 And iUnidad = 1 Then iCadena = iCadena + "ONCE "
        If iDecena = 1 And iUnidad = 2 Then iCadena = iCadena + "DOCE "
        If iDecena = 1 And iUnidad = 3 Then iCadena = iCadena + "TRECE "
        If iDecena = 1 And iUnidad = 4 Then iCadena = iCadena + "CATORCE "
        If iDecena = 1 And iUnidad = 5 Then iCadena = iCadena + "QUINCE "
        If iDecena = 1 And iUnidad = 6 Then iCadena = iCadena + "DIECISEIS "
        If iDecena = 1 And iUnidad = 7 Then iCadena = iCadena + "DIECISIETE "
        If iDecena = 1 And iUnidad = 8 Then iCadena = iCadena + "DIECIOCHO "
        If iDecena = 1 And iUnidad = 9 Then iCadena = iCadena + "DIECINUEVE "

        If iDecena = 2 And iUnidad = 0 Then iCadena = iCadena + "VEINTE "
        If iDecena = 3 And iUnidad = 0 Then iCadena = iCadena + "TREINTA "
        If iDecena = 4 And iUnidad = 0 Then iCadena = iCadena + "CUARENTA "
        If iDecena = 5 And iUnidad = 0 Then iCadena = iCadena + "CINCUENTA "
        If iDecena = 6 And iUnidad = 0 Then iCadena = iCadena + "SESENTA "
        If iDecena = 7 And iUnidad = 0 Then iCadena = iCadena + "SETENTA "
        If iDecena = 8 And iUnidad = 0 Then iCadena = iCadena + "OCHENTA "
        If iDecena = 9 And iUnidad = 0 Then iCadena = iCadena + "NOVENTA "

        If iDecena = 2 And iUnidad > 0 Then iCadena = iCadena + "VEINTI"
        If iDecena = 3 And iUnidad > 0 Then iCadena = iCadena + "TREINTA Y "
        If iDecena = 4 And iUnidad > 0 Then iCadena = iCadena + "CUARENTA Y "
        If iDecena = 5 And iUnidad > 0 Then iCadena = iCadena + "CINCUENTA Y "
        If iDecena = 6 And iUnidad > 0 Then iCadena = iCadena + "SESENTA Y "
        If iDecena = 7 And iUnidad > 0 Then iCadena = iCadena + "SETENTA Y "
        If iDecena = 8 And iUnidad > 0 Then iCadena = iCadena + "OCHENTA Y "
        If iDecena = 9 And iUnidad > 0 Then iCadena = iCadena + "NOVENTA Y "

        If iUnidad = 1 And iDecena <> 1 Then iCadena = iCadena + "UNO "
        If iUnidad = 2 And iDecena <> 1 Then iCadena = iCadena + "DOS "
        If iUnidad = 3 And iDecena <> 1 Then iCadena = iCadena + "TRES "
        If iUnidad = 4 And iDecena <> 1 Then iCadena = iCadena + "CUATRO "
        If iUnidad = 5 And iDecena <> 1 Then iCadena = iCadena + "CINCO "
        If iUnidad = 6 And iDecena <> 1 Then iCadena = iCadena + "SEIS "
        If iUnidad = 7 And iDecena <> 1 Then iCadena = iCadena + "SIETE "
        If iUnidad = 8 And iDecena <> 1 Then iCadena = iCadena + "OCHO "
        If iUnidad = 9 And iDecena <> 1 Then iCadena = iCadena + "NUEVE "

        If iUnidad = 0 And iDecena = 0 And iCentena = 0 And iUnidadDeMil = 0 And iDecenaDeMil = 0 And iCentenaDeMil = 0 And iUnidadDeMillon = 0 Then iCadena = "CERO "

        Return (iCadena & iCentavos)

    End Function

    Public Shared Function numeroALetrasEntero(ByVal eMonto As Integer) As String
        Dim iLongitud As Integer
        Dim iCadena As String
        Dim iUnidad As Integer
        Dim iDecena As Integer
        Dim iCentena As Integer
        Dim iUnidadDeMil As Integer
        Dim iDecenaDeMil As Integer
        Dim iCentenaDeMil As Integer
        Dim iUnidadDeMillon As Integer
        Dim iMontoString As String

        iMontoString = CStr(Format(eMonto))
        iLongitud = Len(iMontoString)
        iCadena = ""

        'Cifras de 1 a 9.
        If iLongitud = 1 Then
            iUnidad = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 99.
        If iLongitud = 2 Then
            iUnidad = Val(Mid(iMontoString, 2, 1))
            iDecena = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 999.
        If iLongitud = 3 Then
            iUnidad = Val(Mid(iMontoString, 3, 1))
            iDecena = Val(Mid(iMontoString, 2, 1))
            iCentena = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 9999.
        If iLongitud = 4 Then
            iUnidad = Val(Mid(iMontoString, 4, 1))
            iDecena = Val(Mid(iMontoString, 3, 1))
            iCentena = Val(Mid(iMontoString, 2, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 99999.
        If iLongitud = 5 Then
            iUnidad = Val(Mid(iMontoString, 5, 1))
            iDecena = Val(Mid(iMontoString, 4, 1))
            iCentena = Val(Mid(iMontoString, 3, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 2, 1))
            iDecenaDeMil = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 999999.
        If iLongitud = 6 Then
            iUnidad = Val(Mid(iMontoString, 6, 1))
            iDecena = Val(Mid(iMontoString, 5, 1))
            iCentena = Val(Mid(iMontoString, 4, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 3, 1))
            iDecenaDeMil = Val(Mid(iMontoString, 2, 1))
            iCentenaDeMil = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 9999999.
        If iLongitud = 7 Then
            iUnidad = Val(Mid(iMontoString, 7, 1))
            iDecena = Val(Mid(iMontoString, 6, 1))
            iCentena = Val(Mid(iMontoString, 5, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 4, 1))
            iDecenaDeMil = Val(Mid(iMontoString, 3, 1))
            iCentenaDeMil = Val(Mid(iMontoString, 2, 1))
            iUnidadDeMillon = Val(Mid(iMontoString, 1, 1))
        End If

        'Cifras de 1 a 99999999.
        If iLongitud = 8 Then
            iUnidad = Val(Mid(iMontoString, 8, 1))
            iDecena = Val(Mid(iMontoString, 7, 1))
            iCentena = Val(Mid(iMontoString, 6, 1))
            iUnidadDeMil = Val(Mid(iMontoString, 5, 1))
            iDecenaDeMil = Val(Mid(iMontoString, 4, 1))
            iCentenaDeMil = Val(Mid(iMontoString, 3, 1))
            iUnidadDeMillon = Val(Mid(iMontoString, 1, 2))
        End If

        'Genera los millones.
        If iUnidadDeMillon = 1 Then iCadena = "UN MILLON "
        If iUnidadDeMillon = 2 Then iCadena = "DOS MILLONES "
        If iUnidadDeMillon = 3 Then iCadena = "TRES MILLONES "
        If iUnidadDeMillon = 4 Then iCadena = "CUATRO MILLONES "
        If iUnidadDeMillon = 5 Then iCadena = "CINCO MILLONES "
        If iUnidadDeMillon = 6 Then iCadena = "SEIS MILLONES "
        If iUnidadDeMillon = 7 Then iCadena = "SIETE MILLONES "
        If iUnidadDeMillon = 8 Then iCadena = "OCHO MILLONES "
        If iUnidadDeMillon = 9 Then iCadena = "NUEVE MILLONES "

        If iUnidadDeMillon > 9 Then
            iCadena = numeroALetrasEntero(iUnidadDeMillon) & " MILLONES "
        End If


        'Genera los cientos redondos.
        If iCentenaDeMil = 1 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "CIEN MIL "
        If iCentenaDeMil = 2 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "DOSCIENTOS MIL "
        If iCentenaDeMil = 3 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "TRECIENTOS MIL "
        If iCentenaDeMil = 4 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "CUATROCIENTOS MIL "
        If iCentenaDeMil = 5 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "QUINIENTOS MIL "
        If iCentenaDeMil = 6 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "SEICIENTOS MIL "
        If iCentenaDeMil = 7 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "SETECIENTOS MIL "
        If iCentenaDeMil = 8 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "OCHOCIENTOS MIL "
        If iCentenaDeMil = 9 And iUnidadDeMil = 0 And iDecenaDeMil = 0 Then iCadena = iCadena + "NOVECIENTOS MIL "

        'Genera los cientos parciales.
        If iCentenaDeMil = 1 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "CIENTO "
        If iCentenaDeMil = 2 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "DOSCIENTOS "
        If iCentenaDeMil = 3 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "TRESCIENTOS "
        If iCentenaDeMil = 4 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "CUATROCIENTOS "
        If iCentenaDeMil = 5 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "QUINIENTOS "
        If iCentenaDeMil = 6 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "SEISCIENTOS "
        If iCentenaDeMil = 7 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "SETECIENTOS "
        If iCentenaDeMil = 8 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "OCHOCIENTOS "
        If iCentenaDeMil = 9 And (iUnidadDeMil > 0 Or iDecenaDeMil > 0) Then iCadena = iCadena + "NOVECIENTOS "

        'Genera las decenas de mil redondas.
        If iDecenaDeMil = 1 And iUnidadDeMil = 0 Then iCadena = iCadena + "DIEZ MIL "
        If iDecenaDeMil = 2 And iUnidadDeMil = 0 Then iCadena = iCadena + "VEINTE MIL "
        If iDecenaDeMil = 3 And iUnidadDeMil = 0 Then iCadena = iCadena + "TREINTA MIL "
        If iDecenaDeMil = 4 And iUnidadDeMil = 0 Then iCadena = iCadena + "CUARENTA MIL "
        If iDecenaDeMil = 5 And iUnidadDeMil = 0 Then iCadena = iCadena + "CINCUENTA MIL "
        If iDecenaDeMil = 6 And iUnidadDeMil = 0 Then iCadena = iCadena + "SESENTA MIL "
        If iDecenaDeMil = 7 And iUnidadDeMil = 0 Then iCadena = iCadena + "SETENTA MIL "
        If iDecenaDeMil = 8 And iUnidadDeMil = 0 Then iCadena = iCadena + "OCHENTA MIL "
        If iDecenaDeMil = 9 And iUnidadDeMil = 0 Then iCadena = iCadena + "NOVENTA MIL "

        'Genera las decenas de mil parciales.
        If iDecenaDeMil = 1 And iUnidadDeMil > 5 Then iCadena = iCadena + "DIECI"
        If iDecenaDeMil = 2 And iUnidadDeMil > 0 Then iCadena = iCadena + "VEINTI"
        If iDecenaDeMil = 3 And iUnidadDeMil > 0 Then iCadena = iCadena + "TREINTA Y "
        If iDecenaDeMil = 4 And iUnidadDeMil > 0 Then iCadena = iCadena + "CUARENTA Y "
        If iDecenaDeMil = 5 And iUnidadDeMil > 0 Then iCadena = iCadena + "CINCUENTA Y "
        If iDecenaDeMil = 6 And iUnidadDeMil > 0 Then iCadena = iCadena + "SESENTA Y "
        If iDecenaDeMil = 7 And iUnidadDeMil > 0 Then iCadena = iCadena + "SETENTA Y "
        If iDecenaDeMil = 8 And iUnidadDeMil > 0 Then iCadena = iCadena + "OCHENTA Y "
        If iDecenaDeMil = 9 And iUnidadDeMil > 0 Then iCadena = iCadena + "NOVENTA Y "

        If iUnidadDeMil = 1 And iDecenaDeMil = 1 Then iCadena = iCadena + "ONCE MIL "
        If iUnidadDeMil = 2 And iDecenaDeMil = 1 Then iCadena = iCadena + "DOCE MIL "
        If iUnidadDeMil = 3 And iDecenaDeMil = 1 Then iCadena = iCadena + "TRECE MIL "
        If iUnidadDeMil = 4 And iDecenaDeMil = 1 Then iCadena = iCadena + "CATORCE MIL "
        If iUnidadDeMil = 5 And iDecenaDeMil = 1 Then iCadena = iCadena + "QUINCE MIL "
        If iUnidadDeMil = 6 And iDecenaDeMil = 1 Then iCadena = iCadena + "SEIS MIL "
        If iUnidadDeMil = 7 And iDecenaDeMil = 1 Then iCadena = iCadena + "SIETE MIL "
        If iUnidadDeMil = 8 And iDecenaDeMil = 1 Then iCadena = iCadena + "OCHO MIL "
        If iUnidadDeMil = 9 And iDecenaDeMil = 1 Then iCadena = iCadena + "NUEVE MIL "

        If iUnidadDeMil = 1 And iDecenaDeMil <> 1 Then iCadena = iCadena + "UN MIL "
        If iUnidadDeMil = 2 And iDecenaDeMil <> 1 Then iCadena = iCadena + "DOS MIL "
        If iUnidadDeMil = 3 And iDecenaDeMil <> 1 Then iCadena = iCadena + "TRES MIL "
        If iUnidadDeMil = 4 And iDecenaDeMil <> 1 Then iCadena = iCadena + "CUATRO MIL "
        If iUnidadDeMil = 5 And iDecenaDeMil <> 1 Then iCadena = iCadena + "CINCO MIL "
        If iUnidadDeMil = 6 And iDecenaDeMil <> 1 Then iCadena = iCadena + "SEIS MIL "
        If iUnidadDeMil = 7 And iDecenaDeMil <> 1 Then iCadena = iCadena + "SIETE MIL "
        If iUnidadDeMil = 8 And iDecenaDeMil <> 1 Then iCadena = iCadena + "OCHO MIL "
        If iUnidadDeMil = 9 And iDecenaDeMil <> 1 Then iCadena = iCadena + "NUEVE MIL "

        If iCentena = 1 And (iDecena > 0 Or iUnidad > 0) Then iCadena = iCadena + "CIENTO "
        If iCentena = 1 And (iDecena = 0 And iUnidad = 0) Then iCadena = iCadena + "CIEN "

        If iCentena = 2 Then iCadena = iCadena + "DOSCIENTOS "
        If iCentena = 3 Then iCadena = iCadena + "TRESCIENTOS "
        If iCentena = 4 Then iCadena = iCadena + "CUATROCIENTOS "
        If iCentena = 5 Then iCadena = iCadena + "QUINIENTOS "
        If iCentena = 6 Then iCadena = iCadena + "SEISCIENTOS "
        If iCentena = 7 Then iCadena = iCadena + "SETECIENTOS "
        If iCentena = 8 Then iCadena = iCadena + "OCHOCIENTOS "
        If iCentena = 9 Then iCadena = iCadena + "NOVECIENTOS "

        If iDecena = 1 And iUnidad = 0 Then iCadena = iCadena + "DIEZ "
        If iDecena = 1 And iUnidad = 1 Then iCadena = iCadena + "ONCE "
        If iDecena = 1 And iUnidad = 2 Then iCadena = iCadena + "DOCE "
        If iDecena = 1 And iUnidad = 3 Then iCadena = iCadena + "TRECE "
        If iDecena = 1 And iUnidad = 4 Then iCadena = iCadena + "CATORCE "
        If iDecena = 1 And iUnidad = 5 Then iCadena = iCadena + "QUINCE "
        If iDecena = 1 And iUnidad = 6 Then iCadena = iCadena + "DIECISEIS "
        If iDecena = 1 And iUnidad = 7 Then iCadena = iCadena + "DIECISIETE "
        If iDecena = 1 And iUnidad = 8 Then iCadena = iCadena + "DIECIOCHO "
        If iDecena = 1 And iUnidad = 9 Then iCadena = iCadena + "DIECINUEVE "

        If iDecena = 2 And iUnidad = 0 Then iCadena = iCadena + "VEINTE "
        If iDecena = 3 And iUnidad = 0 Then iCadena = iCadena + "TREINTA "
        If iDecena = 4 And iUnidad = 0 Then iCadena = iCadena + "CUARENTA "
        If iDecena = 5 And iUnidad = 0 Then iCadena = iCadena + "CINCUENTA "
        If iDecena = 6 And iUnidad = 0 Then iCadena = iCadena + "SESENTA "
        If iDecena = 7 And iUnidad = 0 Then iCadena = iCadena + "SETENTA "
        If iDecena = 8 And iUnidad = 0 Then iCadena = iCadena + "OCHENTA "
        If iDecena = 9 And iUnidad = 0 Then iCadena = iCadena + "NOVENTA "

        If iDecena = 2 And iUnidad > 0 Then iCadena = iCadena + "VEINTI"
        If iDecena = 3 And iUnidad > 0 Then iCadena = iCadena + "TREINTA Y "
        If iDecena = 4 And iUnidad > 0 Then iCadena = iCadena + "CUARENTA Y "
        If iDecena = 5 And iUnidad > 0 Then iCadena = iCadena + "CINCUENTA Y "
        If iDecena = 6 And iUnidad > 0 Then iCadena = iCadena + "SESENTA Y "
        If iDecena = 7 And iUnidad > 0 Then iCadena = iCadena + "SETENTA Y "
        If iDecena = 8 And iUnidad > 0 Then iCadena = iCadena + "OCHENTA Y "
        If iDecena = 9 And iUnidad > 0 Then iCadena = iCadena + "NOVENTA Y "

        If iUnidad = 1 And iDecena <> 1 Then iCadena = iCadena + "UNO "
        If iUnidad = 2 And iDecena <> 1 Then iCadena = iCadena + "DOS "
        If iUnidad = 3 And iDecena <> 1 Then iCadena = iCadena + "TRES "
        If iUnidad = 4 And iDecena <> 1 Then iCadena = iCadena + "CUATRO "
        If iUnidad = 5 And iDecena <> 1 Then iCadena = iCadena + "CINCO "
        If iUnidad = 6 And iDecena <> 1 Then iCadena = iCadena + "SEIS "
        If iUnidad = 7 And iDecena <> 1 Then iCadena = iCadena + "SIETE "
        If iUnidad = 8 And iDecena <> 1 Then iCadena = iCadena + "OCHO "
        If iUnidad = 9 And iDecena <> 1 Then iCadena = iCadena + "NUEVE "

        If iUnidad = 0 And iDecena = 0 And iCentena = 0 And iUnidadDeMil = 0 And iDecenaDeMil = 0 And iCentenaDeMil = 0 And iUnidadDeMillon = 0 Then iCadena = "CERO "

        Return iCadena

    End Function

    Public Shared Function numeroALetrasSinCentavos(ByVal eMonto As Double) As String
        Dim iCadena As String

        iCadena = numeroALetrasEntero(CInt(Left(CStr(Format(eMonto, "0.00")), Len(CStr(Format(eMonto, "0.00"))) - 3)))
        iCadena &= " CON " + numeroALetrasEntero(CInt(Right(CStr(Format(eMonto, "0.00")), 2)))

        Return iCadena

    End Function

    Public Shared Function mesALetras(ByVal eMes As Integer) As String
        Select Case eMes
            Case 1
                Return "ENERO"
            Case 2
                Return "FEBRERO"
            Case 3
                Return "MARZO"
            Case 4
                Return "ABRIL"
            Case 5
                Return "MAYO"
            Case 6
                Return "JUNIO"
            Case 7
                Return "JULIO"
            Case 8
                Return "AGOSTO"
            Case 9
                Return "SEPTIEMBRE"
            Case 10
                Return "OCTUBRE"
            Case 11
                Return "NOVIEMBRE"
            Case 12
                Return "DICIEMBRE"
            Case Else
                Return "FECHA INVALIDA"
        End Select
    End Function

    Public Shared Sub zipearDirectorio(ByVal eDirectorio As String, ByVal ePathDestino As String)
        'If IsNothing(ConfigurationManager.AppSettings("ZipiarViejo")) OrElse (ConfigurationManager.AppSettings("ZipiarViejo").ToUpper = "FALSE") Then
        Try
            System.IO.Compression.ZipFile.CreateFromDirectory(eDirectorio, ePathDestino, System.IO.Compression.CompressionLevel.Optimal, True)
        Catch Exception As Exception
            Throw New ArchivoNoZipeadoException(Exception)
        Finally

        End Try
        'Else

        'Dim iNombresArchivos() As String
        '    Dim iCrc32 As New Crc32
        '    Dim iZipOutputStream As ZipOutputStream
        '    Dim iZipEntry As ZipEntry
        '    Dim iNombreAchivo As String
        '    Dim iArchivo As FileStream

        '    Try
        '        iNombresArchivos = Directory.GetFiles(eDirectorio)
        '        iZipOutputStream = New ZipOutputStream(File.Create(ePathDestino))
        '        iZipOutputStream.SetLevel(6)

        '        'Compression Level: 0-9
        '        '0: no(Compression)
        '        '9: maximum compression

        '        For Each iNombreAchivo In iNombresArchivos
        '            iArchivo = File.OpenRead(iNombreAchivo)
        '            Dim iBuffer(iArchivo.Length - 1) As Byte

        '            iArchivo.Read(iBuffer, 0, iBuffer.Length)
        '            iZipEntry = New ZipEntry(Right(iNombreAchivo, iNombreAchivo.Length - InStrRev(iNombreAchivo, "\")))

        '            iZipEntry.DateTime = DateTime.Now
        '            iZipEntry.Size = iArchivo.Length
        '            iArchivo.Close()
        '            iCrc32.Reset()
        '            iCrc32.Update(iBuffer)
        '            iZipEntry.Crc = iCrc32.Value
        '            iZipOutputStream.PutNextEntry(iZipEntry)
        '            iZipOutputStream.Write(iBuffer, 0, iBuffer.Length)
        '        Next

        '    Catch Exception As Exception
        '        Throw New ArchivoNoZipeadoException(Exception)
        '    Finally
        '        If Not IsNothing(iZipOutputStream) Then iZipOutputStream.Finish()
        '        If Not IsNothing(iZipOutputStream) Then iZipOutputStream.Close()
        '        iCrc32 = Nothing
        '        iZipEntry = Nothing
        '        iArchivo = Nothing
        '    End Try
        ' End If
    End Sub

    Public Shared Sub zipearArchivos(ByVal eArchivosAZipear As Collection, ByVal ePathDestino As String)
        '  If IsNothing(ConfigurationManager.AppSettings("ZipiarViejo")) OrElse (ConfigurationManager.AppSettings("ZipiarViejo").ToUpper = "FALSE") Then
        Dim iNombreAchivo As String
        Try
            If File.Exists(ePathDestino) Then File.Delete(ePathDestino)
            Directory.CreateDirectory(ePathDestino.ToUpper.Replace(".ZIP", ""))

            For i As Integer = 1 To eArchivosAZipear.Count
                iNombreAchivo = eArchivosAZipear.Item(i)
                File.Copy(iNombreAchivo, ePathDestino.ToUpper.Replace(".ZIP", "") & Right(iNombreAchivo, iNombreAchivo.Length - iNombreAchivo.LastIndexOf("\")))
            Next
            System.IO.Compression.ZipFile.CreateFromDirectory(ePathDestino.ToUpper.Replace(".ZIP", ""), ePathDestino, System.IO.Compression.CompressionLevel.Optimal, False)
        Catch Exception As Exception
            Throw New ArchivoNoZipeadoException(Exception)
        Finally
            If Directory.Exists(ePathDestino.ToUpper.Replace(".ZIP", "")) Then Directory.Delete(ePathDestino.ToUpper.Replace(".ZIP", ""), True)
        End Try
        'Else

        '    Dim iCrc32 As New Crc32
        '    Dim iZipOutputStream As ZipOutputStream
        '    Dim iZipEntry As ZipEntry
        '    Dim iNombreAchivo As String
        '    Dim iArchivo As FileStream
        '    Dim i As Integer

        '    Try
        '        iZipOutputStream = New ZipOutputStream(File.Create(ePathDestino))
        '        iZipOutputStream.SetLevel(6)

        '        'Compression Level: 0-9
        '        '0: no(Compression)
        '        '9: maximum compression

        '        For i = 1 To eArchivosAZipear.Count
        '            iNombreAchivo = eArchivosAZipear.Item(i)
        '            iArchivo = File.OpenRead(iNombreAchivo)
        '            Dim iBuffer(iArchivo.Length - 1) As Byte

        '            iArchivo.Read(iBuffer, 0, iBuffer.Length)
        '            iZipEntry = New ZipEntry(Right(iNombreAchivo, iNombreAchivo.Length - InStrRev(iNombreAchivo, "\")))
        '            iZipEntry.DateTime = DateTime.Now
        '            iZipEntry.Size = iArchivo.Length
        '            iArchivo.Close()
        '            iCrc32.Reset()
        '            iCrc32.Update(iBuffer)
        '            iZipEntry.Crc = iCrc32.Value
        '            iZipOutputStream.PutNextEntry(iZipEntry)
        '            iZipOutputStream.Write(iBuffer, 0, iBuffer.Length)
        '        Next i


        '    Catch Exception As Exception
        '        Throw New ArchivoNoZipeadoException(Exception)
        '    Finally
        '        If Not IsNothing(iZipOutputStream) Then iZipOutputStream.Finish()
        '        If Not IsNothing(iZipOutputStream) Then iZipOutputStream.Close()
        '        iCrc32 = Nothing
        '        iZipEntry = Nothing
        '        iArchivo = Nothing
        '    End Try
        'End If
    End Sub

    Public Shared Sub UnZip(ByVal ePathArchivoZipeado As String)
        Dim FileZipUploaded As String = ePathArchivoZipeado
        Dim ZipStream As New ZipInputStream(File.OpenRead(FileZipUploaded))
        Dim TheEntry As ZipEntry = ZipStream.GetNextEntry()

        Try
            Do Until TheEntry Is Nothing
                Dim data(1024) As Byte
                Dim fileNameUnzipped As String = Left(ePathArchivoZipeado, InStrRev(ePathArchivoZipeado, "\")) & TheEntry.Name.ToString
                Dim newfs As FileStream = File.Create(fileNameUnzipped)
                Dim bw As New BinaryWriter(newfs)

                Dim buffer(1024) As Byte
                Dim length As Integer
                Dim dataToRead As Long
                dataToRead = TheEntry.Size
                While dataToRead > 0
                    length = ZipStream.Read(buffer, 0, 1024)
                    bw.Write(buffer, 0, length)
                    ReDim buffer(1024) ' Clear the buffer
                    dataToRead = dataToRead - length
                End While
                bw.Close()
                newfs.Close()
                TheEntry = ZipStream.GetNextEntry()
            Loop

        Catch Exception As Exception
            Throw New ArchivoNoZipeadoException(Exception)
        Finally
            FileZipUploaded = Nothing
            ZipStream = Nothing
            TheEntry = Nothing
        End Try

    End Sub

    Public Shared Function UnZipNombreArchivo(ByVal ePathArchivoZipeado As String) As String()
        Dim FileZipUploaded As String = ePathArchivoZipeado
        Dim ZipStream As New ZipInputStream(File.OpenRead(FileZipUploaded))
        Dim TheEntry As ZipEntry = ZipStream.GetNextEntry()
        Dim iArchivo As String()
        Dim iColeccionArchivos As New Collection
        Dim i As Integer
        Try



            Do Until TheEntry Is Nothing
                Dim data(1024) As Byte
                Dim fileNameUnzipped As String = Left(ePathArchivoZipeado, InStrRev(ePathArchivoZipeado, "\")) & TheEntry.Name.ToString

                iColeccionArchivos.Add(TheEntry.Name.ToString)

                i += 1
                Dim newfs As FileStream = File.Create(fileNameUnzipped)
                Dim bw As New BinaryWriter(newfs)

                Dim buffer(1024) As Byte
                Dim length As Integer
                Dim dataToRead As Long
                dataToRead = TheEntry.Size
                While dataToRead > 0
                    length = ZipStream.Read(buffer, 0, 1024)
                    bw.Write(buffer, 0, length)
                    ReDim buffer(1024) ' Clear the buffer
                    dataToRead = dataToRead - length
                End While
                bw.Close()
                newfs.Close()
                TheEntry = ZipStream.GetNextEntry()
            Loop
            ReDim iArchivo(iColeccionArchivos.Count - 1)
            For i = 1 To iColeccionArchivos.Count
                iArchivo(i - 1) = iColeccionArchivos.Item(i)
            Next

            Return iArchivo
        Catch Exception As Exception
            Throw New ArchivoNoZipeadoException(Exception)
        Finally
            FileZipUploaded = Nothing
            ZipStream = Nothing
            TheEntry = Nothing
        End Try

    End Function

    Public Shared Function sqlFormatoFecha(ByVal eCampo As String, ByVal eFormato As enumFormatoFecha) As String

        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'Formatos para SQL server
            Select Case eFormato
                Case enumFormatoFecha.DD
                    Return "DATEPART(d," & eCampo & ")"
                Case enumFormatoFecha.MM
                    Return "DATEPART(m," & eCampo & ")"
                Case enumFormatoFecha.MMDD
                    Return "Convert(varchar, DatePart(MM, " & eCampo & ")) +'/'+ Convert(varchar,DATEPART (DD," & eCampo & "))"
                Case enumFormatoFecha.DDMMYY
                    Return "Convert(varchar," & eCampo & ",3)"
                Case enumFormatoFecha.DDMMYYHHMM
                    Return "Convert(varchar," & eCampo & ",3)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.DDMMYYHHMMSS
                    Return "Convert(varchar," & eCampo & ",3)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
                Case enumFormatoFecha.DDMMYYYY
                    Return "Convert(varchar," & eCampo & ",103)"
                Case enumFormatoFecha.DDMMYYYYHHMM
                    Return "Convert(varchar," & eCampo & ",103)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.DDMMYYYYHHMMSS
                    Return "Convert(varchar," & eCampo & ",103)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
                Case enumFormatoFecha.MMYY
                    Return "( Convert(varchar,DATEPART(m," & eCampo & ")) + '/' + Convert(varchar,right(DATEPART(yyyy," & eCampo & ",2))))"
                Case enumFormatoFecha.MMYYYY
                    Return "( Convert(varchar,DATEPART(m," & eCampo & ")) + '/' + Convert(varchar,DATEPART(yyyy," & eCampo & ")))"
                Case enumFormatoFecha.HHMM
                    Return "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.HHMMSS
                    Return "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
                Case enumFormatoFecha.YYMM
                    Return "( Convert(varchar,right(DATEPART(yyyy," & eCampo & "),2)) + '/' + Convert(varchar,DATEPART(m," & eCampo & ")))"
                Case enumFormatoFecha.MMYYSINSEPARADOR
                    Return "( Convert(varchar,right(DATEPART(yyyy," & eCampo & "),2)) + Convert(varchar,DATEPART(m," & eCampo & ")))"
                Case enumFormatoFecha.YYMMDD
                    Return "Convert(varchar," & eCampo & ",11)"
                Case enumFormatoFecha.YYMMDDHHMM
                    Return "Convert(varchar," & eCampo & ",11)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.YYMMDDHHMMSS
                    Return "Convert(varchar," & eCampo & ",11)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
                Case enumFormatoFecha.YYYYMM
                    Return "( Convert(varchar,DATEPART(yyyy," & eCampo & ")) + '/' + Convert(varchar,DATEPART(m," & eCampo & ")))"
                Case enumFormatoFecha.YYYYMMDD
                    Return "Convert(varchar," & eCampo & ",111)"
                Case enumFormatoFecha.YYYYMMDDHHMM
                    Return "Convert(varchar," & eCampo & ",111)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.YYYYMMDDHHMMSS
                    Return "Convert(varchar," & eCampo & ",20)"
                Case enumFormatoFecha.YYYY
                    Return "Convert(varchar,DatePart(YYYY," & eCampo & "))"
            End Select
        Else

            'Formatos para MySql
            Select Case eFormato

                Case enumFormatoFecha.DD
                    Return "Date_Format(" & eCampo & ",'%d')"
                Case enumFormatoFecha.MM
                    Return "Date_Format(" & eCampo & ",'%m')"
                Case enumFormatoFecha.MMDD
                    Return "Date_Format(" & eCampo & ", '%m-%d')"
                Case enumFormatoFecha.YY
                    Return "Date_Format(" & eCampo & ",'%y')"
                Case enumFormatoFecha.YYYY
                    Return "Date_Format(" & eCampo & ",'%Y')"
                Case enumFormatoFecha.DDMMYY
                    Return "Date_Format(" & eCampo & ",'%d/%m/%y')"
                Case enumFormatoFecha.DDMMYYHHMM
                    Return "Date_Format(" & eCampo & ",'%d/%m/%y %H:%i')"
                Case enumFormatoFecha.DDMMYYHHMMSS
                    Return "Date_Format(" & eCampo & ",'%d/%m/%y %H:%i:%s')"
                Case enumFormatoFecha.DDMMYYYY
                    Return "Date_Format(" & eCampo & ",'%d/%m/%Y')"
                Case enumFormatoFecha.DDMMYYYYHHMM
                    Return "Date_Format(" & eCampo & ",'%d/%m/%Y %H:%i')"
                Case enumFormatoFecha.DDMMYYYYHHMMSS
                    Return "Date_Format(" & eCampo & ",'%d/%m/%Y %H:%i:%s')"
                Case enumFormatoFecha.MMYY
                    Return "Date_Format(" & eCampo & ",'%m/%y')"
                Case enumFormatoFecha.MMYYSINSEPARADOR
                    Return "Date_Format(" & eCampo & ",'%m%y')"
                Case enumFormatoFecha.MMYYYY
                    Return "Date_Format(" & eCampo & ",'%m/%Y')"
                Case enumFormatoFecha.HHMM
                    Return "Date_Format(" & eCampo & ",'%H:%i')"
                Case enumFormatoFecha.HHMMSS
                    Return "Date_Format(" & eCampo & ",'%H:%i:%s')"
                Case enumFormatoFecha.YYMM
                    Return "Date_Format(" & eCampo & ",'%y/%m')"
                Case enumFormatoFecha.YYMMDD
                    Return "Date_Format(" & eCampo & ",'%y/%m/%d')"
                Case enumFormatoFecha.YYMMDDHHMM
                    Return "Date_Format(" & eCampo & ",'%y/%m/%d %H:%i')"
                Case enumFormatoFecha.YYMMDDHHMMSS
                    Return "Date_Format(" & eCampo & ",'%y/%m/%d %H:%i:%s')"
                Case enumFormatoFecha.YYYYMM
                    Return "Date_Format(" & eCampo & ",'%Y/%m')"
                Case enumFormatoFecha.YYYYMMDD
                    Return "Date_Format(" & eCampo & ",'%Y/%m/%d')"
                Case enumFormatoFecha.YYYYMMDDHHMM
                    Return "Date_Format(" & eCampo & ",'%Y/%m/%d %H:%i')"
                Case enumFormatoFecha.YYYYMMDDHHMMSS
                    Return "Date_Format(" & eCampo & ",'%Y/%m/%d %H:%i:%s')"
                Case enumFormatoFecha.YYYYMMDDGUION
                    Return "Date_Format(" & eCampo & ",'%Y-%m-%d')"
            End Select
        End If
    End Function

    Public Shared Function sqlConcatenar(ByVal eCampos As String) As String
        If (iBaseDeDatos = "SQLSERVER") Then
            'Concatenar con SQL Server
            Dim iCaracter, eColumna, iResultadoFinal As String
            Dim iComasAbiertos As Integer
            iResultadoFinal = ""
            eColumna = ""
            For i As Integer = 1 To Len(eCampos)
                iCaracter = Mid(eCampos, i, 1)
                If iCaracter = "," And iComasAbiertos = 0 Then
                    iResultadoFinal &= "Convert(varchar," & eColumna & ") +"
                    eColumna = ""
                ElseIf iCaracter = "(" Then
                    iComasAbiertos += 1
                    eColumna &= iCaracter
                ElseIf iCaracter = ")" Then
                    iComasAbiertos -= 1
                    eColumna &= iCaracter
                Else
                    eColumna &= iCaracter
                End If
            Next
            iResultadoFinal &= "Convert(varchar," & eColumna & ")"
            eCampos = iResultadoFinal
        ElseIf iBaseDeDatos = "MYSQL" Then
            eCampos = "Concat_ws(''," & eCampos & ")"
        Else
            eCampos = "Concat(" & eCampos & ")"
        End If
        Return eCampos
    End Function

    Public Shared Function sqlGroupConcatenar(ByVal eCampo As String, ByVal eSeparador As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return "dbo.group_concat_d(" & eCampo & "," & eSeparador & ")"
        Else
            'MySQL
            Return "GROUP_CONCAT(" & eCampo & " SEPARATOR " & eSeparador & ")"
        End If
    End Function

    Public Shared Function sqlMOD(ByVal eCampo As String, ByVal eDivisor As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'Right SQL Server
            Return "(" & eCampo & "%" & eDivisor & ")"
        Else
            'lPad MySQL 
            Return "(" & eCampo & " MOD " & eDivisor & ")"
        End If
    End Function

    Public Shared Function sqlRellenarAIzquierda(ByVal eCampo As String, ByVal eCorte As String, ByVal eRelleno As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'Right SQL Server
            Return "right(('" & eRelleno & "' + Convert(varchar," & eCampo & "))," & eCorte & ")"
        Else
            'lPad MySQL 
            Return "lpad(" & eCampo & "," & eCorte & ",'" & eRelleno & "')"
        End If
    End Function

    Public Shared Function sqlRellenarADerecha(ByVal eCampo As String, ByVal eCorte As String, ByVal eRelleno As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'Right SQL Server
            Return "left((Convert(varchar," & eCampo & ")) + '" & eRelleno & "'," & eCorte & ")"
        Else
            'lPad MySQL 
            Return "rpad(" & eCampo & "," & eCorte & ",'" & eRelleno & "')"
        End If
    End Function

    Public Shared Function sqlDiferenciaDias(ByVal ePrimerCampo As String, ByVal eSegundoCampo As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            '    'SQL Server
            Return "datediff(day," & eSegundoCampo & "," & ePrimerCampo & ")"
        Else
            'MySQL
            Return "to_days(" & ePrimerCampo & ") - to_days(" & eSegundoCampo & ")"
        End If
    End Function

    Public Shared Function sqlTruncar(ByVal eCampo As String, ByVal eCantidadDeDecimales As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return "round(" & eCampo & "," & eCantidadDeDecimales & ",1)"
        Else
            'MySQL
            Return "truncate(" & eCampo & "," & eCantidadDeDecimales & ")"
        End If
    End Function

    Public Shared Function sqlConvertirFecha(ByVal eCampo As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return "CONVERT(DATE,'" & eCampo & "')"
        Else
            'MySQL
            Return "CONVERT('" & eCampo & "',DATE)"
        End If
    End Function

    Public Shared Function sqlMD5(ByVal eCampo As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return "LOWER(CONVERT(NVARCHAR(32),HashBytes('MD5', " & eCampo & "),2))"
        Else
            'MySQL
            Return "MD5(" & eCampo & ")"
        End If
    End Function

    Public Shared Function sqlSumarFecha(ByVal eFecha As String, ByVal eSuma As String, ByVal eTipoFechaASumar As enumFecha) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            If eTipoFechaASumar = enumFecha.DIA Then
                Return "DATEADD(d," & eSuma & "," & eFecha & ")"
            ElseIf eTipoFechaASumar = enumFecha.MES Then
                Return "DATEADD(m," & eSuma & "," & eFecha & ")"
            Else
                Return "DATEADD(yy," & eSuma & "," & eFecha & ")"
            End If
        Else
            If eTipoFechaASumar = enumFecha.DIA Then
                Return "DATE_ADD(" & eFecha & ",interval " & eSuma & " day)"
            ElseIf eTipoFechaASumar = enumFecha.MES Then
                Return "DATE_ADD(" & eFecha & ",interval " & eSuma & " month)"
            Else
                Return "DATE_ADD(" & eFecha & ",interval " & eSuma & " year)"
            End If
        End If

    End Function

    Public Shared Function sqlWhereMesCompleto(ByVal eColumna As String, ByVal eMes As Integer, ByVal eAño As Integer) As String

        Return "mouth(" & eColumna & ")='" & eMes & " and year(" & eColumna & ")='" & eAño & "'"
    End Function

    Public Shared Function obtenerEspaciosHTML(ByVal eCantidadDeEspacios As Integer) As String
        Dim iEspacio As String = "&nbsp"
        Dim iHTML As String = ""
        Dim i As Integer

        For i = 1 To eCantidadDeEspacios
            iHTML += iEspacio
        Next i

        Return iHTML
    End Function

    Public Shared Function calcularDigitoVerificador(ByVal eDocumento As String) As Integer
        Dim iDigitoVerificador As Integer

        Try

            eDocumento = Right("0000000000" & eDocumento, 10)
            iDigitoVerificador = CLng(Mid(eDocumento, 1, 1)) * 2 + CLng(Mid(eDocumento, 2, 1)) + CLng(Mid(eDocumento, 3, 1)) * 2 + CLng(Mid(eDocumento, 4, 1)) + CLng(Mid(eDocumento, 5, 1)) * 2 + CLng(Mid(eDocumento, 6, 1)) + CLng(Mid(eDocumento, 7, 1)) * 2 + CLng(Mid(eDocumento, 8, 1)) + CLng(Mid(eDocumento, 9, 1)) * 2 + CLng(Mid(eDocumento, 10, 1))
            iDigitoVerificador = 10 - Right(iDigitoVerificador, 1)

            Return iDigitoVerificador

        Catch Exception As Exception
            Throw New DigitoVerificadorNoCalculadoException(Exception)
        End Try
    End Function

    Public Shared Function validarLongitudArray(ByVal eArray As String()) As Integer
        Dim iCantidadLineas As Integer = 0

        Try

            For Each iValor As String In eArray
                If Trim(FuncionComun.vacioSiEsNothing(iValor)) <> "" Then
                    iCantidadLineas = iCantidadLineas + 1
                End If
            Next

            Return iCantidadLineas

        Catch Exception As Exception
            Throw New DigitoVerificadorNoCalculadoException(Exception)
        End Try
    End Function

    Public Shared Function validarCBU(ByVal eCBU As String) As Boolean
        Dim iBloque1, iBloque2 As String
        Dim iSuma, iDigitoVerificador As Integer
        Try
            If Len(eCBU) = 22 Then
                iBloque1 = Mid(eCBU, 1, 8)
                iSuma = CLng(Mid(iBloque1, 1, 1)) * 7 + CLng(Mid(iBloque1, 2, 1)) * 1 + CLng(Mid(iBloque1, 3, 1)) * 3 + CLng(Mid(iBloque1, 4, 1)) * 9 + CLng(Mid(iBloque1, 5, 1)) * 7 + CLng(Mid(iBloque1, 6, 1)) * 1 + CLng(Mid(iBloque1, 7, 1)) * 3
                iDigitoVerificador = 10 - Right(iSuma, 1)
                If Right(iDigitoVerificador, 1) <> CLng(Mid(iBloque1, 8, 1)) Then
                    Return False
                End If
                iBloque2 = Mid(eCBU, 9, 14)
                iSuma = CLng(Mid(iBloque2, 1, 1)) * 3 + CLng(Mid(iBloque2, 2, 1)) * 9 + CLng(Mid(iBloque2, 3, 1)) * 7 + CLng(Mid(iBloque2, 4, 1)) * 1 + CLng(Mid(iBloque2, 5, 1)) * 3 + CLng(Mid(iBloque2, 6, 1)) * 9 + CLng(Mid(iBloque2, 7, 1)) * 7 + CLng(Mid(iBloque2, 8, 1)) * 1 + CLng(Mid(iBloque2, 9, 1)) * 3 + CLng(Mid(iBloque2, 10, 1)) * 9 + CLng(Mid(iBloque2, 11, 1)) * 7 + CLng(Mid(iBloque2, 12, 1)) * 1 + CLng(Mid(iBloque2, 13, 1)) * 3
                iDigitoVerificador = 10 - Right(iSuma, 1)
                If Right(iDigitoVerificador, 1) <> CLng(Mid(iBloque2, 14, 1)) Then
                    Return False
                End If
                Return True
            Else
                Return False
            End If
        Catch Exception As Exception
            Return False
        End Try
    End Function

    Public Shared Sub obtenerCBUDividido(ByVal eCBU As String, ByRef eCodigoBanco As String, ByRef eNumeroSucursal As String, ByRef eNumeroCuenta As String)

        Try

            If Len(eCBU) = 22 Then
                eCodigoBanco = eCBU.Substring(0, 3)
                eNumeroSucursal = eCBU.Substring(3, 4)
                eNumeroCuenta = eCBU.Substring(8, 13)
            End If

        Catch exception As Exception
        End Try
    End Sub

    Public Shared Function calcularDigitoVerificadorFactura(ByVal eCodigoBarra As String) As Integer
        Dim iDigitoVerificador As Integer
        Dim iTotalPares As Integer
        Dim iTotalImpares As Integer
        Dim x As Double
        Dim i As Integer
        Try

            iTotalImpares = 0
            For i = 1 To Len(eCodigoBarra) Step 2
                iTotalImpares = iTotalImpares + CDbl(Mid(eCodigoBarra, i, 1))
            Next i
            iTotalImpares = iTotalImpares * 3


            iTotalPares = 0
            For i = 2 To Len(eCodigoBarra) Step 2
                iTotalPares = iTotalPares + CDbl(Mid(eCodigoBarra, i, 1))
            Next i

            x = iTotalImpares + iTotalPares
            iDigitoVerificador = 0

            Do While Int(x / 10) * 10 <> x
                x = x + 1
                iDigitoVerificador = iDigitoVerificador + 1
            Loop

            Return iDigitoVerificador

        Catch Exception As Exception
            Throw New DigitoVerificadorNoCalculadoException(Exception)
        End Try
    End Function

    Public Shared Function redondear(ByVal eNumero As Double, eAccesoDatos As accesoDatos) As Double
        Dim iTipoRedondeo As enumTipoRedondeo
        Dim iDecimal As Double
        Dim iEntero As Integer
        'Optional ByVal eAccesoDatos As accesoDatos = Nothing
        iTipoRedondeo = obtenerTipoRedondeo(eAccesoDatos)

        If eNumero > 0 Then
            Select Case iTipoRedondeo
                Case enumTipoRedondeo.MONTOINFERIOR
                    Return Decimal.Truncate(eNumero)
                Case enumTipoRedondeo.MONTOSUPERIOR
                    iDecimal = eNumero - Decimal.Truncate(eNumero)
                    If iDecimal > 0.001 Then
                        Return Decimal.Truncate(eNumero) + 1
                    Else
                        Return Decimal.Truncate(eNumero)
                    End If
                Case enumTipoRedondeo.MONTOMEDIO
                    iDecimal = eNumero - Decimal.Truncate(eNumero)
                    If iDecimal < 0.5 Then
                        iDecimal = 0
                    Else
                        iDecimal = 1
                    End If
                    Return Decimal.Truncate(eNumero) + iDecimal
                Case enumTipoRedondeo.SINREDONDEO
                    Return eNumero
               'REDONDEO CREDIBASE
                Case enumTipoRedondeo.MONTOPERZONALIZADO
                    Dim iNumeroStringCompleto As String
                    Dim iNumeroStringUltimoNumero As String

                    'OBTENGO LA PARTE ENTERA
                    iEntero = Decimal.ToInt32(eNumero)
                    'CONVIERTO EN STRING PARA OBTENER EL ULTIMO NUMERO DE LA PARTE ENTERA
                    iNumeroStringCompleto = iEntero.ToString
                    'OBTENGO EL ULTIMO NUMERO DE LA PARTE ENTERA
                    iNumeroStringUltimoNumero = Right(iNumeroStringCompleto, 1)
                    Dim iDiferencia As Integer
                    If (Convert.ToInt32(iNumeroStringUltimoNumero) <= 5) Then
                        iDiferencia = 5 - Convert.ToInt32(iNumeroStringUltimoNumero)
                        Return iEntero + iDiferencia
                    Else
                        iDiferencia = 10 - Convert.ToInt32(iNumeroStringUltimoNumero)
                        Return iEntero + iDiferencia
                    End If
                Case enumTipoRedondeo.MONTOPROPORCIONALALADECENA
                    Dim iNumeroStringCompleto As String
                    Dim iNumeroDecimalCompleto As Double
                    Dim iNumeroStringUltimoNumero As String
                    'OBTENGO LA PARTE ENTERA
                    iEntero = Decimal.ToInt32(eNumero)
                    iNumeroDecimalCompleto = eNumero - iEntero
                    'CONVIERTO EN STRING PARA OBTENER EL ULTIMO NUMERO DE LA PARTE ENTERA
                    iNumeroStringCompleto = iEntero.ToString
                    'OBTENGO EL ULTIMO NUMERO DE LA PARTE ENTERA
                    iNumeroStringUltimoNumero = Right(iNumeroStringCompleto, 1)
                    Dim iDiferencia As Double
                    Dim iNumeroFinal As Double
                    iNumeroFinal = Convert.ToDouble(iNumeroStringUltimoNumero) + iNumeroDecimalCompleto
                    If (iNumeroFinal <= 5.0) Then
                        iDiferencia = 0 - iNumeroFinal
                        Return eNumero + iDiferencia
                    Else
                        iDiferencia = 10 - iNumeroFinal
                        Return eNumero + iDiferencia
                    End If
            End Select
        Else
            Return 0
        End If

    End Function

    Public Shared Function redondearMontoMedio(ByVal eNumero As Double) As Double
        Dim iDecimal As Double

        If eNumero > 0 Then
            iDecimal = eNumero - Decimal.Truncate(eNumero)
            If iDecimal < 0.5 Then
                iDecimal = 0
            Else
                iDecimal = 1
            End If
            Return Decimal.Truncate(eNumero) + iDecimal
        Else
            Return eNumero
        End If
    End Function

    Public Shared Function redondearSuperior(ByVal eNumero As Double) As Double
        Dim iDecimal As Double
        If eNumero > 0 Then
            iDecimal = eNumero - Decimal.Truncate(eNumero)
            If iDecimal > 0.001 Then
                Return Decimal.Truncate(eNumero) + 1
            Else
                Return Decimal.Truncate(eNumero)
            End If
        Else
            Return 0
        End If

    End Function
    Public Shared Function redondearDecimalSuperior(ByVal eNumero As Double) As Double
        Dim iDecimal As Double
        If eNumero > 0 Then
            eNumero = eNumero * 100
            iDecimal = eNumero - Decimal.Truncate(eNumero)
            If iDecimal > 0.001 Then
                Return (Decimal.Truncate(eNumero) + 1) / 100
            Else
                Return (Decimal.Truncate(eNumero) / 100)
            End If
        Else
            Return 0
        End If
    End Function



    Public Shared Function calcularCuit(ByVal eDocumento As Long, ByVal eSexo As Sexo) As Long()
        Dim iCuil As Long()
        Dim iStringDocumento As String
        Dim iVectorDocumento As Integer()
        Dim iAcumulado As Long
        Dim i As Integer

        Try

            ReDim iCuil(2)

            iStringDocumento = CStr(Right("00000000" & eDocumento, 8))

            ReDim iVectorDocumento(7)

            For i = 0 To 7
                iVectorDocumento(i) = iStringDocumento.Substring(i, 1)
            Next i

            Select Case ConfigurationManager.AppSettings("Region")

                Case "URUGUAY"
                    iCuil = calcularDigitoVerificadorCuitUY(eDocumento)
                    iCuil(1) = eDocumento

                Case "PARAGUAY"
                    iCuil = iCuil

                Case "COLOMBIA"
                    iCuil = iCuil

                Case Else ' INCLUYE ARGENTINA
                    iAcumulado = iVectorDocumento(0) * 3
                    iAcumulado += iVectorDocumento(1) * 2
                    iAcumulado += iVectorDocumento(2) * 7
                    iAcumulado += iVectorDocumento(3) * 6
                    iAcumulado += iVectorDocumento(4) * 5
                    iAcumulado += iVectorDocumento(5) * 4
                    iAcumulado += iVectorDocumento(6) * 3
                    iAcumulado += iVectorDocumento(7) * 2

                    iCuil = calcularDigitoVerificadorCuit(iAcumulado, eSexo)
                    iCuil(1) = eDocumento
            End Select

            Return iCuil

        Catch exception As Exception
            Return Nothing
        End Try

    End Function

    Private Shared Function calcularDigitoVerificadorCuit(ByVal eAcumulado As Long, ByVal eSexo As Sexo, Optional ByVal eDigitoVerificador As Integer = Nothing) As Long()
        Dim iAcumuladoFinal As Long
        Dim iDivision As Integer
        Dim iCuil() As Long

        Try
            ReDim iCuil(2)

            If eDigitoVerificador <> Nothing Then
                iCuil(0) = 23
                iAcumuladoFinal = eAcumulado + 22
            ElseIf eSexo.id = Sexo.MASCULINO Then
                iCuil(0) = 20
                iAcumuladoFinal = eAcumulado + 10
            Else
                iCuil(0) = 27
                iAcumuladoFinal = eAcumulado + 38
            End If

            iDivision = Decimal.Truncate(iAcumuladoFinal / 11)
            iDivision = iDivision * 11
            iAcumuladoFinal = iAcumuladoFinal - iDivision
            iAcumuladoFinal = 11 - iAcumuladoFinal

            Select Case iAcumuladoFinal
                Case 10
                    iCuil = calcularDigitoVerificadorCuit(eAcumulado, eSexo, iAcumuladoFinal)
                Case 11
                    iAcumuladoFinal = Nothing
                Case Else
                    iCuil(2) = iAcumuladoFinal
            End Select

            Return iCuil

        Catch exception As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function calcularDigitoVerificadorCuitUY(ByVal eDocumento As String) As Long()
        Dim iDigitos As Integer()
        Dim iSuma As Integer = 0
        Dim iModulo As Integer = 0
        Dim iDigitoVerificador As Integer = -1
        Dim iCuil() As Long
        Dim iTotal As Integer
        Dim iAlgoritmo As Integer() = {2, 9, 8, 7, 6, 3, 4}
        Dim iPosAlgoritmo As Integer

        Try

            ReDim iCuil(2)

            Try
                eDocumento = CStr(Right("0000000" & eDocumento, 7))

                ReDim iDigitos(eDocumento.Length())
                iTotal = iDigitos.Length - 1

                'Si un documento tiene 6 digitos, debe empezar con la segunda posición del algoritmo (9)
                iPosAlgoritmo = eDocumento.Length - iAlgoritmo.Length

                For i As Integer = 0 To iTotal - 1
                    iDigitos(i + 1) = CInt("" & GetChar(eDocumento, (i + 1)))
                    iSuma = iSuma + (iDigitos(i + 1) * iAlgoritmo(i + iPosAlgoritmo))
                Next i

                iModulo = iSuma Mod 10

                If iModulo <> 0 Then
                    iDigitoVerificador = 10 - iModulo
                Else
                    iDigitoVerificador = 0
                End If

            Catch e As Exception
                iDigitoVerificador = -1
            End Try

            iCuil(1) = 0
            iCuil(2) = iDigitoVerificador

            Return iCuil

        Catch Exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Sub obtenerComun(ByVal eFechaAUtilizar As Date, Optional ByVal eLogin As Boolean = False, Optional eAccesoDatos As accesoDatos = Nothing)
        Dim iFecha As Date
        Try
            iFecha = CDate("15/01/2024")
            If iFecha <= eFechaAUtilizar Then
                If eLogin Then marcaComun(eAccesoDatos)
                Throw New ComunException("Consulte a soporte tecnico")
            End If
        Catch exception As Exception
            FuncionComun.loguearErrores("Error Consulte a Soporte: " & eFechaAUtilizar.ToString & " " & exception.Message)
            Throw New ComunException("ERROR:Consulte a soporte tecnico")
        End Try
    End Sub

    Public Shared Function fechaJuliana(ByVal eFecha As Date) As Integer
        Return (DateDiff(DateInterval.Day, CDate("01/01/" & Format(eFecha, "yyyy")), eFecha) + 1)
    End Function

    Public Shared Function digitoVerificadorPagoFacil(ByVal eString As String) As Integer
        Dim iAcumulado, i, iMultiplicador As Integer
        Dim iString As Integer()

        ReDim iString(eString.Length)

        For i = 0 To eString.Length - 1
            iString(i) = CInt(eString.Substring(i, 1))
        Next i
        iMultiplicador = 3
        iAcumulado = iString(0)
        For i = 1 To eString.Length - 1
            iAcumulado += iString(i) * iMultiplicador
            If iMultiplicador = 9 Then
                iMultiplicador = 3
            Else
                iMultiplicador += 2
            End If
        Next
        Return Mid(CStr(iAcumulado \ 2), CStr(iAcumulado \ 2).Length, 1)
    End Function

    Public Shared Function digitoVerificadorCortoCorto(ByVal eCampo As String) As Integer
        Dim iDigito, i, iParte, iSuma As Integer
        Dim iMulti As String = "971397139713"

        i = 0
        iSuma = 0
        iParte = 0

        While i < 12
            iParte = CInt(eCampo.Substring(i, 1)) * CInt(iMulti.Substring(i, 1))
            iSuma = iSuma + Right(Convert.ToString(1000000000 + iParte), 1)
            i = i + 1
        End While

        If CInt(Right(iSuma, 1)) = 0 Then
            iDigito = 0
        Else
            iDigito = 10 - CInt(Right(iSuma, 1))
        End If

        Return iDigito

    End Function

    Public Shared Function digitoVerificadorSiro(ByVal eString As String) As Integer
        Dim iAcumulado, i, iMultiplicador As Integer
        Dim iString As Integer()

        ReDim iString(eString.Length)

        For i = 0 To eString.Length - 1
            iString(i) = CInt(eString.Substring(i, 1))
        Next i

        iMultiplicador = 3
        iAcumulado = iString(0)

        For i = 1 To eString.Length - 1
            iAcumulado += iString(i) * iMultiplicador
            If iMultiplicador = 9 Then
                iMultiplicador = 3
            Else
                iMultiplicador += 2
            End If
        Next

        iAcumulado = iAcumulado \ 2

        Return iAcumulado Mod 10
    End Function

    Public Shared Function formatoCodigoBarra(ByVal eString As String) As String
        Dim iPares As Integer()
        Dim iString As String
        Dim i, j As Integer
        Dim iCodigoBarra As String
        ReDim iPares((eString.Length / 2) - 1)

        For i = 1 To Len(eString)
            If Asc(Mid(eString, i, 1)) > 47 And Asc(Mid(eString, i, 1)) < 58 Then iString = iString & Mid(eString, i, 1)
        Next

        For i = 1 To Len(iString) Step 2
            iPares(j) = CInt(Mid(iString, i, 2))
            j += 1
        Next
        For i = 0 To iPares.Length - 1
            Select Case iPares(i)
                Case 20
                    iCodigoBarra = iCodigoBarra & Chr(241)
                Case 33
                    iCodigoBarra = iCodigoBarra & Chr(246)
                Case Else
                    iCodigoBarra = iCodigoBarra & Chr(iPares(i) + 140)
            End Select
        Next

        Return "<" & iCodigoBarra & ">"
    End Function

    Public Shared Function soloLetras(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String = ""
        If eString <> Nothing Then
            For i = 0 To eString.Length - 1
                If System.Char.IsLetter(eString.Chars(i)) OrElse System.Char.IsWhiteSpace(eString.Chars(i)) Then
                    iString = iString & eString.Chars(i)
                End If
            Next
        End If
        Return iString
    End Function

    Public Shared Function soloLetrasComas(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String = ""
        If eString <> Nothing Then
            For i = 0 To eString.Length - 1
                If System.Char.IsLetter(eString.Chars(i)) OrElse System.Char.IsWhiteSpace(eString.Chars(i)) OrElse eString.Chars(i) = CChar(",") OrElse eString.Chars(i) = CChar("#") Then
                    iString = iString & eString.Chars(i)
                End If
            Next
        End If
        Return iString
    End Function

    Public Shared Function soloLetrasNumeroYPunto(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String = ""
        If eString <> Nothing Then
            For i = 0 To eString.Length - 1
                If System.Char.IsNumber(eString.Chars(i)) OrElse System.Char.IsLetter(eString.Chars(i)) OrElse System.Char.IsWhiteSpace(eString.Chars(i)) OrElse eString.Chars(i) = CChar(".") Then
                    iString = iString & eString.Chars(i)
                End If
            Next
        End If
        Return iString
    End Function

    Public Shared Function filtroCaracteres(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String = ""
        If eString <> Nothing Then
            For i = 0 To eString.Length - 1
                If System.Char.IsNumber(eString.Chars(i)) OrElse System.Char.IsLetter(eString.Chars(i)) OrElse System.Char.IsWhiteSpace(eString.Chars(i)) OrElse eString.Chars(i) = CChar(".") OrElse eString.Chars(i) = CChar("#") OrElse eString.Chars(i) = CChar(",") OrElse eString.Chars(i) = CChar("º") Then
                    iString = iString & eString.Chars(i)
                End If
            Next
        End If
        Return iString
    End Function

    Public Shared Function soloNumero(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String
        If eString <> Nothing Then
            For i = 0 To eString.Length - 1
                If System.Char.IsNumber(eString.Chars(i)) Then
                    iString = iString & eString.Chars(i)
                End If
            Next
        End If
        Return iString
    End Function

    Public Shared Function soloSignos(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String
        If eString <> Nothing Then
            For i = 0 To eString.Length - 1
                If eString.Chars(i) = ">" OrElse eString.Chars(i) = "<" OrElse eString.Chars(i) = "=" Then
                    iString = iString & eString.Chars(i)
                End If
            Next
        End If
        Return iString
    End Function

    Public Shared Function soloNumeroYLetra(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String
        If eString <> Nothing Then
            For i = 0 To eString.Length - 1
                If System.Char.IsNumber(eString.Chars(i)) OrElse System.Char.IsLetter(eString.Chars(i)) OrElse System.Char.IsWhiteSpace(eString.Chars(i)) Then
                    iString = iString & eString.Chars(i)
                End If
            Next
        End If
        Return iString
    End Function

    Public Shared Function soloCaracteresValidos(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String
        If eString <> Nothing Then
            For i = 0 To eString.Length - 1
                If System.Char.IsNumber(eString.Chars(i)) OrElse System.Char.IsLetter(eString.Chars(i)) OrElse System.Char.IsWhiteSpace(eString.Chars(i)) OrElse eString.Chars(i) = vbTab OrElse eString.Chars(i) = "." OrElse eString.Chars(i) = "," OrElse eString.Chars(i) = "´" Then
                    iString = iString & eString.Chars(i)
                End If
            Next
        End If
        Return iString
    End Function

    Public Shared Function calcularDigitoVerificadorTarjeta(ByVal eNumero As String, ByVal eCantidadDigitos As Integer) As Integer
        'CALCULO EN BASE 10
        Dim iDigitoVerificador As Integer
        Dim iNumero As String
        Dim iDigito1 As Integer
        Dim iDigito2 As Integer
        Dim i As Integer
        Try

            eNumero = Right(StrDup(eCantidadDigitos, "0") & eNumero, eCantidadDigitos)
            For i = 1 To eCantidadDigitos
                If i Mod 2 <> 0 Then
                    iNumero = iNumero & CLng(Mid(eNumero, i, 1)) * 2
                Else
                    iNumero = iNumero & CLng(Mid(eNumero, i, 1))
                End If
            Next
            For i = 1 To iNumero.Length
                iDigito1 += CLng(Mid(iNumero, i, 1))
            Next
            If iDigito1 Mod 10 = 0 Then
                iDigitoVerificador = 0
            Else
                iDigito2 = iDigito1
                While iDigito2 Mod 10 <> 0
                    iDigito2 += 1
                End While
                iDigitoVerificador = iDigito2 - iDigito1
            End If

            Return iDigitoVerificador

        Catch Exception As Exception
            Throw New DigitoVerificadorNoCalculadoException(Exception)
        End Try
    End Function

    Public Shared Function validarTelefonoRepeticionNumero(ByVal eAccesoDatos As accesoDatos, ByVal eCaracteristica As String, ByVal eNumero As String) As Boolean
        Dim iTelefonoCelularValido As Boolean = True
        Dim iNumeroCompleto As String
        Dim x As Integer

        Try
            iNumeroCompleto = eCaracteristica + eNumero
            For x = 0 To (9)
                iNumeroCompleto = iNumeroCompleto.Replace(x.ToString, "")
                If iNumeroCompleto.Length < 2 Then
                    iNumeroCompleto = Nothing
                    iTelefonoCelularValido = False
                    Exit For
                Else
                    iNumeroCompleto = eCaracteristica + eNumero
                End If
            Next
            Return iTelefonoCelularValido


        Catch exception As Exception
            Return False
        Finally
            iNumeroCompleto = Nothing
        End Try
    End Function

    Public Shared Function validarTelefonoRepeticionNumero(ByVal eCaracteristica As String, ByVal eNumero As String) As Boolean
        Dim iTelefonoCelularValido As Boolean
        Dim iAccesoDatos As accesoDatos
        Try


            iAccesoDatos = New accesoDatos
            iTelefonoCelularValido = validarTelefonoRepeticionNumero(iAccesoDatos, eCaracteristica, eNumero)
            Return iTelefonoCelularValido

        Catch exception As Exception
            Return False
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Shared Function corregirTelefono(ByVal eTelefono As String) As String
        Dim iTelefonoCorrecto, iElemento As String
        Dim i As Integer

        Try
            If eTelefono = Nothing Then eTelefono = ""
            iTelefonoCorrecto = ""
            eTelefono = Replace(eTelefono, " ", "")

            'Buscamos las aclaraciones que tiene al final
            For i = 1 To Len(eTelefono)
                iElemento = Mid(eTelefono, i, 1)
                If IsNumeric(iElemento) Then
                    iTelefonoCorrecto = iTelefonoCorrecto & iElemento
                End If
            Next

            Return iTelefonoCorrecto

        Catch exception As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function quitarCerosAIzquierda(ByVal eDato As String) As String
        Try

            While Left(eDato, 1) = "0"
                eDato = Right(eDato, Len(eDato) - 1)
            End While

            Return eDato

        Catch exception As Exception
            Return eDato
        End Try
    End Function

    Public Shared Function diferenciaHoras(ByVal ePrimerCampo As String, ByVal eSegundoCampo As String) As String
        Return "timediff(" & ePrimerCampo & "," & eSegundoCampo & ")"
    End Function
    Public Shared Function nuloSiEsNothingFechaHora(ByVal eObject As Object) As String
        If IsNothing(eObject) OrElse eObject = Nothing Then
            Return "null"
        ElseIf TypeOf (eObject) Is Date Then
            Return "'" & Format(eObject, "yyyy/MM/dd HH:mm:ss") & "'"
        Else
            Return "'" & eObject & "'"
        End If
    End Function

    Public Shared Function nuloSiEsNothingHora(ByVal eObject As Object) As String
        If IsNothing(eObject) OrElse eObject = Nothing Then
            Return "null"
        Else
            If TypeOf (eObject) Is Double Then
                eObject = "'" & eObject & "'"
                Return Replace(eObject, ",", ".")
            ElseIf TypeOf (eObject) Is Date Then
                Return "'" & Format(eObject, "HH:mm:ss") & "'"
            Else
                Return "'" & eObject & "'"
            End If
        End If
    End Function

    Public Shared Sub loguearInjection(ByVal eResultado As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "INJECTION" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eResultado)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearPaginas(ByVal ePagina As String, ByVal eUsuario As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "Paginas" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearErroresServicios(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresServicios" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearTokenServicios(ByVal eToken As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "TokenServicios" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eToken)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearErroresEnvioEstudio(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresEnvioEstudio" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearServiciosBST(ByVal eVariables As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ServiciosBST" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eVariables)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearServiciosBank(ByVal eVariables As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ServiciosBank" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eVariables)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearRiesgonetxml(ByVal ePagina As String, ByVal eUsuario As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "RiesgonetXML" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearSiisaxml(ByVal ePagina As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "SiisaXML" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearSiisaApiXml(ByVal ePagina As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "SiisaApiXML" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearEquifax(ByVal ePagina As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "Equifax" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearEntreConsultas(ByVal eLog As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogEntreConsultas" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eLog)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearFTP(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresFTP" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearSMS(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "SMS" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearPyP(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresPyP" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearGrupar(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresGrupar" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearMigracionDocumentacion(ByVal eArchivo As String, ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "MigracionDocumentacion" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - Archivo: " & eArchivo & " - Error: " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearNosis(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "logNOSIS" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearServicioMutual(ByVal eTipo As String, ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ServicioMutual" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(eTipo & " - " & Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearMotor(eUsuario As String, eDatoLoguear As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogueoMotor" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - " & eDatoLoguear)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearMotorBesmart(ByVal eLog As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "MotorBesmart" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eLog)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub


    Public Shared Sub loguearServiciosEnvioLiquidacion(ByVal eLog As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ServiciosLiquidacion" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eLog)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearVeraz(ByVal ePagina As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "Veraz" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Function obtenerNombre(ByVal eNombreCompleto As String) As String
        Try
            eNombreCompleto = Trim(eNombreCompleto)

            Return Right(eNombreCompleto, Len(eNombreCompleto) - InStr(eNombreCompleto, ","))

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function obtenerApellido(ByVal eNombreCompleto As String) As String
        Try
            eNombreCompleto = Trim(eNombreCompleto)
            Return Left(eNombreCompleto, InStr(eNombreCompleto, ",") - 1)

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function obtenerMesPagoFacil(ByVal eFecha As Date) As String
        Select Case Format(eFecha, "MM")
            Case "01"
                Return "A"
            Case "02"
                Return "B"
            Case "03"
                Return "C"
            Case "04"
                Return "D"
            Case "05"
                Return "E"
            Case "06"
                Return "F"
            Case "07"
                Return "G"
            Case "08"
                Return "H"
            Case "09"
                Return "I"
            Case "10"
                Return "J"
            Case "11"
                Return "K"
            Case "12"
                Return "L"
        End Select
    End Function

    Public Shared Function calcularDigitoVerificadorIdClienteCompleto(ByVal eString As String) As String
        Dim iResultado As String

        iResultado = calcularDigitoVerificadorIdCliente(eString)
        iResultado &= calcularDigitoVerificadorIdCliente(eString & iResultado)

        Return iResultado

    End Function

    Public Shared Function calcularDigitoVerificadorIdCliente(ByVal eString As String) As String
        Dim iString As Integer()
        Dim iMultiplicador As Integer()
        Dim iAcumulado, i As Integer
        Dim iResultado As Double

        ReDim iMultiplicador(20)
        ReDim iString(eString.Length)

        'lleno el vector por el cual se van a multiplicar cada una de las posiciones del string
        iMultiplicador(0) = 1
        iMultiplicador(1) = 3
        iMultiplicador(2) = 5
        iMultiplicador(3) = 7
        iMultiplicador(4) = 9
        iMultiplicador(5) = 3
        iMultiplicador(6) = 5
        iMultiplicador(7) = 7
        iMultiplicador(8) = 9
        iMultiplicador(9) = 3
        iMultiplicador(10) = 5
        iMultiplicador(11) = 7
        iMultiplicador(12) = 9
        iMultiplicador(13) = 3
        iMultiplicador(14) = 5
        iMultiplicador(15) = 7
        iMultiplicador(16) = 9
        iMultiplicador(17) = 3
        iMultiplicador(18) = 5
        iMultiplicador(19) = 7
        iMultiplicador(20) = 9

        'meto el string en un vector
        For i = 0 To eString.Length - 1
            iString(i) = CInt(eString.Substring(i, 1))
        Next i

        For i = 0 To eString.Length - 1
            iAcumulado += iString(i) * iMultiplicador(i)
        Next

        iResultado = iAcumulado / 2

        Return CStr(Int(iResultado) Mod 10)

    End Function

    Public Shared Function redondearAMultiplos(ByVal eNumero As Double, ByVal eValor As Integer) As Double
        Dim iEntera As Double
        Dim iDecimal As Double
        iEntera = eNumero / eValor
        iDecimal = iEntera - Decimal.Truncate(iEntera)
        iEntera = iEntera - iDecimal
        If iDecimal > 0 And iDecimal <= 0.5 Then
            iDecimal = 0
        ElseIf iDecimal > 0.5 And iDecimal <= 1 Then
            iDecimal = 1
        End If
        Return (iEntera + iDecimal) * eValor

    End Function

    Public Shared Sub loguearCodigo(ByVal eUsuario As String, ByVal eCodigoBarra As String)
        Dim iArchivoPaginas As StreamWriter
        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "EscaneoCodigoBarra" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - CODIGO: " & eCodigoBarra)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearLogin(ByVal eUsuario As String, ByVal eExito As Boolean, ByVal eBloqueado As Boolean, ByVal eContraseniaIngresada As String)
        Dim iArchivoPaginas As StreamWriter
        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LoginUsuario" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & IIf(eExito, " Ingreso correctamente", IIf(eBloqueado, " Usuario Bloqueado", " Contraseña Erronea: " & eContraseniaIngresada)))

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Function validarNombre(ByVal eNombre As String) As Boolean
        Dim iPosicionComa As String

        Try

            iPosicionComa = InStr(eNombre, ",")
            If iPosicionComa > 0 Then
                'Valido que no tenga espacios a izquierda y a derecha de la coma y  que haya un caracter a izq o derecha
                Return eNombre.Substring(iPosicionComa - 2, 1) <> " " And eNombre.Substring(iPosicionComa, 1) <> " "
            Else
                Return False
            End If

        Catch exception As Exception
            Return False
        End Try
    End Function

    Public Shared Sub obtenerNombreDividido(ByVal eNombreCompleto As String, ByRef eNombreDividido As String, ByRef eApellidoDividido As String, Optional eForzarComa As Boolean = False)
        Dim iCantidadEspacios As Integer
        Dim i As Integer

        Try
            eNombreCompleto = Trim(eNombreCompleto)
            eNombreCompleto = eNombreCompleto.Replace("  ", " ")

            For i = 0 To Len(eNombreCompleto) - 1
                If eNombreCompleto.Substring(i, 1) = " " Then iCantidadEspacios += 1
            Next i

            If iCantidadEspacios <= 2 OrElse eForzarComa Then
                eApellidoDividido = Left(eNombreCompleto, InStr(eNombreCompleto, " ") - 1)
                eNombreDividido = Right(eNombreCompleto, Len(eNombreCompleto) - InStr(eNombreCompleto, " "))
            Else
                eApellidoDividido = Nothing
                eNombreDividido = Nothing
            End If

        Catch exception As Exception
            eNombreDividido = Nothing
            eApellidoDividido = Nothing
        End Try
    End Sub

    Public Shared Sub obtenerNombreDivididoPucAfip(ByVal eNombreCompleto As String, ByRef eNombreDividido As String, ByRef eApellidoDividido As String)
        Dim iCantidadEspacios As Integer
        Dim iNombreFinal, iNombreTemporal As String
        Dim iNombres As String()
        Dim i As Integer
        Try

            eNombreCompleto = Trim(eNombreCompleto)

            For i = 0 To Len(eNombreCompleto) - 1
                If eNombreCompleto.Substring(i, 1) = " " Then iCantidadEspacios += 1
            Next i

            If iCantidadEspacios > 0 Then
                eApellidoDividido = Left(eNombreCompleto, InStr(eNombreCompleto, " ") - 1)
                eNombreDividido = Right(eNombreCompleto, Len(eNombreCompleto) - InStr(eNombreCompleto, " "))
                'If eNombreDividido.Split(" ").Length > 0 Then
                '    iNombres = eNombreDividido.Split(" ")
                '    For i = 0 To iNombres.Length - 1
                '        iNombreTemporal = iNombres(i)
                '        If validarBaseNombre(iConexion, iNombreTemporal) Then
                '            iNombreFinal &= iNombreTemporal & " "
                '        Else
                '            eApellidoDividido &= " " & iNombreTemporal
                '        End If
                '    Next
                '    eNombreDividido = Trim(iNombreFinal)

                'End If
            ElseIf Len(eNombreCompleto) > 0 Then
                eApellidoDividido = eNombreCompleto
            Else
                eApellidoDividido = Nothing
                eNombreDividido = Nothing
            End If

        Catch exception As Exception
            eNombreDividido = Nothing
            eApellidoDividido = Nothing
        End Try
    End Sub

    Public Shared Sub formatearNombreConComa(ByVal eNombreCompleto As String, ByRef eNombreFormateado As String)
        Dim iNombreFinal, iApellidoFinal As String

        obtenerNombreDivididoPucAfip(eNombreCompleto, iNombreFinal, iApellidoFinal)
        If iNombreFinal = Nothing Then
            eNombreFormateado = iApellidoFinal
        Else
            eNombreFormateado = iApellidoFinal & "," & iNombreFinal
        End If

    End Sub

    Public Shared Function validarBaseNombre(eAccesoDatos As accesoDatos, eNombre As String) As Boolean
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iGeneradorSql.agregarTabla("nombres")
            iGeneradorSql.agregarColumna("Nombre")
            iGeneradorSql.agregarCondicionWhere("Nombre='" & eNombre & "'")
            iDataReader = eAccesoDatos.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Return True
            Else
                Return False
            End If

        Catch exception As Exception
            Return False
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Shared Function validarEmail(ByVal eEmail As String) As String
        Dim strTmp As String
        Dim n As Long
        Dim sEXT As String
        Dim iMensajeError As String
        Dim iEmailAdress As MailAddress


        If Len(eEmail) > 0 Then
            sEXT = eEmail

            Do While InStr(1, sEXT, ".") <> 0
                sEXT = Right(sEXT, Len(sEXT) - InStr(1, sEXT, "."))
            Loop

            If InStr(1, eEmail, "@") = 0 Then
                iMensajeError = iMensajeError & "La dirección de email no contiene el signo @" & vbNewLine
            ElseIf InStr(1, eEmail, "@") = 1 Then
                iMensajeError = iMensajeError & "El @ No puede estar al principio" & vbNewLine
            ElseIf InStr(1, eEmail, "@") = Len(eEmail) Then
                iMensajeError = iMensajeError & "El @ no puede estar al final de la dirección" & vbNewLine
            ElseIf EXTisOK(sEXT) = False Then
                iMensajeError = iMensajeError & "LA dirección no tiene un dominio válido, "
                iMensajeError = iMensajeError & "por ejemplo : "
                iMensajeError = iMensajeError & ".com, .net, .gov, .org, .edu, .biz, .tv etc.. " & vbNewLine
            ElseIf Len(eEmail) < 6 Then
                iMensajeError = iMensajeError & "La dirección no puede ser menor a 6 caracteres." & vbNewLine
            ElseIf eEmail.Contains("..") Then
                iMensajeError = iMensajeError & "La dirección no puede contener dos puntos seguidos." & vbNewLine
            ElseIf eEmail.Contains(" ") Then
                iMensajeError = iMensajeError & "La dirección no puede contener espacios." & vbNewLine
            ElseIf eEmail.EndsWith(".") Then
                iMensajeError = iMensajeError & "La dirección no puede terminar con un punto." & vbNewLine
            End If

            strTmp = eEmail

            Do While InStr(1, strTmp, "@") <> 0
                n = 1
                strTmp = Right(strTmp, Len(strTmp) - InStr(1, strTmp, "@"))
            Loop

            If n > 1 Then iMensajeError = iMensajeError & "Solo puede haber un @ en la dirección de email" & vbNewLine

            Dim pos As Integer

            pos = InStr(1, eEmail, "@")

            If (iMensajeError = Nothing) Then

                If Mid(eEmail, pos + 1, 1) = "." Then iMensajeError = iMensajeError & "El punto no puede estar seguido del @" & vbNewLine

                If Mid(eEmail, pos - 1, 1) = "." Then iMensajeError = iMensajeError & "El punto no puede estar antes del @" & vbNewLine

                Dim iExpresionRegular As New Regex("^[_a-zA-Z-0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$")
                'Dim iExpresionRegular As New Regex("([a-zA-Z][[a-zA-Z0-9].-]+@)+[a-zA-Z][a-zA-Z0-9.-]+")


                If Not iExpresionRegular.IsMatch(eEmail) Then
                    iMensajeError = iMensajeError & "La dirección contiene caracteres inválidos"
                End If

                Try
                    iEmailAdress = New MailAddress(eEmail)
                Catch ex As Exception
                    iMensajeError = iMensajeError & "La dirección ingresada no es válida"
                End Try

                If iMensajeError = Nothing Then
                    Try
                        System.Net.Dns.GetHostEntry(eEmail.Substring(pos, eEmail.Length - pos))
                    Catch ex As Exception
                        iMensajeError = iMensajeError & "No existe el dominio de la dirección del email"
                    End Try
                End If
            End If
        End If

        Return iMensajeError

    End Function

    Private Shared Function EXTisOK(ByVal sEXT As String) As Boolean
        Dim EXT As String

        EXTisOK = False

        If Left(sEXT, 1) <> "." Then sEXT = "." & sEXT
        sEXT = UCase(sEXT)
        EXT = EXT & ".COM.EDU.GOV.NET.BIZ.ORG.TV"
        EXT = EXT & ".AF.AL.DZ.As.AD.AO.AI.AQ.AG.AP.AR.AM.AW.AU.AT.AZ.BS.BH.BD.BB.BY"
        EXT = EXT & ".BE.BZ.BJ.BM.BT.BO.BA.BW.BV.BR.IO.BN.BG.BF.MM.BI.KH.CM.CA.CV.KY"
        EXT = EXT & ".CF.TD.CL.CN.CX.CC.CO.KM.CG.CD.CK.CR.CI.HR.CU.CY.CZ.DK.DJ.DM.DO"
        EXT = EXT & ".TP.EC.EG.SV.GQ.ER.EE.ET.FK.FO.FJ.FI.CS.SU.FR.FX.GF.PF.TF.GA.GM.GE.DE"
        EXT = EXT & ".GH.GI.GB.GR.GL.GD.GP.GU.GT.GN.GW.GY.HT.HM.HN.HK.HU.IS.IN.ID.IR.IQ"
        EXT = EXT & ".IE.IL.IT.JM.JP.JO.KZ.KE.KI.KW.KG.LA.LV.LB.LS.LR.LY.LI.LT.LU.MO.MK.MG"
        EXT = EXT & ".MW.MY.MV.ML.MT.MH.MQ.MR.MU.YT.MX.FM.MD.MC.MN.MS.MA.MZ.NA"
        EXT = EXT & ".NR.NP.NL.AN.NT.NC.NZ.NI.NE.NG.NU.NF.KP.MP.NO.OM.PK.PW.PA.PG.PY"
        EXT = EXT & ".PE.PH.PN.PL.PT.PR.QA.RE.RO.RU.RW.GS.SH.KN.LC.PM.ST.VC.SM.SA.SN.SC"
        EXT = EXT & ".SL.SG.SK.SI.SB.SO.ZA.KR.ES.LK.SD.SR.SJ.SZ.SE.CH.SY.TJ.TW.TZ.TH.TG.TK"
        EXT = EXT & ".TO.TT.TN.TR.TM.TC.TV.UG.UA.AE.UK.US.UY.UM.UZ.VU.VA.VE.VN.VG.VI"
        EXT = EXT & ".WF.WS.EH.YE.YU.ZR.ZM.ZW"
        EXT = UCase(EXT)

        If InStr(1, EXT, sEXT, 0) <> 0 Then
            EXTisOK = True
        End If

    End Function

    Public Shared Function obtenerDiferenciaMeses(ByVal eFechaDesde As Date, ByVal eFechaHasta As Date) As Integer

        Try

            Return Int(DateDiff(DateInterval.Day, eFechaDesde, eFechaHasta) / 30)

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function isEntero(ByVal eNumero As Double) As Boolean

        Dim iDecimal As Double
        iDecimal = eNumero - Decimal.Truncate(eNumero)
        If iDecimal = 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function convertirFechaInterfacesInsercion(ByVal eFecha As Date, eFormatoFecha As enumFormatoFechaInterfaces) As String

        Try
            Select Case eFormatoFecha
                Case "DDMMYY"
                    '310521
                    Return Format(eFecha, "ddMMyy").ToString
                Case "YYMMDD"
                    '210531
                    Return Format(eFecha, "yyMMdd").ToString
                Case "DDMMYYYY"
                    '31052021
                    Return Format(eFecha, "ddMMyyyy").ToString
                Case "YYYYMMDD"
                    '20210531
                    Return Format(eFecha, "yyyyMMdd").ToString
                Case "DD_MM_YYYY"
                    '31/05/21
                    Return Format(eFecha, "dd_MM_yy").ToString
                Case "YYYY_MM_DD"
                    '2021/05/21
                    Return Format(eFecha, "yyyy_MM_dd").ToString
                Case "DD_MM_YY"
                    '31/05/2021
                    Return Format(eFecha, "dd_MM_yyyy").ToString
                Case "YY_MM_DD"
                    '21/05/31
                    Return Format(eFecha, "yy_MM_dd").ToString
                Case "JULIANA"
                    '2021/05/21
                    Return fechaJuliana(eFecha).ToString
            End Select

        Catch e As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function convertirFechaInterfaces(ByVal eFecha As String, eFormatoFecha As enumFormatoFechaInterfaces) As Date
        Dim iDia, iMes, iAño As String

        Try
            Select Case eFormatoFecha
                Case enumFormatoFechaInterfaces.DDMMYY
                    '310521
                    iDia = eFecha.Substring(0, 2)
                    iMes = eFecha.Substring(2, 2)
                    iAño = eFecha.Substring(4, 2)
                Case enumFormatoFechaInterfaces.DDMMYYYY
                    '31052021
                    iDia = eFecha.Substring(0, 2)
                    iMes = eFecha.Substring(2, 2)
                    iAño = eFecha.Substring(4, 4)
                Case enumFormatoFechaInterfaces.DD_MM_YY
                    '31/05/21
                    iDia = eFecha.Substring(0, 2)
                    iMes = eFecha.Substring(3, 2)
                    iAño = eFecha.Substring(6, 2)
                Case enumFormatoFechaInterfaces.DD_MM_YYYY
                    '31/05/2021
                    iDia = eFecha.Substring(0, 2)
                    iMes = eFecha.Substring(3, 2)
                    iAño = eFecha.Substring(6, 4)
                Case enumFormatoFechaInterfaces.YYMMDD
                    '210531
                    iAño = eFecha.Substring(0, 2)
                    iMes = eFecha.Substring(2, 2)
                    iDia = eFecha.Substring(4, 2)
                Case enumFormatoFechaInterfaces.YYYYMMDD
                    '20210531
                    iAño = eFecha.Substring(0, 4)
                    iMes = eFecha.Substring(4, 2)
                    iDia = eFecha.Substring(6, 2)
                Case enumFormatoFechaInterfaces.YY_MM_DD
                    '21/05/31
                    iAño = eFecha.Substring(0, 2)
                    iMes = eFecha.Substring(3, 2)
                    iDia = eFecha.Substring(6, 2)
                Case enumFormatoFechaInterfaces.YYYY_MM_DD
                    '2021/05/21
                    iAño = eFecha.Substring(0, 4)
                    iMes = eFecha.Substring(5, 2)
                    iDia = eFecha.Substring(8, 2)
                Case enumFormatoFechaInterfaces.JULIANA
                    '2021/05/21
                    iAño = eFecha.Substring(0, 4)
                    iMes = eFecha.Substring(3, 2)
                    iDia = eFecha.Substring(6, 2)
            End Select

            If IsDate(iDia & "/" & iMes & "/" & iAño) Then
                Return CDate(iDia & "/" & iMes & "/" & iAño)
            Else
                Return Nothing
            End If

        Catch e As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function convertirFecha(ByVal eFecha As String, Optional eAnioMesDia As Boolean = False) As Date
        Try
            If IsDate(eFecha) Then
                Return eFecha
            ElseIf eFecha = Nothing Then
                Return Nothing
            Else
                Dim iDia, iMes, iAño As String
                If Len(eFecha) = 8 Then
                    If eAnioMesDia Then
                        iDia = eFecha.Substring(4, 4)
                        iMes = eFecha.Substring(2, 2)
                        iAño = eFecha.Substring(0, 2)
                    Else
                        iDia = eFecha.Substring(0, 2)
                        iMes = eFecha.Substring(2, 2)
                        iAño = eFecha.Substring(4, 4)
                    End If
                    If IsDate(iDia & "/" & iMes & "/" & iAño) Then
                        Return CDate(iDia & "/" & iMes & "/" & iAño)
                    Else
                        Return Nothing
                    End If
                ElseIf Len(eFecha) = 6 Then
                    If eAnioMesDia Then
                        iDia = eFecha.Substring(4, 2)
                        iMes = eFecha.Substring(2, 2)
                        iAño = eFecha.Substring(0, 2)
                    Else
                        iDia = eFecha.Substring(0, 2)
                        iMes = eFecha.Substring(2, 2)
                        iAño = eFecha.Substring(4, 2)
                    End If

                    If iAño < 50 Then
                        iAño = "20" & iAño
                    Else
                        iAño = "19" & iAño
                    End If
                    If IsDate(iDia & "/" & iMes & "/" & iAño) Then
                        Return CDate(iDia & "/" & iMes & "/" & iAño)
                    Else
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            End If

        Catch e As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function convertirHora(ByVal eFecha As String) As String
        Try
            If eFecha = Nothing Then
                Return Nothing
            Else
                Dim iHora As String
                iHora = Format$(eFecha, "hh:mm:ss")
                Return iHora
            End If

        Catch e As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function obtenerDireccion(ByVal eDireccion As String) As String
        Dim i As Integer
        Dim iElemento As String
        Dim iDatoSalida As String = ""
        Dim iRegistro() As String
        Try
            'Evaluo si solo hay numeros en la ultima parte de la direccion 

            iRegistro = Trim(eDireccion).Split(" ")

            If iRegistro.Length > 0 AndAlso IsNumeric(iRegistro(iRegistro.Length - 1)) Then
                For i = 0 To iRegistro.Length - 2
                    iDatoSalida &= iRegistro(i) & " "
                Next
                Return Trim(iDatoSalida)
            Else
                For i = 1 To Len(eDireccion)
                    iElemento = Mid(eDireccion, i, 1)
                    If Not IsNumeric(iElemento) Then
                        iDatoSalida = iDatoSalida & iElemento
                    Else
                        Exit For
                    End If
                Next

                Return iDatoSalida

            End If
        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function obtenerNumeroDireccion(ByVal eDireccion As String) As String
        Dim i As Integer
        Dim iElemento As String
        Dim iDatoSalida As String = ""
        Dim iCaracterEncontrado As Boolean
        Dim iNumeroEncontrado As Boolean
        Dim iRegistro() As String
        Try
            'Evaluo si solo hay numeros en la ultima parte de la direccion 

            iRegistro = Trim(eDireccion).Split(" ")

            If iRegistro.Length > 0 AndAlso IsNumeric(iRegistro(iRegistro.Length - 1)) Then

                iDatoSalida = iRegistro(iRegistro.Length - 1)

                Return iDatoSalida
            Else
                iCaracterEncontrado = False
                iNumeroEncontrado = False
                For i = 1 To Len(eDireccion)
                    iElemento = Mid(eDireccion, i, 1)
                    If IsNumeric(iElemento) And Not iCaracterEncontrado Then
                        iDatoSalida = iDatoSalida & iElemento
                        iNumeroEncontrado = True
                    ElseIf iNumeroEncontrado Then
                        iCaracterEncontrado = True
                    End If
                Next

                Return iDatoSalida
            End If
        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function convertirNumeroALetra(ByVal eString As String) As String
        Dim i As Integer
        Dim iString As String = ""
        For i = 0 To eString.ToUpper.Length - 1
            Select Case eString.ToUpper.Chars(i)
                Case "A", "B", "C"
                    iString = iString & "2"
                Case "D", "E", "F"
                    iString = iString & "3"
                Case "G", "H", "I"
                    iString = iString & "4"
                Case "J", "K", "L"
                    iString = iString & "5"
                Case "M", "N", "O"
                    iString = iString & "6"
                Case "P", "Q", "S", "R"
                    iString = iString & "1"
                Case "T", "U", "V"
                    iString = iString & "8"
                Case "W", "X", "Y", "Z"
                    iString = iString & "9"
            End Select
        Next
        Return iString
    End Function

    Private Shared Function obtenerTipoRedondeo(Optional ByVal eAccesoDatos As accesoDatos = Nothing) As enumTipoRedondeo
        Dim iConexion As accesoDatos
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iDataSetSingleton As DataSet
        Dim iDataRow As DataRow
        Try
            iDataSetSingleton = ParametroSingleton.getInstancia(False).dataSet
            Dim iBusqueda(1) As Object
            If Not IsNothing(iDataSetSingleton) Then
                iBusqueda(0) = "tipoRedondeo"
                iBusqueda(1) = FuncionComun.GRUPOEMPRESA
                iDataRow = iDataSetSingleton.Tables("parametros").Rows.Find(iBusqueda)
                If Not IsNothing(iDataRow) Then
                    Return iDataRow.Item("valor")
                End If
            End If

            If eAccesoDatos Is Nothing Then
                iConexion = New accesoDatos
            Else
                iConexion = eAccesoDatos
            End If

            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarCondicionWhere("descripcion='tipoRedondeo'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Return iDataReader.Item("valor")
            Else
                Return enumTipoRedondeo.SINREDONDEO
            End If

        Catch exception As Exception
            Throw New ComunException("Se produjo un error al obtener el tipo de redondeo")
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            If (IsNothing(eAccesoDatos)) Then
                If Not IsNothing(iConexion) Then iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Shared Function formatearImagen(ByVal eHeight As Integer, ByVal eWidth As Integer, ByVal eHeightImagen As Integer, ByVal eWidthImagen As Integer) As FormatoImagenVO
        Dim iProporcional As Double
        Dim iFormatoImageVO As New FormatoImagenVO

        Try

            If eHeightImagen > eHeight Then
                iProporcional = (eHeightImagen - eHeight) / eHeightImagen
                iFormatoImageVO.height = eHeightImagen
                iFormatoImageVO.width = eWidthImagen - (eWidthImagen * iProporcional)
            ElseIf eWidthImagen > eWidth Then
                iProporcional = (eWidthImagen - eWidth) / eWidthImagen
                iFormatoImageVO.width = eWidthImagen
                iFormatoImageVO.height = eHeightImagen - (eHeightImagen * iProporcional)
            End If

            Return iFormatoImageVO

        Catch Exception As Exception
            Throw New Exception("no se pudo dar el formato a la imagen")
        End Try
    End Function

    Public Shared Function esArchivoImagen(ByVal eNombreArchivo As String) As Boolean
        Dim iExtension As String

        Try

            iExtension = obtenerExtensionArchivo(eNombreArchivo)

            Return iExtension.Contains("JPG") OrElse iExtension.Contains("JPEG") OrElse iExtension.Contains("PNG") OrElse iExtension.Contains("BMP") OrElse iExtension.Contains("TIF") OrElse iExtension.Contains("GIF") OrElse iExtension.Contains("RAW") OrElse iExtension.Contains("TIFF")

        Catch Exception As Exception
            Return False
        End Try
    End Function

    Public Shared Function formatoFecha(ByVal eString As String) As String
        Try
            eString = eString.Replace(" ", "")
            If Trim(eString).Length = 10 Then
                Dim i As Integer
                For i = 0 To eString.Length - 1
                    If System.Char.IsLetter(eString.Chars(i)) OrElse System.Char.IsWhiteSpace(eString.Chars(i)) Then
                        eString = "0000-00-00"
                        Exit For
                    End If
                Next
                If CInt(eString.Substring(eString.Length - 2, 2)) = 0 Then eString = eString.Substring(0, 8) & "01"
                If eString.Substring(eString.Length - 2, 2) = "00" Then eString = "0000-00-00"
                If eString.Substring(eString.Length - 5, 2) = "00" Then eString = "0000-00-00"
                If eString.Substring(0, 4) = "0000" Then eString = "0000-00-00"
                If CInt(eString.Substring(0, 4)) > 2100 AndAlso eString <> "0000-00-00" Then eString = "00" & eString.Substring(2, 8)
                If CInt(eString.Substring(0, 4)) < 20 AndAlso eString <> "0000-00-00" Then eString = "20" & eString.Substring(2, 8)
                If CInt(eString.Substring(0, 4)) < 1900 AndAlso eString <> "0000-00-00" Then eString = "19" & eString.Substring(2, 8)
                If CInt(eString.Substring(eString.Length - 5, 2)) > 12 Then eString = eString.Substring(0, 5) & "12" & eString.Substring(eString.Length - 3, 3)
                If CInt(eString.Substring(eString.Length - 2, 2)) > 31 Then eString = eString.Substring(0, 8) & "31"

                If eString.Substring(eString.Length - 5, 2) = "04" AndAlso CInt(eString.Substring(eString.Length - 2, 2)) > 30 Then eString = eString.Substring(0, 8) & "30"
                If eString.Substring(eString.Length - 5, 2) = "06" AndAlso CInt(eString.Substring(eString.Length - 2, 2)) > 30 Then eString = eString.Substring(0, 8) & "30"
                If eString.Substring(eString.Length - 5, 2) = "09" AndAlso CInt(eString.Substring(eString.Length - 2, 2)) > 30 Then eString = eString.Substring(0, 8) & "30"
                If eString.Substring(eString.Length - 5, 2) = "11" AndAlso CInt(eString.Substring(eString.Length - 2, 2)) > 30 Then eString = eString.Substring(0, 8) & "30"
                If eString.Substring(eString.Length - 5, 2) = "02" Then
                    Select Case CInt(eString.Substring(0, 4))
                        Case 2028, 2024, 2020, 2016, 2012, 2008, 2004, 2000, 1996, 1992, 1988, 1984, 1980, 1976, 1972, 1968, 1964, 1960, 1956, 1952, 1948, 1944, 1940, 1936, 1932, 1928, 1924, 1920, 1916, 1912, 1908, 1904, 1900
                            If CInt(eString.Substring(eString.Length - 2, 2)) > 29 Then eString = eString.Substring(0, 8) & "29"
                        Case Else
                            If CInt(eString.Substring(eString.Length - 2, 2)) > 28 Then eString = eString.Substring(0, 8) & "28"
                    End Select
                End If
            Else
                If Not IsDate(eString) Then
                    eString = "0000-00-00"
                End If
            End If
            Return eString
        Catch exception As Exception
            Throw exception
        End Try
    End Function

    Public Shared Function obtenerExtensionArchivo(ByVal ePathArchivo As String) As String
        Dim iArchivo As FileInfo

        Try
            If ePathArchivo <> Nothing AndAlso Len(ePathArchivo) > 0 Then
                iArchivo = New FileInfo(ePathArchivo)

                Return iArchivo.Extension.ToUpper
            Else
                Return ""
            End If

        Catch Exception As Exception
            Throw New Exception("no se pudo obtener la extension del archivo enviado")
        Finally
            iArchivo = Nothing
        End Try
    End Function

    Public Shared Function obtenerNombreArchivo(ByVal ePathArchivo As String) As String
        Dim iArchivo As New FileInfo(ePathArchivo)
        Try
            Return iArchivo.Name.ToUpper
        Catch Exception As Exception
            Throw New Exception("No se pudo obtener el nombre del archivo enviado")
        Finally
            iArchivo = Nothing
        End Try
    End Function

    Public Shared Function obtenerCFTTEA(iGuess As Double, eValores As Double(), eFechas As Double()) As Double
        Dim iXIRR As New FormulaFinanciera.xirr
        Dim iResultado As Double
        Try
            iResultado = iXIRR.Newtons_method(iGuess, iXIRR.total_f_xirr(eValores, eFechas), iXIRR.total_df_xirr(eValores, eFechas))

            Return iResultado

        Catch exception As Exception
            Throw exception
        End Try
    End Function

    Public Shared Function obtenerTIR(iGuess As Double, eValores As Double(), eFechas As Double()) As Double
        Dim iXIRR As New FormulaFinanciera.xirr
        Dim iResultado As Double
        Try
            iResultado = iXIRR.Newtons_method(iGuess, iXIRR.total_f_xirr(eValores, eFechas), iXIRR.total_df_xirr(eValores, eFechas)) * 100

            Return iResultado

        Catch exception As Exception
            Throw exception
        End Try
    End Function

    Public Shared Function obtenerTIR(eValores As Double(), iGuess As Double) As Double

        Dim iResultado As Double

        Try
            iResultado = Financial.IRR(eValores, iGuess) * 100

            Return iResultado

        Catch exception As Exception
            Throw exception
        End Try
    End Function

    Public Shared Function calcularDigitoVerificadorBCRA(ByVal eString As String) As Integer
        Dim iDigitoVerificador As Integer
        Dim iNumeroASumar As String
        Dim i As Integer
        Try

            eString = Right(StrDup(21, "0") & eString, 21)
            For i = 1 To 21
                If i Mod 2 <> 0 Then
                    iNumeroASumar = Right("00" & CLng(Mid(eString, i, 1)) * 2, 2)
                Else
                    iNumeroASumar = Right("00" & CLng(Mid(eString, i, 1)), 2)
                End If
                iDigitoVerificador += CLng(Mid(iNumeroASumar, 1, 1)) + CLng(Mid(iNumeroASumar, 2, 1))
            Next
            iDigitoVerificador = 10 - Right(iDigitoVerificador, 1)
            If iDigitoVerificador = 10 Then
                Return 0
            Else
                Return iDigitoVerificador
            End If
        Catch Exception As Exception
            Throw New DigitoVerificadorNoCalculadoException(Exception)
        End Try
    End Function

    Public Shared Function validarSoloLetras(ByVal eString As String) As Boolean
        Dim i As Integer
        Dim iString As String
        Dim iBoolean As Boolean = True
        For i = 0 To eString.Length - 1
            If Not (System.Char.IsLetter(eString.Chars(i)) OrElse System.Char.IsWhiteSpace(eString.Chars(i))) Then
                iBoolean = False
            End If
        Next
        Return iBoolean
    End Function

    Public Shared Sub enviarMailProceso(eAccesoDatos As accesoDatos, eMensaje As String, Optional ByVal eDireccionEmail As String = Nothing)
        Dim iConexion As accesoDatos
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iMAil As String = ""
        Try
            iConexion = eAccesoDatos

            If eDireccionEmail = Nothing Then
                iGeneradorSql.agregarTabla("Parametro")
                iGeneradorSql.agregarColumna("valor")
                iGeneradorSql.agregarCondicionWhere("descripcion='eMailProcesoAutomaticoFacturacion'")

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    iMAil = iDataReader.Item("valor").ToString
                    iDataReader.Close()
                End If
            Else
                iMAil = eDireccionEmail
            End If

            If iMAil <> Nothing Then
                EnviaMail.enviarMail(iMAil, "PROCESO AUTOMATICO", eMensaje)
            End If

        Catch exception As Exception
            Throw New ComunException("Se produjo un error al enviar el mail.")
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Shared Function obtenerCodigoHashMD5(eTexto As String) As String
        Using md5Hash As MD5 = MD5.Create()
            Return generarMD5(md5Hash, eTexto)
        End Using
    End Function

    Shared Function generarMD5(ByVal eMD5Hash As MD5, ByVal eCampo As String) As String
        Dim data As Byte() = eMD5Hash.ComputeHash(Encoding.ASCII.GetBytes(eCampo))
        Dim sBuilder As New StringBuilder()
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i
        Return sBuilder.ToString()
    End Function

    Public Shared Function esPasswordFuerte(eAccesoDatos As accesoDatos, ePassword As String, ByRef eMensaje As String) As Boolean
        Dim iGeneradorSql As New GeneradorSql
        Dim iConexion As accesoDatos
        Dim iDataSet As DataSet

        Dim letterCount As Integer = 0
        Dim numbreCount As Integer = 0
        Dim symbolCount As Integer = 0
        Dim lowerCount As Integer = 0
        Dim upperCount As Integer = 0

        Dim iIncluirCaracteresEspecialesContrasenia As Boolean
        Dim iUsarMayusculasMinusculasConstasenia As Boolean
        Dim iIncluirNumerosContrasenia As Boolean
        Dim iLargoMinimoContrasenia As Integer
        Dim iSuperaValidacion As Boolean

        Try

            If IsNothing(eAccesoDatos) Then
                iConexion = New accesoDatos
            Else
                iConexion = eAccesoDatos
            End If

            'Obtengo los paramtros
            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'LargoMinimoContrasenia'")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            If iDataSet.Tables("Parametros").Rows.Count > 0 Then
                iLargoMinimoContrasenia = FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametros").Rows.Item(0).Item("valor").ToString())
            End If

            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'UsarMayusculasMinusculasConstasenia'")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            If iDataSet.Tables("Parametros").Rows.Count > 0 Then
                iUsarMayusculasMinusculasConstasenia = FuncionComun.byteBoolean(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametros").Rows.Item(0).Item("valor").ToString()))
            End If

            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'IncluirNumerosContrasenia'")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            If iDataSet.Tables("Parametros").Rows.Count > 0 Then
                iIncluirNumerosContrasenia = FuncionComun.byteBoolean(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametros").Rows.Item(0).Item("valor").ToString()))
            End If

            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'IncluirCaracteresEspecialesContrasenia'")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            If iDataSet.Tables("Parametros").Rows.Count > 0 Then
                iIncluirCaracteresEspecialesContrasenia = FuncionComun.byteBoolean(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametros").Rows.Item(0).Item("valor").ToString()))
            End If

            iSuperaValidacion = True

            For i As Integer = 0 To ePassword.Length - 1
                If [Char].IsLower(ePassword(i)) Then
                    lowerCount += 1
                ElseIf [Char].IsUpper(ePassword(i)) Then
                    upperCount += 1
                ElseIf [Char].IsLetter(ePassword(i)) Then
                    letterCount += 1
                ElseIf [Char].IsNumber(ePassword(i)) Then
                    numbreCount += 1
                Else
                    symbolCount += 1
                End If
            Next

            If iLargoMinimoContrasenia > 0 AndAlso iLargoMinimoContrasenia > ePassword.Length Then iSuperaValidacion = False
            If iSuperaValidacion AndAlso iUsarMayusculasMinusculasConstasenia AndAlso (lowerCount = 0 OrElse upperCount = 0) Then iSuperaValidacion = False
            If iSuperaValidacion AndAlso iIncluirNumerosContrasenia AndAlso numbreCount = 0 Then iSuperaValidacion = False
            If iSuperaValidacion AndAlso iIncluirCaracteresEspecialesContrasenia AndAlso symbolCount = 0 Then iSuperaValidacion = False

            If Not iSuperaValidacion Then
                eMensaje = "<p>La contraseña debe respetar los siguientes lineamientos"
                eMensaje &= "<ul>"

                If iLargoMinimoContrasenia > 0 Then eMensaje &= "<li>Deber contener como minimo " & iLargoMinimoContrasenia & " caracteres " & IIf(iLargoMinimoContrasenia > ePassword.Length, "FALLO", "PASO") & "</li>"
                If iUsarMayusculasMinusculasConstasenia Then eMensaje &= "<li>Deber contener mayusculas y minusculas " & IIf(lowerCount = 0 OrElse upperCount = 0, "FALLO", "PASO") & "</li>"
                If iIncluirNumerosContrasenia Then eMensaje &= "<li>Deber contener por lo menos un numero " & IIf(numbreCount = 0, "FALLO", "PASO") & "</li>"
                If iIncluirCaracteresEspecialesContrasenia Then eMensaje &= "<li>Deber contener por lo menos un simbolo " & IIf(symbolCount = 0, "FALLO", "PASO") & "</li>"

                eMensaje &= "</ul>"
                eMensaje &= "</p>"
            End If

            Return iSuperaValidacion

        Catch exception As Exception
            Return False
        Finally
            If IsNothing(eAccesoDatos) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
        End Try
    End Function


    Public Shared Function obtenerSepararEnParrafos(ByVal eString As String, eCantidadMAximaCaracteres As Integer) As Collection
        Dim i, j As Integer
        Dim iElemento As String
        Dim iStringSalida As New Collection
        Dim iPalabras() As String
        Try
            iPalabras = Trim(eString).Split(" ")

            If iPalabras.Length > 0 Then

                For i = 0 To iPalabras.Length - 1
                    If (iElemento & iPalabras(i)).Length > eCantidadMAximaCaracteres Then
                        iStringSalida.Add(Trim(iElemento))
                        iElemento = iPalabras(i) & " "
                    Else
                        iElemento &= iPalabras(i) & " "
                    End If
                Next
                If iElemento <> Nothing Then
                    iStringSalida.Add(Trim(iElemento))
                End If
            End If
            Return iStringSalida
        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function obtenerMotivoOriginal(ByVal eExcepcion As Exception, Optional eSinVBNewLine As Boolean = False) As String
        Dim iMensaje As String

        Try
            If Not IsNothing(eExcepcion) Then
                If TypeOf (eExcepcion) Is RootException Then
                    Dim iExcepcionAplicacion As RootException = eExcepcion
                    iMensaje = iExcepcionAplicacion.ToString & IIf(eSinVBNewLine, "", vbNewLine)
                    While Not IsNothing(iExcepcionAplicacion.originalCause)
                        If TypeOf (iExcepcionAplicacion.originalCause) Is RootException Then
                            iExcepcionAplicacion = iExcepcionAplicacion.originalCause
                            iMensaje = iExcepcionAplicacion.ToString & IIf(eSinVBNewLine, "", vbNewLine)
                        Else
                            iMensaje = iExcepcionAplicacion.originalCause.ToString
                            iExcepcionAplicacion.originalCause = Nothing
                        End If
                    End While
                Else
                    iMensaje = eExcepcion.ToString
                End If
            End If
            iMensaje = iMensaje.Replace(vbNewLine, "")
            Return iMensaje

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Sub loguearDepuracionCreditos(ByVal eCredito As String, ByVal eMensajeError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "DepuracionCreditos" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eCredito & " - " & eMensajeError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

#Region "Excel"

    Public Shared Function obtenerEstilosExcel(eStilo As IXLStyle, eNombre As enumFormatoExcel) As IXLStyle
        Try
            Select Case eNombre
                Case enumFormatoExcel.ESTILOTITULOAZUL

                    With eStilo
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Blue)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White

                    End With
                Case enumFormatoExcel.ESTILOTITULOAZULDECIMAL
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.White
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Fill.SetBackgroundColor(XLColor.Blue)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                        .NumberFormat.Format = "_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                    End With
                Case enumFormatoExcel.ESTILOTITULOAZULPESOS
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Blue)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                        Select Case ConfigurationManager.AppSettings("Region")
                            Case "ARGENTINA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "URUGUAY"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "PARAGUAY"
                                .NumberFormat.Format = "_ Gs * #,##0.00_ ;_ Gs * -#,##0.00_ ;_ Gs * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "COLOMBIA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                        End Select
                    End With
                Case enumFormatoExcel.ESTILOSUBTITULO
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 12
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Font.FontColor = XLColor.Black
                        .Fill.SetBackgroundColor(XLColor.White)
                    End With
                Case enumFormatoExcel.ESTILOTITULO
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 14
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.Single
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Black)
                    End With
                Case enumFormatoExcel.ESTILOTITULOCENTRADO
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 14
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.Single
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Black)
                    End With
                Case enumFormatoExcel.ESTILOTITULOAZULSINBORDE
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Blue)
                    End With
                Case enumFormatoExcel.ESTILOTITULONARANJA
                    With eStilo
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.General
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Orange)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                    End With
                Case enumFormatoExcel.ESTILOTITULOROJO
                    With eStilo
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Red)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                    End With
                Case enumFormatoExcel.ESTILOTITULOVERDE
                    With eStilo
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.General
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Green)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                    End With
                Case enumFormatoExcel.ESTILOTITULOVERDESINBORDE
                    With eStilo
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Green)
                    End With
                Case enumFormatoExcel.ESTILOTITULOAMARILLO
                    With eStilo
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.Gray
                        .Fill.SetBackgroundColor(XLColor.Yellow)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                    End With
                Case enumFormatoExcel.ESTILOTITULOVIOLETA
                    With eStilo
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.General
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Violet)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                    End With
                Case enumFormatoExcel.ESTILONNORMALNUMERO
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = False
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.Black
                        '.NumberFormat.Format = "_ * #,##0_ ;_ * -#,##0_ ;_ * " & Chr(34) & "-" & Chr(34) & "_ ;_ @_ "
                        .NumberFormat.Format = "_(0_);_( \(0\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
                    End With
                Case enumFormatoExcel.ESTILONNORMALNUMEROROJO
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Red)
                        '                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        '                       .Border.InsideBorderColor = XLColor.White
                        .NumberFormat.Format = "_ * #,##0_ ;_ * -#,##0_ ;_ * " & Chr(34) & "-" & Chr(34) & "_ ;_ @_ "
                    End With
                Case enumFormatoExcel.ESTILONNORMALDECIMAL
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = False
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.Black
                        .NumberFormat.Format = "_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                    End With
                Case enumFormatoExcel.ESTILONNORMALFECHA
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = False
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Font.FontColor = XLColor.Black
                    End With
                Case enumFormatoExcel.ESTILOTITULOGRISPESOS
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Gray)
                        Select Case ConfigurationManager.AppSettings("Region")
                            Case "ARGENTINA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "URUGUAY"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "PARAGUAY"
                                .NumberFormat.Format = "_ Gs * #,##0.00_ ;_ Gs * -#,##0.00_ ;_ Gs * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "COLOMBIA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                        End Select
                    End With
                Case enumFormatoExcel.ESTILOTITULOGRISCLAROPESOS
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.Black
                        .Fill.SetBackgroundColor(XLColor.Gainsboro)
                        Select Case ConfigurationManager.AppSettings("Region")
                            Case "ARGENTINA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "URUGUAY"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "PARAGUAY"
                                .NumberFormat.Format = "_ Gs * #,##0.00_ ;_ Gs * -#,##0.00_ ;_ Gs * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "COLOMBIA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                        End Select
                    End With
                Case enumFormatoExcel.ESTILOTITULOGRISPORCENTAJE
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Gray)
                        Select Case ConfigurationManager.AppSettings("Region")
                            Case "ARGENTINA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "URUGUAY"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "PARAGUAY"
                                .NumberFormat.Format = "_ Gs * #,##0.00_ ;_ Gs * -#,##0.00_ ;_ Gs * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "COLOMBIA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                        End Select
                    End With
                Case enumFormatoExcel.ESTILOTITULOGRISNUMERO
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Gray)
                        Select Case ConfigurationManager.AppSettings("Region")
                            Case "ARGENTINA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "URUGUAY"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "PARAGUAY"
                                .NumberFormat.Format = "_ Gs * #,##0.00_ ;_ Gs * -#,##0.00_ ;_ Gs * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                            Case "COLOMBIA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                        End Select
                    End With
                Case enumFormatoExcel.ESTILOTITULOGRIS
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Gray)
                    End With
                Case enumFormatoExcel.ESTILOSUBTITULOGRIS
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.DarkGray)
                    End With
                Case enumFormatoExcel.ESTILONNORMALPESOS
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = False
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.Black
                        '.NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                        Select Case ConfigurationManager.AppSettings("Region")
                            Case "ARGENTINA"
                                .NumberFormat.Format = "_($ #,##0.00_);_($ \(#,##0.00\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
                            Case "URUGUAY"
                                .NumberFormat.Format = "_($ #,##0.00_);_($ \(#,##0.00\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
                            Case "PARAGUAY"
                                .NumberFormat.Format = "_(Gs #,##0.00_);_(Gs \(#,##0.00\);_(* " & Chr(34) & "-" & Chr(34) & "??_);_(@_)"
                            Case "COLOMBIA"
                                .NumberFormat.Format = "_ $ * #,##0.00_ ;_ $ * -#,##0.00_ ;_ $ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                        End Select
                    End With
                Case enumFormatoExcel.ESTILONNORMALPORCENTAJE
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = False
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.Black
                        .NumberFormat.Format = "_ * #,##0.00 %_ ;_ * -#,##0.00 %_ ;_ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                    End With
                Case enumFormatoExcel.ESTILONNORMALTEXTO
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = False
                        .Font.Underline = XLFontUnderlineValues.None
                        .NumberFormat.Format = "@"
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Font.FontColor = XLColor.Black
                    End With
                Case enumFormatoExcel.ESTILONNORMALTEXTOROJO
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Red)
                        ' .Border.InsideBorder = XLBorderStyleValues.Thin
                        ' .Border.InsideBorderColor = XLColor.White
                    End With
                Case enumFormatoExcel.ESTILONNORMALPORCENTAJEAZUL
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Blue)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                        .NumberFormat.Format = "_ * #,##0.00 %_ ;_ * -#,##0.00 %_ ;_ * " & Chr(34) & "-" & Chr(34) & "??_ ;_ @_ "
                    End With
                Case enumFormatoExcel.ESTILONNORMALNUMEROAZUL
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Blue)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                        .NumberFormat.Format = "_ * #,##0_ ;_ * -#,##0_ ;_ * " & Chr(34) & "-" & Chr(34) & "_ ;_ @_ "
                    End With
                Case enumFormatoExcel.ESTILONNORMALTEXTOAZUL
                    With eStilo
                        .Font.FontName = "Tahoma"
                        .Font.FontSize = 10
                        .Font.Bold = True
                        .Font.Underline = XLFontUnderlineValues.None
                        .Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        .Font.FontColor = XLColor.White
                        .Fill.SetBackgroundColor(XLColor.Blue)
                        .Border.InsideBorder = XLBorderStyleValues.Thin
                        .Border.InsideBorderColor = XLColor.White
                    End With
            End Select

            Return eStilo
        Catch ex As Exception
        End Try
    End Function

#End Region

    Public Shared Sub loguearErroresInsercionInterface(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresInsercionInterface" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearErrores(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresGenericos" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearProcesoWindows(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "PROCESOWINDOWS" & Format(Today, "MMyyyy") & ".XLS", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearProcesoVersion(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "VERSION" & Format(Today, "MMyyyy") & ".XLS", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearReportesOnline(ByVal eString As String)
        Dim iArchivoPremiosOtorgados As StreamWriter

        Try
            iArchivoPremiosOtorgados = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ReportesOnline" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPremiosOtorgados.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eString)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPremiosOtorgados) Then iArchivoPremiosOtorgados.Close()
            iArchivoPremiosOtorgados = Nothing
        End Try
    End Sub

    Public Shared Sub loguearProcesoRendicionRapiPago(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "rapipago" & Format(Today, "MMyyyy") & ".XLS", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearInterfaceNovedades(ByVal eMensaje As String, ByVal eNovedad As String)
        Dim iArchivoInterfaceNovedades As StreamWriter

        Try
            iArchivoInterfaceNovedades = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogueoInterfaceNovedades" & eNovedad & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoInterfaceNovedades.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoInterfaceNovedades) Then iArchivoInterfaceNovedades.Close()
            iArchivoInterfaceNovedades = Nothing
        End Try
    End Sub
    Public Shared Function validarColumnasExcel(eListaColumnas As List(Of String), eDataTable As DataTable) As String
        Dim iListaColumnasMalEscrita As New List(Of String)
        Dim iColumnaEncontrada As List(Of String)
        Dim iMensajeError As String = ""
        Dim i As Integer

        Try

            'Recorro las columnas de la tabla obtenida para validar las misma 
            For i = 0 To eDataTable.Columns.Count - 1
                iColumnaEncontrada = eListaColumnas.FindAll(Function(columna) columna = eDataTable.Columns(i).ColumnName.ToString)
                If Not IsNothing(iColumnaEncontrada) AndAlso iColumnaEncontrada.Count > 0 Then
                    eListaColumnas.Remove(eDataTable.Columns(i).ColumnName.ToString)
                Else
                    iListaColumnasMalEscrita.Add(eDataTable.Columns(i).ColumnName.ToString)
                End If
            Next

            'Verifico si falto agregar una columna al excel
            If Not IsNothing(eListaColumnas) AndAlso eListaColumnas.Count > 0 Then
                iMensajeError = "FORMATO DE ARCHIVO MAL ARMADO, FALTA" & IIf(eListaColumnas.Count > 1, "N", "") & " LA" & IIf(eListaColumnas.Count > 1, "S", "") & " SIGUIENTE" & IIf(eListaColumnas.Count > 1, "S", "") & " COLUMNA" & IIf(eListaColumnas.Count > 1, "S", "") & ":" & vbNewLine
                For i = 0 To eListaColumnas.Count - 1
                    iMensajeError &= eListaColumnas(i) & " | "
                Next
                iMensajeError = Left(iMensajeError, iMensajeError.Length - 3)
                iMensajeError &= vbNewLine
            End If

            'Verifico si una de las columnas fue mas escrita
            If iListaColumnasMalEscrita.Count > 0 Then
                iMensajeError &= "FORMATO DE ARCHIVO MAL ARMADO, ESTA" & IIf(iListaColumnasMalEscrita.Count > 1, "N", "") & " MAL ESCRITA" & IIf(iListaColumnasMalEscrita.Count > 1, "S", "") & " LAS SIGUIENTE" & IIf(iListaColumnasMalEscrita.Count > 1, "S", "") & " COLUMNA" & IIf(iListaColumnasMalEscrita.Count > 1, "S", "") & ":" & vbNewLine

                For i = 0 To iListaColumnasMalEscrita.Count - 1
                    iMensajeError &= iListaColumnasMalEscrita(i) & " | "
                Next
                iMensajeError = Left(iMensajeError, iMensajeError.Length - 3)
                iMensajeError &= vbNewLine
            End If

            Return iMensajeError

        Catch exception As Exception
            Return iMensajeError
        Finally
            iListaColumnasMalEscrita = Nothing
            iColumnaEncontrada = Nothing
        End Try
    End Function

    Public Shared Sub loguearBind(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogBind" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearRenaper(ByVal eDato As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogRenaper" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eDato)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearLinkCorto(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogLinkCorto" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearClear(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogClear" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearCrediteck(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogCrediteck" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearEnaCom(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogEnaCom" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearCoelsaSantander(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogCoelsaSantander" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearProcesoCobranzas(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogProcesoCobranzas" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearItau(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "loguearItau" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearEnvioCobranza(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "loguearEnvioCobranza" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearFacephiSantander(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogFacephiSantander" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearMotorSiisa(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogMotorSiisa" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearBaseIN00(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogBaseIN00" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearTransferenciaPioneer(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogTransferenciaPioneer" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Sub loguearSignatura(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogSignatura" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub
    Public Shared Function formatoNombreSinComa(ByVal eNombre As String, eValor As String) As String
        Dim ePosicionComa As String

        ePosicionComa = InStr(eNombre, ",")

        If ePosicionComa <> 0 Then
            eNombre = Replace(eNombre, eValor, " ", , 1)
            Return eNombre.ToString
        Else
            Return eNombre.ToString
        End If
    End Function

    Public Shared Function copiarDataRow(ByRef eDataSet As DataSet, eNombreTabla As String, eDataRow() As DataRow)

        For Each iDataRow As DataRow In eDataRow
            Dim iDataRowNuevo As DataRow
            iDataRowNuevo = eDataSet.Tables(eNombreTabla).NewRow
            For i As Integer = 0 To eDataSet.Tables(eNombreTabla).Columns.Count - 1
                iDataRowNuevo(i) = iDataRow(i)
            Next
            eDataSet.Tables(eNombreTabla).Rows.Add(iDataRowNuevo)
        Next

    End Function

    Public Shared Sub loguearNosisValidadorIdentidad(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "NosisValidadorIdentidad" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub armarCSV(ByVal eDataTable As DataTable, ByVal ePathArchivo As String, Optional eAgregarNombreColumnas As Boolean = False, Optional eSeparador As String = ";")
        Dim iArchivo As StreamWriter
        Dim iLinea As String

        Try

            iArchivo = New StreamWriter(ePathArchivo, False, System.Text.Encoding.GetEncoding(1252))

            If eAgregarNombreColumnas Then
                For Each iColumna As DataColumn In eDataTable.Columns
                    iLinea &= iColumna.ColumnName & eSeparador
                Next
                iArchivo.WriteLine(iLinea)
            End If

            For Each iDataRow As DataRow In eDataTable.Rows
                iLinea = ""
                For Each iColumna As DataColumn In eDataTable.Columns
                    iLinea &= iDataRow.Item(iColumna.ColumnName).ToString.Replace(vbNewLine, " ").Replace(vbTab, " ").Replace(vbCr, "").Replace(vbLf, "").Replace(";", " ") & eSeparador
                Next
                iArchivo.WriteLine(iLinea)
            Next

        Catch ex As Exception
            Throw New Exception
        Finally
            If Not IsNothing(iArchivo) Then iArchivo.Close()
            iArchivo = Nothing
        End Try

    End Sub

    Public Shared Sub armarCSVLoad(ByVal eDataTable As DataTable, ByVal ePathArchivo As String, Optional eAgregarNombreColumnas As Boolean = False, Optional eSeparador As String = ";")
        Dim iArchivo As StreamWriter
        Dim iLinea As String

        Try

            iArchivo = New StreamWriter(ePathArchivo, False, System.Text.Encoding.GetEncoding(1252))

            If eAgregarNombreColumnas Then
                For Each iColumna As DataColumn In eDataTable.Columns
                    iLinea &= iColumna.ColumnName & eSeparador
                Next
                iArchivo.WriteLine(iLinea)
            End If

            For Each iDataRow As DataRow In eDataTable.Rows
                iLinea = ""
                For Each iColumna As DataColumn In eDataTable.Columns
                    iLinea &= FuncionComun.nuloSiEsVacio(iDataRow.Item(iColumna.ColumnName).ToString.Replace(vbNewLine, " ").Replace(vbTab, " ").Replace(vbCr, "").Replace(vbLf, "").Replace(";", " ")) & eSeparador
                Next
                'If Len(iLinea) > 2 Then
                '    iLinea = Left(iLinea, iLinea.Length - 2)
                'End If
                iArchivo.WriteLine(iLinea)
            Next

        Catch ex As Exception
            Throw New Exception
        Finally
            If Not IsNothing(iArchivo) Then iArchivo.Close()
            iArchivo = Nothing
        End Try

    End Sub
    Public Shared Function CSVaDataTable(ByVal ePathArchvivo As String, ByVal eNombreTabla As String, ByVal eSeparador As String, Optional eSoloNombresColumnas As Boolean = False) As DataTable
        Dim myTable As DataTable = New DataTable(eNombreTabla)
        Dim i As Integer
        Dim myRow As DataRow
        Dim myColumn As DataColumn
        Dim MyType As String
        Dim fieldValues As String()

        Dim ColumnNames As String()
        Dim iCantidadColumnas As Integer
        Dim myReader As New StreamReader(ePathArchvivo, System.Text.Encoding.GetEncoding(1252))
        Dim iLinea As String
        Try

            'Open file and read first two lines
            ColumnNames = myReader.ReadLine().Split(eSeparador)
            If ColumnNames.Length() <= 1 Then Throw New Exception()

            'Create data columns named according to first line of data, with type according to second line
            For i = 0 To ColumnNames.Length() - 1
                myColumn = New DataColumn()
                myColumn.ColumnName = ColumnNames(i)
                myTable.Columns.Add(myColumn)
            Next
            iCantidadColumnas = ColumnNames.Length()

            If Not eSoloNombresColumnas Then
                'Read the body of the data to data table
                While myReader.Peek() <> -1
                    Try
                        iLinea = myReader.ReadLine()
                        fieldValues = iLinea.Split(eSeparador)
                        myRow = myTable.NewRow
                        For i = 0 To iCantidadColumnas - 1
                            myRow.Item(i) = fieldValues(i).ToString
                        Next
                        myTable.Rows.Add(myRow)
                    Catch ex As Exception
                        FuncionComun.loguearErrorInterface("ERROR AL CONVERTIR CSV: " & FuncionComun.obtenerMotivoOriginal(ex) & "-" & fieldValues.ToString, ePathArchvivo)
                    End Try
                End While
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            myReader.Close()
        End Try
        Return myTable
    End Function

    Public Shared Sub loguearErrorInterface(ByVal eResultado As String, ByVal eUsuario As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "Interfaces" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - " & eResultado)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearResultadoMoraCliente(ByVal eResultado As String, ByVal eUsuario As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ResultadoMoraCliente" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - " & eResultado)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearResultadoEquivalenteScore(ByVal eResultado As String, ByVal eUsuario As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ResultadoEquivalenteScore" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - " & eResultado)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub


    Public Shared Sub loguearBOT(ByVal eResultado As String, ByVal eUsuario As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "BOT" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - " & eResultado)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Function ReemplazarCaracteresEspecialesMaps(iPalabra As String) As String
        Try

            'Minusculas
            iPalabra = iPalabra.Replace("ã", "a")
            iPalabra = iPalabra.Replace("é", "e")
            iPalabra = iPalabra.Replace("í", "i")
            iPalabra = iPalabra.Replace("õ", "o")
            iPalabra = iPalabra.Replace("ú", "u")
            'MAYUSCULA
            iPalabra = iPalabra.Replace("Ã­", "A")
            iPalabra = iPalabra.Replace("É", "E")
            iPalabra = iPalabra.Replace("Í", "I")
            iPalabra = iPalabra.Replace("Õ", "O")
            iPalabra = iPalabra.Replace("Ú", "U")

            iPalabra = Replace(iPalabra, "\$%.*?%\$", "", System.Text.RegularExpressions.RegexOptions.Compiled)
            Return iPalabra



        Catch exception As Exception
            Throw exception
        Finally

        End Try
    End Function

    Public Shared Sub loguearRecepcionSMS(ByVal eResultado As String, ByVal eUsuario As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "RecepcionSMS" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eUsuario & " - " & eResultado)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearBot(ByVal eMensaje As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogBot" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eMensaje)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearSiscardApi(ByVal ePagina As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "SiscardApi" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearBCU(ByVal ePagina As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "BCU" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearRondanet(ByVal ePagina As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "Rondanet" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & ePagina)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Sub loguearFacturacionQR(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "LogFacturacionQR" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

    Public Shared Function sacarPuntos(ByVal eTexto As String) As String
        If eTexto <> Nothing Then
            Return eTexto.Replace(".", "")
        End If

        Return eTexto
    End Function

    Public Shared Function validarPassword(ByVal ePassword As String, Optional ByVal eMinimoLongitud As Integer = 8, Optional ByVal eNumeroMayusculas As Integer = 2, Optional ByVal eNumeroMinuscula As Integer = 2, Optional ByVal eCantidadNumeros As Integer = 2, Optional ByVal eCantidadEspeciales As Integer = 2) As Boolean

        ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        Dim iMayusculas As New System.Text.RegularExpressions.Regex("[A-Z]")
        Dim iMinusculas As New System.Text.RegularExpressions.Regex("[a-z]")
        Dim iNumeros As New System.Text.RegularExpressions.Regex("[0-9]")

        ' Special is "none of the above".
        Dim iEspeciales As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")

        ' Check the length.
        If Len(ePassword) < eMinimoLongitud Then Return False
        ' Check for minimum number of occurrences.
        If iMayusculas.Matches(ePassword).Count < eNumeroMayusculas Then Return False
        If iMinusculas.Matches(ePassword).Count < eNumeroMinuscula Then Return False
        If iNumeros.Matches(ePassword).Count < eCantidadNumeros Then Return False
        If iEspeciales.Matches(ePassword).Count < eCantidadEspeciales Then Return False

        ' Passed all checks.
        Return True

    End Function

    Public Shared Function rotarImagenAutomaticamente(sImageFilePath As String) As Boolean
        Dim rft As RotateFlipType = RotateFlipType.RotateNoneFlipNone
        Dim img As Bitmap = Drawing.Image.FromFile(sImageFilePath)
        Dim properties As PropertyItem() = img.PropertyItems
        Dim bReturn As Boolean = False
        For Each p As PropertyItem In properties
            If p.Id = 274 Then
                Dim orientation As Short = BitConverter.ToInt16(p.Value, 0)
                Select Case orientation
                    Case 1
                        rft = RotateFlipType.RotateNoneFlipNone
                    Case 3
                        rft = RotateFlipType.Rotate180FlipNone
                    Case 6
                        rft = RotateFlipType.Rotate90FlipNone
                    Case 8
                        rft = RotateFlipType.Rotate270FlipNone
                End Select
            End If
        Next
        If rft <> RotateFlipType.RotateNoneFlipNone Then
            img.RotateFlip(rft)
            System.IO.File.Delete(sImageFilePath)
            img.Save(sImageFilePath, System.Drawing.Imaging.ImageFormat.Jpeg)
            bReturn = True
        End If
        Return bReturn

    End Function

    Public Shared Function CodificarToken(ByVal eMensaje As String, ByVal eSecret As String) As String
        Dim encoding = New System.Text.ASCIIEncoding()
        Dim keyByte As Byte() = encoding.GetBytes(eSecret)
        Dim messageBytes As Byte() = encoding.GetBytes(eMensaje)

        Using hmacsha256 = New HMACSHA256(keyByte)
            Dim hashmessage As Byte() = hmacsha256.ComputeHash(messageBytes)
            Return Convert.ToBase64String(hashmessage)
        End Using
    End Function

    Public Shared Function contenidoEntreCaracteres(ByVal eValor As String, ByVal ePrimerCaracter As String, ByVal eSegundoCaracter As String) As String
        Dim iExpresion, iContenido As String
        Try
            iExpresion = "(?<=\" & ePrimerCaracter & ")(.*?)(?=\" & eSegundoCaracter & ")"

            Dim iRegex As New System.Text.RegularExpressions.Regex(iExpresion)

            iContenido = iRegex.Match(eValor).ToString

            Return iContenido

        Catch ex As Exception
            Return eValor
        End Try
    End Function

    Public Shared Function obtenerNombreArchivoCobranzaInterface(ByVal eNombre As String) As String
        Dim iFechaObtenida As String
        Dim iTipo As String
        Dim iNombreObtenido As String
        Try

            If eNombre.Contains("<FECHA") Then
                iTipo = contenidoEntreCaracteres(eNombre, "<", ">")
                iFechaObtenida = FuncionComun.convertirFechaInterfacesInsercion(Today, contenidoEntreCaracteres(eNombre, "[", "]"))
                iNombreObtenido = eNombre.Replace("<" & iTipo & ">", iFechaObtenida)
            Else
                iNombreObtenido = eNombre
            End If

        Catch ex As Exception
            Return iNombreObtenido
        End Try
    End Function

    Public Shared Function cambiarGuionBajoPorEspacio(eString As String) As String
        Return eString.Replace("_", " ")
    End Function
    Public Shared Function obtenerSignoMoneda() As String
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                Return "$"
            Case "URUGUAY"
                Return "$"
            Case "PARAGUAY"
                Return "Gs"
            Case "COLOMBIA"
                Return "$"
        End Select

        Return ""

    End Function

    Public Shared Function obtenerCodigoMoneda() As String
        Try
            Select Case ConfigurationManager.AppSettings("Region")
                Case "URUGUAY"
                    Return "UYU"
                Case "PARAGUAY"
                    Return "PYG"
                Case "COLOMBIA"
                    Return "COP"
                Case Else ' INCLUYE ARGENTINA
                    Return "ARS"
            End Select
        Catch exception As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Sub ordenarListbox(ByRef eItems As ListItemCollection)
        Dim iItemsArray As ListItem()

        Try
            iItemsArray = New ListItem(eItems.Count - 1) {}
            eItems.CopyTo(iItemsArray, 0)
            Array.Sort(iItemsArray, Function(x, y) (String.Compare(x.Text, y.Text, StringComparison.CurrentCulture)))
            eItems.Clear()
            eItems.AddRange(iItemsArray)

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function generarQR(ByVal eTexto As String) As String

        Return "https://chart.apis.google.com/chart?cht=qr&chs=200x200&chl=" & eTexto & "&chld=H|1"

    End Function

    Public Shared Function encodeStringABase64(eTexto As String) As String
        Dim iByte As Byte() = System.Text.Encoding.UTF8.GetBytes(eTexto)
        Dim iBase64 As String = Convert.ToBase64String(iByte)
        Return iBase64
    End Function

    Public Shared Function stringAGuid(ByVal eTexto As String) As Guid
        Dim paramGuid As Guid = Guid.Empty
        Guid.TryParse(eTexto, paramGuid)
        Return paramGuid
    End Function

    Public Shared Function generarId() As String
        Dim dt As Date = Date.Now
        Return dt.ToString("yyMMddHHmmssfff")
    End Function
#End Region

End Class