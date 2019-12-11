define(['knockout', 'dataService', 'store'], function (ko, ds, store) {

  var search = ko.observable('');
  var searchResult = ko.observableArray([]);
  const userSearchHistoryEndpoint = "http://localhost:5001/api/search";
  const userAuthToken = localStorage.getItem('token');

  search.subscribe(function(data) {
    var sanitizedQuery = data.split(' ').join('+');
    if (sanitizedQuery.length === 0) {
      searchResult([]);
      return;
    }
    searchPosts(sanitizedQuery);
  });

  var searchPosts = query => {

    ds.searchPosts(query, function(data) {
      if (data == 204) {
        searchResult([]);
      } else {
          searchResult(data);
      }
    });
  };
/*
let index = 0;
index++;
setTimeout(function() {
      if(query.length >= 4 && 4 <= index){
        console.log(userSearchEndpoint + "?q=" + query);
          $.ajax({
            url: userSearchEndpoint + "?q=" + query,
            type: "POST",
            dataType: "json",
            processData: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + userAuthToken);
            },
          }).done(function(jData) {
            console.log('Successfully stored  user search ', jData);

          }).fail(function(jFail){
            console.log(jFail);
          })
      }
},4000)

*/



  $(document).on('click','.markPost', function(event) {
      let id = event.target.id;
      storeMarkingUsers(id);
  });

   storeMarkingUsers = (id) => {
       var selectedQuestionId = id;
       var url = "http://localhost:5001/api/markings";
       var token = localStorage.getItem('token');

       console.log(selectedQuestionId);

       const data = {
          postid: parseInt(id)
       };

       $.ajax({
           url: url,
           type: "POST",
           dataType: "json",
           processData: false,
           contentType: 'application/json',
           data: JSON.stringify(data),
           beforeSend: function(xhr) {
                   xhr.setRequestHeader('Authorization', 'Bearer ' + JSON.parse(token));
           },
       }).done(function(jData) {
          console.log(jData);

       }).fail(function(jFail){
          console.log(jFail);
       });
    };


  return function(params) {
    return {
      search,
      searchResult,
      storeMarkingUsers
    };
  };
});
