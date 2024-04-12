mergeInto(LibraryManager.library, 
{
  ShowAlert: function (str) {
    window.alert("Текущее время в Москве: " + UTF8ToString(str))
  },
});