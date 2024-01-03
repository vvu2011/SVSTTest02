console.log("HomeIndexJSView");

// Post запрос расчетов
let data = new Object();

setInterval(function () {
    $.ajax({
        type: "GET",
        url: "/Home/GetDataForChart",
        data: data,
        timeout: 180000,  // Timeout in milliseconds (180 seconds)
        success: function (result) {
            let jsonData = JSON.parse(result);
            console.log("данные от сервера");
            console.log(jsonData);

            let chartArray = new Array();
            for (let i = 0; i < jsonData.length; i++) {
                let rowArray = new Array();
                rowArray[0] = jsonData[i].GAS_VAL_DATE;
                rowArray[1] = jsonData[i].H2_VAL;
                rowArray[2] = jsonData[i].O2_VAL;

                chartArray[chartArray.length] = rowArray;
            }

            console.log("данные для диаграммы");
            console.log(chartArray);

            let container = document.getElementById("container");
            container.innerHTML = "";

            CreateChart(chartArray);
        },
        error: function () {
            //alert('Failed to receive the Data');
            console.log('Failed ');
        },
    });

}, 5000);

function CreateChart(data) {
    // create data tables on loaded data
    var msftDataTable = anychart.data.table();
    msftDataTable.addData(data);

    // create stock chart
    var chart = anychart.stock();

    // create first plot on the chart with column series
    var firstPlot = chart.plot(0);
    // create area series on the first plot
    var msftSeries = firstPlot.area(msftDataTable.mapAs({ value: 1 }));
    msftSeries.name('H2_VAL');

    // create second plot on the chart
    var secondPlot = chart.plot(1);
    var orclSeries = secondPlot.area(msftDataTable.mapAs({ value: 2 }));
    orclSeries.name('O2_VAL');

    // create forth plot
    var forthPlot = chart.plot(2);
    forthPlot
        .line()
        .name('H2_VAL')
        .data(msftDataTable.mapAs({ value: 1 }))
        .tooltip(false);
    forthPlot
        .spline()
        .name('O2_VAL')
        .data(msftDataTable.mapAs({ value: 2 }))
        .tooltip(false);

    // create scroller series with mapped data
    chart.scroller().area(msftDataTable.mapAs({ value: 1 }));

    // set chart selected date/time range
    end = new Date();
    end.setHours(end.getHours() + 3);

    begin = new Date();
    begin.setHours(begin.getHours() + 3);
    begin.setMinutes(begin.getMinutes() - 5);

    console.log("begin=" + begin + " end=" + end);
    chart.selectRange(begin, end);

    // set container id for the chart
    chart.container('container');
    // initiate chart drawing
    chart.draw();

    // create range picker
    var rangePicker = anychart.ui.rangePicker();
    // init range picker
    rangePicker.render(chart);

    // create range selector
    var rangeSelector = anychart.ui.rangeSelector();
    // init range selector
    rangeSelector.render(chart);
}