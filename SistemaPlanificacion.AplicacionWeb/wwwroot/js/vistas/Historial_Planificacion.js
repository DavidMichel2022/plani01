const VISTA_BUSQUEDA = {

    busquedaFecha:() => {
        $("txtFechaInicio").val("");
        $("txtFechaFin").val("");
        $("txtNumeroPlanificacion").val("");

        $(".busqueda-fecha").show();
        $(".busqueda-planificacion").hide();

    },
    busquedaPlanificacion: () => {
        $("txtFechaInicio").val("");
        $("txtFechaFin").val("");
        $("txtNumeroPlanificacion").val("");

        $(".busqueda-fecha").hide();
        $(".busqueda-Planificacion").show();
    }
}


$(document).ready(function () {

     VISTA_BUSQUEDA["busquedaFecha"]()

    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaInicio").datepicker({ dateFormat: " dd/mm/yy" })
    $("#txtFechaFin").datepicker({ dateFormat: " dd/mm/yy" })


})

$("#cboBuscarPor").change(function () {

    if ($("#cboBuscarPor").val() == "fecha") {
        VISTA_BUSQUEDA["busquedaFecha"]()
    } else {
        VISTA_BUSQUEDA["busquedaPlanificacion"]()
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
        if ($("#txtNumeroPlanificacion").val().trim == "") {
            toastr.warning("", "Debe Ingresar El Numero De Carpeta")
            return;
        }
    }

    let numeroPlanificacion = $("#txtNumeroPlanificacion").val();
    let fechaInicio = $("#txtFechaInicio").val().trim();
    let fechaFin = $("#txtFechaFin").val().trim();

    $(".card-body").find("div.row").LoadingOverlay("show");

    fetch(`/Planificacion/Historial?numeroPlanificacion=${numeroPlanificacion}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)

        .then(response => {
            $(".card-body").find("div.row").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            console.log(responseJson);

            $("#tbplanificacion tbody").html("");

            if (responseJson.length > 0) {

                responseJson.forEach((planificacion) => {

                    $("#tbplanificacion tbody").append(

                        $("<tr>").append(
                            $("<td>").text(planificacion.fechaRegistro),
                            $("<td>").text(planificacion.numeroPlanificacion),
                            $("<td>").text(planificacion.citePlanificacion),
                            $("<td>").text(planificacion.nombreResponsable),
                            $("<td>").text(planificacion.nombreActividad),
                            $("<td>").text(planificacion.TotalPlanificacion),
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
})

$("#tbplanificacion tbody").on("click", ".btn-info", function () {
    let Contador = 0
    let d = $(this).data("planificacion")

    $("#txtFechaRegistro").val(d.fechaRegistro)
    $("#txtNumPlanificacion").val(d.numeroPlanificacion)
    $("#txtCitePlanificacion").val(d.citePlanificacion)
    $("#txtnombreResponsable").val(d.nombreResponsable)
    $("#txtnombreActividad").val(d.nombreActividad)
    $("#txtTotalPlanificacion").val(d.TotalPlanificacion)

    $("#tbPartidas tbody").html("")

    d.detallePlanificacion.forEach((item) => {
        contador++;
        $("#tbPartidas tbody").append(
            $("<tr>").append(
                $("<td>").text(contador),
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td>").text(item.cantidad),
                $("<td>").text(item.precio),
                $("<td>").text(item.precioTotal),
                $("<td>").text(item.actividad)
            )
        )
    })

    $("#linkImprimir").attr("href", `/Planificacion/MostrarPDFPlanificacion?numeroPlanificacion=${d.numeroPlanificacion}`);

    $("#modalData").modal("show");

})


