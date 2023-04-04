const MODELO_BASE = {
    idPlanificacion: 0,
    citePlanificacion: "",
    numeroPlanificacion: "",
    idDocumento: "",
    idCentro: "",
    idUnidadResponsable: "",
    idUsuario: "",
    lugar: "",
    certificadoPoa: "",
    referenciaPlanificacion: "",
    nombreRegional: "",
    nombreEjecutora: "",
    montoPlanificacion: 0,
    montoPoa: 0,
    montoPresupuesto: 0,
    montoCompra: 0,
    unidadProceso: "",
    estadoCarpeta: "",
    fechaPlanificacion: "",
    //nombreCentro:"",
    //nombreUnidadResponsable: "",
}

$(document).ready(function () {
    llamarMisCarpetasVista();
    llamarComplementosCombo();
    //llamarComplementosBusquedaVista();
})

let tablaData;
function llamarMisCarpetasVista() {
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

    let filaSeleccionada;

    $("#tbdata tbody").on("click", ".btn-ver", function () {
        if ($(this).closest("tr").hasClass("child")) {
            filaSeleccionada = $(this).closest("tr").prev();
        }
        else {
            filaSeleccionada = $(this).closest("tr")
        }

        const data = tablaData.row(filaSeleccionada).data();

        $("#txtFechaRegistroVista").val(data.fechaPlanificacion)
        $("#txtNumeroPlanificacionVista").val(data.numeroPlanificacion)
        $("#txtCitePlanificacionVista").val(data.citePlanificacion)
        $("#txtUnidadSolicitanteVista").val(data.nombreCentro)
        $("#txtUnidadResponsableVista").val(data.nombreUnidadResponsable)
        $("#txtObservacionVista").val(data.estadoCarpeta)
        if (data.estadoCarpeta == "INI") {
            $("#txtObservacionVista").val("INICIAL")
        }
        else {
            if (data.estadoCarpeta == "ANU") {
                $("#txtObservacionVista").val("ANULADO")
            }
            else {
                $("#txtObservacionVista").val("EN TRAMITE")
            }
        }

        $("#txtTotalVista").val(data.montoPlanificacion)

        if (data.estadoCarpeta == "INI") {
            $("#txtUnidadResponsableVista").val("INICIAL")
        }
        else {
            if (data == "ANU") {
                $("#txtUnidadResponsableVista").val("ANULADO")
            }
            else {
                $("#txtUnidadResponsableVista").val("EN TRAMITE")
            }
        }

        $("#tbPartidasVista tbody").html("")
        cont = 0;
        data.detallePlanificacion.forEach((item) => {
            cont++;
            $("#tbPartidasVista tbody").append(
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
        $("#modalDataVista").modal("show");
    })
}

function mostrarModalVista(modelo = MODELO_BASE) {
    $("#txtIdVista").val(modelo.idPlanificacion)
    $("#txtIdDocumentoVista").val(modelo.idDocumento)
    $("#txtIdCentroVista").val(modelo.idCentro)
    $("#txtIdUnidadResponsableVista").val(modelo.idUnidadResponsable)
    $("#txtIdUsuarioVista").val(modelo.idUsuario)
    $("#txtLugarVista").val(modelo.lugar)
    $("#txtCertificadoPoaVista").val(modelo.certificadoPoa)
    $("#txtReferenciaPlanificacionVista").val(modelo.referenciaPlanificacion)
    $("#txtNombreRegionalVista").val(modelo.nombreRegional)
    $("#txtNombreEjecutoraVista").val(modelo.nombreEjecutora)
    $("#txtMontoPoaVista").val(modelo.montoPoa)
    $("#txtMontoPresupuestoVista").val(modelo.montoPresupuesto)
    $("#txtMontoCompraVista").val(modelo.montoCompra)
    $("#txtUnidadProcesoVista").val(modelo.unidadProceso)
    $("#txtFechaRegistroVista").val(modelo.fechaPlanificacion)
    $("#txtNumeroPlanificacionVista").val(modelo.numeroPlanificacion)
    $("#txtCitePlanificacionVista").val(modelo.citePlanificacion)
    $("#txtUnidadSolicitanteVista").val(modelo.nombreCentro)
    $("#txtUnidadResponsableVista").val(modelo.nombreUnidadResponsable)
    $("#txtEstadoCarpetaVista").val(modelo.estadoCarpeta)
    $("#txtTotalVista").val(modelo.montoPlanificacion)
    if (modelo.estadoCarpeta == "INI") {
        $("#txtObservacionVista").val("INICIAL")
    }
    else {
        if (modelo.estadoCarpeta == "ANU") {
            $("#txtObservacionVista").val("ANULADO")
        }
        else {
            $("#txtObservacionVista").val("EN TRAMITE")
        }
    }
}

function llamarComplementosCombo() {
    fetch("/Planificacion/ListaCentrosalud")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboCentro").append(
                        $("<option>").val(item.idCentro).text(item.nombre)
                    )
                })
            }
        })
    fetch("/Planificacion/ListaUnidadResponsable")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboUnidadResponsable").append(
                        $("<option>").val(item.idUnidadResponsable).text(item.nombre)
                    )
                })
            }
        })
    fetch("/Planificacion/ListaTipoDocumento")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboDocumento").append(
                        $("<option>").val(item.idDocumento).text(item.descripcion)
                    )
                })
            }
        })
}

//function llamarComplementosBusquedaVista() {
//    $("#cboBuscarPartida").select2({
//        ajax: {
//            url: "/Planificacion/ObtenerPartidas",
//            dataType: 'json',
//            contentType: "/application/json; charset=utf-8",
//            delay: 250,
//            data: function (params) {
//                return {
//                    busqueda: params.term
//                };
//            },
//            processResults: function (data,) {
//                return {
//                    results: data.map((item) => (
//                        {
//                            id: item.idPartida,
//                            text: item.nombre,

//                            codigo: item.codigo,
//                            precio: parseFloat(item.precio)
//                        }
//                    ))
//                };
//            }
//        },
//        language: "es",
//        placeholder: 'Buscar Partida Presupuestaria.....',
//        minimumInputLength: 1,
//        templateResult: formatoResultadosVista,
//    });
//}
function formatoResultadosVista(data) {
    if (data.loading)
        return data.text;

    var contenedor = $(
        `<table width="100%">
            <tr>
                <td>
                    <p style="font-weight: bolder; margin:2px">${data.codigo}</p>
                    <p style="margin:2px">${data.text}</p>
                </td>
            </tr>
        </table>`
    );
    return contenedor;
}


//$(document).on("select2:open", function () {
//    document.querySelector(".select2-search__field").focus();
//})

//let PartidasParaPlanificacion = [];
//$("#cboBuscarPartida").on("select2:select", function (e) {
//    const data = e.params.data;

//    let partida_encontrada = PartidasParaPlanificacion.filter(p => p.idPartida == data.id);

//    swal({
//        title: `Partida:[${data.codigo}] : ${data.text} `,
//        html: true,
//        text: '<hr><table width="100% ">' +
//            "<tr> <td> Detalle Requerimiento</td> </tr>" +
//            '<tr><td><textarea id="txtSwalDetalle" class="form- control" rows="3"  style=" width:100% !important "></textarea></td></tr>' +
//            "<tr> <td> Unidad de Medida</td> </tr>" +
//            '<tr><td><input type="text" id="txtSwalUnidadMedida" style=" width: 50% !important "/></td></tr>' +
//            "<tr> <td> Cantidad</td> </tr>" +
//            '<tr><td><input type="text" id="txtSwalCantidad" style=" width: 50% !important "/></td></tr>' +
//            "<tr> <td> Precio Unitario</td> </tr>" +
//            '<tr><td><input type="text" id="txtSwalPrecioUnitario" style=" width: 50% !important "/></td></tr>' +
//            "<tr> <td> Actividad</td> </tr>" +
//            '<tr><td><input type="text" id="txtSwalActividad" style=" width: 10% !important "/></td></tr>' +
//            "</table> ",

//        showCancelButton: true,
//        closeOnConfirm: false,
//    },
//        function (e) {

//            if (e === false) return false;

//            var uDetalle = $('#txtSwalDetalle').val();
//            var uMedida = $('#txtSwalUnidadMedida').val();
//            var uCantidad = $('#txtSwalCantidad').val();
//            var uPrecioUnitario = $('#txtSwalPrecioUnitario').val();
//            var uActividad = $('#txtSwalActividad').val();

//            if (uDetalle === "") {
//                toastr.warning("", "Necesita Ingresar La Descripcion De La Partida")
//                return false;
//            }

//            if (uMedida === "") {
//                toastr.warning("", "No Deje En Blanco La Unidad De Medida")
//                return false;
//            }

//            if (uCantidad === "") {
//                toastr.warning("", "No Deje La Cantidad En Blanco")
//                return false;
//            }

//            if (uPrecioUnitario === "") {
//                toastr.warning("", "No Deje Precio Unitario En Blanco")
//                return false;
//            }

//            if (uActividad === "") {
//                toastr.warning("", "No Deje En Blanco La Actividad")
//                return false;
//            }

//            if (isNaN(parseInt(uActividad))) {
//                toastr.warning("", "Debe Ingresar Un Valor Numerico")
//                return false;
//            }

//            if (parseInt(uActividad) < 1 || parseInt(uActividad) > 41) {
//                toastr.warning("", "Debe Estar En El RAndo de 1 - 41")
//                return false;
//            }


//            if (isNaN(parseInt(uCantidad))) {
//                toastr.warning("", "Debe Ingresar Un Valor Numerico")
//                return false;
//            }
//            if (isNaN(parseInt(uPrecioUnitario))) {
//                toastr.warning("", "Debe Ingresar Un Valor Numerico")
//                return false;
//            }

//            let partida = {
//                idPartida: data.id,
//                nombrePartida: data.text,
//                nombreItem: uDetalle,
//                medida: uMedida,
//                codigoPartida: data.codigo,
//                cantidad: parseInt(uCantidad),
//                precio: parseFloat(uPrecioUnitario),
//                total: (uCantidad * uPrecioUnitario),
//                codigoActividad: uActividad
//            }

//            //console.log(partida)

//            PartidasParaPlanificacion.push(partida)

//            mostrarPartida_Precios()

//            $("#cboBuscarPartida").val("").trigger("change")

//            swal.close()
//        }
//    )
//})
//function mostrarPartida_Precios() {
//    let total = 0;

//    $("#tbPartida tbody").html("")
//    PartidasParaPlanificacion.forEach((item) => {
//        total = total + parseFloat(item.total)

//        $("#tbPartida tbody").append(
//            $("<tr>").append(
//                $("<td>").append(
//                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
//                        $("<I>").addClass("fas fa-trash-alt")
//                    ).data("idPartida", item.idPartida)
//                ),
//                $("<td>").text(item.codigoPartida),
//                $("<td>").text(item.nombreItem),
//                $("<td>").text(item.medida),
//                $("<td>").text(item.cantidad),
//                $("<td>").text(item.precio),
//                $("<td>").text(item.total.toFixed(2)),
//                $("<td>").text(item.codigoActividad),
//            )
//        )
//    })
//    $("#txtTotal").val(total.toFixed(2))
//}


let filaSeleccionadaVista;
$("#tbdata tbody").on("click", ".btn-ver", function () {

    const modelo = structuredClone(MODELO_BASE);

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionadaVista = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionadaVista = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionadaVista).data();

    mostrarModalVista(data);

    $("#tbPartidaVista tbody").html("")
    cont = 0;
    data.detallePlanificacion.forEach((item) => {
        cont++;
        $("#tbPartidaVista tbody").append(
            $("<tr>").append(
                $("<td>").text(cont),
                $("<td>").text(item.codigoPartida),
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
    $("#modalDataVista").modal("show");
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

    mostrarModalVista(data);

    //$("#txtIdVista").val(data.idPlanificacion)
    //$("#txtIdDocumentoVista").val(data.idDocumento)
    //$("#txtIdCentroVista").val(data.idCentro)
    //$("#txtIdUnidadResponsableVista").val(data.idUnidadResponsable)
    //$("#txtIdUsuarioVista").val(data.idUsuario)
    //$("#txtLugarVista").val(data.lugar)
    //$("#txtCertificadoPoaVista").val(data.certificadoPoa)
    //$("#txtReferenciaPlanificacionVista").val(data.referenciaPlanificacion)
    //$("#txtNombreRegionalVista").val(data.nombreRegional)
    //$("#txtNombreEjecutoraVista").val(data.nombreEjecutora)
    //$("#txtMontoPoaVista").val(data.montoPoa)
    //$("#txtMontoPresupuestoVista").val(data.montoPresupuesto)
    //$("#txtMontoCompraVista").val(data.montoCompra)
    //$("#txtUnidadProcesoVista").val(data.unidadProceso)
    //$("#txtFechaRegistroVista").val(data.fechaPlanificacion)
    //$("#txtNumeroPlanificacionVista").val(data.numeroPlanificacion)
    //$("#txtCitePlanificacionVista").val(data.citePlanificacion)
    //$("#txtUnidadSolicitanteVista").val(data.nombreCentro)
    //$("#txtUnidadResponsableVista").val(data.nombreUnidadResponsable)
    //$("#txtEstadoCarpetaVista").val(data.estadoCarpeta)
    //$("#txtObservacionVista").val(data.estadoCarpeta)
    //$("#txtTotalVista").val(data.montoPlanificacion)

    //if (data.estadoCarpeta == "INI") {
    //    $("#txtObservacionVista").val("")
    //}
    //else {
    //    if (data.estadoCarpeta == "ANU") {
    //        $("#txtObservacionVista").val("ANULADO")
    //    }
    //    else {
    //        $("#txtObservacionVista").val("")
    //    }
    //}

    //$("#tbPartidaVista tbody").html("")
    //cont = 0;
    //data.detallePlanificacion.forEach((item) => {
    //    cont++;
    //    $("#tbPartidaVista tbody").append(
    //        $("<tr>").append(
    //            $("<td>").text(cont),
    //            $("<td>").text(item.nombreItem),
    //            $("<td>").text(item.medida),
    //            $("<td>").text(item.cantidad),
    //            $("<td>").text(item.precio),
    //            $("<td>").text(item.total),
    //            $("<td>").text(item.codigoActividad),
    //        )
    //    )
    //})
    //$("#linkImprimir").attr("href", `/Planificacion/MostrarPDFCarpeta?numeroCarpeta=${data.numeroCarpeta}`);

    //if ($("#txtEstadoCarpetaVista").val() == "ANU") {
    //    swal("Atencion", "Carpeta De Planificacion Ya Esta Anulada!","warning");
    //    return;
    //}

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

                modelo["idPlanificacion"] = parseInt($("#txtIdVista").val())
                modelo["estadoCarpeta"] = "ANU"

                fetch("/Planificacion/Anular", {
                    method: "PUT",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(modelo)
                })
                    .then(response => {
                        $("#modalDataVista").find("div.modal-content").LoadingOverlay("hide");
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










$("#tbdata tbody").on("click", ".btn-editar", function () {

    const modelo = structuredClone(MODELO_BASE);

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();

    $("#txtEstadoCarpeta").val(modelo.estadoCarpeta)

    mostrarModalPlanificacion(data);

    if ($("#txtEstadoCarpeta").val() == "ANU") {
        swal("Atencion", "Esta Carpeta Esta Anulada. No Se Puede Modificar!", "warning");
        return;
    }

    $("#tbPartida tbody").html("")
    cont = 0;
    data.detallePlanificacion.forEach((item) => {
        cont++;
        $("#tbPartida tbody").append(
            $("<tr>").append(
                $("<td>").text(cont),
                $("<td>").text(item.codigoPartida),
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td>").text(item.cantidad),
                $("<td>").text(item.precio),
                $("<td>").text(item.total),
                $("<td>").text(item.codigoActividad),
            )
        )
    })
    $("#linkImprimir").attr("href", `/Planificacion/MostrarPDFCarpeta?numeroPlanificacion=${data.numeroPlanificacion}`);

    $("#modalData").modal("show")
})