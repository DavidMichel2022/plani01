let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/RequerimientoPoa/ListaPoaMiUnidad',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idRequerimientoPoa" },
            { "data": "nombreRegional" },
            { "data": "nombreCentro" },
            { "data": "nombreUnidadResponsable" },
            {
                "data": "detalle", render: function (data) {
                    return data;
                }
            },
            { "data": "codigoPartida" },
            { "data": "medida" },
            { "data": "cantidad" },
            { "data": "precio" },
            { "data": "total" },
            { "data": "observacion" },
            {
                "data": "lugar", render: function (data) {
                    return "Habilitado";
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
