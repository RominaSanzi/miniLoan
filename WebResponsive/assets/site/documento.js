function resultadoVerificacion(input, response) {
    console.log(response);
    console.log(input);

    if (response.d == 0) {
        input.value = "";
        site.showSwal('mensaje-cedula-uruguay');
    }
}

function erroresVerificacion(input, response) {
    console.log(response);
    console.log(input);
}

function calcularVerificador(input) {
    if (input.value !== "") {
        $.ajax({
            type: "POST",
            url: "DocumentoServicios.aspx/calcularCuit",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{eNumero: '" + input.value + "'}",
            success: function(response) {
                resultadoVerificacion(input, response)
            },
            error: function (response) {
                erroresVerificacion(input, response)
            }, 
            fail: (function (response) {
                console.log(response);
            }),
            always: (function () {
            })
        });
    }
}