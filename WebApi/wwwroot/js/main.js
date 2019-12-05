require.config({
  baseUrl: 'js',
  paths: {
    jquery: '../lib/jquery/dist/jquery',
    knockout: '../lib/knockout/build/output/knockout-latest.debug',
    dataService: 'services/dataService',
    postService: 'services/postService'
  }
});

require(['knockout', 'app'], function(ko, app, bs) {
  //console.log(app.name);

  ko.applyBindings(app);
});
