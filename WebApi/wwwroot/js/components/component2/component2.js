define(['knockout', 'store'], function (ko, store) {
    return function () {
        var person = ko.observable(store.getState().selectedPerson);
        var cloudWidth = 1200 | '100%';
        var cloudHeight = 500 | 'auto';

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
