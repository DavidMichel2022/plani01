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

$(document).ready(function () {
    $.ajax({
        url: '/Planificacion/ObtenerHora',
        type: 'GET',
        success: function (data) {
            $("#txtFechaRegistro").val(data);
        }
    });

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

    fetch("/Negocio/Obtener")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.estado) {
                const d = responseJson.objeto;
                $("#inputGroupTotal").text(`Total - ${d.simboloMoneda}`)
            }
        })

    $("#cboBuscarPartida").select2({
        ajax: {
            url: "/Planificacion/ObtenerPartidas",
            dataType: 'json',
            contentType:"/application/json; charset=utf-8",
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
        language:"es",
        placeholder: 'Buscar Partida Presupuestaria.....',
        minimumInputLength: 1,
        templateResult: formatoResultados,
    });
})

function formatoResultados(data)
{
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

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let PartidasParaPlanificacion = [];
$("#cboBuscarPartida").on("select2:select", function (e) {
    const data = e.params.data;

    let partida_encontrada = PartidasParaPlanificacion.filter(p => p.idPartida == data.id);

    let tituloMensajeCodigo = data.codigo.trim();
    let tituloMensajeNombre = data.text;

    $('#txtIdPartidaModal').val(data.id)
    $('#txtCodigoPartidaModal').val(data.codigo)
    $('#txtNombrePartidaModal').val(data.nombrePartida)

    $("#txtTituloMensaje").val(tituloMensajeCodigo + " - " + tituloMensajeNombre);

    $("#modalData").modal("show")
})

function mostrarPartida_Precios() {
    let total = 0;

    $("#tbPartida tbody").html("")
    PartidasParaPlanificacion.forEach((item) => {
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
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.cantidad)),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.precio)),
                $("<td class='text-right'>").text(item.total),
                $("<td>").text(item.temporalidad),
                $("<td>").text(item.observacion)
            )
        )
    })

    let ImportePlanificacion = formateadorDecimal.format(total)
    $("#txtTotal").val(ImportePlanificacion)
    $("#txtMontoPlanificacionE").val(ImportePlanificacion)
}

function mostrarPartida_Modal() {
    let total = 0;

    $("#tbPartida tbody").html("")
    PartidasParaPlanificacion.forEach((item) => {
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
                $("<td>").text(item.temporalidad),
                $("<td>").text(item.observacion),
            )
        )
    })

    let ImportePlanificacion = total
    $("#txtTotal").val(ImportePlanificacion)
}

$(document).on("click", "button.btn-eliminar", function () {
    const _idPartida = $(this).data("idFila")

    PartidasParaPlanificacion = PartidasParaPlanificacion.filter(p => p.idFila != _idPartida);
    mostrarPartida_Precios();
})

$("#btnCargar").click(function () {
    $("#modalData").modal("show");
})

$("#btnTerminarSolicitud").click(function () {
    let cadenacite = $("#txtCiteCarpeta").val().trim();
    let citePlanificacion = $("#txtCiteCarpeta").val();

    if (cadenacite == "") {
        swal.fire({
            title: "Atencion!",
            text: "No Deje El Nro. De Cite En Blanco",
            icon: "warning",
            allowOutsideClick: false,
            showConfirmButton: true,
        })
            .then(resultado => {
                swal.close();
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
            fetch(`/Planificacion/ObtenerPlanificaciones?citePlanificacion=${citePlanificacion}`).then(response => { return response.ok ? response.json() : Promise.reject(response); })
                .then(responseJson => {
                    if (responseJson.length > 0) {
                        swal.fire({
                            title: "Atencion!",
                            text: "Este Nro. De Planificacion Ya Existe.",
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
                                allowOutsideClick: true,
                                icon: "warning",
                                showConfirmButton: true,
                            },
                                function () {
                                    swal.close();
                                }
                            );
                        }
                        else {
                            const vmDetallePlanificacions = PartidasParaPlanificacion;
                            const Planificacion = {
                                citePlanificacion: $("#txtCiteCarpeta").val(),
                                lugar: $("#cboLugar").val(),
                                estadoPlanificacion: "INI",
                                nombreRegional: $("#cboUnidadRegional").val(),
                                nombreEjecutora: $("#cboUnidadEjecutora").val(),
                                idCentro: $("#cboCentro").val(),
                                idUnidadResponsable: $("#cboUnidadResponsable").val(),
                                montoPoa: $("#txtTotal").val(),
                                fechaPlanificacion: $("#txtFechaRegistro").val(),
                                DetallePlanificacions: vmDetallePlanificacion
                            }
                            $("#btnTerminarSolicitud").LoadingOverlay("show");
                            fetch("/Planificacion/RegistrarPlanificacion", {
                                method: "POST",
                                headers: { "Content-Type": "application/json; charset=utf-8" },
                                body: JSON.stringify(Planificacion)
                            })
                                .then(response => {
                                    $("#btnTerminarSolicitud").LoadingOverlay("hide");
                                    return response.ok ? response.json() : Promise.reject(response);
                                })
                                .then(responseJson => {

                                    if (responseJson.estado) {

                                        PartidasParaPlanificacion = [];
                                        mostrarPartida_Precios();
                                        swal.fire({
                                            title: "Registrado!",
                                            text: `N°. Cite : ${responseJson.objeto.citePlanificacion}`,
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
                                            text: "No Se Pudo Registrar La Carpeta De Planificacion",
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

    $('#txtActividadModal').val("")
    $('#txtDetalleModal').val("")
    $('#txtMedidaModal').val("")
    $('#txtCantidadModal').val("")
    $('#txtPrecioModal').val("")
    $('#txtTemporalidadModal').val("")
    $('#txtObservacionModal').val("")

    document.querySelector('#grupo__txtActividadModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtDetalleModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtMedidaModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtCantidadModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtPrecioModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtTemporalidadModal i').classList.remove('fa-check-circle')
    document.querySelector('#grupo__txtObservacionModal i').classList.remove('fa-check-circle')
}

$("#btnGuardarModal").click(function () {
    var uTotal = 0;

    $('#txtActividadModal').val() == "" ? cActividad = 0 : cActividad = parseFloat($('#txtActividadModal').val());
    $('#txtCantidadModal').val() == "" ? cCantidad = 0 : cCantidad = parseFloat($('#txtCantidadModal').val());
    $('#txtPrecioModal').val() == "" ? cPrecio = 0 : cPrecio = parseFloat($('#txtPrecioModal').val());

    var uIdPartidaModal = $('#txtIdPartidaModal').val();
    var uCodigoPartidaModal = $('#txtCodigoPartidaModal').val();
    var uNombrePartidaModal = $('#txtNombrePartidaModal').val();

    var uActividad = $('#txtActividadModal').val();
    var uDetalle = $('#txtDetalleModal').val();
    var uMedida = $('#txtMedidaModal').val();
    var uCantidad = $('#txtCantidadModal').val();
    var uPrecioUnitario = $('#txtPrecioModal').val();
    var uTemporalidad = $('#txtTemporalidadModal').val();
    var uObservacion = $('#txtObservacionModal').val();

    var uTotal = (parseFloat(uCantidad) * parseFloat(uPrecioUnitario));

    var rd = Math.floor(Math.random() * 99999);

    if ((cActividad <= 0) || (cActividad > 42) ) {
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

    if (uTemporalidad == "") {
        swal.fire({
            title: "Atencion!",
            text: "No Deje La Temporalidad En Blanco",
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
        cantidad: parseFloat(uCantidad),
        precio: parseFloat(uPrecioUnitario),
        temporalidad:uTemporalidad,
        total: uTotal,
        observacion: uObservacion,
        idFila: rd
    }

    PartidasParaPlanificacion.push(partida)

    mostrarPartida_Modal()

    $("#cboBuscarPartida").val("").trigger("change")

    limpiarModal()

    $("#modalData").modal("hide")
})