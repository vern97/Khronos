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

$(document).ready(function () {
    $('#modal').click(function () {
        $('#modalid').modal('show');
    });
});

$(document).ready(function () {
    $('#modal-1').click(function () {
        $('#modalid-1').modal('show');
    });
});

$(document).ready(function () {
    $('#modal-2').click(function () {
        $('#modalid-2').modal('show');
    });
});

$('.menu .item')
    .tab();
