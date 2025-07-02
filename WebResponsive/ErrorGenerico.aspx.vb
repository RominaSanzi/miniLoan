Partial Class ErrorGenerico
    Inherits PaginaLogueo

#Region "Botones"
    Private Sub btnAtras_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAtras.Click
        Response.Redirect("IndexIframe.aspx", False)
    End Sub
#End Region

#Region "Pagina"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
#End Region

End Class
