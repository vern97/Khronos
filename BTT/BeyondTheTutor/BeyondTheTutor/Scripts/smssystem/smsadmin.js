function sendSMS() {

    getsubject = document.querySelector('#Subject');
    getmessage = document.querySelector('#Message');
    getreceiver = document.querySelector('#Receiver');
    getpriority = document.querySelector('#Priority');

    subject = getsubject.value;
    message = getmessage.value;
    receiver = getreceiver.value;
    priority = getpriority.value;

    $('#Subject').attr('placeholder', getsubject.value);
    $('#Message').attr('placeholder', getmessage.value);

    document.getElementById("Subject").value = "";
    document.getElementById("Message").value = "";

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/SMs/SendMessageAdmin",
        data: { 'subject': subject, 'message': message, 'receiver': receiver, 'priority': priority },
        success: showSuccess,
        error: errorOnAjax
    });
};

function errorOnAjax() {
    console.log("ERROR in ajax request.");
}

function showSuccess(data) {
    console.log('success');

    $('#sent_success_modal').empty();

    $('#show_saved_modal').append(`
         <div class="ui tiny modal" id="sms_id_1"></div>
            `)

    $('#sms_id_1').append(`
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
    $('#sms_id_1').modal('show');
}