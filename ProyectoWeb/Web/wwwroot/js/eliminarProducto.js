async function eliminarProducto(carritoProductoId) {
    const resp = await fetch(`${window.endpoints.eliminarProducto}${carritoProductoId}`, { method: 'DELETE' });
    if (!resp.ok) {
        return;
    }

    const prodDiv = document.getElementById(`producto-${carritoProductoId}`);
    if (prodDiv) prodDiv.remove();
    const carritoContainer = document.querySelector('.card-body[data-carrito-id]');
    if (!carritoContainer) return;
    const carritoId = carritoContainer.dataset.carritoId;
    const respCarrito = await fetch(`${window.endpoints.obtenerCarritoPorId}${carritoId}`);
    if (!respCarrito.ok) return;
    const carrito = await respCarrito.json();

    const totalElem = document.getElementById('total');
    if (totalElem) totalElem.textContent = `₡${parseFloat(carrito.total).toFixed(2)}`;

    if (!carrito.productos || carrito.productos.length === 0) {
        document.getElementById('cart-with-items')?.classList.add('d-none');
        document.getElementById('empty-cart')?.classList.remove('d-none');
    }
}

window.eliminarProducto = eliminarProducto;