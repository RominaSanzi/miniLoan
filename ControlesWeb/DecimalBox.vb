Imports System.ComponentModel
Imports System.Configuration
Imports System.Web.UI

<ValidationPropertyAttribute("Text"), ParseChildren(False), DefaultProperty("Text"),
ToolboxData("<{0}:DecimalBox runat=server></{0}:DecimalBox>")>
Public Class DecimalBox

    Inherits System.Web.UI.WebControls.TextBox

#Region "Constructor"
    Public Sub New()
        MyBase.New()
        MyBase.MaxLength = 11
        Width = Me.Width.Pixel(76)   'Valor por defecto
    End Sub
#End Region

#Region "Propiedades"
    'Permite que el texto del control se seleccione cuando obtiene el foco.
    Private _SelectOnFocus As Boolean = True
    'Permite habilitar o no la característica de Autocompletar de IE mas allá de la conf. del usuario
    Private _AutoComplete As Boolean = True
    'En el caso que se quiera permitir un valor nulo(espacio)
    Private _AllowNull As Boolean
    Private iUsaSeparadorPuntos As Boolean = True
    Private iVista As Boolean = False
    Private iCambioValor As Boolean = False

    <Category("Behavior"), DefaultValue("True"),
    Description("Permite que el TextBox se seleccione al tomar el foco")>
    Public Property SelectOnFocus() As Boolean
        Get
            Return _SelectOnFocus
        End Get
        Set(ByVal Value As Boolean)
            _SelectOnFocus = Value
        End Set
    End Property

    <Category("Behavior"), DefaultValue("True"),
    Description("Permite activar o anular la opcion de autocompletar de IExplorer")>
    Public Property AutoComplete() As Boolean
        Get
            Return _AutoComplete
        End Get
        Set(ByVal Value As Boolean)
            _AutoComplete = Value
        End Set
    End Property

    'Property Value() As Double
    '    Get
    '        If Me.Text = String.Empty Then
    '            Return Nothing
    '        Else
    '            Return CDbl(Me.Text)
    '        End If
    '    End Get

    '    Set(ByVal Value As Double)
    '        MyBase.Text = CDbl(Value)
    '    End Set
    'End Property

    <Category("Behavior"), DefaultValue(False),
    Description("Permite dejar el campo vacío")>
    Property AllowNull() As Boolean
        Get
            Return _AllowNull
        End Get
        Set(ByVal Value As Boolean)
            _AllowNull = Value
        End Set
    End Property

    Property UsaSeparadorPuntos() As Boolean
        Get
            Return iUsaSeparadorPuntos
        End Get

        Set(ByVal Value As Boolean)
            iUsaSeparadorPuntos = Value
        End Set
    End Property

    Public Property vista As Boolean
        Get
            Return iVista
        End Get
        Set(value As Boolean)
            iVista = value
        End Set
    End Property
#End Region

    <ComponentModel.Browsable(True)>
    <ComponentModel.DesignerSerializationVisibility(ComponentModel.DesignerSerializationVisibility.Hidden)>
    <ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    <ComponentModel.Bindable(True)>
    Public Overrides Property Text As String
        Get
            If iUsaSeparadorPuntos AndAlso Not iVista Then
                Return If(MyBase.Text <> Nothing, CDbl(MyBase.Text), MyBase.Text)
            Else
                Return MyBase.Text
            End If
        End Get

        Set
            Try
                Dim iValorActual, iValorNuevo As String
                iValorNuevo = ""
                iValorActual = MyBase.Text
                If Len(iValorActual) > 0 Then
                    If iValorActual.Contains(".") AndAlso iValorActual.Contains(",") Then iValorActual = iValorActual.Replace(".", "") 'si vienen ambas cosas tengo que eliminar la coma que separa los miles
                    iValorActual = CDbl(iValorActual.Replace(".", ","))
                End If
                If Len(Value) > 0 Then iValorNuevo = CDbl(Value.Replace(".", ""))
                iCambioValor = iValorActual <> iValorNuevo 'Verificamos si realmente cambió a nivel de valor numérico, no de expresión
            Catch ex As Exception
                iCambioValor = True
            End Try
            MyBase.Text = Value
        End Set
    End Property

#Region "Metodos"
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        'Llamo a mi base antes que nada
        MyBase.OnPreRender(e)

        If iUsaSeparadorPuntos Then
            vista = True

            Try
                MyBase.Text = If(Me.Text <> Nothing AndAlso Me.Text.Trim <> Nothing, Format(CDbl(Me.Text), "#,#0.00"), Me.Text)
            Catch ex As Exception
                MyBase.Text = Me.Text
            End Try
        End If

        'Si el script no esta registrado...
        If Not MyBase.Page.ClientScript.IsClientScriptBlockRegistered("DecimalBox") Then
            'Registro todo el script del control
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "DecimalBox", FuncionJava())
        End If
    End Sub

    Private Function FuncionJava() As String
        Dim f As String
        f = vbNewLine & "<script language=""javascript""> " & vbNewLine &
        "function FiltroDecimal(NombreControl) { " & vbNewLine &
        "   var key = window.event.keyCode; " & vbNewLine &
        "   var valor = document.all[NombreControl].value; " & vbNewLine &
        "   var posi = valor.indexOf(','); " & vbNewLine &
        "   if ( (key > 47 && key < 58 || key == 46 || key == 44) || key == 13 ) " & vbNewLine &
        "      if ( key == 46 && posi >= 0 || key == 44 && posi >= 0) " & vbNewLine &
        "        { window.event.returnValue = null;  " & vbNewLine &
        "          window.event.keyCode = null;  " & vbNewLine &
        "         }" & vbNewLine &
        "      else { " & vbNewLine &
        "         if ( key == 46) " & vbNewLine &
        "         {" & vbNewLine &
        "             window.event.returnValue = null;" & vbNewLine &
        "             window.event.keyCode = null;  " & vbNewLine &
        "             document.all[NombreControl].value = document.all[NombreControl].value + ',';" & vbNewLine &
        "         }" & vbNewLine &
        "         else " & vbNewLine &
        "             return;  " & vbNewLine &
        "   } else { " & vbNewLine &
        "      window.event.returnValue = null;  " & vbNewLine &
        "      window.event.keyCode = null;  " & vbNewLine &
        "   } " & vbNewLine &
        "} " & vbNewLine &
        "</script> " & vbNewLine
        f.Replace(" ", "")
        Return f
    End Function

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        If _SelectOnFocus Then
            MyBase.Attributes.Add("onfocus", "this.select()")
        End If
        If Not _AutoComplete Then
            MyBase.Attributes.Add("autocomplete", "off")
        End If

        MyBase.Attributes.Add("AllowNull", CStr(_AllowNull).ToLower)
        Dim NombreContenedor As String = Me.NamingContainer.ClientID
        Dim NombreControl As String

        NombreControl = Me.NamingContainer.FindControl(Me.ID).ClientID

        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                Me.MaxLength = 11
            Case "URUGUAY"
                Me.MaxLength = 11
            Case "PARAGUAY"
                Me.MaxLength = 20
            Case "COLOMBIA"
                Me.MaxLength = 11
        End Select

        Me.MaxLength = Me.MaxLength + (Me.MaxLength \ 2)

        If iUsaSeparadorPuntos Then MyBase.Attributes.Add("onkeyup", "formatearDecimal('" & NombreControl & "')")
        MyBase.Attributes.Add("onkeypress", "return FiltroDecimal('" & NombreControl & "')")
        MyBase.Render(writer)

    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        If iCambioValor Then MyBase.OnTextChanged(e)
    End Sub

#End Region

End Class