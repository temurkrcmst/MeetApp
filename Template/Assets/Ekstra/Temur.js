var MeetApp = (function (m) {
    m.MeetingHistories = {
        Add: function (obj, cb) {
            
            MeetApp.User.Check(function (data) {
                if (data == 0) {
                    window.location = "http://localhost/Meet/login.html";
                }
                else {

            $.ajax({
                type: 'POST',
                data: JSON.stringify(obj),
                dataType: "json",
                contentType: "application/json",
                url: 'http://localhost/MeetService/api/v1/AddMeetingHistory'
            }).done(function (data) {
                cb(data);
                        });
                }
            });
        },
        Get: function (params, cb) {
            var GetObj = {
                Column: "ID",
                ColumnType: "string",
                Value: "",
                Statement: ""
            };
            if (params != null) {
                GetObj = params;
            }


            $.ajax({
                type: 'POST',
                data: JSON.stringify(GetObj),
                dataType: "json",
                contentType: "application/json",
                url: 'http://localhost/MeetService/api/v1/GetMeetingHistory'
            }).done(function (data) {
                cb(data);

            });
        },
        Delete: function (params, cb) {
            MeetApp.User.Check(function (data) {
                if (data == 0) {
                    window.location = "http://localhost/Meet/login.html";
                }
                else {
            $.ajax({
                type: 'POST',
                data: JSON.stringify(params),
                dataType: "json",
                contentType: "application/json",
                url: 'http://localhost/MeetService/api/v1/Delete'
            }).done(function (data) {
                cb(data);
                        });
                }
            });
        },
        Edit: function (obj, cb) {
            MeetApp.User.Check(function (data) {
                if (data == 0) {
                    window.location = "http://localhost/Meet/login.html";
                }
                else {
            $.ajax({
                type: 'POST',
                data: JSON.stringify(obj),
                dataType: "json",
                contentType: "application/json",
                url: 'http://localhost/MeetService/api/v1/UpdateMeetingHistory'
            }).done(function (data) {
                cb(data);
                        });
                }
            });
        },

    }
    return m;
}(MeetApp || {}))


$(document).ready(function () {
    $("#MeetingHistorySave").click(function () {
        
        var title = $("#Title").val();
        var start = $("#StartDate").val();
        var finish = $("#FinishDate").val();
        var number = $("#ParticipantNumber").val();
        var information = $("#Salloon").val();
        var moderator = $("#Moderator").val();
        var branchıd = $("#BranchID").val();

        var sendObj = {
            ID: "1",
            Title: title,
            StartDate: start,
            FinishDate: finish,
            ParticipantNumber: number,
            Salloon: information,
            Moderator: moderator,
            BranchID: branchıd
        };

        if (start == "") {
            alert("Başlangıç süresi hatalı");
            return false;
        }

        if (finish == "") {
            alert("Bitiş süresi hatalı");
            return false;
        }   
        MeetApp.MeetingHistories.Add(sendObj, function (data) {
            var alert = $('<div class="alert alert-success">' + title + ' Başarıyla eklendi..</div>');

            if (data.Type == 0) {
                $(alert).html("Kayıt ekleme esnasında hata oluştu");
                $(alert).removeClass("alert-success");
                $(alert).addClass("alert-danger");
            }

            $(".infoArea").html("").append(alert);


        });

    });
    $("#MeetingHistoryDelete").click(function () {
        var deleteObj = {
            FilterCol: "ID",
            FilterVal: getID
        };

        MeetApp.MeetingHistories.Delete(deleteObj, function (data) {
            var alert = $('<div class="alert alert-success"> Başarıyla silindi..</div>');

            if (data.Type == 0) {
                $(alert).html("Kayıt silme esnasında hata oluştu");
                $(alert).removeClass("alert-success");
                $(alert).addClass("alert-danger");
            }
            $(".infoArea").html("").append(alert);
        });

    });
    $("#BtnUptadeMeetingHistory").click(function () {
        var title = $("#Title").val();
        var start = $("#StartDate").val();
        var finish = $("#FinishDate").val();
        var number = $("#ParticipantNumber").val();
        var branchıd = $("#BranchID").val();
        var information = $("#Salloon").val();
        var moderator = $("#Moderator").val();
        

        var updateObj = {
            FilterCol: "ID",
            FilterVal: getID,
            Title: title,
            StartDate: start,
            FinishDate: finish,
            ParticipantNumber: number,
            BranchID: branchıd,
            Salloon: information,
            Moderator: moderator
        };
        MeetApp.MeetingHistories.Edit(updateObj, function (data) {
            var alert = $('<div class="alert alert-success">' + title + ' Başarıyla Güncellendi..</div>');

            if (data.Type == 0) {
                $(alert).html("Kayıt güncelleme esnasında hata oluştu");
                $(alert).removeClass("alert-success");
                $(alert).addClass("alert-danger");
            }

            $(".infoArea").html("").append(alert);

        });

    });
});

//# sourceURL=temur.js