<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
document.addEventListener("DOMContentLoaded", function () {
    if (window.crearProductoExito === true) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'El producto se ha creado correctamente',
            timer: 2000,
            showConfirmButton: false
        });
    } else if (window.crearProductoExito === false) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'No se pudo crear el producto',
            showConfirmButton: true
        });
    }
});