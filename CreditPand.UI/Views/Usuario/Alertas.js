
//Alerta para el registro de un usuario
$(document).ready(function () {
$('#btn-registro').click(function () {
    Swal.fire({
        title: '¿Enviar Formulario?',
        text: '',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si',
        cancelButtonText: 'No',
    });
});
},

//Alerta para el envío de una solicitud para una tarjeta
$(document).ready(function (){
$(form-submit).click(function () {
    Swal.fire("Envío Exitoso");
});
},


    $(document).ready(function () {
        $('#form-submit').click(function () {
            Swal.fire("Envío Exitoso");
        });
    },


//Alerta para login, si la contraseña o el usuario son incorrectos
    <script>

        @if(ViewBag.ErrorMensaje != null){
            Swal.fire(
                '@ViewBag.ErrorMensaje',
                '',
                'success')
	}

    </script>


$(document).ready(function () {
$('#btn-log').click(function () {
    Swal.fire("Usuario o contraseña incorrecta");
});
},


//Alerta para DELETE en el mantenimiento de Usuarios y Tarjetas
$("#item.Username").click(function () {
    Swal.fire("Registro Eliminado");
}),


//Alerta para UPDATE en el mantenimiento de Usuarios y Tarjetas
$("#item.Username").click(function () {
    Swal.fire("Registro Eliminado");
}),    
    
