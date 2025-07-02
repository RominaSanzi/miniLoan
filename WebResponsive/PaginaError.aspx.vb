Imports di.financiera.excepciones
Imports di.financiera.seguridad
Imports di.financiera.reglasnegocios
Imports System.Configuration
Imports System.IO

Partial Class PaginaError
    Inherits System.Web.UI.Page






#Region "Pagina"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            '    setFocus(btnAtras)
            mostrarExcepcion()

        End If
    End Sub
#End Region

    Private Sub mostrarExcepcion()
        Dim iExcepcion As Exception

        Try
            iExcepcion = Session(Session.SessionID & "ultimaExcepcion")

            If Not IsNothing(iExcepcion) Then
                If TypeOf (iExcepcion) Is RootException Then
                    Dim iExcepcionAplicacion As RootException = iExcepcion

                    lblMotivoDetalle.Text = iExcepcionAplicacion.ToString & vbNewLine
                    While Not IsNothing(iExcepcionAplicacion.originalCause)
                        If TypeOf (iExcepcionAplicacion.originalCause) Is RootException Then
                            iExcepcionAplicacion = iExcepcionAplicacion.originalCause
                            lblMotivoDetalle.Text = iExcepcionAplicacion.ToString & vbNewLine
                        Else
                            lblMotivoDetalle.Text = iExcepcionAplicacion.originalCause.ToString
                            iExcepcionAplicacion.originalCause = Nothing
                        End If
                    End While
                Else
                    lblMotivoDetalle.Text = iExcepcion.ToString
                End If
                loguearAccion(lblMotivoDetalle.Text)
            End If

        Catch exception As Exception
            'Si hay error lo controlo pero no hago nada
        Finally
            iExcepcion = Nothing
            removerSessiones()
        End Try
    End Sub

    Private Sub btnAtras_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAtras.Click
        If Session(Session.SessionID & "ultimaPagina") <> Nothing Then Response.Redirect(Session(Session.SessionID & "ultimaPagina"))
    End Sub

    Private Sub loguearAccion(ByVal iException As String)
        Dim iLog As New Log()
        Dim iAdministradorUsuarios As New AdministradorUsuarios()
        Dim iDatosAccion As DatosAccion
        Dim iDetalle As String
        Try
            iLog.fecha = Now
            iLog.usuario = Session(Session.SessionID & "usuario")
            iLog.accion = New Accion
            iDetalle = "ORIGEN:" & Right(Session(Session.SessionID & "ultimaPagina"), Len(Session(Session.SessionID & "ultimaPagina")) - InStrRev(Session(Session.SessionID & "ultimaPagina"), "/")) & vbNewLine
            iDetalle = iDetalle & "EXCEPCION:" & lblMotivoDetalle.Text
            iLog.detalle = iDetalle
            iLog.accion.id = iDatosAccion.getInstancia.acciones.Item("Error.aspx")

            iAdministradorUsuarios.crearLog(iLog)

        Catch exception As Exception
            Throw New LogNoCreadoException
        Finally
            iAdministradorUsuarios = Nothing
            iLog = Nothing
        End Try
    End Sub

    Private Sub removerSessiones()
        Session.Remove(Session.SessionID & "ultimaExcepcion")
    End Sub


End Class
