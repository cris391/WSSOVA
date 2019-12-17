define(['jquery'], function($) {
        const userSearchEndpoint = 'http://localhost:5001/api/search/history';
        const userAnnotationEndpoint = 'http://localhost:5001/api/markings';
/*
  var authenticateUser = function(callback) {
    const token = localStorage.getItem('token');
    const authenticated = false;
    var finalToken = token.slice(1, token.length - 1);
    if(finalToken !== null){
      console.log(currentMenu);
      console.log('remove or add ');
       authenticated = true;
     }
     authenticated = false;
    callback(authenticated);
  }*/

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
    const token = localStorage.getItem('token');
    var finalToken = token.slice(1, token.length - 1);
    if (query.length > 2) {
      var response = await fetch(`api/search?q=${query}`, {
        method: 'GET',
        headers: {
          Authorization: 'Bearer ' + finalToken
        }
      });
      if (response.status == 204) {
        callback(response.status);
      } else {
        var data = await response.json();
        callback(data);
      }
    }
  };

  var getHistory = async function(callback) {
    let token = localStorage.getItem('token');
    token = token.slice(1, token.length - 1);

    var response = await fetch(`api/search/history`, {
      method: 'GET',
      headers: {
        Authorization: 'Bearer ' + token
      }
    });

    var data = await response.json();
    callback(data);
  };

  var getPostsWithJQuery = function(url, callback) {
    $.getJSON(url, callback);
  };


  var getUserSearchHistory = async function(callback){
      const token = localStorage.getItem('token');
      var finalToken = token.slice(1, token.length - 1);
        var response = await fetch(userSearchEndpoint, {
          method: 'GET',
          headers: {
            Authorization: 'Bearer ' + finalToken
          }
        });

        var data = await response.json();
        callback(data);
  }


  var getMarkingAnnotationData = async function(callback){
    const token = localStorage.getItem('token');
    var finalToken = token.slice(1, token.length - 1);

    var response = await fetch(userAnnotationEndpoint, {
      method: 'GET',
      headers: {
        Authorization: 'Bearer ' + finalToken
      }
    });
    var data = await response.json();
    callback(data);
  }


  return {
    getNames,
    getWords,
    getPosts,
    getPost,
    getPostsWithJQuery,
    searchPosts,
    getHistory,
    getUserSearchHistory,
    getMarkingAnnotationData
  };
});
