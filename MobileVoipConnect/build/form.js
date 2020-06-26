$(document).ready(function() {


    $("#savedata").click(function() {




        var settings = {
            "url": "https://api.mobilevoipconnect.com/Token",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/x-www-form-urlencoded"

            },
            "data": {
                "grant_type": "password",
                "username": $("#user").val(),
                "password": $("#password").val()
            }
        };

        $.ajax(settings).done(function(response) {


            var tokenizer = response.access_token;

            chrome.storage.sync.set({ key: tokenizer }, function() {
                console.log(tokenizer);
                alert('Credencial Set!')
            });


        });

    });


    $("span").click(function() {

        if (localStorage.getItem('token')) {

            var settings = {
                "dataType": "jsonp",
                "url": "https://devapi.mobilevoipconnect.com/api/1.0.0/Callback/" + localStorage.getItem('cbn'),
                "method": "GET",
                "timeout": 0,
                "headers": {
                    "Authorization": "Bearer " + localStorage.getItem('token')
                },
            };

            $.ajax(settings).done(function(response) {
                console.log(response);
            });
        } else {
            alert('Token Auth Missing Kindly Save Authentication Credencial');
        }
    });
});