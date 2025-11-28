Imports di.financiera.entidades

Public Class PasanteDetalles
    Inherits System.Web.UI.Page

    Private documentoPasante As String

#Region "Métodos y sub"
    Private Sub llenarformularios()
        ' Obtener el documento del QueryString
        If Not String.IsNullOrEmpty(Request.QueryString("documento")) Then
            documentoPasante = Request.QueryString("documento")
            CargarDatosPasante(documentoPasante)
        Else
            Master.modalError("PasanteDetalles.aspx", "Error", "No se especificó el documento del pasante")
            Return
        End If
    End Sub

    Private Sub CargarDatosPasante(documento As String)
        Try
            Dim iPasante As New Pasante()
            iPasante.documento = CLng(documento)

            ' Obtener los datos completos del pasante
            Dim pasanteCompleto As Pasante = iPasante.ObtenerPorDocumento(documento)

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
                        txtSexo.Text = pasanteCompleto.sexo.descripcion
                    ElseIf Not IsNothing(pasanteCompleto.sexo) AndAlso Not IsNothing(pasanteCompleto.sexo.id) Then
                        txtSexo.Text = pasanteCompleto.sexo.id.ToString()
                    Else
                        txtSexo.Text = ""
                    End If
                Catch
                    txtSexo.Text = ""
                End Try

                ' Tipo documento
                Try
                    If Not IsNothing(pasanteCompleto.tipoDocumento) AndAlso Not IsNothing(pasanteCompleto.tipoDocumento.descripcion) Then
                        txtTipoDNI.Text = pasanteCompleto.tipoDocumento.descripcion
                    ElseIf Not IsNothing(pasanteCompleto.tipoDocumento) AndAlso Not IsNothing(pasanteCompleto.tipoDocumento.id) Then
                        txtTipoDNI.Text = pasanteCompleto.tipoDocumento.id.ToString()
                    Else
                        txtTipoDNI.Text = ""
                    End If
                Catch
                    txtTipoDNI.Text = ""
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
                            txtLocalidad.Text = pasanteCompleto.domicilio.localidad.descripcion
                        Else
                            txtLocalidad.Text = ""
                        End If
                    Catch
                        txtLocalidad.Text = ""
                    End Try

                    ' Barrio
                    Try
                        If Not String.IsNullOrEmpty(pasanteCompleto.domicilio.barrio) Then
                            txtBarrio.Text = pasanteCompleto.domicilio.barrio
                        Else
                            txtBarrio.Text = ""
                        End If
                    Catch
                        txtBarrio.Text = ""
                    End Try
                Else
                    txtCalle.Text = ""
                    intNumero.Text = ""
                    intPiso.Text = ""
                    intCodigoPostal.Text = ""
                    txtLocalidad.Text = ""
                    txtBarrio.Text = ""
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
                        txtEscuela.Text = pasanteCompleto.escuela
                    Else
                        txtEscuela.Text = ""
                    End If
                Catch
                    txtEscuela.Text = ""
                End Try

            Else
                Master.modalError("PasanteDetalles.aspx", "Error", "No se encontró el pasante con documento: " & documento)
            End If

        Catch ex As Exception
            Master.modalError("PasanteDetalles.aspx", "Pasante", "Error al cargar datos: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "Página"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            llenarformularios()
        End If
    End Sub
#End Region

#Region "Botones"
    Private Sub btnAtras_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAtras.Click
        Response.Redirect("PasanteAdministrador.aspx")
    End Sub

    Private Sub btnAyuda_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAyuda.Click
        Master.ayuda(Request.UrlReferrer.AbsoluteUri.Split("/")(Request.UrlReferrer.AbsoluteUri.Split("/").Length - 1))
    End Sub
#End Region

End Class
