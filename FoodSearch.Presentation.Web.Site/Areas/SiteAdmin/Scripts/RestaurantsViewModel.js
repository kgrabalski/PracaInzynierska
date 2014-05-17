function RestaurantsViewModel() {
	var self = this;
	var dt = $("#restaurantList").dataTable();
	
	function Restaurant(id, lid, na, ci, di, st, no) {
		var rthis = this;
		rthis.RestaurantId = ko.observable(id);
		rthis.LogoId = ko.observable(lid);
		rthis.Name = ko.observable(na);
		rthis.City = ko.observable(ci);
		rthis.District = ko.observable(di);
		rthis.Street = ko.observable(st);
		rthis.Number = ko.observable(no);
		rthis.Address = ko.computed(function() {
			return rthis.Street() + " " + rthis.Number();
		});
		rthis.LogoUrl = ko.computed(function() {
			return "/SiteAdmin/Restaurant/GetLogo?logoId=" + rthis.LogoId();
		});
	}

	self.Restaurants = ko.observableArray();

	self.GetRestaurants = function () {
		$.ajax({
			url: "/SiteAdmin/Restaurant/GetRestaurants",
			type: "POST",
			dataType: "json",
			success: function (restaurants) {
				for (var i = 0; i < restaurants.length; i++) {
					var r = restaurants[i];
					self.Restaurants.push(new Restaurant(r.RestaurantId, r.LogoId, r.Name, r.City, r.District, r.Street, r.Number));
				}
			}
		});
	};

	$("#addRestaurant").click(function () {
		var selectNumber;
		var $selectStreet = $("#addRestStreet").selectize({
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
						url: "/Home/GetStreetNumbers",
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
		});
		
		var $selectNumber = $("#addRestNumber").selectize({
			valueField: "AddressId",
			labelField: "Number",
			sortField: "Number",
			searchField: ["Number"],
			create: false
		});
		
		selectNumber = $selectNumber[0].selectize;
		selectNumber.disable();

		$("#addRestForm").submit(function(e) {
			e.preventDefault();
			
			var dataString = new FormData($("#addRestForm").get(0));
			$.ajax({
				type: "POST",
				url: "/SiteAdmin/Restaurant/UploadLogo",
				data: dataString,
				dataType: "json",
				contentType: false,
				processData: false,
				success: function(logoId) {
					var name = $("#addRestName").val();
					var addressId = $("#addRestNumber").val();
					var userName = $("#addRestUser").val();
					var password = $("#addRestPass").val();
					$.ajax({
						url: "/SiteAdmin/Restaurant/Create",
						type: 'POST',
						dataType: 'json',
						data: {
							'name': name,
							'addressId': addressId,
							'userName': userName,
							'password': password,
							'logoId': logoId
						},
						success: function(r) {
							if (r != null) {
								self.Restaurants.push(new Restaurant(r.RestaurantId, r.LogoId, r.Name, r.City, r.District, r.Street, r.Number));
								$("#addRestaurantModal").modal("hide");
							}
						},
					});
				}
			});
		});

		$("#addRestaurantModal .btn-primary").click(function() {
			$("#addRestForm").submit();
		});

		$("#addRestaurantModal").modal();
	});

	self.GetRestaurants();
}