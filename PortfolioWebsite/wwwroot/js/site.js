// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// /Projects Div Fade in function.  working but not well, needs tweaks and better fireing mech
$(window).ready(function () {
    var delay = 500; // Delay in milliseconds 
    $("div[class='repoHolder']").each(function (index) {
        console.log($(this).attr('id'));

        $(this).delay((index + 1) * delay).fadeTo(2500, 1);
    });
});

$(document).ready(function () {
    //remove the underline from all to clear
    $('a.nav-link').css("text-decoration", 'none');
    //switch to active tab and set an underline
    switch (window.location.pathname) {
        case "/":
            console.log(window.location.pathname);
            $('a:contains("Home")').css('text-decoration', 'underline');
            break;
        case "/Resume":
            console.log(window.location.pathname);
            $('a:contains("Resume")').css('text-decoration', 'underline');

            break;
        case "/Projects":
            console.log(window.location.pathname);
            $('a:contains("Projects")').css('text-decoration', 'underline');

            break;
        case "/Contact":
            console.log(window.location.pathname);
            $('a:contains("Contact")').css('text-decoration', 'underline');
            break;
        default:
            console.log(window.location.pathname);
            console.log("Default case: check");
            break;
    }
})

//Projects Clickable
$(".repoHolder").click(function () {
    var target_url = $(this).find("a").attr("href");
    window.open(target_url, "_blank");
});





