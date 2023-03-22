const MODELO_BASE = {
    idEmpresa: 0,
    codigo: "",
    nombre: "",
    esActivo: 1,
}

let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Planificacion/ListaMisCarpetas',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idPlanificacion", "visible": true, "searchable": true },
            { "data": "numeroPlanificacion" },
            { "data": "citePlanificacion" },
            { "data": "nombreRegional" },
            { "data": "nombreEjecutora" },
            { "data": "montoPlanificacion" },
            {
                "data": "estadoCarpeta", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">Inicial</span>';
                    else
                        return '<span class="badge badge-info">En Tramite</span>';
                }
            },
            //{ "data": "unidadSolicitante" },
            { "data": "fechaPlanificacion" },
            {
                "defaultContent": '<div class="form-inline">' +
                    '<button class="btn btn-info btn-ver btn-sm"><i class="fas fa-eye"></i></button>' +
                    '<button class="btn btn-primary btn-editar btn-sm"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>' +
                    '</div>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Listado Mis Carpetas',
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


let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-ver", function () {

    
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();
   
    $("#txtFechaRegistro").val(data.fechaRegistro)
    $("#txtNumVenta").val(data.numeroPlanificacion)
    /*$("#txtUsuarioRegistro").val(data.idRegional)
    $("#txtTipoDocumento").val(data.citePlanificacion)
    $("#txtDocumentoCliente").val(data.operacion)
    $("#txtNombreCliente").val(data.unidadResponsable)
    $("#txtSubTotal").val(data.tipo)
    $("#txtIGV").val(data.estado)
    $("#txtTotal").val(data.montoTotal)
    $("#tbProductos tbody").html("")
    cont = 0;
    data.detalleCarpeta.forEach((item) => {
        cont++;
        $("#tbProductos tbody").append(
            $("<tr>").append(
                $("<td>").text(cont),
                $("<td>").text(item.detalle),
                $("<td>").text(item.partida),
                $("<td>").text(item.unidadMedida),
                $("<td>").text(item.precioTotal)
            )
        )
    })*/
    $("#linkImprimir").attr("href", `/Planificacion/MostrarPDFCarpeta?numeroCarpeta=${data.numeroCarpeta}`);
    $("#modalData").modal("show");

})

$("#tbdata tbody").on("click", ".btn-eliminar", function () {

    let fila;

    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    }
    else {
        fila = $(this).closest("tr")
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "Está Seguro?",
        text: `Eliminar La Carpeta de Requerimiento: "${data.numeroCarpeta}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, Eliminar",
        cancelButtonText: "No, Cancelar",
        closeOnConfirm: false,
        closeOnCancel: true,
    },
        function (respuesta) {
            if (respuesta) {
                $(".showSweetalert").LoadingOverlay("show");
                alert('TEST');
                /* fetch(`/Empresa/Eliminar?IdEmpresa=${data.idEmpresa}`, {
                     method: "DELETE"
                 })
                     .then(response => {
                         $(".showSweetalert").LoadingOverlay("hide");
                         return response.ok ? response.json() : Promise.reject(response);
                     })
                     .then(responseJson => {
 
                         if (responseJson.estado) {
 
                             tablaData.row(fila).remove().draw()
 
                             swal("Listo!", "La Empresa Fue Eliminada", "success")
                         }
                         else {
                             swal("Lo Sentimos", responseJson.mensaje, "error")
                         }
                     })*/
            }
        }
    )
})

$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();

    // mostrarModal(data);
    alert("Editar Carpeta Requerimiento PENDIENTE......");
})


