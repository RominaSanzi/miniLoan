Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports di.financiera.entidades

Public Class USCCuil
    Inherits System.Web.UI.UserControl

#Region "Registro script"
    Public Sub alertCustom(ByVal eMensaje As String)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "alertCustom", "document.addEventListener('DOMContentLoaded', function(event) { site.showSwal(); Swal.fire({ type: 'info', html: '" & eMensaje & "', buttonsStyling: false,confirmButtonClass: 'btn btn-info btn-round btn-block min-width-200'}) });", True)
    End Sub
#End Region

#Region "Variables"
    Private iCuil1 As String
    Private iDocumentoCuil As String
    Private iCuil2 As String
    Private iObligatorio As Boolean = False
    Private iAutoPostBack As Boolean = False
    Private iEnabled As Boolean = True
    Private iVisible As Boolean = True
    Private iBackColor As System.Drawing.Color
    Private iPrefijo As String
    Private iText As String
    Private iEmpresa As Boolean = False
#End Region

#Region "Atributos"
    Public Property Cuil1() As String
        Get
            Return obtenerCui1()
        End Get
        Set(ByVal value As String)
            setearCui1(value)
        End Set
    End Property
    Public Property DocumentoCuil() As String
        Get
            Return obtenerDocumentoCuil()
        End Get
        Set(ByVal value As String)
            setearDocumentoCuil(value)
        End Set
    End Property
    Public Property Cuil2() As String
        Get
            Return obtenerCuil2()
        End Get
        Set(ByVal value As String)
            setearCuil2(value)
        End Set
    End Property
    Public Property Obligatorio() As Boolean
        Get
            Return iObligatorio
        End Get
        Set(ByVal value As Boolean)
            iObligatorio = value
        End Set
    End Property
    Public Property AutoPostBack() As Boolean
        Get
            Return iAutoPostBack
        End Get
        Set(ByVal value As Boolean)
            iAutoPostBack = value
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
    Public Property Text() As String
        Get
            Return iText
        End Get
        Set(ByVal value As String)
            iText = value
        End Set
    End Property
    Public Property Empresa() As Boolean
        Get
            Return iEmpresa
        End Get
        Set(ByVal value As Boolean)
            iEmpresa = value
        End Set
    End Property
#End Region

#Region "Metodos privados"
    Private Function obtenerCui1() As String
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                iCuil1 = intCuil1.Text
            Case "URUGUAY"
                iCuil1 = intCuil1.Text
            Case "PARAGUAY"
                iCuil1 = intCuil1.Text
            Case "COLOMBIA"
                iCuil1 = intCuil1.Text
        End Select
        Return iCuil1
    End Function
    Private Sub setearCui1(eCuil1 As String)
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                intCuil1.Text = eCuil1
            Case "URUGUAY"
                intCuil1.Text = eCuil1
            Case "PARAGUAY"
                intCuil1.Text = eCuil1
            Case "COLOMBIA"
                intCuil1.Text = eCuil1
        End Select
        iCuil1 = eCuil1
    End Sub
    Private Function obtenerDocumentoCuil() As String
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                iDocumentoCuil = txtDocumentoCuil.Text
            Case "URUGUAY"
                iDocumentoCuil = txtDocumentoCuil.Text
            Case "PARAGUAY"
                iDocumentoCuil = txtDocumentoCuil.Text
            Case "COLOMBIA"
                iDocumentoCuil = txtDocumentoCuil.Text
        End Select
        Return iDocumentoCuil
    End Function
    Private Sub setearDocumentoCuil(eDocumentoCuil As String)
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                If iEmpresa Then
                    txtDocumentoCuil.Text = eDocumentoCuil
                Else
                    txtDocumentoCuil.Text = Right(StrDup(8, "0") & eDocumentoCuil, 8)
                End If
            Case "URUGUAY"
                If iEmpresa Then
                    txtDocumentoCuil.Text = Right(StrDup(12, "0") & eDocumentoCuil, 12)
                Else
                    txtDocumentoCuil.Text = Right(StrDup(8, "0") & eDocumentoCuil, 8)
                End If
            Case "PARAGUAY"
                If iEmpresa Then
                    txtDocumentoCuil.Text = Right(StrDup(12, "0") & eDocumentoCuil, 12)
                Else
                    txtDocumentoCuil.Text = Right(StrDup(8, "0") & eDocumentoCuil, 8)
                End If
            Case "COLOMBIA"
                If iEmpresa Then
                    txtDocumentoCuil.Text = eDocumentoCuil
                Else
                    txtDocumentoCuil.Text = eDocumentoCuil
                End If
        End Select
        iDocumentoCuil = eDocumentoCuil
    End Sub
    Private Function obtenerCuil2() As String
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                iCuil2 = intCuil2.Text
            Case "URUGUAY"
                iCuil2 = intCuil2.Text
            Case "PARAGUAY"
                iCuil2 = intCuil2.Text
            Case "COLOMBIA"
                iCuil2 = intCuil2.Text
        End Select
        Return iCuil2
    End Function
    Private Sub setearCuil2(eCuil2 As String)
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                intCuil2.Text = FuncionComun.ceroSiEsVacio(eCuil2)
            Case "URUGUAY"
                intCuil2.Text = FuncionComun.ceroSiEsVacio(eCuil2)
            Case "PARAGUAY"
                intCuil2.Text = FuncionComun.ceroSiEsVacio(eCuil2)
            Case "COLOMBIA"
                intCuil2.Text = FuncionComun.ceroSiEsVacio(eCuil2)
        End Select
        iCuil2 = eCuil2
    End Sub
    Protected Sub setearControl()
        If Not IsNothing(iBackColor) Then
            intCuil1.BackColor = iBackColor
            txtDocumentoCuil.BackColor = iBackColor
            intCuil2.BackColor = iBackColor
        End If

        intCuil1.Enabled = iEnabled
        txtDocumentoCuil.Enabled = iEnabled
        intCuil2.Enabled = iEnabled
        lblCuil.Visible = iVisible

        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                lblCuil.Text = "CUIL " & iText
                txtDocumentoCuil.MaxLength = 8

                If iObligatorio Then
                    valCuil1.Visible = iVisible And Not iEmpresa
                    valDocumentoCuil.Visible = iVisible
                    valCuil2.Visible = iVisible And Not iEmpresa
                    valCuil1.ErrorMessage = "El Cuil 1 es un campo requerido."
                    valDocumentoCuil.ErrorMessage = "El Documento Cuil es un campo requerido."
                    valCuil2.ErrorMessage = "El Cuil 2 es un campo requerido."
                    lblCuil.Text = "CUIL " & iText & " *"
                    If iEmpresa Then
                        txtDocumentoCuil.CssClass = "form-control input-requerido solonumeros"
                        txtDocumentoCuil.MaxLength = 11
                    Else
                        intCuil1.CssClass = "form-control cuil-inicio input-requerido solonumeros"
                        txtDocumentoCuil.CssClass = "form-control cuil-documento input-requerido solonumeros"
                        intCuil2.CssClass = "form-control cuil-final input-requerido solonumeros"
                    End If
                Else
                    If iEmpresa Then
                        txtDocumentoCuil.MaxLength = 11
                        txtDocumentoCuil.CssClass = "form-control solonumeros"
                    Else
                        intCuil1.CssClass = "form-control cuil-inicio solonumeros"
                        txtDocumentoCuil.CssClass = "form-control cuil-documento solonumeros"
                        intCuil2.CssClass = "form-control cuil-final solonumeros"
                    End If
                End If
                intCuil1.MaxLength = 2
                intCuil2.MaxLength = 1
                intCuil1.Visible = iVisible And Not iEmpresa
                txtDocumentoCuil.Visible = iVisible
                intCuil2.Visible = iVisible And Not iEmpresa
            Case "URUGUAY"
                If iObligatorio Then
                    valDocumentoCuil.Visible = iVisible
                    valDocumentoCuil.ErrorMessage = "El RUT es un campo requerido."
                    If iEmpresa Then
                        lblCuil.Text = "RUC " & iText & " *"
                    Else
                        lblCuil.Text = "CI " & iText & " *"
                    End If
                    txtDocumentoCuil.CssClass = "form-control input-requerido"
                Else
                    txtDocumentoCuil.CssClass = "form-control"
                    If iEmpresa Then
                        lblCuil.Text = "RUC " & iText
                        txtDocumentoCuil.MaxLength = 12
                    Else
                        lblCuil.Text = "CI " & iText
                        txtDocumentoCuil.MaxLength = 8
                    End If
                End If
                txtDocumentoCuil.Visible = iVisible
            Case "PARAGUAY"
                If iObligatorio Then
                    valDocumentoCuil.Visible = iVisible
                    valDocumentoCuil.ErrorMessage = "El RUT es un campo requerido."
                    If iEmpresa Then
                        lblCuil.Text = "RUC " & iText & " *"
                    Else
                        lblCuil.Text = "CI " & iText & " *"
                    End If
                    txtDocumentoCuil.CssClass = "form-control input-requerido"
                Else
                    txtDocumentoCuil.CssClass = "form-control"
                    If iEmpresa Then
                        lblCuil.Text = "RUC " & iText
                        txtDocumentoCuil.MaxLength = 12
                    Else
                        lblCuil.Text = "CI " & iText
                        txtDocumentoCuil.MaxLength = 8
                    End If
                End If
                txtDocumentoCuil.Visible = iVisible
            Case "COLOMBIA"
                If iObligatorio Then
                    valDocumentoCuil.Visible = iVisible
                    valDocumentoCuil.ErrorMessage = "El RUT es un campo requerido."
                    If iEmpresa Then
                        lblCuil.Text = "RUT " & iText & " *"
                    Else
                        lblCuil.Text = "RUT " & iText & " *"
                    End If
                    txtDocumentoCuil.CssClass = "form-control input-requerido"
                Else
                    txtDocumentoCuil.CssClass = "form-control"
                    If iEmpresa Then
                        lblCuil.Text = "RUT " & iText
                        txtDocumentoCuil.MaxLength = 14
                    Else
                        lblCuil.Text = "RUT " & iText
                        txtDocumentoCuil.MaxLength = 14
                    End If
                End If
                txtDocumentoCuil.Visible = iVisible
        End Select
    End Sub
#End Region

#Region "Metodos publicos"
    Public Sub SetFocus()
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                Page.SetFocus(intCuil1)
            Case "URUGUAY"
                Page.SetFocus(txtDocumentoCuil)
            Case "PARAGUAY"
                Page.SetFocus(txtDocumentoCuil)
            Case "COLOMBIA"
                Page.SetFocus(txtDocumentoCuil)
        End Select
    End Sub
    Public Function validar() As Boolean
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
            Case "URUGUAY"
            Case "PARAGUAY"
            Case "COLOMBIA"
        End Select
        Return True
    End Function
#End Region

#Region "Eventos pagina"
    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            setearControl()
        End If
    End Sub
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        lblCuil.ID = lblCuil.ID & iPrefijo
        intCuil1.ID = intCuil1.ID & iPrefijo
        txtDocumentoCuil.ID = txtDocumentoCuil.ID & iPrefijo
        intCuil2.ID = intCuil2.ID & iPrefijo
        valCuil1.ID = valCuil1.ID & iPrefijo
        valDocumentoCuil.ID = valDocumentoCuil.ID & iPrefijo
        valCuil2.ID = valCuil2.ID & iPrefijo

        valCuil1.ControlToValidate = intCuil1.ID
        valDocumentoCuil.ControlToValidate = txtDocumentoCuil.ID
        valCuil2.ControlToValidate = intCuil2.ID
    End Sub
    Protected Overrides Sub LoadViewState(ByVal viewState As Object)
        If viewState IsNot Nothing Then
            For i = 0 To viewState.First.Count - 1
                If TypeOf viewState.First(i) Is UI.IndexedString Then
                    Select Case CType(viewState.First(i), UI.IndexedString).Value
                        Case "iAutoPostBack"
                            iAutoPostBack = viewState.First(i + 1)
                        Case "iEnabled"
                            iEnabled = viewState.First(i + 1)
                        Case "iObligatorio"
                            iObligatorio = viewState.First(i + 1)
                        Case "iPrefijo"
                            iPrefijo = viewState.First(i + 1)
                        Case "iVisible"
                            iVisible = viewState.First(i + 1)
                        Case "iEmpresa"
                            iEmpresa = viewState.First(i + 1)
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
        ViewState("iAutoPostBack") = iAutoPostBack
        ViewState("iEnabled") = iEnabled
        ViewState("iObligatorio") = iObligatorio
        ViewState("iPrefijo") = iPrefijo
        ViewState("iVisible") = iVisible
        ViewState("iEmpresa") = iEmpresa
        Return MyBase.SaveViewState()
    End Function
#End Region

End Class