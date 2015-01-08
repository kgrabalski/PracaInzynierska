$(document).ready(function() {
    var selectNumber, selectStreet, selectCity;
    var self = this;

    selectCity = $("#selectCity").selectize({
        valueField: 'Id',
        labelField: 'Name',
        sortField: 'Name',
        create: false,
        onChange: function (value) {
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
        load: function (query, callback) {
            if (query.length < 3) return callback();
            $.ajax({
                url: "/Address/Streets",
                data: {
                    'cityId': $("#selectCity").val(),
                    'query': query
                },
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
            selectNumber.load(function (callback) {
                $.ajax({
                    url: "/Address/StreetNumbers",
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

    function DeliveryAddress(id, aid, cid, c, sid, s, sn, fn) {
        this.Id = id;
        this.AddressId = aid;
        this.CityId = cid;
        this.City = c;
        this.StreetId = sid;
        this.Street = s;
        this.StreetNumber = sn;
        this.FlatNumber = fn;
        this.DisplayText = this.City + ", " + this.Street + " " + this.StreetNumber;
        if (this.FlatNumber != "") {
            this.DisplayText += "/" + this.FlatNumber;
        }
    }

    self.Addresses = new Array();

    function getDeliveryAddress(id) {
        for (var i = 0; i < self.Addresses.length; i++) {
            var a = self.Addresses[i];
            if (a.Id == id) return a;
        }
    }

    var selectAddress = $("#selectAddress").selectize({
        valueField: 'Id',
        labelField: 'DisplayText',
        create: false,
        onChange: function(value) {
            var address = getDeliveryAddress(value);

            selectCity.addOption({ Id: address.CityId, Name: address.City });
            selectCity.setValue(address.CityId);

            selectStreet.enable();
            selectStreet.addOption({ Id: address.StreetId, Name: address.Street });
            selectStreet.setValue(address.StreetId);

            selectNumber.enable();
            selectNumber.addOption({ Id: address.AddressId, Number: address.StreetNumber });
            selectNumber.setValue(address.AddressId);
        }
    })[0].selectize;

    selectAddress.load(function(callback) {
        $.ajax({
            url: "/Home/GetDeliveryAddresses",
            type: "POST",
            success: function(response) {
                for (var i = 0; i < response.length; i++) {
                    var a = response[i];
                    self.Addresses.push(
                        new DeliveryAddress(a.Id, a.AddressId, a.CityId, a.City,
                                            a.StreetId, a.Street, a.StreetNumber, a.FlatNumber));
                }
                callback(self.Addresses);
            }
        });
    });

});