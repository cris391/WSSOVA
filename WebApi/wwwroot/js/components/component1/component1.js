define(['knockout', 'dataService', 'store'], function (ko, ds, store) {
    var persons = ko.observableArray([]);
    
    var selectPerson = function (person) {
        store.dispatch(store.actions.selectPerson(person));
        store.dispatch(store.actions.selectMenu("Component 2"));
        //postman.publish("selectperson", person);
    };

    ds.getNames(persons);

    return function (params) {
        return {
            persons,
            selectPerson
        };
    };
});