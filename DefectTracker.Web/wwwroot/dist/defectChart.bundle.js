/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./js/DefectChart/main.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./js/DefectChart/main.js":
/*!********************************!*\
  !*** ./js/DefectChart/main.js ***!
  \********************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("$().ready(function () {\r\n    var backgroundColors = [\r\n        'rgba(255, 99, 132, 0.2)',\r\n        'rgba(54, 162, 235, 0.2)',\r\n        'rgba(255, 206, 86, 0.2)',\r\n        'rgba(75, 192, 192, 0.2)',\r\n        'rgba(153, 102, 255, 0.2)',\r\n        'rgba(255, 159, 64, 0.2)',\r\n        'rgba(255, 99, 132, 0.2)',\r\n        'rgba(54, 162, 235, 0.2)'\r\n    ];\r\n    var borderColors = [\r\n        'rgba(255,99,132,1)',\r\n        'rgba(54, 162, 235, 1)',\r\n        'rgba(255, 206, 86, 1)',\r\n        'rgba(75, 192, 192, 1)',\r\n        'rgba(153, 102, 255, 1)',\r\n        'rgba(255, 159, 64, 1)',\r\n        'rgba(255,99,132,1)',\r\n        'rgba(54, 162, 235, 1)'\r\n    ];\r\n    var categorizedDefects = [];\r\n    var dataSets = [];\r\n    var dates = [];\r\n    var defectCountPerDateRange = [];\r\n\r\n    initializeDates();\r\n    createDefectTypeArrays();\r\n    mapDefects();\r\n    getCountsForDays();\r\n    createDataSetsForChart();\r\n\r\n    function compare(a, b) {\r\n        var aDate = a.split(\"/\");\r\n        var bDate = b.split(\"/\");\r\n\r\n        if (aDate[2] > bDate[2])\r\n            return 1;\r\n\r\n        if (aDate[2] < bDate[2])\r\n            return -1;\r\n\r\n        if (aDate[2] === bDate[2]) {\r\n            if (aDate[0] > bDate[0])\r\n                return 1;\r\n\r\n            if (aDate[0] < bDate[0])\r\n                return -1;\r\n\r\n            if (aDate[0] === b[0]) {\r\n                if (aDate[1] > bDate[1])\r\n                    return 1;\r\n\r\n                if (aDate[1] < bDate[1])\r\n                    return -1;\r\n\r\n                return 0;\r\n            }\r\n        }\r\n    }\r\n\r\n    function createDataSetsForChart() {\r\n        for (var i = 0; i < defectTypes.length; i++) {\r\n            dataSets.push({\r\n                label: defectTypes[i].name,\r\n                data: defectCountPerDateRange[i],\r\n                backgroundColor: backgroundColors[i],\r\n                borderColor: borderColors[i],\r\n                borderWidth: 1\r\n            });\r\n        }\r\n\r\n        console.log('Chart Data Sets: ');\r\n        console.log(dataSets);\r\n    }\r\n\r\n    function createDefectTypeArrays() {\r\n        for (var i = 1; i < defectTypes.length; i++) {\r\n            categorizedDefects[i] = [];\r\n        }\r\n    }\r\n\r\n    function getCountsForDays() {\r\n        for (var i = 1; i < categorizedDefects.length; i++) {\r\n\r\n            var defectsByDay = [];\r\n\r\n            for (var x = 0; x < dates.length; x++) {\r\n                var defectsForDate = categorizedDefects[i].filter(function (defect) {\r\n                    debugger;\r\n\r\n                    return defect.originDate == dates[x];\r\n                });\r\n\r\n                defectsByDay.push(defectsForDate.length);\r\n            }\r\n\r\n            defectCountPerDateRange.push(defectsByDay);\r\n        }\r\n\r\n        console.log('Defect Counts Per Day By Type: ');\r\n        console.log(defectCountPerDateRange);\r\n    }\r\n\r\n    function initializeDates() {\r\n        var today = new Date();\r\n        var formattedDate = (today.getMonth() + 1) + \"/\" + today.getDate() + \"/\" + today.getFullYear();\r\n\r\n        dates.push(originDate);\r\n        dates.push(formattedDate);\r\n    }\r\n\r\n    function mapDefects() {\r\n        for (var key in defects) {\r\n            var currentDefect = defects[key];\r\n\r\n            categorizedDefects[currentDefect.defectTypeId].push(currentDefect);\r\n\r\n            var findDate = dates.filter(function(date) {\r\n                return date == currentDefect.originDate;\r\n            });\r\n\r\n            if (findDate.length == 0)\r\n                dates.push(currentDefect.originDate);\r\n        }\r\n\r\n        console.log('Categorized Defects: ');\r\n        console.log(categorizedDefects);\r\n\r\n        dates.sort(compare);\r\n    }\r\n\r\n    var ctx = document.getElementById(\"myChart\");\r\n    var myChart = new Chart(ctx, {\r\n        type: 'line',\r\n        data: {\r\n            labels: dates,\r\n            datasets: dataSets\r\n        },\r\n        options: {\r\n            scales: {\r\n                yAxes: [{\r\n                    ticks: {\r\n                        beginAtZero: true\r\n                    }\r\n                }]\r\n            },\r\n            legend: {\r\n                position: 'right'\r\n            }\r\n        }\r\n    });\r\n});\n\n//# sourceURL=webpack:///./js/DefectChart/main.js?");

/***/ })

/******/ });