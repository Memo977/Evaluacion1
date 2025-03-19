// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Código para manejar las alertas

// Versión moderna - simplemente usar la forma abreviada
$(function () {
    // Inicializar todos los componentes dismissible de Bootstrap
    $('.alert').alert();

    // Manejador de eventos para el botón de cierre personalizado
    $('.alert .close').on('click', function () {
        $(this).closest('.alert').fadeOut('fast', function () {
            $(this).remove(); // Eliminar completamente la alerta del DOM
        });
        return false; // Prevenir el comportamiento default
    });

    // Auto-cerrado después de 5 segundos para alertas de éxito
    setTimeout(function () {
        $('.alert-success').fadeOut('slow', function () {
            $(this).remove();
        });
    }, 5000);

    // Agregar clases active a los elementos de navegación según la URL actual
    var currentUrl = window.location.pathname;

    if (currentUrl.includes('/Estudiantes')) {
        $('.nav-item a[href$="Estudiantes"]').addClass('active');
    } else if (currentUrl.includes('/Matriculas')) {
        $('.nav-item a[href$="Matriculas"]').addClass('active');
    } else if (currentUrl === '/' || currentUrl.includes('/Home')) {
        $('.nav-item a[href$="Home"]').addClass('active');
    }
});

// Función para confirmar eliminación usando SweetAlert2
function confirmDelete(formId, message) {
    $('#' + formId).on('submit', function (e) {
        e.preventDefault();

        Swal.fire({
            title: '¿Está seguro?',
            text: message || "Esta acción no se puede revertir",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#dc3545',
            cancelButtonColor: '#6c757d',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                this.submit();
            }
        });
    });
}