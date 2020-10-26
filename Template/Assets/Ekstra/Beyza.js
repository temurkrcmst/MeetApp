var MeetApp = (function (m) {

    m.Users = {
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
                url: 'http://localhost/MeetService/api/v1/AddUser'
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
                url: 'http://localhost/MeetService/api/v1/GetUser'
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
                url: 'http://localhost/MeetService/api/v1/DeleteUser'
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
                url: 'http://localhost/MeetService/api/v1/UpdateUser'
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



    $("#UserSave").click(function () {


        if ($('#Password').val() != $('#ConfirmPassword').val()) {
            return false;
        }

        var form = $("#addForm")


        if (form[0].checkValidity() == false) {
            event.preventDefault()
            event.stopPropagation()
        }
        else {

            var Name = $("#Name").val();
            var Surname = $("#Surname").val();
            var Degree = $("#Degree").val();
            var Email = $("#Email").val();
            var Phone = $("#Phone").val();
            var Password = $("#Password").val();
            var ConfirmPassword = $("#ConfirmPassword").val();
            var Authority = $("input[name='userType']:checked").val();
            var State = $("input[name='userState']:checked").val();
            var BranchID = $("#BranchID").val();


            var sendObject = {
                ID: "1",
                Name: Name,
                Surname: Surname,
                Degree: Degree,
                Email: Email,
                Phone: Phone,
                Password: Password,
                ConfirmPassword: ConfirmPassword,
                Authority: Authority,
                State: State,
                BranchID: BranchID

            };


            MeetApp.Users.Add(sendObject, function (data) {
                var alertSave = $('<div class="alert alert-success" >' + Name + " " + Surname + ' Başarıyla eklendi..</div>');

                if (data.Type == 0) {
                    $(alertSave).html("Kayıt ekleme esnasında hata oluştu..");
                    $(alertSave).removeClass("alert-success");
                    $(alertSave).addClass("alert-danger");
                }
                else {
                }

                $(".infoArea").html("").append(alertSave);
            });
        }
        form.addClass('was-validated');

    })

    $('#Password, #ConfirmPassword').on('keyup', function () {

        if ($('#Password').val() == $('#ConfirmPassword').val()) {
            $('#message').html('Şifreler eşleşti..').css('color', 'green');
        } else
            $('#message').html('Şifreler eşleşmiyor..').css('color', 'red');
    });

    $("#Update").click(function () {


        var Name = $("#Name").val();
        var Surname = $("#Surname").val();
        var Degree = $("#Degree").val();
        var Email = $("#Email").val();
        var Phone = $("#Phone").val();
        var Password = $("#Password").val();
        var ConfirmPassword = $("#ConfirmPassword").val();
        var Authority = $("input[name='userType']:checked").val();
        var State = $("input[name='userState']:checked").val();
        var BranchID = $("#BranchID").val();

        var form = $("#updateForm")

        $('#Password, #ConfirmPassword').on('keyup', function () {

            if ($('#Password').val() == $('#ConfirmPassword').val()) {
                $('#message').html('Şifreler eşleşti..').css('color', 'green');
            } else
                $('#message').html('Şifreler eşleşmiyor..').css('color', 'red');
        });


        if (form[0].checkValidity() == false) {
            event.preventDefault()
            event.stopPropagation()
        }
        else {

            var updateObject = {
                FilterCol: "ID",
                FilterVal: getID,
                Name: Name,
                Surname: Surname,
                Degree: Degree,
                Email: Email,
                Password: Password,
                ConfirmPassword: ConfirmPassword,
                Authority: Authority,
                State: State,
                Phone: Phone,
                BranchID: BranchID
            };

            MeetApp.Users.Edit(updateObject, function (data) {
                var alertUpdate = $('<div class="alert alert-success" >' + Name + " " + Surname + ' Başarıyla güncellendi..</div>');

                if (data.Type == 0) {
                    $(alertUpdate).html("Kayıt güncelleme esnasında hata oluştu..");
                    $(alertUpdate).removeClass("alert-success");
                    $(alertUpdate).addClass("alert-danger");
                }
                else {
                }

                $(".infoArea").html("").append(alertUpdate);

            });
        }
        form.addClass('was-validated');

    });

    $("#Delete").click(function () {
        var Name = $("#Name").val();
        var Surname = $("#Surname").val();
        var deleteObject = {
            FilterCol: "ID",
            FilterVal: getID
        };


        MeetApp.Users.Delete(deleteObject, function (data) {

            var alertDelete = $('<div class="alert alert-danger" >' + Name + " " + Surname + ' Başarıyla silindi..</div>');
            if (data.Type == 0) {
                $(alertDelete).html("Kayıt silme sırasında hata oluştu..");
                $(alertDelete).removeClass("alert-success");
                $(alertDelete).addClass("alert-danger");
            }
            else {
            }

            $(".infoArea").html("").append(alertDelete);

        });


    });
});