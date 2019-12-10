define(['knockout', 'dataService', 'store'], function (ko, ds, store) {
    var persons = ko.observableArray([]);

    $(document).ready(function () {

        const userSearchEndpoint = 'http://localhost:5001/api/search/history';
        const userAnnotationEndpoint = 'http://localhost:5001/api/markings';
        var userTokenString = localStorage.getItem('token');
        console.log(userTokenString);
        var userName = localStorage.getItem('username');
        //var userToken = JSON.parse(userTokenString);
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
                        '<td><a href="' + jData[i].linkPost + '">' + jData[i].queryText + jData[i].searchDate + '</a></td>' +
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
                //  console.log(jData);
                for (var i = 0; i < jData.items.length; i++) {
                    console.log(jData.items[i]);
                    $('.user-mark-annotations').append(
                        '<tr>' +
                        '<th scope="row">' + i + '</th>' +
                        '<a href="'+ jData.items[i].linkPost + '"> <td>  ' + jData.items[i].title + '</td></a>' +
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
