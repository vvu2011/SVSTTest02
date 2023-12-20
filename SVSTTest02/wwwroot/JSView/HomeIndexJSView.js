console.log("HomeIndexJSView");

// Post запрос расчетов
let data = new Object();
//data.fullNameVU = selectorVU.GetSelectedValue();
//data.fullnameFactory = DateTimeSelector.SelectorFactory.GetSelectedValue();
//data.begin = DateTimeSelector.GetBegin();
//data.end = DateTimeSelector.GetEnd();
//data.interval = DateTimeSelector.GetInterval();
$.ajax({
    type: "GET",
    url: "/Home/GetDataForChart",
    data: data,
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

        CreateChart(chartArray);
    },
    error: function () {
        alert('Failed to receive the Data');
        console.log('Failed ');
    },
});

function CreateChart(data) {
    // The data used in this sample can be obtained from the CDN
    // https://cdn.anychart.com/csv-data/msft-daily-short.js
    // https://cdn.anychart.com/csv-data/orcl-daily-short.js
    // https://cdn.anychart.com/csv-data/csco-daily-short.js
    // https://cdn.anychart.com/csv-data/ibm-daily-short.js

    // create data tables on loaded data
    var msftDataTable = anychart.data.table();
    msftDataTable.addData(data);

    //var orclDataTable = anychart.data.table();
    //orclDataTable.addData(get_orcl_daily_short_data());

    //var cscoDataTable = anychart.data.table();
    //cscoDataTable.addData(get_csco_daily_short_data());

    //var ibmDataTable = anychart.data.table();
    //ibmDataTable.addData(get_ibm_daily_short_data());

    // create stock chart
    var chart = anychart.stock();

    // create first plot on the chart with column series
    var firstPlot = chart.plot(0);
    // create area series on the first plot
    var msftSeries = firstPlot.area(msftDataTable.mapAs({ value: 1 }));
    msftSeries.name('H2_VAL');

    // create second plot on the chart
    var secondPlot = chart.plot(1);
    // create spline area series on the second plot
    var orclSeries = secondPlot.splineArea(
        msftDataTable.mapAs({ value: 2 })
    );
    orclSeries.name('O2_VAL').fill('#1976d2 0.65').stroke('1.5 #1976d2');

    //// create third plot
    //var thirdPlot = chart.plot(2);
    //// create step area series on the third plot
    //var cscoSeries = thirdPlot.stepArea(cscoDataTable.mapAs({ value: 4 }));
    //cscoSeries.name('CSCO').fill('#ef6c00 0.65').stroke('1.5 #ef6c00');

    // create forth plot
    var forthPlot = chart.plot(2);
    forthPlot
        .line()
        .name('MSFT')
        .data(msftDataTable.mapAs({ value: 1 }))
        .tooltip(false);
    forthPlot
        .spline()
        .name('ORCL')
        .data(msftDataTable.mapAs({ value: 2 }))
        .tooltip(false);
    //forthPlot
    //    .stepLine()
    //    .name('CSCO')
    //    .data(cscoDataTable.mapAs({ value: 4 }))
    //    .tooltip(false);

    // create scroller series with mapped data
    chart.scroller().area(msftDataTable.mapAs({ value: 1 }));

    // set chart selected date/time range
    //chart.selectRange('2005-01-03', '2005-11-20');
    chart.selectRange(data[0][0], data[data.length - 1][0]);
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