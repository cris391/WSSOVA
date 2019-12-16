define(['knockout', 'store'], function(ko, store) {
  var menuElements = [
    {
      name: 'Home',
      component: 'home-component'
    },
    {
      name: 'Search Posts',
      component: 'search-component'
    },
    {
      name: 'Component 2',
      component: 'component2'
    },
    {
      name: "Auth",
       component: "authcomponent"
    },
    {
       name: "Profile",
       component: "profilecomponent"
     },
    

  ];

  var currentMenu = ko.observable(menuElements[0]);
  var currentComponent = ko.observable(currentMenu().component);

  var changeContent = function(menu) {
    store.dispatch(store.actions.selectMenu(menu.name));
  };

  var changeComponentContent = function(data) {
    store.dispatch(store.actions.selectComponent(data));
  };

  store.subscribe(() => {
    var menuName = store.getState().selectedMenu;
    if (menuName != undefined) {
      var menu = menuElements.find(x => x.name === menuName);
      if (menu) {
        currentMenu(menu);
        currentComponent(menu.component);
      }
    }
  });

  var isSelected = function(menu) {
    return menu === currentMenu() ? 'active' : '';
  };

  return {
    currentComponent,
    menuElements,
    changeContent,
    isSelected,
    changeComponentContent
  };

});
