var myApp = angular.module('myApp', ['TitleService', 'BookService', 'CheckInService', 'ui.bootstrap', 'ngSanitize']);

myApp.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});


myApp.controller('TitleCtrl', function (
    $scope,
    TitleService) {
    $scope.display = true;
    $scope.TitlesList = [];
    $scope.TitleCategoryList = [];
    $scope.LevelList = [];
    $scope.loadData = false;
    $scope.tempTitles = [];
    $scope.EditingRows = [];
    $scope.b = { filterSplzation: [] };
    $scope.filterName = '';
    $scope.filterAuthor = '';
    $scope.filterPublisher = '';
    $scope.filterTitleCategory = { Name: '' };
    $scope.filterLevel = { Name: '' };
    $scope.ErrorMessage = '';
    $scope.loadError = false;

    $scope.GetTitles = function () {
        TitleService.getTitles().then((function (data) {
            $scope.loadData = true;
            $scope.TitlesList = data.data;
        }), (function (error, status) { $scope.loadData = false; }))
    };
    $scope.GetTitles();

    $scope.GetTitleCategories = function () {
        TitleService.getTitleCategories().then((function (data) {
            $scope.loadData = true;
            $scope.TitleCategoryList = data.data;
        }), (function (error, status) { $scope.loadData = false; }))
    };
    $scope.GetTitleCategories();

    $scope.GetLevels = function () {
        TitleService.getLevels().then((function (data) {
            $scope.loadData = true;
            $scope.LevelList = data.data;
        }), (function (error, status) { $scope.loadData = false; }))
    };
    $scope.GetLevels();

    $scope.UpdateTitle = function (Title) {
        TitleService.updateTitles(Title).then((function (data) {
            $scope.GetTitles();
            $scope.HideEditing();
            $scope.tempTitles = {};
        }), (function (error, status) { }))
    };

    $scope.AddTitle = function (Title, isValid) {
        if (isValid) {
            TitleService.addTitle(Title).then((function (data) {
                $scope.loadError = true;
                $scope.ErrorMessage = data.data.Message;
                $scope.GetTitles();
            }), (function (error) { }))
                .finally(function () {
                    $scope.addTitle = null;
                    $scope.TitleForm.$setPristine();
                });
        } else {
            alert('Please provide valid inputs');
        }
    };

    $scope.DeleteTitle = function (id) {
        TitleService.deleteTitle(id).then((function (data, status) {
            $scope.GetTitles();
        }), (function (error) { }))
    };

    $scope.HideEditing = function () {
        for (var index = 0; index < $scope.TitlesList.length; index++) {
            $scope.EditingRows[index] = false;
        }
    };

    $scope.Editing = function (Title, index) {
        $scope.tempTitles[index] = angular.copy(Title);
        $scope.EditingRows[index] = !$scope.EditingRows[index];
    };

    $scope.CancelEditing = function (Title, index) {
        $scope.TitlesList[index] = $scope.tempTitles[index];
        $scope.display = true;
        $scope.EditingRows[index] = !$scope.EditingRows[index];
    };

    //OR filter
    //$scope.filterSplz = function (obj) {
    //    var res = false;
    //    if (!$scope.b.filterSplzation.length) return true;
    //    obj.Specializations.forEach(function (element) { if ($scope.b.filterSplzation.toString().indexOf(element) >= 0) { res = true; } });
    //    return res;
    //};

    //AND filter
    $scope.filterTitleList = function (obj) {

        if ($scope.filterName == ''
            && $scope.filterAuthor == ''
            && $scope.filterPublisher == ''
            && $scope.filterTitleCategory.Name == ''
            && $scope.filterLevel.Name == '') return true;
        return obj.Name.toLowerCase().indexOf($scope.filterName.toLowerCase()) != -1
            && obj.Author.toLowerCase().indexOf($scope.filterAuthor.toLowerCase()) != -1
            && obj.Publisher.toLowerCase().indexOf($scope.filterPublisher.toLowerCase()) != -1
            && obj.TitleCategory.toLowerCase().indexOf($scope.filterTitleCategory.Name.toLowerCase()) != -1
            && ($scope.filterLevel.Name == '' || obj.Level == $scope.filterLevel.Name);
    };
})
    .controller('BookCtrl', function (
        $scope,
        $window,
        $location,
        BookService) {

        $scope.TitlesList = [];
        $scope.BookConditionList = [];
        $scope.loadData = false;
        $scope.IssuedStatusList = {
            Yes: 1,
            No: 0
        };

        $scope.BarcodeList = [];

        $scope.GetTitles = function () {
            BookService.getTitles().then((function (data) {
                $scope.loadData = true;
                $scope.TitlesList = data.data;
            }), (function (error, status) { $scope.loadData = false; }))
        };
        $scope.GetTitles();

        $scope.GetBookConditionList = function () {
            BookService.getBookConditionList().then((function (data) {
                $scope.loadData = true;
                $scope.BookConditionList = data.data;
            }), (function (error, status) { $scope.loadData = false; }))
        };
        $scope.GetBookConditionList();

        $scope.AddBooks = function (Book, NoOfBooks, isValid) {
            if (isValid) {
                BookService.addBooks(Book, NoOfBooks).then((function (data) {
                    $scope.BarcodeList = chunk(data.data, 2);
                    //$location.url('http://localhost:92/Book/Barcode');
                    //$window.open('/Book/Barcode', '_blank');
                }), (function (error) { }))
                    .finally(function () {
                        $scope.addBook = null;
                        $scope.NoOfBooks = null;
                        $scope.BookForm.$setPristine();
                    });
            } else {
                alert('Please provide valid inputs');
            }
        };
        function chunk(arr, size) {
            var newArr = [];
            for (var i = 0; i < arr.length; i += size) {
                newArr.push(arr.slice(i, i + size));
            }
            return newArr;
        }

        var today = new Date();
        $scope.dateFormat = 'dd-MMMM-yyyy';

        $scope.availablePurchaseDatePopup = {
            opened: false
        };
        $scope.OpenAvailablePurchaseDate = function () {
            $scope.availablePurchaseDatePopup.opened = !$scope.availablePurchaseDatePopup.opened;
        };
        $scope.availableLostDatePopup = {
            opened: false
        };
        $scope.OpenAvailableLostDate = function () {
            $scope.availableLostDatePopup.opened = !$scope.availableLostDatePopup.opened;
        };
    })
    .controller('CheckInCtrl', function (
        $scope,
        CheckInService) {

        $scope.FairyList = [];
        $scope.dateFormat = 'dd-MMMM-yyyy';
        $scope.loadData = false;
        //$scope.Barcodes = [];

        $scope.GetFairies = function () {
            CheckInService.getFairies().then((function (data) {
                $scope.loadData = true;
                $scope.FairyList = data.data;
            }), (function (error, status) { $scope.loadData = false; }))
        };
        $scope.GetFairies();

        $scope.availableLostDatePopup = {
            opened: false
        };
        $scope.OpenAvailableDate = function () {
            $scope.availableLostDatePopup.opened = !$scope.availableLostDatePopup.opened;
        };

        $scope.AddCheckInRecord = function (addCheckIn, Barcodes, isValid) {
            if (isValid) {
                CheckInService.addCheckInRecords(addCheckIn, Barcodes).then((function (data) {
                }), (function (error) { }))
                    .finally(function () {
                        $scope.addCheckIn = null;
                        $scope.Barcodes = null;
                        $scope.CheckInForm.$setPristine();
                    });
            } else {
                alert('Please provide valid inputs');
            }
        };
    });