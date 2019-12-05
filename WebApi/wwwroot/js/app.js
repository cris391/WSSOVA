define(['knockout', 'store'], function(ko, store) {
  var currentComponent = ko.observable('parent');
  var currentParams = ko.observable({});

  var changeContent = () => {
    // if (currentComponent() === 'page1') {
    //   currentParams({ name: 'Ellen' });
    //   currentComponent('page2');
    // } else {
    //   currentParams({});
    //   currentComponent('page1');
    // }
  };

  //   var changeContent = function(menu) {
  //     store.dispatch(store.actions.selectMenu(menu.name));
  //   };

  store.subscribe(() => {
    console.log(123);
    var menuName = store.getState();
    console.log(menuName);

    // var menu = menuElements.find(x => x.name === menuName);
    // if (menu) {
    //   currentMenu(menu);
    //   currentComponent(menu.component);
    // }
  });

  return {
    currentComponent,
    currentParams,
    changeContent
  };
});
