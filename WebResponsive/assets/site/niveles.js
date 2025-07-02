var iSelect;

function limpiarPuntoVenta() {
    $.ajax({
        type: "POST",
        url: "ComercioServicios.aspx/removerSessione",
        contentType: "application/json; charset=utf-8"
    });

    $('#intCodigoPuntoVenta').val('');
    $('#txtNombrePuntoVenta').val('');

    try {
        cambioComercio();
    }
    catch (ex) {
    }
}

function cambioSeleccion(select) {
    iSelect = select;
    getPuntoVenta();
}

$('#lstEmpresaGrupo').on('change', function () {
    cambioSeleccion(this);
});
$('#lstUnidadDeNegocios').on('change', function () {
    cambioSeleccion(this);
});
$('#lstSucursal').on('change', function () {
    cambioSeleccion(this);
});
$('#intCodigoPuntoVenta').on('change', function () {
    getPuntoVenta();
});

function getPuntoVenta() {
    if ($('#intCodigoPuntoVenta').val() != null && $('#intCodigoPuntoVenta').val() != "") {
        var iNivel = "0";
        var iNivelValor = "0";

        if (iSelect != null && iSelect.value > 0) {
            iNivel = iSelect.id;
            iNivelValor = iSelect.value;
        }

        $.ajax({
            type: "POST",
            url: "ComercioServicios.aspx/obtenerPuntoVenta",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{eNivel:'" + iNivel + "', eNivelValor:'" + iNivelValor + "', eCodigoPuntoVenta:'" + $("#intCodigoPuntoVenta").val() + "'}",
            success: resultadoNiveles,
            error: errores,
            fail: (function (response) {
                console.log(response);
            }),
            always: (function (response) {
                console.log(response);
            })
        });

    } else {
        limpiarPuntoVenta();
    }
}

function resultadoNiveles(response) {
    if (response.d !== undefined) {
        $('#intCodigoPuntoVenta').val(response.d[0]);
        $('#txtNombrePuntoVenta').val(response.d[1]);
        $('#ccdSucursal').val(response.d[2]);
        $('#ccdUnidadDeNegocios').val(response.d[3]);
        $('#ccdEmpresaGrupo').val(response.d[4]);

        try {
            cambioComercio();
        }
        catch (ex) {
        }
    }

}

function errores(response) {
    limpiarPuntoVenta();
}