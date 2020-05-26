﻿$('#document').ready(function () {

    var ajax_call = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/Tutor/TutorMessages/GetSentMessages',
            success: displaySentSMS,
            error: errorOnAjax
        });
    }

    ajax_call.call();

    var interval = 1000 * 5;
    window.setInterval(ajax_call, interval);

    function errorOnAjax() {
        console.log('Error on displaysentsms');
    }

    function displaySentSMS(data) {
        $('#outgoing_sms').empty();

        if (data.length == 0) {
            $('#outgoing_sms').append(`
                <div class="ui floating message">
                     <p><b>No Sent Messages</b></p>
                </div>
            `)
        }
        else {

            $('#outgoing_sms').append(`
        <table class="ui red table" id="sent_messages"></table>
    `)
            for (var i = 0; i < data.length; i++) {
                if (data[i].priority == 1) {
                    data[i].priority = "Normal";
                    color = "#000000";
                    weight = "normal";
                }
                else {
                    data[i].priority = "High";
                    color = "#db0a29";
                    weight = "bold";
                }

                if (data[i].receiver == " " || data[i].receiver == null) {
                    data[i].receiver = "Everyone";
                }

                $('#sent_messages').append(`
                <thead>
                    <tr>
                        <th>Priority</th>
                        <th>Subject</th>
                        <th>To</th>
                        <th>Date</th>
                        <th>Time</th>            
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="color: ${color}; font-weight: ${weight};">${data[i].priority}</td>
                        <td>${data[i].subject}</td>
                        <td>${data[i].receiver}</td>
                        <td>${data[i].date}</td>
                        <td>${data[i].time}</td>         
                    </tr>
                </tbody>
                <thead>
                    <tr>                      
                        <th colspan="5">Message</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>                      
                        <td rows="2" colspan="5" style="max-width: 150px; border-bottom: solid; border-bottom-width: 2px; border-bottom-color: #db0a29;">   
                           ${data[i].message}                         
                        </td>
                    </tr>
                </tbody>
        `)
            }
        }
    }
});