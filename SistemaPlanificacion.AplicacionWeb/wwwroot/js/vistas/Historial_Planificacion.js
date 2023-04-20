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
                                    ).data("planificacion", planificacion)
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
                                    ).data("planificacion", planificacion)
                                )
                            )
                        )
                    })
                }

            })
    }
})

$("#tbPlanificacion tbody").on("click", ".btn-info", function () {
    let Contador = 0
    let d = $(this).data("planificacion")

    //console.log(d);

    let ImportePlanificacion = formateadorDecimal.format(d.montoPlanificacion)

    $("#txtFechaRegistro").val(d.fechaPlanificacion)
    $("#txtNumPlanificacion").val(d.numeroPlanificacion)
    $("#txtCiteCarpeta").val(d.citePlanificacion)
    $("#txtNombreResponsable").val(d.nombreUnidadResponsable)
    $("#txtNombreCentro").val(d.nombreCentro)
    $("#txtTotalPlanificacion").val(ImportePlanificacion)

    $("#tbPartidas tbody").html("")

    d.detallePlanificacion.forEach((item) => {
        Contador++;
        $("#tbPartidas tbody").append(
            $("<tr>").append(
                $("<td>").text(Contador),
                $("<td>").text(item.codigoPartida),
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td class='text-right'>").text(formateadorEntero.format(item.cantidad)),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.precio)),
                $("<td class='text-right'>").text(formateadorDecimal.format(item.total)),
                $("<td class='text-center'>").text(item.codigoActividad)
            )
        )
    })

    $("#linkImprimir").attr("href", `/Planificacion/MostrarPDFPlanificacion?numeroPlanificacion=${d.numeroPlanificacion}`);

    $("#modalData").modal("show");

})