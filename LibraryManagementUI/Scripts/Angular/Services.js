var TitleService = angular.module('TitleService', [])
TitleService.factory('TitleService', function ($http) {
    var url = '/Title/';
    return {
        getTitles: function () {
            return $http.get(url + 'GetAllTitles')
        }
        ,
        getTitleCategories: function () {
            return $http.get(url + 'GetTitleCategories')
        }
        ,
        getLevels: function () {
            return $http.get(url + 'GetLevels')
        }
        ,
        updateTitles: function (Title) {
            return $http({ method: 'POST', url: url + 'AddUpdateTitle', data: Title, cache: false })
        }
        ,
        addTitle: function (Title) {
            return $http({ method: 'POST', url: url + 'AddUpdateTitle', data: Title, cache: false })
        }
        ,
        deleteTitle: function (id) {
            return $http.post(url + 'DeleteTitle?id=' + id)
        }
    };
});
var BookService = angular.module('BookService', [])
BookService.factory('BookService', function ($http) {
    var url = '/Book/';
    return {
        getTitles: function () {
            return $http.get(url + 'GetAllTitles')
        }
        ,
        getBookConditionList: function () {
            return $http.get(url + 'GetBookConditionList')
        }
        ,
        addBooks: function (Book, NoOfBooks) {
            return $http({ method: 'POST', url: url + 'AddBooks', data: { Book, NoOfBooks }, cache: false })
        }
    }
});
var CheckInService = angular.module('CheckInService', [])
CheckInService.factory('CheckInService', function ($http) {
    var url = '/CheckIn/';
    return {
        getFairies: function () {
            return $http.get(url + 'GetAllFairies')
        }        
        ,
        addCheckInRecords: function (addCheckIn, Barcodes) {
            return $http({ method: 'POST', url: url + 'AddCheckInRecords', data: { addCheckIn, Barcodes }, cache: false })
        }
    }
});