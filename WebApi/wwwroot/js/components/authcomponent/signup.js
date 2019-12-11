define(['knockout', 'dataService', 'store'], function (ko, ds, store) {

    var user = ko.observableArray([]);

    $(document).ready(function () {

    // Signup new user 

        $('#signup').click(function (e) {
         e.preventDefault();

        var username = $('#username').val();
        var password = $('#password').val();
         var name = username; //$('#name').value();

        var dataObject = {
            username: username,
            name: name,
            password: password
        }

        $.ajax({
            url: "http://localhost:5001/api/auth/create/user",
            dataType: "json",
            type: "POST",
            processData: false,
            contentType: 'application/json',
            data: JSON.stringify(dataObject)
        }).done(function (jData) {
            console.log(jData);

        }).fail(function (jFail) {
            console.log('failed', jFail);
        })
    });


    // Authenticate user

      $('#login').click(function (e) {
        e.preventDefault();
        var username = $('#username').val();
        var password = $('#password').val();

        var loginData = {
            username: username,
            password: password
        }

        $.ajax({
            url: "http://localhost:5001/api/auth/token",
            dataType: "json",
            type: "POST",
            processData: false,
            contentType: 'application/json',
            data: JSON.stringify(loginData)

        }).done(function (jData) {
            var token = localStorage.setItem('token', JSON.stringify(jData.token));
            var username = localStorage.setItem('username', JSON.stringify(jData.username));
            console.log(token, username);

        }).fail(function (jFail) {
            console.log(jFail);
        })

    });

    });

      return function (params) {
        return {
            //persons,
           // selectPerson
        };
    };
});