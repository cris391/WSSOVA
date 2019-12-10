require.config({
  baseUrl: 'js',
  paths: {
    jquery: '../lib/jquery/dist/jquery',
    knockout: '../lib/knockout/build/output/knockout-latest.debug',
    text: '../lib/requirejs-text/text',
    jqcloud: '../lib/jqcloud2/dist/jqcloud',
    dataService: 'services/dataService',
    postman: 'services/postman',
    store: 'services/store'
  },
  shim: {
    jqcloud: ['jquery']
  }
});

require(["knockout"], function (ko) {
    ko.components.register('cloud', {
        viewModel: { require: "components/wordcloud/cloud" },
        template: { require: "text!components/wordcloud/cloud.html" }
    });
    ko.components.register('parent', {
        viewModel: { require: "components/parent/parent" },
        template: { require: "text!components/parent/parent.html" }
    });
    ko.components.register('child', {
        viewModel: { require: "components/child/child" },
        template: { require: "text!components/child/child.html" }
    });
    ko.components.register('component1', {
        viewModel: { require: "components/component1/component1" },
        template: { require: "text!components/component1/component1.html" }
    });
    ko.components.register('component2', {
        viewModel: { require: "components/component2/component2" },
        template: { require: "text!components/component2/component2.html" }
    });
    ko.components.register('authcomponent', {
        viewModel: { require: "components/authcomponent/signup" },
        template: { require: "text!components/authcomponent/signup.html" }
    });
    ko.components.register('profilecomponent', {
        viewModel: { require: "components/profilecomponent/profile" },
        template: { require: "text!components/profilecomponent/profile.html" }
    });  ko.components.register('home-component', {
  ko.components.register('home-component', {
 	viewModel: { require: 'components/home-component/home-component' },
    template: { require: 'text!components/home-component/home-component.html' }
  });
  ko.components.register('search-component', {
    viewModel: { require: 'components/search-component/search-component' },
    template: { require: 'text!components/search-component/search-component.html' }
  });
  ko.components.register('post-component', {
    viewModel: { require: 'components/post-component/post-component' },
    template: { require: 'text!components/post-component/post-component.html' }
  });
});


require(['knockout', 'store', 'navbarApp'], function(ko, store, app) {
  store.subscribe(() => console.log(store.getState()));
  ko.applyBindings(app);
});
