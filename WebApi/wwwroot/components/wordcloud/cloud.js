define(["jquery", "knockout", "dataService", "jqcloud"], function($, ko, ds) {
    return function (params) {

        var width = params.width || 200;
        var height = params.height || 200;
        var words = [];

        var word = ko.observable();
        var weight = ko.observable();
        var addWord = function() {
            words.push({ text: word(), weight: weight() });
            word("");
            weight("");
            $('#cloud').jQCloud('update', words);
        };

        ds.getWords(data => {
            words = data;
            $('#cloud').jQCloud(words,
                {
                    width: width,
                    height: height
                });
        });

        return {
            word,
            weight,
            addWord
        };
    };
});