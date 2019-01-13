var apiURI = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + "내APIKey";
$.ajax({
    url: apiURI,
    dataType: "json",
    type: "GET",
    async: "false",
    success: function(resp)
    {
        console.log(resp);
        console.log("현재온도 : " + resp.main.temp - 273.15);
        console.log("현재습도 : " + resp.main.humidity);
        console.log("날씨 : " + resp.weather[0].main);
        console.log("상세날씨설명:" + resp.weather[0].description);
    }
})