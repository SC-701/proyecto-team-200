function renderProductosBuscados(data) {
    let $contenedor = $("#productosInventario");
    $contenedor.empty();
    $contenedor.innerHTML = "<div class='col-12 text-center'><span>Cargando...</span></div>";
    const productos = Array.isArray(data) ? data : [data];


    if (productos.length === 0) {
        $contenedor.html(`
            <div class="col-12">
                <div class="alert alert-warning w-100 text-center" role="alert">
                    Productos no disponibles
                </div>
            </div>
        `);
        return;
    }



    $.each(productos, function (i, p) {
        let lowStockHtml = "";


        if (p.stock < 15) {
            lowStockHtml = `
            <div class="low-stock-indicator position-absolute top-0 end-0 m-2" title="Stock bajo">
                <span class="badge bg-danger rounded-circle p-2 shadow">
                    <i class="bi bi-exclamation"></i>
                </span>
            </div>
        `;
        }

        $contenedor.append(`
            <div class="col">

    <div class="card h-100 shadow-sm rounded-4 overflow-hidden position-relative">
        <img src="${p.imagenUrl}"
             alt="Imagen de ${p.nombre}"
             class="card-img-top producto-img"
             style="cursor:pointer;" />
        <div class="card-body d-flex flex-column">
            <h5 class="card-title mb-1 fw-semibold text-truncate" title="${p.nombre}">${p.nombre}</h5>
            <p class="card-text fw-bold text-primary mb-1">₡${p.precio.toLocaleString()}</p>
            <p class="text-muted mb-3 stock-info" >Stock:${p.stock}</p>
            <div class="mt-auto d-flex justify-content-between gap-2">
                <form method="post" asp-page-handler="EliminarProducto" asp-route-idProducto="${p.idProducto}" class="d-inline">
                    <button type="submit" class="btn btn-outline-danger rounded-circle p-2 shadow-sm" title="Eliminar">
                        <i class="bi bi-trash"></i>
                    </button>
                </form>
                <button data-bs-toggle="modal" data-bs-target="#modalFormularioEditar" data-url="/Productos/Inventario?handler=FormularioModalEditar&idProducto=${p.idProducto}" class="btn btn-outline-secondary rounded-circle">
                            <i class="bi bi-pencil"></i>
                </button>
            </div>
        </div>
            ${lowStockHtml}
    </div>
</div>
                    `);
    });

}

$(document).on("submit", "#BusquedaForm", function (e) {
    e.preventDefault();
    const $resultados = $("#resultadosBusqueda");
    $resultados.empty();

    const query = $(this).find("#buscador").val().trim();
    let $contenedor = $("#productosInventario");

    $.ajax({
        url: `https://localhost:7266/api/Productos/Busqueda/${encodeURIComponent(query)}`,
        method: "GET",
        dataType: "json",
        success: function (productos) {
            renderProductosBuscados(productos);
        },
        error: function (xhr, status) {
            $contenedor.html('<div class="p-2 text-danger">Error al buscar productos.</div>');
        }
    });

    
});


$(function () {
    let debounceTimer;
    let cache = {};
    let currentRequest = null;

    $("#buscador").on("input", function () {
        clearTimeout(debounceTimer);
        const query = $(this).val().trim();

        debounceTimer = setTimeout(function () {
            buscarProductos(query);
        }, 300);
    });

    function buscarProductos(query) {
        const $resultados = $("#resultadosBusqueda");

        if (query.length < 2) {
            $resultados.addClass("d-none").empty();
            return;
        }


        if (cache[query]) {
            renderResultados(cache[query]);
            return;
        }


        if (currentRequest) {
            currentRequest.abort();
        }

        currentRequest = $.ajax({
            url: `https://localhost:7266/api/Productos/Busqueda/${encodeURIComponent(query)}`,
            method: "GET",

            dataType: "json",
            success: function (productos) {
                cache[query] = productos;
                renderResultados(productos);
            },
            error: function (xhr, status) {
                if (status !== "abort") {
                    $resultados.html('<div class="p-2 text-danger">Error al buscar productos.</div>').removeClass("d-none");
                }
            }
        });
    }

    function renderResultados(productos) {
        const $resultados = $("#resultadosBusqueda");

        if (productos.length === 0) {
            $resultados.html('<div class="p-2 text-muted">No se encontraron productos.</div>').removeClass("d-none");
            return;
        }

        const html = productos.slice(0, 5).map((p, i) => `
        <div class="resultado-item" >
            <img src="${p.imagenUrl}" alt="${p.nombre}">
            <div class="resultado-nombre">${p.nombre}</div>
            <div class="resultado-precio text-primary">₡${p.precio.toLocaleString()}</div>
        </div>
    `);

        $resultados.html(html.join("")).removeClass("d-none");
        
        
    }
    
});


$(document).ready(function () {

    $(document).on('click', function (event) {

        if (!$(event.target).closest('#buscador, #resultadosBusqueda').length) {
            $('#resultadosBusqueda').hide();
        }
    });


    $('#buscador').on('focus', function () {
        $('#resultadosBusqueda').show();
    });
});