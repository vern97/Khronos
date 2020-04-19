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
        url: "/Calculators/WeightedGradeResults",
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
        url: "/Calculators/FinalGradeResults",
        data: { 'currentGrade': currentGrade, 'goalGrade': goalGrade, 'finalWeight': finalWeight },
        success: showFinalGradeResults,
        error: errorOnAjax
    });
};

function getCurrentGPA() {
    jQuery.ajaxSettings.traditional = true

    currentGradesArray = [];
    currentCreditsArray = [];

    selectElement = document.querySelectorAll(".currentSemesterGrades");

    for (i = 0; i < 6; i++) {

        selection = selectElement[i].value;
        currentGradesArray[i] = selection;
    }

    selectElement = document.querySelectorAll(".currentSemesterCredits");

    for (i = 0; i < 6; i++) {

        selection = selectElement[i].value;
        currentCreditsArray[i] = selection;
    }

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Calculators/CurrentGPAResults",
        data: { 'currentGradesArray': currentGradesArray, 'currentCreditsArray': currentCreditsArray },
        success: showCurrentGPAResults,
        error: errorOnAjax
    });
};

function getCumulativeGPA() {
    jQuery.ajaxSettings.traditional = true

    selectCurrentGPA = document.querySelector('#calculated');
    selectPreviousGPA = document.querySelector('#previousGPA');
    selectPreviousCredits = document.querySelector('#previousCredits');

    currentGPA = selectCurrentGPA.value;
    previousGPA = selectPreviousGPA.value;
    previousCredits = selectPreviousCredits.value;

    currentCreditsArray = [];
    selectElement = document.querySelectorAll(".currentSemesterCredits");

    for (i = 0; i < 6; i++) {

        selection = selectElement[i].value;
        currentCreditsArray[i] = selection;
    }

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Calculators/CumulativeGPAResults",
        data: { 'currentGPA': currentGPA, 'previousGPA': previousGPA, 'previousCredits': previousCredits, 'calculated': currentGPA, 'currentCreditsArray': currentCreditsArray },
        success: showCumulativeGPAResults,
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

function showCurrentGPAResults(data) {
    document.getElementById("currentGPA").remove();
    $('#currentGPATable').append($('<table id=\"currentGPA\">'));
    $('#currentGPA').append($('<tr id=\"tableTr\">'));
    $('#currentGPA').append($('</tr>'));
    $('#currentGPATable').append($('</table>'));

    var table = document.getElementById("currentGPA");
    var row = table.insertRow(1);
    var cell = row.insertCell(0);
    cell.innerHTML = '<center>' + data;
}

function showCumulativeGPAResults(data) {
    document.getElementById("cumulative").remove();
    $('#cumulativeTable').append($('<table id=\"cumulative\">'));
    $('#cumulative').append($('<tr id=\"tableTr\">'));
    $('#cumulative').append($('</tr>'));
    $('#cumulativeTable').append($('</table>'));

    var table = document.getElementById("cumulative");
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

$(function () {
    $('#resetCurrentGPA').on('click', function () {
        $('input[class="currentSemesterGrades"]').val('');
        $('input[class="currentSemesterCredits"]').val('');
    });
});

$(function () {
    $('#resetCumulativeGPA').on('click', function () {
        $('input[id="calculated"]').val('');
        $('input[id="previousGPA"]').val('');
        $('input[id="previousCredits"]').val('');
    });
});

