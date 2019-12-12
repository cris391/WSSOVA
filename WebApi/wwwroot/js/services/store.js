define([], function() {
  const selectPerson = 'SELECT_PERSON';
  const selectMenu = 'SELECT_MENU';
  const selectComponent = 'SELECT_COMPONENT';
  const selectPost = 'SELECT_POST';
  const authenticatedUser = 'AUTHENTICATED_USER';

  var subscribers = [];

  var currentState = {};

  var getState = () => currentState;

  var subscribe = function(callback) {
    subscribers.push(callback);

    return function() {
      subscribers = subscribers.filter(x => x !== callback);
    };
  };

  var reducer = function(state, action) {
    switch (action.type) {
      case selectPerson:
        return Object.assign({}, state, { selectedPerson: action.selectedPerson });
      case selectMenu:
        return Object.assign({}, state, { selectedMenu: action.selectedMenu });
      case selectComponent:
        return Object.assign({}, state, { selectedComponent: action.selectComponent });
      case selectPost:
        return Object.assign({}, state, { selectedPost: action.selectedPost, selectedComponent: 'post-component' });
      default:
        return state;
    }
  };

  var dispatch = function(action) {
    currentState = reducer(currentState, action);
    console.log(action)

    subscribers.forEach(callback => callback());
  };

  var actions = {

    authenticateuser: function(user) {
      return {
        type: selectUser,
        selectedUser: user
      };
    },
    selectPerson: function(person) {
      return {
        type: selectPerson,
        selectedPerson: person
      };
    },
    selectMenu: function(menu) {
      return {
        type: selectMenu,
        selectedMenu: menu
      };
    },
    selectComponent: function(component) {
      return {
        type: selectComponent,
        selectedComponent: component
      };
    },
    selectPost: function(post) {
      return {
        type: selectPost,
        selectedPost: post,

      };
    }
  };

  return {
    getState,
    subscribe,
    dispatch,
    actions
  };
});
