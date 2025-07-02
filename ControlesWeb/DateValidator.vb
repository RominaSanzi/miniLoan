Imports System.Web.UI
Imports System.ComponentModel

<DefaultProperty("ErrorMessage"), ToolboxData("<{0}:DateValidator runat=server></{0}:DateValidator>")> _
   Public Class DateValidator

    Inherits System.Web.UI.WebControls.BaseValidator

#Region "Constructor"
    Sub New()
        MyBase.New()
        Me.ErrorMessage = "*"
    End Sub
#End Region

#Region "Metodos"
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)
        'Si el script del control no esta registrado....
        If Me.EnableClientScript Then
            If Not MyBase.Page.IsClientScriptBlockRegistered("DateValidator") Then
                'Registro todo el script del control
                Page.RegisterClientScriptBlock("DateValidator", FuncionJava())
            End If
        End If
    End Sub

    Private Function FuncionJava() As String
        'Funciones que se enviaran al navegador.
        'La funciones javascript "ValidatorTrim" y "ValidatorGetValue" son parte de ASP.Net y normalmente se 
        'encuentran en C:\wwwroot\aspnet_client\system_web\1_0_3705_288
        'Luego simplemente verifica que la fecha sea correcta. La única "complicación"
        'que tiene, es que se fija si el control a validar permite fecha nula o no...

        'Además, como en el control DateBox... Seguro que un programador Java puede optimizarla,
        'como ven es Java escrito al "estilo Visual Basic". :P
        Dim f As String
        f = "<script language=""javascript"">" & _
        "function ValidarFecha(val) { " & _
        "var valor = ValidatorTrim(ValidatorGetValue(val.controltovalidate)); " & _
        "var pernul = document.all[val.controltovalidate].AllowNull; " & _
        "if (valor == '') {if (pernul == 'true') {return true;} else {return false;}};" & _
        "var mmm = valor.slice(2,4); " & _
        "var ddd = valor.slice(0,2); " & _
        "if (ddd.slice(0,1) == '0') { ddd = ddd.slice(1) }; " & _
        "if (mmm.slice(0,1) == '0') { mmm = mmm.slice(1) }; " & _
        "var AAAA = parseInt(valor.slice(-4)); " & _
        "var MM = parseInt(mmm); " & _
        "var DD = parseInt(ddd); " & _
        "if (isNaN(AAAA) || isNaN(MM) || isNaN(DD)) {if (valor.length == 8) {return false;} else {return true;} } " & _
        "var selectedDate = new Date(AAAA, MM-1, DD); " & _
        "if (selectedDate.getMonth() != MM -1 || selectedDate.getDate() != DD) { " & _
        "   return false; " & _
        "} else { " & _
        "   return true; " & _
        "} " & _
        "} " & _
        "</script>"
        f.Replace(" ", "")
        Return f
    End Function

    'Recordamos que ASP.Net tiene la posibilidad de validar los controles tanto en el cliente como en el servidor,
    'es por esta razon que incluimos esta rutina de validación en el servidor. 
    Protected Overrides Function EvaluateIsValid() As Boolean
        'Esta por las dudas usen el validator en un control textbox por ejemplo, 
        'donde no se formatea automáticamente la fecha, y en ese caso la función java no lo valida.. 
        'También esta por si un Navegador tiene deshabilitado las funciones java en el cliente
        Dim Valor As String
        Valor = Me.GetControlValidationValue(Me.ControlToValidate)
        If Valor.Trim = String.Empty Then
            Return True 'Puede ser fecha nula
        End If
        If Valor.Length <> 10 Then
            Return False
        Else
            Return ValidarFecha(Valor.Substring(Valor.Length - 4), Valor.Substring(3, 2), Valor.Substring(0, 2))
        End If
    End Function

    'Función de validación de Fecha llamada por EvaluateIsValid
    Private Function ValidarFecha(ByVal AAAA As String, ByVal MM As String, ByVal DD As String) As Boolean
        Dim miFecha As Date
        Try
            miFecha = Date.Parse(AAAA + "-" + MM + "-" + DD)
            If miFecha.Day <> CLng(DD) Or miFecha.Month <> CLng(MM) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        'Aquí esta la clave.. Le decimos que función nuestra se usa para validar...
        MyBase.Attributes.Add("evaluationfunction", "ValidarFecha")
        MyBase.Render(writer)
    End Sub
#End Region

End Class