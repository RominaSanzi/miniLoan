Imports di.financiera.entidades
Imports Org.BouncyCastle.Asn1.Utilities

Public Class PasanteModificar
    Inherits System.Web.UI.Page

#Region "Variables"
    Private documentoPasante As Long
#End Region

#Region "Listas"
    Private Sub llenarlistas()
        Dim iSortedList As SortedList
        Dim ilistaLocalidades As New List(Of Localidad)
        Dim iLocalidad As New Localidad
        Dim ilistaEscuelas As New List(Of Escuela)
        Dim iEscuela As New Escuela

        lstSexo.Items.Clear()
        lstSexo.Items.Add(New ListItem("GENÉRICO", 1))
        lstSexo.Items.Add(New ListItem("MASCULINO", 2))
        lstSexo.Items.Add(New ListItem("FEMENINO", 3))

        lstBarrio.Items.Clear()
        lstBarrio.Items.Add(New ListItem("GENÉRICO", 1))

        Try
            ' Tipos Documento
            iSortedList = TipoDocumento.obtenerTiposDocumento()
            lstTipoDNI.Items.Clear()
            For i = 0 To iSortedList.Count - 1
                lstTipoDNI.Items.Add(New ListItem(iSortedList.GetValueList.Item(i), iSortedList.GetKeyList.Item(i)))
            Next

            ' Localidades
            ilistaLocalidades = iLocalidad.obtenerListaLocalidad()
            lstLocalidad.Items.Clear()
            For Each localidad As Localidad In ilistaLocalidades
                lstLocalidad.Items.Add(New ListItem(localidad.descripcion, localidad.id))
            Next

            ' Escuelas
            ilistaEscuelas = iEscuela.obtenerListaEscuelas()
            lstEscuela.Items.Clear()
            For Each escuela As Escuela In ilistaEscuelas
                lstEscuela.Items.Add(New ListItem(escuela.nombre, escuela.id))
            Next

        Catch ex As Exception
            Master.modalError("", "Error", "Ocurrió un error al cargar listas.")
        End Try
    End Sub
#End Region

#Region "Carga de datos del pasante"
    Private Sub llenarformularios()
        If Not String.IsNullOrEmpty(Request.QueryString("documento")) Then
            documentoPasante = CLng(Request.QueryString("documento"))
            CargarDatosPasante(documentoPasante)
        Else
            Master.modalError("PasanteAdministrador.aspx", "Error", "No se especificó el documento del pasante.")
        End If
    End Sub

    Private Sub CargarDatosPasante(doc As Long)
        Dim iPasante As New Pasante()
        Try

            iPasante.documento = CLng(doc)

            ' Obtener los datos completos del pasante
            Dim pasanteCompleto As Pasante = iPasante.ObtenerPorDocumento(doc)
            Session(Session.SessionID & "Pasante") = pasanteCompleto
            Session(Session.SessionID & "xPasante") = Session(Session.SessionID & "Pasante")

            If pasanteCompleto IsNot Nothing Then
                ' ---------------------------
                ' DATOS PERSONALES
                ' ---------------------------

                ' DNI
                If pasanteCompleto.documento.HasValue Then
                    intDNI.Text = pasanteCompleto.documento.Value.ToString()
                Else
                    intDNI.Text = ""
                End If

                ' Nombre y apellido
                txtNombreApellido.Text = FuncionComun.vacioSiEsNothing(pasanteCompleto.nombre)

                ' Fecha nacimiento
                If Not IsNothing(pasanteCompleto.fechaNacimiento) AndAlso pasanteCompleto.fechaNacimiento <> Nothing Then
                    Try
                        dtbFechaNacimiento.Text = pasanteCompleto.fechaNacimiento.ToString("dd/MM/yyyy")
                    Catch
                        dtbFechaNacimiento.Text = ""
                    End Try
                Else
                    dtbFechaNacimiento.Text = ""
                End If

                ' Email
                txtEmail.Text = FuncionComun.vacioSiEsNothing(pasanteCompleto.email)

                ' ----------------------------------------
                ' CUIL completo: cuil1 + documento + cuil2
                ' ----------------------------------------
                Dim cuilText As String = ""

                Try
                    If pasanteCompleto.cuil1 <> 0 Then
                        cuilText &= pasanteCompleto.cuil1.ToString()
                    End If
                Catch
                End Try

                Try
                    If pasanteCompleto.documento.HasValue Then
                        cuilText &= pasanteCompleto.documento.Value.ToString()
                    End If
                Catch
                End Try

                Try
                    If pasanteCompleto.cuil2 <> 0 Then
                        cuilText &= pasanteCompleto.cuil2.ToString()
                    End If
                Catch
                End Try

                intCUIL.Text = cuilText

                ' Sexo
                Try
                    If Not IsNothing(pasanteCompleto.sexo) AndAlso Not IsNothing(pasanteCompleto.sexo.descripcion) Then
                        lstSexo.Text = pasanteCompleto.sexo.descripcion
                    ElseIf Not IsNothing(pasanteCompleto.sexo) AndAlso Not IsNothing(pasanteCompleto.sexo.id) Then
                        lstSexo.Text = pasanteCompleto.sexo.id.ToString()
                    Else
                        lstSexo.Text = ""
                    End If
                Catch
                    lstSexo.Text = ""
                End Try

                ' Tipo documento
                Try
                    If Not IsNothing(pasanteCompleto.tipoDocumento) AndAlso Not IsNothing(pasanteCompleto.tipoDocumento.descripcion) Then
                        lstTipoDNI.Text = pasanteCompleto.tipoDocumento.descripcion
                    ElseIf Not IsNothing(pasanteCompleto.tipoDocumento) AndAlso Not IsNothing(pasanteCompleto.tipoDocumento.id) Then
                        lstTipoDNI.Text = pasanteCompleto.tipoDocumento.id.ToString()
                    Else
                        lstTipoDNI.Text = ""
                    End If
                Catch
                    lstTipoDNI.Text = ""
                End Try

                ' ---------------------------
                ' DOMICILIO
                ' ---------------------------
                If Not IsNothing(pasanteCompleto.domicilio) Then
                    txtCalle.Text = FuncionComun.vacioSiEsNothing(pasanteCompleto.domicilio.calle)
                    intNumero.Text = FuncionComun.vacioSiEsNothing(pasanteCompleto.domicilio.numero)
                    intPiso.Text = FuncionComun.vacioSiEsNothing(pasanteCompleto.domicilio.piso)
                    intCodigoPostal.Text = FuncionComun.vacioSiEsNothing(pasanteCompleto.domicilio.codigoPostal)

                    ' Localidad
                    Try
                        If Not IsNothing(pasanteCompleto.domicilio.localidad) Then
                            lstLocalidad.Text = pasanteCompleto.domicilio.localidad.descripcion
                        Else
                            lstLocalidad.Text = ""
                        End If
                    Catch
                        lstLocalidad.Text = ""
                    End Try

                    ' Barrio
                    Try
                        If Not String.IsNullOrEmpty(pasanteCompleto.domicilio.barrio) Then
                            lstBarrio.Text = pasanteCompleto.domicilio.barrio
                        Else
                            lstBarrio.Text = ""
                        End If
                    Catch
                        lstBarrio.Text = ""
                    End Try
                Else
                    txtCalle.Text = ""
                    intNumero.Text = ""
                    intPiso.Text = ""
                    intCodigoPostal.Text = ""
                    lstLocalidad.Text = ""
                    lstBarrio.Text = ""
                End If

                ' ---------------------------
                ' DATOS DE PASANTÍA
                ' ---------------------------
                intLegajoEscuela.Text = FuncionComun.vacioSiEsNothing(pasanteCompleto.legajoEscuela)

                If Not IsNothing(pasanteCompleto.fechaInicioPasantia) AndAlso pasanteCompleto.fechaInicioPasantia <> Nothing Then
                    Try
                        dtbFechaInicio.Text = pasanteCompleto.fechaInicioPasantia.ToString("dd/MM/yyyy")
                    Catch
                        dtbFechaInicio.Text = ""
                    End Try
                Else
                    dtbFechaInicio.Text = ""
                End If

                If Not IsNothing(pasanteCompleto.fechaFinalizacionPasantia) AndAlso pasanteCompleto.fechaFinalizacionPasantia <> Nothing Then
                    Try
                        dtbFechaFinalizacion.Text = pasanteCompleto.fechaFinalizacionPasantia.ToString("dd/MM/yyyy")
                    Catch
                        dtbFechaFinalizacion.Text = ""
                    End Try
                Else
                    dtbFechaFinalizacion.Text = ""
                End If

                ' Escuela
                Try
                    If Not String.IsNullOrEmpty(pasanteCompleto.escuela) Then
                        lstEscuela.Text = pasanteCompleto.escuela
                    Else
                        lstEscuela.Text = ""
                    End If
                Catch
                    lstEscuela.Text = ""
                End Try

            Else
                Master.modalError("PasanteDetalles.aspx", "Error", "No se encontró el pasante con documento: " & doc)
            End If

        Catch ex As Exception
            Master.modalError("PasanteDetalles.aspx", "Pasante", "Error al cargar datos: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "Botón Guardar"
    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Dim iDomicilio As New Domicilio
        Dim iPasante As New Pasante

        Try
            iPasante = Session(Session.SessionID & "xPasante")
            With iDomicilio
                .calle = txtCalle.Text
                .numero = intNumero.Text
                .piso = intPiso.Text
                .codigoPostal = intCodigoPostal.Text
                .localidad = New Localidad
                .localidad.id = lstLocalidad.SelectedValue
                .barrio = lstBarrio.SelectedValue
                .id = iPasante.domicilio.id
            End With
            With iPasante
                Select Case CLng(lstTipoDNI.SelectedItem.Value)
                    Case TipoDocumento.DNI
                        .tipoDocumento = New DNI()
                    Case TipoDocumento.CUIT
                        .tipoDocumento = New CUIT
                    Case TipoDocumento.CI
                        .tipoDocumento = New CI()
                    Case TipoDocumento.LE
                        .tipoDocumento = New LE()
                    Case TipoDocumento.LC
                        .tipoDocumento = New LC()
                    Case TipoDocumento.PAS
                        .tipoDocumento = New PAS()
                End Select
                .documento = intDNI.Text
                .nombre = txtNombreApellido.Text
                .fechaNacimiento = dtbFechaNacimiento.Text
                .fechaAlta = Now
                .email = txtEmail.Text
                .cuil1 = intCUIL.Text.Substring(0, 2)
                .cuil2 = intCUIL.Text.Substring(10, 1)
                .domicilio = New Domicilio
                .domicilio = iDomicilio
                .sexo = New Sexo
                .sexo.id = lstSexo.SelectedValue
                .estado = New Alta
                .legajoEscuela = intLegajoEscuela.Text
                .fechaInicioPasantia = dtbFechaInicio.Text
                .fechaFinalizacionPasantia = dtbFechaFinalizacion.Text
                .escuela = lstEscuela.SelectedValue
            End With
            iPasante.modificar()

            Master.modal("PasanteAdministrador.aspx", "Pasante", "Operación realizada exitosamente")
        Catch ex As Exception
            Master.modalError("", "Pasante", "Ocurrió un error")
        End Try
    End Sub
#End Region

#Region "Página"
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            llenarlistas()
            llenarformularios()
        End If
    End Sub
#End Region

End Class
