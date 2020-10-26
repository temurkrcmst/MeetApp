var MeetApp = (function (m) {

    m.Salloons = {
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
                url: 'http://localhost/MeetService/api/v1/AddSalloon'
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
            }

            if (params != null) {
                GetObj = params;
            }

            $.ajax({
                type: 'POST',
                data: JSON.stringify(GetObj),
                dataType: "json",
                contentType: "application/json",
                url: 'http://localhost/MeetService/api/v1/GetSalloon'
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
                        url: 'http://localhost/MeetService/api/v1/DeleteSalloon'
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
                url: 'http://localhost/MeetService/api/v1/UpdateSalloon'
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

    $("#SalloonSave").click(function () {

        var form = $("#AddSalloonForm");
        if (form[0].checkValidity() == false) {
            event.preventDefault()
            event.stopPropagation()
        }
        else {
            var Branch = $("#Branch").val();
            var Capacity = $("#Capacity").val();
            var ProjectionStatus = $("#ProjectionStatus").val();
            var InternetStatus = $("input[name='optradio']:checked").val();
            var AirConditioningStatus = $("input[name='optradioo']:checked").val();
            var ProvisionsStatus = $("#ProvisionsStatus").prop("checked");
            var SalloonName = $("#SalloonName").val();
            //var BranchID = $("#BranchID").val();

            var sendObject = {
                ID: "1",
                Branch: Branch,
                Capacity: Capacity,
                ProjectionStatus: ProjectionStatus,
                InternetStatus: InternetStatus,
                AirConditioningStatus: AirConditioningStatus,
                ProvisionsStatus: ProvisionsStatus,
                SalloonName: SalloonName,
                //  BranchID: "1"
            };

            MeetApp.Salloons.Add(sendObject, function (data) {
                var alertSave = $('<div class="alert alert-success"> ' + Branch + ' Basarıyla eklendi..</div>');
                if (data.type == 0) {
                    $(alertSave).html("Kayıt ekleme esansında hata oluştu..");
                    $(alertSave).removeClass("alert-succees");
                    $(alertSave).addClass("alert-danger");
                }
                else {

                }
                $(".infoArea").html("").append(alertSave);
            });
        }
        form.addClass("was-validated");
    })

    $("#BtnUpdateSalloon").click(function () {

        var Branch = $("#Branch").val();
        var Capacity = $("#Capacity").val();
        var ProjectionStatus = $("#ProjectionStatus").val();
        var InternetStatus = $("input[name='optradio']:checked").val();
        var AirConditioningStatus = $("input[name='optradioo']:checked").val();
        var ProvisionsStatus = $("#ProvisionsStatus").prop("checked");
        var SalloonName = $("#SalloonName").val();
        //var BranchID = $("#BranchID").val();


        var sendObj = {
            FilterCol: "ID",
            FilterVal: getID,
            Branch: Branch,
            Capacity: Capacity,
            ProjectionStatus: ProjectionStatus,
            InternetStatus: InternetStatus,
            AirConditioningStatus: AirConditioningStatus,
            ProvisionsStatus: ProvisionsStatus,
            SalloonName: SalloonName,
            //BranchID: "1"
        };


        MeetApp.Salloons.Edit(sendObj, function (data) {
            var alertUpdate = $('<div class="alert alert-success"> ' + Branch + ' Basarıyla güncellendi..</div>');
            if (data.type == 0) {
                $(alertUpdate).html("Kayıt güncelleme esansında hata oluştu..");
                $(alertUpdate).removeClass("alert-succees");
                $(alertUpdate).addClass("alert-danger");
            }
            $(".infoArea").html("").append(alertUpdate);
        });
    });

    $("#BtnDeleteSalloon").click(function () {

        var Branch = $("#Branch").val();

        var deleteObject = {
            FilterCol: "ID",
            FilterVal: getID,
        };

        MeetApp.Salloons.Delete(deleteObject, function (data) {

            var alertDelete = $('<div class="alert alert-success"> ' + Branch + ' Basarıyla silindi..</div>');
            if (data.type == 0) {
                $(alertDelete).html("Kayıt silme esansında hata oluştu..");
                $(alertDelete).removeClass("alert-succees");
                $(alertDelete).addClass("alert-danger");
            }
            else {

            }
            $(".infoArea").html("").append(alertDelete);


        });
    });

});
