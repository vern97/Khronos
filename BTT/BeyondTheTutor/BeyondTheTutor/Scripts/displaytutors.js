$('#document').ready(function () {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: 'Home/GetTutors',
        success: displayTutors,
        error: errorOnAjax
    });
});

function errorOnAjax() {
    console.log('Error on AJAX return');
}

function displayTutors(data) {
    $('#display_tutors').append(`
        <div class="ui cards" id="tutor_cards"></div>
    `)
    for (var i = 0; i < data.length; i++) {
        $('#tutor_cards').append(`
            <div class="red card">
                <div class="image">
                    <img src="../Content/images/BeyondtheTutor_Logo.png" alt="Alternate Text" />
                </div>
                <div class="content">
                    <div class="header">${data[i].fName} ${data[i].lName}</div>
                    <div class="meta">
                        <a>Tutor</a>
                    </div>
                    <div class="description">
                        Graduation Year: ${data[i].gradYear}
                    </div>
                </div>
            </div>
        `)
    }
}