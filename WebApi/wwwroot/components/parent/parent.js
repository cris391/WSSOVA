define(['knockout', 'dataService'], function (ko, ds) {
    var persons = ko.observableArray([]);
    var selectedPerson = ko.observable();

    var selectPerson = function(person) {
        selectedPerson(person);
    };

    ds.getNames(persons);

    return function(params) {
        return {
            persons,
            selectedPerson,
            selectPerson
        };
    };
});