// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var elementos = document.getElementsByName('Actualizar');
for (var i = 0; i < elementos.length; i++) {
    elementos[i].style.display = 'none';
}

function mostrarBotones() {
    var elementos = document.getElementsByName('Actualizar');
    for (var i = 0; i < elementos.length; i++) {
        // Comprobar si el elemento ya está visible
        if (elementos[i].style.display === 'none' || elementos[i].style.display === '') {
            elementos[i].style.display = 'table-cell'; // Mostrar si está oculto
        } else {
            elementos[i].style.display = 'none'; // Ocultar si está visible
        }
    }
}