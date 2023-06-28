let tablaData;

$(document).ready(function () {
    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/AnteproyectoPoa/ListaPoaMiUnidad',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idAnteproyecto" },
            //{ "data": "nombreRegional" },
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
            { "data": "mesEne" },
            { "data": "mesFeb" },
            { "data": "mesMar" },
            { "data": "mesAbr" },
            { "data": "mesMay" },
            { "data": "mesJun" },
            { "data": "mesJul" },
            { "data": "mesAgo" },
            { "data": "mesSep" },
            { "data": "mesOct" },
            { "data": "mesNov" },
            { "data": "mesDic" },
            { "data": "observacion" }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Anteproyecto POA Mi Unidad',
                exportOptions: {
                    columns: [4, 3, 5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20]
                }
            },
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})
