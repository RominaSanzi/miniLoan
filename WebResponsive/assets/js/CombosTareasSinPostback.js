function seleccionarValor(eIdControl, eIdControlValor) 
	{
	var select = document.getElementById(eIdControl);
	var valor = document.getElementById(eIdControlValor);
	valor.value=select.value;
	}
	
function limpiarControl(eIdControl) 
	{
	var select = document.getElementById(eIdControl);
	while (select.length > 1) 
		{
		select.remove(1);
		}
	}

function xmlhttp() {
	var obj;
	var xml = new Array();
	xml[0] = "MSXML2.XMLHTTP.5.0";
	xml[1] = "MSXML2.XMLHTTP.4.0";
	xml[2] = "MSXML2.XMLHTTP.3.0";
	xml[3] = "MSXML2.XMLHTTP";
	xml[4] = "Microsoft.XMLHTTP";
	xml[5] = "WinHttp.WinHttpRequest.5";
	xml[6] = "WinHttp.WinHttpRequest.5.1";
	if (window.ActiveXObject) {
	for (var i=0; i<xml.length; i++) {
	try {
	obj = new ActiveXObject(xml[i]);
	break;
	} catch(e) {
	obj = null;
	}
	}
	} else if(window.XMLHttpRequest) {
	try {
	obj = new XMLHttpRequest();
	} catch(e) {
	obj = null;
	}
	}
	return obj;
	}

function obtenerTareas(eIdControlPadre , eIdControlHijo, eIdControlValor) 
	{
		var select = document.getElementById(eIdControlPadre);
		var url = "ObtenerTareas.aspx?&idTareaPadre="  + select.value;
		var documento = xmlhttp();				
		var select = document.getElementById(eIdControlHijo);
		
		seleccionarValor(eIdControlPadre, eIdControlValor) 
		limpiarControl(eIdControlHijo);
			
		documento.open('GET', url, false);
		documento.send(null);
		

		if (documento.responseText!="")
			{
			var stringTareas = documento.responseText;
			var dato;
			var linea;
			var option;
			
			dato = stringTareas.split("@");
			
			for (var i=0;i<dato.length;i++)
				{
				linea = dato[i].split("#");
				
				option = document.createElement("option");
				option.value = linea[1];
				option.appendChild(document.createTextNode(linea[0]));
				select.appendChild(option);
				}
			}	
		documento=null;
	}	


function findText(finder_textbox, finder_selectbox)
{ 
var searchStr = finder_textbox.value;

var myExp = new RegExp(("^" + searchStr), "i");
var foundResult = false;
var i=0;

//alert(finder_textbox.value);
  
while ((foundResult = false) || (i < finder_selectbox.length))
{
 
if( myExp.test(finder_selectbox.options[i].text))
{
finder_selectbox.options[i].selected = true;
foundResult = true;
i = 4000;   //make this the length of the list + 1
}
i++;
  
}
if (i <=4000) 
{	
finder_selectbox.options[0].selected = true;
}
}
// -->

function limpiarControlFechaCompromiso(eIdControl) 
	{
	var select = document.getElementById(eIdControl);
	while (select.length >0) 
		{
		select.remove(0);
		}
	}

function obtenerFechasCompromiso(eIdControlTarea, eIdControlFechaCompromiso) {
    var select = document.getElementById(eIdControlTarea);
    var url = "ObtenerFechasCompromiso.aspx?&idTarea=" + select.value;
    var documento = xmlhttp();
    var select = document.getElementById(eIdControlFechaCompromiso);

    console.log(eIdControlFechaCompromiso);
    limpiarControlFechaCompromiso(eIdControlFechaCompromiso);

    documento.open('GET', url, false);
    documento.send(null);

    console.log(documento);

    if (documento.responseText != "") {
        var stringFechas = documento.responseText;
        var dato;
        var option;

        dato = stringFechas.split("@");

        for (var i = 0; i < dato.length; i++) {

            option = document.createElement("option");
            option.value = dato[i];
            option.appendChild(document.createTextNode(dato[i]));
            select.appendChild(option);
        }
        select.selectpicker("refresh");
    }
    documento = null;
}

function checkearTodo(objRef) {
    var GridView = objRef.parentNode.parentNode.parentNode.parentNode.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i = 0; i < inputList.length; i++) {
        if (inputList[i].type === "checkbox") {
            if (objRef.checked) {
                //si el checkbox del header está tildado
                //checkeo todas las checkbox
                inputList[i].checked = true;
            }
            else {
                //si el checkbox del header está tildado
                //descheckeo todas las checkbox
                inputList[i].checked = false;
            }
        }
    }
}