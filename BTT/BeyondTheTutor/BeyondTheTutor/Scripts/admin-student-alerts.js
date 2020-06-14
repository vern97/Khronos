function delete_message(messageID) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Admin/StudentAlerts/DeleteMessage",
        data: { 'messageID': messageID },
        success: execute_delete_message,
        error: errorOnAjax
    });
}

function get_student_alerts() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/Student/StudentAlerts/GetStudentAlerts',
        success: retrieve_student_alerts,
        error: errorOnAjax
    });
}

$('#document').ready(function () {

    

    // Call ajax on window load
    get_student_alerts.call();

    // Set interval on ajax call to refresh every 2 seconds
    var interval = 1000 * 2;
    window.setInterval(get_student_alerts, interval);
});

function create_post() {
    $('#show_create_post').modal('show');
}

function retrieve_student_alerts(data) {
    $('#student_alerts').empty();

    for (var i = (data.length - 1); i >= 0; i--) {
        $('#student_alerts').append(`
                    <div class="event">
                        <div class="label">
                            <img src="/Home/RetrieveCurrentTutorProfilePicture/0">
                        </div>
                        <div class="content">
                            <div class="date">
                                ${data[i].PostedDate} at ${data[i].Postedtime}
                            </div>
                            <div class="summary">
                                <a style="cursor:default;">${data[i].AdminName}</a> posted
                            </div>
                            <div class="extra text">
                                <strong>${data[i].Subject}</strong>
                                <br />
                                ${data[i].Message}
                            </div>
                            <div class="meta">
                                <a onClick="show_delete_confirmation('${data[i].ID}')">Delete</a>
                            </div>
                        </div>
                    </div>
            `)
    }
}

function show_delete_confirmation(data) {
    $('#confirm_delete').append(`
        <div class="ui mini modal" id="confirm_delete_modal">
          
        </div>
    `)

    $('#confirm_delete_modal').append(`
          <div class="header large">Delete message?</div>
          <div class="actions">
            <button class="ui positive right labeled icon button" onClick="delete_message('${data}')"><i class="checkmark icon"></i>Yes</button>
            <button class="ui negative right labeled icon button"><i class="times icon"></i>No</button>
          </div>
    `)

    $('#confirm_delete_modal').modal('show');
}

function execute_delete_message() {
    console.log('successfully deleted message');

    get_student_alerts.call();
}

function errorOnAjax() {
    console.log('Error on ajax');
}