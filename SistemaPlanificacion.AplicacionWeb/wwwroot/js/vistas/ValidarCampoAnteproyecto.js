const formulario = document.getElementById('formulario');
const inputs = document.querySelectorAll('#formulario input');

const formulario01 = document.getElementById('formulariogrupo01');
const inputs01 = document.querySelectorAll('#formulariogrupo01 input');

const formulario02 = document.getElementById('formulariogrupo02');
const inputs02 = document.querySelectorAll('#formulariogrupo02 input');

const formulario03 = document.getElementById('formulariogrupo03');
const inputs03 = document.querySelectorAll('#formulariogrupo03 input');

const expresiones = {
	txtCiteCarpeta: /^[a-zA-Z0-9\-\/\ ]{4,30}$/, // Letras, numeros, guion y guion_bajo
	txtActividadModal: /^\d{1,2}$/, // Solo Numeros
	txtMedidaModal: /^[ a-zA-Z0-9\-]{1,15}$/, // Letras, numeros, guion y guion_bajo
	txtCantidadModal: /^[0-9.]{1,18}$/, // Solo Numeros y Punto Decimal
	txtPrecioModal: /^[0-9.]{1,18}$/, // Solo Numeros y Punto Decimal
	txtEneroModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtFebreroModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtMarzoModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtAbrilModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtMayoModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtJunioModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtJulioModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtAgostoModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtSeptiembreModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtOctubreModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtNoviembreModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
	txtDiciembreModal: /^[0-9.]{0,18}$/, // Solo Numeros y Punto Decimal
}

const campos = {
	txtCiteCarpeta: false,
	txtActividadModal: false,
	txtMedidaModal: false,
	txtCantidadModal: false,
	txtPrecioModal: false,
	txtEneroModal: false,
	txtFebreroModal: false,
	txtMarzoModal: false,
	txtAbrilModal: false,
	txtMayoModal: false,
	txtJunioModal: false,
	txtJulioModal: false,
	txtAgostoModal: false,
	txtSeptiembreModal: false,
	txtOctubreModal: false,
	txtNoviembreModal: false,
	txtDiciembreModal: false
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
		case "txtEneroModal":
			validarCampo(expresiones.txtEneroModal, e.target, 'txtEneroModal');
			break;
		case "txtFebreroModal":
			validarCampo(expresiones.txtFebreroModal, e.target, 'txtFebreroModal');
			break;
		case "txtMarzoModal":
			validarCampo(expresiones.txtMarzoModal, e.target, 'txtMarzoModal');
			break;
		case "txtAbrilModal":
			validarCampo(expresiones.txtAbrilModal, e.target, 'txtAbrilModal');
			break;
		case "txtMayoModal":
			validarCampo(expresiones.txtMayoModal, e.target, 'txtMayoModal');
			break;
		case "txtJunioModal":
			validarCampo(expresiones.txtJunioModal, e.target, 'txtJunioModal');
			break;
		case "txtJulioModal":
			validarCampo(expresiones.txtJulioModal, e.target, 'txtJulioModal');
			break;
		case "txtAgostoModal":
			validarCampo(expresiones.txtAgostoModal, e.target, 'txtAgostoModal');
			break;
		case "txtSeptiembreModal":
			validarCampo(expresiones.txtSeptiembreModal, e.target, 'txtSeptiembreModal');
			break;
		case "txtOctubreModal":
			validarCampo(expresiones.txtOctubreModal, e.target, 'txtOctubreModal');
			break;
		case "txtNoviembreModal":
			validarCampo(expresiones.txtNoviembreModal, e.target, 'txtNoviembreModal');
			break;
		case "txtDiciembreModal":
			validarCampo(expresiones.txtDiciembreModal, e.target, 'txtDiciembreModal');
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

	if (campos.txtCiteCarpeta && campos.txtActividadModal && campos.txtMedidaModal && campos.txtCantidadModal && campos.txtPrecioModal && campos.txtEneroModal && campos.txtFebreroModal &&
		campos.txtMarzoModal && campos.txtAbrilModal && campos.txtMayoModal && campos.txtJunioModal && campos.txtJulioModal && campos.txtAgostoModal && campos.txtSeptiembreModal &&
		campos.txtOctubreModal && campos.txtNoviembreModal && campos.txtDiciembreModal) {
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