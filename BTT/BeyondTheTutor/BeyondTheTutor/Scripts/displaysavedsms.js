function deleteSMS(currentID) {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Tutor/SMSArchives/DeleteMessage",
        data: { 'messageID': currentID },
        success: showSuccessfulDeletion,
        error: errorOnAjax
    });
};

function readArchivedSMS(currentMessage) {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Tutor/TutorMessages/ReadArchivedMessage",
        data: { 'messageID': currentMessage },
        success: readArchivedMessage,
        error: errorOnAjax
    });
};


$('#document').ready(function () {

    var ajax_call = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: '/Tutor/TutorMessages/GetArchivedMessages',
            success: displayArchivedSMS,
            error: errorOnAjax
        });
    }

    ajax_call.call();

    var interval = 1000 * 30;
    window.setInterval(ajax_call, interval);

    function errorOnAjax() {
        console.log('Error on displayarchivedsms');
    }

    function displayArchivedSMS(data) {
        $('#archived_sms').empty();

        if (data.length == 0) {
            $('#archived_sms').append(`
                <div class="ui floating message">
                     <p><b>No Saved Messages</b></p>
                </div>
            `)
        }
        else {

            $('#archived_sms').append(`
        <table class="ui red table" id="archived_messages"></table>
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

                $('#archived_messages').append(`
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
                           <button onclick="readArchivedSMS('${data[i].id}')" class="mini ui fluid button">View Message</button>                      
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="border-bottom: solid; border-bottom-width: 2px; border-bottom-color: #db0a29;">
                            <button onclick="deleteSMS('${data[i].id}')" class="mini ui fluid red right labeled icon button">
                                <i class="x icon"></i>
                                Delete Archived Message
                            </button>
                        </td>
                    </tr>
                </tbody>
        `)
            }
        }
    }
});

function showSuccessfulDeletion(data) {
    console.log('success');

    $('#show_deleted').empty();

    $('#show_deleted').append(`
        <div class="ui modal" id="sms_id_4"></div>
            `)

    $('#sms_id_4').append(`
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
    $('#sms_id_4').modal('show');
}

function readArchivedMessage(data) {
    console.log('success');

    $('#read_archived_sms').empty();

    $('#read_archived_sms').append(`
        <div class="ui modal" id="sms_id_5"></div>
            `)

    $('#sms_id_5').append(`
          <div class="ui icon header">
            <i class="envelope open outline icon"></i>
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
    $('#sms_id_5').modal('show');
}
