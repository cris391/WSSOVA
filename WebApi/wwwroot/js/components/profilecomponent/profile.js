define(['knockout', 'dataService', 'store'], function (ko, ds, store) {
    var persons = ko.observableArray([]);

    $(document).ready(function () {

        $('#logout').click(function() {
            console.log('logout');
            localStorage.removeItem('token');
            localStorage.removeItem('username');
            Swal.fire(
                'Good job!',
                'You have now logged out',
                'success'
              )
        });

        const userSearchEndpoint = 'http://localhost:5001/api/search/history';
        const userAnnotationEndpoint = 'http://localhost:5001/api/markings';
        var userTokenString = localStorage.getItem('token');
        console.log(userTokenString);
        var userName = localStorage.getItem('username');
        var finalToken = userTokenString.slice(1, userTokenString.length - 1);
        console.log(finalToken);

        $('.welcome').append(userName);

        // Request User Search History Data
        requestUserSearchHistory = () => {
            $.ajax({
                url: userSearchEndpoint,
                dataType: 'JSON',
                type: 'GET',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + finalToken);
                },
            }).done(function (jData) {
                console.log(jData);
                for (var i = 0; i < jData.length; i++) {
                    $('.search-user-result').append('<tr>' +
                        '<th scope="row">' + i + '</th>' +
                        '<td>' + jData[i].queryText + jData[i].searchDate + '</td>' +
                        '</tr>');
                }
            }).fail(function (jFail) {
                console.log(jFail);
            })
          }

      // Request User Annotation & Markings
        requestMarkingAnnotationUserData = () => {
            $.ajax({
                url: userAnnotationEndpoint,
                dataType: 'JSON',
                type: 'GET',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + finalToken);
                },
            }).done(function (jData) {
              console.log(jData);
                for (var i = 0; i < jData.items.length; i++) {
                    console.log(jData.items[i].linkPost);
                    $('.user-mark-annotations').append(
                        '<tr>' +
                        '<th scope="row">' + i + '</th>' +
                        '<td><a href="'+ jData.items[i].linkPost + '">' + jData.items[i].title + '</a></td>' +
                        '</tr>');
                }
            }).fail(function (jFail) {
                console.log(jFail);
            })
        }

        requestMarkingAnnotationUserData();
        requestUserSearchHistory();
    });


    var selectPerson = function (person) {
        store.dispatch(store.actions.selectPerson(person));
        store.dispatch(store.actions.selectMenu("Component 2"));
        //postman.publish("selectperson", person);
    };


    ds.getNames(persons);

    return function (params) {
        return {
            persons,
            selectPerson
        };
    };
});
