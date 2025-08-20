(function () {
    /**
     * Retrasa la ejecución de una función hasta que haya pasado un cierto tiempo sin que se vuelva a llamar.
     * Útil para evitar llamadas excesivas a la API, por ejemplo, al escribir en un campo de texto.
     */
    function debounce(func, delay) {
        let timer;
        return function (...args) {
            clearTimeout(timer);
            timer = setTimeout(() => func.apply(this, args), delay);
        };
    }

    /**
     * Recalcula el total de cada producto y el total general del carrito de forma local.
     * No realiza llamadas a la API.
     */
    function actualizarTotalesLocal() {
        let totalGeneral = 0;

        document.querySelectorAll('.cart-item').forEach(productoDiv => {
            const input = productoDiv.querySelector('.quantity-input');
            const cantidad = parseInt(input?.value, 10) || 0;
            const precioUnit = parseFloat(productoDiv.dataset.precioUnit || 0);
            const itemTotalElem = productoDiv.querySelector('.item-total');

            const totalLinea = cantidad * precioUnit;
            if (itemTotalElem) itemTotalElem.textContent = `₡${totalLinea.toFixed(2)}`;

            totalGeneral += totalLinea;
        });

        const totalElem = document.getElementById('total');
        if (totalElem) totalElem.textContent = `₡${totalGeneral.toFixed(2)}`;
    }

    /**
     * Sincroniza el stock de los productos con la información más reciente del servidor.
     * Actualiza el atributo 'max' de los campos de cantidad.
     */
    async function refrescarStockGlobal() {
        try {
            const resp = await fetch('/Carrito/Carrito?handler=Refrescar');
            if (!resp.ok) {
                console.error('Error al obtener el stock actualizado.');
                return null;
            }
            const data = await resp.json();

            if (!data || !data.success || !Array.isArray(data.productos)) {
                console.error('La respuesta del stock no es válida.');
                return null;
            }

            document.querySelectorAll('.cart-item').forEach(productoDiv => {
                const input = productoDiv.querySelector('.quantity-input');
                if (!input) return;

                const carritoProductoId = productoDiv.dataset.carritoProductoId || productoDiv.id.replace('producto-', '');
                const cantidadAnterior = Number(input.value) || 0;

                const productoApi = data.productos.find(p => {
                    const cId = carritoProductoId.toString();
                    return p.id?.toString() === cId || p.carritoProductoId?.toString() === cId;
                });

                if (!productoApi) return;

                const stockApi = Number(productoApi.stock ?? productoApi.stockDisponible ?? productoApi.StockDisponible ?? 0);
                const nuevoMax = Math.max(0, stockApi + cantidadAnterior);
                input.max = nuevoMax;

                // Ajusta el valor del input si la cantidad actual supera el nuevo máximo
                if (cantidadAnterior > nuevoMax) {
                    input.value = nuevoMax;
                }
            });

            actualizarTotalesLocal();
            return data;
        } catch (err) {
            console.error('Error refrescando stock global:', err);
            return null;
        }
    }

    /**
     * Actualiza la cantidad de un producto en el carrito a través de la API, con un retraso (debounce).
     */
    const actualizarCantidadAPI = debounce(async (carritoProductoId, cantidadNueva, cantidadAnterior) => {
        try {
            const productoDiv = document.getElementById(`producto-${carritoProductoId}`);
            if (!productoDiv) return;

            const input = productoDiv.querySelector('.quantity-input');
            if (!input) return;

            const response = await fetch(`${window.endpoints.actualizarProducto}${carritoProductoId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    cantidad: cantidadNueva,
                    productosId: productoDiv.dataset.productosId
                })
            });

            if (!response.ok) {
                let errorMsg = 'Error al actualizar la cantidad';
                try {
                    const errData = await response.json();
                    if (errData && errData.mensaje) errorMsg = errData.mensaje;
                } catch (e) { /* ignore */ }

                const data = await refrescarStockGlobal();
                if (data && data.success) {
                    const productoApi = data.productos.find(p => {
                        const cId = carritoProductoId.toString();
                        return p.id?.toString() === cId || p.carritoProductoId?.toString() === cId;
                    });

                    if (productoApi) {
                        const stockApi = Number(productoApi.stock ?? productoApi.stockDisponible ?? productoApi.StockDisponible ?? 0);
                        const nuevoMax = Math.max(0, stockApi + cantidadAnterior);
                        input.max = nuevoMax;
                        input.value = Math.min(cantidadAnterior, nuevoMax);
                        input.setCustomValidity(errorMsg);
                        input.reportValidity();
                    } else {
                        input.value = cantidadAnterior;
                        input.setCustomValidity(errorMsg);
                        input.reportValidity();
                    }

                    actualizarTotalesLocal();
                }

                return;
            }

            if (input) input.setCustomValidity('');
        } catch (err) {
            console.error('Error actualizarCantidadAPI:', err);
        }
    }, 300);

    /**
     * Valida y actualiza la cantidad de un producto, ya sea eliminándolo si es 0 o llamando a la API si el valor es válido.
     */
    function actualizarCantidad(carritoProductoId, valorInput) {
        const productoDiv = document.getElementById(`producto-${carritoProductoId}`);
        if (!productoDiv) return;

        const input = productoDiv.querySelector('.quantity-input');
        if (!input) return;

        const cantidadAnterior = Number(input.value) || 0;
        let nuevaCantidad = Number(valorInput) || 0;
        const max = Number(input.max) || 0;

        if (nuevaCantidad > max) {
            nuevaCantidad = max;
            input.value = max;
            input.setCustomValidity(`La cantidad no puede ser mayor a ${max}`);
            input.reportValidity();
        } else {
            input.setCustomValidity('');
        }

        if (nuevaCantidad <= 0) {
            if (typeof window.eliminarProducto === 'function') {
                window.eliminarProducto(carritoProductoId);
            } else {
                productoDiv.remove();
                actualizarTotalesLocal();
            }
            return;
        }

        input.value = nuevaCantidad;
        actualizarTotalesLocal();

        actualizarCantidadAPI(carritoProductoId, nuevaCantidad, cantidadAnterior);
    }

    /**
     * Aumenta o disminuye la cantidad de un producto en el carrito.
     */
    function cambiarCantidad(carritoProductoId, cambio) {
        const productoDiv = document.getElementById(`producto-${carritoProductoId}`);
        if (!productoDiv) return;
        const input = productoDiv.querySelector('.quantity-input');
        if (!input) return;
        const nuevaCantidad = (Number(input.value) || 0) + cambio;
        actualizarCantidad(carritoProductoId, nuevaCantidad);
    }

    /**
     * Actualiza el conteo de productos y la vista de "carrito vacío".
     */
    function actualizarItemCountYVista() {
        const items = document.querySelectorAll('.cart-item');
        const countElem = document.getElementById('item-count');
        if (countElem) countElem.textContent = items.length;

        const emptyCartElem = document.getElementById('empty-cart');
        const cartWithItems = document.getElementById('cart-with-items');
        if (items.length === 0) {
            if (emptyCartElem) emptyCartElem.classList.remove('d-none');
            if (cartWithItems) cartWithItems.classList.add('d-none');
        } else {
            if (emptyCartElem) emptyCartElem.classList.add('d-none');
            if (cartWithItems) cartWithItems.classList.remove('d-none');
        }
    }

    /**
     * Elimina un producto del carrito, tanto en el servidor como en la interfaz de usuario.
     */
    async function eliminarProducto(carritoProductoId) {
        const productoDiv = document.getElementById(`producto-${carritoProductoId}`);
        if (!productoDiv) return;

        const controles = productoDiv.querySelectorAll('button, input');
        controles.forEach(c => c.disabled = true);

        try {
            const resp = await fetch(`${window.endpoints.eliminarProducto}${carritoProductoId}`, {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json' }
            });

            if (!resp.ok) {
                console.error('Error al eliminar el producto del carrito.');
                controles.forEach(c => c.disabled = false);
                return;
            }

            productoDiv.remove();
            actualizarItemCountYVista();
            if (typeof window.actualizarTotalesLocal === 'function') {
                window.actualizarTotalesLocal();
            }

        } catch (err) {
            console.error('Error en la solicitud de eliminación:', err);
            const prodDiv = document.getElementById(`producto-${carritoProductoId}`);
            if (prodDiv) {
                const botones = prodDiv.querySelectorAll('button, input');
                botones.forEach(b => b.disabled = false);
            }
        }
    }

    /**
     * Inicializa las validaciones de los campos de cantidad.
     */
    function initInputValidation() {
        document.querySelectorAll('.quantity-input').forEach(input => {
            input.addEventListener('input', () => {
                const max = Number(input.max) || 0;
                const val = Number(input.value) || 0;
                if (val > max) {
                    input.setCustomValidity(`La cantidad no puede ser mayor a ${max}`);
                } else {
                    input.setCustomValidity('');
                }
                input.reportValidity();
            });
        });
    }

    // Configura las funciones para que estén disponibles globalmente
    document.addEventListener('DOMContentLoaded', () => {
        initInputValidation();
        window.actualizarTotalesLocal = actualizarTotalesLocal;
        window.refrescarStock = refrescarStockGlobal;
        window.actualizarCantidad = actualizarCantidad;
        window.cambiarCantidad = cambiarCantidad;
        window.eliminarProducto = eliminarProducto;
        window.refrescarStockGlobal = refrescarStockGlobal;
    });
})();