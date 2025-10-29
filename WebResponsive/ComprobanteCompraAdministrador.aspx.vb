Imports di.financiera.entidades
Imports di.financiera.reglasnegocios
Imports di.financiera.excepciones
Imports di.financiera.seguridad

Partial Class ComprobanteCompraAdministrador
    Inherits Pagina






#Region "Ayuda"
    Private Overloads Sub btnayuda_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAyuda.Click
        Master.ayuda(Request.UrlReferrer.AbsoluteUri.Split("/")(Request.UrlReferrer.AbsoluteUri.Split("/").Length - 1))
    End Sub
#End Region

#Region "Administrador"
    Private Sub paginarGrilla(eGrilla As DataGrid, Optional eForzarBusqueda As Boolean = False)
        Dim iDataTable As DataTable
        Dim iRefrescarDatos As Boolean
        Dim iInicio As Boolean
        Dim iOrigen As String

        Try

            iOrigen = Me.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "")

            '=============================================================================================
            'VERIFICO SI PROVIENE DE UNA ACCION DEL ADMINISTRADOR
            '=============================================================================================
            If Not IsNothing(Session(Session.SessionID & "detalle" & iOrigen)) AndAlso CBool(Session(Session.SessionID & "detalle" & iOrigen)) Then
                iRefrescarDatos = False
            ElseIf Not IsNothing(Session(Session.SessionID & "modificar" & iOrigen)) AndAlso CBool(Session(Session.SessionID & "modificar" & iOrigen)) Then
                iRefrescarDatos = True
            ElseIf Not IsNothing(Session(Session.SessionID & "eliminar" & iOrigen)) AndAlso CBool(Session(Session.SessionID & "eliminar" & iOrigen)) Then
                iRefrescarDatos = False
            ElseIf Not IsNothing(Session(Session.SessionID & "alta" & iOrigen)) AndAlso CBool(Session(Session.SessionID & "alta" & iOrigen)) Then
                If Not IsNothing(Session(Session.SessionID & "DataTable" & iOrigen)) Then iRefrescarDatos = True
            Else
                iInicio = True
            End If

            '=============================================================================================
            'BORRO LAS SESSIONES ANTES QUE NADA
            '=============================================================================================
            Session.Remove(Session.SessionID & "detalle" & iOrigen)
            Session.Remove(Session.SessionID & "modificar" & iOrigen)
            Session.Remove(Session.SessionID & "eliminar" & iOrigen)
            Session.Remove(Session.SessionID & "alta" & iOrigen)

            '=============================================================================================
            'PRECARGO LA PANTALLA SI TENGO SESION DE ENTIDAD
            '=============================================================================================
            If Not IsNothing(Session(Session.SessionID & "entidad" & iOrigen)) AndAlso Not iInicio Then cargarPagina()

            '=============================================================================================
            'REALIZO LA CARGA DE LA GRILLA
            '=============================================================================================
            If iRefrescarDatos OrElse eForzarBusqueda Then
                iDataTable = obtenerOrigenDatos()
            Else
                If Not iInicio Then iDataTable = Session(Session.SessionID & "DataTable" & iOrigen)
            End If

            '=============================================================================================
            'LLENAMOS LA GRILLA
            '=============================================================================================
            eGrilla.DataSource = iDataTable
            eGrilla.DataBind()

            '=============================================================================================
            'GUARDAMOS LOS VALORES EN SESSION
            '=============================================================================================
            Session(Session.SessionID & "DataTable" & iOrigen) = iDataTable

        Catch exception As Exception
            If Not TypeOf (exception) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = exception
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
            iDataTable = Nothing
        End Try
    End Sub

    Private Sub cargarPagina()
        Dim iComprobanteCompra As New ComprobanteCompra
        Dim iOrigen As String

        Try

            iOrigen = Me.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "")

            If TypeOf Session(Session.SessionID & "entidad" & iOrigen) Is ComprobanteCompra Then
                With CType(Session(Session.SessionID & "entidad" & iOrigen), ComprobanteCompra)

                End With
            End If

        Catch exception As Exception
            If Not TypeOf (exception) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = exception
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
            iComprobanteCompra = Nothing
        End Try
    End Sub

    Private Function obtenerOrigenDatos() As DataTable
        Dim iAdministradorNiveles As New AdministradorNiveles
        'Dim iAdministradorComprobantes As New AdministradorComprobantes
        Dim iComprobanteCompra As New ComprobanteCompra
        Dim iNivel As Nivel
        Dim iDataSet As DataSet

        Try
            If Not Session(Session.SessionID & "Usuario").nivel.isSucursal Then
                '    If lstSucursal.Items.Count > 0 AndAlso lstSucursal.SelectedItem.Value <> 0 Then
                iNivel = New Sucursal()
                    '        iNivel.id = lstSucursal.SelectedItem.Value
                    '    ElseIf lstUnidadDeNegocios.Items.Count > 0 AndAlso lstUnidadDeNegocios.SelectedItem.Value <> 0 Then
                    iNivel = New UnidadDeNegocios()
                    '    iNivel.id = lstUnidadDeNegocios.SelectedItem.Value
                    '    ElseIf lstEmpresaGrupo.Items.Count > 0 AndAlso lstEmpresaGrupo.SelectedItem.Value <> 0 Then
                    iNivel = New EmpresaGrupo()
                    '        iNivel.id = lstEmpresaGrupo.SelectedItem.Value
                Else
                    iNivel = Session(Session.SessionID & "Usuario").nivel
                End If
            'Else
            iNivel = Session(Session.SessionID & "Usuario").nivel
            'End If

            'iComprobanteCompra.proveedor = New Proveedor
            If Trim(intCodigo.Text) <> "" Then
                '    iComprobanteCompra.proveedor.codigo = Trim(intCodigo.Text)
            Else
                '    iComprobanteCompra.proveedor.codigo = Nothing
            End If
            'iComprobanteCompra.proveedor.razonSocial = txtNombre.Text

            If lstTipoFactura.SelectedValue <> "" Then
                Select Case lstTipoFactura.SelectedValue
                    Case TipoComprobante.FACTURAA
                        iComprobanteCompra.tipoComprobante = New FacturaA
                    Case TipoComprobante.FACTURAB
                        iComprobanteCompra.tipoComprobante = New FacturaB
                    Case TipoComprobante.FACTURAC
                        iComprobanteCompra.tipoComprobante = New FacturaC
                    Case TipoComprobante.NOTADECREDITOA
                        iComprobanteCompra.tipoComprobante = New NotaDeCreditoA
                    Case TipoComprobante.NOTADEDEBITOA
                        iComprobanteCompra.tipoComprobante = New NotaDeDebitoA
                    Case TipoComprobante.NOTADECREDITOB
                        iComprobanteCompra.tipoComprobante = New NotaDeCreditoB
                    Case TipoComprobante.NOTADEDEBITOB
                        iComprobanteCompra.tipoComprobante = New NotaDeDebitoB
                    Case TipoComprobante.NOTADECREDITOC
                        iComprobanteCompra.tipoComprobante = New NotaDeCreditoC
                    Case TipoComprobante.NOTADEDEBITOC
                        iComprobanteCompra.tipoComprobante = New NotaDeDebitoC
                End Select
            End If

            If lstFormaPago.SelectedValue <> "" Then
                Select Case lstFormaPago.SelectedValue
                    Case ComprobanteCompra.enumFormaPago.CAJA
                        iComprobanteCompra.formaPago = ComprobanteCompra.enumFormaPago.CAJA
                    Case ComprobanteCompra.enumFormaPago.BANCO
                        iComprobanteCompra.formaPago = ComprobanteCompra.enumFormaPago.BANCO
                End Select
            End If

            grillaComprobanteConsulta.CurrentPageIndex = 0
            '    iDataSet = iAdministradorComprobantes.obtenerComprobanteCompraGrilla(iComprobanteCompra, iNivel)
            grillaComprobanteConsulta.DataSource = iDataSet
            Session(Session.SessionID & "xDataSet") = iDataSet

            Return iDataSet.Tables("comprobantegrilla")

        Catch exception As Exception
            If Not TypeOf (exception) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = exception
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
            iComprobanteCompra = Nothing
            '   iAdministradorComprobantes = Nothing
        End Try
    End Function

    Private Sub grillaComprobanteConsulta_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grillaComprobanteConsulta.ItemCommand

        Try

            Select Case e.CommandName
                Case "Detalles"
                    detalles(CLng(e.Item.Cells(0).Text))
                Case "Modificar"
                    modificar(CLng(e.Item.Cells(0).Text))
                Case "Eliminar"
                    eliminar(CLng(e.Item.Cells(0).Text))
            End Select

        Catch Exception As Exception
            If Not TypeOf (Exception) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = Exception
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
        End Try
    End Sub

    Private Sub detalles(eId As Long)
        'Dim iAdministradorComprobantes As New AdministradorComprobantes
        Dim iComprobanteCompra As New ComprobanteCompra
        Dim iOrigen As String

        Try

            iOrigen = Me.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "")
            Session(Session.SessionID & "detalle" & iOrigen) = True

            Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri

            iComprobanteCompra.id = eId
            'iComprobanteCompra = iAdministradorComprobantes.obtenerComprobanteCompra(iComprobanteCompra)
            Session(Session.SessionID & "ComprobanteCompra") = iComprobanteCompra

            Response.Redirect("ComprobanteCompraDetalles.aspx", False)

        Catch Exception As Exception
            If Not TypeOf (Exception) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = Exception
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
            iComprobanteCompra = Nothing
        End Try
    End Sub

    Private Sub modificar(eId As Long)
        Dim iComprobanteCompra As New ComprobanteCompra
        'Dim iAdministradorComprobantes As New AdministradorComprobantes
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        Dim iUsuario As Usuario
        Dim iOrigen As String

        Try

            iUsuario = Session(Session.SessionID & "usuario")
            If Not iAdministradorUsuarios.puedeAcceder(iUsuario, "ComprobanteCompraModificar.aspx") Then
                Master.alertCustom("no tiene permiso para acceder a esta pagina")
            Else
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                iOrigen = Me.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "")
                Session(Session.SessionID & "modificar" & iOrigen) = True
                iComprobanteCompra.id = eId
                '   iComprobanteCompra = iAdministradorComprobantes.obtenerComprobanteCompra(iComprobanteCompra)
                Session(Session.SessionID & "ComprobanteCompra") = iComprobanteCompra

                Response.Redirect("ComprobanteCompraModificar.aspx", False)
            End If

        Catch Exception As Exception
            If Not TypeOf (Exception) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = Exception
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
            iComprobanteCompra = Nothing
            iAdministradorUsuarios = Nothing
            iUsuario = Nothing
        End Try
    End Sub

    Private Sub eliminar(eId As Long)
        Dim iComprobanteCompra As New ComprobanteCompra
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        'Dim iAdministradorComprobantes As New AdministradorComprobantes
        Dim iColeccionAEliminar As New Collection
        Dim iDataTable As DataTable
        Dim iUsuario As Usuario
        Dim iOrigen As String

        Try

            iUsuario = Session(Session.SessionID & "usuario")
            If Not iAdministradorUsuarios.puedeAcceder(iUsuario, "ComprobanteCompraEliminar.aspx") Then
                Master.alertCustom("no tiene permiso para acceder a esta pagina")
            Else

                iOrigen = Me.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "")
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri

                iDataTable = Session(Session.SessionID & "DataTable" & iOrigen)
                Session(Session.SessionID & "eliminar" & iOrigen) = True

                iComprobanteCompra.id = eId
                'iComprobanteCompra = iAdministradorComprobantes.obtenerComprobanteCompra(iComprobanteCompra)
                'iAdministradorComprobantes.eliminarComprobanteCompra(iComprobanteCompra)

                For Each iRow As DataRow In iDataTable.Rows
                    If iRow.Item("id") = eId Then
                        iColeccionAEliminar.Add(iRow)
                    End If
                Next

                For Each iRow As DataRow In iColeccionAEliminar
                    iDataTable.Rows.Remove(iRow)
                Next

                Session(Session.SessionID & "DataTable" & iOrigen) = iDataTable

                Master.modal("", "Comprobante", "Operacion realizada correctamente")
            End If

        Catch Exception As Exception
            If Not TypeOf (Exception) Is Threading.ThreadAbortException Then
                Session(Session.SessionID & "ultimaExcepcion") = Exception
                Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                Master.modalError("", "Comprobante", "Ocurrió un error")
            End If
        Finally
            iComprobanteCompra = Nothing
            'iAdministradorComprobantes = Nothing
            iAdministradorUsuarios = Nothing
            iColeccionAEliminar = Nothing
            iDataTable = Nothing
            iUsuario = Nothing
        End Try
    End Sub

    Private Sub alta()
        Dim iOrigen As String = Me.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "")

        Session(Session.SessionID & "alta" & iOrigen) = True
        Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri

        Response.Redirect("ComprobanteCompraAlta.aspx", False)
    End Sub

#End Region

#Region "Listas"

    Private Sub llenarListas()
        Dim iAdministradorNiveles As New AdministradorNiveles
        Dim iDataReader As IDataReader
        'Dim iAdministradorComprobantes As New AdministradorComprobantes
        Dim iTipoComprobante As TipoComprobante
        Dim iSortedList As SortedList
        Dim i As Integer

        Try
            'llenarListasNiveles()

            lstTipoFactura.Items.Add(New ListItem("Todos", ""))
            'iSortedList = iAdministradorComprobantes.obtenerTiposComprobanteProveedor()
            'For i = 0 To iSortedList.Count - 1
            '    lstTipoFactura.Items.Add(New ListItem(iSortedList.GetValueList.Item(i), iSortedList.GetKeyList.Item(i)))
            'Next

            'If Not Session(Session.SessionID & "usuario").nivel.isSucursal Then
            '    grillaComprobanteConsulta.Columns(1).Visible = True
            'Else
            '    grillaComprobanteConsulta.Columns(1).Visible = False
            'End If

            lstFormaPago.Items.Add(New ListItem(""))
            lstFormaPago.Items.Add(New ListItem("CAJA", ComprobanteCompra.enumFormaPago.CAJA))
            lstFormaPago.Items.Add(New ListItem("BANCO", ComprobanteCompra.enumFormaPago.BANCO))

        Catch ex As Exception
            Session(Session.SessionID & "ultimaExcepcion") = ex
            Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
            Response.Redirect("Error.aspx", True)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iAdministradorNiveles = Nothing
        End Try

    End Sub

#End Region

#Region "Listas Niveles"
    'Private Sub llenarListasNiveles()
    '    Try

    '        If Session(Session.SessionID & "usuario").nivel.isUnidadDeNegocios Then
    '            ccdUnidadDeNegocios.SelectedValue = Session(Session.SessionID & "usuario").nivel.id
    '            'lblSucursal.Visible = True
    '        ElseIf Session(Session.SessionID & "usuario").nivel.isEmpresaGrupo Then
    '            ccdEmpresaGrupo.SelectedValue = Session(Session.SessionID & "usuario").nivel.id
    '            lblUnidadDeNegocios.Visible = True
    '            lblSucursal.Visible = True
    '        ElseIf Session(Session.SessionID & "usuario").nivel.isGrupoEmpresas Then
    '            ccdGrupoEmpresa.SelectedValue = Session(Session.SessionID & "usuario").nivel.id
    '            lblSucursal.Visible = True
    '            lblUnidadDeNegocios.Visible = True
    '            lblEmpresaGrupo.Visible = True
    '        End If

    '    Catch Exception As Exception
    '        If Not TypeOf (Exception) Is Threading.ThreadAbortException Then
    '            Session(Session.SessionID & "ultimaExcepcion") = Exception
    '            Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
    '            Response.Redirect("Error.aspx", True)
    '        End If
    '    Finally
    '    End Try
    'End Sub

    Public Function nivelColumna() As String

        If Session(Session.SessionID & "usuario").nivel.isSucursal Then
            Return "col-md-12"
        ElseIf Session(Session.SessionID & "usuario").nivel.isUnidadDeNegocios Then
            Return "col-md-12"
        ElseIf Session(Session.SessionID & "usuario").nivel.isEmpresaGrupo Then
            Return "col-md-6"
        ElseIf Session(Session.SessionID & "usuario").nivel.isGrupoEmpresas Then
            Return "col-md-4"
        Else
            Return "col-md-12"
        End If

    End Function

    Public Function bordeEmpresaGrupo() As String
        'Return IIf(Me.lblEmpresaGrupo.Visible, "block", "none")
    End Function

    Public Function bordeUnidadDeNegocios() As String
        'Return IIf(Me.lblUnidadDeNegocios.Visible, "block", "none")
    End Function

    Public Function bordeSucursal() As String
        'Return IIf(Me.lblSucursal.Visible, "block", "none")
    End Function
#End Region

#Region "Botones"
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBuscar.Click
        paginarGrilla(grillaComprobanteConsulta, True)
    End Sub

    Private Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click
        alta()
    End Sub
#End Region

#Region "Pagina"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            paginarGrilla(grillaComprobanteConsulta)
            llenarListas()
            'SetFocus(lstEmpresaGrupo)
            borarrSessionControlesNiveles()
        Else
            'mantenerEstadoNiveles(lstEmpresaGrupo.SelectedItem.Value, lstUnidadDeNegocios.SelectedItem.Value)
        End If
    End Sub
#End Region
End Class
