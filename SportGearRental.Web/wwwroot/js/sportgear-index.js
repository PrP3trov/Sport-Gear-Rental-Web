
        // Enhanced page interactions
        document.addEventListener('DOMContentLoaded', function() {
            // Add floating animation to background shapes
            const shapes = document.querySelectorAll('.floating-shapes .shape');
            shapes.forEach((shape, index) => {
                shape.style.left = Math.random() * 90 + 5 + '%';
                shape.style.top = Math.random() * 90 + 5 + '%';
                shape.style.animationDelay = Math.random() * 10 + 's';
            });

            // Staggered animation for gear cards
            const observer = new IntersectionObserver((entries) => {
                entries.forEach((entry, index) => {
                    if (entry.isIntersecting) {
                        setTimeout(() => {
                            entry.target.style.opacity = '1';
                            entry.target.style.transform = 'translateY(0)';
                        }, index * 100);
                    }
                });
            });

            const gearCards = document.querySelectorAll('.gear-card');
            gearCards.forEach(card => {
                card.style.opacity = '0';
                card.style.transform = 'translateY(30px)';
                card.style.transition = 'all 0.6s ease';
                observer.observe(card);
            });

            // Enhanced hover effects
            gearCards.forEach(card => {
                card.addEventListener('mouseenter', function() {
                    this.style.transform = 'translateY(-15px) scale(1.02)';
                });

                card.addEventListener('mouseleave', function() {
                    this.style.transform = 'translateY(0) scale(1)';
                });
            });
        });

        // Original delete modal functionality
        var deleteModal = document.getElementById('deleteModal');
        deleteModal.addEventListener('show.bs.modal', function (event) {
            var button = event.relatedTarget;
            var gearId = button.getAttribute('data-gear-id');
            var gearName = button.getAttribute('data-gear-name');

            var modalGearName = deleteModal.querySelector('#gearName');
            modalGearName.textContent = gearName;

            var form = deleteModal.querySelector('#deleteForm');
            form.action = '/SportGear/Delete/' + gearId;
        });
    </script>

    <script>
        // Original rental modal functionality
        document.addEventListener('DOMContentLoaded', function () {
            let startDateInput = document.getElementById("startDate");
            let endDateInput = document.getElementById("endDate");
            let pricePerDayInput = document.getElementById("pricePerDay");
            let totalPriceInput = document.getElementById("totalPrice");
            let gearIdInput = document.getElementById("gearId");
            let dateError = document.getElementById("dateError");
            let rentalForm = document.getElementById("rentalForm");

            // При отваряне на модала
            document.querySelectorAll('[data-bs-target="#rentalModal"]').forEach(button => {
                button.addEventListener('click', function () {
                    let price = this.getAttribute('data-price');
                    let gearId = this.getAttribute('data-gear-id');

                    pricePerDayInput.value = parseFloat(price).toFixed(2) + " лв.";
                    gearIdInput.value = gearId;

                    totalPriceInput.value = "";
                    startDateInput.value = "";
                    endDateInput.value = "";
                    dateError.style.display = "none";
                });
            });

            // Минимална крайна дата според началната
            startDateInput.addEventListener("change", function () {
                endDateInput.setAttribute("min", this.value);
                updateTotalPrice();
            });

            // Пресмятане при промяна на крайната дата
            endDateInput.addEventListener("change", updateTotalPrice);

            // Преди изпращане на формата - проверка
            rentalForm.addEventListener("submit", function (e) {
                updateTotalPrice(); // гарантираме, че цената е изчислена

                if (!totalPriceInput.value) {
                    e.preventDefault();
                    dateError.innerText = "Моля, изберете валидни дати, за да се изчисли цената.";
                    dateError.style.display = "block";
                }
            });

            function updateTotalPrice() {
                let start = new Date(startDateInput.value);
                let end = new Date(endDateInput.value);
                let price = parseFloat(pricePerDayInput.value.replace(" лв.", ""));

                if (!startDateInput.value || !endDateInput.value) {
                    dateError.innerText = "Моля, изберете начална и крайна дата.";
                    dateError.style.display = "block";
                    totalPriceInput.value = "";
                    return;
                }

                if (end < start) {
                    dateError.innerText = "Крайната дата не може да е преди началната.";
                    dateError.style.display = "block";
                    totalPriceInput.value = "";
                    return;
                }

                let today = new Date();
                today.setHours(0,0,0,0);
                if (start < today) {
                    dateError.innerText = "Началната дата не може да е в миналото.";
                    dateError.style.display = "block";
                    totalPriceInput.value = "";
                    return;
                }

                dateError.style.display = "none";

                let days = Math.floor((end - start) / (1000 * 60 * 60 * 24)) + 1;
                let total = (days * price).toFixed(2);

                // Показваме с лв. за потребителя
                totalPriceInput.value = total + " лв.";
            }
        });