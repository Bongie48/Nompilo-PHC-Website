// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Nkosithandile
var slideIndex = 0;
var slides = document.querySelectorAll('#slideShowContainer img');
setInterval(function () {
    slideIndex++;
    if (slideIndex >= slides.length) {
        slideIndex = 0;
    }
    for (var i = 0; i < slides.length; i++) {
        slides[i].style.opacity = 0;
    }
    slides[slideIndex].style.opacity = 1;
}, 5000);

/* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown menu if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('#dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

function Booking() {
    document.getElementById("myDropdown1").classList.toggle("show");
}

document.addEventListener('DOMContentLoaded', function () {
    const showPopupButton = document.getElementById('showPopupButton');
    const closePopupButton = document.getElementById('closePopupButton');
    const popupDiv = document.getElementById('popupDiv');
    const closePop = document.getElementById('closePop');
    const Pop = document.getElementById('Pop');

    // Show pop-up when the button is clicked
    showPopupButton.addEventListener('click', function () {
        popupDiv.style.display = 'block';
    });

    // Hide pop-up when the close button is clicked
    closePopupButton.addEventListener('click', function () {
        popupDiv.style.display = 'none';
    });

});




//Nkosithandile
//Yolanda
function track(select) {
    if (select.value == 1 || select.value == 2 || select.value == 3 ) {
        document.getElementById('hidden1').style.display = "block";
    }
    else {
        document.getElementById('hidden1').style.display = "none";
    }
    if (select.value == 4 || select.value == 5 || select.value == 6) {
        document.getElementById('hidden2').style.display = "block";
    }
    else {
        document.getElementById('hidden2').style.display = "none";
    }
    if (select.value == 7 || select.value == 8 || select.value == 9) {
        document.getElementById('hidden3').style.display = "block";
    }
    else {
        document.getElementById('hidden3').style.display = "none";
    }
}
