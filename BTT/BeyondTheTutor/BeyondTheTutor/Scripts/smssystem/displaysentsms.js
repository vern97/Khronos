function readSentSMS(currentMessage) {   
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Tutor/TutorMessages/ReadMessage",
        data: { 'messageID': currentMessage },
        success: readSentMessage,
        error: errorOnAjax
    });
};

$('#document').ready(function () {

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

    var interval = 1000 * 2;
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

                if (data[i].target == " " || data[i].target == null) {
                    data[i].target= "Everyone";
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
                        <td>${data[i].target}</td>
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
                        <button onclick="readSentSMS('${data[i].id}')" class="ui vertical animated button mini ui fluid button" tabindex="0">
                            <div class="hidden content"><i class="envelope open outline icon"></i></div>
                            <div class="visible content">
                                View Message
                            </div>
                        </button>                                                
                        </td>
                    </tr>
                </tbody>
        `)
            }
        }
    }
});

function readSentMessage(data) {
    console.log('success');

    $('#read_sent_sms').empty();

    $('#read_sent_sms').append(`
        <div class="ui tiny modal" id="sms_id_6"></div>
            `)

    $('#sms_id_6').append(`
          <div class="ui icon header">
            <i class="envelope open outline icon"></i>
            ${data.message}
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
    $('#sms_id_6').modal('show');
}

