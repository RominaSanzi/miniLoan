$(document).ready(function () {
    $('.administrador').prepend($("<thead></thead>").append($('.administrador').find("tr:first")));


    (function (factory) {
        if (typeof define === "function" && define.amd) {
            define(["jquery", "moment", "datatables.net"], factory);
        } else {
            factory(jQuery, moment);
        }
    }(function ($, moment) {

        $.fn.dataTable.moment = function (format, locale) {
            var types = $.fn.dataTable.ext.type;

            // Add type detection
            types.detect.unshift(function (d) {
                if (d) {
                    // Strip HTML tags and newline characters if possible
                    if (d.replace) {
                        d = d.replace(/(<.*?>)|(\r?\n|\r)/g, '');
                    }

                    // Strip out surrounding white space
                    d = $.trim(d);
                }

                // Null and empty values are acceptable
                if (d === '' || d === null) {
                    return 'moment-' + format;
                }

                return moment(d, format, locale, true).isValid() ?
                    'moment-' + format :
                    null;
            });

            // Add sorting method - use an integer for the sorting
            types.order['moment-' + format + '-pre'] = function (d) {
                if (d) {
                    // Strip HTML tags and newline characters if possible
                    if (d.replace) {
                        d = d.replace(/(<.*?>)|(\r?\n|\r)/g, '');
                    }

                    // Strip out surrounding white space
                    d = $.trim(d);
                }

                return d === '' || d === null ?
                    -Infinity :
                    parseInt(moment(d, format, locale, true).format('x'), 10);
            };
        };

    }));


    $.fn.dataTable.moment('DD/MM/YYYY');
    $('.administrador').DataTable({
            "dom": "Blfrtip",
        "pagingType": "full_numbers",
        "order": [],
            buttons: [
                {
                    extend: 'excel',
                    text: '<i class="far fa-file-excel fa-lg" style="margin:5px"></i>',
                    titleAttr: 'Excel',
                    title: function () {
                        var titulo = $(".card-title:first").html();
                        return titulo.replace("_", "");
                    },        
                    filename: function () {
                        var titulo = $(".card-title:first").html();
                        return titulo.replace("_", "");
                    }                    
                },
                {
                    extend: 'pdf',
                    text: '<i class="far fa-file-pdf fa-lg" style="margin:5px"></i>',
                    titleAttr: 'PDF',
                    title: function () {
                        var titulo = $(".card-title:first").html();
                        return titulo.replace("_", "");
                    }, 
                    filename: function () {
                        var titulo = $(".card-title:first").html();
                        return titulo.replace("_", "");
                    }
                },
                {
                    extend: 'print',
                    autoPrint: true,                  
                    text: '<i class="fas fa-print fa-lg" style="margin:5px"></i>',
                    titleAttr: 'Imprimir',
                    title: function () {
                        var titulo = $(".card-title:first").html();
                        return titulo.replace("_", "");
                    }, 
                    filename: function () {
                        var titulo = $(".card-title:first").html();
                        return titulo.replace("_", "");
                    }

                }
            ],
            "lengthMenu": [
                [10, 25, 50, -1],
                [10, 25, 50, "Todo"]
        ],
        "columnDefs": [
            {
            "targets": -1,
            "orderable":false
            },
            //pongo prioridad para la columna 1 y la ultima de las acciones
            { responsivePriority: 1, targets: 1 },
            { responsivePriority: 1, targets: -1 }
        ],
            responsive: true,
            language: {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                }
            }

        });
    });
