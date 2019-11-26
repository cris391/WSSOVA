define(['knockout', 'dataService'], function(ko, ds) {
  var posts = ko.observableArray([]);
  var pageOfPosts = {};

  ds.getPostsWithJQuery('api/questions', function(data) {
    console.log(data)
    posts(data.items);
    pageOfPosts = data;
    console.log(data);
  });

  var prev = function() {
    ds.getPostsWithJQuery(pageOfPosts.prev, function(data) {
      posts(data.items);
      pageOfPosts = data;
      console.log(data);
    });
  };

  var next = function() {
    ds.getPostsWithJQuery(pageOfPosts.next, function(data) {
      posts(data.items);
      pageOfPosts = data;
      console.log(data);
    });
  };

  return {
    posts,
    prev,
    next
  };
});
