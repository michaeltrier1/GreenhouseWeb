var greenhouseID = "";

function setGreenhouseID(id) {
    stopLiveData();
    greenhouseID = id;   
}

function updateData() {
    var response;
    $.ajax({
        url: "getNewestData",
        data: {
            GreenhouseID: greenhouseID
        },
        success: function (data) {
            update(data);
        }
    });
  
    function update(data) {
        var internalTemp = [data.internalTemperature];
        var externalTemp = [data.externalTemperature];
        var humidity = [data.humidity];
        var waterlevel = [data.waterlevel];
        console.log(internalTemp);
        var minSetPoint = [15];
        var maxSetPoint = [30];
        var titleText = "";

        console.log(externalTemp);

        if (internalTemp[0] == null) {
            titleText = "Inside Temperature\n doesn't work";
        } else {
            titleText = 'Inside Temperature';
        }

        var chartData = {
            type: 'gauge',  // Specify your chart type here.
            "scale-r": {
                "aperture": 200,     //Specify your scale range.
                "values": "0:40:1"
            },
            title: {
                text: titleText // Adds a title to your chart

            },
            "series": [
                {
                    "values": minSetPoint,
                    "csize": "5%",
                    "size": "65%",
                    "background-color": "#CC0066"
                },
                {
                    "values": maxSetPoint,
                    "csize": "5%",
                    "size": "65%",
                    "background-color": "#CC0066"
                },
                {
                    "values": internalTemp,
                    "indicator": [0, 10, 0, 0, 0.3],
                    "csize": "10%",
                    "size": "70%",
                    "background-color": "#66CCFF #FFCCFF"

                }
            ]
        };
        zingchart.render({ // Render Method[3]
            id: 'InTemperature',
            data: chartData,
            height: 200,
            width: 300
        });

  
        if (externalTemp[0] == null) {
            titleText = "Outside Temperature\n doesn't work";
        } else {
            titleText = 'Outside Temperature';
        }
        var chartData = {
            type: 'gauge',  // Specify your chart type here.
            "scale-r": {
                "aperture": 200,     //Specify your scale range.
                "values": "0:40:1"
            },
            title: {
                text: titleText // Adds a title to your chart

            },
            "series": [
                {
                    "values": minSetPoint,
                    "csize": "5%",
                    "size": "65%",
                    "background-color": "#CC0066"
                },
                {
                    "values": maxSetPoint,
                    "csize": "5%",
                    "size": "65%",
                    "background-color": "#CC0066"
                },
                {
                    "values": externalTemp,
                    "indicator": [0, 10, 0, 0, 0.3],
                    "csize": "10%",
                    "size": "70%",
                    "background-color": "#66CCFF #FFCCFF"

                }
            ]
        };
        zingchart.render({ // Render Method[3]
            id: 'outTemperature',
            data: chartData,
            height: 200,
            width: 300
        });




        if (humidity[0] == null) {
            titleText = "Humidity\n doesn't work";
    } else {
        titleText = 'Humidity';
    }
        var chartData = {
            type: 'gauge',  // Specify your chart type here.
            "scale-r": {
                "aperture": 200,     //Specify your scale range.
                "values": "0:40:1"
            },
            title: {
                text: titleText // Adds a title to your chart

            },
            "series": [
                {
                    "values": minSetPoint,
                    "csize": "5%",
                    "size": "65%",
                    "background-color": "#CC0066"
                },
                {
                    "values": maxSetPoint,
                    "csize": "5%",
                    "size": "65%",
                    "background-color": "#CC0066"
                },
                {
                    "values": humidity,
                    "indicator": [0, 10, 0, 0, 0.3],
                    "csize": "10%",
                    "size": "70%",
                    "background-color": "#66CCFF #FFCCFF"

                }
            ]
        };
        zingchart.render({ // Render Method[3]
            id: 'Humidity',
            data: chartData,
            height: 200,
            width: 300
        });


        if (waterlevel[0] == null) {
            titleText = "Waterlevel\n doesn't work";
    } else {
        titleText = 'Waterlevel';
    }
        var chartData = {
            type: 'gauge',  // Specify your chart type here.
            "scale-r": {
                "aperture": 200,     //Specify your scale range.
                "values": "0:40:1"
            },
            title: {
                text: titleText // Adds a title to your chart

            },
            "series": [
                {
                    "values": minSetPoint,
                    "csize": "5%",
                    "size": "65%",
                    "background-color": "#CC0066"
                },
                {
                    "values": maxSetPoint,
                    "csize": "5%",
                    "size": "65%",
                    "background-color": "#CC0066"
                },
                {
                    "values": waterlevel,
                    "indicator": [0, 10, 0, 0, 0.3],
                    "csize": "10%",
                    "size": "70%",
                    "background-color": "#66CCFF #FFCCFF"

                }
            ]
        };
        zingchart.render({ // Render Method[3]
            id: 'Waterlevel',
            data: chartData,
            height: 200,
            width: 300
        });
}
    console.log(greenhouseID);
}


function stopLiveData() {
    if (greenhouseID != null) {
        $.ajax({
            type: "POST",
            url: "StopLiveData",
            data: { GreenhouseID: greenhouseID }, //insert id as parameter
            success: function (data) {
                document.getElementById("label").innerHTML =
                    "Live Data on Greenhouse with ID: " + " " + greenhouseid + " " + "succesfully stopped";
            }
        });
    } else {
        console.log("GreenhouseID is null");
    }
    
}