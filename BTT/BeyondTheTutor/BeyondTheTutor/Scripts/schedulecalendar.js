$(document).ready(function () {
    $('#calendar').fullCalendar({
        defaultView: 'agendaWeek',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'agendaWeek,agendaDay'
        },
        buttonText: {
            week: 'Week',
            day: 'Day',
            today: 'Today'
        },
        events: '/Home/GetTutorSchedules',
        timezone: "local",
        minTime: "08:00:00",
        maxTime: "18:00:00",
        allDaySlot: false,
        height: 'auto'
    })
});