require.config({
  baseUrl: 'js',
  paths: {
    jquery: '../lib/jquery/dist/jquery',
    knockout: '../lib/knockout/build/output/knockout-latest.debug'
  }
});

require(['knockout', 'app'], function(ko, app) {
  //console.log(app.name);

  ko.applyBindings(app);
});
