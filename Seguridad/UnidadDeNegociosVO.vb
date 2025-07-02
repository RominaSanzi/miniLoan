
Public Class UnidadDeNegociosVO

#Region "Atributos"
    Private iUnidadDeNegocios As unidadDeNegocios
    Private iCasaCentral As Object
    Private iNumeroCredito As Object
    Private iNumeroRecibo As Object
    Private iTipoCalculoPunitorios As Object
    Private iPorcentajeSobreTasa As Object
    Private iMontoMaximoPunitorios As Object
    Private iTipoInteresFrances As Object
    Private iTipoCapitalSolicitado As Object
    Private iSociedad As Object
    Private iSociedadNumeroCuit As Object
    Private iProcesadora As Object
    Private iProcesadoraNumeroCuit As Object
    Private iImpresionConvenio As Object
    Private iImpresionSolicitud As Object
    Private iImpresionRecibo As Object
    Private iImpresionRecordatorioVencimiento As Object
    Private iOperacionRealizadaSolicitud As Object
    Private iTipoimpresionchequeras As Object
    Private iTipoimpresionComprobante As Object
    Private iMostrarLiquidacion100Pesos As Object
    Private iNombreFantasia As Object
    Private iNombreFantasia2 As Object
    Private iFactuaConsumidorFinal As Object
    Private iTelefonosCartas As Object
    Private iHorarioCartas As Object
    Private iAutorizacionEspecialGeneraTramite As Object
    Private iAutorizacionEspecialTarea As Object
    Private iCertificadoCancelacionLugar As Object
    Private iCertificadoCancelacionEntidad As Object
    Private iCertificadoCancelacionFirmante As Object
    Private iSolicitudCambioVencimientoGeneraTramite As Object
    Private iSolicitudCambioVencimientoTarea As Object
    Private iGastosCobranza As Object
    Private iDiasAplicaGastosCobranza As Object
    Private iCantidadCuotasPreCancelacion As Object
    Private iCantidadDiasVencimientoPreCancelacion As Object
    Private iDiaCortePreCancelacion As Object
    Private iTipoCancelacion As Object
	Private iPorcentajeGastosCancelacionAnticipada As Object
    Private iGastosCobranza2 As Object
    Private iDiasAplicaGastosCobranza2 As Object
    Private iPorcentajeDiferenciaCancelacion As Object
    Private iPorcentajeComisionPrecancelacion As Object
    Private iPuntoVentaDGICancelacion As Object
    Private iTYCFront As Object
    Private iCuerpoMailBienvenida As Object
    Private iAsuntoMailBienvenida As Object
    Private iCalculaPunitoriosSabados As Object
    Private iCuerpoMailRechazo As Object
    Private iAsuntoMailRechazo As Object
#End Region

#Region "Propiedades"
    Public Property unidadDeNegocios() As UnidadDeNegocios
        Get
            Return iUnidadDeNegocios
        End Get
        Set(ByVal Value As unidadDeNegocios)
            iUnidadDeNegocios = Value
        End Set
    End Property
    Public Property casaCentral() As Object
        Get
            Return iCasaCentral
        End Get
        Set(ByVal Value As Object)
            iCasaCentral = Value
        End Set
    End Property
    Public Property tipoCalculoPunitorios As Object
        Get
            Return iTipoCalculoPunitorios
        End Get
        Set(value As Object)
            iTipoCalculoPunitorios = value
        End Set
    End Property
    Public Property porcentajeSobreTasa As Object
        Get
            Return iPorcentajeSobreTasa
        End Get
        Set(value As Object)
            iPorcentajeSobreTasa = value
        End Set
    End Property
    Public Property montoMaximoPunitorios As Object
        Get
            Return iMontoMaximoPunitorios
        End Get
        Set(value As Object)
            iMontoMaximoPunitorios = value
        End Set
    End Property
    Public Property tipoInteresFrances As Object
        Get
            Return iTipoInteresFrances
        End Get
        Set(value As Object)
            itipoInteresFrances = value
        End Set
    End Property
    Public Property numeroCredito As Object
        Get
            Return iNumeroCredito
        End Get
        Set(value As Object)
            iNumeroCredito = value
        End Set
    End Property
    Public Property numeroRecibo As Object
        Get
            Return iNumeroRecibo
        End Get
        Set(value As Object)
            iNumeroRecibo = value
        End Set
    End Property
    Public Property tipoCapitalSolicitado As Object
        Get
            Return iTipoCapitalSolicitado
        End Get
        Set(value As Object)
            iTipoCapitalSolicitado = value
        End Set
    End Property
    Public Property sociedad As Object
        Get
            Return iSociedad
        End Get
        Set(value As Object)
            iSociedad = value
        End Set
    End Property
    Public Property sociedadNumeroCuit As Object
        Get
            Return iSociedadNumeroCuit
        End Get
        Set(value As Object)
            iSociedadNumeroCuit = value
        End Set
    End Property
    Public Property procesadora As Object
        Get
            Return iProcesadora
        End Get
        Set(value As Object)
            iProcesadora = value
        End Set
    End Property
    Public Property procesadoraNumeroCuit As Object
        Get
            Return iProcesadoraNumeroCuit
        End Get
        Set(value As Object)
            iProcesadoraNumeroCuit = value
        End Set
    End Property
    Public Property impresionConvenio As Object
        Get
            Return iImpresionConvenio
        End Get
        Set(ByVal value As Object)
            iImpresionConvenio = value
        End Set
    End Property
    Public Property impresionSolicitud As Object
        Get
            Return iImpresionSolicitud
        End Get
        Set(ByVal value As Object)
            iImpresionSolicitud = value
        End Set
    End Property
    Public Property impresionRecibo As Object
        Get
            Return iImpresionRecibo
        End Get
        Set(value As Object)
            iImpresionRecibo = value
        End Set
    End Property
    Public Property impresionRecordatorioVencimiento As Object
        Get
            Return iImpresionRecordatorioVencimiento
        End Get
        Set(value As Object)
            iImpresionRecordatorioVencimiento = value
        End Set
    End Property
    Public Property operacionRealizadaSolicitud As Object
        Get
            Return iOperacionRealizadaSolicitud
        End Get
        Set(value As Object)
            iOperacionRealizadaSolicitud = value
        End Set
    End Property
    Public Property tipoimpresionchequeras As Object
        Get
            Return iTipoimpresionchequeras
        End Get
        Set(value As Object)
            iTipoimpresionchequeras = value
        End Set
    End Property
    Public Property tipoimpresionComprobante As Object
        Get
            Return iTipoimpresionComprobante
        End Get
        Set(value As Object)
            iTipoimpresionComprobante = value
        End Set
    End Property
    Public Property mostrarLiquidacion100Pesos As Object
        Get
            Return iMostrarLiquidacion100Pesos
        End Get
        Set(value As Object)
            iMostrarLiquidacion100Pesos = value
        End Set
    End Property
    Public Property nombreFantasia As Object
        Get
            Return iNombreFantasia
        End Get
        Set(value As Object)
            iNombreFantasia = value
        End Set
    End Property
    Public Property nombreFantasia2 As Object
        Get
            Return iNombreFantasia2
        End Get
        Set(ByVal value As Object)
            iNombreFantasia2 = value
        End Set
    End Property
    Public Property factuaConsumidorFinal As Object
        Get
            Return iFactuaConsumidorFinal
        End Get
        Set(value As Object)
            iFactuaConsumidorFinal = value
        End Set
    End Property
    Public Property telefonosCartas As Object
        Get
            Return iTelefonosCartas
        End Get
        Set(ByVal value As Object)
            iTelefonosCartas = value
        End Set
    End Property
    Public Property horarioCartas As Object
        Get
            Return iHorarioCartas
        End Get
        Set(ByVal value As Object)
            iHorarioCartas = value
        End Set
    End Property
    Public Property autorizacionEspecialGeneraTramite As Object
        Get
            Return iAutorizacionEspecialGeneraTramite
        End Get
        Set(ByVal value As Object)
            iAutorizacionEspecialGeneraTramite = value
        End Set
    End Property
    Public Property autorizacionEspecialTarea As Object
        Get
            Return iAutorizacionEspecialTarea
        End Get
        Set(ByVal value As Object)
            iAutorizacionEspecialTarea = value
        End Set
    End Property
    Public Property certificadoCancelacionLugar As Object
        Get
            Return iCertificadoCancelacionLugar
        End Get
        Set(ByVal value As Object)
            iCertificadoCancelacionLugar = value
        End Set
    End Property
    Public Property certificadoCancelacionEntidad As Object
        Get
            Return iCertificadoCancelacionEntidad
        End Get
        Set(ByVal value As Object)
            iCertificadoCancelacionEntidad = value
        End Set
    End Property
    Public Property certificadoCancelacionFirmante As Object
        Get
            Return iCertificadoCancelacionFirmante
        End Get
        Set(ByVal value As Object)
            iCertificadoCancelacionFirmante = value
        End Set
    End Property
    Public Property solicitudCambioVencimientoGeneraTramite As Object
        Get
            Return iSolicitudCambioVencimientoGeneraTramite
        End Get
        Set(ByVal value As Object)
            iSolicitudCambioVencimientoGeneraTramite = value
        End Set
    End Property
    Public Property solicitudCambioVencimientoTarea As Object
        Get
            Return iSolicitudCambioVencimientoTarea
        End Get
        Set(ByVal value As Object)
            iSolicitudCambioVencimientoTarea = value
        End Set
    End Property
    Public Property gastosCobranza As Object
        Get
            Return iGastosCobranza
        End Get
        Set(ByVal value As Object)
            iGastosCobranza = value
        End Set
    End Property
    Public Property diasAplicaGastosCobranza As Object
        Get
            Return iDiasAplicaGastosCobranza
        End Get
        Set(ByVal value As Object)
            iDiasAplicaGastosCobranza = value
        End Set
    End Property
    Public Property cantidadCuotasPreCancelacion As Object
        Get
            Return iCantidadCuotasPreCancelacion
        End Get
        Set(ByVal value As Object)
            iCantidadCuotasPreCancelacion = value
        End Set
    End Property
    Public Property cantidadDiasVencimientoPreCancelacion As Object
        Get
            Return iCantidadDiasVencimientoPreCancelacion
        End Get
        Set(ByVal value As Object)
            iCantidadDiasVencimientoPreCancelacion = value
        End Set
    End Property
    Public Property diaCortePreCancelacion As Object
        Get
            Return iDiaCortePreCancelacion
        End Get
        Set(ByVal value As Object)
            iDiaCortePreCancelacion = value
        End Set
    End Property
    Public Property tipoCancelacion As Object
        Get
            Return iTipoCancelacion
        End Get
        Set(ByVal value As Object)
            iTipoCancelacion = value
        End Set
    End Property
    Public Property gastosCobranza2 As Object
        Get
            Return iGastosCobranza2
        End Get
        Set(ByVal value As Object)
            iGastosCobranza2 = value
        End Set
    End Property
    Public Property diasAplicaGastosCobranza2 As Object
        Get
            Return iDiasAplicaGastosCobranza2
        End Get
        Set(ByVal value As Object)
            iDiasAplicaGastosCobranza2 = value
        End Set
    End Property
    Public Property porcentajeGastosCancelacionAnticipada As Object
        Get
            Return iPorcentajeGastosCancelacionAnticipada
        End Get
        Set(ByVal value As Object)
            iPorcentajeGastosCancelacionAnticipada = value
        End Set
    End Property
  Public Property porcentajeDiferenciaCancelacion As Object
        Get
            Return iPorcentajeDiferenciaCancelacion
        End Get
        Set(ByVal value As Object)
            iPorcentajeDiferenciaCancelacion = value
        End Set
    End Property
    Public Property puntoVentaDGICancelacion As Object
        Get
            Return iPuntoVentaDGICancelacion
        End Get
        Set(ByVal value As Object)
            iPuntoVentaDGICancelacion = value
        End Set
    End Property

    Public Property TYCFront As Object
        Get
            Return iTYCFront
        End Get
        Set(value As Object)
            iTYCFront = value
        End Set
    End Property

    Public Property cuerpoMailBienvenida As Object
        Get
            Return iCuerpoMailBienvenida
        End Get
        Set(value As Object)
            iCuerpoMailBienvenida = value
        End Set
    End Property

    Public Property asuntoMailBienvenida As Object
        Get
            Return iAsuntoMailBienvenida
        End Get
        Set(value As Object)
            iAsuntoMailBienvenida = value
        End Set
    End Property

    Public Property calculaPunitoriosSabados As Object
        Get
            Return iCalculaPunitoriosSabados
        End Get
        Set(value As Object)
            iCalculaPunitoriosSabados = value
        End Set
    End Property

    Public Property cuerpoMailRechazo As Object
        Get
            Return iCuerpoMailRechazo
        End Get
        Set(value As Object)
            iCuerpoMailRechazo = value
        End Set
    End Property

    Public Property asuntoMailRechazo As Object
        Get
            Return iAsuntoMailRechazo
        End Get
        Set(value As Object)
            iAsuntoMailRechazo = value
        End Set
    End Property

    Public Property porcentajeComisionPrecancelacion As Object
        Get
            Return iPorcentajeComisionPrecancelacion
        End Get
        Set(value As Object)
            iPorcentajeComisionPrecancelacion = value
        End Set
    End Property
#End Region

End Class
