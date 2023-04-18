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

            if (uDetalle === "") {
                toastr.warning("", "Necesita Ingresar La Descripcion De La Partida")
                return false;
            }

            if (uMedida === "") {
                toastr.warning("", "No Deje En Blanco La Unidad De Medida")
                return false;
            }

            if (uCantidad === "") {
                toastr.warning("", "No Deje La Cantidad En Blanco")
                return false;
            }

            if (uPrecioUnitario === "") {
                toastr.warning("", "No Deje Precio Unitario En Blanco")
                return false;
            }

            if (uActividad === "") {
                toastr.warning("", "No Deje En Blanco La Actividad")
                return false;
            }

            if (isNaN(parseInt(uActividad))) {
                toastr.warning("", "Debe Ingresar Un Valor Numerico")
                return false;
            }

            if (parseInt(uActividad) < 1 || parseInt(uActividad) > 41){
                toastr.warning("", "Debe Estar En El RAndo de 1 - 41")
                return false;
            }


            if (isNaN(parseInt(uCantidad))) {
                toastr.warning("", "Debe Ingresar Un Valor Numerico")
                return false;
            }
            if (isNaN(parseInt(uPrecioUnitario))) {
                toastr.warning("", "Debe Ingresar Un Valor Numerico")
                return false;
            }
            var rd = Math.floor(Math.random() * 99999);

            let partida = {
                idPartida: data.id,
                nombrePartida: data.text,
                nombreItem: uDetalle, 
                medida: uMedida,
                codigoPartida: data.codigo,
                cantidad: parseInt(uCantidad),
                precio: parseFloat(uPrecioUnitario),
                total: (uCantidad * uPrecioUnitario),
                codigoActividad: uActividad,
                idFila: rd
            }

            //console.log(partida)

            PartidasParaPlanificacion.push(partida)

            mostrarPartida_Precios()
            
            $("#cboBuscarPartida").val("").trigger("change")

            swal.close()
        }
    )
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
}

$(document).on("click", "button.btn-eliminar", function () {
    const _idPartida = $(this).data("idFila")

    PartidasParaPlanificacion = PartidasParaPlanificacion.filter(p => p.idFila != _idPartida);
    mostrarPartida_Precios();
})

$("#btnTerminarSolicitud").click(function () {

    if ($("#txtCiteCarpeta").val() === "") {
        toastr.warning("", "Necesita Registrar El Cite De La Carpeta")
        return false;
    }

    if (PartidasParaPlanificacion.length < 1) {
        toastr.warning("", "Debe Ingresar Partidas Presupuestarias")
        return;
    }

    const vmDetallePlanificacion = PartidasParaPlanificacion;

    const planificacion = {
        citePlanificacion: $("#txtCiteCarpeta").val(),
        lugar: $("#cboLugar").val(),
        certificadoPoa: "",
        referenciaPlanificacion: $("Mensaje01").val(),
        unidadProceso: "UNI",
        estadoCarpeta: "INI",
        referenciaPlanificacion: "Carpeta De Servicio",
        montoPoa: 0.00,
        montoPresupuesto: 0.00,
        montoCompra: 0.00,
        nombreRegional: $("#cboUnidadRegional").val(),
        nombreEjecutora: $("#cboUnidadEjecutora").val(),
        idCentro: $("#cboCentro").val(),
        idUnidadResponsable: $("#cboUnidadResponsable").val(),
        idDocumento: $("#cboDocumento").val(),
        montoPlanificacion: $("#txtTotal").val(),
        fechaPlanificacion: $("#txtFechaRegistro").val(),
        DetallePlanificacion: vmDetallePlanificacion
    }

    //alert($("#txtFechaRegistro").val());
    //console.log(VMDetallePlanificacion);

    //alert($("#cboDocumento").val());

    $("#btnTerminarSolicitud").LoadingOverlay("show");


    fetch("/Planificacion/RegistrarPlanificacion", {
        method: "POST",
        headers: {"Content-Type":"application/json; charset=utf-8" },
        body: JSON.stringify(planificacion)
    })
        .then(response => {
            $("#btnTerminarSolicitud").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {

                //console.log(responseJson);

                PartidasParaPlanificacion = [];
                mostrarPartida_Precios();

                $("#txtCiteCarpeta").val("")
                $("#cboCentro").val($("#cboCentro option:first").val())
                $("#cboUnidadResponsable").val($("#cboUnidadResponsable option:first").val())
                $("#cboDocumento").val($("#cboDocumento option:first").val())

                swal("Registrado!", `Numero Planificacion : ${responseJson.objeto.numeroPlanificacion}`, "success")
            }
            else {
                swal("Lo Sentimos!", "No Se Pudo Registrar La Carpeta De Planificacion", "error")
            }
        })
})