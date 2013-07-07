angular.module('userslab', ['ngResource', 'globalErrors']).
    factory('User', function ($resource) {
        var User = $resource('/Action/JsonUsers/:id',
             {
                //update: { method: 'PUT' }
            }
        );

        User.prototype.update = function (cb) {
            return User.update({ id: this.id },
                angular.extend({}, this, { id: undefined }), cb);
        };

        User.prototype.destroy = function (cb) {
            return User.remove({ id: this.id }, cb);
        };

        return User;
    });