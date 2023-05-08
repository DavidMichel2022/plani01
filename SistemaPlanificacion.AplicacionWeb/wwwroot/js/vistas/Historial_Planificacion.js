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

const VISTA_BUSQUEDA = {
    busquedafecha: () => {
        $("txtFechaInicio").val("");
        $("txtFechaFin").val("");
        $("txtNumeroPlanificacion").val("");

        $(".busqueda-fecha").show();
        $(".busqueda-planificacion").hide();

    },
    busquedaplanificacion:() => {
        $("txtFechaInicio").val("");
        $("txtFechaFin").val("");
        $("txtNumeroPlanificacion").val("");

        $(".busqueda-planificacion").show();
        $(".busqueda-fecha").hide();
    }
}

let tablaData;
$(document).ready(function () {
    VISTA_BUSQUEDA["busquedafecha"]()

    $("#txtFechaInicio").datepicker({
        dateFormat: 'dd/mm/yy',
    }).datepicker("setDate", new Date());

    $("#txtFechaFin").datepicker({
        dateFormat: 'dd/mm/yy',
    }).datepicker("setDate", new Date());
})

$("#cboBuscarPor").change(function () {
    $("#tbPlanificacion tbody").html("")
    if ($("#cboBuscarPor").val() == "fecha") {
        VISTA_BUSQUEDA["busquedafecha"]()
    } else {
        VISTA_BUSQUEDA["busquedaplanificacion"]()
    }
})


$("#btnBuscar").click(function () {

    if ($("#cboBuscarPor").val() == "fecha") {
        if ($("#txtFechaInicio").val().trim() == "" || $("#txtFechaFin").val().trim() == "") {
            toastr.warning("", "Debe Ingresar Fecha Inicio y Fin")
            return;
        }
    }
    else {
        if ($("#txtNumeroPlanificacion").val() == "") {
            toastr.warning("", "Debe Ingresar El Numero De Carpeta")
            return;
        }
    }

    let numeroPlanificacion = $("#txtNumeroPlanificacion").val();


    let fechaInicio = $("#txtFechaInicio").val();
    let fechaFin = $("#txtFechaFin").val().trim();

    if ($("#cboBuscarPor").val() == "fecha")
    {
        fetch(`/Planificacion/Historial?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
            .then(response => {
                $(".card-body").find("div.row").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                $("#tbPlanificacion tbody").html("");
                if (responseJson.length > 0) {
                    responseJson.forEach((planificacion) => {
                        $("#tbPlanificacion tbody").append(
                            $("<tr>").append(
                                $("<td>").text(planificacion.fechaPlanificacion),
                                $("<td>").text(planificacion.numeroPlanificacion),
                                $("<td>").text(planificacion.citePlanificacion),
                                $("<td>").text(planificacion.nombreUnidadResponsable),
                                $("<td>").text(planificacion.nombreCentro),
                                $("<td class='text-right'>").text(formateadorDecimal.format(planificacion.montoPlanificacion)),
                                $("<td>").append(
                                    $("<button>").addClass("btn btn-info btn-sm").append(
                                        $("<i>").addClass("fas fa-eye")
                                    ).data("planificacion", planificacion), " <a class='btn btn-default btn-sm' href='/Planificacion/MostrarPDFPlanificacion?numeroPlanificacion=" + planificacion.numeroPlanificacion+"'><i class='fas fa-print'></i></a>"                                    
                                )
                            )
                        )
                    })
                }
            })
    }
    else
    {
        fetch(`/Planificacion/Historial?numeroPlanificacion=${numeroPlanificacion}`)
            .then(response => {
                $(".card-body").find("div.row").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                $("#tbPlanificacion tbody").html("");
                if (responseJson.length > 0) {
                    responseJson.forEach((planificacion) => {
                        $("#tbPlanificacion tbody").append(
                            $("<tr>").append(
                                $("<td>").text(planificacion.fechaPlanificacion),
                                $("<td>").text(planificacion.numeroPlanificacion),
                                $("<td>").text(planificacion.citePlanificacion),
                                $("<td>").text(planificacion.nombreUnidadResponsable),
                                $("<td>").text(planificacion.nombreCentro),
                                $("<td class='text-right'>").text(formateadorDecimal.format(planificacion.montoPlanificacion)),
                                $("<td>").append(
                                    $("<button>").addClass("btn btn-info btn-sm").append(
                                        $("<i>").addClass("fas fa-eye")
                                    ).data("planificacion", planificacion), " <a class='btn btn-default btn-sm' href='/Planificacion/MostrarPDFPlanificacion?numeroPlanificacion=" + planificacion.numeroPlanificacion + "'><i class='fas fa-print'></i></a>"
                                )
                            )
                        )
                    })
                }

            })
    }
})

$("#tbPlanificacion tbody").on("click", ".btn-info", function () {
    //let Contador = 0
    //let d = $(this).data("planificacion")

    const data = $(this).data("planificacion");

    //console.log(data);

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
                $("<td>").text(item.codigoActividad),
                $("<td>").text(item.codigoPartida),
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td>").text(formateadorEntero.format(item.cantidad)),
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
    $("#linkImprimir").attr("href", `/Planificacion/MostrarPDFPlanificacion?numeroPlanificacion=${data.numeroPlanificacion}`);

    $("#modalData").modal("show");

})