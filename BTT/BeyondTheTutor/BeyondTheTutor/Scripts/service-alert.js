$('#document').ready(function () {

    var ajax_call = function () {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: 'Home/GetServiceAlerts',
            success: displayServiceAlerts,
            error: errorOnAjax
        });
    }

    ajax_call.call();

    var interval = 1000 * 2;
    window.setInterval(ajax_call, interval);

    function errorOnAjax() {
        console.log('Error on displayServiceAlerts');
    }

    function displayServiceAlerts(data) {
        console.log('success');
        $('#display_service_alerts').empty();

        var numAlerts = data.length;

        for (var i = 0; i < numAlerts; i++) {
            if (data[i].status == "Absent") {
                $('#display_service_alerts').append(`
                    <div class="ui negative message">
                        <div class="center aligned content">
                            <div class="ui medium center aligned header">
                                <i class="bullhorn icon"></i>
                                Service Alert 
                            </div>
                            <div class="ui center aligned segment">
                                <div class="ui small center aligned header">
                                    ${data[i].tutorName} will not be available for tutoring today. Don't worry though, we can still help!
                                </div>
                                If you need immediate help or had a scheduled appointment, please contact us via beyondthetutor@gmail.com or use our messaging feature found in the Student Portal
                            </div>
                        </div>
                    </div>
                `)
            }
            else if (data[i].status == "Late") {
                $('#display_service_alerts').append(`
                    <div class="ui negative message">
                        <div class="center aligned content">
                            <div class="ui medium center aligned header">
                                <i class="bullhorn icon"></i>
                                Service Alert 
                            </div>
                            <div class="ui center aligned segment">
                                <div class="ui small center aligned header">
                                    ${data[i].tutorName} is running a tad late. Check back for this nifty banner to disappear when they arrive.
                                </div>
                                If you need immediate help, use our messaging feature found in the Student Portal
                            </div>
                        </div>
                    </div>
                `)
            }
        }
    }
});