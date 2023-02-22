const MODELO_BASE = {
    idPrograma: 0,
    codigo:"",
    nombre: "",
    esActivo: 1,
}

let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Programa/Lista',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idPrograma", "visible": false, "searchable": false },
            { "data": "codigo" },
            { "data": "nombre" },

            {
                "data": "esActivo", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">Activo</span>';
                    else
                        return '<span class="badge badge-danger">No Activo</span>';
                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
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
                filename: 'Reporte Programas',
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

function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idPrograma)
    $("#txtCodigo").val(modelo.codigo)
    $("#txtNombre").val(modelo.nombre)
    $("#cboEstado").val(modelo.esActivo)

    $("#modalData").modal("show")
}

$("#btnNuevo").click(function () {
    mostrarModal()
})

$("#btnGuardar").click(function () {

    //debugger;

    if ($("#txtCodigo").val().trim() == "") {
        toastr.warning("", "No Deje En Blanco El Codigo De Programa")
        $("#txtCodigo").focus()
        return;
    }
    if ($("#txtNombre").val().trim() == "") {
        toastr.warning("", "No Deje En Blanco Nombre De Programa")
        $("#txtNombre").focus()
        return;
    }

    const modelo = structuredClone(MODELO_BASE);

    modelo["idPrograma"] = parseInt($("#txtId").val())
    modelo["codigo"] = $("#txtCodigo").val()
    modelo["nombre"] = $("#txtNombre").val()
    modelo["esActivo"] = $("#cboEstado").val()

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idPrograma == 0) {
        fetch("/Programa/Crear", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.estado) {

                    tablaData.row.add(responseJson.objeto).draw(false)
                    $("#modalData").modal("hide")
                    swal("Listo!", "El Programa Fue Creado", "success")
                }
                else {
                    swal("Lo Sentimos", responseJson.mensaje, "error")
                }
            })
    }
    else {
        fetch("/Programa/Editar", {
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
                    swal("Listo!", "El Programa Fue Modificado", "success")
                }
                else {
                    swal("Lo Sentimos", responseJson.mensaje, "error")
                }
            })
    }
})

let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr")
    }

    const data = tablaData.row(filaSeleccionada).data();

    mostrarModal(data);
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
        text: `Eliminar El Programa"${data.nombre}"`,
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

                fetch(`/Programa/Eliminar?IdPrograma=${data.idPrograma}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetalert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw()

                            swal("Listo!", "El Programa Fue Eliminado", "success")
                        }
                        else {
                            swal("Lo Sentimos", responseJson.mensaje, "error")
                        }
                    })
            }
        }
    )
})