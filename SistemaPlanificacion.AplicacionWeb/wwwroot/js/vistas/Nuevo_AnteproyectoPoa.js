let formateadorDecimal = new Intl.NumberFormat('en-US', {
    maximumFractionDigits: 2,
    minimumFractionDigits: 2
});

let formateadorEntero = new Intl.NumberFormat('en-US', {
    maximumFractionDigits: 2,
    minimumFractionDigits: 0
});

$(document).ready(function () {
    $.ajax({
        url: '/AnteproyectoPoa/ObtenerHora',
        type: 'GET',
        success: function (data) {
            $("#txtFechaRegistro").val(data);
        }
    });

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
    fetch("/Negocio/Obtener").then(response => { return response.ok ? response.json() : Promise.reject(response); })
        .then(responseJson => {
            if (responseJson.estado) {
                const d = responseJson.objeto;
                $("#inputGroupTotal").text(`Total - ${d.simboloMoneda}`)
            }
        })

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
        templateResult: formatoResultados,
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

function formatoResultados(data) {
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
    console.log(data);
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

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let PartidasParaAnteproyectoPoa = [];
let partida_encontrada;

$("#cboBuscarPartida").on("select2:select", function (e) {
    const data = e.params.data;

    let partida_encontrada = PartidasParaAnteproyectoPoa.filter(p => p.idPartida == data.id);

    let tituloMensajeCodigo = data.codigo.trim();
    let tituloMensajeNombre = data.text;

    $('#txtIdPartidaModal').val(data.id)
    $('#txtCodigoPartidaModal').val(data.codigo)
    $('#txtNombrePartidaModal').val(data.nombrePartida)

    $("#txtTituloMensaje").val(tituloMensajeCodigo + " - " + tituloMensajeNombre);

    $("#modalData").modal("show")
})

$("#cboBuscarUnidad").on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let PartidasParaAnteproyectoPoaUnidad = [];
let partida_encontradaUnidad;
$("#cboBuscarUnidad").on("select2:select", function (e) {
    const data = e.params.data;

    let partida_encontradaUnidad = PartidasParaAnteproyectoPoaUnidad.filter(p => p.idUnidad == data.id);

    $("#modalData").modal("show")
})

function mostrarPartida_Precios() {
    let total = 0;

    $("#tbPartida tbody").html("")
    PartidasParaAnteproyectoPoa.forEach((item) => {
        total = total + parseFloat(item.total)

        $("#tbPartida tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<I>").addClass("fas fa-trash-alt")
                    ).data("idFila", item.idFila)
                ),
                $("<td class='text-center'>").text(item.codigoActividad),
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

    let ImporteAnteproyecto = total;
    document.getElementById("txtTotal").value = formateadorDecimal.format(ImporteAnteproyecto.toFixed(2))

    document.getElementById("txtImporteTotalAnteproyectoGeneral").value = ImporteAnteproyecto.toFixed(2)
}

function mostrarPartida_Modal() {
    let total = 0;

    $("#tbPartida tbody").html("")
    PartidasParaAnteproyectoPoa.forEach((item) => {
        total = total + parseFloat(item.total)

        $("#tbPartida tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<I>").addClass("fas fa-trash-alt")
                    ).data("idFila", item.idFila)
                ),
                $("<td class='text-center'>").text(item.codigoActividad),
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

    let ImporteAnteproyecto = total
    document.getElementById("txtTotal").value = formateadorDecimal.format(ImporteAnteproyecto.toFixed(2))
    document.getElementById("txtImporteTotalAnteproyectoGeneral").value = ImporteAnteproyecto.toFixed(2)
}

$(document).on("click", "button.btn-eliminar", function () {
    const _idPartida = $(this).data("idFila")

    PartidasParaAnteproyectoPoa = PartidasParaAnteproyectoPoa.filter(p => p.idFila != _idPartida);
    mostrarPartida_Precios();
})

$("#btnCargar").click(function () {
    $("#modalData").modal("show");
})

$("#btnTerminarSolicitud").click(function () {
    let cadenacite = $("#txtCiteCarpeta").val().trim();
    let citeAnteproyecto = $("#txtCiteCarpeta").val();

    if (cadenacite == "") {
        swal.fire({
            title: "Atencion!",
            text: "No Deje El Nro. De Cite En Blanco",
            icon: "warning",
            allowOutsideClick: false,
            showConfirmButton: true,
        })
            .then(resultado => {
                swal.hide();
                document.getElementById(`grupo__txtCiteCarpeta`).classList.remove('formulario__grupo-incorrecto');
                document.getElementById(`grupo__txtCiteCarpeta`).classList.remove('formulario__grupo-correcto');
                document.querySelector('#grupo__txtCiteCarpeta i').classList.remove('fa-check-circle')
                document.querySelector('#grupo__txtCiteCarpeta i').classList.remove('fa-times-circle')
                document.querySelector(`#grupo__txtCiteCarpeta .formulario__input-error`).classList.remove('formulario__input-error-activo');
                $("#txtCiteCarpeta").val("");
                didClose: () => { $("#txtCiteCarpeta").focus(); }
            })
    }
    else {
        if (cadenacite.length <= 3) {
            swal.fire({
                title: "Atencion!",
                text: "Debe Tener Al Menos 4 Caracteres El Nro. De Cite",
                allowOutsideClick: false,
                icon: "warning",
                showConfirmButton: true,
            })
                .then(resultado => {
                    document.getElementById(`grupo__txtCiteCarpeta`).classList.remove('formulario__grupo-incorrecto');
                    document.getElementById(`grupo__txtCiteCarpeta`).classList.remove('formulario__grupo-correcto');
                    document.querySelector('#grupo__txtCiteCarpeta i').classList.remove('fa-check-circle')
                    document.querySelector('#grupo__txtCiteCarpeta i').classList.remove('fa-times-circle')
                    document.querySelector(`#grupo__txtCiteCarpeta .formulario__input-error`).classList.remove('formulario__input-error-activo');
                    $("#txtCiteCarpeta").focus();
                    swal.close();

                })
        }
        else {
            fetch(`/AnteproyectoPoa/ObtenerAnteproyectos?citeAnteproyecto=${citeAnteproyecto}`).then(response => { return response.ok ? response.json() : Promise.reject(response); })
                .then(responseJson => {
                    if (responseJson.length > 0) {
                        swal.fire({
                            title: "Atencion!",
                            text: "Este Nro. De Anteproyecto Poa Ya Existe.",
                            icon: "warning",
                            allowOutsideClick: false,
                            showConfirmButton: true,
                        })
                            .then(resultado => {
                                swal.close();
                            })
                    }
                    else {
                        if ($("#txtTotal").val() == 0) {
                            swal.fire({
                                title: "Atencion!",
                                text: "Debe Tener Cargado Al Menos Un Registro de Partidas Presupuestarias",
                                allowOutsideClick: false,
                                icon: "warning",
                                showConfirmButton: true,
                            },
                                function () {
                                    swal.close();
                                }
                            );
                        }
                        else {
                            const vmDetalleAnteproyectoPoa = PartidasParaAnteproyectoPoa;
                            const anteproyectopoa = {
                                citeAnteproyecto: $("#txtCiteCarpeta").val(),
                                lugar: $("#cboLugar").val(),
                                estadoAnteproyecto: "INI",
                                nombreRegional: $("#cboUnidadRegional").val(),
                                nombreEjecutora: $("#cboUnidadEjecutora").val(),
                                idCentro: $("#cboCentro").val(),
                                idUnidadResponsable: $("#cboUnidadResponsable").val(),
                                montoAnteproyecto: $("#txtImporteTotalAnteproyectoGeneral").val(),
                                fechaAnteproyecto: $("#txtFechaRegistro").val(),
                                DetalleAnteproyectoPoas: vmDetalleAnteproyectoPoa
                            }
                            $("#btnTerminarSolicitud").LoadingOverlay("show");
                            fetch("/AnteproyectoPoa/RegistrarAnteproyectoPoa", {
                                method: "POST",
                                headers: { "Content-Type": "application/json; charset=utf-8" },
                                body: JSON.stringify(anteproyectopoa)
                            })
                                .then(response => {
                                    $("#btnTerminarSolicitud").LoadingOverlay("hide");
                                    return response.ok ? response.json() : Promise.reject(response);
                                })
                                .then(responseJson => {

                                    if (responseJson.estado) {

                                        PartidasParaAnteproyectoPoa = [];
                                        mostrarPartida_Precios();
                                        swal.fire({
                                            title: "Registrado!",
                                            text: `N°. Cite : ${responseJson.objeto.citeAnteproyecto}`,
                                            icon: "success",
                                            showConfirmButton: true,
                                        },
                                            function () {
                                                document.getElementById(`grupo__txtCiteCarpeta`).classList.remove('formulario__grupo-incorrecto');
                                                document.querySelector('#grupo__txtCiteCarpeta i').classList.remove('fa-check-circle')
                                                $("#txtCiteCarpeta").focus();
                                                swal.close();
                                            }
                                        );
                                        document.getElementById(`grupo__txtCiteCarpeta`).classList.remove('formulario__grupo-incorrecto');
                                        document.getElementById(`grupo__txtCiteCarpeta`).classList.remove('formulario__grupo-correcto');
                                        document.querySelector('#grupo__txtCiteCarpeta i').classList.remove('fa-check-circle')
                                        document.querySelector('#grupo__txtCiteCarpeta i').classList.remove('fa-times-circle')
                                        document.querySelector(`#grupo__txtCiteCarpeta .formulario__input-error`).classList.remove('formulario__input-error-activo');
                                        $("#txtCiteCarpeta").val("")
                                        $("#cboCentro").val($("#cboCentro option:first").val())
                                        $("#cboUnidadResponsable").val($("#cboUnidadResponsable option:first").val())
                                        $("#cboDocumento").val($("#cboDocumento option:first").val())
                                        $("#txtCiteCarpeta").focus();
                                    }
                                    else {
                                        swal.fire({
                                            title: "Lo Sentimos!",
                                            text: "No Se Pudo Registrar La Carpeta De Anteproyecto Poa",
                                            icon: "error",
                                            showConfirmButton: true,
                                        }),
                                            function () {
                                                swal.close();
                                            }
                                    }
                                });
                        }
                    }
                });
        }
    }
});

function mostrarModal() {
    $("#modalData").modal("show")
}

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

$("#btnGuardarModal").click(function () {
    var uTotal = 0;

    $('#txtActividadModal').val() == "" ? cActividad = 0 : cActividad = parseFloat($('#txtActividadModal').val());
    $('#txtCantidadModal').val() == "" ? cCantidad = 0 : cCantidad = parseFloat($('#txtCantidadModal').val());
    $('#txtPrecioModal').val() == "" ? cPrecio = 0 : cPrecio = parseFloat($('#txtPrecioModal').val());

    var codigoUnidad = document.getElementById("cboBuscarUnidad").value;
    var combo = document.getElementById("cboBuscarUnidad");
    var selected = combo.options[combo.selectedIndex].text;

    var uUnidadMedida = selected;

    var uIdPartidaModal = $('#txtIdPartidaModal').val();
    var uCodigoPartidaModal = $('#txtCodigoPartidaModal').val();
    var uNombrePartidaModal = $('#txtNombrePartidaModal').val();

    var uActividad = $('#txtActividadModal').val();
    var uDetalle = $('#txtDetalleModal').val();
    var uMedida = uUnidadMedida;
    var uCantidad = $('#txtCantidadModal').val();
    var uPrecioUnitario = $('#txtPrecioModal').val();
    var uObservacion = $('#txtObservacionModal').val();
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
        idPartida: uIdPartidaModal,
        codigoPartida: uCodigoPartidaModal,
        nombrePartida: uNombrePartidaModal,
        codigoActividad: uActividad,
        detalle: uDetalle,
        medida: uMedida,
        idUnidad:codigoUnidad,
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

    PartidasParaAnteproyectoPoa.push(partida)

    mostrarPartida_Modal()

    $("#cboBuscarPartida").val("").trigger("change")

    limpiarModal()

    $("#modalData").modal("hide")
})
function CalcularImporteTotal()
{
    try
    {
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
    catch(e)
    {

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

