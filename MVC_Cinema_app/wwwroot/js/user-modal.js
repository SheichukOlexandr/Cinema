document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("reservation-modal");
    const closeButton = document.querySelector(".button");
    const modalPoster = document.getElementById("modal-poster");
    const openButtons = document.querySelectorAll(".open-modal-btn");

    openButtons.forEach(button => {
        button.addEventListener("click", function () {
            document.getElementById("modal-movie-name").textContent = this.dataset.movie;
            document.getElementById("modal-date").textContent = this.dataset.date;
            document.getElementById("modal-time").textContent = this.dataset.time;
            document.getElementById("modal-price").textContent = this.dataset.price + " грн";
            document.getElementById("modal-seat").textContent = this.dataset.seat;
            document.getElementById("modal-reservation-id").value = this.dataset.id;
            document.getElementById("modal-seat-price").textContent = this.dataset.seatPrice + " грн";
            // Додаємо постер
            if (this.dataset.poster) {
                modalPoster.src = this.dataset.poster;
                modalPoster.style.display = "block";
            } else {
                modalPoster.style.display = "none"; // Ховаємо, якщо немає постера
            }

            modal.style.display = "block";
            setTimeout(openModal, 10); // Невелика затримка, щоб спрацювала анімація
        });
    });

    // Функція відкриття модального вікна
    function openModal() {
        modal.classList.add("show");

        modal.classList.remove("hide");

        document.body.classList.add("no-scroll");
    }

    // Функція закриття модального вікна
    function closeModal() {
        modal.classList.add("hide");
        modal.classList.remove("show");

        // Через 400 мс (час анімації) ставимо display: none
        setTimeout(() => {
            modal.style.display = "none";
        }, 400);

        document.body.classList.remove("no-scroll");
    }

    // Закриваємо модальне вікно при натисканні на кнопку
    closeButton.addEventListener("click", closeModal);

    // Закриваємо модальне вікно при натисканні поза його межами
    window.addEventListener("click", function (event) {
        if (event.target === modal) {
            closeModal();
        }
    });
});

// Функції підтвердження дій
function confirmReservation(totalPrice) {
    return confirm("Ви впевнені, що хочете підтвердити бронювання? Загальна вартість: " + totalPrice);
}

function confirmCancel() {
    return confirm("Ви впевнені, що хочете скасувати бронювання?");
}

function confirmDelete() {
    return confirm("Ви впевнені, що хочете видалити бронювання?");
}

function toggleDetails(id) {
    let details = document.getElementById("details-" + id);
    if (details) {
        details.style.display = (details.style.display === "none" || details.style.display === "") ? "block" : "none";
    }
}
