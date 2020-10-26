
function require(script) {
    $.ajax({
        url: script,
        dataType: "script",
        async: false,           // <-- This is the key
        success: function (data) {
            // all good...
        },
        error: function () {
            //throw new Error("Could not load script " + script);
        }
    });
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
};


require("Assets/Ekstra/Server.js");
require("Assets/Ekstra/Login.js");



require("Assets/Ekstra/Havva.js");
require("Assets/Ekstra/Beyza.js");
require("Assets/Ekstra/Ela.js");
require("Assets/Ekstra/Temur.js");


////MeetApp.User.Check(function (data) {
//    if (data == 0) {
//         //window.location = "http://localhost/Meet/login.html";
//    }
//    else {
//        //dn..
//    }
//});

$(document).ready(function () {

    var token = getUrlVars()["key"];

    if (typeof (token) != "function") {
        localStorage.setItem("token", token);

    }

    setTimeout(function () {

        MeetApp.User.Check(function (data) {
            if (data == 0) {
                //window.location = "http://localhost/Meet/login.html";
            }
            else {
                //dn..
            }
        });
    }, 10);


});

$(".navbar-sidenav a").click(function (event) {
    if ($(event.target).attr("data-toggle") == undefined) {
        var page = $(event.target).attr('href');
        if (page != undefined) {
            var href = $(event.target).attr('href') + "?key=" + localStorage.getItem("token");

            // alert(href);
            event.preventDefault();
            window.location = href;

        }
    }
});