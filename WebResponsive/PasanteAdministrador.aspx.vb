Imports di.financiera.entidades
Imports Google.Protobuf.WellKnownTypes

Public Class PasanteAdministrador
    Inherits System.Web.UI.Page

#Region "Botones"
    Private Sub btnAtras_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAtras.Click
        Response.Redirect(Session(Session.SessionID & "UltimaPagina"))
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBuscar.Click
        llenarGrilla()
    End Sub

    Private Overloads Sub btnayuda_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAyuda.Click
        Master.ayuda(Request.UrlReferrer.AbsoluteUri.Split("/")(Request.UrlReferrer.AbsoluteUri.Split("/").Length - 1))
    End Sub

    Protected Sub BtnAlta_Click(sender As Object, e As EventArgs) Handles BtnAlta.Click
        Response.Redirect("PasanteAlta.aspx")
    End Sub

    ' Manejar eventos de la grilla
    Protected Sub grillaPasante_ItemCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles grillaPasante.ItemCommand
        Dim documento As String = e.Item.Cells(0).Text

        If e.CommandName = "Detalles" Then
            Response.Redirect("PasanteDetalles.aspx?documento=" & documento)

        ElseIf e.CommandName = "Modificar" Then
            Response.Redirect("PasanteModificar.aspx?documento=" & documento)

        ElseIf e.CommandName = "Eliminar" Then
            Session(Session.SessionID) = documento
            eliminarPasante(documento)

        End If
    End Sub
#End Region

#Region "Funciones y Sub"
    Private Sub eliminarPasante(documento As String)
        Dim iPasante As New Pasante
        Dim iColeccionPasante As New List(Of Pasante)

        Try
            iPasante = iPasante.ObtenerPorDocumento(documento)

            If iPasante IsNot Nothing Then
                iPasante.eliminar()

                Session(Session.SessionID & "DocumentoEliminar") = Nothing
                Session(Session.SessionID & "PasanteEliminar") = Nothing

                Master.modal("PasanteAdministrador.aspx", "Pasante", "El pasante ha sido eliminado correctamente.")
            Else
                Master.modalError("PasanteAdministrador.aspx", "Pasante", "No se encontró el pasante.")
            End If

        Catch ex As Exception
            Master.modalError("PasanteAdministrador.aspx", "Pasante", "Ocurrió un error al eliminar el pasante: " & ex.Message)
        Finally
            llenarGrilla()
        End Try
    End Sub

    Public Sub llenarGrilla()
        Dim iPasante As New Pasante
        Dim listaPasantes As List(Of Pasante)

        Try
            With iPasante
                ' Si el campo DNI está vacío, dejamos documento en Nothing
                If Not String.IsNullOrEmpty(intDNI.Text) Then
                    .documento = CLng(intDNI.Text)
                Else
                    .documento = Nothing
                End If
                .nombre = FuncionComun.vacioSiEsNothing(txtNombre.Text)
            End With

            listaPasantes = iPasante.BuscarPasantes()
            grillaPasante.DataSource = listaPasantes
            grillaPasante.DataBind()

        Catch ex As Exception
            Master.modalError("PasanteAdministrador.aspx", "Pasante", "Ocurrió un error: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "Página"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim iOrigen As String

        Try
            If Not IsPostBack Then
                ' Obtener la URL de origen
                iOrigen = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "").Replace(".aspx", "")

                ' Guardar la última página visitada
                If Request.UrlReferrer IsNot Nothing Then
                    Session(Session.SessionID & "ultimaPagina") = Request.UrlReferrer.AbsoluteUri
                End If

                ' Verificar si viene de una confirmación de eliminación
                If Session(Session.SessionID & "PasanteEliminar") IsNot Nothing AndAlso
                   CBool(Session(Session.SessionID & "PasanteEliminar")) = True Then

                    Dim documento As String = Session(Session.SessionID & "DocumentoEliminar").ToString()
                    eliminarPasante(documento)
                End If

                ' Cargar la grilla
                llenarGrilla()
            End If

        Catch ex As Exception
            If TypeOf ex Is Threading.ThreadAbortException Then
                ' Ignorar ThreadAbortException causada por Response.Redirect
            Else
                Session(Session.SessionID & "ultimaExcepcion") = ex
                Master.modalError("PasanteAdministrador.aspx", "Pasante", "Ocurrió un error: " & ex.Message)
            End If
        Finally
            ' Limpiar variables de sesión después de procesar
            Session(Session.SessionID & "DocumentoEliminar") = Nothing
            Session(Session.SessionID & "PasanteEliminar") = Nothing
        End Try
    End Sub
#End Region
End Class