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
            url: "/RequerimientoPoa/ObtenerRequerimientosPoaMiUnidad",
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
                            id: item.idDetalleRequerimientoPoa,
                            idRequerimientoPoa: item.idRequerimientoPoa,
                            idUnidadResponsable: item.idUnidadResponsable,
                            idDetalleRequerimientoPoa: item.idDetalleRequerimientoPoa,
                            codigo: item.codigoPartida,
                            detalle: item.detalle,
                            medida: item.medida,
                            cantidad: item.cantidad,
                            precio: parseFloat(item.precio),
                            total: parseFloat(item.total),
                            observacion: item.observacion,
                            mesEne: item.mesEne,
                            mesFeb: item.mesFeb,
                            mesMar: item.mesMar,
                            mesAbr: item.mesAbr,
                            mesMay: item.mesMay,
                            mesJun: item.mesJun,
                            mesJul: item.mesJul,
                            mesAgo: item.mesAgo,
                            mesSep: item.mesSep,
                            mesOct: item.mesOct,
                            mesNov: item.mesNov,
                            mesDic: item.mesDic
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

function formatoResultados(data) {
    if (data.loading)
        return data.detalle;
    var contenedor = $(

        `<table width="100%">
            <tr>
                <td>
                    <p style="font-weight: bolder; margin:2px">${data.codigo}</p>
                    <p style="margin:2px">${data.detalle}</p>
                </td>
            </tr>
        </table>`
    );
    //console.log("---Desplegando Resultados..................");
    return contenedor;
}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let PartidasParaPlanificacion = [];
$("#cboBuscarPartida").on("select2:select", function (e) {
    const data = e.params.data;
    let partida_encontrada = PartidasParaPlanificacion.filter(p => p.idPartida == data.id);
    let idDetalleRequerimientoPoa = data.idDetalleRequerimientoPoa;



    console.log("Seleccionando Datos Data=");
    console.log(data);
    console.log("Partida encontrada=");
    console.log(partida_encontrada);
    if (partida_encontrada.length == 0) {
        let tituloMensajeCodigo = data.codigo.trim();
        let tituloMensajeNombre = data.detalle;
        var rd = Math.floor(Math.random() * 99999);
        var temporalidad = "";
        if (data.mesEne != 0)
            temporalidad = temporalidad + " Enero,";
        if (data.mesFeb != 0)
            temporalidad = temporalidad + " Febrero,";
        if (data.mesMar != 0)
            temporalidad = temporalidad + " Marzo,";
        if (data.mesAbr != 0)
            temporalidad = temporalidad + " Abril,";
        if (data.mesMay != 0)
            temporalidad = temporalidad + " Mayo,";
        if (data.mesJun != 0)
            temporalidad = temporalidad + " Junio,";
        if (data.mesJul != 0)
            temporalidad = temporalidad + " Julio,";
        if (data.mesAgo != 0)
            temporalidad = temporalidad + " Agosto,";
        if (data.mesSep != 0)
            temporalidad = temporalidad + " Septiembre,";
        if (data.mesOct != 0)
            temporalidad = temporalidad + " Octubre,";
        if (data.mesNov != 0)
            temporalidad = temporalidad + " Noviembre,";
        if (data.mesDic != 0)
            temporalidad = temporalidad + " Diciembre,";
        let partida = {
            idDetalleRequerimientoPoa: data.idDetalleRequerimientoPoa,
            idEstado: data.estado,
            codigo: data.codigo,
            id: data.id,
            detalle: data.detalle,
            medida: data.medida,
            cantidad: data.cantidad,
            precio: data.precio,
            total: data.total,
            observacion: data.observacion,
            temporalidad: temporalidad,
            idFila: rd
            
            //IdDetallePlanificacion
            //IdPlanificacion
            //IdPartida
            //NombrePartida
            //ProgramaPartida
            //NombreItem
            
            //CodigoActividad 
            //Temporalidad 
            //observacion 
             
        }
        PartidasParaPlanificacion.push(partida)
        mostrarPartida_Precios();
    } else {
        swal.fire({
            title: "Solicitud de Modificacion!",
            text: "La partida a modificar, se encuentra agregada",
            icon: "warning",
            showConfirmButton: true,
        },
            function () {
                swal.close();
            }
        );
    }


  


})

function mostrarPartida_Precios() {
    let total = 0;
    var contador = 0;
    var size = PartidasParaPlanificacion.length;

    console.log("--Mostrar info--");
    console.log(PartidasParaPlanificacion);

    $("#tbPartida tbody").html("")
    PartidasParaPlanificacion.forEach((item) => {
        contador++;
        console.log("--->" + contador);
        total = total + parseFloat(item.total)

        $("#tbPartida tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<I>").addClass("fas fa-trash-alt")
                    ).data("idFila", item.idFila)
                ),
                $("<td>").text(item.id),
                $("<td>").text(item.codigo),
                $("<td>").text(item.detalle),
                $("<td>").text(item.medida),
                $("<td>").text(item.cantidad),
                $("<td>").text(formateadorDecimal.format(item.precio)),
                $("<td>").text(formateadorDecimal.format(item.total)),
                $("<td>").text(item.temporalidad),
                $("<td>").text(item.observacion)
            )
        )
        /*if (contador == size) {
            $("#tbPartida tbody").append(
                $('<tr style:"background-color:white" >').append(
                    $("<td colspan='7' class='text-right'>").text("Total"),
                    $("<td>").text(total),
                ))
        }*/

    })

    let ImportePlanificacion = formateadorDecimal.format(total)
    $("#txtTotal").val(total) //ImportePlanificacion
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
                        else {  //---

                            const vmDetallePlanificacion = PartidasParaPlanificacion;
                            const Planificacion = {
                                citePlanificacion: $("#txtCiteCarpeta").val(),
                                lugar: $("#cboLugar").val(),
                                estadoPlanificacion: "INI",
                                estadoCarpeta: "INI",
                                nombreRegional: $("#cboUnidadRegional").val(),
                                nombreEjecutora: $("#cboUnidadEjecutora").val(),
                                idCentro: $("#cboCentro").val(),
                                idUnidadResponsable: $("#cboUnidadResponsable").val(),
                                montoPoa: $("#txtTotal").val(),
                                fechaPlanificacion: $("#txtFechaRegistro").val(),
                                DetallePlanificacion: vmDetallePlanificacion
                            }

                            console.log(vmDetallePlanificacion);

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


                        } // End
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