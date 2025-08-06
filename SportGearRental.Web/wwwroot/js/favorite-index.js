
    document.addEventListener('DOMContentLoaded', function() {
        const removeModal = new bootstrap.Modal(document.getElementById('removeModal'));
    const removeBtns = document.querySelectorAll('.remove-btn');
    const viewBtns = document.querySelectorAll('.view-btn');
    const favoritesGrid = document.getElementById('favoritesGrid');
    const sortSelect = document.querySelector('.sort-select');

        removeBtns.forEach(btn => {
        btn.addEventListener('click', function () {
            const gearName = this.getAttribute('data-gear-name');
            const form = this.closest('form');

            document.getElementById('gearNameModal').textContent = gearName;
            document.getElementById('removeForm').action = form.action;
            document.getElementById('removeForm').innerHTML = form.innerHTML +
                '<input type="hidden" name="gearId" value="' + form.querySelector('input[name="gearId"]').value + '">';

            removeModal.show();
        });
        });

        viewBtns.forEach(btn => {
        btn.addEventListener('click', function () {
            viewBtns.forEach(b => b.classList.remove('active'));
            this.classList.add('active');

            const view = this.getAttribute('data-view');
            if (view === 'list') {
                favoritesGrid.classList.add('list-view');
            } else {
                favoritesGrid.classList.remove('list-view');
            }
        });
        });

    if (sortSelect) {
        sortSelect.addEventListener('change', function () {
            const sortBy = this.value;
            const cards = Array.from(document.querySelectorAll('.favorite-card'));

            cards.sort((a, b) => {
                switch (sortBy) {
                    case 'name':
                        return a.getAttribute('data-name').localeCompare(b.getAttribute('data-name'));
                    case 'price-low':
                        return parseFloat(a.getAttribute('data-price') || 0) - parseFloat(b.getAttribute('data-price') || 0);
                    case 'price-high':
                        return parseFloat(b.getAttribute('data-price') || 0) - parseFloat(a.getAttribute('data-price') || 0);
                    case 'recent':
                    default:
                        return 0; // Keep original order for recent
                }
            });

            cards.forEach(card => favoritesGrid.appendChild(card));
        });
        }
    });