$(document).ready(function() {

    if (localStorage.getItem('token')) {
        $("#user").hide()
        $("#password").hide()
        $("#savedata").prop('value', 'Authentication Save');
        $('#savedata').prop('disabled', true);
    }

    $("#savedata").click(function() {
        alert('123')
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

            });

            chrome.storage.sync.get(['key'], function(result) {
                // alert(result.key);
            });

        });

    });


    $("span").click(function() {

        if (localStorage.getItem('token').length >= 10) {

            var settings = {
                "dataType": "jsonp",
                "url": "https://api.mobilevoipconnect.com/api/1.0.0/Callback/" + localStorage.getItem('cbn'),
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
            alert('Token Auth Missing kindly Save Authentication Data');

        }
    });
});