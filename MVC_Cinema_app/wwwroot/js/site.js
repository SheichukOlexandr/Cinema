'use strict';

const header = document.querySelector('header');
const nav = document.querySelector('nav');
const navbarMenuBtn = document.querySelector('.navbar-menu-btn');

const navbarForm = document.querySelector('.navbar-form');
const navbarFormCloseBtn = document.querySelector('.navbar-form-close');
const navbarSearchBtn = document.querySelector('.navbar-search-btn');

function navIsActive() {
    header.classList.toggle('active');
    nav.classList.toggle('active');
    navbarMenuBtn.classList.toggle('active');
}

navbarMenuBtn.addEventListener('click', navIsActive);

const searchBarIsActive = () => navbarForm.classList.toggle('active');

navbarSearchBtn.addEventListener('click', searchBarIsActive);
navbarFormCloseBtn.addEventListener('click', searchBarIsActive);

document.addEventListener("DOMContentLoaded", function () {
    const carousel = document.querySelector(".carousel-inner");
    const items = document.querySelectorAll(".carousel-item");
    const prevBtn = document.querySelector(".prev");
    const nextBtn = document.querySelector(".next");

    let index = 0;

    function updateCarousel() {
        carousel.style.transform = `translateX(-${index * 100}%)`;
    }

    function nextSlide() {
        index = (index + 1) % items.length;
        updateCarousel();
    }

    function prevSlide() {
        index = (index - 1 + items.length) % items.length;
        updateCarousel();
    }

    nextBtn.addEventListener("click", nextSlide);
    prevBtn.addEventListener("click", prevSlide);

    setInterval(nextSlide, 7000); // Автоматична зміна кожні 7 секунд
});
