(function () {

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

            

            productoDiv.remove();

            actualizarItemCountYVista();
            const carritoContainer = document.querySelector('.card-body[data-carrito-id]');
            if (!carritoContainer) {
                if (typeof window.actualizarTotalesLocal === 'function') window.actualizarTotalesLocal();
                return;
            }

            const carritoId = carritoContainer.dataset.carritoId;
            const respCarrito = await fetch(`${window.endpoints.obtenerCarritoPorId}${carritoId}`);
            if (!respCarrito.ok) {
                if (typeof window.actualizarTotalesLocal === 'function') window.actualizarTotalesLocal();
                return;
            }

            const carrito = await respCarrito.json();

            const totalElem = document.getElementById('total');
            if (totalElem && carrito.total != null) {
                totalElem.textContent = `₡${parseFloat(carrito.total).toFixed(2)}`;
            } else if (typeof window.actualizarTotalesLocal === 'function') {
                window.actualizarTotalesLocal();
            }

            if (Array.isArray(carrito.productos)) {
                carrito.productos.forEach(p => {
                    const id = (p.carritoProductoId || p.id || p.carritoProductoId)?.toString();
                    const row = document.getElementById(`producto-${id}`);
                    if (!row) return;

                    const input = row.querySelector('.quantity-input');
                    const itemTotalElem = row.querySelector('.item-total');

                    const stock = Number(p.stock ?? p.stockDisponible ?? p.StockDisponible ?? 0);
                    const cantidad = Number(p.cantidad ?? p.Cantidad ?? input?.value ?? 0);
                    const totalLinea = (p.totalLinea ?? p.TotalLinea) ?? (cantidad * Number(row.dataset.precioUnit || 0));

                    if (input) {
                        input.max = Math.max(0, stock + cantidad);
                        input.value = cantidad;
                        input.setCustomValidity('');
                    }
                    if (itemTotalElem) itemTotalElem.textContent = `₡${Number(totalLinea).toFixed(2)}`;
                });
            }

            if (!carrito.productos || carrito.productos.length === 0) {
                document.getElementById('cart-with-items')?.classList.add('d-none');
                document.getElementById('empty-cart')?.classList.remove('d-none');
            }

        } catch (err) {
            console.error('Error eliminando producto:', err);
            const prodDiv = document.getElementById(`producto-${carritoProductoId}`);
            if (prodDiv) {
                const botones = prodDiv.querySelectorAll('button, input');
                botones.forEach(b => b.disabled = false);
            }
        }
    }
    window.eliminarProducto = eliminarProducto;
})();