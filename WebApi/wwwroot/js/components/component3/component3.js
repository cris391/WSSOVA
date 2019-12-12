define(['knockout', 'store'], function (ko, store) {
    return function () {
        var home = ko.observable(store.getState().selectedHome);


        store.subscribe(function () {
            var state = store.getState();
            home(stat.selectedHome);
        });
        return {};
    };
});

