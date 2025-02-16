document.addEventListener("DOMContentLoaded", function () {
    const apiUrl = `https://api.themoviedb.org/3/discover/movie?language=uk-UA&with_original_language=uk`;
    const apiKey = 'Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJkMWFiZTI4YjI0YmRhNjQ1YzFmOWE4MGZmZWUwYmNmYSIsIm5iZiI6MTczNjk0ODcwMS41MzYsInN1YiI6IjY3ODdiYmRkZjM4YjVkMzMzOGJiMGY4ZiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.CjHezFml_x3CSeihgAQLcSEfLgvIkmX5yW3ZiG_6_6Q';
    let currentPage = 1;
    let genreMap = {};

    const genreDropdown = document.getElementById('genreDropdown');
    const yearDropdown = document.querySelector('select[name="year"]');
    const gradeRadios = document.querySelectorAll('input[name="grade"]');

    function loadMovies(page, genre = '', year = '', grade = '') {
        let url = `${apiUrl}&page=${page}`;
        if (genre && genre !== 'all') url += `&with_genres=${genre}`;
        if (year && year !== 'all years') {
            if (year.includes('-')) {
                const [startYear, endYear] = year.split('-');
                url += `&primary_release_date.gte=${startYear}-01-01&primary_release_date.lte=${endYear}-12-31`;
            } else {
                url += `&primary_release_year=${year}`;
            }
        }
        if (grade) {
            switch (grade) {
                case 'featured':
                    url += `&sort_by=popularity.desc`;
                    break;
                case 'popular':
                    url += `&sort_by=vote_count.desc`;
                    break;
                case 'newest':
                    url += `&sort_by=release_date.desc`;
                    break;
            }
        }

        fetch(url, {
            headers: {
                'Authorization': apiKey
            }
        })
            .then(response => response.json())
            .then(data => {
                const moviesGrid = document.getElementById('moviesGrid');
                if (page === 1) moviesGrid.innerHTML = ''; // Clear previous movies if it's the first page
                data.results.forEach(movie => {
                    // Фильтруем фильмы, у которых нет украинского названия
                    if (!movie.title) return;

                    const movieCard = document.createElement('div');
                    movieCard.classList.add('movie-card', 'visible');

                    const genres = movie.genre_ids.map(id => genreMap[id]).join(', ');

                    movieCard.innerHTML = `
                    <div class="card-head">
                        <img src="https://image.tmdb.org/t/p/w500${movie.poster_path}" alt="${movie.title}" class="card-img">
                        <div class="card-overlay"></div>
                        <div class="bookmark"><ion-icon name="bookmark-outline"></ion-icon></div>
                        <div class="rating"><ion-icon name="star"></ion-icon><span>${movie.vote_average.toFixed(1)}</span></div>
                        <div class="play"><ion-icon name="play-circle-outline"></ion-icon></div>
                    </div>
                    <div class="card-body">
                        <h3 class="card-title">${movie.title}</h3>
                        <div class="card-info">
                            <span class="genres">${genres}</span>
                        </div>
                        <div class="card-info">
                        <span class="release-date">${movie.release_date}</span>
                        </div>
                    </div>
                `;

                    moviesGrid.appendChild(movieCard);
                });
            })
            .catch(error => console.error('Error fetching movies:', error));
    }

    // Load genres for the dropdown
    fetch('https://api.themoviedb.org/3/genre/movie/list?language=uk-UA', {
        headers: {
            'Authorization': apiKey
        }
    })
        .then(response => response.json())
        .then(data => {
            const allGenresOption = document.createElement('option');
            allGenresOption.value = 'all';
            allGenresOption.textContent = 'Всі жанри';
            genreDropdown.appendChild(allGenresOption);

            data.genres.forEach(genre => {
                genreMap[genre.id] = genre.name;
                const option = document.createElement('option');
                option.value = genre.id;
                option.textContent = genre.name;
                genreDropdown.appendChild(option);
            });

            // Установить "Всі жанри" как выбранный по умолчанию
            genreDropdown.value = 'all';

            // Load initial movies after genres are loaded
            loadMovies(currentPage);
        })
        .catch(error => console.error('Error fetching genres:', error));

    // Load more movies on button click
    document.querySelector('.load-more').addEventListener('click', function () {
        currentPage++;
        loadMovies(currentPage, genreDropdown.value, yearDropdown.value, document.querySelector('input[name="grade"]:checked').id);
    });

    // Filter movies on filter change
    genreDropdown.addEventListener('change', function () {
        currentPage = 1;
        loadMovies(currentPage, genreDropdown.value, yearDropdown.value, document.querySelector('input[name="grade"]:checked').id);
    });

    yearDropdown.addEventListener('change', function () {
        currentPage = 1;
        loadMovies(currentPage, genreDropdown.value, yearDropdown.value, document.querySelector('input[name="grade"]:checked').id);
    });

    gradeRadios.forEach(radio => {
        radio.addEventListener('change', function () {
            currentPage = 1;
            loadMovies(currentPage, genreDropdown.value, yearDropdown.value, document.querySelector('input[name="grade"]:checked').id);
        });
    });
});



