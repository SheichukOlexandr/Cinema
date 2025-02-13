document.addEventListener("DOMContentLoaded", function () {
    const moviesGrid = document.getElementById('moviesGrid');
    const genreDropdown = document.getElementById('genreDropdown');
    const trailerModal = document.getElementById('trailerModal');
    const trailerFrame = document.getElementById('trailerFrame');
    const closeModal = document.getElementsByClassName('close')[0];
    const modalPoster = document.getElementById('modalPoster');
    const modalTitle = document.getElementById('modalTitle');
    const modalDirector = document.getElementById('modalDirector');
    const modalGenre = document.getElementById('modalGenre');
    const modalDescription = document.getElementById('modalDescription');

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
                            <div class="play" data-trailer="${movie.trailerURL || movie.TrailerURL}" data-poster="${movie.posterURL || movie.PosterURL}" data-title="${movie.title || movie.Title}" data-director="${movie.director || movie.Director}" data-genre="${movie.genreName}" data-description="${movie.description || movie.Description}">
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

            // Обработчик клика на кнопку "play"
            document.querySelectorAll('.play').forEach(playButton => {
                playButton.addEventListener('click', function () {
                    console.log('Play button clicked'); // Логирование клика
                    const trailerURL = this.getAttribute('data-trailer');
                    const youtubeEmbedURL = `https://www.youtube.com/embed/${getYouTubeVideoId(trailerURL)}`;
                    trailerFrame.src = youtubeEmbedURL;

                    // Заполнение модального окна данными
                    modalPoster.src = this.getAttribute('data-poster');
                    modalTitle.textContent = this.getAttribute('data-title');
                    modalDirector.textContent = `Director: ${this.getAttribute('data-director')}`;
                    modalGenre.textContent = `Genre: ${this.getAttribute('data-genre')}`;
                    modalDescription.textContent = this.getAttribute('data-description');

                    trailerModal.style.display = "flex";
                    setTimeout(() => {
                        trailerModal.classList.add('show');
                        trailerModal.classList.remove('hide');
                    }, 10); // Небольшая задержка для плавного открытия
                });
            });

            // Закрытие модального окна
            closeModal.onclick = function () {
                console.log('Close button clicked'); // Логирование клика
                trailerModal.classList.add('hide');
                trailerModal.classList.remove('show');
                setTimeout(() => {
                    trailerModal.style.display = "none";
                    trailerFrame.src = "";
                }, 300); // Задержка для завершения анимации
            };

            window.onclick = function (event) {
                if (event.target == trailerModal) {
                    console.log('Window clicked'); // Логирование клика
                    trailerModal.classList.add('hide');
                    trailerModal.classList.remove('show');
                    setTimeout(() => {
                        trailerModal.style.display = "none";
                        trailerFrame.src = "";
                    }, 300); // Задержка для завершения анимации
                }
            };

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
});

function getYouTubeVideoId(url) {
    const regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
    const match = url.match(regExp);
    return (match && match[2].length == 11) ? match[2] : null;
}
