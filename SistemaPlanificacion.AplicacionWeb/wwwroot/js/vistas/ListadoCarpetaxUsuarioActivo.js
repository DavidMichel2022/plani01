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

const MODELO_BASE = {
    idPlanificacion: 0,
    estadoCarpeta: ""
}

const MODELO_BASEEDICION = {
    idPlanificacion: 0,
    citePlanificacion: "",
    numeroPlanificacion: "",
    idDocumento: 0,
    idCentro: 0,
    idUnidadResponsable: 0,
    idUsuario: 0,
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
    nombreCentro: "",
    nombreUnidadResponsable: "",
    nombreDocumento: "",
    detallePlanificacion: "",
}

let filaSeleccionada;
let tablaData;
$(document).ready(function () {

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

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": `/Planificacion/ListaCarpetasxUsuario`,
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

            {
                "data": "montoPlanificacion", render: function (data) {
                    return '<div class="text-right">' + formateadorDecimal.format(data) + '</div>';
                }
            },

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
        //lengthMenu: [
        //    [10],
        //    ['Solo 10 filas']
        //],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });

    $("#cboBuscarPartida").select2({
        ajax: {
            url: "/Planificacion/ObtenerPartidas",
            dataType: 'json',
            contentType: "/application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return {
                    busqueda: params.term
                };
            },
            processResults: function (data,) {
                return {
                    results: data.map((item) => (
                        {
                            id: item.idPartida,
                            text: item.nombre,

                            codigo: item.codigo,
                            precio: parseFloat(item.precio)
                        }
                    ))
                };
            }
        },
        language: "es",
        placeholder: 'Buscar Partida Presupuestaria.....',
        minimumInputLength: 1,
        templateResult: formatoResultadosEdicion,
    });
    function formatoResultadosEdicion(data) {
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
})

$("#tbdata tbody").on("click", ".btn-ver", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }
    const data = tablaData.row(filaSeleccionada).data();

    let ImportePlanificacion = formateadorDecimal.format(data.montoPlanificacion)

    $("#txtFechaRegistro").val(data.fechaPlanificacion)
    $("#txtNumeroPlanificacion").val(data.numeroPlanificacion)
    $("#txtCitePlanificacion").val(data.citePlanificacion)
    $("#txtUnidadSolicitante").val(data.nombreCentro)
    $("#txtUnidadResponsable").val(data.nombreUnidadResponsable)
    $("#txtNombreDocumento").val(data.nombreDocumento)
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
    $("#txtTotal").val(ImportePlanificacion)

    $("#tbPartidas tbody").html("")
    cont = 0;
    data.detallePlanificacion.forEach((item) => {
        cont++;
        $("#tbPartidas tbody").append(
            $("<tr>").append(
                $("<td>").text(cont),
                $("<td>").text(item.codigoPartida),
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td class='text-right'>").text(formateadorEntero.format(item.cantidad)),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.precio)),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.total)),
                $("<td class='text-center'>").text(item.codigoActividad),
            )
        )
    })
    $("#linkImprimir").attr("href", `/Planificacion/MostrarPDFPlanificacion?numeroPlanificacion=${data.numeroPlanificacion}`);
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

    let ImportePlanificacion = formateadorDecimal.format(data.montoPlanificacion)

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
    $("#txtTotal").val(ImportePlanificacion)

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
                            let nueva_url = `/Planificacion/ListaCarpetasxUsuario`;
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

    //const modelo = structuredClone(MODELO_BASEEDICION);

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }
    const data = tablaData.row(filaSeleccionada).data();

    let ImportePlanificacion = formateadorDecimal.format(data.montoPlanificacion)

    $("#txtIdE").val(data.idPlanificacion)

    $("#txtCitePlanificacionE").val(data.citePlanificacion)
    $("#txtNumeroPlanificacionE").val(data.numeroPlanificacion)
    $("#txtIdDocumentoE").val(data.idDocumento)
    $("#txtIdCentroE").val(data.idCentro)
    $("#txtIdUnidadResponsableE").val(data.idUnidadResponsable)
    $("#txtIdUsuarioE").val(data.idUsuario)
    $("#txtLugarE").val(data.lugar)
    $("#txtCertificadoPoaE").val(data.certificadoPoa)
    $("#txtReferenciaPlanificacionE").val(data.referenciaPlanificacion)
    $("#txtNombreRegionalE").val(data.nombreRegional)
    $("#txtNombreEjecutoraE").val(data.nombreEjecutora)
    $("#txtTotalPlanificacionE").val(ImportePlanificacion)
    $("#txtMontoPoaE").val(data.montoPoa)
    $("#txtMontoPresupuestoE").val(data.montoPresupuesto)
    $("#txtMontoCompraE").val(data.montoCompra)
    $("#txtUnidadProcesoE").val(data.unidadProceso)
    $("#txtEstadoCarpetaE").val(data.estadoCarpeta)
    $("#txtFechaRegistroE").val(data.fechaPlanificacion)

    $("#txtUnidadSolicitanteE").val(data.nombreCentro)
    $("#txtUnidadResponsableE").val(data.nombreUnidadResponsable)

    $("#cboCentro").val(data.idCentro == 0 ? $("#cboCentro option:first").val() : data.idCentro)
    $("#cboUnidadResponsable").val(data.idUnidadResponsable == 0 ? $("#cboUnidadResponsable option:first").val() : data.idUnidadResponsable)
    $("#cboDocumento").val(data.idDocumento == 0 ? $("#cboDocumento option:first").val() : data.idDocumento)

    if ($("#txtEstadoCarpetaE").val() == "INI") {
        $("#txtObservacionE").val("INICIAL")
    }
    else {
        if ($("#txtEstadoCarpetaE").val() == "ANU") {
            $("#txtObservacionE").val("ANULADO")
        }
        else {
            $("#txtObservacionE").val("EN TRAMITE")
        }
    }

    //alert(data.idCentro);
    //alert($("#txtEstadoCarpetaE").val());

    $("#txtTotalPlanificacionE").val(data.montoPlanificacion)

    $("#tbPartidaEdicion tbody").html("")

    if ($("#txtObservacionE").val() == "ANULADO") {
        swal("Atencion", "Carpeta De Planificacion Anulada!", "warning");
        return;
    }


    CargarDetallePartidas(data.detallePlanificacion);

    $("#modalDataEdicion").modal("show");
})

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let PartidasParaEdicion = [];
$("#cboBuscarPartida").on("select2:select", function (e) {
    const data = e.params.data;

    let partida_encontrada = PartidasParaEdicion.filter(p => p.idPartida == data.id);

    swal({
        title: `Partida:[${data.codigo}] : ${data.text} `,
        html: true,
        text: '<hr><table width="100% ">' +
            "<tr> <td> Detalle Requerimiento</td> </tr>" +
            '<tr><td><textarea id="txtSwalDetalle" class="form- control" rows="3"  style=" width:100% !important "></textarea></td></tr>' +
            "<tr> <td> Unidad de Medida</td> </tr>" +
            '<tr><td><input type="text" id="txtSwalUnidadMedida" style=" width: 50% !important "/></td></tr>' +
            "<tr> <td> Cantidad</td> </tr>" +
            '<tr><td><input type="text" id="txtSwalCantidad" style=" width: 50% !important "/></td></tr>' +
            "<tr> <td> Precio Unitario</td> </tr>" +
            '<tr><td><input type="text" id="txtSwalPrecioUnitario" style=" width: 50% !important "/></td></tr>' +
            "<tr> <td> Actividad</td> </tr>" +
            '<tr><td><input type="text" id="txtSwalActividad" style=" width: 10% !important "/></td></tr>' +
            "</table> ",

        showCancelButton: true,
        closeOnConfirm: false,
    },
        function (e) {

            if (e === false) return false;

            var uDetalle = $('#txtSwalDetalle').val();
            var uMedida = $('#txtSwalUnidadMedida').val();
            var uCantidad = $('#txtSwalCantidad').val();
            var uPrecioUnitario = $('#txtSwalPrecioUnitario').val();
            var uActividad = $('#txtSwalActividad').val();

            var rd = Math.floor(Math.random() * 99999);

            let partida = {
                idPartida: data.id,
                codigoPartida: data.codigo,
                nombrePartida: data.text,
                nombreItem: uDetalle,
                medida: uMedida,
                cantidad: parseInt(uCantidad),
                precio: parseFloat(uPrecioUnitario),
                total: (uCantidad * uPrecioUnitario),
                codigoActividad: uActividad,
                idFila: rd
            }

            PartidasParaEdicion.push(partida)
            mostrarPartida_Precios()

            $("#cboBuscarPartida").val("").trigger("change")

            swal.close()
        }
    )
})
function mostrarPartida_Precios() {
    let total = 0;

    $("#tbPartidaEdicion tbody").html("")
    PartidasParaEdicion.forEach((item) => {
        total = total + parseFloat(item.total)

        $("#tbPartidaEdicion tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<I>").addClass("fas fa-trash-alt")
                    ).data("idFila", item.idFila)
                ),
                $("<td>").text(item.codigoPartida),
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td class='text-right'>").text(formateadorEntero.format(item.cantidad)),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.precio)),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.total)),
                $("<td class='text-center'>").text(item.codigoActividad),
            )
        )
    })

    let ImportePlanificacion = formateadorDecimal.format(total)

    $("#txtTotal").val(ImportePlanificacion)
    $("#txtTotalPlanificacionE").val(ImportePlanificacion)
}

function CargarDetallePartidas(TablaDetalle) {
    PartidasParaEdicion.splice(0, PartidasParaEdicion.length);

    TablaDetalle.forEach((item) => {
        var rd = Math.floor(Math.random() * 99999);
        let partida = {
            idPartida: item.idPartida,
            codigoPartida: item.codigoPartida,
            nombrePartida: item.text,
            nombreItem: item.nombreItem,
            medida: item.medida,
            cantidad: parseInt(item.cantidad),
            precio: parseFloat(item.precio),
            total: item.total,
            codigoActividad: item.codigoActividad,
            idFila: rd
        }
        PartidasParaEdicion.push(partida)
        mostrarPartida_Precios();
    })
}

$(document).on("click", "button.btn-eliminar", function () {
    const _idPartida = $(this).data("idFila")
    PartidasParaEdicion = PartidasParaEdicion.filter(p => p.idFila != _idPartida);
    mostrarPartida_Precios();
})

$("#btnGuardar").click(function (e) {
    const modelo = structuredClone(MODELO_BASEEDICION);

    modelo["idPlanificacion"] = parseInt($("#txtIdE").val())
    modelo["citePlanificacion"] = $("#txtCitePlanificacionE").val()
    modelo["numeroPlanificacion"] = $("#txtNumeroPlanificacionE").val()
    modelo["idDocumento"] = $("#cboDocumento").val()
    modelo["idCentro"] = $("#cboCentro").val()
    modelo["idUnidadResponsable"] = $("#cboUnidadResponsable").val()
    modelo["lugar"] = $("#txtLugarE").val()
    modelo["certificadoPoa"] = $("#txtCertificadoPoaE").val()
    modelo["referenciaPlanificacion"] = $("#txtReferenciaPlanificacionE").val()
    modelo["nombreRegional"] = $("#txtNombreRegionalE").val()
    modelo["nombreEjecutora"] = $("#txtNombreEjecutoraE").val()
    modelo["montoPlanificacion"] = parseFloat($("#txtTotalPlanificacionE").val())
    modelo["montoPoa"] = parseFloat($("#txtMontoPoaE").val())
    modelo["montoPresupuesto"] = parseFloat($("#txtMontoPresupuestoE").val())
    modelo["montoCompra"] = parseFloat($("#txtMontoCompraE").val())
    modelo["unidadProceso"] = $("#txtUnidadProcesoE").val()
    modelo["estadoCarpeta"] = $("#txtEstadoCarpetaE").val()
    modelo["fechaPlanificacion"] = $("#txtFechaRegistroE").val()


    modelo["detallePlanificacion"] = PartidasParaEdicion

    const data = tablaData.row(filaSeleccionada).data();


    IdPlanificacion = modelo["idPlanificacion"];

    ////alert('este es el Valor ' + IdPlanificacion);
    //alert('este es el Valor ' + data.idPlanificacion);
    //alert($("#txtTotalPlanificacionE").val());
    //console.log(modelo);

    fetch("/Planificacion/Editar", {
        method: "PUT",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(modelo)
    })
        .then(response => {
            $(".showSweetalert").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {
                tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                //swal("Listo!", "La Carpeta De Planificacion Fue Modificada", "success")
            }
            else {
                swal("Lo Sentimos", responseJson.mensaje, "error")
            }
        })

    //console.log(data);
    //alert('este es el Valor ' + data.idPlanificacion);  

    swal({
        title: "Listo!",
        text: "La Carpeta De Planificacion Fue Modificada",
        icon: "success",
        showConfirmButton: true,
    },
        function (e) {
            $("#modalDataEdicion").modal("hide");
            let nueva_url = `/Planificacion/ListaCarpetasxUsuario`;
            tablaData.ajax.url(nueva_url).load();
            location.reload(true);
        }
    );
})