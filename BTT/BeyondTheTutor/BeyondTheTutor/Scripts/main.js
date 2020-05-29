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

!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = 'https://weatherwidget.io/js/widget.min.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'weatherwidget-io-js');
