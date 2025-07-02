Imports System.ComponentModel
Imports System.Web.UI

<ValidationPropertyAttribute("Text"), ParseChildren(False), DefaultProperty("Text"), _
ToolboxData("<{0}:DateBoxMesAño runat=server></{0}:DateBoxMesAño>")> _
Public Class DateBoxMesAño

    Inherits System.Web.UI.WebControls.TextBox

#Region "Constructor"
    Public Sub New()
        MyBase.New()
        MyBase.MaxLength = 7        'La fecha en formato: MM/AAAA
        Width = Me.Width.Pixel(76)   'Valor por defecto
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

    'Para permitir MM/AAAA
    Public Shadows ReadOnly Property MaxLength() As Integer
        Get
            Return 7
        End Get
    End Property

    'Esta propiedad permite asignar y/o leer el valor del control
    Property Value() As Date
        Get
            If Me.Text = String.Empty Then
                Return Nothing
            Else
                Try
                    Return Date.Parse(MyBase.Text.Substring(MyBase.Text.Length - 4) + _
           "/" + MyBase.Text.Substring(3, 2))
                Catch
                    Return Nothing
                End Try
            End If
        End Get

        Set(ByVal Value As Date)
            Dim Mes As String = "00" + CStr(Value.Month)
            Dim Anio As String = "0000" + CStr(Value.Year)
            MyBase.Text = Mes.Substring(Mes.Length - 2) + _
      "/" + Anio.Substring(Anio.Length - 4)
        End Set
    End Property

    'Es solo lectura para evitar que escriban un texto que no represente una fecha
    'Shadows Property Text() As String
    '    Get
    '        Return MyBase.Text
    '    End Get
    'End Property

    <Category("Behavior"), DefaultValue(False), _
    Description("Permite dejar el campo vacío, de manera que la fecha sea nula.")> _
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

        ''Si el script no esta registrado...
        'If Not MyBase.Page.IsClientScriptBlockRegistered("DateBoxMesAnio") Then
        '    'Registro todo el script del control
        '    Page.RegisterClientScriptBlock("DateBoxMesAnio", FuncionJava())
        'End If
        'Si el script no esta registrado...
        If Not MyBase.Page.ClientScript.IsClientScriptBlockRegistered("DateBoxMesAnio") Then
            'Registro todo el script del control
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "DateBoxMesAnio", FuncionJava())
        End If
    End Sub

    Private Function FuncionJava() As String
        Dim f As String
        f = "<script language=""javascript""> " &
        "function FiltroFechaMesAnio() { " &
        "   var key = window.event.keyCode; " &
        "   if ( (key > 47 && key < 58) || key == 13 ) " &
        "      return; " &
        "   else { " &
        "      window.event.returnValue = null;  " &
        "   } " &
        "} " &
        "function FormatoFechaMesAnio(NombreControl, PermiteNulo) { " &
        "   var valor = document.all[NombreControl].value; " &
        "   var  mes, anio; " &
        "   if (valor == '' && PermiteNulo == true) return;" &
        "   longitud = valor.length; " &
        "   if (longitud == 6) { " &
        "      mes = valor.substring(0,2); " &
        "      anio = valor.substring(2,6); " &
        "   document.all[NombreControl].value =  mes + '/' + anio ; " &
        "   } " &
        "   if (longitud == 4) { " &
        "      mes = valor.substring(0,2); " &
        "      anio = valor.substring(2,4); " &
        "      if (anio < 100 ) {if (anio < 50) {anio = 20 + anio} else {anio= 19 + anio;}} " &
        "   document.all[NombreControl].value =  mes + '/' + anio ; " &
        "   } " &
        "   if (longitud != 0 &&  longitud != 6 && longitud != 4 && longitud != 7){ " &
        "   document.all[NombreControl].value = '' ; " &
        "   document.all[NombreControl].focus();" & vbNewLine &
        "   return false;" & vbNewLine &
        "   } " &
        "   if (longitud == 7 &&  valor.substring(2,1) != '/' ){ " &
        "   alert('La fecha ingresada es inválida');" & vbNewLine &
        "   document.all[NombreControl].value = '' ; " &
        "   document.all[NombreControl].focus();" & vbNewLine &
        "   return false;" & vbNewLine &
        "   } " &
        "if (mes>12 || mes==0){" & vbNewLine &
                "alert('La fecha ingresada es inválida');" & vbNewLine &
                "document.all[NombreControl].value = '' ; " & vbNewLine &
                "document.all[NombreControl].focus();" & vbNewLine &
                "return false;" & vbNewLine &
        "} " &
        "if (anio<1900){" & vbNewLine &
            "alert('La fecha ingresada es inválida');" & vbNewLine &
            "document.all[NombreControl].value = '' ; " & vbNewLine &
            "document.all[NombreControl].focus();" & vbNewLine &
            "return false;" & vbNewLine &
        "}" & vbNewLine &
        "} " &
        "</script> "

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
        Dim NombreContenedor As String = Me.NamingContainer.ClientID
        Dim NombreControl As String

        NombreControl = Me.ID

        MyBase.Attributes.Add("onkeypress", "FiltroFechaMesAnio()")
        MyBase.Attributes.Add("onblur", "FormatoFechaMesAnio('" + NombreControl + "', " + _
     _AllowNull.ToString.ToLower + ")")
        MyBase.Render(writer)
    End Sub
#End Region

End Class