Imports di.financiera.entidades
Imports di.financiera.reglasnegocios
Imports di.financiera.excepciones
Imports di.financiera.seguridad

Partial Class ComprobanteCompraAlta
    Inherits Pagina

#Region "Ayuda"
    Private Overloads Sub btnayuda_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAyuda.Click
        Master.ayuda(Request.UrlReferrer.AbsoluteUri.Split("/")(Request.UrlReferrer.AbsoluteUri.Split("/").Length - 1))
    End Sub
#End Region

#Region "Abrir"
    Private Sub abrirImpresionComprobante(ByVal eUrl As String)
        Dim abrirImpresionComprobante As String = "<SCRIPT language='javascript'> " & vbNewLine &
                "window.open('" & eUrl & " ','comprobante','dependent=yes,width=730,height=515,resizable=no,scrollbars=yes,status=yes');" & vbNewLine &
                "</SCRIPT>"
        RegisterStartupScript("abrirImpresionComprobante", abrirImpresionComprobante)
    End Sub
#End Region

#Region "Listas"
    Private Sub llenarListas()
        'Dim iAdministradorComprobantes As New AdministradorComprobantes

        Dim iAdministradorComunes As New AdministradorComunes
        'Dim iTipoCondicionCompra As New TipoCondicionCompra
        Dim iSortedList As SortedList
        Dim IDataReader As IDataReader

        Try

            'IDataReader = iAdministradorComunes.obtenerTipoCondicionCompras(iTipoCondicionCompra)
            'While IDataReader.Read
            '    lstCondicionCompra.Items.Add(New ListItem(IDataReader.Item("descripcion").ToString, IDataReader.Item("id").ToString))
            'End While
            'IDataReader.Close()
            'iTipoCondicionCompra.dispose()
            'iTipoCondicionCompra = Nothing

            lstTerminoPago.Items.Add(New ListItem("10", "10"))
            lstTerminoPago.Items.Add(New ListItem("15", "15"))
            lstTerminoPago.Items.Add(New ListItem("30", "30"))
            lstTerminoPago.Items.Add(New ListItem("60", "60"))
            lstTerminoPago.Items.Add(New ListItem("90", "90"))

            dtbFecha.Text = Format(Now, "dd/MM/yyyy")
            dtbFechaContable.Text = Format(Now, "dd/MM/yyyy")

            lstFormaPago.Items.Add(New ListItem("CAJA", ComprobanteCompra.enumFormaPago.CAJA))
            lstFormaPago.Items.Add(New ListItem("BANCO", ComprobanteCompra.enumFormaPago.BANCO))

        Catch ex As Exception
            If Not TypeOf (ex) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = ex
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
            If Not IsNothing(IDataReader) AndAlso Not IDataReader.IsClosed Then IDataReader.Close()
            IDataReader = Nothing
            'iAdministradorComprobantes = Nothing
            iAdministradorComunes = Nothing
            'iTipoCondicionCompra = Nothing
            iSortedList = Nothing
        End Try

    End Sub
#End Region

#Region "Botones"
    Private Sub btnAtras_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAtras.Click
        Response.Redirect(Session(Session.SessionID & "UltimaPagina"))
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        'Dim iAdministradorComprobantes As New AdministradorComprobantes
        ' Dim iAdministradorProveedores As New AdministradorProveedores
        'Dim iFactura As New ComprobanteCompra
        Dim iMontoIva, iNetoGrabado As Double

        Try

            iNetoGrabado = FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado105.Text) + FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado21.Text) + FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado27.Text)
            iMontoIva = FuncionComun.ceroSiEsVacio(Format(FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado105.Text) * 0.105, "0.00")) + FuncionComun.ceroSiEsVacio(Format(FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado21.Text) * 0.21, "0.00")) + FuncionComun.ceroSiEsVacio(Format(FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado27.Text) * 0.27, "0.00"))

            'With iFactura
            '    .proveedor = New Proveedor
            '    .proveedor = Session(Session.SessionID & "xProveedor")
            '    Select Case lstTipoFactura.SelectedValue
            '        Case TipoComprobante.FACTURAA
            '            .tipoComprobante = New FacturaA
            '        Case TipoComprobante.FACTURAB
            '            .tipoComprobante = New FacturaB
            '        Case TipoComprobante.FACTURAC
            '            .tipoComprobante = New FacturaC
            '        Case TipoComprobante.NOTADECREDITOA
            '            .tipoComprobante = New NotaDeCreditoA
            '        Case TipoComprobante.NOTADEDEBITOA
            '            .tipoComprobante = New NotaDeDebitoA
            '        Case TipoComprobante.NOTADECREDITOB
            '            .tipoComprobante = New NotaDeCreditoB
            '        Case TipoComprobante.NOTADEDEBITOB
            '            .tipoComprobante = New NotaDeDebitoB
            '        Case TipoComprobante.NOTADECREDITOC
            '            .tipoComprobante = New NotaDeCreditoC
            '        Case TipoComprobante.NOTADEDEBITOC
            '            .tipoComprobante = New NotaDeDebitoC
            '    End Select
            '    .numeroFactura = intNumeroFactura.Text
            '    .fecha = CDate(FuncionComun.nothingSiEsVacio(dtbFecha.Text))
            '    .fechaContable = CDate(FuncionComun.nothingSiEsVacio(dtbFechaContable.Text))
            '    .montoNetoGravado105 = FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado105.Text)
            '    .montoNetoGravado21 = FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado21.Text)
            '    .montoNetoGravado27 = FuncionComun.ceroSiEsVacio(dcbImporteNetoGravado27.Text)
            '    '.porcIVA = FuncionComun.ceroSiEsVacio(dcbProcIVA.Text)
            '    '.montoIVA = FuncionComun.ceroSiEsVacio(dcbImporteMontoIVA.Value)
            '    .montoIVA = iMontoIva
            '    .montoNoGravado = FuncionComun.ceroSiEsVacio(dcbMontoNoGravado.Text)
            '    .montoPercepIVA = FuncionComun.ceroSiEsVacio(dcbMontoPercepIVA.Text)
            '    .montoPercepIIBB = FuncionComun.ceroSiEsVacio(dcbMontoPercepIIBB.Text)
            '    .montoRetenSicreb = FuncionComun.ceroSiEsVacio(dcbMontoRetenSicreb.Text)
            '    .montoRetenIIBB = FuncionComun.ceroSiEsVacio(dcbMontoRetenIIBB.Text)
            '    .montoRetenIVA = FuncionComun.ceroSiEsVacio(dcbMontoRetenIVA.Text)
            '    .montoRetenSuss = FuncionComun.ceroSiEsVacio(dcbMontoRetenSuss.Text)
            '    .montoRetenGanancias = FuncionComun.ceroSiEsVacio(dcbMontoRetenGanancias.Text)
            '    '.montoTotal = FuncionComun.ceroSiEsVacio(dcbMontoTotal.Value) / 100
            '    .montoTotal = .montoNetoGravado105 + .montoNetoGravado21 + .montoNetoGravado27 + .montoIVA + .montoNoGravado + .montoPercepIVA + .montoPercepIIBB + .montoRetenSicreb + .montoRetenIIBB + .montoRetenIVA + .montoRetenSuss + .montoRetenGanancias
            '    .detalle = txtDetalle.Text
            '    .pagado = 0
            '    .tipoCondicionCompra = New TipoCondicionCompra
            '    .tipoCondicionCompra.id = lstCondicionCompra.SelectedValue
            '    .terminoDePago = FuncionComun.ceroSiEsVacio(lstTerminoPago.SelectedValue)
            '    If lstAsientosModelos.Items.Count > 0 AndAlso lstAsientosModelos.SelectedItem.Value > 0 Then
            '        .asientosModelos = New AsientosModelos
            '        .asientosModelos.id = lstAsientosModelos.SelectedItem.Value
            '    End If
            '    .formaPago = lstFormaPago.SelectedItem.Value
            'End With

            'iAdministradorComprobantes.crearComprobanteCompra(iFactura)

            'Session(Session.SessionID & "Mensaje") = "Comprobante de Proveedor Nro: " & iFactura.numeroFactura
            Master.modal("", "Comprobante", "Operacion realizada correctamente")

        Catch ex As Exception
            If Not TypeOf (ex) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = ex
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
            'iFactura = Nothing
            'iAdministradorComprobantes = Nothing
            'iAdministradorProveedores = Nothing
        End Try
    End Sub
#End Region

#Region "Proveedor"
    Protected Sub txtRazonSocial_TextChanged(sender As Object, e As EventArgs) Handles txtRazonSocial.TextChanged

        txtCodigoProveedor.Text = ""
        txtCUIT.Text = ""
        If txtRazonSocial.Text <> Nothing Then
            'obtenerProveedor()
            If txtRazonSocial.Text = Nothing Then SetFocus(txtRazonSocial)
        Else
            txtCodigoProveedor.Text = ""
            txtCUIT.Text = ""
            txtRazonSocial.Text = ""
            txtTipoIVA.Text = ""
            SetFocus(txtRazonSocial)
            Session.Remove(Session.SessionID & "xProveedor")
        End If
    End Sub
    Protected Sub txtCUIT_TextChanged(sender As Object, e As EventArgs) Handles txtCUIT.TextChanged
        txtCodigoProveedor.Text = ""
        txtRazonSocial.Text = ""
        If txtCUIT.Text <> Nothing Then
            'obtenerProveedor()
            If txtCUIT.Text = Nothing Then SetFocus(txtCUIT)
        Else
            txtCodigoProveedor.Text = ""
            txtCUIT.Text = ""
            txtRazonSocial.Text = ""
            txtTipoIVA.Text = ""
            SetFocus(txtCUIT)
            Session.Remove(Session.SessionID & "xProveedor")
        End If
    End Sub

    Protected Sub txtCodigoProveedor_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoProveedor.TextChanged
        txtRazonSocial.Text = ""
        txtCUIT.Text = ""
        If txtCodigoProveedor.Text <> Nothing Then
            'obtenerProveedor()
            If txtCodigoProveedor.Text = Nothing Then SetFocus(txtCodigoProveedor)
        Else
            txtCodigoProveedor.Text = ""
            txtCUIT.Text = ""
            txtRazonSocial.Text = ""
            txtTipoIVA.Text = ""
            SetFocus(txtCodigoProveedor)
            Session.Remove(Session.SessionID & "xProveedor")
        End If
    End Sub

    'Private Sub obtenerProveedor()
    '    'Dim iProveedor As New Proveedor
    '    'Dim iAdministradorProveedores As New AdministradorProveedores
    '    'Dim iAdministradorComprobantes As New AdministradorComprobantes
    '    'Dim iAsientosModelos As New AsientosModelos
    '    Dim iTipoComprobante As TipoComprobante
    '    Dim iSortedList As SortedList

    '    Try
    '        iProveedor.codigo = FuncionComun.ceroSiEsVacio(txtCodigoProveedor.Text)
    '        iProveedor.CUIT = txtCUIT.Text
    '        iProveedor.razonSocial = txtRazonSocial.Text
    '        iProveedor = iAdministradorProveedores.obtenerProveedor(iProveedor)
    '        txtCodigoProveedor.Text = iProveedor.codigo
    '        txtCUIT.Text = iProveedor.CUIT
    '        txtRazonSocial.Text = iProveedor.razonSocial
    '        txtTipoIVA.Text = iProveedor.tipoIva.descripcion

    '        If iProveedor.tipoIva.id = TipoIva.INSCRIPTO OrElse iProveedor.tipoIva.id = TipoIva.MONOTRIBUTISTA Then
    '            iSortedList = iAdministradorComprobantes.obtenerTiposComprobanteProveedorInscripto()
    '            For i = 0 To iSortedList.Count - 1
    '                lstTipoFactura.Items.Add(New ListItem(iSortedList.GetValueList.Item(i), iSortedList.GetKeyList.Item(i)))
    '            Next
    '        Else
    '            iSortedList = iAdministradorComprobantes.obtenerTiposComprobanteProveedorNoInscripto()
    '            For i = 0 To iSortedList.Count - 1
    '                lstTipoFactura.Items.Add(New ListItem(iSortedList.GetValueList.Item(i), iSortedList.GetKeyList.Item(i)))
    '            Next
    '        End If
    '        lstAsientosModelos.Items.Clear()
    '        If Not IsNothing(iProveedor.proveedorAsientoModelos) Then
    '            For i = 0 To iProveedor.proveedorAsientoModelos.Count - 1
    '                lstAsientosModelos.Items.Add(New ListItem(iProveedor.proveedorAsientoModelos(i).asientosModelos.descripcion, iProveedor.proveedorAsientoModelos(i).asientosModelos.id))
    '            Next
    '        End If
    '        If (lstTipoFactura.SelectedValue = TipoComprobante.FACTURAC OrElse lstTipoFactura.SelectedValue = TipoComprobante.NOTADEDEBITOC OrElse lstTipoFactura.SelectedValue = TipoComprobante.NOTADECREDITOC) Then
    '            controlesTipoComprobanteC()
    '        Else
    '            controles(lstTipoFactura.SelectedValue = TipoComprobante.FACTURAA OrElse lstTipoFactura.SelectedValue = TipoComprobante.NOTADEDEBITOA OrElse lstTipoFactura.SelectedValue = TipoComprobante.NOTADECREDITOA)
    '        End If

    '        Session(Session.SessionID & "xProveedor") = iProveedor
    '    Catch ex As Exception
    '        txtCodigoProveedor.Text = ""
    '        txtCUIT.Text = ""
    '        txtRazonSocial.Text = ""
    '        txtTipoIVA.Text = ""
    '    Finally
    '    End Try
    'End Sub

    Private Sub lstTipoFactura_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTipoFactura.SelectedIndexChanged
        If (lstTipoFactura.SelectedValue = TipoComprobante.FACTURAC OrElse lstTipoFactura.SelectedValue = TipoComprobante.NOTADEDEBITOC OrElse lstTipoFactura.SelectedValue = TipoComprobante.NOTADECREDITOC) Then
            controlesTipoComprobanteC()
        Else
            controles(lstTipoFactura.SelectedValue = TipoComprobante.FACTURAA OrElse lstTipoFactura.SelectedValue = TipoComprobante.NOTADEDEBITOA OrElse lstTipoFactura.SelectedValue = TipoComprobante.NOTADECREDITOA)
        End If
        SetFocus(dtbFechaContable)
    End Sub
    Private Sub controles(evalor As Boolean)
        dcbImporteNetoGravado105.Enabled = evalor
        dcbImporteNetoGravado21.Enabled = evalor
        dcbImporteNetoGravado27.Enabled = evalor
        'dcbImporteMontoIVA.Enabled = evalor
        dcbMontoNoGravado.Enabled = evalor
        dcbMontoPercepIVA.Enabled = evalor
        dcbMontoPercepIIBB.Enabled = evalor
        dcbMontoRetenSicreb.Enabled = evalor
        dcbMontoRetenIIBB.Enabled = evalor
        dcbMontoRetenIVA.Enabled = evalor
        dcbMontoRetenSuss.Enabled = evalor
        dcbMontoRetenGanancias.Enabled = evalor
        'dcbMontoTotal.ReadOnly = True
    End Sub
    Private Sub controlesTipoComprobanteC()
        dcbImporteNetoGravado105.Enabled = False
        dcbImporteNetoGravado21.Enabled = False
        dcbImporteNetoGravado27.Enabled = False
        dcbImporteMontoIVA.Enabled = False
        dcbMontoNoGravado.Enabled = True
        dcbMontoPercepIVA.Enabled = False
        dcbMontoPercepIIBB.Enabled = False
        dcbMontoRetenSicreb.Enabled = False
        dcbMontoRetenIIBB.Enabled = False
        dcbMontoRetenIVA.Enabled = False
        dcbMontoRetenSuss.Enabled = False
        dcbMontoRetenGanancias.Enabled = False
        'dcbMontoTotal.ReadOnly = True
    End Sub
#End Region

#Region "Paginas"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            llenarListas()
            SetFocus(txtCodigoProveedor)
        End If
    End Sub
#End Region

End Class
