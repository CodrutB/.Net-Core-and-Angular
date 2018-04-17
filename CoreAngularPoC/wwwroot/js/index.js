$(document).ready(function () {

    var button = $("#buyButton");
    button.on("click", function () {
        console.log("buying item.")
    });

    var productsInfo = $(".product-props li");
    productsInfo.on("click", function () {
        console.log($(this).text());
    });

    var loginToggle = $("#loginToggle");
    var popupForm = $(".popupForm");

    loginToggle.on("click", function () {
        popupForm.fadeToggle(1000);
    });
});
