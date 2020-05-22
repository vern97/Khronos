$('#document').ready(function () {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: 'Student/Home/GetStudent',
        success: displayTutors,
        error: errorOnAjax
    });
});

function errorOnAjax() {
    console.log('Error on AJAX return');
}

function displayTutors(data) {

    $('#student_card').append(`
        <div class="ui card portal">
            <div class="ui image">
                <img src="/Home/RetrieveCurrentTutorProfilePicture/${data[0].profilePictureID}" alt="Alternate Text" />
            </div>
            <div class="content">
                <div class="ui centered dividing header"><p id="portal-headers-black">${data[0].FullName}</p></div>
                <div class="description">
                    <center>
                        <span class="date">${data[0].ClassStanding}</span><br />
                        <span class="date">Class of ${data[0].GradYear}</span>
                    </center>
                </div>
            </div>
        </div>
    `)

}