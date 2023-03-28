let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({

        responsive: true,
        "ajax": {
            "url": `/Planificacion/ListaMisCarpetas`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //{ "data": "idPlanificacion", "visible": false, "searchable": true },
            { "data": "fechaPlanificacion" },
            { "data": "numeroPlanificacion" },
            { "data": "citePlanificacion" },
            { "data": "nombreCentro" },
            { "data": "nombreUnidadResponsable" },
            { "data": "montoPlanificacion" },
            {
                "data": "estadoCarpeta", render: function (data) {
                    if (data == "INI")
                        return `<span class="badge badge-info">Inicial</span>`;
                    else
                        return '<span class="badge badge-info">En Tramite</span>';
                }
            },
            {
                "defaultContent": `<div class="form-inline">` +
                    `<button class="btn btn-info btn-ver btn-sm"><i class="fas fa-eye"></i></button>` +
                    `<button class="btn btn-primary btn-editar btn-sm"><i class="fas fa-pencil-alt"></i></button>` +
                    `<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>` +
                    `</div>`,
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
                filename: `Reporte Listado Mis Carpetas`,
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            }, `pageLength`
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
    let nueva_url = `/Planificacion/ListaMisCarpetas`;

    //tablaData.ajax.url(nueva_url).load();
})

//alert($("data.citePlanificacion").val();

let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-ver", function () {

    
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();
   
    $("#txtFechaRegistro").val(data.fechaPlanificacion)
    $("#txtNumeroPlanificacion").val(data.numeroPlanificacion)
    $("#txtCiteCarpeta").val(data.citePlanificacion)
    $("#txtUnidadSolicitante").val(data.nombreCentro)
    $("#txtUnidadResponsable").val(data.nombreUnidadResponsable)
    $("#txtTotal").val(data.montoPlanificacion)

    $("#tbPartidas tbody").html("")
    cont = 0;
    data.detallePlanificacion.forEach((item) => {
        cont++;
        $("#tbPartidas tbody").append(
            $("<tr>").append(
                $("<td>").text(cont),
                $("<td>").text(item.nombreItem),
                $("<td>").text(item.medida),
                $("<td>").text(item.cantidad),
                $("<td>").text(item.precio),
                $("<td>").text(item.total),
                $("<td>").text(item.codigoActividad),
            )
        )
    })
    $("#linkImprimir").attr("href",`/Planificacion/MostrarPDFCarpeta?numeroCarpeta=${data.numeroCarpeta}`);
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
        text: `Anular La Carpeta de Requerimiento: "${data.numeroCarpeta}"`,
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
                fetch("/Planificacion/Anular", {
                    method: "PUT",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(modelo)
                })
                    .then(response => {
                        $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);

                            filaSeleccionada = null;

                            $("#modalData").modal("hide")
                            swal("Listo!", "La Carpeta De Planificacion Fue Anulada", "success")
                        }
                        else {
                            swal("Lo Sentimos", responseJson.mensaje, "error")
                        }
                    })
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


