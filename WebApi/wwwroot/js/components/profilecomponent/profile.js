define(['knockout', 'dataService', 'store'], function (ko, ds, store) {

  var userSearchHistory = ko.observableArray([]);

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


        var reqUserHistory = query =>  {
            ds.getUserSearchHistory(function(data){
              userSearchHistory(data);
              for (var i = 0; i < data.length; i++) {
              if(data[i].queryText.length > 4){
                $('.search-user-result').append('<tr>' +
                    '<th scope="row">' + i + '</th>' +
                    '<td class="title">' + '<h6 class="search-q">'+ data[i].queryText.slice(1,data[i].queryText.length -1)+ '</h6>' + '<p class="datetime">' + data[i].searchDate + '</p>'+ '</td>' +
                    '</tr>');
                }
              }
            });
        }

        var requestMarkingAnnotationUserData = query => {
            ds.getMarkingAnnotationData(function(data) {
              for (var i = 0; i < data.items.length; i++) {
                  $('.user-mark-annotations').append(
                      '<tr>' +
                      '<th scope="row">' + i + '</th>' +
                      '<td class="title"><a href="'+ data.items[i].linkPost + '">' + data.items[i].title + '</a></td>' +
                      '</tr>');
                }
            });
      }

        reqUserHistory();
        requestMarkingAnnotationUserData();
    });


    return function (params) {
        return {
        };
    };
});
