(function (app) {

    app.controller('paymentMethodrCtrl', paymentMethodrCtrl);

    paymentMethodrCtrl.$inject = ['$scope', 'apiService', '$window'];

    function paymentMethodrCtrl($scope, apiService, $window) {
        var self = this;

        init();

        function init() {
            apiService.get('https://localhost:5001/api/PaymentMethod', null, loadpaymentMethod, failedpaymentMethod);

        }

        function loadpaymentMethod(result) {
            self.paymentMethods = result.data;
            angular.element(document).ready(function () {
                paymentMethodTable = $('#paymentMethod_table');
                paymentMethodTable.DataTable();
            });
        
        }

        function failedpaymentMethod(reponse) {
            console.log(response);
        }




        self.edit = function (member) {
            $window.location.pathname = 'PaymentMethod/Edit/' + member.id;
        };

        self.delete = function (member) {
            $window.location.pathname = 'PaymentMethod/Delete/' + member.id;
        };

    }

})(angular.module('NgoApp'));