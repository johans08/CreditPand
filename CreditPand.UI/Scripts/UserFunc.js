
//Alerta para el registro de un usuario
$(document).ready(function () {
    $('#btn-registro').click(function () {
        Swal.fire("Registroso Exitoso");
    });
});

//Alerta para el envío de una solicitud para una tarjeta
$(document).ready(function () {
    $('#btn-Solicitud').click(function () {
        Swal.fire("Envío Exitoso");
    });
});

//Alerta al actualizar un interes
$(document).ready(function () {
    $('#btn-Config').click(function () {
        Swal.fire("Interés actualizado");
    });
});

//Alerta al actualizar un registro de TARJETA/CLIENTE
$(document).ready(function () {
    $('#btn-Update').click(function () {
        Swal.fire("Registro actualizado");
    });
});


//Alerta al eliminar un registro de TARJETA/CLIENTE
$(document).ready(function () {
    $('#btn-Delete').click(function () {
        Swal.fire("Registro eliminado");
    });
});


//Alerta para el Login
$(document).ready(function () {
    $('#btn-Login').click(function () {
        Swal.fire("Datos incorrectos, intente denuevo");
    });
});








/*  

                //Paginación para mantenimientos de clientes y tarjetas
                $(document).ready(function () {
                    $('#tableClients').pageMe({
                        pagerSelector: '#Mantenimientos',
                        showPrevNext: true,
                        hidePageNumbers: false,
                        perPage: 5
                    });
                });
*/