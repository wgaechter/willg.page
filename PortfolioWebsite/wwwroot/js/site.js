// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// /Projects Div Fade in function.  working but not well, needs tweaks and better fireing mech
$(window).ready(function () {
    var delay = 500; // Delay in milliseconds 
    $("div[class='repoHolder']").each(function (index) {
        console.log($(this).attr('id'));

        // Get the color of the icon inside .cornerIcon and set it as the border color
        var icon = $(this).find("div.cornerIcon > i");
        var iconColor = icon.css("color");
        $(this).find("div.cornerIcon").css("border-color", iconColor);
        $(this).find("div.repoHolder").css("border-color", iconColor);

        // Fade in each repoHolder with a delay
        $(this).delay((index + 1) * delay).fadeTo(1500, 1);
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

//Articles Shift
//On Read More button click

//Get All articles
//Shift animate all divs down off screen
//Shift generated article onto screen with full content w/ return button


