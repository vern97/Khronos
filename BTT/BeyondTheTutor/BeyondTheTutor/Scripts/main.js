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

function getResults() {

    jQuery.ajaxSettings.traditional = true

    gradesArray = [];
    weightsArray = [];

    selectElement = document.querySelectorAll(".grades");
    for (i = 0; i < 10; i++) {
        
        selection = selectElement[i].value;
        gradesArray[i] = selection;
    }

    selectElement = document.querySelectorAll(".weights");
    for (i = 0; i < 10; i++) {

        selection = selectElement[i].value;
        weightsArray[i] = selection;
    }

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Home/WeightedGradeResults",
        data: { 'gradesArray': gradesArray, 'weightsArray': weightsArray },
        success: showWeightedGradeResults,
        error: errorOnAjax
    });
};

function errorOnAjax() {
    console.log("ERROR in ajax request.");
}

function showWeightedGradeResults(data) {
    document.getElementById("weighted").remove();
    $('#weightedTable').append($('<table id=\"weighted\">'));
    $('#weighted').append($('<tr id=\"tableTr\">'));
    $('#weighted').append($('</tr>'));
    $('#weightedTable').append($('</table>'));

        var table = document.getElementById("weighted");
        var row = table.insertRow(1);
        var cell = row.insertCell(0);
        cell.innerHTML = '<center>' + data;
}

$(function () {
    $('#reset').on('click', function () {
        $('input[type="number"]').val('');
    });
});

