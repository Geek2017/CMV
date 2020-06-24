'use strict';

// Controller for View1
angular.module('newApp').controller('Main', function($scope) {



    $('#blur').css({ 'filter': 'blur(3px)' });
    $('#li-myaccount').hide();
    $('#btn-re-password').hide();
    $('#btn-create').hide();
    $('#btn-reset').hide();
    $('#link-login').hide();
    $('.infoi').show();

    $('#time-search').hide();
    $('.badge').hide();
    $('#ochat').hide();
    $('#navbarMenu').css({ 'filter': 'blur(3px)' });

    $scope.errorfound = function() {

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
        $('#btn-password').show();
    }

    $scope.islogout = function() {

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
        $('#time-search').hide();
        $('.badge').hide();
        $('#navbarMenu').css({ 'filter': 'blur(3px)' });

    }

    $scope.reset = function() {
        const email = $('#ipt-email').val();
        console.log(email);

        firebase.auth().sendPasswordResetEmail(email)
            .then(function() {
                alert('Reset link has been sent to provided email address');
                $scope.errorfound();
            })
    }

    $scope.islogin = function() {
        firebase.auth().onAuthStateChanged(function(user) {
            if (user && user.emailVerified) {

                $('#blur').css({ 'filter': 'blur(0px)' });
                $('.infoi').hide();
                $(".container").removeClass("navi");
                $('#li-myaccount').show();
                $('#time-search').show();
                $('.badge').show();
                $('#ochat').show();
                $('#navbarMenu').css({ 'filter': 'blur(0px)' });

            } else {
                $scope.islogout();
            }
            if (user.emailVerified == false) {
                alert('email not verified, please check your email for confirmation');
            }

        });
    }

    $scope.signin = function() {

        if ($('#ipt-email').val() != '' && $('#ipt-password').val() != '') {
            //login the user
            var data = {
                email: $('#ipt-email').val(),
                password: $('#ipt-password').val()
            };



            JSON.stringify(data);

            firebase.auth().signInWithEmailAndPassword(data.email, data.password)
                .then(function(authData) {


                    if (authData) {
                        $scope.islogin();
                        console.log(authData);
                    } else {
                        $scope.islogout();
                    }

                })
                .catch(function(error) {

                    console.log("Err:", error.message);
                    alert("Firebase Err:" + error.message);
                    $scope.islogout();
                });
        }
    }

    $scope.signup = function() {
        var data = {
            email: $('#ipt-email').val()
        };

        var passwords = {
            password: $('#ipt-password').val(), //get the pass from Form
            cPassword: $('#ipt-re-password').val(), //get the confirmPass from Form
        }

        if (data.email != '' && passwords.password != '' && passwords.cPassword != '') {
            if (passwords.password == passwords.cPassword) {
                //create the user

                firebase.auth()
                    .createUserWithEmailAndPassword(data.email, passwords.password)
                    .then(function(user) {

                        // if (user) {
                        //     user.updateProfile({
                        //         displayName: $('#cusname').val(),
                        //         photoURL: ""
                        //     })
                        // }

                        sendEmailVerification(data);
                        save_cus_credencials();

                        function sendEmailVerification(data) {
                            var email = $('#ipt-email').val();

                            $scope.errorfound(alert('Data Sent!'));

                            // cusname = firebase.auth().currentUser;
                            email = data.email || user.email;
                            var urlr = location.origin;

                            return user.emailVerified || user.sendEmailVerification({
                                url: urlr,
                            });

                        }
                        //save customer cred to firebase
                        function save_cus_credencials() {


                            var uid = firebase.database().ref().child('users').push().key;
                            var cusid = "2019"; //$('#cusid').val();
                            var cusname = "" //$('#cusname').val();
                            var cusemail = $('#ipt-email').val();



                            var data = {
                                user_id: uid,
                                cusid: cusid,
                                cusname: cusname,
                                cusemail: cusemail,
                                role: "1",
                                designation: "Admin",
                                userimage: "" //localStorage.getItem('userimgbase64')

                            }

                            var updates = {};
                            updates['/users/' + uid] = data;
                            firebase.database().ref().update(updates);


                        }


                    }).catch(function(error) {
                        console.log("Registration Failed!", error.message);
                        alert(error.message);
                        $scope.errorfound();
                    });


            }
        }
    }




    function trigger() {
        var myVar = setInterval(myTimer, 100);

        function myTimer() {
            getuserinfo();
        }

        function myStopFunction() {

            clearInterval(myVar);

        }

        setTimeout(function() {
            myStopFunction()

        }, 1000);

    }


    function getuserinfo() {

        //gefusersinfo
        var ref = firebase.database().ref("users");
        ref.orderByChild("cusemail").equalTo(localStorage.getItem('username')).on("child_added", function(snapshot) {
            console.log(snapshot.val());
            localStorage.setItem('cusname', snapshot.val().cusname);

            localStorage.setItem('childkey', snapshot.key)

            localStorage.setItem('cusname', snapshot.val().cusname);

            localStorage.setItem('role', snapshot.val().role);

            localStorage.setItem('mobile', snapshot.val().mobile);

            localStorage.setItem('phone', snapshot.val().phone);

            localStorage.setItem('designation', snapshot.val().designation);

            localStorage.setItem('userimg', snapshot.val().userimage);

            if (localStorage.getItem('userimg')) {
                $('#imagePreview').css('background-image', 'url(' + localStorage.getItem('userimg') + ')');
                $('#avatarsmall').attr('src', localStorage.getItem('userimg'));
            }
        });
    }



    $("#acctsettings").click(function() {
        getuserinfo();
        $('#u-username').val(localStorage.getItem('cusname'));
        $('#u-email').val(localStorage.getItem('username'));
        $('#u-phone').val(localStorage.getItem('phone'));
        $('#u-mobile').val(localStorage.getItem('mobile'));
        $('#avatarimg').attr('src', localStorage.getItem('userimg'));
        // $('#exampleModalCenter').modal({
        //     backdrop: 'static',
        //     keyboard: false
        // })
    });

    $('#updateprofile').click('submit', function() {

        var uid = firebase.database().ref().child('users').push().key;
        var data = {
            cusid: localStorage.getItem('curuserid'),
            cusname: $('#u-username').val(),
            cusemail: $('#u-email').val(),
            phone: $('#u-phone').val(),
            designation: localStorage.getItem('designation'),
            mobile: $('#u-mobile').val(),
            userimage: localStorage.getItem('userimg'),
            used_id: localStorage.getItem('childkey'),
            role: localStorage.getItem('role')
        }

        var updates = {};
        updates['/users/' + localStorage.getItem('childkey')] = data;
        firebase.database().ref().update(updates);

    });


    $("#form-login").submit(function(e) {
        $scope.execute();
        console.log('Trigger set....')
        trigger();
        e.preventDefault();
        $('#blur').css({ 'filter': 'blur(0px)' });
        $('.infoi').hide();
        $(".container").removeClass("navi");
    });

    $scope.execute = function() {

        var dbRef = firebase.database().ref().child('partner_data');
        dbRef.on('value', snapshot => {
            var data = snapshot.val();

            for (var i = 0; i <= data.length; i++) {
                var dbRef0 = firebase.database().ref().child('partner_data/' + i + '/custid');
                dbRef0.on('value', dataval => {

                    if (dataval.val() == $('#ipt-pid').val()) {
                        // console.log(dataval.val(),i);
                        // alert('Customer Validated AutoFill will take place');
                        var dbRef = firebase.database().ref().child('partner_data/' + i);
                        dbRef.on('value', calling => {
                            console.log('Data Fetch....')
                            console.log(calling.val());

                            localStorage.setItem('comadd', calling.val().company);
                            localStorage.setItem('comno', calling.val().phone);
                            localStorage.setItem('comadd', calling.val().address + ' ' + calling.val().state);

                        });



                    }

                });
            }

        })
    }

    $("#btn-logout").click(function() {
        $('#blur').css({ 'filter': 'blur(3px)' });
        $('#li-myaccount').hide();
        $('.infoi').show();
        $(".container").addClass("navi");
    });

    $("#link-reset").click(function() {
        $('#btn-login').hide();
        $('#btn-reset').show();
        $('#ipt-re-password').hide();
        $('#btn-password').hide();
        $('#link-login').show();
        $('#remember-me').hide();
        $('#midlepipe').hide();
        $('#link-create').hide();
        $('#link-reset').hide();
    });

    $("#link-create").click(function() {

        $('#btn-create').show();
        $('#btn-re-password').show();
        $('#link-login').show();
        $('#ipt-password').show();
        $('#ipt-re-password').show();
        $('#midlepipe').hide();

        $('#link-create').hide();
        $('#btn-login').hide();
        $('#link-reset').hide();
        $('#remember-me').hide();
    });

    $("#link-login").click(function() {
        $('#btn-login').show();
        $('#ipt-email').show();
        $('#btn-password').show();

        $('#btn-create').hide();
        $('#btn-reset').hide();
        $('#btn-re-password').hide();
        $('#link-login').hide();
        $('#link-create').show();
        $('#remember-me').show();
        $('#midlepipe').show();
        $('#link-reset').show();
    });

    firebase.auth().onAuthStateChanged(function(user) {
        if (user) {

            console.log('Current Login:', user)
            localStorage.setItem('username', user.email)
            $('#blur').css({ 'filter': 'blur(0px)' });
            $('.infoi').hide();
            $(".container").removeClass("navi");
            $('#li-myaccount').show();

            var fields = localStorage.getItem('username')
            var nf = fields.split('@');
            console.log(nf)
            console.log(nf[0])
            $('#uname').text(nf[0]).css("color", "red");
            $('#time-search').show();
            $('.badge').show();
            $('#ochat').show();
            $('#navbarMenu').css({ 'filter': 'blur(0px)' });
            $('#avatarsmall').attr('src', localStorage.getItem('userimg'));
        }
    });


    $("#ochat").click(function() {
        document.getElementById("myForm").style.display = "block";
    });

    $("#cchat").click(function() {
        document.getElementById("myForm").style.display = "none";
    });

});