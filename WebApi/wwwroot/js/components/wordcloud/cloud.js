define(['jquery', 'knockout', 'dataService', 'jqcloud'], function($, ko, ds) {
  return function(params) {
    var width = params.width || 200;
    var height = params.height || 200;
    var words = [];

    var word = ko.observable();
    var weight = ko.observable();

    var addWord = function() {
      words.push({ text: word(), weight: weight() });
      word('');
      weight('');
      $('#cloud').jQCloud('update', words);
    };

    ds.getHistory(data => {
      let totalTerms = [];
      for (let index = 0; index < data.length; index++) {
        const searchTerms = data[index].queryText.slice(0, data[index].queryText.length - 1);
        const terms = searchTerms.split(',');
        totalTerms.push(...terms);
      }
      console.log(totalTerms);
      mapWords(totalTerms);
    });

    function mapWords(terms) {
      terms.sort();

      var current = null;
      var cnt = 0;
      for (var i = 0; i < terms.length; i++) {
        if (terms[i] != current) {
          if (cnt > 0) {
            words.push({ text: current, weight: cnt });
          }
          current = terms[i];
          cnt = 1;
        } else {
          cnt++;
        }
      }
      if (cnt > 0) {
        words.push({ text: current, weight: cnt });
      }

      $('#cloud').jQCloud(words, {
        width: width,
        height: height
      });
    }

    return {
      word,
      weight,
      addWord
    };
  };
});
