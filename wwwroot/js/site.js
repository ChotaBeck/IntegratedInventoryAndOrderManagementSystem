// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
      
        // Automatically collapse sidebar on larger screens
        function adjustSidebar() {
            var screenWidth = $(window).width();
            var toggleWidth = $('#sidebarCollapse').outerWidth();

            if (screenWidth >= 192) {
                $('#sidebar').removeClass('active');
            } else {
                $('#sidebar').addClass('active');
            }
        }

        // Adjust sidebar on page load and window resize
        adjustSidebar();
        $(window).resize(adjustSidebar);
    });