﻿const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
})

$(document).ready(function () {
    tablaData = $('#tbData').DataTable(
        {
            language: {
                url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
            }
        }
    );

    $.ajax({
        url: '/AnteproyectoPoa/ObtenerHora',
        type: 'GET',
        success: function (data) {
            $("#txtFechaRegistro").val(data);
        }
    });
})

function mostrarDatos() {

    const input = document.getElementById("inputExcel");
    const formData = new FormData();
    formData.append("ArchivoExcel", input.files[0]);

    fetch("/AnteproyectoPoa/MostrarDatos", {
        method: "POST",
        body: formData
    })
        .then(response => {
            return response.json()
        })
        .then(responseJson => {
            console.log(responseJson);

            responseJson.forEach((item) => {
                $('#tbData').DataTable().row.add([
                    item.idAnteproyecto, item.CiteAnteproyecto, item.codigoPartida, item.nombreUnidadResponsable, item.cantidad, item.precio, item.MontoAnteproyecto,
                    item.mesEne, item.mesFeb, item.mesMar, item.mesAbr, item.mesMay, item.mesJun, item.mesJul, item.mesAgo, item.mesSep, item.mesOct, item.mesNov, item.mesDic,
                    item.observacion, item.detalle
                ]).draw()
            })

        })
}

function enviarDatos() {
    const input = document.getElementById("inputExcel");
    const formData = new FormData();
    if ($("#txtCiteCarpeta").val() === "") {
        toastr.warning("", "No Deje En Blanco el cite o Gestion")
        return false;
    }

    formData.append("ArchivoExcel", input.files[0]);
    formData.append("id", "123");
    formData.append("Cite", $("#txtCiteCarpeta").val());
    formData.append("Lugar", $("#textLugar").val());
    formData.append("Fecha", $("#txtFechaRegistro").val());

    fetch("/AnteproyectoPoa/EnviarDatos", {
        method: "POST",
        body: formData
    })
        .then(response => {
            swalWithBootstrapButtons.fire(
                'Lo Sentimos',
                'Su Anteproyecto No Fue Modificado',
                'error'
            )
            return response.json()
        })
        .then(responseJson => {
            swalWithBootstrapButtons.fire(
                'Lo Sentimos',
                'Su Anteproyecto No Fue Modificado',
                'error'
            )
             swSaltoSwal = true;
        })
    }
}