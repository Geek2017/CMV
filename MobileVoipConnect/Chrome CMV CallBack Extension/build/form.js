$(document).ready(function() {

    chrome.storage.sync.get(['key1'], function(result) {
        console.log('Getting Tokeen:' + result.key1);
        if (result.key1) {
            $("#user").val(result.key1);
        }
    });

    chrome.storage.sync.get(['key2'], function(result) {
        console.log('Getting Tokeen:' + result.key2);
        if (result.key2) {
            $("#password").val(result.key2);
        }
    });

    chrome.storage.sync.get(['key3'], function(result) {
        console.log('Getting State:' + result.key3);
        if (result.key3 == "Y") {
            $('.login').attr("disabled", "disabled");
        } else {
            $('.logout').attr("disabled", "disabled");
        }
    });

    $(".logout").click(function(e) {
        e.preventDefault();
        let state = "N";
        chrome.storage.sync.set({ key3: state }, function() {
            console.log('Key3 Value is set to ' + state);
        });


        $('.logout').attr("disabled", "disabled");

        $(".login").attr("disabled", false);

        $("#notif").append('<div class="alert alert-danger"> Logout Successful! </div>');
        setTimeout(function() {
                chrome.tabs.getSelected(null, function(tab) {
                    chrome.tabs.reload(tab.id);
                });
                $("#notif").hide();
            },
            3000);

    })

    $(".login").click(function(e) {
        e.preventDefault();
        let user = $("#user").val();
        let pwd = $("#password").val();
        let state = "Y";

        chrome.storage.sync.set({ key1: user }, function() {
            console.log('Key1 Value is set to ' + user);
        });

        chrome.storage.sync.set({ key2: pwd }, function() {
            console.log('Key2 Value is set to ' + pwd);
        });

        chrome.storage.sync.set({ key3: state }, function() {
            console.log('Key3 Value is set to ' + state);
        });
        $('.login').attr("disabled", "disabled");


        $("#notif").append('<div class="alert alert-success">Credential Set:Login Successful! </div>');
        setTimeout(function() {
                chrome.tabs.getSelected(null, function(tab) {
                    chrome.tabs.reload(tab.id);
                });
                $("#notif").hide();
            },
            3000);

        $(".logout").attr("disabled", false);
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

                    $.ajax(settings).done(function(response) {
                        console.log(response.message);
                        alert('CallBack:' + response.message)

                    });
                });



            }
        }, 2000);
    });
});