let formateadorDecimal = new Intl.NumberFormat('en-US', {
    //style: 'currency',
    //currency: 'BOB',
    maximumFractionDigits: 2,
    minimumFractionDigits: 2
});

let formateadorEntero = new Intl.NumberFormat('en-US', {
    //style: 'currency',
    //currency: 'BOB',
    maximumFractionDigits: 2,
    minimumFractionDigits: 0
});

let tablaData;

$(document).ready(function () {

    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaInicio").datepicker({ dateFormat: " dd/mm/yy" })
    $("#txtFechaFin").datepicker({ dateFormat: " dd/mm/yy" })

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": `/Reporte/ReportePlanificacion?fechaInicio=01/01/1991&fechaFin=01/01/1991`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fechaPlanificacion" },
            { "data": "numeroPlanificacion" },
            { "data": "citePlanificacion" },
            { "data": "nombreCentro" },
            { "data": "nombreUnidadResponsable" },

            {
                "data": "montoPlanificacion", render: function (data) {
                    return '<div class="text-right">' + formateadorDecimal.format(data) + '</div>';
                }
            },

            { "data": "nombreItem" },
            { "data": "medida" },
            {
                "data": "cantidad", render: function (data) {
                    return '<div class="text-right">' + formateadorEntero.format(data) + '</div>';
                }
            },
            {
                "data": "precio", render: function (data) {
                    return '<div class="text-right">' + formateadorDecimal.format(data) + '</div>';
                }
            },
            {
                "data": "total", render: function (data) {
                    return '<div class="text-right">' + formateadorDecimal.format(data) + '</div>';
                }
            },
            {
                "data": "codigoActividad", render: function (data) {
                    return '<div class="text-center">' + data + '</div>';
                }
            },
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Carpetas Planificacion',
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})

$("#btnBuscar").click(function ()
{
    if ($("#cboBuscarPor").val() == "fecha") {
        if ($("#txtFechaInicio").val().trim() == "" || $("#txtFechaFin").val().trim() == "") {
            toastr.warning("", "Debe Ingresar Fecha Inicio y Fin")
            return;
        }
    }

    let fechaInicio = $("#txtFechaInicio").val().trim();
    let fechaFin = $("#txtFechaFin").val().trim();

    //alert($("#txtFechaInicio").val());
    //alert($("#txtFechaFin").val());

    let nueva_url = `/Reporte/ReportePlanificacion?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`;

    tablaData.ajax.url(nueva_url).load();
})