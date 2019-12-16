define(['knockout', 'dataService', 'store'], function (ko, ds, store) {

    $(document).ready(function () {
        $('#logout').click(function() {
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
        var userName = localStorage.getItem('username');
        var finalToken = userTokenString.slice(1, userTokenString.length - 1);

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
                for (var i = 0; i < jData.length; i++) {
                  if(jData[i].queryText.length > 4){
                    $('.search-user-result').append('<tr>' +
                        '<th scope="row">' + i + '</th>' +
                        '<td class="title">' + '<h6 class="search-q">'+ jData[i].queryText.slice(1,jData[i].queryText.length -1)+ '</h6>' + '<p class="datetime">' + jData[i].searchDate + '</p>'+ '</td>' +

                        '</tr>');
                  }
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
                    $('.user-mark-annotations').append(
                        '<tr>' +
                        '<th scope="row">' + i + '</th>' +
                        '<td class="title"><a href="'+ jData.items[i].linkPost + '">' + jData.items[i].title + '</a></td>' +
                        '</tr>');
                }
            }).fail(function (jFail) {
                console.log(jFail);
            })
        }

        requestMarkingAnnotationUserData();
        requestUserSearchHistory();
    });


    return function (params) {
        return {
        };
    };
});
