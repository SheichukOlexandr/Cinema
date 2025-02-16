const wrapper = document.querySelector(".banner-wrapper");
const items = document.querySelectorAll(".banner-card");
const prevBtn = document.querySelector(".prev");
const nextBtn = document.querySelector(".next");

let index = 0;
let slideInterval;

function updateCarousel() {
    wrapper.style.transform = `translateX(-${index * 100}%)`;
}

function nextSlide() {
    index = (index + 1) % items.length;
    updateCarousel();
    resetInterval();
}

function prevSlide() {
    index = (index - 1 + items.length) % items.length;
    updateCarousel();
    resetInterval();
}

function startAutoSlide() {
    slideInterval = setInterval(nextSlide, 5000); // Auto-slide every 5 sec
}

function resetInterval() {
    clearInterval(slideInterval);
    startAutoSlide(); // Restart after user interaction
}

startAutoSlide();