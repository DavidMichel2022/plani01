const formulario = document.getElementById('formulario');
const inputs = document.querySelectorAll('#formulario input');

const formulario01 = document.getElementById('formulariogrupo01');
const inputs01 = document.querySelectorAll('#formulariogrupo01 input');

const formulario02 = document.getElementById('formulariogrupo02');
const inputs02 = document.querySelectorAll('#formulariogrupo02 input');

const formulario03 = document.getElementById('formulariogrupo03');
const inputs03 = document.querySelectorAll('#formulariogrupo03 input');

const expresiones = {
	txtCiteCarpeta: /^[a-zA-Z0-9\-\/]{4,30}$/, // Letras, numeros, guion y guion_bajo
	txtActividadModal: /^\d{1,2}$/, // Solo Numeros
	txtMedidaModal: /^[ a-zA-Z0-9\-]{1,15}$/, // Letras, numeros, guion y guion_bajo
	txtCantidadModal: /^[0-9.]{1,18}$/, // Solo Numeros y Punto Decimal
	txtPrecioModal: /^[0-9.]{1,18}$/, // Solo Numeros y Punto Decimal
	txtTemporalidadModal: /^[a-zA-Z0-9\-]{1,20}$/, // Letras, numeros, guion y guion_bajo
}

const campos = {
	txtCiteCarpeta: false,
	txtActividadModal: false,
	txtMedidaModal: false,
	txtCantidadModal: false,
	txtPrecioModal: false,
	txtTemporalidadModal: false
}

const validarFormulario = (e) => {
	switch (e.target.name) {
		case "txtCiteCarpeta":
			validarCampo(expresiones.txtCiteCarpeta, e.target, 'txtCiteCarpeta');
			break;
		case "txtActividadModal":
			validarCampo(expresiones.txtActividadModal, e.target, 'txtActividadModal');
			break;
		case "txtMedidaModal":
			validarCampo(expresiones.txtMedidaModal, e.target, 'txtMedidaModal');
			break;
		case "txtCantidadModal":
			validarCampo(expresiones.txtCantidadModal, e.target, 'txtCantidadModal');
			break;
		case "txtPrecioModal":
			validarCampo(expresiones.txtPrecioModal, e.target, 'txtPrecioModal');
			break;
		case "txtTemporalidadModal":
			validarCampo(expresiones.txtTemporalidadModal, e.target, 'txtTemporalidadModal');
			break;
	}
}

const validarCampo = (expresion, input, campo) => {
	if (expresion.test(input.value)) {
		document.getElementById(`grupo__${campo}`).classList.remove('formulario__grupo-incorrecto');
		document.getElementById(`grupo__${campo}`).classList.add('formulario__grupo-correcto');
		document.querySelector(`#grupo__${campo} i`).classList.add('fa-check-circle');
		document.querySelector(`#grupo__${campo} i`).classList.remove('fa-times-circle');
		document.querySelector(`#grupo__${campo} .formulario__input-error`).classList.remove('formulario__input-error-activo');
		campos[campo] = true;
	} else {
		document.getElementById(`grupo__${campo}`).classList.add('formulario__grupo-incorrecto');
		document.getElementById(`grupo__${campo}`).classList.remove('formulario__grupo-correcto');
		document.querySelector(`#grupo__${campo} i`).classList.add('fa-times-circle');
		document.querySelector(`#grupo__${campo} i`).classList.remove('fa-check-circle');
		document.querySelector(`#grupo__${campo} .formulario__input-error`).classList.add('formulario__input-error-activo');
		campos[campo] = false;
	}
}

inputs.forEach((input) => {
	input.addEventListener('keyup', validarFormulario);
	input.addEventListener('blur', validarFormulario);
});
inputs01.forEach((input) => {
	input.addEventListener('keyup', validarFormulario);
	input.addEventListener('blur', validarFormulario);
});
inputs02.forEach((input) => {
	input.addEventListener('keyup', validarFormulario);
	input.addEventListener('blur', validarFormulario);
});
inputs03.forEach((input) => {
	input.addEventListener('keyup', validarFormulario);
	input.addEventListener('blur', validarFormulario);
});

formulario.addEventListener('submit', (e) => {
	e.preventDefault();

	if (campos.txtCiteCarpeta && campos.txtActividadModal && campos.txtMedidaModal && campos.txtCantidadModal && campos.txtPrecioModal && campos.txtTemporalidadModal) {
		formulario.reset();

		document.getElementById('formulario__mensaje-exito').classList.add('formulario__mensaje-exito-activo');
		setTimeout(() => {
			document.getElementById('formulario__mensaje-exito').classList.remove('formulario__mensaje-exito-activo');
		}, 5000);

		document.querySelectorAll('.formulario__grupo-correcto').forEach((icono) => {
			icono.classList.remove('formulario__grupo-correcto');
		});
	} else {
		document.getElementById('formulario__mensaje').classList.add('formulario__mensaje-activo');
	}
});