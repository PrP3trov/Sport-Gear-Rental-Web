document.addEventListener('DOMContentLoaded', function () {
    const collapsibles = document.querySelectorAll('.collapsible-header');

    collapsibles.forEach(button => {
        button.addEventListener('click', function () {
            const section = this.closest('.collapsible-section');

            section.classList.toggle('active');

            // Update aria-expanded attribute
            const isExpanded = section.classList.contains('active');
            this.setAttribute('aria-expanded', isExpanded);

            if (isExpanded) {
                collapsibles.forEach(otherButton => {
                    if (otherButton !== this) {
                        const otherSection = otherButton.closest('.collapsible-section');
                        otherSection.classList.remove('active');
                        otherButton.setAttribute('aria-expanded', 'false');
                    }
                });
            }
        });
    });

    if (collapsibles.length > 0) {
        const firstSection = collapsibles[0].closest('.collapsible-section');
        firstSection.classList.add('active');
        collapsibles[0].setAttribute('aria-expanded', 'true');
    }
});