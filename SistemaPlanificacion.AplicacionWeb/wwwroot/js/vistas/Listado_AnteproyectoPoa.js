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
    montoPoa: 0,
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

    //$("#txtCiteAnteproyecto").focus();

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
    $("#txtTotalAnteproyecto").val(ImporteAnteproyecto)

    $("#tbPartidas tbody").html("")
    cont = 0;
    //console.log(data);
    //console.log(data.detalleAnteproyectoPoas);

    data.detalleAnteproyectoPoas.forEach((item) => {
        cont++;
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
        swal("Atencion", "Carpeta De Anteproyecto Poa Ya Esta Anulada!", "warning");
        return;
    }

    swal({
        title: "Está Seguro de Anular?",
        text: '\n' + `Carpeta Anteproyecto: Cite N°. "${data.citeAnteproyecto}"` + "\n" +
            `Fecha Registro: "${data.fechaAnteproyecto}"` + "\n" +
            `Importe Total: "${data.montoAnteproyecto}"` + "\n" + "\n" +
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

                            swal("Listo!", "La Carpeta De Anteproyecto Poa Fue Anulada", "success")
                            let nueva_url = `/AnteproyectoPoa/ListaMisAnteproyectosPoa`;
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

$("#tbdata tbody").on("click", ".btn-editar", function (e) {
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
    $("#txtTotalAnteproyecto").val(ImporteAnteproyecto)

    $("#txtIdCentroE").val(data.idCentro)
    $("#txtIdUsuarioE").val(data.idUsuario)
    $("#txtLugarE").val(data.lugar)
    $("#txtNombreRegionalE").val(data.nombreRegional)
    $("#txtNombreEjecutoraE").val(data.nombreEjecutora)
    $("#txtTotalAnteproyectoE").val(ImporteAnteproyecto)
    $("#txtEstadoAnteproyectoE").val(data.estadoAnteproyecto)

    $("#cboCentro").val(data.idCentro == 0 ? $("#cboCentro option:first").val() : data.idCentro)
    $("#cboUnidadResponsable").val(data.idUnidadResponsable == 0 ? $("#cboUnidadResponsable option:first").val() : data.idUnidadResponsable)

    $("#tbPartidaEdicion tbody").html("")

    if ($("#txtObservacionE").val() == "ANULADO") {
        swal.fire("Atencion", "Carpeta De Anteproyecto Poa Anulada!", "warning");
        return;
    }

    CargarDetallePartidas(data.detalleAnteproyectoPoas);

    //console.log(data.detalleAnteproyectoPoas);

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

    let ImporteRequerimiento = formateadorDecimal.format(total)

    $("#txtTotalAnteproyecto").val(ImporteAnteproyecto)
    $("#txtTotalAnteproyectoE").val(ImporteAnteproyecto)
    $("#txtMontoAnteproyectoE").val(total)
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

$("#btnGuardar").click(function (e) {

    const modelo = structuredClone(MODELO_BASEEDICION);

    modelo["idAnteproyecto"] = parseInt($("#txtIdE").val())
    modelo["citeAnteproyecto"] = $("#txtCiteAnteproyectoE").val()
    modelo["idCentro"] = $("#cboCentro").val()
    modelo["idUnidadResponsable"] = $("#cboUnidadResponsable").val()
    modelo["lugar"] = $("#txtLugarE").val()
    modelo["nombreRegional"] = $("#txtNombreRegionalE").val()
    modelo["nombreEjecutora"] = $("#txtNombreEjecutoraE").val()
    modelo["montoAnteproyecto"] = parseFloat($("#txtMontoAnteproyectoE").val())
    modelo["estadoAnteproyecto"] = $("#txtEstadoAnteproyectoE").val()
    modelo["fechaAnteproyecto"] = $("#txtFechaRegistroE").val()

    modelo["detalleAnteproyectoPoas"] = PartidasParaEdicion

    const data = tablaData.row(filaSeleccionada).data();


    IdAnteproyecto = modelo["idAnteproyecto"];

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
            }
            else {
                swal("Lo Sentimos", responseJson.mensaje, "error")
            }
        })

    swal({
        title: "Listo!",
        text: "La Carpeta De Anteproyecto Poa Fue Modificada",
        icon: "success",
        showConfirmButton: true,
    },
        function (e) {
            $("#modalDataEdicion").modal("hide");
            let nueva_url = `/AnteproyectoPoa/ListaMisAnteproyectosPoa`;
            tablaData.ajax.url(nueva_url).load();
            location.reload(true);
        }
    );
})

function limpiarModal() {
    $('#txtIdPartidaModal').val("")
    $('#txtCodigoPartidaModal').val("")
    $('#txtNombrePartidaModal').val("")

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

    document.querySelector('#grupo__txtActividadModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtDetalleModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtMedidaModal i').classList.remove('fa-check-circle')
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
    campos.txtMedidaModal = false;
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

    var uActividad = $('#txtActividadModal').val();
    var uDetalle = $('#txtDetalleModal').val();
    var uMedida = $('#txtMedidaModal').val();
    var uCantidad = $('#txtCantidadModal').val();
    var uPrecioUnitario = $('#txtPrecioModal').val();
    var uObservacion = $('#txtObservacionModal').val();
    var uMesEne = $('#txtEneroModal').val();
    var uMesFeb = $('#txtFebreroModal').val();
    var uMesMar = $('#txtMarzoModal').val();
    var uMesAbr = $('#txtAbrilModal').val();
    var uMesMay = $('#txtMayoModal').val();
    var uMesJun = $('#txtJunioModal').val();
    var uMesJul = $('#txtJulioModal').val();
    var uMesAgo = $('#txtAgostoModal').val();
    var uMesSep = $('#txtSeptiembreModal').val();
    var uMesOct = $('#txtOctubreModal').val();
    var uMesNov = $('#txtNoviembreModal').val();
    var uMesDic = $('#txtDiciembreModal').val();
    var uTotal = (parseFloat(uCantidad) * parseFloat(uPrecioUnitario));

    var rd = Math.floor(Math.random() * 99999);

    if (!campos.txtActividadModal) {
        swal.fire({
            title: "Atencion!",
            text: "Codigo De Actividad Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if (!campos.txtMedidaModal) {
        swal.fire({
            title: "Atencion!",
            text: "Unidad De Medida Incorrecta!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if (!campos.txtCantidadModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor De La Cantidad Incorrecta!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if (!campos.txtPrecioModal) {
        swal.fire({
            title: "Atencion!",
            text: "Precio Unitario Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtEneroModal').val() == "") { campos.txtEneroModal = true; }
    if (!campos.txtEneroModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Enero Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtFebreroModal').val() == "") { campos.txtFebreroModal = true; }
    if (!campos.txtFebreroModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Febrero Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtMarzoModal').val() == "") { campos.txtMarzoModal = true; }
    if (!campos.txtMarzoModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Marzo Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtAbrilModal').val() == "") { campos.txtAbrilModal = true; }
    if (!campos.txtAbrilModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Abril Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtMayoModal').val() == "") { campos.txtMayoModal = true; }
    if (!campos.txtMayoModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Mayo Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtJunioModal').val() == "") { campos.txtJunioModal = true; }
    if (!campos.txtJunioModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Junio Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtJulioModal').val() == "") { campos.txtJulioModal = true; }
    if (!campos.txtJulioModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Julio Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtAgostoModal').val() == "") { campos.txtAgostoModal = true; }
    if (!campos.txtAgostoModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Agosto Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtSeptiembreModal').val() == "") { campos.txtSeptiembreModal = true; }
    if (!campos.txtSeptiembreModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Septiembre Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtOctubreModal').val() == "") { campos.txtOctubreModal = true; }
    if (!campos.txtOctubreModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Octubre Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtNoviembreModal').val() == "") { campos.txtNoviembreModal = true; }
    if (!campos.txtNoviembreModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Noviembre Incorrecto!.",
            allowOutsideClick: false,
            icon: "error",
            showConfirmButton: true,
        }),
            function () {
                swal.close();
            }
        return false;
    }
    if ($('#txtDiciembreModal').val() == "") { campos.txtDiciembreModal = true; }
    if (!campos.txtDiciembreModal) {
        swal.fire({
            title: "Atencion!",
            text: "Valor Del Mes Diciembre Incorrecto!.",
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
        idPartida: data.id,
        nombrePartida: data.text,
        codigoActividad: uActividad,
        detalle: uDetalle,
        codigoPartida: data.codigo,
        medida: uMedida,
        cantidad: parseFloat(uCantidad),
        precio: parseFloat(uPrecioUnitario),
        total: parseFloat(uCantidad * uPrecioUnitario),
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