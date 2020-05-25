$('#document').ready(function () {

    var ajax_call = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/Tutor/TutorMessages/GetNewMessages',
            success: displayNewSMS,
            error: errorOnAjax
        });
    }

    ajax_call.call();

    var interval = 1000 * 2;
    window.setInterval(ajax_call, interval);

    function errorOnAjax() {
        console.log('Error on displaynewsms');
    }

    function displayNewSMS(data) {
        $('#incoming_sms').empty();
        $('#incoming_sms').append(`
        <table class="ui red table" id="tutor_messages"></table>
    `)
        for (var i = 0; i < data.length; i++) {
            if (data[i].priority == 1) {
                data[i].priority = "Normal";
            }
            else {
                data[i].priority = "High";
            }  
            
            $('#tutor_messages').append(`
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Time</th>
                        <th>From</th>
                        <th>Priority</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>${data[i].date}</td>
                        <td>${data[i].time}</td>
                        <td>${data[i].sender}</td>
                        <td>${data[i].priority}</td>
                    </tr>
                </tbody>
                <thead>
                    <tr>
                        <th>Subject</th>
                        <th colspan="2">Message</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="border-bottom: solid; border-top-width: 2px; border-bottom-color: #db0a29;">${data[i].subject}</td>
                        <td> 
                            <div class="disabled field">
                                <textarea disabled="" style="height:50px;">${data[i].message}</textarea>
                            </div>
                        </td>
                    </tr>
                </tbody>
        `)
        }
    }
});