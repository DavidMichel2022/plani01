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
        url: '/RequerimientoPoa/ObtenerHora',
        type: 'GET',
        success: function (data) {
            $("#txtFechaRegistro").val(data);
        }
    });

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
    fetch("/Negocio/Obtener").then(response => { return response.ok ? response.json() : Promise.reject(response); })
        .then(responseJson => {
            if (responseJson.estado) {
                const d = responseJson.objeto;
                $("#inputGroupTotal").text(`Total - ${d.simboloMoneda}`)
            }
        })    
    $("#cboBuscarPartida").select2({
        ajax: {
            url: "/RequerimientoPoa/ObtenerPartidasRequerimiento",
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
        return contenedor;
    }

    $("#cboBuscarRequerimiento").select2({
        ajax: {
            url: "/RequerimientoPoa/ObtenerRequerimientosPoaMiUnidad",
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
                            id: item.idRequerimientoPoa,
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
        language: "es",
        placeholder: 'Buscar Partida Requerimientos....',
        minimumInputLength: 1,
        templateResult: formatoResultadosRequerimiento,
    });

})


function formatoResultadosRequerimiento(data) {
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



/*
$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})*/

let PartidasParaRequerimientoPoa = [];
let ModificacionParaRequerimientoPoa = [];
let partida_encontrada;

$("#cboBuscarPartida").on("select2:select", function (e) {
    const data = e.params.data;
    let partida_encontrada = PartidasParaRequerimientoPoa.filter(p => p.idPartida == data.id);
    let tituloMensajeCodigo = data.codigo.trim();
    let tituloMensajeNombre = data.text;

    $('#txtIdPartidaModal').val(data.id)
    $('#txtCodigoPartidaModal').val(data.codigo)
    $('#txtNombrePartidaModal').val(data.nombrePartida)
    $("#txtTituloMensaje").val(tituloMensajeCodigo + " - " + tituloMensajeNombre);
    $("#modalData").modal("show")
});

$("#cboBuscarRequerimiento").on("select2:select", function (e) {

    const data = e.params.data;
    console.log("seleccionando Datos:");
    console.log(data);
    //console.log("Seleccionar Modificacion");
    //console.log(ModificacionParaRequerimientoPoa);
    let partida_encontrada = ModificacionParaRequerimientoPoa.filter(p => p.id == data.id);
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
        //idPartida: 1,//uIdPartidaModal,
        codigo: data.codigo,
        //nombrePartida: "n",//uNombrePartidaModal,
       // codigoActividad: "2",//uActividad,
        id: data.id,
        detalle: data.detalle,
        medida: data.medida,
        cantidad: data.cantidad,
        precio: data.precio,
        total: data.total,
        observacion: data.observacion,
        temporalidad: temporalidad,        
        idFila: rd
    }

    ModificacionParaRequerimientoPoa.push(partida)
   // alert("cargando dato");
 //   console.log(partida);
  //  console.log("---Modificacion Lista---");
//    console.log(ModificacionParaRequerimientoPoa);
  //  console.log("---Modificacion End---");
    mostrarPartida_Requerimientos()

    /*
    $('#txtIdPartidaModal').val(data.id)
    $('#txtCodigoPartidaModal').val(data.codigo)
    $('#txtNombrePartidaModal').val(data.nombrePartida)
    $("#txtTituloMensaje").val(tituloMensajeCodigo + " - " + tituloMensajeNombre);
    $("#modalData").modal("show")*/
});

function mostrarPartida_Precios() {
    let total = 0;

    $("#tbPartida tbody").html("")
    PartidasParaRequerimientoPoa.forEach((item) => {
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

    let ImporteRequerimientoPoa = formateadorDecimal.format(total)
    $("#txtTotal").val(ImporteRequerimientoPoa)
    $("#txtMontoRequerimientoPoaE").val(ImporteRequerimientoPoa)
}

function mostrarPartida_Requerimientos() {
    let total = 0;
    var contador = 0;
    //alert("Cargar Informacion a Tabla");

    console.log("--Mostrar info--");
    console.log(ModificacionParaRequerimientoPoa);

    $("#tbRequerimiento tbody").html("")
    ModificacionParaRequerimientoPoa.forEach((item) => {
        contador++;
        console.log("--->"+contador);
        total = total + parseFloat(item.total)

        $("#tbRequerimiento tbody").append(
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
    })
    $("#tbRequerimiento tfooter").html("<tr><td>PRUEBA</td></tr>")

    let ImporteRequerimientoPoa = formateadorDecimal.format(total)
    $("#txtTotal").val(ImporteRequerimientoPoa)
    $("#txtMontoRequerimientoPoaE").val(ImporteRequerimientoPoa)
}


function mostrarPartida_Modal() {
    let total = 0;
    cont = 0;
    var size = PartidasParaRequerimientoPoa.length;
    $("#tbPartida tbody").html("")
    PartidasParaRequerimientoPoa.forEach((item) => {
        cont++;
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
                //$("<td>").text(formateadorDecimal.format(item.mesJul)),
                ///$("<td>").text(formateadorDecimal.format(item.mesAgo)),
                //$("<td>").text(formateadorDecimal.format(item.mesSep)),
                //$("<td>").text(formateadorDecimal.format(item.mesOct)),
                //$("<td>").text(formateadorDecimal.format(item.mesNov)),
               // $("<td>").text(formateadorDecimal.format(item.mesDic)),
               
            )
        )

        if (cont == size) {

            $("#tbPartida tbody").append(
                $("<tr>").append(
                    $("<td>").text("Total"),
                    $("<td>").text(""),
                    $("<td>").text("")
                ))
        }
    })

    //$("#tbPartida tfooter").html("")
    //$("#tbPartida tfooter").append(
    //    $("<tr>").append(
    //        $("<td>").text(" MONTO ")
    //    )
    //)

  

    let ImporteRequerimientoPoa = total
    $("#txtTotal").val(ImporteRequerimientoPoa)
}

$(document).on("click", "button.btn-eliminar", function () {
    const _idPartida = $(this).data("idFila")

    PartidasParaRequerimientoPoa = PartidasParaRequerimientoPoa.filter(p => p.idFila != _idPartida);
    mostrarPartida_Precios();
})

$("#btnCargar").click(function () {
    $("#modalData").modal("show");
})

$("#btnTerminarSolicitud").click(function () {
    let cadenacite = $("#txtCiteCarpeta").val().trim();
    let citeRequerimientoPoa = $("#txtCiteCarpeta").val();
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
            fetch(`/RequerimientoPoa/ObtenerRequerimientos?citeRequerimientoPoa=${citeRequerimientoPoa}`).then(response => { return response.ok ? response.json() : Promise.reject(response); })
                .then(responseJson => {
                    if (responseJson.length > 0) {
                        swal.fire({
                            title: "Atencion!",
                            text: "Este Nro. De Requerimiento Poa Ya Existe.",
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
                            const vmDetalleRequerimientoPoa = PartidasParaRequerimientoPoa;
                            const requerimientopoa = {
                                citeRequerimientoPoa: $("#txtCiteCarpeta").val(),
                                lugar: $("#cboLugar").val(),
                                estadoRequerimientoPoa: "INI",
                                nombreRegional: $("#cboUnidadRegional").val(),
                                nombreEjecutora: $("#cboUnidadEjecutora").val(),
                                idCentro: $("#cboCentro").val(),
                                idUnidadResponsable: $("#cboUnidadResponsable").val(),
                                montoPoa: $("#txtTotal").val(),
                                fechaRequerimientoPoa: $("#txtFechaRegistro").val(),
                                DetalleRequerimientoPoas: vmDetalleRequerimientoPoa
                            }
                            $("#btnTerminarSolicitud").LoadingOverlay("show");
                            fetch("/RequerimientoPoa/RegistrarRequerimientoPoa", {
                                method: "POST",
                                headers: { "Content-Type": "application/json; charset=utf-8" },
                                body: JSON.stringify(requerimientopoa)
                            })
                                .then(response => {
                                    $("#btnTerminarSolicitud").LoadingOverlay("hide");
                                    return response.ok ? response.json() : Promise.reject(response);
                                })
                                .then(responseJson => {

                                    if (responseJson.estado) {

                                        PartidasParaRequerimientoPoa = [];
                                        mostrarPartida_Precios();
                                        swal.fire({
                                            title: "Registrado!",
                                            text: `N°. Cite : ${responseJson.objeto.citeRequerimientoPoa}`,
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
                                            text: "No Se Pudo Registrar La Carpeta De Requerimiento Poa",
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
    var uObservacion = $('#txtObservacionModal').val();

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

    let partida = {
        idPartida: uIdPartidaModal,
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

    PartidasParaRequerimientoPoa.push(partida)

    mostrarPartida_Modal()

    $("#cboBuscarPartida").val("").trigger("change")

    limpiarModal()

    $("#modalData").modal("hide")
})