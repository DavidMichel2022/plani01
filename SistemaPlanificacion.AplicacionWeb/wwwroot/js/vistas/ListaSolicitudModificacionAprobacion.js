let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/ModificacionPoa/ListaMisModificacionesPoa',
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
                    return data;
                }
            },
            { "data": "editCantidad" },
            { "data": "editTemporalidad" },
            { "data": "totalActual" },
            { "data": "totalModificar" },
            { "data": "fechaRegistro" },
            { "data": "estado" },
            {
                "data": "estado", render: function (data) {
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
    //----------------------------
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }
    const data = tablaData.row(filaSeleccionada).data();
    //cite: "SIS-ADM 736/2022"
    //detalleModificacions: Array[]
    //detalleModificados: Array[]
    //editCantidad: false
    //editIndicador: false
    //editPrecio: false
    //editTemporalidad: false
    //estado: null
    //"fechaAprobación": null
    //fechaModificacion: null
    //fechaRegistro: null
    //idModificacionPoa: 17
    //idUsuarioAprobacion: null
    //idUsuarioModificacion: null
    //idUsuarioRegistro: null
    //justificacion: "Solicitud de Modificacion"
    //lugar: "Santa Cruz"
    //tipoAjuste: 3
    //totalActual: null
    //totalModificar: 81.19
    console.log("-----informacion------");
    console.log(data);
    //----------------------------
    $("#txtIdModificacionPoa").val(data.idModificacionPoa);
    $("lbIdModificacionPoa").html(data.idModificacionPoa);

    $("#modalData").modal("show");
})

$("#btnModificarSolicitud").click(function () {
    var modificacionRequerimientoPoa = {
        idModificacionPoa: $("#txtIdModificacionPoa").val(),        
    };

    alert("Guardando...");
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


