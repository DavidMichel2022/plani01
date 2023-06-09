let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/ModificacionPoa/ListaModificacionesPoa',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idModificacionPoa" },
            { "data": "lugar" },
            { "data": "cite" },
            { "data": "justificacion" },
            {
                "data": "editIndicador", render: function (data) {
                    if (data)
                        return "X";
                    else return "";
                }
            },
            {
                "data": "editCantidad", render: function (data) {
                    if (data)
                        return "X";
                    else return "";
                }
},
            {
                "data": "editTemporalidad", render: function (data) {
                    if (data)
                        return "X";
                    else return "";
                } },
            { "data": "totalActual" },
            { "data": "totalModificar" },
            { "data": "fechaRegistro" },
            { "data": "estadoModificacion" },
            {
                "data": "estadoModificacion", render: function (data) {
                    return "<button class='btn btn-primary btn-aprobar' > Aprobacion</button> &nbsp;<button class='btn btn-success' disabled> Excel</button>";
                }
            }

        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte POA Mi Unidad',
                exportOptions: {
                    columns: [1, 2, 3]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})


$(document).on("click", "button.btn-aprobar", function () {
    //const _idSolicitudModificacion = $(this).data("idFila");
    var filaSeleccionada = '';
    $("#tbPartida tbody").html("");
    $("#tbRequerimiento tbody").html("");
    //----------------------------
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }
    const data = tablaData.row(filaSeleccionada).data();   
    console.log("-----informacion------");
    console.log(data);
    //----------------------------
    $("#txtIdModificacionPoa").val(data.idModificacionPoa);
    $("#lbIdModificacionPoa").html(data.idModificacionPoa);
    $("#lbNombreUnidadResponsable").html("---");
    $("#lbGestion").html(data.Gestion);
    $("#lbFechaRegistro").html(data.fechaRegistro);
    $("#lbCiteModificacion").html(data.cite);
    $("#lbJustificacion").html(data.justificacion);
    if (data.editCantidad) {
        $("#checkCantidad").prop('checked', true);
    }
    if (data.editIndicador) {
        $("#checkIndicador").prop('checked', true);
    }
    if (data.editPrecio) {
        $("#checkPrecio").prop('checked', true);
    }
    if (data.editTemporalidad) {
        $("#checkTemporalidad").prop('checked', true);
    }

    var detalleModificacions = data.detalleModificacions;
    var detalleRequerimientosModificados = data.detalleRequerimientosModificados;

    console.log("Modificaciones Agregados");
    for (var i = 0; i < detalleModificacions.length; i++) {
        console.log("---Agregados----");
        var modificacion = detalleModificacions[i];
        console.log(modificacion);

        //-----------------------------
        $("#tbPartida tbody").append(
            $("<tr>").append(
                $("<td>").text(i + 1),
                $("<td>").text(modificacion.idDetalleModificacionPoa),
                $("<td>").text(modificacion.codigo),
                $("<td>").text(modificacion.detalle),
                $("<td>").text(modificacion.medida),
                $("<td>").text(modificacion.cantidad),
                $("<td>").text(modificacion.precio),
                $("<td>").text(modificacion.total),
                $("<td>").text(modificacion.codigo),
                $("<td>").text(modificacion.observacion)
            )
        );
        //-----------------------------
    } 
    console.log("Modificados Reemplazados:DETALLE MODIFICADOS");
    console.log(detalleRequerimientosModificados);
    for (var j = 0; j < detalleRequerimientosModificados.length; j++) {
        console.log("---Reemplazados----"+j);
        var modificado = detalleRequerimientosModificados[j];
        //-----------------------------
        $("#tbRequerimiento tbody").append(
            $("<tr>").append(
                $("<td>").text(j + 1),
                $("<td>").text(modificado.idDetalleModificacionPoa),
                $("<td>").text(modificado.codigo),
                $("<td>").text(modificado.detalle),
                $("<td>").text(modificado.medida),
                $("<td>").text(modificado.cantidad),
                $("<td>").text(modificado.precio),
                $("<td>").text(modificado.total),
                $("<td>").text(modificado.codigo),
                $("<td>").text(modificado.observacion)
            )
        );
        //-----------------------------
    } 
    

    $("#modalData").modal("show");
})

$("#btnModificarSolicitud").click(function () {
    var modificacionRequerimientoPoa = {
        idModificacionPoa: $("#txtIdModificacionPoa").val(),        
    };

    fetch("/ModificacionPoa/AprobarModificacionPoa", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(modificacionRequerimientoPoa)
    })
        .then(response => {
            $("#modalData").modal("hide");
            //$("#btnTerminarSolicitud").LoadingOverlay("hide");
            //return response.ok ? response.json() : Promise.reject(response);
            console.log("Aprobado");
            


    })
        .then(responseJson => {
            $("#modalData").modal("hide");
        });


})


