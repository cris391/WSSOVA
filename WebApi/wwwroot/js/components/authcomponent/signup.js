define(['knockout', 'dataService', 'store'], function (ko, ds, store) {

    var user = ko.observableArray([]);
    var menu = ko.observable

    $(document).ready(function () {

      // Signup new user
        $('#signup').click(function (e) {
         e.preventDefault();

        var username = $('#username').val();
        var password = $('#password').val();
        var name = username;

        // Validate input
        if(username.length < 3){
          $('#username').addClass('invalid-input');
        }
        if (password.length < 3){
          $('#password').addClass('invalid-input');
        }

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
            Swal.fire(
              'Good job!',
              'You Successfully Created a user',
              'success'
            )
            $('#signup').fadeOut(500);
            $('#signup').removeClass('invalid-input');
            $('#password').removeClass('invalid-input');
        }).fail(function (jFail) {
            Swal.fire(
              'Failed',
              'Seems like an error' + jFail,
              'error'
            )
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
            //if(token !== null && username.length > 0 && password.length > 0){}
              Swal.fire(
                'Good job!',
                'You Successfully logged in',
                'success'
              )
              // redirect
              store.dispatch(store.actions.selectMenu("Search Posts"));
        }).fail(function (jFail) {
            console.log(jFail);
            Swal.fire(
              'Failed',
              'Seems like an error' + jFail,
              'error'
            )
        })
    });

/*
    validateForm = () => {
        var forms = document.getElementsByClassName('auth-form');

        var validation = Array.prototype.filter.call(forms, function(form) {
          form.addEventListener('submit', function(event) {
            if(form.checkValidity() === false) {
              event.preventDefault();
              event.stopPropagation();
            }
            form.classList.add('was-validated');
          }, false);
        });
      }
*/

    });

      return function (params) {
        return {
        };
    };
});
