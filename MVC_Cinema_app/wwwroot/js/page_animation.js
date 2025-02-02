document.addEventListener("DOMContentLoaded", function () {
    const links = document.querySelectorAll("a.transition-link");

    links.forEach(link => {
        link.addEventListener("click", function (event) {
            event.preventDefault();
            const targetUrl = this.href;

            document.body.classList.add("fade-out");

            setTimeout(() => {
                window.location.href = targetUrl;
            }, 500);
        });
    });

    document.body.classList.add("fade-in");
});
