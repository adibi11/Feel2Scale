// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    const nav = document.querySelector('.navbar.nav-glass');
    if (!nav) return;
    const onScroll = () => {
        if (window.scrollY > 10) { nav.classList.add('is-scrolled'); }
        else { nav.classList.remove('is-scrolled'); }
    };
    window.addEventListener('scroll', onScroll, { passive: true });
    onScroll();
})();
