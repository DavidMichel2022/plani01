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
    idRequerimientoPoa: 0,
    estadoRequerimientoPoa: ""
}

const MODELO_BASEEDICION = {
    idRequerimientoPoa: 0,
    citeRequerimientoPoa: "",
    idCentro: 0,
    idUnidadResponsable: 0,
    idUsuario: 0,
    lugar: "",
    nombreRegional: "",
    nombreEjecutora: "",
    montoPoa: 0,
    estadoRequerimientoPoa: "",
    fechaRequerimientoPoa: "",
    nombreCentro: "",
    nombreUnidadResponsable: "",
    detalleRequerimientoPoas: "",
}

let filaSeleccionada;
let tablaData;
$(document).ready(function () {

    fetch("/RequerimientoPoa/ListaCentrosalud")
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

    fetch("/RequerimientoPoa/ListaUnidadResponsable")
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
            "url": `/RequerimientoPoa/ListaMisRequerimientosPoa`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //{ "data": "idRequerimientoPoa", "visible": false, "searchable": true },
            { "data": "fechaRequerimientoPoa" },
            { "data": "citeRequerimientoPoa" },
            { "data": "nombreCentro" },
            { "data": "nombreUnidadResponsable" },

            {
                "data": "montoPoa", render: function (data) {
                    return '<div class="text-right">' + formateadorDecimal.format(data) + '</div>';
                }
            },

            {
                "data": "estadoRequerimientoPoa", render: function (data) {
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
                filename: `Reporte Listado Mis Requerimientos Poa`,
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
            url: "/RequerimientoPoa/ObtenerPartidas",
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

    let ImporteRequerimiento = formateadorDecimal.format(data.montoPoa)

    $("#txtFechaRegistro").val(data.fechaRequerimientoPoa)
    $("#txtCiteRequerimientoPoa").val(data.citeRequerimientoPoa)
    $("#txtUnidadSolicitante").val(data.nombreCentro)
    $("#txtUnidadResponsable").val(data.nombreUnidadResponsable)
    $("#txtObservacion").val(data.estadoRequerimientoPoa)
    if (data.RequerimientoPoa == "INI") {
        $("#txtObservacion").val("INICIAL")
    }
    else {
        if (data.estadoRequerimientoPoa == "ANU") {
            $("#txtObservacion").val("ANULADO")
        }
        else {
            $("#txtObservacion").val("EN TRAMITE")
        }
    }
    $("#txtTotalRequerimiento").val(ImporteRequerimiento)

    //console.log(data.detalleRequerimientoPoas);

    $("#tbPartidas tbody").html("")
    cont = 0;
    data.detalleRequerimientoPoas.forEach((item) => {
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
                $("<td>").text(item.temporalidad),
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

    $("#linkImprimir").attr("href", `/RequerimientoPoa/MostrarPDFRequerimientoPoa?citeRequerimientoPoa=${data.citeRequerimientoPoa}`);
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

    let ImporteRequerimiento = formateadorDecimal.format(data.montoPoa)

    $("#txtId").val(data.idRequerimientoPoa)
    $("#txtIdCentro").val(data.idCentro)
    $("#txtIdUnidadResponsable").val(data.idUnidadResponsable)
    $("#txtIdUsuario").val(data.idUsuario)
    $("#txtLugar").val(data.lugar)
    $("#txtNombreRegional").val(data.nombreRegional)
    $("#txtNombreEjecutora").val(data.nombreEjecutora)
    $("#txtFechaRegistro").val(data.fechaRequerimientoPoa)
    $("#txtCiteRequerimiento").val(data.citeRequerimientoPoa)
    $("#txtUnidadSolicitante").val(data.nombreCentro)
    $("#txtUnidadResponsable").val(data.nombreUnidadResponsable)
    $("#txtEstadoRequerimiento").val(data.estadoRequerimientoPoa)
    $("#txtObservacion").val(data.estadoCarpeta)
    $("#txtTotalRequerimiento").val(ImporteRequerimiento)

    if (data.estadoRequerimientoPoa == "INI") {
        $("#txtObservacion").val("")
    }
    else {
        if (data.estadoRequerimientoPoa == "ANU") {
            $("#txtObservacion").val("ANULADO")
        }
        else {
            $("#txtObservacion").val("")
        }
    }

    if ($("#txtEstadoRequerimiento").val() == "ANU") {
        swal("Atencion", "Carpeta De Requerimiento Poa Ya Esta Anulada!", "warning");
        return;
    }

    swal({
        title: "Está Seguro de Anular?",
        text: '\n' + `Carpeta Planificacion: Cite N°. "${data.citeRequerimientoPoa}"` + "\n" +
            `Fecha Registro: "${data.fechaRequerimientoPoa}"` + "\n" +
            `Importe Total: "${data.montoPoa}"` + "\n" + "\n" +
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

                modelo["idRequerimientoPoa"] = parseInt($("#txtId").val())
                modelo["estadoRequerimientoPoa"] = "ANU"

                fetch("/RequerimientoPoa/Anular", {
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

                            swal("Listo!", "La Carpeta De Requerimiento Poa Fue Anulada", "success")
                            let nueva_url = `/RequerimientoPoa/ListaMisRequerimientosPoa`;
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

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }
    const data = tablaData.row(filaSeleccionada).data();

    let ImporteRequerimiento = formateadorDecimal.format(data.montoPoa)

    $("#txtIdE").val(data.idRequerimientoPoa)

    $("#txtCiteRequerimientoE").val(data.citeRequerimientoPoa)
    $("#txtIdCentroE").val(data.idCentro)
    $("#txtIdUnidadResponsableE").val(data.idUnidadResponsable)
    $("#txtIdUsuarioE").val(data.idUsuario)
    $("#txtLugarE").val(data.lugar)
    $("#txtNombreRegionalE").val(data.nombreRegional)
    $("#txtNombreEjecutoraE").val(data.nombreEjecutora)
    $("#txtTotalRequerimientoE").val(ImporteRequerimiento)
    $("#txtEstadoRequerimientoE").val(data.estadoRequerimientoPoa)
    $("#txtFechaRegistroE").val(data.fechaRequerimientoPoa)

    $("#txtUnidadSolicitanteE").val(data.nombreCentro)
    $("#txtUnidadResponsableE").val(data.nombreUnidadResponsable)

    $("#cboCentro").val(data.idCentro == 0 ? $("#cboCentro option:first").val() : data.idCentro)
    $("#cboUnidadResponsable").val(data.idUnidadResponsable == 0 ? $("#cboUnidadResponsable option:first").val() : data.idUnidadResponsable)

    if ($("#txtEstadoRequerimientoE").val() == "INI") {
        $("#txtObservacionE").val("INICIAL")
    }
    else {
        if ($("#txtEstadoRequerimientoE").val() == "ANU") {
            $("#txtObservacionE").val("ANULADO")
        }
        else {
            $("#txtObservacionE").val("EN TRAMITE")
        }
    }

    $("#txtTotalRequerimientoE").val(data.montoPoa)

    $("#tbPartidaEdicion tbody").html("")

    if ($("#txtObservacionE").val() == "ANULADO") {
        swal("Atencion", "Carpeta De Requerimiento Poa Anulada!", "warning");
        return;
    }


    CargarDetallePartidas(data.detalleRequerimientoPoas);

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
        customClass: 'swal-wide',
        text: '<hr><div class="form-row"><label for="txtSwalCodigoActividad">Codigo Actividad:   </label><input type="number" autocomplete="off" class="form-control col-sm-1" id="txtSwalCodigoActividad">' +
            '<label for="txtSwalDetalle">         Detalle Requerimiento:   </label><textarea type="text" class="form-control col-sm-6" rows="3" id="txtSwalDetalle"></textarea></div>' +
            '<div autocomplete="off" class="form-row" style="margin-top:10px;"><label for= "txtSwalUnidadMedida" > Unidad De Medida:   </label> <input type="text" autocomplete="off" maxlength="10" class="form-control col-sm-2" id="txtSwalUnidadMedida">' +
            '<label for="txtSwalCantidad">           Cantidad:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalCantidad">' +
            '<label for="txtSwalPrecioUnitario">            Precio Unitario:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalPrecioUnitario"></div>' +
            '<div autocomplete="off" class="form-row" style="margin-top:10px;"><label for="txtSwalTemporalidad">        Temporalidad:   </label><input type="text" maxlength="20" autocomplete="off" class="form-control col-sm-2" id="txtSwalTemporalidad">' +
            '<label for="txtSwalObservacion">    Observacion:   </label><textarea type="text" class="form-control col-sm-6" rows="3" id="txtSwalObservacion"></textarea></div>' +
            '<hr><div autocomplete="off" class="form-row" style="margin-top:10px;"><label for= "txtSwalEnero" > Enero:   </label> <input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalEnero">' +
            '<label for="txtSwalFebrero">       Febrero:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalFebrero">' +
            '<label for="txtSwalMarzo">     Marzo:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalMarzo">' +
            '<label for="txtSwalAbril">           Abril:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalAbril"></div>' +
            '<div class="form-row" style="margin-top:10px;"><label for= "txtSwalMayo" > Mayo:   </label> <input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalMayo">' +
            '<label for="txtSwalJunio">                Junio:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalJunio">' +
            '<label for="txtSwalJulio">          Julio:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalJulio">' +
            '<label for="txtSwalAgosto">   Agosto:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalAgosto"></div>' +
            '<div autocomplete="off" class="form-row" style="margin-top:10px;"><label for= "txtSwalSeptiembre" > Septiembre:   </label> <input type="number" autocomplete="off" value="0.00" max="99999999.99" min="0" class="form-control col-sm-2" id="txtSwalSeptiembre">' +
            '<label for="txtSwalOctubre">           Octubre:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalOctubre"></div>' +
            '<div autocomplete="off" class="form-row" style="margin-top:10px;"><label for= "txtSwalNoviembre" > Noviembre:   </label> <input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalNoviembre">' +
            '<label for="txtSwalDiciembre">     Diciembre:   </label><input type="number" autocomplete="off" class="form-control col-sm-2" id="txtSwalDiciembre"></div><hr>',
        showCancelButton: true,
        closeOnConfirm: false,
    },
        function (e) {

            if (e === false) return false;

            var uActividad = $('#txtSwalCodigoActividad').val();
            var uDetalle = $('#txtSwalDetalle').val();
            var uMedida = $('#txtSwalUnidadMedida').val();
            var uCantidad = $('#txtSwalCantidad').val();
            var uPrecioUnitario = $('#txtSwalPrecioUnitario').val();
            var uTemporalidad = $('#txtSwalTemporalidad').val();
            var uObservacion = $('#txtSwalObservacion').val();
            var uMesEne = $('#txtSwalEnero').val();
            var uMesFeb = $('#txtSwalFebrero').val();
            var uMesMar = $('#txtSwalMarzo').val();
            var uMesAbr = $('#txtSwalAbril').val();
            var uMesMay = $('#txtSwalMayo').val();
            var uMesJun = $('#txtSwalJunio').val();
            var uMesJul = $('#txtSwalJulio').val();
            var uMesAgo = $('#txtSwalAgosto').val();
            var uMesSep = $('#txtSwalSeptiembre').val();
            var uMesOct = $('#txtSwalOctubre').val();
            var uMesNov = $('#txtSwalNoviembre').val();
            var uMesDic = $('#txtSwalDiciembre').val();
            var uTotal = (parseInt(uCantidad) * parseFloat(uPrecioUnitario));

            var rd = Math.floor(Math.random() * 99999);

            let partida = {
                idPartida: data.id,
                nombrePartida: data.text,
                codigoActividad: uActividad,
                detalle: uDetalle,
                codigoPartida: data.codigo,
                medida: uMedida,
                cantidad: parseFloat(uCantidad),
                precio: parseFloat(uPrecioUnitario),
                total: parseFloat(parseInt(uCantidad) * parseFloat(uPrecioUnitario)),
                temporalidad: uTemporalidad,
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
                $("<td>").text(item.codigoActividad),
                $("<td>").text(item.codigoPartida),
                $("<td>").text(item.detalle),
                $("<td>").text(item.medida),
                $("<td>").text(formateadorDecimal.format(item.cantidad)),
                $("<td>").text(formateadorDecimal.format(item.precio)),
                $("<td>").text(formateadorDecimal.format(item.total)),
                $("<td>").text(item.temporalidad),
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

    $("#txtTotalRequerimiento").val(ImporteRequerimiento)
    $("#txtTotalRequerimientoE").val(ImporteRequerimiento)
    $("#txtMontoRequerimientoE").val(total)
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
            temporalidad: item.temporalidad,
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

    modelo["idRequerimientoPoa"] = parseInt($("#txtIdE").val())
    modelo["citeRequerimientoPoa"] = $("#txtCiteRequerimientoE").val()
    modelo["idCentro"] = $("#cboCentro").val()
    modelo["idUnidadResponsable"] = $("#cboUnidadResponsable").val()
    modelo["lugar"] = $("#txtLugarE").val()
    modelo["nombreRegional"] = $("#txtNombreRegionalE").val()
    modelo["nombreEjecutora"] = $("#txtNombreEjecutoraE").val()
    modelo["montoPoa"] = parseFloat($("#txtMontoRequerimientoE").val())
    modelo["estadoRequerimientoPoa"] = $("#txtEstadoRequerimientoE").val()
    modelo["fechaRequerimientoPoa"] = $("#txtFechaRegistroE").val()

    modelo["detalleRequerimientoPoas"] = PartidasParaEdicion

    const data = tablaData.row(filaSeleccionada).data();


    IdRequerimientoPoa = modelo["idRequerimientoPoa"];

    fetch("/RequerimientoPoa/Editar", {
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
        text: "La Carpeta De Requerimiento Poa Fue Modificada",
        icon: "success",
        showConfirmButton: true,
    },
        function (e) {
            $("#modalDataEdicion").modal("hide");
            let nueva_url = `/RequerimientoPoa/ListaMisRequerimientosPoa`;
            tablaData.ajax.url(nueva_url).load();
            location.reload(true);
        }
    );
})