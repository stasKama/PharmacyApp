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
});