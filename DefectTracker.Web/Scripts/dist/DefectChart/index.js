"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const Chart = require("chart.js");
let dateFormat = 'M/D/yyyy';
let backgroundColors = [
    'rgba(255, 99, 132, 0.2)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'rgba(255, 159, 64, 0.2)',
    'rgba(255, 99, 132, 0.2)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(57, 62, 65, 0.2)',
    'rgba(84, 13, 110, 0.2)',
    'rgba(28, 68, 142, 0.2)',
    'rgba(0, 168, 120, 0.2)'
];
let borderColors = [
    'rgba(255,99,132,1)',
    'rgba(54, 162, 235, 1)',
    'rgba(255, 206, 86, 1)',
    'rgba(75, 192, 192, 1)',
    'rgba(153, 102, 255, 1)',
    'rgba(255, 159, 64, 1)',
    'rgba(255,99,132,1)',
    'rgba(54, 162, 235, 1)',
    'rgba(57, 62, 65, 1)',
    'rgba(84, 13, 110, 1)',
    'rgba(28, 68, 142, 1)',
    'rgba(0, 168, 120, 1)'
];
let categorizedDefects = [];
let dataSets = [];
let dates = [];
let defectCountPerDateRange = [];
initializeDates();
createDefectTypeArrays();
mapDefects();
getCountsForDays();
createDataSetsForChart();
function compare(a, b) {
    let aDate = a.split("/");
    let bDate = b.split("/");
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
    for (let i = 0; i < ViewModel.defectTypes.length; i++) {
        dataSets.push({
            label: ViewModel.defectTypes[i].name,
            data: defectCountPerDateRange[i],
            backgroundColor: backgroundColors[i],
            borderColor: borderColors[i],
            borderWidth: 1
        });
    }
}
function createDefectTypeArrays() {
    categorizedDefects = ViewModel.defectTypes.map(dt => {
        return {
            typeId: dt.id,
            defects: []
        };
    });
}
function getCountsForDays() {
    categorizedDefects.map(cd => {
        let defectsByDay = [];
        dates.map(d => {
            let defectsForDate = cd.defects.filter(function (defect) {
                return defect.originDate == d;
            });
            defectsByDay.push(defectsForDate.length);
        });
        defectCountPerDateRange.push(defectsByDay);
    });
}
function initializeDates() {
    let today = new Date();
    let formattedDate = (today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear();
    dates.push(formattedDate);
}
function mapDefects() {
    ViewModel.defects.map(d => {
        let category = categorizedDefects.find(cd => {
            return cd.typeId == d.defectTypeId;
        });
        category.defects.push(d);
        let findDate = dates.filter(date => {
            return date == d.originDate;
        });
        if (findDate.length == 0)
            dates.push(d.originDate);
    });
    dates.sort(compare);
}
let myChart = new Chart("myChart", {
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
//# sourceMappingURL=index.js.map