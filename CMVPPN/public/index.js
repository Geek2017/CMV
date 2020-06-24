'use strict';


angular.module('newApp').controller('indexdCtrl', function($scope) {



    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function(e) {
                $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
                $('#avatarsmall').attr('src', e.target.result);
                $('#imagePreview').hide();
                $('#imagePreview').fadeIn(650);
                localStorage.setItem('userimg', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);

        }
    }
    $("#imageUpload").change(function() {
        readURL(this);
    });



    // setInterval(function() {

    //     var divUtc = $('#clock');
    //     console.log(divUtc);
    //     var format = 'HH:mm:ss';

    //     // get current local time
    //     var now = moment().tz("America/New_York")

    //     // display the local time
    //     // divLocal.text(now.format(format));

    //     // switch to utc mode and display
    //     divUtc.text(moment.utc().format(format) + ' EST');

    //     // switch to another time zone and display
    //     // divOther.text(now.tz('America/Chicago').format(format));
    // }, 1000);




    $scope.logout = function() {
        firebase.auth().signOut().then(() => {
            console.log('LogOut!')
            $('#blur').css({ 'filter': 'blur(3px)' });
            $('#li-myaccount').hide();
            $('#btn-re-password').hide();
            $('#btn-create').hide();
            $('#btn-reset').hide();
            $('#link-login').hide();
            $('.infoi').show();
            $('#remember-me').show();
            $('#midlepipe').show();
            $('#link-reset').show();
            $('#link-create').show();
            $('#btn-login').show();
            $('.badge').hide();
            $('#time-search').hide();
            localStorage.clear();
            window.location.reload()
        });
    }



});