document.addEventListener("DOMContentLoaded", function () {
    // Завантаження фільмів
    fetch('/api/Movies')
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log('Movies:', data);
            const moviesGrid = document.getElementById('moviesGrid');

            if (!Array.isArray(data)) {
                console.error('Expected an array of movies');
                return;
            }

            // Створення карток фільмів
            data.forEach(movie => {
                const movieCard = document.createElement('div');
                movieCard.className = 'movie-card';
                movieCard.setAttribute('data-genre', movie.genreName);
                movieCard.style.display = "block"; // Переконатися, що всі картки мають display
                movieCard.innerHTML = `
                    <div class="card-head">
                        <img src="${movie.posterURL || movie.PosterURL}" alt="movie" class="card-img">
                        <div class="card-overlay">
                            <div class="bookmark">
                                <ion-icon name="bookmark"></ion-icon>
                            </div>
                            <div class="rating">
                                <ion-icon name="star-outline"></ion-icon>
                                <span>${movie.rating || movie.Rating}</span>
                            </div>
                            <div class="play">
                                <ion-icon name="play-circle-outline"></ion-icon>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <h3 class="card-title">${movie.title || movie.Title}</h3>
                        <div class="card-info">
                            <span class="genre">${movie.genreName}</span> 
                            <span class="separator">|</span>
                            <span class="year">${movie.releaseDate || movie.ReleaseDate}</span>
                        </div>
                    </div>
                `;
                moviesGrid.appendChild(movieCard);
                setTimeout(() => movieCard.classList.add('visible'), 100); // Додаємо клас 'visible' для плавної появи
            });

            // ✅ Додаємо анімовану фільтрацію фільмів
            document.getElementById('genreDropdown').addEventListener('change', function () {
                const selectedGenre = this.value;
                const movies = document.querySelectorAll('.movie-card');

                if (selectedGenre === "all genres") {
                    // Відновлюємо всі фільми
                    movies.forEach(movie => {
                        movie.classList.remove('hidden'); // Видаляємо клас 'hidden'
                        setTimeout(() => movie.classList.add('visible'), 50); // Додаємо клас 'visible' для анімації
                    });
                } else {
                    // Якщо вибрано конкретний жанр
                    movies.forEach(movie => {
                        const movieGenre = movie.getAttribute('data-genre');
                        if (movieGenre === selectedGenre) {
                            movie.classList.remove('hidden'); // Видаляємо клас 'hidden'
                            setTimeout(() => movie.classList.add('visible'), 50); // Додаємо клас 'visible'
                        } else {
                            movie.classList.add('hidden'); // Додаємо клас 'hidden'
                            setTimeout(() => movie.classList.remove('visible'), 200); // Видаляємо клас 'visible'
                        }
                    });
                }
            });

        })
        .catch(error => console.error('Error fetching movies:', error));
});
