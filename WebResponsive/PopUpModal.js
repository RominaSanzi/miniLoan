var popupWindow = null;
var boton = null;

function abrirPopUp(ePagina, eNombrePopUp, eWidth, eHeight, eBoton) {
    var window_width = eWidth;
    var window_height = eHeight;
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;

    popupWindow = window.open(ePagina, eNombrePopUp, "directories=no,status=no,menubar=no,scrollbars=yes,fullscreen=no,width=" + window_width + ",height=" + window_height + ",top=" + window_top + ",left=" + window_left + ",resizable=no");

    boton = eBoton;
    document.onmousedown = parent_disable;
    document.onkeyup = parent_disable;
    document.onmousemove = parent_disable;

    var interval = window.setInterval(function () {
        try {
            if (popupWindow == null || popupWindow.closed) {
                window.clearInterval(interval);
                cerrarPopUp();
            }
        }
        catch (e) {
            console.log(e);
        }
    }, 1000);

    return popupWindow;

};

function modalPopUpRedireccionar(ePagina, eTitulo, eMensaje, ePaginaCerrar) {
    window.parent.parent.$("#aModal").hide();
    window.parent.parent.$("#btnModal").attr("onclick", "window.location.href ='" + ePagina + "'");
    window.parent.parent.$("#btnModal").show();//.onclick = "window.close('" & ePaginaCerrar & "')";
    window.parent.parent.$("#btnModal").focus();//.onclick = "window.close('" & ePaginaCerrar & "')";
    window.parent.parent.$("#modalTitulo").html(eTitulo);
    window.parent.parent.$("#modalMensaje").html(eMensaje);
    window.parent.parent.$("#loading").hide();
    window.parent.parent.$("#modal").show();
};

function abrirPopUpSinPostback(ePagina, eNombrePopUp, eWidth, eHeight, eBoton) {
    var window_width = eWidth;
    var window_height = eHeight;
    var window_top = (screen.height - window_height) / 2;
    var window_left = (screen.width - window_width) / 2;

    popupWindow = window.open(ePagina, eNombrePopUp, "directories=no,status=no,menubar=no,scrollbars=yes,fullscreen=no,width=" + window_width + ",height=" + window_height + ",top=" + window_top + ",left=" + window_left + ",resizable=no");

    boton = eBoton;
    document.onmousedown = parent_disable;
    document.onkeyup = parent_disable;
    document.onmousemove = parent_disable;
};

function parent_disable() {
    if (popupWindow && !popupWindow.closed) {
        popupWindow.focus();
    }
};

function cerrarPopUp() {
    __doPostBack(boton, '');
};

function cargando() {
    window.parent.parent.$(".loading").show();
};

function ocultarCargando() {
    window.parent.parent.$(".loading").hide();
};

function validarMenu() {
    if (window.parent.$("#nav").length == 0) {
        document.getElementById("main-panel").style.width = '100%';
        var elems = document.getElementsByClassName("navbar-toggle");
        for (var i = 0; i < elems.length; i += 1) {
            elems[i].style.cssText = 'display:none !important';
        }
    }
};

function modalPopUp(ePagina, eTitulo, eMensaje, ePaginaCerrar) {
    if (window.parent.parent.$("#nav").length == "0") {
        window.parent.parent.$("#aModal").hide();
        window.parent.parent.$("#btnModal").show();//.onclick = "window.close('" & ePaginaCerrar & "')";
        window.parent.parent.$("#btnModal").focus();//.onclick = "window.close('" & ePaginaCerrar & "')";
        window.parent.parent.$("#modalTitulo").html(eTitulo);
        window.parent.parent.$("#modalMensaje").html(eMensaje);
        window.parent.parent.$("#loading").hide();
        window.parent.parent.$("#modal").show();
    } else {
        var a = window.parent.parent.$("#aModal");
        a.href = ePagina;
        window.parent.parent.$("#aModal").attr("href", ePagina);
        window.parent.parent.$("#aModal").show();
        window.parent.parent.$("#aModal").focus();
        window.parent.parent.$("#btnModal").hide();
        window.parent.parent.$("#modalTitulo").html(eTitulo);
        window.parent.parent.$("#modalMensaje").html(eMensaje);
        window.parent.parent.$("#loading").hide();
        window.parent.parent.$("#modal").show();
    }
};

function modalErrorPopUp(ePagina, eTitulo, eMensaje, ePaginaCerrar) {
    if (window.parent.parent.$("#nav").length == "0") {
        window.parent.parent.$("#modalErrorTitulo").html(eTitulo);
        window.parent.parent.$("#modalErrorMensaje").html(eMensaje);
        window.parent.parent.$("#loading").hide();
        window.parent.parent.$("#modalError").show();
    } else {
        window.parent.parent.$("#modalErrorTitulo").html(eTitulo);
        window.parent.parent.$("#modalErrorMensaje").html(eMensaje);
        window.parent.parent.$("#loading").hide();
        window.parent.parent.$("#modalError").show();
    }
};

function descargar(archivo, nombreArchivo) {
    var a = document.createElement("a");         //Create <a>
    a.href = "data:image/png;base64," + archivo; //Image Base64 Goes here
    a.download = nombreArchivo;                  //File name Here
    a.click();                                   //Downloaded file
}

function modalAyuda(ePagina, eTitulo) {
    if (window.parent.parent.$("#nav").length == "0") {
        window.parent.parent.$("#btnModalAyuda").show();//.onclick = "window.close('" & ePaginaCerrar & "')";
        window.parent.parent.$("#btnModalAyuda").focus();//.onclick = "window.close('" & ePaginaCerrar & "')";
        window.parent.parent.$("#modalTituloAyuda").html(eTitulo);
        window.parent.parent.$("#modalMensajeAyuda").html(eMensaje);
        window.parent.parent.$("#loading").hide();
        window.parent.parent.$("#modalAyuda").show();
    } else {
        var a = window.parent.parent.$("#iframeAyuda");
        a.src = ePagina;
        window.parent.parent.$("#btnModalAyuda").hide();
        window.parent.parent.$("#modalTituloAyuda").html(eTitulo);
        window.parent.parent.$("#modalMensajeAyuda").html(eMensaje);
        window.parent.parent.$("#loading").hide();
        window.parent.parent.$("#modalAyuda").show();
    }
}

function ocultarModalError() {
    window.parent.parent.$("#modalError").hide();
}