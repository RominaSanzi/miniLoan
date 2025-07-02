Imports System.ComponentModel
Imports System.Web.UI

<ValidationPropertyAttribute("Text"), ParseChildren(False), DefaultProperty("Text"), _
ToolboxData("<{0}:TimeBox  runat=server></{0}:TimeBox>")> _
Public Class TimeBox

    Inherits System.Web.UI.WebControls.TextBox

#Region "Constructor"
    Public Sub New()
        MyBase.New()
        MyBase.CssClass = "Textbox"
        MyBase.MaxLength = 8        'La hora en formato: hh/MM/ss
        Width = Me.Width.Pixel(76)   'Valor por defecto
        AllowNull = True
    End Sub
#End Region

#Region "Propiedades"
    'Permite que el texto del control se seleccione cuando obtiene el foco.
    Private _SelectOnFocus As Boolean = True
    'Permite habilitar o no la característica de Autocompletar de IE mas allá de la conf. del usuario
    Private _AutoComplete As Boolean = True
    'En el caso que se quiera permitir un fecha nula (espacio)
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

    'Para permitir hh:MM:ss
    Public Shadows ReadOnly Property MaxLength() As Integer
        Get
            Return 8
        End Get
    End Property

    'Esta propiedad permite asignar y/o leer el valor del control
    Property Value() As Date
        Get
            If Me.Text = String.Empty Then
                Return Nothing
            Else
                Try
                    '         Return Date.Parse(MyBase.Text.Substring(MyBase.Text.Length - 2) + _
                    '":" + MyBase.Text.Substring(3, 2) + ":" + MyBase.Text.Substring(0, 2))
                    Return Date.Parse(MyBase.Text.Substring(0, 2) + _
           ":" + MyBase.Text.Substring(3, 2) + ":" + MyBase.Text.Substring(MyBase.Text.Length - 2))
                Catch
                    Return Nothing
                End Try
            End If
        End Get

        Set(ByVal Value As Date)
            dim Hora As String = "00" + CStr(Value.Hour)
            dim Minuto As String = "00" + CStr(Value.Minute)
            dim Segundo As String = "00" + CStr(Value.Second)
            MyBase.Text = Hora.Substring(Hora.Length - 2) + ":" + Minuto.Substring(Minuto.Length - 2) + _
      ":" + Segundo.Substring(Segundo.Length - 2)
        End Set
    End Property

    <Category("Behavior"), DefaultValue(False), _
    Description("Permite dejar el campo vacío, de manera que la Hora sea nula.")> _
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
        If Not MyBase.Page.IsClientScriptBlockRegistered("TimeBox") Then
            'Registro todo el script del control
            Page.RegisterClientScriptBlock("TimeBox", FuncionJava())
        End If
    End Sub

    Private Function FuncionJava() As String
        dim f As String
        f = "<script language=""javascript""> " & vbNewLine & _
        "function FiltroHora() { " & vbNewLine & _
        "   var key = window.event.keyCode; " & vbNewLine & _
        "   if ( key > 47 && key < 58) " & vbNewLine & _
        "      return; " & vbNewLine & _
        "   else { " & vbNewLine & _
        "      window.event.returnValue = null;  " & vbNewLine & _
        "      window.event.keyCode = null;  " & vbNewLine & _
        "   } " & vbNewLine & _
        "} " & vbNewLine & _
        "function FormatoHora(NombreControl, PermiteNulo) { " & vbNewLine & _
        "   var valor = document.all[NombreControl].value; " & vbNewLine & _
        "   var hora, minuto, segundo; " & vbNewLine & _
        "   if (valor == '' && PermiteNulo == true) return;" & vbNewLine & _
        "   longitud = valor.length; " & vbNewLine & _
        "   if (longitud == 6) { " & vbNewLine & _
        "      hora = valor.substring(0,2); " & vbNewLine & _
        "      minuto = valor.substring(2,4); " & vbNewLine & _
        "      segundo = valor.substring(4,6); " & vbNewLine & _
        "   document.all[NombreControl].value = hora + ':' + minuto + ':' + segundo ; " & vbNewLine & _
        "   } " & vbNewLine & _
        "   if (longitud == 4) { " & vbNewLine & _
        "      hora = valor.substring(0,2); " & vbNewLine & _
        "      minuto = valor.substring(2,4); " & vbNewLine & _
        "      segundo = '00'; " & vbNewLine & _
        "   document.all[NombreControl].value = hora + ':' + minuto + ':' + segundo ; " & vbNewLine & _
        "   } " & vbNewLine & _
        "   if (longitud != 6 && longitud != 4 && longitud != 8){ " & vbNewLine & _
        "   document.all[NombreControl].value = '' ; " & vbNewLine & _
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

        'El siguiente atributo los pone el IDE de Visual Studio en el html del webform,
        'pero solo cuando uno cambia la propiedad AllowNull.
        'Pero como yo lo necesito siempre (ya que lo usa el DateValidator) lo pongo por las dudas,
        'si el IDE ya lo puso, no hay problema, no se duplica :)
        MyBase.Attributes.Add("AllowNull", CStr(_AllowNull).ToLower)

        'Aqui se verifica si el control esta dentro de un contenedor de controles (Repeater o Datalist)
        'Si es así, se modifica el nombre del control...
        dim NombreContenedor As String = Me.NamingContainer.ClientID
        dim NombreControl As String

        NombreControl = Me.ID

        MyBase.Attributes.Add("onkeypress", "FiltroHora()")
        MyBase.Attributes.Add("onblur", "FormatoHora('" + NombreControl + "', " + _
     _AllowNull.ToString.ToLower + ")")
        MyBase.Render(writer)
    End Sub
#End Region

End Class

