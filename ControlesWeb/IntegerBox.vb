Imports System.ComponentModel
Imports System.Web.UI

<ValidationPropertyAttribute("Text"), ParseChildren(False), DefaultProperty("Text"), _
ToolboxData("<{0}:IntegerBox runat=server></{0}:IntegerBox>")> _
Public Class IntegerBox

    Inherits System.Web.UI.WebControls.TextBox

#Region "Constructor"
    Public Sub New()
        MyBase.New()
        MyBase.MaxLength = 10
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
    Private _ReadOnly As Boolean = False
    Private iUsaSeparadorPuntos As Boolean = True
    Private iVista As Boolean = False
    Private iCambioValor As Boolean = False

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

    Property UsaSeparadorPuntos() As Boolean
        Get
            Return iUsaSeparadorPuntos
        End Get

        Set(ByVal Value As Boolean)
            iUsaSeparadorPuntos = Value
        End Set
    End Property

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

    Public Property vista As Boolean
        Get
            Return iVista
        End Get
        Set(value As Boolean)
            iVista = value
        End Set
    End Property

    <ComponentModel.Browsable(True)>
    <ComponentModel.DesignerSerializationVisibility(ComponentModel.DesignerSerializationVisibility.Hidden)>
    <ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    <ComponentModel.Bindable(True)>
    Public Overrides Property Text As String
        Get
            If iUsaSeparadorPuntos AndAlso Not iVista Then
                Return If(MyBase.Text <> Nothing AndAlso MyBase.Text <> String.Empty, CLng(MyBase.Text), MyBase.Text)
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
                    iValorActual = CLng(iValorActual.Replace(".", ""))
                End If
                If Len(Value) > 0 Then iValorNuevo = CLng(Value.Replace(".", ""))
                iCambioValor = iValorActual <> iValorNuevo 'Verificamos si realmente cambió a nivel de valor numérico, no de expresión
            Catch ex As Exception
                iCambioValor = True
            End Try
            MyBase.Text = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        'Llamo a mi base antes que nada
        MyBase.OnPreRender(e)

        If iUsaSeparadorPuntos Then
            vista = True
            Try
                MyBase.Text = If(Me.Text <> Nothing AndAlso Me.Text.Trim <> Nothing, Format(CDbl(Me.Text), "#,#"), Me.Text)
            Catch ex As Exception
                MyBase.Text = Me.Text
            End Try
        End If

        'Si el script no esta registrado...
        If Not MyBase.Page.ClientScript.IsClientScriptBlockRegistered("IntegerBox") Then
            'Registro todo el script del control
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "IntegerBox", FuncionJava())
        End If
    End Sub

    Private Function FuncionJava() As String
        Dim f As String
        f = "<script language=""javascript""> " & vbNewLine & _
        "function FiltroInteger() { " & vbNewLine & _
        "   var key = window.event.keyCode; " & vbNewLine & _
        "   if ( (key > 47 && key < 58) || key == 13) " & vbNewLine & _
        "      return; " & vbNewLine & _
        "   else { " & vbNewLine & _
        "      window.event.returnValue = null;  " & vbNewLine & _
        "      window.event.keyCode = null;  " & vbNewLine & _
        "   } " & vbNewLine & _
        "} " & vbNewLine & _
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

        If iUsaSeparadorPuntos Then
            MyBase.Attributes.Add("onkeyup", "formatearConPunto(this, event)")
            Me.MaxLength = Me.MaxLength + (Me.MaxLength Mod 3)
        End If

        MyBase.Attributes.Add("onkeypress", "FiltroInteger()")
        MyBase.Render(writer)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        If iCambioValor Then MyBase.OnTextChanged(e)
    End Sub
#End Region

End Class