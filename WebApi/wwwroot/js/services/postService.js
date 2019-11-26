define(function() {

  var getPosts = async function(callback) {
    var response = await fetch('api/questions');
    var data = await response.json();
    callback(data);
  };

  return {
    getPosts
  };
});
