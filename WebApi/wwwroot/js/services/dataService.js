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

  var getPost = async function(url, callback) {
    var response = await fetch(url);
    var data = await response.json();
    callback(data);
  };

  var searchPosts = async function(query, callback) {
    var response = await fetch(`api/search?q=${query}`);
    if (response.status == 204) {
      callback(response.status);
    } else {
      var data = await response.json();
      callback(data);
    }
  };

  var getPostsWithJQuery = function(url, callback) {
    $.getJSON(url, callback);
  };

  return {
    getNames,
    getWords,
    getPosts,
    getPost,
    getPostsWithJQuery,
    searchPosts
  };
});
