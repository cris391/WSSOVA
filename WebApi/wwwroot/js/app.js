define(["knockout"], function (ko) {
  var firstName = ko.observable("Peter");
  var lastName = ko.observable("Smith");

  var fullName = ko.computed(function() {
      return firstName() + " " + lastName();
  });

  return {
      firstName,
      lastName,
      fullName
  };
});