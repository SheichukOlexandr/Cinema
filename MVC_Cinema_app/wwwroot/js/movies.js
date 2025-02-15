document.addEventListener("DOMContentLoaded", function () {
    const genreDropdown = document.getElementById('genreDropdown');
    const yearDropdown = document.getElementById('yearDropdown');
    const ageDropdown = document.getElementById('ageDropdown');
    const sessionMinDate = document.getElementById('sessionMinDate');
    const sortRadios = document.querySelectorAll('.filter-radios input[name="grade"]');

    genreDropdown.addEventListener('change', applyFiltersAndSorting)
    yearDropdown.addEventListener('change', applyFiltersAndSorting)
    ageDropdown.addEventListener('change', applyFiltersAndSorting)
    sessionMinDate.addEventListener('change', applyFiltersAndSorting)
    sortRadios.forEach(radio => {
        radio.addEventListener('change', applyFiltersAndSorting);
    });


    // hide all the initial cards to save them :)
    const movies = document.querySelectorAll('.movie-card');
    movies.forEach(movie => movie.style.display = 'none')

    // set the initial value for the date picker in an ugly way :/
    let today = new Date().toISOString().split('T')[0];
    document.getElementById("sessionMinDate").value = today;

    applyFiltersAndSorting();
});

function applyFiltersAndSorting() {
    const moviesGrid = document.getElementById('moviesGrid');

    // here are visible and hidden cards
    const initialMovies = Array.from(document.querySelectorAll('.movie-card'));

    // remove all visible cards from previous sorting/filtering
    const moviesToRemove = [...initialMovies.filter(movie => movie.style.display !== 'none')];
    moviesToRemove.forEach(movie => {
        moviesGrid.removeChild(movie);
    });

    // copy the invisible cards
    const movies = Array.from(document.querySelectorAll('.movie-card')).map(movie => movie.cloneNode(true));

    // Filter and sort the data
    let moviesToDisplay = [...movies].filter(movie => filterMovie(movie));

    const selectedSort = document.querySelector('.filter-radios input[name="grade"]:checked').id;
    moviesToDisplay.sort((a, b) => sortMovies(a, b, selectedSort))

    // display the cards
    moviesToDisplay.forEach(movie => {
        movie.style.display = 'block'
        moviesGrid.appendChild(movie);
    });
}

function filterMovie(movie) {
    const selectedGenre = document.getElementById('genreDropdown').value;
    const selectedYear = document.getElementById('yearDropdown').value;
    const selectedAge = document.getElementById('ageDropdown').value;
    const selectedMinDate = document.getElementById('sessionMinDate').value;

    const movieYear = parseInt(movie.getAttribute('data-year'));
    const movieGenre = movie.getAttribute('data-genre');
    const movieAge = parseInt(movie.getAttribute('data-age'));
    const sessions = JSON.parse(movie.getAttribute('data-sessions'));

    const genreCondition = selectedGenre === 'all genres' || movieGenre === selectedGenre;
    const yearCondition = selectedYear === 'all years' || isMovieInYearRange(movieYear, selectedYear);
    const ageCondition = selectedAge === 'all ages' || parseInt(selectedAge) == movieAge;
    const dateCondition = isMovieAvailableAfter(sessions, selectedMinDate)

    return genreCondition && yearCondition && ageCondition && dateCondition;
}

function sortMovies(a, b, sortType) {
    const ratingA = parseFloat(a.getAttribute('data-rating'));
    const ratingB = parseFloat(b.getAttribute('data-rating'));
    const yearA = parseInt(a.getAttribute('data-year'));
    const yearB = parseInt(b.getAttribute('data-year'));

    switch (sortType) {
        case "popular": return ratingB - ratingA;
        case "newest": return yearB - yearA;
        default: return 0;
    }
}

function isMovieInYearRange(movieYear, yearRange) {
    if (isNaN(movieYear)) return false;

    switch (yearRange) {
        case "2025": return movieYear === 2025;
        case "2020-2024": return movieYear >= 2020 && movieYear <= 2024;
        case "2010-2019": return movieYear >= 2010 && movieYear <= 2019;
        case "2000-2009": return movieYear >= 2000 && movieYear <= 2009;
        case "1980-1999": return movieYear >= 1980 && movieYear <= 1999;
        default: return true;
    }
}

function isMovieAvailableAfter(sessions, minDate) {
    return sessions.filter(session => session.date >= minDate).length > 0
}
