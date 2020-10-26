var MeetApp = (function (m) {
    m.User = {
        Login: function (obj, cb) {

            $.ajax({
                type: 'POST',
                data: JSON.stringify(obj),
                dataType: "json",
                contentType: "application/json",
                url: 'http://localhost/MeetService/api/v1/Session/Login'
            }).done(function (data) {
                cb(data);
            });
        },
        Check: function (cb) {
            var key = localStorage.getItem("token");
            if (key != undefined) {
                if (key != "") {
                    $.ajax({
                        type: 'POST',
                        data: JSON.stringify({ Token: key }),
                        dataType: "json",
                        contentType: "application/json",
                        url: 'http://localhost/MeetService/api/v1/Session/Check',
                        error: function (xhr, exception) {
                            cb(0);
                        }
                    }).done(function (data) {
                        if (data.Type == 0) {
                            cb(0);
                        }
                        else if (data.Type == 1) {
                            cb(1);

                        }
                        else {
                            cb(0);
                        }
                    });

                }
            }
            else {
                cb(0);
            }
        }
    }
    return m;
}(MeetApp || {}))



$(document).ready(function () {

    $("#UserLogin").click(function () {
        var Email = $("#LoginEmail").val();
        var Password = $("#LoginPassword").val();

        var sendObject = {
            usr: Email,
            pwd: Password,
            slidingExpiration: 60,
            appName: "web",
            deviceId: "",
        };

        //$(function () {
        //    var current_progress = 0;
        //    var interval = setInterval(function () {
        //        current_progress += 20;
        //        $("#dynamic")
        //            .css("width", current_progress + "%")
        //            .attr("aria-valuenow", current_progress)
        //            .text(current_progress + "%");
        //        if (current_progress >= 100)
        //            clssearInterval(interval);
        //    }, 1000);
        //});

        MeetApp.User.Login(sendObject, function (data) {

            if (data.Success == 0) {
                window.location = "login.html";
            }
            else {

                var inc = 1;
                var int = setInterval(function () {

                    if (inc < 100) {
                        $("#dynamic").css({ "width": inc + "%" });
                        inc += 1;
                    }
                    else {
                        clearInterval(int);
                    }
                }, 1);
                setTimeout(function () {
                    window.location = "index.html?key=" + data.Token;
                }, 2000);

            }
        })

    });

});


//# sourceURL=login.js