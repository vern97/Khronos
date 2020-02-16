$('#demo').datetimepicker({
    inline: true,
});

$('.message .close')
    .on('click', function () {
        $(this)
            .closest('.message')
            .transition('fade');
});
