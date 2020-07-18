$(document).ready(function() {

    setTimeout(function() {
            chrome.storage.sync.get(['key1'], function(result) {
                console.log('Getting User:' + result.key1);
                if (result.key1) {
                    $("#user").val(result.key1);
                }
            });

            chrome.storage.sync.get(['key2'], function(result) {
                console.log('Getting Password:' + result.key2);
                if (result.key2) {
                    $("#password").val(result.key2);
                }
            });
        },
        3000);

    $("#logout").click(function() {
        localStorage.removeItem("user");
        localStorage.removeItem("pwd");

        $("#notif").append('<div class="alert alert-success">You Logout Successfully </div>');
        setTimeout(function() {
                $("#notif").hide();
            },
            3000);
    });

    $("#login").click(function() {
        if ($('#user').val() !== "" && $('#password').val() !== "") {

            localStorage.setItem('state', 1);

            let user = $("#user").val();
            let pwd = $("#password").val();

            chrome.storage.sync.set({ key1: user }, function() {
                console.log('key1 Value is set to ' + user);
            });

            chrome.storage.sync.set({ key2: pwd }, function() {
                console.log('Key2 Value is set to ' + pwd);
            });



            $("#notif").append('<div class="alert alert-success">  Success!  Credential save! </div>');
            setTimeout(function() {
                    $("#notif").hide();
                },
                3000);
        } else {
            $("#notif").append('<div class="alert alert-danger">  Error! UserName or Password cant be blank </div>');
            setTimeout(function() {
                    $("#notif").hide();
                },
                3000);

        }
    });


    $("svg").click(function() {
        console.log('Calling....')
        setTimeout(function() {
            let cbn = localStorage.getItem('cbn')

            if (localStorage.getItem('cbn') && localStorage.getItem('user') && localStorage.getItem('pwd')) {

                let username = localStorage.getItem('user')
                let password = localStorage.getItem('pwd')

                var settings = {
                    "url": "https://api.mobilevoipconnect.com/Token",
                    "method": "POST",
                    "timeout": 0,
                    "headers": {
                        "Content-Type": "application/x-www-form-urlencoded"
                    },
                    "data": {
                        "grant_type": "password",
                        "username": username,
                        "password": password
                    }
                };

                $.ajax(settings).done(function(response) {

                    console.log(response.userName);

                    var settings = {
                        "url": 'https://api.mobilevoipconnect.com/api/1.0.0/Callback/' + cbn,
                        "method": "GET",
                        "timeout": 0,
                        "headers": {
                            "Authorization": "Bearer " + response.access_token
                        }
                    };




                    $.ajax(settings).done(function(response, ) {
                        console.log(response.message);
                        alert('CallBack:' + response.message)
                    });


                });



            }
        }, 2000);
    });
});