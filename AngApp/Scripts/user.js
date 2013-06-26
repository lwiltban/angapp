angular.module('user', ['userslab']).
  config(function ($routeProvider) {
      $routeProvider.
        when('/', { controller: ListCtrl, templateUrl: 'list.html' }).
        when('/edit/:id', { controller: EditCtrl, templateUrl: 'detail.html' }).
        when('/new', { controller: CreateCtrl, templateUrl: 'detail.html' }).
        otherwise({ redirectTo: '/' });
  });


function ListCtrl($scope, User) {
    $scope.users = User.query();
}


function CreateCtrl($scope, $location, User) {
    $scope.save = function () {
        User.save($scope.user, function (user) {
            $location.path('/edit/' + user.id);
        });
    }
}


function EditCtrl($scope, $location, $routeParams, User) {
    var self = this;

    User.get({ id: $routeParams.projectId }, function (user) {
        self.original = user;
        $scope.project = new User(self.original);
    });

    $scope.isClean = function () {
        return angular.equals(self.original, $scope.user);
    }

    $scope.destroy = function () {
        self.original.destroy(function () {
            $location.path('/list');
        });
    };

    $scope.save = function () {
        $scope.user.update(function () {
            $location.path('/');
        });
    };
}