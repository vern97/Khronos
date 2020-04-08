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
            if (data[i].CurrStatus == "Approved") {
                numApptsAccepted += 1;
            }
        }

        for (var i = 0; i < numAppts; i++) {
            if (data[i].CurrStatus == "Canceled") {
                numApptsCanceled += 1;
            }
        }

        if (numApptsUnderReview > 0) {
            $('#student_requests').append(`
                <div class="ui top attached blue block header" style="width:800px;">
                    You have ${numApptsUnderReview} tutoring requests <i>pending</i>
                </div>
            `)
        }

        if (numApptsAccepted > 0) {
            $('#student_requests').append(`
                    <div class="ui attached green block header">
                        You have ${numApptsAccepted} upcoming tutoring requests
                    </div>
            `)
        }

        if (numApptsCanceled > 0) {
            $('#student_requests').append(`
                    <div class="ui attached red block header">
                        You have ${numApptsCanceled} tutoring requests marked as <i>canceled</i>
                    </div>
            `)
        }

        if (numApptsAccepted > 0 || numApptsCanceled > 0) {
            $('#student_requests').append(`
                    <div class="ui attached segment">
                        Please check the status for any canceled or approved requests <a href="/Student/TutoringAppts"><u>here</u></a>
                    </div>
            `)
        }
    }
});