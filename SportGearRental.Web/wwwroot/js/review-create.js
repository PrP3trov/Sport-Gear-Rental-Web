   document.addEventListener('DOMContentLoaded', function() {
        const starRating = document.querySelector('.star-rating');
    const ratingFeedback = document.querySelector('.selected-rating');
    const textarea = document.querySelector('.content-textarea');
    const charCounter = document.querySelector('.current-count');
    const submitBtn = document.querySelector('.submit-btn');
    const form = document.querySelector('.review-form');

    const ratingTexts = {
        1: '⭐ Много слабо - Не препоръчвам',
    2: '⭐⭐ Слабо - Има какво да се желае',
    3: '⭐⭐⭐ Добро - Приемливо качество',
    4: '⭐⭐⭐⭐ Много добро - Препоръчвам',
    5: '⭐⭐⭐⭐⭐ Отлично - Страхотна екипировка!'
        };

    // Star rating functionality
    if (starRating) {
            const stars = starRating.querySelectorAll('input');
            stars.forEach(star => {
        star.addEventListener('change', function () {
            const rating = this.value;
            if (ratingFeedback && ratingTexts[rating]) {
                ratingFeedback.textContent = ratingTexts[rating];
                ratingFeedback.classList.add('show');
            }
        });
            });
        }

    // Character counter
    if (textarea && charCounter) {
        textarea.addEventListener('input', function () {
            const currentLength = this.value.length;
            charCounter.textContent = currentLength;

            if (currentLength > 450) {
                charCounter.style.color = '#e74c3c';
            } else if (currentLength > 400) {
                charCounter.style.color = '#f39c12';
            } else {
                charCounter.style.color = '#667eea';
            }
        });
        }

    // Form submission loading state
    if (form && submitBtn) {
        form.addEventListener('submit', function () {
            submitBtn.classList.add('loading');
            submitBtn.disabled = true;
        });
        }

    // Validation error handling
    function showValidationError(element, message) {
            const errorSpan = element.querySelector('.error-text');
    if (errorSpan) {
        errorSpan.textContent = message;
    element.classList.add('show');
            }
        }

    function hideValidationError(element) {
        element.classList.remove('show');
        }

    // Auto-hide validation errors on input
    const validationErrors = document.querySelectorAll('.validation-error');
        validationErrors.forEach(error => {
            const formGroup = error.closest('.form-group');
    if (formGroup) {
                const inputs = formGroup.querySelectorAll('input, textarea');
                inputs.forEach(input => {
        input.addEventListener('input', () => hideValidationError(error));
                    input.addEventListener('change', () => hideValidationError(error));
                });
            }
        });
    });