$('#document').ready(function () {

    var ajax_call = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: 'TutorAppointment/GetRequestedTutorAppts',
            success: displayNumRequestedAppts,
            error: errorOnAjax
        });
    }

    var interval = 1000 * 2;
    window.setInterval(ajax_call, interval);

    function errorOnAjax() {
        console.log('Error on displayreqestedappts');
    }

    function displayNumRequestedAppts(data) {
        $('#tutor_requests').empty();
        if (data[0] > 0) {
            $('#tutor_requests').append(`
                <div class="ui cards">
                    <div class="card">
                        <div class="content tutor">
                            <div class="ui centered header"><a href="/Tutor/TutoringAppts" id="portal-headers-alert">${data[0]} New Request</a></div>
                            <div class="meta">Status</div>
                            <div class="description">
                                Click <a href="Tutor/TutoringAppts/Index" id="portal-headers">here</a> to review.
                            </div>
                        </div>
                    </div>
                </div>
            `)
        }
        else {
            $('#tutor_requests').append(`
                <div class="ui cards">
                    <div class="card">
                        <div class="content tutor">
                            <div class="ui centered header"><a href="/Tutor/TutoringAppts" id="portal-headers">Session Alerts</a></div>
                            <div class="meta">Status</div>
                            <div class="description">
                                No new requests.
                            </div>
                        </div>
                    </div>
                </div>
            `)
        }
    }
});