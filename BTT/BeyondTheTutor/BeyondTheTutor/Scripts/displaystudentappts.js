$('#document').ready(function () {

    var ajax_call = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: 'StudentAppointment/GetRequestedStudentAppts',
            success: displayStudentAppts,
            error: errorOnAjax
        });
    }

    var interval = 1000 * 2;
    window.setInterval(ajax_call, interval);

    function errorOnAjax() {
        console.log('Error on displayreqestedappts');
    }

    function displayStudentAppts(data) {
        console.log('success');
        $('#student_requests').empty();

        var numAppts = data.length;
        var numApptsUnderReview = 0;
        var numApptsAccepted = 0;
        var numApptsCanceled = 0;

        for (var i = 0; i < numAppts; i++) {
            if (data[i].CurrStatus == "Requested") {
                numApptsUnderReview += 1;
            }
        }

        for (var i = 0; i < numAppts; i++) {
            if (data[i].CurrStatus == "Canceled") {
                numApptsCanceled += 1;
            }
        }

        if (numApptsUnderReview > 0) {
            $('#student_requests').append(`
                <div class="ui small blue message" id="approved_requests">
                    <div class="ui center aligned header">
                        You have ${numApptsUnderReview} tutoring requests <i>pending</i>
                    </div>
                </div>
            `)
        }

        if (numApptsAccepted > 0) {
            $('#approved_requests').append(`
                <div class="ui center aligned header">
                    and ${numApptsUnderReview} <i>approved</i>
                </div>
            `)
        }

        if (numApptsUnderReview > 0) {
            $('#approved_requests').append(`
                <div class="ui center aligned text">
                    Please check the status for any approved requests <a href="/Student/TutoringAppts"><u>here</u></a> for the agreed time and any notes from the tutor
                </div>
            `)
        }

        if (numApptsCanceled > 0) {
            $('#student_requests').append(`
                <div class="ui small red message" id="approved_requests">
                    <div class="ui center aligned header">
                        You have ${numApptsCanceled} tutoring requests marked as <i>canceled</i>
                    </div>
                    <div class="ui center aligned text">
                        Please check the status for any canceled requests <a href="/Student/TutoringAppts"><u>here</u></a>
                    </div>
                </div>
            `)
        }
    }
});