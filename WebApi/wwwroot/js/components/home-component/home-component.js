define(['knockout', 'dataService', 'store', 'navbarApp'], function(ko, ds, store, navbarApp) {
  var currentComponent = ko.observable('home-component');
  var posts = ko.observableArray([]);
  var postObs = ko.observable();
  var pageOfPosts = {};
  var search = ko.observable('');
  var isPost = ko.observable(false);

  var searchResult = ko.observableArray([]);

  search.subscribe(function(data) {
    console.log(data);
    if (data.length === 0) {
      searchResult([]);
      return;
    }
    var res = firstNames.filter(x => x.name.toLowerCase().startsWith(data));
    searchResult(res);
  });

  ds.getPosts(function(data) {
    posts(data.items);
    pageOfPosts = data;
  });
  var prev = function() {
    ds.getPostsWithJQuery(pageOfPosts.prev, function(data) {
      posts(data.items);
      pageOfPosts = data;
    });
  };

  var next = function() {
    ds.getPostsWithJQuery(pageOfPosts.next, function(data) {
      posts(data.items);
      pageOfPosts = data;
    });
  };

  var selectPost = function(post) {
    store.dispatch(store.actions.selectPost(post));
    navbarApp.currentComponent('post-component');
  };

  return function(params) {
    return {
      posts,
      prev,
      next,
      search,
      searchResult,
      postObs,
      selectPost,
      isPost
    };
  };
});
