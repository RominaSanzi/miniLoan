Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports di.financiera.entidades
Imports di.financiera.reglasnegocios

Public Class USCTelefono
    Inherits System.Web.UI.UserControl

#Region "Registro script"
    Public Sub alertCustom(ByVal eMensaje As String)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "alertCustom", "document.addEventListener('DOMContentLoaded', function(event) { site.showSwal(); Swal.fire({ type: 'info', html: '" & eMensaje & "', buttonsStyling: false,confirmButtonClass: 'btn btn-info btn-round btn-block min-width-200'}) });", True)
    End Sub
    Public Sub scriptAutocomplete()
        Dim iScript As String

        Try

            iScript = "function SetContextKeyCodigoArea" & iPrefijo & "() {
                            $find('" & intTelefonoCodigoArea_AutoCompleteExtender.ID & "').set_contextKey(" & IIf(iTelefonoCelular, FuncionComun.enumTipoTelefono.CELULAR, FuncionComun.enumTipoTelefono.SINDEFINIR) & ");
                      };
                      function SetContextKeyCaracteristica" & iPrefijo & "() {
                            $find('" & intTelefonoCaracteristica_AutoCompleteExtender.ID & "').set_contextKey($('#" & intTelefonoCodigoArea.ID & "').val() + '@' + " & IIf(iTelefonoCelular, FuncionComun.enumTipoTelefono.CELULAR, FuncionComun.enumTipoTelefono.SINDEFINIR) & ");
                      };"

            Select Case ConfigurationManager.AppSettings("Region")
                Case "ARGENTINA"

                    iScript &= vbCrLf & " 
                            function validarCombos" & iPrefijo & "() {
                               var intTelefonoCodigoArea" & iPrefijo & " = document.getElementById('" & intTelefonoCodigoArea.ID & "');
                               var intTelefonoCaracteristica" & iPrefijo & " = document.getElementById('" & intTelefonoCaracteristica.ID & "');
                               var intTelefonoNumero" & iPrefijo & " = document.getElementById('" & intTelefonoNumero.ID & "');

                               if (intTelefonoCodigoArea" & iPrefijo & ".value.length > 0 && intTelefonoCaracteristica" & iPrefijo & ".value.length > 0 && intTelefonoNumero" & iPrefijo & ".value.length) {
                                   if ((intTelefonoCodigoArea" & iPrefijo & ".value.length + intTelefonoCaracteristica" & iPrefijo & ".value.length + intTelefonoNumero" & iPrefijo & ".value.length) !== 10) {
                                      alertSwal('La suma de los dígitos del telefono, debe ser igual a 10');
                                   }
                               }
                          };"

                Case "URUGUAY"

                    iScript &= vbCrLf & " 
                            function validarCombos" & iPrefijo & "() {
                               var intTelefonoCodigoArea" & iPrefijo & " = document.getElementById('" & intTelefonoCodigoArea.ID & "');
                               var intTelefonoCaracteristica" & iPrefijo & " = document.getElementById('" & intTelefonoCaracteristica.ID & "');
                               var intTelefonoNumero" & iPrefijo & " = document.getElementById('" & intTelefonoNumero.ID & "');

                               if (intTelefonoCodigoArea" & iPrefijo & ".value.length > 0 && intTelefonoCaracteristica" & iPrefijo & ".value.length > 0 && intTelefonoNumero" & iPrefijo & ".value.length) {
                                   if (((intTelefonoCodigoArea" & iPrefijo & ".value.length + intTelefonoCaracteristica" & iPrefijo & ".value.length + intTelefonoNumero" & iPrefijo & ".value.length) < 8) || ((intTelefonoCodigoArea" & iPrefijo & ".value.length + intTelefonoCaracteristica" & iPrefijo & ".value.length + intTelefonoNumero" & iPrefijo & ".value.length) > 9)) {
                                      alertSwal('La suma de los dígitos del telefono debe estar entre 8 y 9');
                                   }
                               }
                          };"

                Case "PARAGUAY"

                    iScript &= vbCrLf & " 
                            function validarCombos" & iPrefijo & "() {
                               var intTelefonoCodigoArea" & iPrefijo & " = document.getElementById('" & intTelefonoCodigoArea.ID & "');
                               var intTelefonoCaracteristica" & iPrefijo & " = document.getElementById('" & intTelefonoCaracteristica.ID & "');
                               var intTelefonoNumero" & iPrefijo & " = document.getElementById('" & intTelefonoNumero.ID & "');

                               if (intTelefonoCodigoArea" & iPrefijo & ".value.length > 0 && intTelefonoCaracteristica" & iPrefijo & ".value.length > 0 && intTelefonoNumero" & iPrefijo & ".value.length) {
                                   if (((intTelefonoCodigoArea" & iPrefijo & ".value.length + intTelefonoCaracteristica" & iPrefijo & ".value.length + intTelefonoNumero" & iPrefijo & ".value.length) < 9) || ((intTelefonoCodigoArea" & iPrefijo & ".value.length + intTelefonoCaracteristica" & iPrefijo & ".value.length + intTelefonoNumero" & iPrefijo & ".value.length) > 12)) {
                                      alertSwal('La suma de los dígitos del telefono debe estar entre 9 y 12');
                                   }
                               }
                          };"

                Case "COLOMBIA"

                    iScript = " 
                        function validarCombos" & iPrefijo & "() {
                            var intTelefonoNumero" & iPrefijo & " = document.getElementById('" & intTelefonoNumero.ID & "');

                            if (intTelefonoNumero" & iPrefijo & ".value.length) {
                                if (intTelefonoNumero" & iPrefijo & ".value.length !== 10) {
                                    alertSwal('La suma de los dígitos del telefono, debe ser igual a 10');
                                    intTelefonoNumero" & iPrefijo & ".value = ''
                                }
                            }
                        };"

            End Select

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "ScriptTelefono" & iPrefijo, iScript, True)

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Variables"
    Private iObligatorio As Boolean = False
    Private iEnabled As Boolean = True
    Private iVisible As Boolean = True
    Private iBackColor As System.Drawing.Color
    Private iPrefijo As String
    Private iTelefonoCelular As Boolean = False
    Private iTelefonoCodigoArea As String
    Private iTelefonoCaracteristica As String
    Private iTelefonoNumero As String
    Private iText As String
#End Region

#Region "Atributos"
    Public Property Obligatorio() As Boolean
        Get
            Return iObligatorio
        End Get
        Set(ByVal value As Boolean)
            iObligatorio = value
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return iEnabled
        End Get
        Set(ByVal value As Boolean)
            iEnabled = value
        End Set
    End Property
    Public Property Visible() As Boolean
        Get
            Return iVisible
        End Get
        Set(ByVal value As Boolean)
            iVisible = value
        End Set
    End Property
    Public Property BackColor() As System.Drawing.Color
        Get
            Return iBackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            iBackColor = value
        End Set
    End Property
    Public Property Prefijo() As String
        Get
            Return iPrefijo
        End Get
        Set(ByVal value As String)
            iPrefijo = value
        End Set
    End Property
    Public Property TelefonoCelular() As Boolean
        Get
            Return iTelefonoCelular
        End Get
        Set(ByVal value As Boolean)
            iTelefonoCelular = value
        End Set
    End Property
    Public Property TelefonoCodigoArea() As String
        Get
            Return obtenerTelefonoCodigoArea()
        End Get
        Set(ByVal value As String)
            setearTelefonoCodigoArea(value)
        End Set
    End Property
    Public Property TelefonoCaracteristica() As String
        Get
            Return obtenerTelefonoCaracteristica()
        End Get
        Set(ByVal value As String)
            setearTelefonoCaracteristica(value)
        End Set
    End Property
    Public Property TelefonoNumero() As String
        Get
            Return obtenerTelefonoNumero()
        End Get
        Set(ByVal value As String)
            setearTelefonoNumero(value)
        End Set
    End Property
    Public Property Text() As String
        Get
            Return iText
        End Get
        Set(ByVal value As String)
            iText = value
        End Set
    End Property
#End Region

#Region "Eventos controles"
    Private Sub intTelefonoCodigoArea_AutoCompleteExtender_Init(sender As Object, e As EventArgs) Handles intTelefonoCodigoArea_AutoCompleteExtender.Init
        intTelefonoCodigoArea_AutoCompleteExtender.TargetControlID = intTelefonoCodigoArea.ID & iPrefijo
    End Sub
    Private Sub intTelefonoCaracteristica_AutoCompleteExtender_Init(sender As Object, e As EventArgs) Handles intTelefonoCaracteristica_AutoCompleteExtender.Init
        intTelefonoCaracteristica_AutoCompleteExtender.TargetControlID = intTelefonoCaracteristica.ID & iPrefijo
    End Sub
    Private Sub valTelefonoCodigoArea_Init(sender As Object, e As EventArgs) Handles valTelefonoCodigoArea.Init
        valTelefonoCodigoArea.ControlToValidate = intTelefonoCodigoArea.ID & iPrefijo
    End Sub
    Private Sub valTelefonoCaracteristica_Init(sender As Object, e As EventArgs) Handles valTelefonoCaracteristica.Init
        valTelefonoCaracteristica.ControlToValidate = intTelefonoCaracteristica.ID & iPrefijo
    End Sub
    Private Sub valTelefonoNumero_Init(sender As Object, e As EventArgs) Handles valTelefonoNumero.Init
        valTelefonoNumero.ControlToValidate = intTelefonoNumero.ID & iPrefijo
    End Sub
#End Region

#Region "Metodos privados"
    Protected Sub setearControl()

        lblTelefono.Text = iText
        lblTelefono.Visible = iVisible

        intTelefonoCodigoArea_AutoCompleteExtender.Enabled = iVisible
        intTelefonoCaracteristica_AutoCompleteExtender.Enabled = iVisible

        If Not IsNothing(iBackColor) Then
            intTelefonoCodigoArea.BackColor = iBackColor
            intTelefonoCaracteristica.BackColor = iBackColor
            intTelefonoNumero.BackColor = iBackColor
        End If

        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                intTelefonoCodigoArea.Visible = iVisible
                intTelefonoCaracteristica.Visible = iVisible
                intTelefonoNumero.Visible = iVisible

                intTelefonoCodigoArea.Enabled = iEnabled
                intTelefonoCaracteristica.Enabled = iEnabled
                intTelefonoNumero.Enabled = iEnabled

                intTelefonoCodigoArea.Visible = iVisible
                intTelefonoCaracteristica.Visible = iVisible
                intTelefonoNumero.Visible = iVisible

                intTelefonoCodigoArea.MaxLength = 4
                intTelefonoCaracteristica.MaxLength = 4
                intTelefonoNumero.MaxLength = 4

                If iEnabled Then
                    intTelefonoCodigoArea.Attributes.Add("PlaceHolder", "11")
                    intTelefonoCaracteristica.Attributes.Add("PlaceHolder", "1234")
                    intTelefonoNumero.Attributes.Add("PlaceHolder", "5678")
                End If

                If iVisible AndAlso iEnabled AndAlso iObligatorio Then
                    intTelefonoCodigoArea.CssClass = "form-control tel-cod-area input-requerido"
                    intTelefonoCaracteristica.CssClass = "form-control tel-carac input-requerido"
                    intTelefonoNumero.CssClass = "form-control tel-numero input-requerido"

                    valTelefonoCodigoArea.Visible = iObligatorio
                    valTelefonoCaracteristica.Visible = iObligatorio
                    valTelefonoNumero.Visible = iObligatorio
                End If

            Case "URUGUAY"
                intTelefonoCodigoArea.Visible = iVisible
                intTelefonoCaracteristica.Visible = iVisible
                intTelefonoNumero.Visible = iVisible

                intTelefonoCodigoArea.Enabled = iEnabled
                intTelefonoCaracteristica.Enabled = iEnabled
                intTelefonoNumero.Enabled = iEnabled

                intTelefonoCodigoArea.Visible = iVisible
                intTelefonoCaracteristica.Visible = iVisible
                intTelefonoNumero.Visible = iVisible

                intTelefonoCodigoArea.MaxLength = 3
                intTelefonoCaracteristica.MaxLength = 4
                intTelefonoNumero.MaxLength = 4

                If iEnabled Then
                    intTelefonoCodigoArea.Attributes.Add("PlaceHolder", "1")
                    intTelefonoCaracteristica.Attributes.Add("PlaceHolder", "1234")
                    intTelefonoNumero.Attributes.Add("PlaceHolder", "5678")
                End If

                If iVisible AndAlso iEnabled AndAlso iObligatorio Then
                    intTelefonoCodigoArea.CssClass = "form-control tel-cod-area input-requerido"
                    intTelefonoCaracteristica.CssClass = "form-control tel-carac input-requerido"
                    intTelefonoNumero.CssClass = "form-control tel-numero input-requerido"

                    valTelefonoCodigoArea.Visible = iObligatorio
                    valTelefonoCaracteristica.Visible = iObligatorio
                    valTelefonoNumero.Visible = iObligatorio
                End If
            Case "PARAGUAY"
                intTelefonoCodigoArea.Visible = iVisible
                intTelefonoCaracteristica.Visible = iVisible
                intTelefonoNumero.Visible = iVisible

                intTelefonoCodigoArea.Enabled = iEnabled
                intTelefonoCaracteristica.Enabled = iEnabled
                intTelefonoNumero.Enabled = iEnabled

                intTelefonoCodigoArea.Visible = iVisible
                intTelefonoCaracteristica.Visible = iVisible
                intTelefonoNumero.Visible = iVisible

                intTelefonoCodigoArea.MaxLength = 4
                intTelefonoCaracteristica.MaxLength = 4
                intTelefonoNumero.MaxLength = 4

                If iEnabled Then
                    intTelefonoCodigoArea.Attributes.Add("PlaceHolder", "1")
                    intTelefonoCaracteristica.Attributes.Add("PlaceHolder", "1234")
                    intTelefonoNumero.Attributes.Add("PlaceHolder", "5678")
                End If

                If iVisible AndAlso iEnabled AndAlso iObligatorio Then
                    intTelefonoCodigoArea.CssClass = "form-control tel-cod-area input-requerido"
                    intTelefonoCaracteristica.CssClass = "form-control tel-carac input-requerido"
                    intTelefonoNumero.CssClass = "form-control tel-numero input-requerido"

                    valTelefonoCodigoArea.Visible = iObligatorio
                    valTelefonoCaracteristica.Visible = iObligatorio
                    valTelefonoNumero.Visible = iObligatorio
                End If

            Case "COLOMBIA"
                intTelefonoCodigoArea.Visible = False
                intTelefonoCaracteristica.Visible = False
                intTelefonoNumero.Visible = iVisible

                intTelefonoCodigoArea.Enabled = iEnabled
                intTelefonoCaracteristica.Enabled = iEnabled
                intTelefonoNumero.Enabled = iEnabled

                intTelefonoNumero.MaxLength = 10

                If iEnabled Then
                    intTelefonoNumero.Attributes.Add("PlaceHolder", "1023456789")
                End If

                If iVisible AndAlso iEnabled AndAlso iObligatorio Then
                    intTelefonoNumero.CssClass = "form-control celular-numero input-requerido"
                    valTelefonoNumero.Visible = iObligatorio
                Else
                    intTelefonoNumero.CssClass = "form-control celular-numero"
                End If
        End Select
    End Sub
    Private Function obtenerTelefonoCodigoArea() As String
        Try
            iTelefonoCodigoArea = intTelefonoCodigoArea.Text
            Return iTelefonoCodigoArea
        Catch ex As Exception
        End Try
    End Function
    Private Function obtenerTelefonoCaracteristica() As String
        Try
            iTelefonoCaracteristica = intTelefonoCaracteristica.Text
            Return iTelefonoCaracteristica
        Catch ex As Exception
        End Try
    End Function
    Private Function obtenerTelefonoNumero() As String
        Try
            iTelefonoNumero = intTelefonoNumero.Text
            Return iTelefonoNumero
        Catch ex As Exception
        End Try
    End Function
    Private Sub setearTelefonoCodigoArea(eTelefonoCodigoArea As String)
        intTelefonoCodigoArea.Text = eTelefonoCodigoArea
        iTelefonoCodigoArea = eTelefonoCodigoArea
    End Sub
    Private Sub setearTelefonoCaracteristica(eTelefonoCaracteristica As String)
        intTelefonoCaracteristica.Text = eTelefonoCaracteristica
        iTelefonoCaracteristica = eTelefonoCaracteristica
    End Sub
    Private Sub setearTelefonoNumero(eTelefonoNumero As String)
        intTelefonoNumero.Text = eTelefonoNumero
        iTelefonoNumero = eTelefonoNumero
    End Sub
    Protected Sub onFocusOut()
        intTelefonoCodigoArea.Attributes.Add("onfocusout", "validarCombos" & iPrefijo & "();")
        intTelefonoCodigoArea.Attributes.Add("onkeyup", "SetContextKeyCodigoArea" & iPrefijo & "();")
        intTelefonoCaracteristica.Attributes.Add("onfocusout", "validarCombos" & iPrefijo & "();")
        intTelefonoCaracteristica.Attributes.Add("onkeyup", "SetContextKeyCaracteristica" & iPrefijo & "();")
        intTelefonoNumero.Attributes.Add("onfocusout", "validarCombos" & iPrefijo & "();")
    End Sub
#End Region

#Region "Metodos publicos"
    Public Function validar() As Boolean
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"

                If iObligatorio Then
                    If Len(intTelefonoCodigoArea.Text) + Len(intTelefonoCaracteristica.Text) + Len(intTelefonoNumero.Text) < 10 Then
                        alertCustom(lblTelefono.Text & " inválido, la cantidad de dígitos ingresados deben ser 10")
                        Page.SetFocus(intTelefonoCodigoArea)
                        Return False
                    End If
                End If

                If False Then
                    'intTelefonoCodigoArea.Text = ""
                    'intTelefonoCaracteristica.Text = ""
                    'intTelefonoNumero.Text = ""
                    alertCustom(lblTelefono.Text & " inválido, verifique que el código de área sea existente y que los campos estén completos")
                    Page.SetFocus(intTelefonoCodigoArea)
                    Return False
                End If

                Return True

            Case "URUGUAY"

                If iObligatorio Then
                    If Len(intTelefonoCodigoArea.Text) + Len(intTelefonoCaracteristica.Text) + Len(intTelefonoNumero.Text) < 8 OrElse
                    Len(intTelefonoCodigoArea.Text) + Len(intTelefonoCaracteristica.Text) + Len(intTelefonoNumero.Text) > 9 Then
                        alertCustom(lblTelefono.Text & " inválido, la suma de los dígitos debe estar entre 8 y 9")
                        Page.SetFocus(intTelefonoCodigoArea)
                        Return False
                    End If
                End If

                Return True

            Case "PARAGUAY"

                If iObligatorio Then
                    If Len(intTelefonoCodigoArea.Text) + Len(intTelefonoCaracteristica.Text) + Len(intTelefonoNumero.Text) < 9 OrElse
                    Len(intTelefonoCodigoArea.Text) + Len(intTelefonoCaracteristica.Text) + Len(intTelefonoNumero.Text) > 12 Then
                        alertCustom(lblTelefono.Text & " inválido, la suma de los dígitos debe estar entre 9 y 12")
                        Page.SetFocus(intTelefonoCodigoArea)
                        Return False
                    End If
                End If

                Return True

            Case "COLOMBIA"

                If iObligatorio Then
                    If Len(intTelefonoNumero.Text) < 10 Then
                        alertCustom(lblTelefono.Text & " inválido, la cantidad de dígitos ingresados deben ser 10")
                        Page.SetFocus(intTelefonoNumero)
                        Return False
                    End If
                End If

                Return True

        End Select
    End Function
    Public Sub SetFocus()
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                Page.SetFocus(intTelefonoCodigoArea)
            Case "URUGUAY"
                Page.SetFocus(intTelefonoCodigoArea)
            Case "PARAGUAY"
                Page.SetFocus(intTelefonoCodigoArea)
            Case "COLOMBIA"
                Page.SetFocus(intTelefonoNumero)
        End Select
    End Sub
#End Region

#Region "Eventos pantalla"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            setearControl()
        End If
        scriptAutocomplete()
        onFocusOut()
    End Sub
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        lblTelefono.ID = lblTelefono.ID & iPrefijo
        intTelefonoCodigoArea.ID = intTelefonoCodigoArea.ID & iPrefijo
        intTelefonoCaracteristica.ID = intTelefonoCaracteristica.ID & iPrefijo
        intTelefonoNumero.ID = intTelefonoNumero.ID & iPrefijo
        intTelefonoCodigoArea_AutoCompleteExtender.ID = intTelefonoCodigoArea_AutoCompleteExtender.ID & iPrefijo
        intTelefonoCaracteristica_AutoCompleteExtender.ID = intTelefonoCaracteristica_AutoCompleteExtender.ID & iPrefijo
        valTelefonoCodigoArea.ID = valTelefonoCodigoArea.ID & iPrefijo
        valTelefonoCaracteristica.ID = valTelefonoCaracteristica.ID & iPrefijo
        valTelefonoNumero.ID = valTelefonoNumero.ID & iPrefijo

        intTelefonoCodigoArea_AutoCompleteExtender.TargetControlID = intTelefonoCodigoArea.ID
        intTelefonoCaracteristica_AutoCompleteExtender.TargetControlID = intTelefonoCaracteristica.ID
        valTelefonoCodigoArea.ControlToValidate = intTelefonoCodigoArea.ID
        valTelefonoCaracteristica.ControlToValidate = intTelefonoCaracteristica.ID
        valTelefonoNumero.ControlToValidate = intTelefonoNumero.ID
    End Sub
    Protected Overrides Sub LoadViewState(ByVal viewState As Object)
        If viewState IsNot Nothing Then
            For i = 0 To viewState.First.Count - 1
                If TypeOf viewState.First(i) Is UI.IndexedString Then
                    Select Case CType(viewState.First(i), UI.IndexedString).Value
                        Case "iObligatorio"
                            iObligatorio = viewState.First(i + 1)
                        Case "iEnabled"
                            iEnabled = viewState.First(i + 1)
                        Case "iVisible"
                            iVisible = viewState.First(i + 1)
                        Case "iTelefonoCelular"
                            iTelefonoCelular = viewState.First(i + 1)
                        Case "iPrefijo"
                            iPrefijo = viewState.First(i + 1)
                    End Select
                End If
            Next
            MyBase.LoadViewState(viewState)
        End If
    End Sub
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        'Llamo a mi base antes que nada
        MyBase.OnPreRender(e)
        setearControl()
    End Sub
    Protected Overrides Function SaveViewState() As Object
        ViewState("iObligatorio") = iObligatorio
        ViewState("iEnabled") = iEnabled
        ViewState("iVisible") = iVisible
        ViewState("iTelefonoCelular") = iTelefonoCelular
        ViewState("iPrefijo") = iPrefijo
        Return MyBase.SaveViewState()
    End Function
#End Region

End Class