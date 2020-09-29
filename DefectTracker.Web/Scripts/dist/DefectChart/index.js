"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const Chart = require("chart.js");
const moment = require("moment");
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
    'rgba(255, 99, 132, 0.2)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)'
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
    'rgba(255,99,132,1)',
    'rgba(54, 162, 235, 1)',
    'rgba(255, 206, 86, 1)',
    'rgba(75, 192, 192, 1)'
];
let categorizedDefects = [];
let dataSets = [];
let dates = [];
initializeDates();
createDefectTypeArrays();
mapDefects();
createDataSetsForChart();
function createDataSetsForChart() {
    ViewModel.defectTypes.map((dt, index) => {
        dataSets.push({
            label: dt.name,
            data: categorizedDefects.find(cd => cd.typeId == dt.id).defects.length,
            backgroundColor: backgroundColors[index],
            borderColor: borderColors[index],
            borderWidth: 1
        });
    });
    console.log('Chart Data Sets: ');
    console.log(dataSets);
}
function createDefectTypeArrays() {
    ViewModel.defectTypes.map(dt => {
        categorizedDefects.push({
            typeId: dt.id,
            defects: []
        });
    });
}
function initializeDates() {
    let allDates = [];
    ViewModel.defects.map(d => {
        let createDate = moment(d.originDate).format(dateFormat);
        if (allDates.filter(ad => {
            return ad == createDate;
        }).length == 0) {
            allDates.push(createDate);
        }
    });
    dates = allDates;
}
function mapDefects() {
    ViewModel.defects.map(d => {
        let defectCategory = categorizedDefects.filter(cd => {
            return cd.typeId == d.defectTypeId;
        });
        defectCategory[0].defects.push(d);
        dates.push(moment(d.originDate).format(dateFormat));
    });
    console.log('Categorized Defects: ');
    console.log(categorizedDefects);
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