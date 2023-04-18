const MODELO_BASE = {
    idEmpresa: 0,
    codigo: "",
    nombre: "",
    esActivo: 1,
}

let tablaData;
let formateadorDecimal = new Intl.NumberFormat('en-US', {
    // style: 'currency',
    //  currency: 'BOB',
    maximumFractionDigits: 2,
    minimumFractionDigits: 2
});
$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/planificacion/ListCarpetasCertificarPlanificacion',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idPlanificacion", "visible": true, "searchable": true },
            { "data": "numeroPlanificacion" },
            { "data": "citePlanificacion" },
            { "data": "nombreRegional" },
            { "data": "nombreEjecutora" },
            {
                "data": "detallePlanificacion", render: function (data) {
                    var cantidad = Object.values(data).length;
                    return '<div class="text-center text-red"> '+cantidad+' </div>';
                }
            },
            {
                "data": "montoPlanificacion", render: function (data) {
                    return '<div class="text-right text-info">'+data+'</div>';
                }
            },
            {
                "data": "estadoCarpeta", render: function (data) {
                    if (data == "INI") {
                        return `<span class="badge badge-info">INICIAL</span>`;
                    }
                    else {
                        if (data == "ANU") {
                            return '<span class="badge badge-secondary">ANULADO</span>';
                        }
                        else {
                            if (data == "PLA") {
                                return '<span class="badge badge-success">PLANIFICADO</span>';
                            }
                            else {
                                return '<span class="badge badge-danger">OTRO</span>';
                            }
                        }
                    }

                }
            },
            { "data": "fechaPlanificacion" },
            {
                "data": "estadoCarpeta", render: function (data) {
                    var swBtnEdit = '<button class="btn btn-info btn-certificar btn-sm" href=""><i class="fas fa-edit"></i> Certificar</button> ';
                   if (data == "PLA") {
                       swBtnEdit = '<button class="btn btn-info btn-certificar btn-sm" href=""><i class="fas fa-edit"></i> Modificar</button> ';                                               
                    }
                    return '<div class="form-inline">' + swBtnEdit +
                        '<button class="btn btn-primary btn-ver btn-sm"><i class="fas fa-eye"></i> Ver</button> ' +
                        '<button class="btn btn-default btn-imprimir btn-sm"><i class="fas fa-print"></i></button>' +
                        '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>' +
                        '</div>'                        

                }, "orderable": false,
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


$("#tbdata tbody").on("click", ".btn-imprimir", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();

    //window.location.href = ;
    window.open(`/Planificacion/MostrarPDFCertificacionPlanificacion?numeroPlanificacion=${data.numeroPlanificacion}`, '_blank');


})

$("#tbdata tbody").on("click", ".btn-eliminar", function () {

    const modelo = structuredClone(MODELO_BASE);

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();

    $("#txtId").val(data.idPlanificacion)
    $("#txtIdDocumento").val(data.idDocumento)
    $("#txtIdCentro").val(data.idCentro)
    $("#txtIdUnidadResponsable").val(data.idUnidadResponsable)
    $("#txtIdUsuario").val(data.idUsuario)
    $("#txtLugar").val(data.lugar)
    $("#txtCertificadoPoa").val(data.certificadoPoa)
    $("#txtReferenciaPlanificacion").val(data.referenciaPlanificacion)
    $("#txtNombreRegional").val(data.nombreRegional)
    $("#txtNombreEjecutora").val(data.nombreEjecutora)
    $("#txtMontoPoa").val(data.montoPoa)
    $("#txtMontoPresupuesto").val(data.montoPresupuesto)
    $("#txtMontoCompra").val(data.montoCompra)
    $("#txtUnidadProceso").val(data.unidadProceso)
    $("#txtFechaRegistro").val(data.fechaPlanificacion)
    $("#txtNumeroPlanificacion").val(data.numeroPlanificacion)
    $("#txtCitePlanificacion").val(data.citePlanificacion)
    $("#txtUnidadSolicitante").val(data.nombreCentro)
    $("#txtUnidadResponsable").val(data.nombreUnidadResponsable)
    $("#txtEstadoCarpeta").val(data.estadoCarpeta)
    $("#txtObservacion").val(data.estadoCarpeta)
    $("#txtTotal").val(data.montoPlanificacion)

    if (data.estadoCarpeta == "INI") {
        $("#txtObservacion").val("")
    }
    else {
        if (data.estadoCarpeta == "ANU") {
            $("#txtObservacion").val("ANULADO")
        }
        else {
            $("#txtObservacion").val("")
        }
    }

    $("#tbPartida tbody").html("")
    cont = 0;
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


    if ($("#txtEstadoCarpeta").val() == "ANU") {
        swal("Atencion", "Carpeta De Planificacion Ya Esta Anulada!", "warning");
        return;
    }

    swal({
        title: "Está Seguro de Anular?",
        text: '\n' + `Carpeta Planificacion: N°. "${data.numeroPlanificacion}"` + "\n" +
            `N°. Cite: "${data.citePlanificacion}` + "\n" + "\n" +
            `Fecha Registro: "${data.fechaPlanificacion}"` + "\n" +
            `Importe Total: "${data.montoPlanificacion}"` + "\n" + "\n" +
            `Unidad Solicitante: "${data.nombreCentro}"` + "\n" +
            `Unidad Responsable: "${data.nombreUnidadResponsable}"` + "\n",

        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, Anular",
        cancelButtonText: "No, Cancelar",
        closeOnConfirm: false,
        closeOnCancel: true,
    },
        function (respuesta) {
            if (respuesta) {
                $(".showSweetalert").LoadingOverlay("show");

                const modelo = structuredClone(MODELO_BASE);

                modelo["idPlanificacion"] = parseInt($("#txtId").val())
                modelo["estadoCarpeta"] = "ANU"

                //console.log(modelo);
                //alert(modelo["idPlanificacion"]);

                fetch("/Planificacion/Anular", {
                    method: "PUT",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(modelo)
                })
                    .then(response => {
                        $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);

                            filaSeleccionada = null;

                            swal("Listo!", "La Carpeta De Planificacion Fue Anulada", "success")
                            let nueva_url = `/Planificacion/ListaMisCarpetas`;
                            tablaData.ajax.url(nueva_url).load();
                        }
                        else {
                            swal("Lo Sentimos", responseJson.mensaje, "error")
                        }
                    })
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

    $("#txtTotal").val(formateadorDecimal.format(data.montoPlanificacion))

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
                $("<td class='text-center'>").text(item.cantidad),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.precio)),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.total)),
                $("<td>").text(item.codigoActividad),
            )
        )
    })
    $("#tbPartida tbody").append(
        $("<tr>").append(
            $("<td colspan='5' class='text-right font-weight-bold'>").text("Monto Total:"),
            $("<td class='text-right'>").text(data.montoPlanificacion),
        )
    )
    $("#linkImprimir").attr("href", `/Planificacion/MostrarPDFCarpeta?numeroCarpeta=${data.numeroCarpeta}`);
    $("#modalData").modal("show")
})




