
(function (app) {

    'use strict';
    app.factory('apiService', apiService);
    apiService.$inject = ['$http', '$location'];

    function apiService($http, $location) {
        var service = {
            get: get,
            post: post
        };

        function get(url, config, success, failure) {
            return $http.get(url, config).then(function (result) { success(result); },
                function (error) {
                    failure(error);
                });
        }

        function post(url, data, success, failure) {
            return $http.post(url, data).then(function (result) { success(result); },
                function (error) {
                    failure(error);
                });
        }
        return service;
    }

})(angular.module("common.core"));