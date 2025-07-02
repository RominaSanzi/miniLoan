Imports System.ComponentModel
Imports System.Web.UI

<ValidationPropertyAttribute("Text"), ParseChildren(False), DefaultProperty("Text"), _
ToolboxData("<{0}:TextBoxUpLetras runat=server></{0}:TextBoxUpLetras>")> _
Public Class TextboxUpLetrasComas

    Inherits System.Web.UI.WebControls.TextBox

#Region "Constructor"
    Public Sub New()
        MyBase.New()
        MyBase.MaxLength = 20
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
                Return CLng(Me.Text)
            End If
        End Get

        Set(ByVal Value As Integer)
            MyBase.Text = CLng(Value)
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
        If Not MyBase.Page.ClientScript.IsClientScriptBlockRegistered("TextBoxUp") Then
            'Registro todo el script del control
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "TextBoxUp", FuncionJava())
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "TextBoxUp", FuncionJava())
        End If
    End Sub

    Private Function FuncionJava() As String
        Dim f As String
        f = "<script language=""javascript""> " & vbNewLine & _
        "function FiltroUpLetrasComas() { " & vbNewLine & _
        "   var key = window.event.keyCode; " & vbNewLine & _
        "   if (((key > 64) && (key < 91)) || ((key > 96) && (key < 123)) || (key == 241) || (key == 209) || (key == 32) || (key == 44) || (key == 46) ) {" & vbNewLine & _
        "         if ( key == 46) " & vbNewLine & _
        "         {" & vbNewLine & _
        "             window.event.returnValue = null;" & vbNewLine & _
        "             window.event.keyCode = null;  " & vbNewLine & _
        "             document.all[NombreControl].value = document.all[NombreControl].value + ',';" & vbNewLine & _
        "         }" & vbNewLine & _
        "         else " & vbNewLine & _
        "             return;  " & vbNewLine & _
        "   } else {  " & vbNewLine & _
        "       window.event.returnValue = null;  " & vbNewLine & _
        "       window.event.keyCode = null;  " & vbNewLine & _
        "       } " & vbNewLine & _
        "} " & vbNewLine & _
        "</script> " & vbNewLine & _
         "<script language=""javascript""> " & vbNewLine & _
              "function ChangeCaseLetras(NombreControl)" & vbNewLine & _
             "{" & vbNewLine & _
            " document.all[NombreControl].value = document.all[NombreControl].value.toUpperCase();" & vbNewLine & _
           "}" & vbNewLine & _
          "</script>" & vbNewLine
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

        'NombreControl = Me.ID
        NombreControl = Me.NamingContainer.FindControl(Me.ID).ClientID

        MyBase.Attributes.Add("onkeypress", "FiltroUpLetrasComas()")
        MyBase.Attributes.Add("onblur", "ChangeCaseLetras('" & NombreControl & "');")
        MyBase.Render(writer)
    End Sub
#End Region

End Class
