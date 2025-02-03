document.addEventListener("DOMContentLoaded", function () {
    fetch('/api/GetGenres')
        .then(response => response.json())
        .then(data => {
            if (Array.isArray(data)) {
                const genreDropdown = document.getElementById('genreDropdown');

                // Додаємо опцію "Всі жанри"
                const allGenresOption = document.createElement('option');
                allGenresOption.value = "all genres";
                allGenresOption.textContent = "Всі жанри";
                genreDropdown.appendChild(allGenresOption);

                // Додаємо інші жанри з API
                data.forEach(genre => {
                    const option = document.createElement('option');
                    option.value = genre.name;
                    option.textContent = genre.name;
                    genreDropdown.appendChild(option);
                });
            } else {
                console.error('Expected an array but got:', data);
            }
        })
        .catch(error => console.error('Error fetching genres:', error));
});
