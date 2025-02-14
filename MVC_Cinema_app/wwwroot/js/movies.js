document.addEventListener("DOMContentLoaded", function () {
    const moviesGrid = document.getElementById('moviesGrid');
    const genreDropdown = document.getElementById('genreDropdown');

    // Фільтрація фільмів за жанром
    genreDropdown.addEventListener('change', function () {
        const selectedGenre = this.value;

        filterMovies(selectedGenre)

        // Автоматичне оновлення висоти гріду, щоб уникнути скачків
        moviesGrid.style.minHeight = visibleCount > 0 ? `${Math.ceil(visibleCount / 4) * 250}px` : "400px";

    })
});

function filterMovies(genre) {
    const movies = document.querySelectorAll('.movie-card');
    movies.forEach(movie => {
        movie.style.display = movie.getAttribute('data-genre') === genre || genre === 'all genres' ? 'block' : 'none';
    });
}
