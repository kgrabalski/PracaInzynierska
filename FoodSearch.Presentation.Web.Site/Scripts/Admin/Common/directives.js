var directives = angular.module('FoodSearch.Admin.Common', []);

directives.directive('sidebarmenu', function() {
    return {
        restrict: 'AE',
        replace: true,
        transclude: true,
        template: '<ul class="sidebar-menu" ng-transclude></ul>',
        controller: function() {
            var items = [];

            this.addMenuItem = function(item) {
                items.push(item);
            };

            this.navigateTo = function(menuItem) {
                angular.forEach(items, function(i) {
                    if (i !== menuItem) {
                        i.active = false;
                    }
                });
            };
        }
    };
});

directives.directive('sidebarmenuitem', function() {
    return {
        restrict: 'AE', 
        replace: true,
        transclude: true,
        template: '<li class="active-{{active}}" ng-transclude ></li>',
        require: "^?sidebarmenu",
        scope: true,
        link: function(scope, element, attrs, menuController) {
            scope.active = false;

            menuController.addMenuItem(scope);

            element.children("a").bind("click", function () {
                menuController.navigateTo(scope);
                scope.$apply(function () {
                    scope.active = true;
                });
            });
          
        }
    };
});