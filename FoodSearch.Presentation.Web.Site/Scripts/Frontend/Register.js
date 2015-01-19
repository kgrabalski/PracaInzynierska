(function() {
    $(document).ready(function() {

        var selectNumber, selectStreet, selectCity;

        selectCity = $("#selectCity").selectize({
            valueField: 'Id',
            labelField: 'Name',
            sortField: 'Name',
            create: false,
            onChange: function(value) {
                selectStreet.clearOptions();
                selectNumber.clearOptions();
                if (value != "") {
                    selectStreet.enable();
                } else {
                    selectStreet.disable();
                    selectNumber.disable();
                }
            }
        })[0].selectize;

        selectCity.load(function(callback) {
            $.ajax({
                url: "/Address/Cities",
                type: "POST",
                dataType: "json",
                success: function(cities) {
                    callback(cities);
                }
            });
        });

        selectStreet = $("#selectStreet").selectize({
            valueField: 'Id',
            labelField: 'Name',
            sortField: 'Name',
            searchField: ['Name'],
            create: false,
            load: function(query, callback) {
                if (query.length < 3) return callback();
                $.ajax({
                    url: "/Address/Streets",
                    data: {
                        'cityId': $("#selectCity").val(),
                        'query': query
                    },
                    type: "POST",
                    dataType: 'json',
                    success: function(str) {
                        callback(str);
                    }
                });
            },
            onChange: function(value) {
                selectNumber.disable();
                selectNumber.clearOptions();
                selectNumber.load(function(callback) {
                    $.ajax({
                        url: "/Address/StreetNumbers",
                        data: { 'streetId': value },
                        type: "POST",
                        dataType: 'json',
                        success: function(numbers) {
                            selectNumber.enable();
                            callback(numbers);
                        }
                    });
                });
            }
        })[0].selectize;
        selectStreet.disable();

        selectNumber = $("#selectNumber").selectize({
            valueField: "Id",
            labelField: "Number",
            sortField: "Number",
            searchField: ["Number"],
            create: false
        })[0].selectize;

        selectNumber.disable();

        function toggleValidation($object, success) {
            if (success == true) {
                $object.removeClass("has-error");
                $object.addClass("has-success");
            } else {
                $object.removeClass("has-success");
                $object.addClass("has-error");
            }
        }

        $("#emailInput").change(function() {
            var email = $(this).val();
            var group = $("#emailGroup");

            if (email == "") {
                toggleValidation(group, false);
            }

            $.ajax({
                url: "/Account/IsEmailDuplicated",
                type: "POST",
                data: {
                    "email": email
                },
                success: function(result) {
                    if (result == false) {
                        toggleValidation(group, true);
                    } else {
                        toggleValidation(group, false);
                    }
                }
            });
        });
        $(".form-group input[type=text], " +
            ".form-group select, " +
            ".form-group input[type=password]," +
            ".form-group input[type=number]").change(function() {
            var value = $(this).val();
            var parent = $(this).parent().parent(".form-group");
            if (value != "") {
                toggleValidation(parent, true);
            } else {
                toggleValidation(parent, false);
            }
        });

    });
})();