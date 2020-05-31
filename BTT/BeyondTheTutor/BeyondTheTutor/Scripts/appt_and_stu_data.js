$.ajax({
    type: 'GET',
    dataType: 'json',
    url: 'ViewData/GetStu',
    success: setStudents,
    error: errorOnAjax
});

function setStudents(data) {
     names = [];
     counts = [];
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


$.ajax({
    type: 'GET',
    dataType: 'json',
    url: 'ViewData/GetBars',
    success: setBars,
    error: errorOnAjax
});


function setBars(data) {
    colors = [];
    color = 0;

     names = [];
     counts = [];
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






        barChart = new Chart(document.getElementById("bar-chart"), {
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
                        beginAtZero: true,
                        callback: function (value) {
                            if (value % 1 === 0) {
                                return value;
                            }
                        }
                    }
                }]
            },
            legend: {
                display: false
            }
        }
    });
}




$.ajax({
    type: 'GET',
    dataType: 'json',
    url: 'ViewData/GetLines',
    success: setLines,
    error: errorOnAjax
});




function setLines(data) {
    months = [];
    counts = [];
    data.forEach(m => {
        months.push(m.name);
        counts.push(m.count);
    });

    ctx = document.getElementById("line").getContext("2d");


    myLineChart = new Chart(ctx, {
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

$(window).on('load', function () {
    document.getElementById('line').scrollIntoView({
        behavior: "smooth"
    });
})


function errorOnAjax(data) {

    console.log('Error on AJAX return');
};