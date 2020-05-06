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
        <div class="ui three stackable centered cards" id="tutor_cards"></div>
    `)
    for (var i = 0; i < data.length; i++) {
        $('#tutor_cards').append(`
            <div class="red centered card" style="min-width:240px;" >
                <div class="image">
                    <img src="/Home/RetrieveCurrentTutorProfilePicture/${data[i].profilePictureID}" alt="Alternate Text" style="max-height:320px; max-width:240px; min-height:320px;" />
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