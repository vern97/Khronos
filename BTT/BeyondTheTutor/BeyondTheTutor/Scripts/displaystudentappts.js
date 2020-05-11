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

        $('#student_requests').append(`
            <div class="ui blue message">
                <div class="center aligned content">
                    <div class="ui medium center aligned header">
                        Tutoring Overview
                    </div>
                    <div class="ui top attached blue block header">
                        You have ${numApptsUnderReview} requests <i>pending</i>
                    </div>
                    <div class="ui attached green block header">
                        You have ${numApptsAccepted} requests <i>approved</i>
                    </div>
                        <div class="ui attached red block header">
                        You have ${numApptsCanceled} requests <i>cancelled</i>
                    </div>
                </div>
            </div>
        `)

    }
});