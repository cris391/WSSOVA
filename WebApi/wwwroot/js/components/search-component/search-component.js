define(['knockout', 'dataService', 'store'], function(ko, ds, store) {
  var search = ko.observable('');
  
  var searchResult = ko.observableArray([]);
  
  search.subscribe(function(data) {
    var sanitizedQuery = data.split(' ').join('+');
    if (sanitizedQuery.length === 0) {
      searchResult([]);
      return;
    }

    searchPosts(sanitizedQuery);
  });

  var searchPosts = query => {
    ds.searchPosts(query, function(data) {
      if (data == 204) {
        searchResult([]);
      } else {
        searchResult(data);
      }
    });
  };

  return function(params) {
    return {
      search,
      searchResult
    };
  };
});
