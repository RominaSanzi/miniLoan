Imports System.ComponentModel
Imports System.Web.UI

<ValidationPropertyAttribute("Text"), ParseChildren(False), DefaultProperty("Text"), _
ToolboxData("<{0}:DateBox runat=server></{0}:DateBox>")> _
Public Class DateBox

    Inherits System.Web.UI.WebControls.TextBox

#Region "Constructor"
    Public Sub New()
        MyBase.New()
        MyBase.MaxLength = 10        'La fecha en formato: DD/MM/AAAA
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

    'Para permitir DD/MM/AAAA
    Public Shadows ReadOnly Property MaxLength() As Integer
        Get
            Return 10
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
           "/" + MyBase.Text.Substring(3, 2) + "/" + MyBase.Text.Substring(0, 2))
                Catch
                    Return Nothing
                End Try
            End If
        End Get

        Set(ByVal Value As Date)
            Dim Dia As String = "00" + CStr(Value.Day)
            Dim Mes As String = "00" + CStr(Value.Month)
            Dim Anio As String = "0000" + CStr(Value.Year)
            MyBase.Text = Dia.Substring(Dia.Length - 2) + "/" + Mes.Substring(Mes.Length - 2) + _
      "/" + Anio.Substring(Anio.Length - 4)
        End Set
    End Property

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

        'Si el script no esta registrado...
        If Not MyBase.Page.ClientScript.IsClientScriptBlockRegistered("DateBox") Then
            'Registro todo el script del control
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "DateBox", FuncionJava())
        End If
    End Sub

    Private Function FuncionJava() As String
        Dim f As String
        f = "<script language=""javascript""> " & vbNewLine & _
        "function FiltroFecha() { " & vbNewLine & _
        "   var key = window.event.keyCode; " & vbNewLine & _
        "   if ( (key > 47 && key < 58) || key == 13) " & vbNewLine & _
        "      return; " & vbNewLine & _
        "   else { " & vbNewLine & _
        "      window.event.returnValue = null;  " & vbNewLine & _
        "      window.event.keyCode = null;  " & vbNewLine & _
        "   } " & vbNewLine & _
        "} " & vbNewLine & _
        "function comprobarSiBisisesto(anio){" & vbNewLine & _
            "if ( ( anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {" & vbNewLine & _
                "return true;" & vbNewLine & _
                "}" & vbNewLine & _
            "else {" & vbNewLine & _
                "return false;" & vbNewLine & _
                "}" & vbNewLine & _
            "}" & vbNewLine & _
        "function FormatoFecha(NombreControl, PermiteNulo) { " & vbNewLine & _
        "   var valor = document.all[NombreControl].value; " & vbNewLine & _
        "   var dia, mes, anio; " & vbNewLine & _
        "   if (valor == '' && PermiteNulo == true) return;" & vbNewLine & _
        "   longitud = valor.length; " & vbNewLine & _
        "   if (longitud == 8) { " & vbNewLine & _
        "      dia = valor.substring(0,2); " & vbNewLine & _
        "      mes = valor.substring(2,4); " & vbNewLine & _
        "      anio = valor.substring(4,8); " & vbNewLine & _
        "   document.all[NombreControl].value = dia + '/' + mes + '/' + anio ; " & vbNewLine & _
        "   } " & vbNewLine & _
        "   if (longitud == 6) { " & vbNewLine & _
        "      dia = valor.substring(0,2); " & vbNewLine & _
        "      mes = valor.substring(2,4); " & vbNewLine & _
        "      anio = valor.substring(4,6); " & vbNewLine & _
        "      if (anio < 100 ) {if (anio < 50) {anio = 20 + anio} else {anio= 19 + anio;}} " & vbNewLine & _
        "   document.all[NombreControl].value = dia + '/' + mes + '/' + anio ; " & vbNewLine & _
        "   } " & vbNewLine & _
        "   if (longitud != 6 && longitud != 8 && longitud != 10){ " & vbNewLine & _
        "   document.all[NombreControl].value = '' ; " & vbNewLine & _
        "   } " & vbNewLine & _
        "if (document.all[NombreControl].value != ''){ " & vbNewLine & _
        "var fecha = document.all[NombreControl].value; " & vbNewLine & _
        "var dia  =  parseInt(fecha.substring(0,2),10);" & vbNewLine & _
        "var mes  =  parseInt(fecha.substring(3,5),10);" & vbNewLine & _
        "var anio =  parseInt(fecha.substring(6),10);" & vbNewLine & _
        "switch(mes){" & vbNewLine & _
            "case 1:" & vbNewLine & _
            "case 3:" & vbNewLine & _
            "case 5:" & vbNewLine & _
            "case 7:" & vbNewLine & _
            "case 8:" & vbNewLine & _
            "case 10:" & vbNewLine & _
            "case 12:" & vbNewLine & _
                "numDias=31;" & vbNewLine & _
                "break;" & vbNewLine & _
            "case 4: case 6: case 9: case 11:" & vbNewLine & _
                "numDias=30;" & vbNewLine & _
                "break;" & vbNewLine & _
            "case 2:" & vbNewLine & _
                "if (comprobarSiBisisesto(anio)){ numDias=29 }else{ numDias=28};" & vbNewLine & _
                "break;" & vbNewLine & _
            "default:" & vbNewLine & _
                "alert('La fecha ingresada es inválida');" & vbNewLine & _
                "document.all[NombreControl].value = '' ; " & vbNewLine & _
                "document.all[NombreControl].focus();" & vbNewLine & _
                "return false;" & vbNewLine & _
        "}" & vbNewLine & _
            "if (dia>numDias || dia==0){" & vbNewLine & _
                "alert('La fecha ingresada es inválida');" & vbNewLine & _
                "document.all[NombreControl].value = '' ; " & vbNewLine & _
                "document.all[NombreControl].focus();" & vbNewLine & _
                "return false;" & vbNewLine & _
            "}" & vbNewLine & _
            "if (anio<1900){" & vbNewLine & _
                "alert('La fecha ingresada es inválida');" & vbNewLine & _
                "document.all[NombreControl].value = '' ; " & vbNewLine & _
                "document.all[NombreControl].focus();" & vbNewLine & _
                "return false;" & vbNewLine & _
            "}" & vbNewLine & _
        "}" & vbNewLine & _
        "}" & vbNewLine & _
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
        NombreControl = Me.NamingContainer.FindControl(Me.ID).ClientID

        MyBase.Attributes.Add("onkeypress", "FiltroFecha()")
        MyBase.Attributes.Add("onblur", "FormatoFecha('" + NombreControl + "', " + _
     _AllowNull.ToString.ToLower + ")")
        MyBase.Render(writer)
    End Sub
#End Region

End Class

