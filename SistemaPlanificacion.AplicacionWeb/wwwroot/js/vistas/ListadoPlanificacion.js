const MODELO_BASE = {
    idEmpresa: 0,
    codigo: "",
    nombre: "",
    esActivo: 1,
}

let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/planificacion/ListaMisCarpetas',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idPlanificacion", "visible": true, "searchable": true },
            { "data": "numeroPlanificacion" },
            { "data": "citePlanificacion" },
            { "data": "nombreRegional" },
            { "data": "nombreEjecutora" },
            { "data": "montoPlanificacion" },
            { "data": "idCentro" 
                /*"data": "estadoCarpeta", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">Certificado</span>';
                    else
                        return '<span class="badge badge-warning">Pendiente</span>';
                }*/
            },
            //{ "data": "unidadSolicitante" },
            { "data": "fechaPlanificacion" },
            {
                "defaultContent": '<div class="form-inline">' +
                    '<button class="btn btn-info btn-certificar btn-sm" href=""><i class="fas fa-edit"></i> Certificar</button> ' +
                    '<button class="btn btn-primary btn-ver btn-sm"><i class="fas fa-eye"></i> Ver</button> ' +
                    '<button class="btn btn-default btn-imprimir btn-sm"><i class="fas fa-print"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>' +
                    '</div>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Listado Mis Carpetas',
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


let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-certificar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();

    window.location.href = `/Planificacion/CertificarPlanificacion?numeroCarpeta=${data.numeroPlanificacion}`;


})



$("#tbdata tbody").on("click", ".btn-eliminar", function () {

    let fila;

    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    }
    else {
        fila = $(this).closest("tr")
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "Está Seguro?",
        text: `Eliminar La Carpeta de Requerimiento: "${data.numeroCarpeta}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, Eliminar",
        cancelButtonText: "No, Cancelar",
        closeOnConfirm: false,
        closeOnCancel: true,
    },
        function (respuesta) {
            if (respuesta) {
                $(".showSweetalert").LoadingOverlay("show");
                alert('TEST');
                /* fetch(`/Empresa/Eliminar?IdEmpresa=${data.idEmpresa}`, {
                     method: "DELETE"
                 })
                     .then(response => {
                         $(".showSweetalert").LoadingOverlay("hide");
                         return response.ok ? response.json() : Promise.reject(response);
                     })
                     .then(responseJson => {
 
                         if (responseJson.estado) {
 
                             tablaData.row(fila).remove().draw()
 
                             swal("Listo!", "La Empresa Fue Eliminada", "success")
                         }
                         else {
                             swal("Lo Sentimos", responseJson.mensaje, "error")
                         }
                     })*/
            }
        }
    )
})

$("#tbdata tbody").on("click", ".btn-ver", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();
    $("#txtFechaRegistro").val(data.fechaPlanificacion)
    $("#txtNumeroPlanificacion").val(data.numeroPlanificacion)
    $("#txtCitePlanificacion").val(data.citePlanificacion)
    $("#txtUnidadSolicitante").val(data.nombreCentro)
    $("#txtUnidadResponsable").val(data.nombreUnidadResponsable)
    $("#txtObservacion").val(data.estadoCarpeta)
    if (data.estadoCarpeta == "INI") {
        $("#txtObservacion").val("INICIAL")
    }
    else {
        if (data.estadoCarpeta == "ANU") {
            $("#txtObservacion").val("ANULADO")
        }
        else {
            $("#txtObservacion").val("EN TRAMITE")
        }
    }

    $("#txtTotal").val(data.montoPlanificacion)

    if (data.estadoCarpeta == "INI") {
        $("#txtUnidadResponsable").val("INICIAL")
    }
    else {
        if (data == "ANU") {
            $("#txtUnidadResponsable").val("ANULADO")
        }
        else {
            $("#txtUnidadResponsable").val("EN TRAMITE")
        }
    }

    $("#tbPartida tbody").html("")
    cont = 0;
    console.log(data);
    data.detallePlanificacion.forEach((item) => {
        cont++;
        $("#tbPartida tbody").append(
            $("<tr>").append(
                $("<td>").text(cont),
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td>").text(item.cantidad),
                $("<td>").text(item.precio),
                $("<td>").text(item.total),
                $("<td>").text(item.codigoActividad),
            )
        )
    })
    $("#linkImprimir").attr("href", `/Planificacion/MostrarPDFCarpeta?numeroCarpeta=${data.numeroCarpeta}`);
    $("#modalData").modal("show")
})




