﻿function saveSMS(currentID) {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Tutor/SMSArchives/ArchiveMessage",
        data: { 'messageID': currentID },
        success: showSuccessfulSave,
        error: errorOnAjax
    });
};

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

    var interval = 1000 * 3;
    window.setInterval(ajax_call, interval);

    function errorOnAjax() {
        console.log('Error on displaynewsms');
    }

    function displayNewSMS(data) {
        $('#incoming_sms').empty();      

        if (data.length == 0) {
            $('#incoming_sms').append(`
                <div class="ui floating message">
                     <p><b>No New Messages</b></p>
                </div>
            `)
        }
        else {

            $('#incoming_sms').append(`
        <table class="ui red table" id="tutor_messages"></table>
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

                $('#tutor_messages').append(`
                <thead>
                    <tr>
                        <th>Priority</th>
                        <th>Subject</th>
                        <th>From</th>
                        <th>Date</th>
                        <th>Time</th>            
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="color: ${color}; font-weight: ${weight};">${data[i].priority}</td>
                        <td>${data[i].subject}</td>
                        <td>${data[i].sender}</td>
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
                        <td rows="2" colspan="5" style="max-width: 150px;">   
                           ${data[i].message}                         
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="border-bottom: solid; border-bottom-width: 2px; border-bottom-color: #db0a29;">
                            <div class="fluid ui buttons">
                                 <button class="ui labeled icon button">
                                  <i class="share icon"></i>
                                  Reply
                                </button>
                                <button onclick="saveSMS('${data[i].id}')" class="ui right labeled icon button">
                                  <i class="upload icon"></i>
                                  Save
                                </button>
                            </div>
                        </td>
                    </tr>
                </tbody>
        `)
            }
        }
    }
});

function showSuccessfulSave(data) {
    console.log('success');
    $('#sms_id_2').modal('show');
    $('#sms_id_2').empty();

    $('#sms_id_2').append(`
          <div class="ui icon header">
            <i class="envelope outline icon"></i>
            ${data}
          </div>
          <div class="actions">
            <center>
                <div class="ui green ok inverted button">
                  <i class="checkmark icon"></i>
                  OK
                </div>
            </center>
          </div>
        `)
}