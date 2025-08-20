document.addEventListener('DOMContentLoaded', async () => {
    const navUl = document.getElementById('navCategorias');
    if (!navUl) return;

    navUl.innerHTML = '';

    try {

        const rPadres = await fetch('/Productos/Index?handler=ObtenerCategoriasPadres');
        if (!rPadres.ok) throw new Error('Error padres ' + rPadres.status);
        const padres = await rPadres.json();


        const checks = await Promise.all(
            padres.map(async (padre) => {
                const rH = await fetch(`/Productos/Index?handler=ObtenerCategoriasHijas&id=${padre.categoriasId}`);
                if (!rH.ok) return null;
                const data = await rH.json();
                if (data && data.tieneHijas && Array.isArray(data.categorias) && data.categorias.length > 0) {
                    return { padre, hijas: data.categorias };
                }
                return null;
            })
        );


        const conHijas = checks.filter(x => !!x).slice(0, 4);


        conHijas.forEach(({ padre, hijas }) => {
            const li = document.createElement('li');
            li.className = 'nav-item dropdown';

            const a = document.createElement('a');
            a.className = 'nav-link dropdown-toggle text-white';
            a.href = '#';
            a.id = `cat_${padre.categoriasId}`;
            a.setAttribute('role', 'button');
            a.setAttribute('data-bs-toggle', 'dropdown');
            a.setAttribute('aria-expanded', 'false');

            a.innerHTML = `<i class="fas fa-tags me-1 text-white"></i> ${padre.nombre}`;

            const ulDrop = document.createElement('ul');
            ulDrop.className = 'dropdown-menu';
            ulDrop.setAttribute('aria-labelledby', a.id);

            hijas.forEach(hija => {
                const liH = document.createElement('li');
                const aH = document.createElement('a');
                aH.className = 'dropdown-item';
                aH.href = `/Productos/ProductosxCategoria?categoriaId=${hija.categoriasId}`;
                aH.textContent = hija.nombre;
                liH.appendChild(aH);
                ulDrop.appendChild(liH);
            });

            li.appendChild(a);
            li.appendChild(ulDrop);
            navUl.appendChild(li);
        });


        if (navUl.children.length === 0) {
            navUl.style.display = 'none';
            console.warn('No hay categorías con hijas para mostrar.');
        }
    } catch (err) {
        console.error('Error construyendo menú de categorías:', err);
    }
});