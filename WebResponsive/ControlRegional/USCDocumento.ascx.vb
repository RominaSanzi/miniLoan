Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports di.financiera.entidades

Public Class USCDocumento
    Inherits System.Web.UI.UserControl

#Region "Variables"
    Private iText As String
    Private iObligatorio As Boolean = False
    Private iAutoPostBack As Boolean = False
    Private iEnabled As Boolean = True
    Private iValidarDNI As Boolean
    Private iBackColor As System.Drawing.Color
    Private iPrefijo As String
#End Region

#Region "Atributos"
    Public Property Text() As String
        Get
            Return obtenerDocumento()
        End Get
        Set(ByVal value As String)
            setearDocumento(value)
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
    Public Property ValidarDNI() As Boolean
        Get
            Return iValidarDNI
        End Get
        Set(ByVal value As Boolean)
            iValidarDNI = value
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
#End Region

#Region "Eventos"
    Public Event USCDocumento_TextChanged As EventHandler
#End Region

#Region "Eventos controles"
    Protected Overridable Sub intDocumento_TextChanged(sender As Object, e As EventArgs) Handles intDocumento.TextChanged
        RaiseEvent USCDocumento_TextChanged(sender, e)
    End Sub
    Private Sub valintDocumento_Init(sender As Object, e As EventArgs) Handles valintDocumento.Init
        valintDocumento.ControlToValidate = intDocumento.ID & iPrefijo
    End Sub
#End Region

#Region "Metodos privados"
    Protected Sub setearControl()

        If Not IsNothing(iBackColor) Then
            intDocumento.BackColor = iBackColor
            intDocumento.BackColor = iBackColor
        End If

        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                If iObligatorio Then
                    valintDocumento.Visible = True
                    valintDocumento.ErrorMessage = "El Documento es un campo requerido."
                    lblDocumento.Text = "Documento *"
                    intDocumento.CssClass = "form-control input-requerido"
                End If
                intDocumento.MaxLength = 8
                intDocumento.Visible = True
                intDocumento.AutoPostBack = iAutoPostBack
                intDocumento.Enabled = iEnabled
            Case "URUGUAY"
                If iValidarDNI Then onblur()
                If iObligatorio Then
                    valintDocumento.Visible = True
                    valintDocumento.ErrorMessage = "El Número es un campo requerido."
                    lblDocumento.Text = "Número *"
                    intDocumento.CssClass = "form-control input-requerido"
                Else
                    lblDocumento.Text = "Número"
                End If
                intDocumento.Visible = True
                intDocumento.MaxLength = 8
                intDocumento.AutoPostBack = iAutoPostBack
                intDocumento.Enabled = iEnabled
            Case "PARAGUAY"
                If iObligatorio Then
                    valintDocumento.Visible = True
                    valintDocumento.ErrorMessage = "El Ruc es un campo requerido."
                    lblDocumento.Text = "Número *"
                    intDocumento.CssClass = "form-control input-requerido"
                Else
                    lblDocumento.Text = "Número"
                End If
                intDocumento.Visible = True
                intDocumento.MaxLength = 7
                intDocumento.AutoPostBack = iAutoPostBack
                intDocumento.Enabled = iEnabled
            Case "COLOMBIA"
                If iObligatorio Then
                    valintDocumento.Visible = True
                    valintDocumento.ErrorMessage = "La cédula es un campo requerido."
                    lblDocumento.Text = "Cédula *"
                    intDocumento.CssClass = "form-control input-requerido"
                Else
                    lblDocumento.Text = "Cédula"
                End If
                intDocumento.MaxLength = 12
                intDocumento.Visible = True
                intDocumento.AutoPostBack = iAutoPostBack
                intDocumento.Enabled = iEnabled
        End Select
    End Sub
    Private Function obtenerDocumento() As String
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                iText = FuncionComun.ceroSiEsVacio(intDocumento.Text)
            Case "URUGUAY"
                iText = FuncionComun.ceroSiEsVacio(intDocumento.Text)
            Case "PARAGUAY"
                iText = FuncionComun.ceroSiEsVacio(intDocumento.Text)
            Case "COLOMBIA"
                iText = FuncionComun.ceroSiEsVacio(intDocumento.Text)
        End Select
        Return iText
    End Function
    Private Sub setearDocumento(eText As String)
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                intDocumento.Text = eText
            Case "URUGUAY"
                intDocumento.Text = eText
            Case "PARAGUAY"
                intDocumento.Text = eText
            Case "COLOMBIA"
                intDocumento.Text = eText
        End Select
        iText = eText
    End Sub
    Protected Sub onblur()
        intDocumento.Attributes.Add("onblur", "calcularVerificador(this);")
    End Sub
#End Region

#Region "Metodos publicos"
    Public Sub SetFocus()
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                Page.SetFocus(intDocumento)
            Case "URUGUAY"
                Page.SetFocus(intDocumento)
            Case "PARAGUAY"
                Page.SetFocus(intDocumento)
            Case "COLOMBIA"
                Page.SetFocus(intDocumento)
        End Select
    End Sub
#End Region

#Region "Eventos pagina"
    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            setearControl()
        End If
    End Sub
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        lblDocumento.ID = lblDocumento.ID & iPrefijo
        intDocumento.ID = intDocumento.ID & iPrefijo
        intDocumento.ID = intDocumento.ID & iPrefijo
        valintDocumento.ID = valintDocumento.ID & iPrefijo
        valintDocumento.ID = valintDocumento.ID & iPrefijo

        valintDocumento.ControlToValidate = intDocumento.ID
        valintDocumento.ControlToValidate = intDocumento.ID
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
        Return MyBase.SaveViewState()
    End Function
#End Region

End Class