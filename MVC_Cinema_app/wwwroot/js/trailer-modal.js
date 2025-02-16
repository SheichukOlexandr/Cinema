// Функция открытия модального окна с трейлером
function openTrailerModal(trailerUrl) {
    if (!trailerUrl) {
        alert("Трейлер недоступний");
        return;
    }

    let trailerId = getYouTubeVideoId(trailerUrl);
    if (!trailerId) {
        alert("Невірне посилання на трейлер");
        return;
    }

    let modal = document.getElementById("trailerModal");
    let iframe = document.getElementById("trailerFrame");
    iframe.src = `https://www.youtube.com/embed/${trailerId}?autoplay=1`;
    modal.style.display = "block";
}

// Функция закрытия модального окна
function closeTrailerModal() {
    let modal = document.getElementById("trailerModal");
    let iframe = document.getElementById("trailerFrame");
    iframe.src = "";
    modal.style.display = "none";
}

// Извлекаем ID видео из YouTube-ссылки
function getYouTubeVideoId(url) {
    let match = url.match(/[?&]v=([^&]+)/) || url.match(/youtu\.be\/([^?]+)/);
    return match ? match[1] : null;
}

// Закрытие модального окна при клике вне его
window.onclick = function (event) {
    let modal = document.getElementById("trailerModal");
    if (event.target === modal) {
        closeTrailerModal();
    }
};
