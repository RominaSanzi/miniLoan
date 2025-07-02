function MostrarNotificacion(mensaje) {
    color = 'info';
    mensaje = mensaje.toUpperCase();
    $.notify({
        icon: "now-ui-icons ui-1_bell-53",
        message: mensaje

    }, {
        type: color,
        timer: 4000,
        placement: {
            from: 'bottom',
            align: 'right'
        }
    });
};

function MostrarNotificacionError(mensaje) {
    color = 'danger';
    mensaje = mensaje.toUpperCase();

    $.notify({
        icon: "ui-1_simple-remove",
        message: mensaje

    }, {
        type: color,
        timer: 4000,
        placement: {
            from: 'bottom',
            align: 'right'
        }
    });
};

function MostrarErrores(errores, control) {
    //Creo elemento de lista para los errores
    //var result = document.createElement('ul');
    var mensaje = "";

    //Oculto y vacio los errores en pantalla
    //$(control).hide('easeInOutExpo').empty();

    //Creo html errores
    for (var i = 0; i < errores.length; i++) {
        //var item = document.createElement('li');
        //$(item).append(errores[i]);
        //result.appendChild(item);
        mensaje += errores[i] + "<br/>";
    }

    //Lleno y muestro los errores
    //$(control).append(result).show('easeInOutExpo');
    MostrarNotificacionError(mensaje);
};

function MostrarErroresLogueo(errores, control) {
    //Creo elemento de lista para los errores
    var result = document.createElement('ul');

    //Oculto y vacio los errores en pantalla
    $(control).hide('easeInOutExpo').empty();

    //Creo html errores
    for (var i = 0; i < errores.length; i++) {
        var item = document.createElement('li');
        $(item).append(errores[i]);
        result.appendChild(item);
    }

    //Lleno y muestro los errores
    $(control).append(result).show('easeInOutExpo');
}


/* Capturar imagen con el celular */

var fileInput1 = document.getElementsByClassName('file-input')[0];
var fileInput2 = document.getElementsByClassName('file-input-2')[0];

if (fileInput1) {
    fileInput1.onchange = function (e) {
        loadImage(
            e.target.files[0],
            function (img) {
                $('#result').empty().append(img);
                $('.buttons').removeClass('is-hidden');
                $('.foto-demo, .main-cta').addClass('is-hidden');
            }, {
            maxWidth: 300,
            orientation: false
        }
        );
    };
}

if (fileInput2) {
    fileInput2.onchange = function (e) {
        loadImage(
            e.target.files[0],
            function (img) {
                $('#result').empty().append(img);
            }, {
            maxWidth: 300,
            orientation: false
        }
        );
    };
}

function readURL(input) {
    var inicioAchicarImagen = Date.now();
    if (input.files && input.files[0]) {
        loadImage(
            input.files[0],
            function (img) {
                $('#result').empty().append(img);
            }, {
            maxWidth: 300,
            orientation: false
        }
        );

        var finAchicarImagen = Date.now();
        let resta = finAchicarImagen - inicioAchicarImagen;
        var segundosAchicarImagen = Math.round(resta / (1000));
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#result').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function readURL300(input) {
    var inicioAchicarImagen = Date.now();
    if (input.files && input.files[0]) {
        loadImage(
            input.files[0],
            function (img) {
                $('#result2').empty().append(img);
            }, {
            maxWidth: 900,
            orientation: 1
        });
        var reader = new FileReader();
        reader.readAsDataURL(input.files[0]);
    }
}


$("body").on("change", ".foto-input", function () {

    $('#btnagregarImagen').show();
    $('.upload-btn-wrapper').hide();
    $("#btnFinalizar").removeAttr("disabled");
    $('#EnvioCodigoExtraccion').prop('checked', true);

    readURL(this);
    readURL300(this);
    $("#foto-input").val("");
});

var listadoReferencias = ["Padre/Madre", "Hermana/o", "Tia/o", "Prima/o", "Amiga/o", "Vecina/o", "Compañera/o Laboral", "Tutor/a", "Conyuge", "Hijo/a"];

try {
    $(".js-relaciones")
        /* llenar el listado con los valores del array relaciones */
        .autocomplete({
            source: listadoReferencias,
            minLength: 0,
            select: function (event, ui) {
                // console.log("seleccion")
            }
        });

} catch (e) {

};

function validarEmail(valor) {
    if (/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test(valor)) {
        return true;
    } else {
        return false;
    }
}

function validarCBU(valor) {
    if (valor.length < 22) {
        return false;
    } else {
        var cbu;
        cbu = valor;

        var ponderador;
        ponderador = '7139713';

        var i;
        var nDigito;
        var nPond;
        var bloque1;
        var bloque2;
        var nTotal;

        nTotal = 0;
        bloque1 = cbu.substring(0, 7);

        for (i = 0; i <= 7; i++) {
            nDigito = bloque1.charAt(i);
            nPond = ponderador.charAt(i);
            nTotal = nTotal + (nPond * nDigito);
        }

        var iDigitoVerificador;
        iDigitoVerificador = 10 - nTotal.toString().substr(- 1);

        // i = digito verificador
        if (cbu.substring(7, 8) !== iDigitoVerificador.toString().substr(-1)) {
            return false;
        } else {
            nTotal = 0;
            ponderador = '3971397139713';
            bloque2 = cbu.substring(8, 21);
            for (i = 0; i <= 15; i++) {
                nDigito = bloque2.charAt(i);
                nPond = ponderador.charAt(i);
                nTotal = nTotal + (nPond * nDigito);
            }

            iDigitoVerificador = 10 - nTotal.toString().substr(-1);

            // i = digito verificador
            if (cbu.substring(21, 22) !== iDigitoVerificador.toString().substr(-1)) {
                return false;
            } else {
                return true;
            }
        }
    }
};

function formatoPlata(amount, decimalCount , decimal , thousands) {
    if (!decimalCount) decimalCount = 2;
    if (!decimal) decimal = ",";
    if (!thousands) thousands = ".";

    try {
        decimalCount = Math.abs(decimalCount);
        decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

        const negativeSign = amount < 0 ? "-" : "";

        let i = parseInt(amount = Math.abs(Number(amount) || 0).toFixed(decimalCount)).toString();
        let j = (i.length > 3) ? i.length % 3 : 0;

        return negativeSign + (j ? i.substr(0, j) + thousands : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) + (decimalCount ? decimal + Math.abs(amount - i).toFixed(decimalCount).slice(2) : "");
    } catch (e) {
        console.log(e)
    }
};

