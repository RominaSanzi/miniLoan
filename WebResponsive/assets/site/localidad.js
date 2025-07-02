function resultadoLocalidad(response) {

}

function errores(response) {
    console.log(response);
}

function cambioProvincia(option, prefijo) {
    $.ajax({
        type: "POST",
        url: "LocalidadServicios.aspx/cambioProvincia",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{eIdProvincia: '" + option.value + "', eDescripcionProvincia: '" + option.options[option.selectedIndex].text + "', ePrefijo: '" + prefijo + "'}",
        success: resultadoLocalidad,
        error: errores,
        fail: (function (response) {
            console.log(response);
        }),
        always: (function () {
        })
    });
}

function cambioPartido(option, prefijo) {
    $.ajax({
        type: "POST",
        url: "LocalidadServicios.aspx/cambioPartido",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: '{eIdPartido: "' + option.value + '", eDescripcionPartido: "' + option.options[option.selectedIndex].text + '", ePrefijo: "' + prefijo + '"}',
        success: resultadoLocalidad,
        error: errores,
        fail: (function (response) {
            console.log(response);
        }),
        always: (function () {
        })
    });
}

function cambioLocalidad(option, prefijo) {
    $.ajax({
        type: "POST",
        url: "LocalidadServicios.aspx/cambioLocalidad",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{eIdLocalidad: '" + option.value + "', eDescripcionLocalidad: '" + option.options[option.selectedIndex].text + "', ePrefijo: '" + prefijo + "'}",
        success: resultadoLocalidad,
        error: errores,
        fail: (function (response) {
            console.log(response);
        }),
        always: (function () {
        })
    });
}