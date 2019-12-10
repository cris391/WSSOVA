define(['knockout', 'dataService', 'store'], function(ko, ds, store) {
  return function() {
    var post = ko.observable({
      question: {
        title: '',
        owner: {
          userDisplayName: '',
          creationDate: '',
          userLocation: ''
        },
        comments: []
      }
    });
    var tags = ko.observable('');

    const url = store.getState().selectedPost.linkPost;

    ds.getPost(url, function(data) {
      console.log(data);
      post(data);
      var array = data.tags.value;
      tags(array.split('::'));
    });
    return {
      post,
      tags
    };
  };
});
