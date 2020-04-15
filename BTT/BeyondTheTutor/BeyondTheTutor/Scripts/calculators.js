$('.menu .item')
    .tab();

function getWeightedGrade() {
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

function getFinalGrade() {
    selectCurrentGrade = document.querySelector('#currentGrade');
    selectGoalGrade = document.querySelector('#goalGrade');
    selectFinalWeight = document.querySelector('#finalWeight');

    currentGrade = selectCurrentGrade.value;
    goalGrade = selectGoalGrade.value;
    finalWeight = selectFinalWeight.value;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Home/FinalGradeResults",
        data: { 'currentGrade': currentGrade, 'goalGrade': goalGrade, 'finalWeight': finalWeight },
        success: showFinalGradeResults,
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

function showFinalGradeResults(data) {
    document.getElementById("final").remove();
    $('#finalTable').append($('<table id=\"final\">'));
    $('#final').append($('<tr id=\"tableTr\">'));
    $('#final').append($('</tr>'));
    $('#finalTable').append($('</table>'));

    var table = document.getElementById("final");
    var row = table.insertRow(1);
    var cell = row.insertCell(0);
    cell.innerHTML = '<center>' + data;
}

$(function () {
    $('#resetWeighted').on('click', function () {
        $('input[type="number"]').val('');
    });
});

$(function () {
    $('#resetFinal').on('click', function () {
        $('input[id="currentGrade"]').val('');
        $('input[id="goalGrade"]').val('');
        $('input[id="finalWeight"]').val('');
    });
});
