﻿let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/ModificacionPoa/ListaMisModificacionesPoa',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idModificacionPoa" },
            { "data": "lugar" },
            { "data": "cite" },
            { "data": "justificacion" },
            {
                "data": "editIndicador", render: function (data) {
                    if (data)
                        return "X";
                    else return "";
                }
            },
            {
                "data": "editCantidad", render: function (data) {
                    if (data)
                        return "X";
                    else return "";
                } },
            {
                "data": "editTemporalidad", render: function (data) {
                    if (data)
                        return "X";
                    else return "";
                }
},
            { "data": "totalActual" },
            { "data": "totalModificar" },
            { "data": "fechaRegistro" },
            { "data": "estadoModificacion" },
            {
                "data": "estadoModificacion", render: function (data) {
                    return "<button class='btn btn-default' disabled> Ver</button><button class='btn btn-success' disabled> Excel</button><button class='btn btn-danger' disabled> Anular</button>";
                }
            }

        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Listado Solicitudes Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte POA Mi Unidad',
                exportOptions: {
                    columns: [1, 2, 3]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})
