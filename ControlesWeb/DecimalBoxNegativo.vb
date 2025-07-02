Imports System.ComponentModel
Imports System.Web.UI

<ValidationPropertyAttribute("Text"), ParseChildren(False), DefaultProperty("Text"), _
ToolboxData("<{0}:DecimalBoxNegativo runat=server></{0}:DecimalBoxNegativo>")> _
Public Class DecimalBoxNegativo

    Inherits System.Web.UI.WebControls.TextBox

#Region "Constructor"
    Public Sub New()
        MyBase.New()
        MyBase.CssClass = "Textbox"
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

    <Category("Behavior"), DefaultValue("True"), _
    Description("Permite que el TextBox se seleccione al tomar el foco")> _
    Public Property SelectOnFocus() As Boolean
        Get
            Return _SelectOnFocus
        End Get
        Set(ByVal Value As Boolean)
            _SelectOnFocus = Value
        End Set
    End Property

    <Category("Behavior"), DefaultValue("True"), _
    Description("Permite activar o anular la opcion de autocompletar de IExplorer")> _
    Public Property AutoComplete() As Boolean
        Get
            Return _AutoComplete
        End Get
        Set(ByVal Value As Boolean)
            _AutoComplete = Value
        End Set
    End Property

    Property Value() As Integer
        Get
            If Me.Text = String.Empty Then
                Return Nothing
            Else
                Return CInt(Me.Text)
            End If
        End Get

        Set(ByVal Value As Integer)
            MyBase.Text = CInt(Value)
        End Set
    End Property

    <Category("Behavior"), DefaultValue(False), _
    Description("Permite dejar el campo vacío")> _
    Property AllowNull() As Boolean
        Get
            Return _AllowNull
        End Get
        Set(ByVal Value As Boolean)
            _AllowNull = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        'Llamo a mi base antes que nada
        MyBase.OnPreRender(e)

        'Si el script no esta registrado...
        If Not MyBase.Page.IsClientScriptBlockRegistered("DecimalBoxNegativo") Then
            'Registro todo el script del control
            Page.RegisterClientScriptBlock("DecimalBoxNegativo", FuncionJava())
        End If
    End Sub

    'Para punto
    '    Private Function FuncionJava() As String
    '        Dim f As String
    '        f = "<script language=""javascript""> " & _
    '        "function FiltroDecimal(NombreControl) { " & _
    '        "   var key = window.event.keyCode; " & _
    '        "   var valor = document.all[NombreControl].value; " & _
    '        "   var posi = valor.indexOf('.'); " & _
    '        "   if ( key > 47 && key < 58 || key == 46 ) " & _
    '        "      if ( key == 46 && posi >= 0) " & _
    '        "         window.event.returnValue = null;  " & _
    '        "      else { " & _
    '        "         return;  " & _
    '        "   } else { " & _
    '        "      window.event.returnValue = null;  " & _
    '        "   } " & _
    '        "} " & _
    '        "</script> "
    '        f.Replace(" ", "")
    '        Return f
    '    End Function


    Private Function FuncionJava() As String
        Dim g As String
        g = "<script language=""javascript""> " & vbNewLine & _
        "function FiltroDecimalNegativo(NombreControl) { " & vbNewLine & _
        "   var key = window.event.keyCode; " & vbNewLine & _
        "   var valor = document.all[NombreControl].value; " & vbNewLine & _
        "   var posi = valor.indexOf(','); " & vbNewLine & _
        "   var posnegat = valor.indexOf('-'); " & vbNewLine & _
        "   if ( key > 47 && key < 58 || key == 46 || key == 44 || key == 45 ) " & vbNewLine & _
        "      if ( key == 46 && posi >= 0 || key == 44 && posi >= 0 && key != 45) " & vbNewLine & _
        "         window.event.returnValue = null;  " & vbNewLine & _
        "         window.event.keyCode = null;  " & vbNewLine & _
        "      else { " & vbNewLine & _
        "         if ( key == 46) " & vbNewLine & _
        "             window.event.keyCode = 44;" & vbNewLine & _
        "         else " & vbNewLine & _
        "             return;  " & vbNewLine & _
        "         if ( key == 45 && posnegat >= 0) " & vbNewLine & _
        "             window.event.returnValue = null;" & vbNewLine & _
        "             window.event.keyCode = null;  " & vbNewLine & _
        "         else " & vbNewLine & _
        "             return;  " & vbNewLine & _
        "   } else { " & vbNewLine & _
        "      window.event.returnValue = null;  " & vbNewLine & _
        "      window.event.keyCode = null;  " & vbNewLine & _
        "   } " & vbNewLine & _
        "} " & vbNewLine & _
        "</script> " & vbNewLine
        g.Replace(" ", "")
        Return g
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
        If NombreContenedor = "" Then
            NombreControl = Me.ID
        Else
            NombreControl = NombreContenedor + "_" + Me.ID
        End If

        MyBase.Attributes.Add("onkeypress", "return FiltroDecimalNegativo('" & NombreControl & "')")
        MyBase.Render(writer)
    End Sub
#End Region

End Class