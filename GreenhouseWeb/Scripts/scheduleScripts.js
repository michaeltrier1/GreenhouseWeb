﻿(function (Handsontable) {
    function ligthValidator(a, callback) {
        if (a < 0 || a > 100) {
            callback(false);
            document.getElementById("btn1").disabled = true;
            document.getElementById("btn3").disabled = true;
            document.getElementById("label").innerHTML =
                "<span style='color:#FF0000'>One or more cells are incorrect</span>";
        } else {
            callback(true);
            document.getElementById("btn1").disabled = false;
            document.getElementById("btn3").disabled = false;
            document.getElementById("label").innerHTML = " ";

        }

    }
    Handsontable.validators.registerValidator('ligth', ligthValidator);

})
    (Handsontable);
(function (Handsontable) {
    function temperatureValidator(a, callback) {
        if (a < -100 || a > 100) {
            callback(false);
            document.getElementById("btn1").disabled = true;
            document.getElementById("btn3").disabled = true;
            document.getElementById("label").innerHTML =
                "<span style='color:#FF0000'>One or more cells are incorrect</span>";
        } else {
            callback(true);
            document.getElementById("btn1").disabled = false;
            document.getElementById("btn3").disabled = false;
            document.getElementById("label").innerHTML = " ";
        }

    }
    Handsontable.validators.registerValidator('temp', temperatureValidator);

})(Handsontable);

(Handsontable);
(function (Handsontable) {
    function humidityValidator(a, callback) {
        if (a < 10 || a > 90) {
            callback(false);
            document.getElementById("btn1").disabled = true;
            document.getElementById("btn3").disabled = true;
            document.getElementById("label").innerHTML =
                "<span style='color:#FF0000'>One or more cells are incorrect</span>";
        } else {
            callback(true);
            document.getElementById("btn1").disabled = false;
            document.getElementById("btn3").disabled = false;
            document.getElementById("label").innerHTML = " ";
        }

    }
    Handsontable.validators.registerValidator('humidity', humidityValidator);

})(Handsontable);

(Handsontable);
(function (Handsontable) {
    function waterLevelValidator(a, callback) {
        if (a < 0 || a > 25) {
            callback(false);
            document.getElementById("btn1").disabled = true;
            document.getElementById("btn3").disabled = true;
            document.getElementById("label").innerHTML =
                "<span style='color:#FF0000'>One or more cells are incorrect</span>";
        } else {
            callback(true);
            document.getElementById("btn1").disabled = false;
            document.getElementById("btn3").disabled = false;
            document.getElementById("label").innerHTML = " ";
        }

    }
    Handsontable.validators.registerValidator('waterlevel1', waterLevelValidator);

})(Handsontable);
function getData() {
    return [
        { time: '00.00-02.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '02.00-04.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '04.00-06.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '06.00-08.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '08.00-10.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '10.00-12.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '12.00-14.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '14.00-16.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '16.00-18.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '18.00-20.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '20.00-22.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '22.00-24.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 }

    ];
};        
           
                var
                $$ = function (id) {
                    return document.getElementById(id);
                },
                container = $$('example1'),
                exampleConsole = $$('example1console'),
                hot;

            hot = new Handsontable(container, {
                data: getData(),
                licenseKey: 'non-commercial-and-evaluation',
                colWidths: 87,
                fillHandle: {
                    direction: 'vertical',
                    autoInsertRow: false
                },
                colHeaders: ['Time', 'Blue Light', 'Red Light', 'Temperature', 'Humidity', 'Waterlevel'],
                allowEmpty: false,
                columns: [
                    {
                        data: 'time',
                        readOnly: true
                    },
                    {
                        data: 'blueLight',
                        type: 'numeric',
                        validator: 'ligth'

                    },
                    {
                        data: 'redLight',
                        type: 'numeric',
                        validator: 'ligth'
                    },
                    {
                        data: 'temperature',
                        type: 'numeric',
                        validator: 'temp'
                    },
                    {
                        data: 'humidity',
                        type: 'numeric',
                        validator: 'humidity'
                    },
                    {
                        data: 'waterlevel',
                        type: 'numeric',

                        validator: 'waterlevel1'
                    },

                ],
            });
function save() {
    var scheduleName = prompt("Enter name of the schedule");
    if (scheduleName === "") { // do nothing
        document.getElementById("label").innerHTML = "no name was chosen";
    } else if (scheduleName) { // save schedule
        var scheduleNAME = scheduleName.trim();

        var schedule = JSON.stringify({ rawSchedule: hot.getData() });
        $.ajax({
            type: "POST",
            url: "saveSchedule",
            data: { rawSchedule: schedule, scheduleID: scheduleNAME }, //insert id as parameter
            success: function (data) {
                document.getElementById("label").innerHTML =
                    "Schedule" + " " + scheduleName + " " + "succesfully saved";
            }
        });
    } else { // do nothing
        document.getElementById("label").innerHTML = "user chose to cancel";
    }
}

function load() {

    $.ajax({
        type: "GET",
        url: "getScheduleNames",
        success: function (result) {
            for (id of result){
                var div = document.createElement("div");
                div.id = id;
                div.class = "listGroupItem";
                div.onclick = "loadSchedule(this.id)";
                document.getElementById("listView").appendChild(div);

            }

            console.log(result);
        }


    });
    document.getElementById("label").innerHTML =
        "Schedule" + " " + name + " " + "succesfully loaded";
}

function apply() {
    var schedule = JSON.stringify({ rawSchedule: hot.getData() });
    $.ajax({
        type: "POST",
        url: "saveSchedule",
        data: { rawSchedule: schedule, greenhouseID: greenhouseId }, //insert id as parameter
        success: function (data) {
            document.getElementById("label").innerHTML =
                "Schedule" + " " + greenhouseid + " " + "succesfully applied";
        }
    });

    document.getElementById("label").innerHTML =
        "Schedule" + " " + name + " " + "succesfully applied";
}
var greenhouseId = "";

function setGreenhouseID(id) {
    greenhouseId = id;
}

function getData() {
    return [
        { time: '00.00-02.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '02.00-04.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '04.00-06.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '06.00-08.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '08.00-10.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '10.00-12.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '12.00-14.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '14.00-16.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '16.00-18.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '18.00-20.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '20.00-22.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 },
        { time: '22.00-24.00', blueLight: 20, redLight: 20, temperature: 20, humidity: 20, waterlevel: 20 }

    ];
};
function showTable() {
    var
        $$ = function (id) {
            return document.getElementById(id);
        },
        container = $$('example1'),
        exampleConsole = $$('example1console'),
        hot;

    hot = new Handsontable(container, {
        data: getData(),
        licenseKey: 'non-commercial-and-evaluation',
        colWidths: 87,
        fillHandle: {
            direction: 'vertical',
            autoInsertRow: false
        },
        colHeaders: ['Time', 'Blue Light', 'Red Light', 'Temperature', 'Humidity', 'Waterlevel'],
        allowEmpty: false,
        columns: [
            {
                data: 'time',
                readOnly: true
            },
            {
                data: 'blueLight',
                type: 'numeric',
                validator: 'ligth'

            },
            {
                data: 'redLight',
                type: 'numeric',
                validator: 'ligth'
            },
            {
                data: 'temperature',
                type: 'numeric',
                validator: 'temp'
            },
            {
                data: 'humidity',
                type: 'numeric',
                validator: 'humidity'
            },
            {
                data: 'waterlevel',
                type: 'numeric',

                validator: 'waterlevel1'
            },

        ],
    });
}


