// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const scrollAmount = 300; // Pixels to scroll per click

function scrollAnime(direction) {
    const carousel = document.getElementById("animeCarousel");
    carousel.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
}

function scrollManga(direction) {
    const carousel = document.getElementById("mangaCarousel");
    carousel.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
}