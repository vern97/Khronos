function saveSMS(currentID) {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Tutor/SMSArchives/ArchiveMessage",
        data: { 'messageID': currentID },
        success: showSuccessfulSave,
        error: errorOnAjax
    });
};

function readSMS(currentMessage) {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Tutor/TutorMessages/ReadMessage",
        data: { 'messageID': currentMessage },
        success: readMessage,
        error: errorOnAjax
    });
};

function sendResponse(currentID) {

    getUserResponse = document.querySelector('#userResponse');
    getMessagePriority = document.querySelector('[name="priority"]:checked');

    priorityValue = getMessagePriority.value;

    userResponse = getUserResponse.value;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Tutor/SMSReplies/SendResponse",
        data: { 'messageID': currentID, 'userResponse': userResponse, 'messagePriority': priorityValue },
        success: showSuccessfulResponse,
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

    var interval = 1000 * 1.5;
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
                        <button onclick="readSMS('${data[i].id}')" class="ui vertical animated button mini ui fluid button" tabindex="0">
                            <div class="hidden content"><i class="envelope open outline icon"></i></div>
                            <div class="visible content">
                                View Message
                            </div>
                        </button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="border-bottom: solid; border-bottom-width: 2px; border-bottom-color: #db0a29;">
                        <button onclick="saveSMS('${data[i].id}')" class="ui vertical animated button mini ui red fluid button" tabindex="0">
                            <div class="hidden content"><i class="upload icon"></i></div>
                            <div class="visible content">
                                Save Message
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

function showSuccessfulSave(data) {
    console.log('success');

    $('#show_saved_modal').empty();

    $('#show_saved_modal').append(`
        <div class="ui tiny modal" id="sms_id_2"></div>
            `)

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
    $('#sms_id_2').modal('show');
}

function readMessage(data) {
    console.log('success');

    $('#read_sms_modal').empty();

    $('#read_sms_modal').append(`
        <div class="ui tiny modal" id="sms_id_3"><i class="close icon"></i></div>
            `)

    $('#sms_id_3').append(`
          <div class="ui icon header">
            <i class="envelope open outline icon"></i>
            ${data.message}
          </div>
            </br>
            <center>
               <button class="ui mini button">Toggle Reply Form</button>
            </center>
               <div class="ui one column padded grid">
                 <div class="column">
                    <div class="response" style="display: none; text-align: center;">
                        <div class="field">                           
                            <h5>Compose Message</h5>
                            <textarea id="userResponse"></textarea>                          
                        </div>
                        </br>
                        <div class="ui form" style="padding-left: 25%;">
                          <div class="inline fields">
                            <label>Choose a priority</label>
                            <div class="field">
                              <div class="ui radio checkbox">
                                <input type="radio" name="priority" id="priority" value="1" checked="checked">
                                <label>Normal</label>
                              </div>
                            </div>
                            <div class="field">
                              <div class="ui radio checkbox">
                                <input type="radio" name="priority" id="priority" value="2" >
                                <label>High</label>
                              </div>
                            </div>
                          </div>
                        </div>
                        <div class="ui hidden divider"></div>
                        <button onclick="sendResponse(${data.id})" class="fluid ui button schedule">Send Reply</button>
                    </div>
                 </div>
            </div>
        `)
    $('#sms_id_3').modal('show');

    $('.ui.checkbox').checkbox(); 

    $("button").click(function () {
        $(".response").toggle();
    });
}

function showSuccessfulResponse(data) {
    console.log('success');

    location.reload();
}


