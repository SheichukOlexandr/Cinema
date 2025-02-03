document.addEventListener("DOMContentLoaded", function () {
    fetch('/api/Movies')
        .then(response => response.json())
        .then(data => {
            console.log('Movies:', data); // Отладочное сообщение
            const moviesGrid = document.getElementById('moviesGrid');
            if (data.$values) {
                data.$values.forEach(movie => {
                    const movieCard = document.createElement('div');
                    movieCard.className = 'movie-card';
                    movieCard.innerHTML = `
                        <div class="card-head">
                            <img src="${movie.posterURL}" alt="movie" class="card-img">
                            <div class="card-overlay">
                                <div class="bookmark">
                                    <ion-icon name="bookmark"></ion-icon>
                                </div>
                                <div class="rating">
                                    <ion-icon name="star-outline"></ion-icon>
                                    <span>${movie.rating}</span>
                                </div>
                                <div class="play">
                                    <ion-icon name="play-circle-outline"></ion-icon>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h3 class="card-title">${movie.title}</h3>
                            <div class="card-info">
                                <span class="genre">${movie.genreId}</span>
                                <span class="year">${movie.releaseDate}</span>
                            </div>
                        </div>
                    `;
                    moviesGrid.appendChild(movieCard);
                });
            } else {
                console.error('Expected an array of movies');
            }
        })
        .catch(error => console.error('Error fetching movies:', error)); // Отладочное сообщение
});
