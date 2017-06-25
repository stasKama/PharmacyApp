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
            console.log(medicine.data);
        }, function () {
            alert('Data not found');
        });
    }

    $scope.filterAvailability = false;
    $scope.filterAnalogue = true;

    $scope.setFilterAvailability = function (value) {
        $scope.filterAvailability = value;
    };
    $scope.setFilterAnalogue = function (value) {
        $scope.filterAnalogue = value;
    }

    $scope.FilterPharmacy = function (medicineItem) {
        return $scope.FilterAvailability(medicineItem) && $scope.FilterAnalogue(medicineItem);
    }

    $scope.FilterAvailability = function (medicineItem) {
        return $scope.filterAvailability ? medicineItem.availabilityPharmacy : true;
    };
    $scope.FilterAnalogue = function (medicineItem) {
        return $scope.filterAnalogue ? true : !medicineItem.analogue;
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