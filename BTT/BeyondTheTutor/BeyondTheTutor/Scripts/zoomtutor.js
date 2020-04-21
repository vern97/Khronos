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
        $('#meeting_div').empty();
        if (data.length == 0) {
            $('#meeting_div').append(`
                <p><b>It seems you have no scheduled Tutoring Appointments today</b></p>
            `);
        } else {
            for (var i = 0; i < data.length; i++) {
                $('#meeting_div').append(`
                <div class="item">
                    <div class= "right floated content" >
                        <a class="ui button" href="https://zoom.us/j/8623070324" target="_blank">
                            Start Meeting
                        </a>
                    </div >
                    <div class="content">
                        <p class="header">${data[i].Requestor} - ${data[i].Class}</p>
                        <div class="description">${data[i].StartTime}-${data[i].EndTime} - ${data[i].Length}</div>
                    </div>
                </div >
            `);
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
        $('#zoom_info_div').append(`
            <p>Hello, <i>${data.users[0].first_name} ${data.users[0].last_name}</i></p>
            <p><b>Your Personal Meeting ID:</b></p>
            <p>${data.users[0].pmi}</p>
            <p><b>Your Assigned Meeting Room:</b></p>
            <p>https://zoom.us/j/${data.users[0].pmi}</p>

        `)
    }

    function errorOnAjax() {
        console.log('Error on ajax');
    }

    // Call ajax on window load
    get_online_appts.call();
    get_zoom_info.call();

    // Set interval on ajax call to refresh every 2 seconds
    var interval = 1000 * 2;
    window.setInterval(get_online_appts, interval);
});