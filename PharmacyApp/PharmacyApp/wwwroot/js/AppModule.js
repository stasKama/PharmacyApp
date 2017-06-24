var app = angular.module("PharmacyApp", []);

app.service("PharmacyService", function ($http) {
    this.getPharmacy = function () {
        return $http.get("/Pharmacy/GetMedicines");
    };
});

app.controller("PharmacyCtrl", function ($scope, PharmacyService) {

    GetMedicines();

    function GetMedicines() {
        
        var getAllMedicine = PharmacyService.getPharmacy();
        getAllMedicine.then(function (medicine) {
            $scope.medicines = medicine.data;
        }, function () {
            alert('Data not found');
        });
    }

    $scope.isAvailabilityPharmacy = function (medicineItem) {
        return medicineItem.availabilityPharmacy === true;
    };

    $scope.sortField = undefined;
    $scope.reverse = false;

    $scope.sort = function (fieldName) {
        if ($scope.sortField === fieldName) {
            $scope.reverse = !$scope.reverse;
        } else {
            $scope.sortField = fieldName;
            $scope.reverse = false;
        }
    };

    $scope.isSortUp = function (fieldName) {
        return $scope.sortField === fieldName && !$scope.reverse;
    };

    $scope.isSortDown = function (fieldName) {
        return $scope.sortField === fieldName && $scope.reverse;
    };

});