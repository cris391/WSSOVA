define(['knockout', 'dataService', 'store'], function(ko, ds, store) {
  var searchValue = ko.observable('');
  var searchResult = ko.observableArray([]);
  const userSearchHistoryEndpoint = 'http://localhost:5001/api/search';
  const userAuthToken = localStorage.getItem('token');

  searchValue.subscribe(function(data) {
    console.log(data);
  });

  var search = (data, event) => {
    var sanitizedQuery = searchValue().split(' ').join('+');
    console.log(sanitizedQuery)
    if (sanitizedQuery.length === 0) {
      searchResult([]);
      return;
    }
    if (userAuthToken !== null) {
      searchPosts(sanitizedQuery);
    } else {
      Swal.fire('Please Login to Search', 'We help you store the search results', 'success');
    }
  };

  var searchPosts = query => {
    ds.searchPosts(query, function(data) {
      if (data == 204) {
        searchResult([]);
      } else {
        searchResult(data);
      }
    });
  };

  $(document).on('click', '.markPost', function(event) {
    //let id = event.target.id;
    let id = $(this).attr('id');
    if (userAuthToken.length > 1) {
      storeMarkingUsers(id);
    } else {
    }
  });

  storeMarkingUsers = id => {
    var selectedQuestionId = id;
    var url = 'http://localhost:5001/api/markings';
    var token = localStorage.getItem('token');

    const data = {
      postid: parseInt(id)
    };

    $.ajax({
      url: url,
      type: 'POST',
      dataType: 'json',
      processData: false,
      contentType: 'application/json',
      data: JSON.stringify(data),
      beforeSend: function(xhr) {
        xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(token));
      }
    })
      .done(function(jData) {
        console.log(jData);
        Swal.fire('Marked - Post', '' + jData.title, 'success');
      })
      .fail(function(jFail) {
        console.log(jFail);
      });
  };

  return function(params) {
    return {
      search,
      searchValue,
      searchResult,
      storeMarkingUsers
    };
  };
});
