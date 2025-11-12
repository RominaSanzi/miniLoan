Imports di.financiera.entidades

Public Class PasanteAlta
    Inherits System.Web.UI.Page
#Region "Variables"

#End Region

#Region "Listas"
    Private Sub llenarlistas()
        Dim iSortedList As SortedList
        Dim ilistaLocalidades As New List(Of Localidad)
        Dim iLocalidad As New Localidad
        Dim ilistaEscuelas As New List(Of Escuela)
        Dim iEscuela As New Escuela

        lstSexo.Items.Add(New ListItem("GENÉRICO", 1))
        lstSexo.Items.Add(New ListItem("MASCULINO", 2))
        lstSexo.Items.Add(New ListItem("FEMENINO", 3))

        lstBarrio.Items.Add(New ListItem("GENÉRICO", 1))

        Try
            iSortedList = TipoDocumento.obtenerTiposDocumento()
            For i = 0 To iSortedList.Count - 1
                lstTipoDNI.Items.Add(New ListItem(iSortedList.GetValueList.Item(i), iSortedList.GetKeyList.Item(i)))
            Next

            ilistaLocalidades = iLocalidad.obtenerListaLocalidad()
            For Each localidad As Localidad In ilistaLocalidades
                lstLocalidad.Items.Add(New ListItem(localidad.descripcion, localidad.id))
            Next

            ilistaEscuelas = iEscuela.obtenerListaEscuelas()
            For Each escuela As Escuela In ilistaEscuelas
                lstEscuela.Items.Add(New ListItem(escuela.nombre, escuela.id))
            Next
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Botones"
    Private Sub btnAtras_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAtras.Click
        Response.Redirect(Session(Session.SessionID & "UltimaPagina"))
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Dim iDomicilio As New Domicilio
        Dim iPasante As New Pasante

        Try
            With iDomicilio
                .calle = txtCalle.Text
                .numero = intNumero.Text
                .piso = intPiso.Text
                .codigoPostal = intCodigoPostal.Text
                .localidad = New Localidad
                .localidad.id = lstLocalidad.SelectedValue
                .barrio = lstBarrio.SelectedValue
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
            iPasante.crear(False)

            Master.modal("PasanteAdministrador.aspx", "Pasante", "Operación realizada exitosamente")
        Catch ex As Exception
            Master.modalError("", "Pasante", "Ocurrió un error")
        End Try


    End Sub
#End Region

#Region "Pagina"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            llenarlistas()
        End If
    End Sub
#End Region

End Class