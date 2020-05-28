$('#document').ready(function () {

    var get_student_alerts = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/Student/StudentAlerts/GetStudentAlerts',
            success: retrieve_student_alerts,
            error: errorOnAjax
        });
    }

    function retrieve_student_alerts(data) {
        $('#student_alerts').empty();

        for (var i = (data.length - 1); i >= 0; i--) {
            $('#student_alerts').append(`
                    <div class="event">
                        <div class="label">
                            <img src="/Home/RetrieveCurrentTutorProfilePicture/0">
                        </div>
                        <div class="content">
                            <div class="date">
                                ${data[i].PostedDate} at ${data[i].Postedtime}
                            </div>
                            <div class="summary">
                                <a style="cursor:default;">${data[i].AdminName}</a> posted
                            </div>
                            <div class="extra text">
                                <strong>${data[i].Subject}</strong>
                                <br />
                                ${data[i].Message}
                            </div>
                        </div>
                    </div>
            `)
        }
    }

    function errorOnAjax() {
        console.log('Error on student alerts');
    }

    // Call ajax on window load
    get_student_alerts.call();

    // Set interval on ajax call to refresh every 2 seconds
    var interval = 1000 * 2;
    window.setInterval(get_student_alerts, interval);
});