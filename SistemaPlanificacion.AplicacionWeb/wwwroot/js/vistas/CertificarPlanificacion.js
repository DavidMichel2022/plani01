var idFila = 1;
var nro = 0;
var montoTotal = 0;



function clear_data_solicitud() {
    $('#textPartida').val(null);
    $('#textDetalle').val(null);
    $('#textMedida').val(null);
    $('#textPrecio').val(null);
    $('#textPresupuesto').val(null);
}

function guardarCertificacionPOA() {
    var montoTotal = 0;
    var count = $('#tbdatos').children().length;
    var vmDetalle = [];
    var montoTotal = 0;

    if ($('#textCodigoCertificacion').val() != '') {
        var tbl = $("#tbdatos").children();
        var contador = 0;
        var montosVacios = false;



        $("#tbdatos").children().each(function () {
            console.log("Fila :" + contador + "columna 4: ");
            var txtDetalle = $(this)[0].cells[0].firstChild.data;
            console.log("IdDetalle: " + txtDetalle);
            var txtMontoPresupuesto = $("#txtMontoPresupuesto_" + txtDetalle).val();
            console.log("Monto: " + txtMontoPresupuesto);
            if (txtMontoPresupuesto == "")
                montosVacios = true;
            let item = {
                //  idDetalle:214,
                idDetalle: txtDetalle,
                montoPresupuesto: txtMontoPresupuesto
            }
            //console.log(item);
            vmDetalle.push(item);
            //console.log(vmDetalle);
            montoTotal = parseFloat(montoTotal) + parseFloat(txtMontoPresupuesto);
        });

        if (montosVacios) {
            swal("Lo Sentimos", "Ingrese los montos Presupuestarios", "warning")
        } else {
            //--------------------------------------           

            const carpetaRequerimiento = {
                IdCarpeta: $("#txtIdCarpeta").val(),
                Codigo: $("#textCodigoCertificacion").val(),
                TotalCertificado: montoTotal,
                EstadoCertificacion: "A",
                IdUsuario: 1,
                DetalleCertificacionPoas: vmDetalle
            }
            console.log(carpetaRequerimiento);
            $("#btnTerminarCarpeta").LoadingOverlay("show");

            fetch("/CertificacionPoa/Crear", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(carpetaRequerimiento)
            }).then(response => {
                console.log("----response: ");
                //  console.log(response);
                $("#btnTerminarCarpeta").LoadingOverlay("hide");
                if (response.ok) {
                    console.log("-Carpeta Registrada-");
                    // PartidasParaCarpeta = [];
                    // mostrarItemDetalle();
                    $("#textCodigoCertificacion").val("");
                    swal("Registrado!!", 'Certifiacion Poa Registrada', "success")

                } else {
                    swal("No se puedo Registrar!!", 'Error al intentar Registrar la Certifiacion Poa', "warning")
                }

                return response.ok ? response.json() : Promise.reject(response);
            }).then(responseJson => {
                /* console.log("--respuesta Json-");
                 console.log(responseJson);
                 if (responseJson.estado) {
                     swal("Registrado!", `Numero ID Carpeta : ${responseJason.objeto.idCarpeta}`, "success")
                 } else {
                     swal("Lo sentimos!", 'No se pudo registrar la Carpeta', "error")
                 }*/
            })
            //--------------------------------------

        }

    } else {
        swal("Lo Sentimos", "Ingrese Codigo de Certificación", "warning")
    }


}

