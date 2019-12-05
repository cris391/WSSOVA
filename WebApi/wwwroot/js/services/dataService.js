define(['jquery'], function($) {
    var getPostsWithJQuery = function(url, callback) {
      $.getJSON(url, callback);
    };
  
    var getNamesWithFetch = function(callback) {
      fetch('api/names')
        .then(function(response) {
          return response.json();
        })
        .then(function(data) {
          callback(data);
        });
    };
  
    var getNamesWithFetchAsync = async function(callback) {
      var response = await fetch('api/posts');
      var data = await response.json();
      callback(data);
    };
  
    return {
      getPostsWithJQuery,
      getNamesWithFetch,
      getNamesWithFetchAsync
    };
  });
  