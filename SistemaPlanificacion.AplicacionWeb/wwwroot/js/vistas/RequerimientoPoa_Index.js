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
            { "data": "idRequerimientoPoa"}
           
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


//$(document).ready(function () {

   /* tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/RequerimientoPoa/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idRequerimientoPoa" },
            { "data": "idRequerimientoPoa" },
            { "data": "idRequerimientoPoa" },
            { "data": "idRequerimientoPoa" },
            { "data": "idRequerimientoPoa" },
            { "data": "idRequerimientoPoa" },
            { "data": "idRequerimientoPoa" }
           /* { "data": "citeRequerimientoPoa" },
            { "data": "idCentro" },
            { "data": "nombreEjecutora" },
            { "data": "nombreEjecutora" },
            { "data": "nombreEjecutora" },
            { "data": "montoPoa" }/*,
            {
                "data": "detalleRequerimientoPoas", render: function (data) {
                    console.log(data);
                    return '<span class="badge badge-info">---</span>';
                }
            },
            { "data": "estadoRequerimientoPoa" }  */       
            
       // ]
       /* ,
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'ListadoPoaMiUnidad',
                exportOptions: {
                    columns: [1, 2, 3]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
        
    });*/
//})
