define(['knockout', 'dataService', 'store', 'navbarApp'], function(ko, ds, store, navbarApp) {
  var searchValue = ko.observable('');
  var searchResult = ko.observableArray([]);
  const userSearchHistoryEndpoint = 'http://localhost:5001/api/search';
  const userAuthToken = localStorage.getItem('token');

  var search = (data, event) => {
    var sanitizedQuery = searchValue().split(' ').join('+');
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
        $('.search-result-total').text('Your Search Resulted in : ' + data.length + ' Posts Found');
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

  var selectPost = function(post) {
    store.dispatch(store.actions.selectPost(post));
    navbarApp.currentComponent('post-component');
  };
  return function(params) {
    return {
      search,
      searchValue,
      searchResult,
      storeMarkingUsers,
      selectPost
    };
  };
});
