document.addEventListener('DOMContentLoaded', async () => {
    const sidebar = document.querySelector('.sidebar-nav nav');

    const rPadres = await fetch('/Productos/Index?handler=ObtenerCategoriasPadres');
    const padres = await rPadres.json();
    console.log("Categorías padre:", padres);

    padres.forEach(padre => {
        const padreLink = document.createElement('a');
        padreLink.className = 'nav-link d-flex align-items-center p-3 rounded-3 mb-1 text-decoration-none text-dark';
        padreLink.href = '#';
        padreLink.dataset.padreId = padre.categoriasId;

        const icon = document.createElement('i');
        icon.className = 'fas fa-circle me-2';
        padreLink.appendChild(icon);

        const spanText = document.createElement('span');
        spanText.className = 'nav-text';
        spanText.textContent = padre.nombre;
        padreLink.appendChild(spanText);
        const hijasContainer = document.createElement('div');
        hijasContainer.className = 'hijas-container mt-1 ps-3';
        hijasContainer.style.display = 'none';

        padreLink.addEventListener('click', async (e) => {
            e.preventDefault();

            document.querySelectorAll('.hijas-container').forEach(div => {
                if (div !== hijasContainer) div.style.display = 'none';
            });

            if (hijasContainer.style.display === 'none') {
                if (!hijasContainer.hasChildNodes()) {
                    const rHijas = await fetch(`/Productos/Index?handler=ObtenerCategoriasHijas&id=${padre.categoriasId}`);
                    const respuesta = await rHijas.json();
                    console.log(`Hijas de ${padre.nombre}:`, respuesta);

                    if (respuesta.tieneHijas) {
                        const select = document.createElement('select');
                        select.className = 'form-select form-select-sm mb-2';
                        select.innerHTML = `<option value="" disabled selected>—</option>`;
                        select.className = 'form-select form-select-sm mb-2 shadow-sm border-0 bg-light';

                        respuesta.categorias.forEach(hija => {
                            const option = document.createElement('option');
                            option.value = hija.categoriasId;
                            option.textContent = hija.nombre;
                            select.appendChild(option);
                        });

                        select.addEventListener('change', (e) => {
                            const subId = e.target.value;
                            if (subId) {
                                window.location.href = `/Productos/ProductosxCategoria?categoriaId=${subId}`;
                            }
                        });

                        hijasContainer.appendChild(select);
                    } else {
                        console.log(padre.categoriasId);
                        window.location.href = `/Productos/ProductosxCategoria?categoriaId=${padre.categoriasId}`;
                    }
                }
                hijasContainer.style.display = 'block';
            } else {
                hijasContainer.style.display = 'none';
            }
        });

        sidebar.appendChild(padreLink);
        sidebar.appendChild(hijasContainer);
    });
    sidebar.addEventListener('mouseleave', () => {
        document.querySelectorAll('.hijas-container').forEach(div => {
            div.style.display = 'none';
        });
    });

});