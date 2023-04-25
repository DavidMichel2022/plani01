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
                $("#tbData tbody").append(
                    $("<tr>").append(                        
                        $("<td>").text(item.numero),
                        $("<td>").text(item.partida),
                        $("<td>").text(item.detalle),
                        $("<td>").text(item.unidad),
                        $("<td>").text(item.cantidad),
                        $("<td>").text(item.precioUnitario),
                        $("<td>").text(item.precioTotal)
                    )
                )
            })

        })

  
}