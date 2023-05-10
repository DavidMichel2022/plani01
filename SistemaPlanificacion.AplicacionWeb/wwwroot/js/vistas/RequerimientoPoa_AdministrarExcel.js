
$(document).ready(function () {
    tablaData = $('#tbData').DataTable();
    $.ajax({
        url: '/Planificacion/ObtenerHora',
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

    fetch("/RequerimientoPoa/MostrarDatos", {
        method: "POST",
        body: formData
    })
        .then(response => {
            console.log(response);
            return response.json()
        })
        .then(responseJson => {
            console.log(responseJson);
            responseJson.forEach((item) => {               
                $('#tbData').DataTable().row.add([
                    item.idRequerimientoPoa, '2', '3', '4', '5', '6', '7'
                ]).draw()
            })

        })  
}

function enviarDatos() {

    const input = document.getElementById("inputExcel");
    const formData = new FormData();
    if ($("#txtCiteCarpeta").val()=== "") {
        toastr.warning("", "No Deje En Blanco el cite o Gestion")
        return false;
    }

    formData.append("ArchivoExcel", input.files[0]);
    formData.append("id", "123");
    formData.append("Cite", $("#txtCiteCarpeta").val());
    formData.append("Lugar", $("#textLugar").val());
    formData.append("Fecha", $("#txtFechaRegistro").val());

    fetch("/RequerimientoPoa/EnviarDatos", {
        method: "POST",
        body: formData
    })
        .then(response => {
            console.log(response);
            return response.json()
        })
        .then(responseJson => {
            console.log(responseJson);
            responseJson.forEach((item) => {
                console.log(item);
                alert("Datos enviado y registrados");
               /* $("#tbData tbody").append(
                    $("<tr>").append(
                        $("<td>").text(item.numero),
                        $("<td>").text(item.partida),
                        $("<td>").text(item.detalle),
                        $("<td>").text(item.unidad),
                        $("<td>").text(item.cantidad),
                        $("<td>").text(item.precioUnitario),
                        $("<td>").text(item.precioTotal)
                    )
                )*/
            })

        })
}