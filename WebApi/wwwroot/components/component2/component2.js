define(['knockout', 'store'], function (ko, store) {
    return function () {
        var person = ko.observable(store.getState().selectedPerson);
        var cloudWidth = 300;
        var cloudHeight = 300;

        store.subscribe(function() {
            var state = store.getState();
            person(state.selectedPerson);
        });
        //postman.subscribe("selectperson", person);
        
        return {
            person,
            cloudWidth,
            cloudHeight
        };
    };
});