"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.GroupChartBy = void 0;
const Chart = require("chart.js");
const moment = require("moment");
var GroupChartBy;
(function (GroupChartBy) {
    GroupChartBy["Day"] = "Day";
    GroupChartBy["Week"] = "Week";
    GroupChartBy["Month"] = "Month";
})(GroupChartBy = exports.GroupChartBy || (exports.GroupChartBy = {}));
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
createDefectTypeArrays();
mapDefects();
getCountsForDays();
createDataSetsForChart();
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
            let defectsForDate = cd.defects.filter((defect) => {
                switch (ViewModel.groupBy) {
                    case GroupChartBy.Day:
                        return defect.originDate == d;
                    case GroupChartBy.Month:
                        return moment(defect.originDate).format('MMMM') == d;
                    case GroupChartBy.Week:
                        return GetWeekString(defect.originDate) == d;
                }
            });
            defectsByDay.push(defectsForDate.length);
        });
        defectCountPerDateRange.push(defectsByDay);
    });
}
function mapDefects() {
    ViewModel.defects.map(d => {
        let category = categorizedDefects.find(cd => {
            return cd.typeId == d.defectTypeId;
        });
        category.defects.push(d);
        switch (ViewModel.groupBy) {
            case GroupChartBy.Day:
                let findDate = dates.filter(date => {
                    return date == d.originDate;
                });
                if (findDate.length == 0)
                    dates.push(d.originDate);
                break;
            case GroupChartBy.Month:
                let monthString = moment(d.originDate).format('MMMM');
                let findMonth = dates.find(date => {
                    return date == monthString;
                });
                if (findMonth == null)
                    dates.push(monthString);
                break;
            case GroupChartBy.Week:
                debugger;
                let weekString = GetWeekString(d.originDate);
                let findWeek = dates.find(date => {
                    return date == weekString;
                });
                if (findWeek == null)
                    dates.push(weekString);
                break;
        }
    });
}
function GetWeekString(date) {
    let week = moment(date).week();
    let start = moment().week(week).day('Sunday');
    let end = moment().week(week).day('Saturday');
    return `${moment(start).format('M/D/yyyy')} - ${moment(end).format('M/D/yyyy')}`;
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