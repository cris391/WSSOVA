define(['jquery'], function($) {
  var getNames = async function(callback) {
    var response = await fetch('api/names');
    var data = await response.json();
    callback(data);
  };

  var getWords = async function(callback) {
    var response = await fetch('api/words');
    var data = await response.json();
    callback(data);
  };

  var getPosts = async function(callback) {
    var response = await fetch('api/questions');
    var data = await response.json();
    callback(data);
  };

  var searchPosts = async function(query, callback) {
    const token = localStorage.getItem('token');
    var finalToken = token.slice(1, token.length - 1);
    if(query.length > 2) {
      var response = await fetch(`api/search?q=${query}`, {
        method: 'GET',
        headers: {
          'Authorization': 'Bearer ' + finalToken
        },
      })
      if (response.status == 204) {
        callback(response.status);
      } else {
        var data = await response.json();
        callback(data);
      }
    }
  };

  var getPostsWithJQuery = function(url, callback) {
    $.getJSON(url, callback);
  };

  return {
    getNames,
    getWords,
    getPosts,
    getPostsWithJQuery,
    searchPosts
  };
});
