$(document).ready(function () {
    $('#statusDropDown').change(function () {
        if ($(this).val() == "Absent") {
            $('.endTimeDropDown').addClass("disabled");
        }
        else {
            $('.endTimeDropDown').removeClass("disabled");
        }
    })
});