var MeetApp = (function (m) {

    m.Branches = {

        Add: function (params, cb) {
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
                url: 'http://localhost/MeetService/api/v1/AddBranch'
            }).done(function (data) {
                cb(data);
                        });
                }
            });
        },
        Get: function (params, cb) {


            var GetObj = {
                Column: "",
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
                url: 'http://localhost/MeetService/api/v1/GetAllBranches'
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
                        url: 'http://localhost/MeetService/api/v1/DeleteBranch'
                    }).done(function (data) {
                        cb(data);
                    });
                }
            });
        },
        Edit: function (params, cb) {
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
                url: 'http://localhost/MeetService/api/v1/UpdateBranch'
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
    $("#BtnSaveBranch").click(function () {
        var form = $("#addBranchForm");

        if (form[0].checkValidity() == false) {
            event.preventDefault()
            event.stopPropagation()
        }
        else {


            var name = $("#Name").val();
            var address = $("#Address").val();
            var phone = $("#Phone").val();

            var sendObj = {
                ID: "1",
                Name: name,
                Address: address,
                Phone: phone
            };

            //MeetApp.Process.Run("Add", "Branch", sendObj, function (data) {

            //    var alert = $('<div class="alert alert-success" >Kayıt Başarıyla Eklendi..</div>');

            //    if (data.Type == 0) {
            //        $(alert).html("Kayıt ekleme esnasında hata oluştu..");
            //        $(alert).removeClass("alert-success");
            //        $(alert).addClass("alert-danger");
            //    }


            //    $(".infoArea").html("").append(alert);

            //});



            MeetApp.Branches.Add(sendObj, function (data) {
                var alert = $('<div class="alert alert-success" >Kayıt Başarıyla Eklendi..</div>');


                if (data.Type == 0) {
                    $(alert).html("Kayıt ekleme esnasında hata oluştu..");
                    $(alert).removeClass("alert-success");
                    $(alert).addClass("alert-danger");
                }


                $(".infoArea").html("").append(alert);
            });



        }
        form.addClass('was-validated');
    });

    $("#BtnUpdateBranch").click(function () {
        var form = $("#addBranchForm");

        if (form[0].checkValidity() == false) {
            event.preventDefault()
            event.stopPropagation()
        } else {
            var name = $("#Name").val();
            var address = $("#Address").val();
            var phone = $("#Phone").val();

            var updateObj = {
                FilterCol: "ID",
                FilterVal: getID,
                Name: name,
                Address: address,
                Phone: phone
            };

            MeetApp.Branches.Edit(updateObj, function (data) {
                console.log(data);

                var alert = $('<div class="alert alert-success" >Kayıt Başarıyla Güncellendi..</div>');


                if (data.Type == 0) {
                    $(alert).html("Kayıt güncelleme esnasında hata oluştu..");
                    $(alert).removeClass("alert-success");
                    $(alert).addClass("alert-danger");
                }


                $(".infoArea").html("").append(alert);
            });

            form.addClass('was-validated');
        }
    })

    $("#BtnDeleteBranch").click(function () {

        var form = $("#addBranchForm");

        if (form[0].checkValidity() == false) {
            event.preventDefault()
            event.stopPropagation()
        } else {

            var deleteObj = {
                FilterCol: "ID",
                FilterVal: getID,
            };

            MeetApp.Branches.Delete(deleteObj, function (data) {
                var alert = $('<div class="alert alert-success" >Kayıt Başarıyla Silindi..</div>');


                if (data.Type == 0) {
                    $(alert).html("Kayıt silme esnasında hata oluştu..");
                    $(alert).removeClass("alert-success");
                    $(alert).addClass("alert-danger");
                }

                $(".infoArea").html("").append(alert);
            });

        }
        form.addClass('was-validated');

    });

});
//# sourceURL=havva.js