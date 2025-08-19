function debounce(func, delay) {
    let timer;
    return function (...args) {
        clearTimeout(timer);
        timer = setTimeout(() => func.apply(this, args), delay);
    };
}

function actualizarTotalesLocal() {
    let totalGeneral = 0;

    document.querySelectorAll('.cart-item').forEach(productoDiv => {
        const input = productoDiv.querySelector('.quantity-input');
        const cantidad = parseInt(input.value, 10) || 0;
        const precioUnit = parseFloat(productoDiv.dataset.precioUnit) || 0;
        const itemTotalElem = productoDiv.querySelector('.item-total');

        const totalLinea = cantidad * precioUnit;
        if (itemTotalElem) itemTotalElem.textContent = `₡${totalLinea.toFixed(2)}`;

        totalGeneral += totalLinea;
    });

    const totalElem = document.getElementById('total');
    if (totalElem) totalElem.textContent = `₡${totalGeneral.toFixed(2)}`;
}

async function refrescarStock(carritoProductoId, input, cantidadAnterior) {
    try {
        const response = await fetch(`/Carrito/Carrito?handler=Refrescar`);
        const data = await response.json();

      

        const productoActualizado = data.productos.find(p => p.id === carritoProductoId);
        if (!productoActualizado) return 0;

        const nuevoMax = (parseInt(productoActualizado.stock, 10) || 0) + (parseInt(cantidadAnterior, 10) || 0);

        input.max = nuevoMax;
        if (parseInt(input.value, 10) > nuevoMax) {
            input.value = nuevoMax;
        }

        input.setCustomValidity('');
        actualizarTotalesLocal();
        return nuevoMax;
    } catch (err) {
        console.error("Error al refrescar stock:", err);
        return 0;
    }
}

const actualizarCantidadAPI = debounce(async (carritoProductoId, cantidadNueva, cantidadAnterior) => {
    try {
        const productoDiv = document.getElementById(`producto-${carritoProductoId}`);
        if (!productoDiv) return;

        const input = productoDiv.querySelector('.quantity-input');

        const response = await fetch(`${window.endpoints.actualizarProducto}${carritoProductoId}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                cantidad: cantidadNueva,
                productosId: productoDiv.dataset.productosId
            })
        });

        if (!response.ok) {
            let errorMsg = "Error al actualizar la cantidad";
            try {
                const errorData = await response.json();
                if (errorData.mensaje) errorMsg = errorData.mensaje;
            } catch (e) { }


            await refrescarStock(carritoProductoId, input, cantidadAnterior);

            return;
        }

        if (input) input.setCustomValidity('');

    } catch (err) {
        console.error(err);
    }
}, 300);

function actualizarCantidad(carritoProductoId, valorInput) {
    const productoDiv = document.getElementById(`producto-${carritoProductoId}`);
    if (!productoDiv) return;

    const input = productoDiv.querySelector('.quantity-input');
    if (!input) return;
    const cantidadAnterior = parseInt(input.value, 10) || 0;

    let nuevaCantidad = parseInt(valorInput, 10) || 0;
    const max = parseInt(input.max, 10) || 0;

    // Validar máximo (local)
    if (nuevaCantidad > max) {
        nuevaCantidad = max;
        input.value = max;

        input.setCustomValidity(`La cantidad no puede ser mayor a ${max}`);
        input.reportValidity();
    } else {
        input.setCustomValidity('');
    }

    if (nuevaCantidad <= 0) {
        eliminarProducto(carritoProductoId);
        actualizarTotalesLocal();
        return;
    }

    input.value = nuevaCantidad;
    actualizarTotalesLocal();
    actualizarCantidadAPI(carritoProductoId, nuevaCantidad, cantidadAnterior);
}

window.actualizarCantidad = actualizarCantidad;

function cambiarCantidad(carritoProductoId, cambio) {
    const productoDiv = document.getElementById(`producto-${carritoProductoId}`);
    if (!productoDiv) return;

    const input = productoDiv.querySelector('.quantity-input');
    if (!input) return;

    const nuevaCantidad = (parseInt(input.value, 10) || 0) + cambio;
    actualizarCantidad(carritoProductoId, nuevaCantidad);
}

window.cambiarCantidad = cambiarCantidad;


document.querySelectorAll('.quantity-input').forEach(input => {
    input.addEventListener('input', () => {
        const max = parseInt(input.max, 10) || 0;
        const valor = parseInt(input.value, 10) || 0;

        if (valor > max) {
            input.setCustomValidity(`La cantidad no puede ser mayor a ${max}`);
        } else {
            input.setCustomValidity('');
        }
        input.reportValidity();
    });
});