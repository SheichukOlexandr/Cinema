document.addEventListener("DOMContentLoaded", function () {
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

            data.forEach(movie => {
                const movieCard = document.createElement('div');
                movieCard.className = 'movie-card';
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
                            <span class="genre">${movie.genreName || "Невідомий жанр"}</span> 
                            <span> | </span>
                            <span class="year">${movie.releaseDate || movie.ReleaseDate}</span>
                        </div>
                    </div>
                `;
                moviesGrid.appendChild(movieCard);
            });
        })
        .catch(error => console.error('Error fetching movies:', error));
});
