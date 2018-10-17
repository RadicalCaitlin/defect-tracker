$().ready(function () {
    var backgroundColors = [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
        'rgba(75, 192, 192, 0.2)',
        'rgba(153, 102, 255, 0.2)',
        'rgba(255, 159, 64, 0.2)',
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)'
    ];
    var borderColors = [
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)',
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)'
    ];
    var categorizedDefects = [];
    var dataSets = [];
    var dates = [];
    var defectCountPerDateRange = [];

    initializeDates();
    createDefectTypeArrays();
    mapDefects();
    getCountsForDays();
    createDataSetsForChart();

    function compare(a, b) {
        var aDate = a.split("/");
        var bDate = b.split("/");

        if (aDate[2] > bDate[2])
            return 1;

        if (aDate[2] < bDate[2])
            return -1;

        if (aDate[2] === bDate[2]) {
            if (aDate[0] > bDate[0])
                return 1;

            if (aDate[0] < bDate[0])
                return -1;

            if (aDate[0] === b[0]) {
                if (aDate[1] > bDate[1])
                    return 1;

                if (aDate[1] < bDate[1])
                    return -1;

                return 0;
            }
        }
    }

    function createDataSetsForChart() {
        for (var i = 0; i < defectTypes.length; i++) {
            dataSets.push({
                label: defectTypes[i].name,
                data: defectCountPerDateRange[i],
                backgroundColor: backgroundColors[i],
                borderColor: borderColors[i],
                borderWidth: 1
            });
        }

        console.log('Chart Data Sets: ');
        console.log(dataSets);
    }

    function createDefectTypeArrays() {
        for (var i = 1; i < defectTypes.length; i++) {
            categorizedDefects[i] = [];
        }
    }

    function getCountsForDays() {
        for (var i = 1; i < categorizedDefects.length; i++) {

            var defectsByDay = [];

            for (var x = 0; x < dates.length; x++) {
                var defectsForDate = categorizedDefects[i].filter(function (defect) {
                    debugger;

                    return defect.originDate == dates[x];
                });

                defectsByDay.push(defectsForDate.length);
            }

            defectCountPerDateRange.push(defectsByDay);
        }

        console.log('Defect Counts Per Day By Type: ');
        console.log(defectCountPerDateRange);
    }

    function initializeDates() {
        var today = new Date();
        var formattedDate = (today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear();

        dates.push(originDate);
        dates.push(formattedDate);
    }

    function mapDefects() {
        for (var key in defects) {
            var currentDefect = defects[key];

            categorizedDefects[currentDefect.defectTypeId].push(currentDefect);

            var findDate = dates.filter(function(date) {
                return date == currentDefect.originDate;
            });

            if (findDate.length == 0)
                dates.push(currentDefect.originDate);
        }

        console.log('Categorized Defects: ');
        console.log(categorizedDefects);

        dates.sort(compare);
    }

    var ctx = document.getElementById("myChart");
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: dates,
            datasets: dataSets
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            legend: {
                position: 'right'
            }
        }
    });
});