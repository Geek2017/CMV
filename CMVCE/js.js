$("#fire").click(function() {
    alert('fire')
    let username = 'chromeapi';
    let password = 'Chrome@198';

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

        console.log(response);

        let number = '1594139733005';

        var settings = {
            "url": 'https://api.mobilevoipconnect.com/api/1.0.0/Callback/' + number,
            "method": "GET",
            "timeout": 0,
            "headers": {
                "Authorization": "Bearer " + response.access_token
            }
        };

        $.ajax(settings).done(function(response) {
            console.log(response);
        });
    });
});