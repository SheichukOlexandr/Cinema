fetch('/api/Movies')
    .then(response => {
        if (!response.ok) {
            throw new Error(`HTTP помилка! Статус: ${response.status}`);
        }
        return response.json();
    })
    .then(data => {
        console.log('Movies:', data); // Логирование данных

        if (!Array.isArray(data)) {
            console.error('Очікувався масив фільмів');
            return;
        }

        // Додавання фільмів у сітку
        data.forEach(movie => {
            const movieCard = document.createElement('div');
            movieCard.className = 'movie-card visible';
            movieCard.setAttribute('data-genre', movie.genreName);

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
                    </div>
                    <div class="card-info">
                        <span class="year">${movie.releaseDate || movie.ReleaseDate}</span>
                    </div>
                </div>
            `;

            moviesGrid.appendChild(movieCard);
        });

        // Фільтрація фільмів за жанром
        genreDropdown.addEventListener('change', function () {
            const selectedGenre = this.value;
            const movies = document.querySelectorAll('.movie-card');

            let visibleCount = 0;

            movies.forEach(movie => {
                const movieGenre = movie.getAttribute('data-genre');

                if (selectedGenre === "all genres" || movieGenre === selectedGenre) {
                    movie.classList.remove('hidden', 'hide');
                    movie.classList.add('visible');
                    visibleCount++;
                } else {
                    movie.classList.add('hidden');
                    movie.classList.remove('visible');

                    // Через 300 мс після анімації видаляємо з DOM
                    setTimeout(() => {
                        movie.classList.add('hide');
                        moviesGrid.style.display = "grid"; // Примусове оновлення
                    }, 300);
                }
            });

            // Автоматичне оновлення висоти гріду, щоб уникнути скачків
            moviesGrid.style.minHeight = visibleCount > 0 ? `${Math.ceil(visibleCount / 4) * 250}px` : "400px";
        });

    })
    .catch(error => console.error('Помилка завантаження фільмів:', error));
