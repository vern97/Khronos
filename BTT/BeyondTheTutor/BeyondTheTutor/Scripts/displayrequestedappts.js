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
        console.log('success');
        $('#tutor_requests').empty();
        if (data[0] > 0) {
            $('#tutor_requests').append(`
                <div class="ui small warning message">
                    <div class="ui center aligned header">
                        There are currently ${data[0]} unattended tutoring requests. Click <a href="Tutor/TutoringAppts/Index">here</a> to review them.
                    </div>
                </div>
            `)
        }
    }
});