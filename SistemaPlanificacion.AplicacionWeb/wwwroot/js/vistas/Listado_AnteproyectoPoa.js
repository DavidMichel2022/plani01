let formateadorDecimal = new Intl.NumberFormat('en-US', {
    maximumFractionDigits: 2,
    minimumFractionDigits: 2
});

let formateadorEntero = new Intl.NumberFormat('en-US', {
    maximumFractionDigits: 2,
    minimumFractionDigits: 0
});

const MODELO_BASE = {
    idAnteproyecto: 0,
    estadoAnteproyecto: ""
}

const MODELO_BASEEDICION = {
    idAnteproyecto: 0,
    citeAnteproyecto: "",
    idCentro: 0,
    idUnidadResponsable: 0,
    idUsuario: 0,
    lugar: "",
    nombreRegional: "",
    nombreEjecutora: "",
    montoAnteproyecto: 0,
    estadoAnteproyecto: "",
    fechaAnteproyecto: "",
    nombreCentro: "",
    nombreUnidadResponsable: "",
    detalleAnteproyectoPoas: "",
}

let ImporteAnteproyecto = 0;
let filaSeleccionada;
let tablaData;
let TablaDetalle;

$(document).ready(function () {

    fetch("/AnteproyectoPoa/ListaCentrosalud")
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

    fetch("/AnteproyectoPoa/ListaUnidadResponsable")
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

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": `/AnteproyectoPoa/ListaMisAnteproyectosPoa`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //{ "data": "idAnteproyecto", "visible": false, "searchable": true },
            { "data": "fechaAnteproyecto" },
            { "data": "citeAnteproyecto" },
            { "data": "nombreCentro" },
            { "data": "nombreUnidadResponsable" },

            {
                "data": "montoAnteproyecto", render: function (data) {
                    return '<div class="text-right">' + formateadorDecimal.format(data) + '</div>';
                }
            },

            {
                "data": "estadoAnteproyecto", render: function (data) {
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
                filename: `Reporte Listado Mis Anteproyectos Poa`,
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
        ],
        lengthMenu: [
            [10],
            ['Solo 10 filas']
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });

    $("#cboBuscarPartida").select2({

        ajax: {
            url: "/AnteproyectoPoa/ObtenerPartidasAnteproyecto",
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

    $("#cboBuscarUnidad").select2({
        ajax: {
            url: "/AnteproyectoPoa/ObtenerUnidadesAnteproyecto",
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
                            id: item.idUnidad,
                            text: item.nombre,
                            codigo: item.codigo,
                        }
                    ))
                };
            }
        },
        language: "es",
        placeholder: 'Buscar Unidad De Presentacion.....',
        minimumInputLength: 1,
        templateResult: formatoResultadosUnidad,
    });
})
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

function formatoResultadosUnidad(data) {
    if (data.loading)
        return data.text;
    var contenedorUnidad = $(

        `<table width="100%">
            <tr>
                <td>
                    <p style="font-weight: bolder; margin:2px">${data.text}</p>
                </td>
            </tr>
        </table>`
    );
    return contenedorUnidad;
}

$("#tbdata tbody").on("click", ".btn-ver", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }
    const data = tablaData.row(filaSeleccionada).data();

    let ImporteAnteproyecto = formateadorDecimal.format(data.montoAnteproyecto)

    $("#txtFechaRegistro").val(data.fechaAnteproyecto)
    $("#txtCiteAnteproyecto").val(data.citeAnteproyecto)
    $("#txtUnidadSolicitante").val(data.nombreCentro)
    $("#txtUnidadResponsable").val(data.nombreUnidadResponsable)
    $("#txtObservacion").val(data.estadoAnteproyecto)
    if (data.Anteproyecto == "INI") {
        $("#txtObservacion").val("INICIAL")
    }
    else {
        if (data.estadoAnteproyecto == "ANU") {
            $("#txtObservacion").val("ANULADO")
        }
        else {
            $("#txtObservacion").val("EN TRAMITE")
        }
    }
    $("#tbPartidas tbody").html("")
    cont = 0;
    //console.log(data);
    //console.log(data.detalleAnteproyectoPoas);
    let totalAnteproyecto = 0;
    data.detalleAnteproyectoPoas.forEach((item) => {
        cont++;
        totalAnteproyecto = totalAnteproyecto + item.total,
        $("#tbPartidas tbody").append(
            $("<tr>").append(
                $("<td>").text(cont),
                $("<td>").text(item.codigoActividad),
                $("<td>").text(item.codigoPartida),
                $("<td>").text(item.detalle),
                $("<td>").text(item.medida),
                $("<td>").text(formateadorDecimal.format(item.cantidad)),
                $("<td>").text(formateadorDecimal.format(item.precio)),
                $("<td>").text(formateadorDecimal.format(item.total)),
                $("<td>").text(item.observacion),
                $("<td>").text(formateadorDecimal.format(item.mesEne)),
                $("<td>").text(formateadorDecimal.format(item.mesFeb)),
                $("<td>").text(formateadorDecimal.format(item.mesMar)),
                $("<td>").text(formateadorDecimal.format(item.mesAbr)),
                $("<td>").text(formateadorDecimal.format(item.mesMay)),
                $("<td>").text(formateadorDecimal.format(item.mesJun)),
                $("<td>").text(formateadorDecimal.format(item.mesJul)),
                $("<td>").text(formateadorDecimal.format(item.mesAgo)),
                $("<td>").text(formateadorDecimal.format(item.mesSep)),
                $("<td>").text(formateadorDecimal.format(item.mesOct)),
                $("<td>").text(formateadorDecimal.format(item.mesNov)),
                $("<td>").text(formateadorDecimal.format(item.mesDic)),
            )
        )
    })
    $("#txtTotalAnteproyecto").val(formateadorDecimal.format(totalAnteproyecto));

    $("#linkImprimir").attr("href", `/AnteproyectoPoa/MostrarPDFAnteproyectoPoa?citeAnteproyectoPoa=${data.citeAnteproyectoPoa}`);

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

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    let ImporteAnteproyecto = formateadorDecimal.format(data.montoAnteproyecto)

    $("#txtId").val(data.idAnteproyecto)
    $("#txtIdCentro").val(data.idCentro)
    $("#txtIdUnidadResponsable").val(data.idUnidadResponsable)
    $("#txtIdUsuario").val(data.idUsuario)
    $("#txtLugar").val(data.lugar)
    $("#txtNombreRegional").val(data.nombreRegional)
    $("#txtNombreEjecutora").val(data.nombreEjecutora)
    $("#txtFechaRegistro").val(data.fechaAnteproyecto)
    $("#txtCiteAnteproyecto").val(data.citeAnteproyecto)
    $("#txtUnidadSolicitante").val(data.nombreCentro)
    $("#txtUnidadResponsable").val(data.nombreUnidadResponsable)
    $("#txtEstadoAnteproyecto").val(data.estadoAnteproyecto)
    $("#txtObservacion").val(data.estadoCarpeta)
    $("#txtTotalAnteproyecto").val(ImporteAnteproyecto)

    if (data.estadoAnteproyecto == "INI") {
        $("#txtObservacion").val("")
    }
    else {
        if (data.estadoAnteproyecto == "ANU") {
            $("#txtObservacion").val("ANULADO")
        }
        else {
            $("#txtObservacion").val("")
        }
    }

    if ($("#txtEstadoAnteproyecto").val() == "ANU") {
        swalWithBootstrapButtons.fire(
            'Anteproyecto Ya Esta Anulado!',
            'No Puede Volver a Anular.',
            'warning'
        )
        return;
    }

    swalWithBootstrapButtons.fire({
        title: 'Está Seguro de Anular?',
        html: `<div>
                Carpeta Anteproyecto: Cite N°. ${data.citeAnteproyecto}<br/>
                Fecha Registro: ${data.fechaAnteproyecto}<br/>
                Importe Total: ${data.montoAnteproyecto}<br/>
                Unidad Solicitante: ${data.nombreCentro}<br/>
                Unidad Responsable: ${data.nombreUnidadResponsable}<br/>
              </div>`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: '<i class="fa fa-thumbs-up"></i> Anular!',
        cancelButtonText: '<i class="fa fa-thumbs-down"></i> Cancelar!',
        reverseButtons: false
    }).then((result) => {
        if (result.isConfirmed) {
                $(".showSweetalert").LoadingOverlay("show");
                const modelo = structuredClone(MODELO_BASE);
                modelo["idAnteproyecto"] = parseInt($("#txtId").val())
                modelo["estadoAnteproyecto"] = "ANU"
            fetch("/AnteproyectoPoa/Anular", {
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

                        swalWithBootstrapButtons.fire(
                            'Anulado!',
                            'Su Anteproyecto Esta Anulado.',
                            'success'
                        )
                        let nueva_url = `/AnteproyectoPoa/ListaMisAnteproyectosPoa`;
                        tablaData.ajax.url(nueva_url).load();
                    }
                    else {
                        swalWithBootstrapButtons.fire(
                            'Lo Sentimos!',
                            'No Fue Posible Anular Este Anteproyecto.',
                            'error'
                        )
                    }
                })
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelado',
                'Su Anteproyecto No Fue Anulado',
                'error'
            )
        }
    })
})

$("#tbdata tbody").on("click", ".btn-editar", function () {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }
    const data = tablaData.row(filaSeleccionada).data();

    let ImporteAnteproyecto = formateadorDecimal.format(data.montoAnteproyecto)

    $("#txtIdE").val(data.idAnteproyecto)

    $("#txtFechaRegistroE").val(data.fechaAnteproyecto)
    $("#txtCiteAnteproyectoE").val(data.citeAnteproyecto)
    $("#txtUnidadSolicitanteE").val(data.nombreCentro)
    $("#txtUnidadResponsableE").val(data.nombreUnidadResponsable)
    if (data.estadoAnteproyecto == "INI") {
        $("#txtObservacionE").val("INICIAL")
    }
    else {
        if (data.estadoAnteproyecto == "ANU") {
            $("#txtObservacionE").val("ANULADO")
        }
        else {
            $("#txtObservacionE").val("EN TRAMITE")
        }
    }
    $("#txtIdCentroE").val(data.idCentro)
    $("#txtIdUnidadResponsableE").val(data.idUnidadResponsable)
    $("#txtIdUsuarioE").val(data.idUsuario)
    $("#txtLugarE").val(data.lugar)
    $("#txtNombreRegionalE").val(data.nombreRegional)
    $("#txtNombreEjecutoraE").val(data.nombreEjecutora)
    $("#txtTotalAnteproyectoE").val(ImporteAnteproyecto)
    $("#txtEstadoAnteproyectoE").val(data.estadoAnteproyecto)

    $("#tbPartidaEdicion tbody").html("")

    if ($("#txtObservacionE").val() == "ANULADO") {
        swalWithBootstrapButtons.fire(
            'Atencion!',
            'El Anteproyecto Poa Fue Anulado',
            'warning'
        )
        return;
    }

    CargarDetallePartidas(data.detalleAnteproyectoPoas);

    $("#modalDataEdicion").modal("show");
})

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let PartidasParaEdicion = [];
$("#cboBuscarPartida").on("select2:select", function (e) {
    const data = e.params.data;

    let partida_encontrada = PartidasParaEdicion.filter(p => p.idPartida == data.id);

    $("#txtIdPartidaModal").val(data.id);
    $("#txtCodigoPartidaModal").val(data.codigo);
    $("#txtNombrePartidaModal").val(data.text);

    let tituloMensaje = ($("#txtCodigoPartidaModal").val().trim() + " - " + $("#txtNombrePartidaModal").val())


    $("#txtTituloMensaje").val(tituloMensaje);

    $("#modalRegistroPartida").modal("show");
})

$("#cboBuscarUnidad").on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let PartidasParaAnteproyectoPoaUnidad = [];
let partida_encontradaUnidad;
$("#cboBuscarUnidad").on("select2:select", function (e) {
    const data = e.params.data;

    let partida_encontradaUnidad = PartidasParaAnteproyectoPoaUnidad.filter(p => p.idUnidad == data.id);

    $("#modalDataEdicion").modal("show")
})

function mostrarPartida_Precios() {
    let totalAnteproyecto = 0;

    $("#tbPartidaEdicion tbody").html("")
    PartidasParaEdicion.forEach((item) => {
        totalAnteproyecto = totalAnteproyecto + parseFloat(item.total)
        $("#tbPartidaEdicion tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<I>").addClass("fas fa-trash-alt")
                    ),
                    $("<button>").addClass("btn btn-primary btn-editar btn-sm").append(
                        $("<I>").addClass("fas fa-pencil-alt")
                    ).data("idFila", item.idFila),
                ),
                $("<td>").text(item.codigoActividad),
                $("<td>").text(item.codigoPartida),
                $("<td>").text(item.detalle),
                $("<td>").text(item.medida),
                $("<td>").text(formateadorDecimal.format(item.cantidad)),
                $("<td>").text(formateadorDecimal.format(item.precio)),
                $("<td>").text(formateadorDecimal.format(item.total)),
                $("<td>").text(item.observacion),
                $("<td>").text(formateadorDecimal.format(item.mesEne)),
                $("<td>").text(formateadorDecimal.format(item.mesFeb)),
                $("<td>").text(formateadorDecimal.format(item.mesMar)),
                $("<td>").text(formateadorDecimal.format(item.mesAbr)),
                $("<td>").text(formateadorDecimal.format(item.mesMay)),
                $("<td>").text(formateadorDecimal.format(item.mesJun)),
                $("<td>").text(formateadorDecimal.format(item.mesJul)),
                $("<td>").text(formateadorDecimal.format(item.mesAgo)),
                $("<td>").text(formateadorDecimal.format(item.mesSep)),
                $("<td>").text(formateadorDecimal.format(item.mesOct)),
                $("<td>").text(formateadorDecimal.format(item.mesNov)),
                $("<td>").text(formateadorDecimal.format(item.mesDic)),
            )
        )
    })

    let ImporteAnteproyecto = totalAnteproyecto
    document.getElementById("txtTotalAnteproyecto").value = formateadorDecimal.format(ImporteAnteproyecto.toFixed(2))
    document.getElementById("txtTotalAnteproyectoE").value = formateadorDecimal.format(ImporteAnteproyecto.toFixed(2))
}

function CargarDetallePartidas(TablaDetalle) {
    PartidasParaEdicion.splice(0, PartidasParaEdicion.length);

    TablaDetalle.forEach((item) => {
        var rd = Math.floor(Math.random() * 99999);
        let partida = {
            codigoActividad: item.codigoActividad,
            idPartida: item.idPartida,
            codigoPartida: item.codigoPartida,
            nombrePartida: item.text,
            detalle: item.detalle,
            medida: item.medida,
            cantidad: parseFloat(item.cantidad),
            precio: parseFloat(item.precio),
            total: item.total,
            observacion: item.observacion,
            mesEne: parseFloat(item.mesEne),
            mesFeb: parseFloat(item.mesFeb),
            mesMar: parseFloat(item.mesMar),
            mesAbr: parseFloat(item.mesAbr),
            mesMay: parseFloat(item.mesMay),
            mesJun: parseFloat(item.mesJun),
            mesJul: parseFloat(item.mesJul),
            mesAgo: parseFloat(item.mesAgo),
            mesSep: parseFloat(item.mesSep),
            mesOct: parseFloat(item.mesOct),
            mesNov: parseFloat(item.mesNov),
            mesDic: parseFloat(item.mesDic),
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

$("#btnGuardar").click(function () {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    const modelo = structuredClone(MODELO_BASEEDICION);

    modelo["idAnteproyecto"] = parseInt($("#txtIdE").val())
    modelo["citeAnteproyecto"] = $("#txtCiteAnteproyectoE").val()
    modelo["idCentro"] = $("#txtIdCentroE").val()
    modelo["idUnidadResponsable"] = $("#txtIdUnidadResponsableE").val()
    modelo["lugar"] = $("#txtLugarE").val()
    modelo["nombreRegional"] = $("#txtNombreRegionalE").val()
    modelo["nombreEjecutora"] = $("#txtNombreEjecutoraE").val()
    modelo["montoAnteproyecto"] = parseFloat($("#txtTotalAnteproyectoE").val())
    modelo["estadoAnteproyecto"] = $("#txtEstadoAnteproyectoE").val()
    modelo["fechaAnteproyecto"] = $("#txtFechaRegistroE").val()

    modelo["detalleAnteproyectoPoas"] = PartidasParaEdicion

    const data = tablaData.row(filaSeleccionada).data();

    IdAnteproyecto = modelo["idAnteproyecto"];

/*    console.log(data.detalleAnteproyectoPoas);*/

    fetch("/AnteproyectoPoa/Editar", {
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

                swalWithBootstrapButtons.fire({
                    title: 'Listo!',
                    text: 'Este Anteproyecto Poa Fue Modificado',
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonText: 'Ok!',
                    cancelButtonText: 'No, Cancelar!',
                    reverseButtons: false
                }).then((result) => {
                    if (result.isConfirmed) {
                        $("#modalDataEdicion").modal("hide");
                    //    let nueva_url = `/RequerimientoPoa/ListaMisAnteproyectosPoa`;
                    //    tablaData.ajax.url(nueva_url).load();
                    //    location.reload(true);
                    } else if (
                        /* Read more about handling dismissals below */
                        result.dismiss === Swal.DismissReason.cancel
                    ) {
                        swalWithBootstrapButtons.fire(
                            'Lo Sentimos',
                            'Su Anteproyecto No Fue Modificado',
                            'error'
                        )
                    }
                })
            }
            else {
                swal("Lo Sentimos", responseJson.mensaje, "error")
            }
        })
})

function limpiarModal() {
    $('#txtIdPartidaModal').val("")
    $('#txtCodigoPartidaModal').val("")
    $('#txtNombrePartidaModal').val("")
    $("#cboBuscarUnidad").html("");
    $('#txtImporteTotalModal').val("")
    $('#txtImporteSaldoModal').val("")

    $('#txtActividadModal').val("")
    $('#txtDetalleModal').val("")
    $('#txtMedidaModal').val("")
    $('#txtCantidadModal').val("")
    $('#txtPrecioModal').val("")
    $('#txtObservacionModal').val("")

    $('#txtEneroModal').val("")
    $('#txtFebreroModal').val("")
    $('#txtMarzoModal').val("")
    $('#txtAbrilModal').val("")
    $('#txtMayoModal').val("")
    $('#txtJunioModal').val("")
    $('#txtJulioModal').val("")
    $('#txtAgostoModal').val("")
    $('#txtSeptiembreModal').val("")
    $('#txtOctubreModal').val("")
    $('#txtNoviembreModal').val("")
    $('#txtDiciembreModal').val("")

    EstiloSaldoModal = document.getElementById("txtImporteTotalModal");
    EstiloSaldoModal.style.border = 'solid black 2px';

    EstiloSaldoModal = document.getElementById("txtImporteSaldoModal");
    EstiloSaldoModal.style.border = 'solid black 2px';

    document.querySelector('#grupo__txtActividadModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtDetalleModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtCantidadModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtPrecioModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtObservacionModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtEneroModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtFebreroModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtMarzoModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtAbrilModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtMayoModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtJunioModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtJulioModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtAgostoModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtSeptiembreModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtOctubreModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtNoviembreModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtDiciembreModal i').classList.remove('fa-check-circle')
}

function reiniciarConstantes() {
    campos.txtActividadModal = false;
    campos.txtCantidadModa = false;
    campos.txtPrecioModal = false;
    campos.txtEneroModal = false;
    campos.txtFebreroModal = false;
    campos.txtMarzoModal = false;
    campos.txtAbrilModal = false;
    campos.txtMayoModal = false;
    campos.txtJunioModal = false;
    campos.txtJulioModal = false;
    campos.txtAgostoModal = false;
    campos.txtSeptiembreModal = false;
    campos.txtOctubreModal = false;
    campos.txtNoviembreModal = false;
    campos.txtDiciembreModal = false;
}

$("#btnGuardarModalAnteproyecto").click(function (data) {
    var uTotal = 0;

    $('#txtActividadModal').val() == "" ? cActividad = 0 : cActividad = parseFloat($('#txtActividadModal').val());
    $('#txtCantidadModal').val() == "" ? cCantidad = 0 : cCantidad = parseFloat($('#txtCantidadModal').val());
    $('#txtPrecioModal').val() == "" ? cPrecio = 0 : cPrecio = parseFloat($('#txtPrecioModal').val());

    var codigoUnidad = document.getElementById("cboBuscarUnidad").value;
    var combo = document.getElementById("cboBuscarUnidad");
    var selected = combo.options[combo.selectedIndex].text;

    var uUnidadMedida = selected;

    var uidPartida = $('#txtIdPartidaModal').val();
    var uCodigoPartidaModal = $('#txtCodigoPartidaModal').val();
    var uNombrePartidaModal = $('#txtNombrePartidaModal').val();

    var uSaldoImporte = $('#txtImporteSaldoSinFormatoModal').val();

    if ($('#txtEneroModal').val() === "") { var uMesEne = 0 } else { var uMesEne = $('#txtEneroModal').val() };
    if ($('#txtFebreroModal').val() === "") { var uMesFeb = 0 } else { var uMesFeb = $('#txtFebreroModal').val() };
    if ($('#txtMarzoModal').val() === "") { var uMesMar = 0 } else { var uMesMar = $('#txtMarzoModal').val() };
    if ($('#txtAbrilModal').val() === "") { var uMesAbr = 0 } else { var uMesAbr = $('#txtAbrilModal').val() };
    if ($('#txtMayoModal').val() === "") { var uMesMay = 0 } else { var uMesMay = $('#txtMayoModal').val() };
    if ($('#txtJunioModal').val() === "") { var uMesJun = 0 } else { var uMesJun = $('#txtJunioModal').val() };
    if ($('#txtJulioModal').val() === "") { var uMesJul = 0 } else { var uMesJul = $('#txtJulioModal').val() };
    if ($('#txtAgostoModal').val() === "") { var uMesAgo = 0 } else { var uMesAgo = $('#txtAgostoModal').val() };
    if ($('#txtSeptiembreModal').val() === "") { var uMesSep = 0 } else { var uMesSep = $('#txtSeptiembreModal').val() };
    if ($('#txtOctubreModal').val() === "") { var uMesOct = 0 } else { var uMesOct = $('#txtOctubreModal').val() };
    if ($('#txtNoviembreModal').val() === "") { var uMesNov = 0 } else { var uMesNov = $('#txtNoviembreModal').val() };
    if ($('#txtDiciembreModal').val() === "") { var uMesDic = 0 } else { var uMesDic = $('#txtDiciembreModal').val() };

    var uActividad = cActividad;
    var uDetalle = $('#txtDetalleModal').val();
    var uMedida = uUnidadMedida;
    var uCantidad = cCantidad;
    var uPrecioUnitario = cPrecio;
    var uObservacion = $('#txtObservacionModal').val();
    var uTotal = (parseFloat(uCantidad) * parseFloat(uPrecioUnitario));

    var rd = Math.floor(Math.random() * 99999);

    if ((cActividad <= 0) || (cActividad > 42)) {
        swal.fire({
            title: "Atencion!",
            text: "El Codigo De Actividad Debe Estar Entre 1 y 42.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }

    if (uDetalle == "") {
        swal.fire({
            title: "Atencion!",
            text: "No Deje El Detalle En Blanco.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }

    if (uUnidadMedida == "") {
        swal.fire({
            title: "Atencion!",
            text: "No Deje La Unidad De Medida En Blanco.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }

    if (uMedida == "") {
        swal.fire({
            title: "Atencion!",
            text: "No Deje La Unidad De Medida En Blanco.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }

    if (cCantidad == 0) {
        swal.fire({
            title: "Atencion!",
            text: "No Deje En Cero La Cantidad Solicitada Para La Partida Presupuestaria.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }

    if (cPrecio == 0) {
        swal.fire({
            title: "Atencion!",
            text: "No Deje En Cero El Precio Solicitado Para La Partida Presupuestaria.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }

    if (uSaldoImporte < 0) {
        swal.fire({
            title: "Atencion!",
            text: "Importe Anteproyecto es Menor a su Distribucion Mensual.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }

    if (uSaldoImporte != 0) {
        swal.fire({
            title: "Atencion!",
            text: "Importe Anteproyecto Tiene Saldo En Su Distribucion Mensual.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }

    let partida = {
        idPartida: uidPartida,
        codigoPartida: uCodigoPartidaModal,
        nombrePartida: uNombrePartidaModal,
        codigoActividad: uActividad,
        detalle: uDetalle,
        medida: uMedida,
        cantidad: parseFloat(uCantidad),
        precio: parseFloat(uPrecioUnitario),
        total: uTotal,
        observacion: uObservacion,
        mesEne: parseFloat(uMesEne),
        mesFeb: parseFloat(uMesFeb),
        mesMar: parseFloat(uMesMar),
        mesAbr: parseFloat(uMesAbr),
        mesMay: parseFloat(uMesMay),
        mesJun: parseFloat(uMesJun),
        mesJul: parseFloat(uMesJul),
        mesAgo: parseFloat(uMesAgo),
        mesSep: parseFloat(uMesSep),
        mesOct: parseFloat(uMesOct),
        mesNov: parseFloat(uMesNov),
        mesDic: parseFloat(uMesDic),
        idFila: rd
    }

    PartidasParaEdicion.push(partida)
    mostrarPartida_Precios()

    $("#cboBuscarPartida").val("").trigger("change")
    limpiarModal()
    reiniciarConstantes()
    $("#modalRegistroPartida").modal("hide")
})

function CalcularImporteTotal() {
    try {
        var CantidadModal = parseFloat(document.getElementById("txtCantidadModal").value) || 0;
        var PrecioModal = parseFloat(document.getElementById("txtPrecioModal").value) || 0;

        var TotalAnteproyectoModal = CantidadModal * PrecioModal;

        document.getElementById("txtImporteTotalModal").value = formateadorDecimal.format((TotalAnteproyectoModal).toFixed(2));
        document.getElementById("txtImporteSaldoModal").value = formateadorDecimal.format((TotalAnteproyectoModal).toFixed(2));

        document.getElementById("txtImporteTotalSinFormatoModal").value = TotalAnteproyectoModal.toFixed(2);
        document.getElementById("txtImporteSaldoSinFormatoModal").value = TotalAnteproyectoModal.toFixed(2);

        EstiloSaldoModal = document.getElementById("txtImporteSaldoModal");
        EstiloSaldoModal.style.border = 'solid blue 2px';
        CalcularImporteSaldo();
    }
    catch (e) {

    }
}

function CalcularImporteSaldo() {
    try {
        var EneroModal = parseFloat(document.getElementById("txtEneroModal").value) || 0;
        var FebreroModal = parseFloat(document.getElementById("txtFebreroModal").value) || 0;
        var MarzoModal = parseFloat(document.getElementById("txtMarzoModal").value) || 0;
        var AbrilModal = parseFloat(document.getElementById("txtAbrilModal").value) || 0;
        var MayoModal = parseFloat(document.getElementById("txtMayoModal").value) || 0;
        var JunioModal = parseFloat(document.getElementById("txtJunioModal").value) || 0;
        var JulioModal = parseFloat(document.getElementById("txtJulioModal").value) || 0;
        var AgostoModal = parseFloat(document.getElementById("txtAgostoModal").value) || 0;
        var SeptiembreModal = parseFloat(document.getElementById("txtSeptiembreModal").value) || 0;
        var OctubreModal = parseFloat(document.getElementById("txtOctubreModal").value) || 0;
        var NoviembreModal = parseFloat(document.getElementById("txtNoviembreModal").value) || 0;
        var DiciembreModal = parseFloat(document.getElementById("txtDiciembreModal").value) || 0;

        var TotalImporteModal = parseFloat(document.getElementById("txtImporteTotalSinFormatoModal").value).toFixed(2) || 0;

        SumaMeses = (EneroModal + FebreroModal + MarzoModal + AbrilModal + MayoModal + JunioModal + JulioModal + AgostoModal + SeptiembreModal + OctubreModal + NoviembreModal + DiciembreModal).toFixed(2);
        SaldoModal = TotalImporteModal - SumaMeses;

        document.getElementById("txtImporteSaldoModal").value = formateadorDecimal.format(SaldoModal.toFixed(2));
        document.getElementById("txtImporteSaldoSinFormatoModal").value = SaldoModal;
        EstiloSaldoModal = document.getElementById("txtImporteSaldoModal");
        if (SaldoModal < 0) {
            EstiloSaldoModal.style.border = 'solid red 2px';
        }
        else {
            EstiloSaldoModal.style.border = 'solid blue 2px';
        }
    }
    catch (e) {

    }
}
