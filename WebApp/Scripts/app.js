//Creates the "Core"
var epatecAPP = angular.module('epatecAPP', ['ngRoute']);

epatecAPP.config(function ($routeProvider) {
	$routeProvider
		.when('/login',
		{
			controller: 'AppController',
			templateUrl: 'Partials/Login/login.html'
		})
		.when('/register',
		{
			controller: 'AppController',
			templateUrl: 'Partials/Register/register.html'
		})
		.when('/maintenance',
		{
			controller: 'AppController',
			templateUrl: 'Partials/Maintenance/maintenance.html'
		})
		.when('/orders',
		{
			controller: 'AppController',
			templateUrl: 'Partials/Orders/orders.html'
		})
		.otherwise({ redirectTo: '/login' });			
		});

epatecAPP.controller('AppController', function SimpleController($scope, $location, $http){

	$scope.selectedRow = null;
	$scope.updateVar = false;

	$scope.customers = [
		{ ID: 187199, name: 'Carlos',lastName: 'Quirós', address: 'Cartago', phoneNumber: 88888888, birthday: '9-10-2016'},
		{ ID: 187197, name: 'Andrea',lastName: 'Quirós', address: 'Cartago', phoneNumber: 88888888, birthday: '6-10-2016'},
		{ ID: 187196, name: 'Pedro',lastName: 'Quirós', address: 'Cartago', phoneNumber: 88888888, birthday: '8-10-2016'}
	];

	$scope.loadCustomers = function(){
		$http.get(url, config).success(function(data){
			$scope.customers = data;
		})
		.error(function(){
			alert("an unexpected error ocurred!");
		});
	}
	
	$scope.goTo = function ( path ) {
    	$location.path( path );
    };

    $scope.setClickedRow = function(index){
		$scope.selectedRow = index;
	}

	$scope.eliminateRow = function(){
		$scope.customers.splice($scope.selectedRow,1);
	}

	$scope.addNew = function(){

		var newFile = {
				ID: $scope.checkValue($scope.newCustomer.id),
				name: $scope.checkValue($scope.newCustomer.name),
				lastName: $scope.checkValue($scope.newCustomer.lName),
				address: $scope.checkValue($scope.newCustomer.address),
				phoneNumber: $scope.checkValue($scope.newCustomer.pNumber),
				birthday: $scope.checkValue($scope.newCustomer.birthday)
			};

		if ($scope.updateVar == false) {
			$scope.customers.push(newFile);
		}
		else {
			$scope.customers[$scope.selectedRow] = newFile;
			$scope.updateVar = false;
		}

		$scope.cleanAll();
	}

	$scope.checkValue = function( input ){
		if (angular.isUndefined(input) || input === null || input === '') {
            return null;
        }
        else{
        	return input;
        }
	}

	$scope.update = function(){
		$scope.newCustomer.id = $scope.customers[$scope.selectedRow].ID;
		$scope.newCustomer.name = $scope.customers[$scope.selectedRow].name;
		$scope.newCustomer.lName = $scope.customers[$scope.selectedRow].lastName;
		$scope.newCustomer.address = $scope.customers[$scope.selectedRow].address;
		$scope.newCustomer.pNumber = $scope.customers[$scope.selectedRow].phoneNumber;
		$scope.newCustomer.birthday = $scope.customers[$scope.selectedRow].birthday;
		$scope.updateVar = true;
	}

	$scope.cleanAll = function(){
		$scope.newCustomer.id = null;
		$scope.newCustomer.name = null;
		$scope.newCustomer.lName = null;
		$scope.newCustomer.address = null;
		$scope.newCustomer.pNumber = null;
		$scope.newCustomer.birthday = null;
	}
});