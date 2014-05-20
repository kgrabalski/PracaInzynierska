$(document).ready(function() {
    var selectNumber;
    $("#selectStreet").selectize({
        valueField: 'StreetId',
        labelField: 'Name',
        sortField: 'Name',
        searchField: ['Name'],
        create: false,
        load: function (query, callback) {
            if (query.length < 3) return callback();
            $.ajax({
                url: "/Home/GetStreets",
                data: { 'query': query },
                type: "POST",
                dataType: 'json',
                success: function (str) {
                    callback(str);
                }
            });
        },
        onChange: function(value) {
            selectNumber.disable();
            selectNumber.clearOptions();
            selectNumber.load(function(callback) {
                $.ajax({
                    url: "/Home/GetStreetNumbers",
                    data: { 'streetId': value },
                    type: "POST",
                    dataType: 'json',
                    success: function (numbers) {
                        selectNumber.enable();
                        callback(numbers);
                    }
                });
            });
        }
    });

    selectNumber = $("#selectNumber").selectize({
        valueField: "AddressId",
        labelField: "Number",
        sortField: "Number",
        searchField: ["Number"],
        create: false
    })[0].selectize;

    selectNumber.disable();
});