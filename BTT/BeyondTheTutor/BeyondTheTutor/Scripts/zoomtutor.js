$('#document').ready(function () {

    var get_online_appts = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/Tutor/Zoom/GetOnlineAppts',
            success: retrieve_appts,
            error: errorOnAjax
        });
    }

    // Update links on page to redirect to Users' Zoom meeting
    function retrieve_appts(data) {
        $('#meeting_div_current').empty();
        $('#meeting_div_backlog').empty();
        if (data.length == 0) {
            $('#meeting_div_current').append(`
                <p><b>It seems you have no scheduled Tutoring Appointments today</b></p>
            `);
        } else {
            if (data.length > 1) {
                $('#meeting_div_backlog').append(`
                        <h3><i>Upcoming</i></h3>
                `);
            }
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    $('#meeting_div_current').append(`
                        <h3><i>Next Up</i></h3>
                    `);
                    $('#meeting_div_current').append(`
                        <div class="item">
                            <div id="appt${i}">
                            </div>
                            <div class="content">
                                <p class="header">${data[i].Requestor} - ${data[i].Class}</p>
                                <div class="description">${data[i].StartTime}-${data[i].EndTime} - ${data[i].Length}</div>
                            </div>
                        </div>
                    `);
                    if (data[i].OpenTime == true) {
                        $('#appt' + i).append(`
                            <div class= "right floated content">
                                <a class="ui green button" href="https://zoom.us/j/8623070324" target="_blank">
                                    Start Meeting
                                </a>
                            </div >
                        `)
                    } else {
                        $('#appt' + i).append(`
                            <div class= "right floated content">
                                <a class="ui disabled button" href="https://zoom.us/j/8623070324" target="_blank">
                                    Start Meeting
                                </a>
                            </div >
                        `)
                    }
                } else {
                    $('#meeting_div_backlog').append(`
                        <div class="item">
                            <div id="appt${i}">
                            </div>
                            <div class="content">
                                <p class="header">${data[i].Requestor} - ${data[i].Class}</p>
                                <div class="description">${data[i].StartTime}-${data[i].EndTime} - ${data[i].Length}</div>
                            </div>
                        </div>
                    `);
                    if (data[i].OpenTime == true) {
                        $('#appt' + i).append(`
                            <div class= "right floated content">
                                <a class="ui green button" href="https://zoom.us/j/8623070324" target="_blank">
                                    Start Meeting
                                </a>
                            </div >
                        `)
                    }
                }
            }
        }
    }

    var get_zoom_info = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/ZoomAPI/Users',
            success: retrieve_user_info,
            error: errorOnAjax
        });
    }

    function retrieve_user_info(data) {
        $('#zoom_info_div').empty();
        $('#zoom_info_div').append(`
            <p>Hello, <i>${data.users[0].first_name} ${data.users[0].last_name}</i></p>
            <p><b>Your Personal Meeting ID:</b></p>
            <p>${data.users[0].pmi}</p>
            <p><b>Your Assigned Meeting Room:</b></p>
            <p>https://zoom.us/j/${data.users[0].pmi}</p>

        `)
    }

    var get_meetings_info = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/ZoomAPI/Meetings',
            success: retrieve_meeting,
            error: errorOnAjax
        });
    }

    function retrieve_meeting(data) {
        $('#meeting_status').empty();
        if (data.meetings.length > 0) {
            $('#meeting_status').append(`
                <div class="ui negative message">
                    <div class="center aligned content">
                        <div class="ui medium center aligned header">
                            <i class="bullhorn icon"></i>
                            You are currently in a meeting
                        </div>
                    </div>
                </div>
        `)
        }
    }

    function errorOnAjax() {
        console.log('Error on ajax');
    }

    // Call ajax on window load
    get_online_appts.call();
    get_zoom_info.call();
    get_meetings_info.call();

    // Set interval on ajax call to refresh every 2 seconds
    var interval = 1000 * 2;
    window.setInterval(get_online_appts, interval);
    window.setInterval(get_meetings_info, interval);
});