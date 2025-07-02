
Public Class EmpresaGrupoVO

#Region "Atributos"
    Private iEmpresaGrupo As EmpresaGrupo
    Private iNombreFantasia As Object
    Private iNumeroCredito As Object
    Private iNumeroRecibo As Object
    Private iFacturaElectronicamente As Object
    Private iFacturaElectronicaValidarVenta As Object
    Private iCantidadDiasVencimientoCertificado As Object
    Private iFacturacionElectronicaIdServicio As Object
    Private iFacturacionElectronicaUrlAutenticacion As Object
    Private iFacturacionElectronicaUrlServicio As Object
    Private iFacturacionElectronicaCuit As Object
    Private iFacturacionElectronicaPassword As Object
    Private iFacturacionElectronicaVencimientoCertificado As Object
    Private iFacturacionElectronicaNombreCertificado As Object
    Private ieMailProcesoAutomaticoFacturacion As Object
    Private iFacturacionElectronicaNombreTemplate As Object
    Private iFacturaElectronicaTipoDocumento As Object
    Private iMontoMinimoPercepcion As Object
    Private iMontoMinimoPercepcioncaba As Object
    Private iFacturacionPorCuotas As Object
    Private iFacturacionPorCuotasDiasProceso As Object
    Private iFacturacionPorCuotasPuntoVentaPrestamoFacA As Object
    Private iFacturacionPorCuotasPuntoVentaPrestamoFacB As Object
    Private iFacturacionPorCuotasPuntoVentaPrestamoNCA As Object
    Private iFacturacionPorCuotasPuntoVentaPrestamoNCB As Object
    Private iFacturacionPorCuotasPuntoVentaPrestamoNDA As Object
    Private iFacturacionPorCuotasPuntoVentaPrestamoNDB As Object
    Private iFacturacionPorCuotasPuntoVentaComercioFacA As Object
    Private iFacturacionPorCuotasPuntoVentaComercioFacB As Object
    Private iFacturacionPorCuotasPuntoVentaComercioNCA As Object
    Private iFacturacionPorCuotasPuntoVentaComercioNCB As Object
    Private iFacturacionPorCuotasPuntoVentaComercioNDA As Object
    Private iFacturacionPorCuotasPuntoVentaComercioNDB As Object
    Private iFacturacionPorCuotasPuntoVentaEspecialFacA As Object
    Private iFacturacionPorCuotasPuntoVentaEspecialFacB As Object
    Private iFacturacionPorCuotasPuntoVentaEspecialNCA As Object
    Private iFacturacionPorCuotasPuntoVentaEspecialNCb As Object
    Private iFacturacionPorCuotasPuntoVentaEspecialNDA As Object
    Private iFacturacionPorCuotasPuntoVentaEspecialNDB As Object
    Private iFacturacionPorCuotasComercioClienteEspecial As Object
    Private iFacturacionMasivaExcelPorCuotasPuntoVentaPrestamoFacA As Object
    Private iFacturacionMasivaExcelPorCuotasPuntoVentaPrestamoFacB As Object
    Private iFacturacionMasivaExcelPorCuotasPuntoVentaComercioFacA As Object
    Private iFacturacionMasivaExcelPorCuotasPuntoVentaComercioFacB As Object
#End Region

#Region "Propiedades"
    Public Property empresaGrupo() As empresaGrupo
        Get
            Return iEmpresaGrupo
        End Get
        Set(ByVal Value As empresaGrupo)
            iEmpresaGrupo = Value
        End Set
    End Property
    Public Property nombreFantasia() As Object
        Get
            Return iNombreFantasia
        End Get
        Set(ByVal Value As Object)
            iNombreFantasia = Value
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
    Public Property facturaElectronicamente As Object
        Get
            Return iFacturaElectronicamente
        End Get
        Set(value As Object)
            iFacturaElectronicamente = value
        End Set
    End Property
    Public Property facturaElectronicaValidarVenta As Object
        Get
            Return iFacturaElectronicaValidarVenta
        End Get
        Set(value As Object)
            iFacturaElectronicaValidarVenta = value
        End Set
    End Property
    Public Property cantidadDiasVencimientoCertificado As Object
        Get
            Return iCantidadDiasVencimientoCertificado
        End Get
        Set(value As Object)
            iCantidadDiasVencimientoCertificado = value
        End Set
    End Property
    Public Property cacturacionElectronicaIdServicio As Object
        Get
            Return iFacturacionElectronicaIdServicio
        End Get
        Set(value As Object)
            iFacturacionElectronicaIdServicio = value
        End Set
    End Property
    Public Property facturacionElectronicaUrlAutenticacion As Object
        Get
            Return iFacturacionElectronicaUrlAutenticacion
        End Get
        Set(value As Object)
            iFacturacionElectronicaUrlAutenticacion = value
        End Set
    End Property
    Public Property facturacionElectronicaUrlServicio As Object
        Get
            Return iFacturacionElectronicaUrlServicio
        End Get
        Set(value As Object)
            iFacturacionElectronicaUrlServicio = value
        End Set
    End Property
    Public Property facturacionElectronicaCuit As Object
        Get
            Return iFacturacionElectronicaCuit
        End Get
        Set(value As Object)
            iFacturacionElectronicaCuit = value
        End Set
    End Property
    Public Property facturacionElectronicaPassword As Object
        Get
            Return iFacturacionElectronicaPassword
        End Get
        Set(value As Object)
            iFacturacionElectronicaPassword = value
        End Set
    End Property
    Public Property facturacionElectronicaVencimientoCertificado As Object
        Get
            Return iFacturacionElectronicaVencimientoCertificado
        End Get
        Set(value As Object)
            iFacturacionElectronicaVencimientoCertificado = value
        End Set
    End Property
    Public Property facturacionElectronicaNombreCertificado As Object
        Get
            Return iFacturacionElectronicaNombreCertificado
        End Get
        Set(value As Object)
            iFacturacionElectronicaNombreCertificado = value
        End Set
    End Property
    Public Property eMailProcesoAutomaticoFacturacion As Object
        Get
            Return ieMailProcesoAutomaticoFacturacion
        End Get
        Set(value As Object)
            ieMailProcesoAutomaticoFacturacion = value
        End Set
    End Property
    Public Property facturacionElectronicaNombreTemplate As Object
        Get
            Return iFacturacionElectronicaNombreTemplate
        End Get
        Set(value As Object)
            iFacturacionElectronicaNombreTemplate = value
        End Set
    End Property
    Public Property facturaElectronicaTipoDocumento As Object
        Get
            Return iFacturaElectronicaTipoDocumento
        End Get
        Set(value As Object)
            iFacturaElectronicaTipoDocumento = value
        End Set
    End Property
    Public Property montoMinimoPercepcion As Object
        Get
            Return iMontoMinimoPercepcion
        End Get
        Set(value As Object)
            iMontoMinimoPercepcion = value
        End Set
    End Property
    Public Property montoMinimoPercepcioncaba As Object
        Get
            Return iMontoMinimoPercepcioncaba
        End Get
        Set(value As Object)
            iMontoMinimoPercepcioncaba = value
        End Set
    End Property
    Public Property facturacionPorCuotas As Object
        Get
            Return iFacturacionPorCuotas
        End Get
        Set(value As Object)
            iFacturacionPorCuotas = value
        End Set
    End Property
    Public Property facturacionPorCuotasDiasProceso As Object
        Get
            Return iFacturacionPorCuotasDiasProceso
        End Get
        Set(value As Object)
            iFacturacionPorCuotasDiasProceso = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaPrestamoFacA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaPrestamoFacA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaPrestamoFacA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaPrestamoFacB As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaPrestamoFacB
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaPrestamoFacB = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaPrestamoNCA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaPrestamoNCA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaPrestamoNCA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaPrestamoNCB As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaPrestamoNCB
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaPrestamoNCB = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaPrestamoNDA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaPrestamoNDA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaPrestamoNDA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaPrestamoNDB As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaPrestamoNDB
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaPrestamoNDB = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaComercioFacA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaComercioFacA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaComercioFacA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaComercioFacB As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaComercioFacB
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaComercioFacB = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaComercioNCA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaComercioNCA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaComercioNCA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaComercioNCB As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaComercioNCB
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaComercioNCB = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaComercioNDA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaComercioNDA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaComercioNDA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaComercioNDB As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaComercioNDB
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaComercioNDB = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaEspecialFacA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaEspecialFacA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaEspecialFacA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaEspecialFacB As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaEspecialFacB
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaEspecialFacB = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaEspecialNCA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaEspecialNCA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaEspecialNCA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaEspecialNCb As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaEspecialNCb
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaEspecialNCb = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaEspecialNDA As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaEspecialNDA
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaEspecialNDA = value
        End Set
    End Property
    Public Property facturacionPorCuotasPuntoVentaEspecialNDB As Object
        Get
            Return iFacturacionPorCuotasPuntoVentaEspecialNDB
        End Get
        Set(value As Object)
            iFacturacionPorCuotasPuntoVentaEspecialNDB = value
        End Set
    End Property
    Public Property facturacionPorCuotasComercioClienteEspecial As Object
        Get
            Return iFacturacionPorCuotasComercioClienteEspecial
        End Get
        Set(value As Object)
            iFacturacionPorCuotasComercioClienteEspecial = value
        End Set
    End Property
    Public Property facturacionMasivaExcelPorCuotasPuntoVentaPrestamoFacA As Object
        Get
            Return iFacturacionMasivaExcelPorCuotasPuntoVentaPrestamoFacA
        End Get
        Set(value As Object)
            iFacturacionMasivaExcelPorCuotasPuntoVentaPrestamoFacA = value
        End Set
    End Property
    Public Property facturacionMasivaExcelPorCuotasPuntoVentaPrestamoFacB As Object
        Get
            Return iFacturacionMasivaExcelPorCuotasPuntoVentaPrestamoFacB
        End Get
        Set(value As Object)
            iFacturacionMasivaExcelPorCuotasPuntoVentaPrestamoFacB = value
        End Set
    End Property
    Public Property facturacionMasivaExcelPorCuotasPuntoVentaComercioFacA As Object
        Get
            Return iFacturacionMasivaExcelPorCuotasPuntoVentaComercioFacA
        End Get
        Set(value As Object)
            iFacturacionMasivaExcelPorCuotasPuntoVentaComercioFacA = value
        End Set
    End Property
    Public Property facturacionMasivaExcelPorCuotasPuntoVentaComercioFacB As Object
        Get
            Return iFacturacionMasivaExcelPorCuotasPuntoVentaComercioFacB
        End Get
        Set(value As Object)
            iFacturacionMasivaExcelPorCuotasPuntoVentaComercioFacB = value
        End Set
    End Property
#End Region

End Class