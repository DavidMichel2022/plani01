const MODELO_BASE = {
    idPlanificacion: 0,
    //citePlanificacion: "",
    //numeroPlanificacion: "",
    //idDocumento: 0,
    //idCentro: 0,
    //idUnidadResponsable: 0,
    //idUsuario: 0,
    //lugar: "",
    //certificadoPoa: "",
    //referenciaPlanificacion: "",
    //nombreRegional: "",
    //nombreEjecutora: "",
    //montoPlanificacion: 0,
    //montoPoa: 0,
    //montoPresupuesto: 0,
    //montoCompra: 0,
    //unidadProceso: "",
    estadoCarpeta: "",
    //fechaPlanificacion: "",
}

let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({

        responsive: true,
        "ajax": {
            "url": `/Planificacion/ListaMisCarpetas`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //{ "data": "idPlanificacion", "visible": false, "searchable": true },
            { "data": "fechaPlanificacion" },
            { "data": "numeroPlanificacion" },
            { "data": "citePlanificacion" },
            { "data": "nombreCentro" },
            { "data": "nombreUnidadResponsable" },
            { "data": "montoPlanificacion" },
            {
                "data": "estadoCarpeta", render: function (data) {
                    if (data == "INI") {
                        return `<span class="badge badge-info">Inicial</span>`;
                    }
                    else {
                        if (data == "ANU") {
                            return '<span class="badge badge-info">Anulado</span>';
                        }
                        else {
                            return '<span class="badge badge-info">En Tramite</span>';
                        }
                    }

                }
            },
            {
                "defaultContent": `<div class="form-inline">` +
                    `<button class="btn btn-info btn-ver btn-sm"><i class="fas fa-eye"></i></button>` +
                    `<button class="btn btn-primary btn-editar btn-sm"><i class="fas fa-pencil-alt"></i></button>` +
                    `<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>` +
                    `</div>`,
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
                filename: `Reporte Listado Mis Carpetas`,
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            }, `pageLength`
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
    let nueva_url = `/Planificacion/ListaMisCarpetas`;

    //tablaData.ajax.url(nueva_url).load();
})

//alert($("data.citePlanificacion").val();

let filaSeleccionada;

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

    $("#tbPartidas tbody").html("")
    cont = 0;
    data.detallePlanificacion.forEach((item) => {
        cont++;
        $("#tbPartidas tbody").append(
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
    $("#modalData").modal("show");
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

    $("#tbPartidas tbody").html("")
    cont = 0;
    data.detallePlanificacion.forEach((item) => {
        cont++;
        $("#tbPartidas tbody").append(
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

/*
$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();

    // mostrarModal(data);
    alert("Editar Carpeta Requerimiento PENDIENTE......");
})*/

$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();

    window.location.href = `/Planificacion/EditarPlanificacion?numeroCarpeta=${data.numeroPlanificacion}`;


})
