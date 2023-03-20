let tablaData;

$(document).ready(function () {

    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaInicio").datepicker({ dateFormat: " dd/mm/yy" })
    $("#txtFechaFin").datepicker({ dateFormat: " dd/mm/yy" })



    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Reporte/ReportePlanificacion?fechaInicio=01/01/1991&fechaFin=01/01/1991',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fechaPlanificacion" },
            { "data": "numeroPlanificacion" },
            { "data": "citePlanificacion" },
            { "data": "nombreCentro" },
            { "data": "nombreActividad" },
            { "data": "montoPlanificacion" },
            { "data": "nombrePartida" },
            { "data": "medida" },
            { "data": "cantidad" },
            { "data": "precio" },
            { "data": "total" }
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

    let nueva_url = '/Reporte/ReportePlanificacion?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}';

    tablaData.ajax.url(nueva_url).load();
}