$('.message .close')
    .on('click', function () {
        $(this)
            .closest('.message')
            .transition('fade');
    });

$('.ui.accordion')
    .accordion();

$(document).ready(function () {
    $('.dropdown').dropdown();
});