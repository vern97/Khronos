$.ajax({
    type: 'GET',
    dataType: 'json',
    url: 'viewdata/getstu',
    success: setStudents,
    error: errorOnAjax
});

/*
$.ajax({
    type: 'GET',
    dataType: 'json',
    url: 'viewdata/bars',
    success: setBars,
    error: errorOnAjax
});*/

$.ajax({
    type: 'GET',
    dataType: 'json',
    url: 'viewdata/getlines',
    success: setLines,
    error: errorOnAjax
});

function setStudents(data) {
    var names = [];
    var counts = [];
    data.forEach(m => {
        names.push(m.name);
        counts.push(m.count);
    });

    new Chart(document.getElementById("pie-chart").getContext('2d'), {
        type: 'doughnut',
        data: {
            datasets: [{
                data: counts,
                backgroundColor: ["Red", "Pink", "Yellow", "Blue"],
                label: 'Dataset 1'
            }],
            labels: names
        },
        options: {
            responsive: true
        }
    });
}

function setLines(data) {
    var months = [];
    var counts = [];
    data.forEach(m => {
        months.push(m.name);
        counts.push(m.count);
    });

    var ctx = document.getElementById("line").getContext("2d");


    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: months,

            datasets: [{
                borderColor: '#FF0000',
                data: counts
            }]
        },
        options: {
            responsive: false,
            legend: {
                display: false,
            },
            scales: {
                yAxes: [{
                    display: true,
                    ticks: {
                        min: 0, // minimum will be 0, unless there is a lower value.
                        // OR //
                        beginAtZero: true // minimum value will be 0.
                    }
                }]
            }
        }
    });
}

/*
function setBars(data) {
    var colors = [];
    var color = 0;

    var names = [];
    var counts = [];
    data.forEach(m => {
        names.push(m.name);
        counts.push(m.count);

        if (color == 0) {
            color = 1;
            colors.push('#FF0000');
        } else {
            color = 0;
            colors.push('#000000');
        }
    });


    var barChart = new Chart(document.getElementById("bar-chart"), {
        type: 'horizontalBar',
        data: {
            labels: names,
            datasets: [{
                backgroundColor: colors,
                minBarLength: 0,
                data: counts
            }]
        },
        options: {
            scales: {
                xAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            legend: {
                display: false
            }
        }
    });
}*/

$(window).on('load', function () {
    document.getElementById('line').scrollIntoView({
        behavior: "smooth"
    });
})

function errorOnAjax(data) {

    console.log('Error on AJAX return' + 'data');
};