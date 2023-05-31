let tablaData;

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
                    return data;
                }
            },
            { "data": "editCantidad" },
            { "data": "editTemporalidad" },
            { "data": "totalActual" },
            { "data": "totalModificar" },
            { "data": "fechaRegistro" },
            { "data": "estado" },
            {
                "data": "estado", render: function (data) {
                    return "<button class='btn btn-primary' disabled> Editar</button>";
                }
            }

        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
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
