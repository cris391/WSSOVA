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


  $(document).on('click','.markPost', function(event) {
      //let id = event.target.id;
      let id = $(this).attr('id');
      storeMarkingUsers(id);
  });

   storeMarkingUsers = (id) => {
       var selectedQuestionId = id;
       var url = "http://localhost:5001/api/markings";
       var token = localStorage.getItem('token');

       console.log(selectedQuestionId);
       console.log(token);
       console.log(JSON.parse(token));

       const data = {
          postid: parseInt(id)
       };

        console.log(data);

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
          Swal.fire(
            'Marked - Post',
            '' + jData.title,
            'success'
          )

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
