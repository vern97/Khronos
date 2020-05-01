$('#document').ready(function () {

    var get_upcoming_appt = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/Student/Zoom/GetStuOnlineAppts',
            success: retrieve_upcoming_appt,
            error: errorOnAjax
        });
    }

    function retrieve_upcoming_appt(data) {
        $('#display_zoom_alert').empty()
        if (data.length == 0) {
            $('#display_zoom_alert').append(`
                    <div class="ui blue message">
                        <div class="center aligned content">
                            <div class="ui medium center aligned header">
                                <i class="comments outline icon"></i>
                                Online Tutoring Appointments
                            </div>
                            <div class="ui center aligned segment" id="zoom_alert_info">
                            
                            </div>
                        </div>
                    </div>
                `)
            $('#zoom_alert_info').append(`
                    <div class="ui small center aligned header">
                        You have no scheduled Zoom appointments
                    </div>
                    Get homework help today
                    <div style="padding-top:15px;">
                        <a class="teal fluid ui button" href="/Student/TutoringAppts/Create">Request Tutoring</a>
                    </div>           
                `)
        }

        for (var i = 0; i < data.length; i++){
            if (data[i].HasStarted == true && data[i].Upcoming == true) {
                $('#display_zoom_alert').append(`
                    <div class="ui blue message">
                        <div class="center aligned content">
                            <div class="ui medium center aligned header">
                                <i class="bullhorn icon"></i>
                                Upcoming Tutoring Appointment
                            </div>
                            <div class="ui center aligned segment" id="zoom_alert_info">
                            
                            </div>
                        </div>
                    </div>
                `)
                $('#zoom_alert_info').append(`
                     <div class="ui small center aligned header">
                        Your tutoring appointment has begun! Please click the link below to join your session.
                    </div>
                    Your appointment for ${data[i].Class} will be with ${data[i].AssignedTutor} from ${data[i].StartTime} to ${data[i].EndTime} for ${data[i].Length}
                    <div style="padding-top:15px;">
                        <a class="teal fluid ui button" href="https://zoom.us/j/8623070324">Join Meeting</a>
                    </div>
                `)
            } else if (data[i].HasStarted == false && data[i].Upcoming == true) {
                $('#display_zoom_alert').append(`
                    <div class="ui blue message">
                        <div class="center aligned content">
                            <div class="ui medium center aligned header">
                                <i class="bullhorn icon"></i>
                                Upcoming Tutoring Appointment
                            </div>
                            <div class="ui center aligned segment" id="zoom_alert_info">
                            
                            </div>
                        </div>
                    </div>
                `)
                $('#zoom_alert_info').append(`
                    <div class="ui small center aligned header">
                        Your tutoring appointment will begin in ${data[i].UntilStart} minutes! The link to your meeting room will enable when it has begun.
                    </div>
                    Your appointment for ${data[i].Class} will be with ${data[i].AssignedTutor} from ${data[i].StartTime} to ${data[i].EndTime} for ${data[i].Length}
                    <div style="padding-top:15px;">
                        <a class="blue fluid ui disabled button">Join Meeting</a>
                    </div>
                    
                `)
            } else {
                $('#display_zoom_alert').append(`
                    <div class="ui blue message">
                        <div class="center aligned content">
                            <div class="ui medium center aligned header">
                                <i class="comments outline icon"></i>
                                Online Tutoring Appointments
                            </div>
                            <div class="ui center aligned segment" id="zoom_alert_info">
                            
                            </div>
                        </div>
                    </div>
                `)
                $('#zoom_alert_info').append(`
                    <div class="ui small center aligned header">
                        You have no scheduled Zoom appointments
                    </div>
                    Get homework help today
                    <div style="padding-top:15px;">
                        <a class="teal fluid ui button" href="/Student/TutoringAppts/Create">Request Tutoring</a>
                    </div>           
                `)

            }
            
        }
    }

    function errorOnAjax() {
        console.log('Error on ajax');
    }

    // Call ajax on window load
    get_upcoming_appt.call();

    // Set interval on ajax call to refresh every 2 seconds
    var interval = 1000 * 2;
    window.setInterval(get_upcoming_appt, interval);
});