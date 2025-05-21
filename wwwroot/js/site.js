function ShowCreateModalForm() {
    $('#createCustomerModal').modal('show');
    return;
}

function SubmitModalForm() {
    var btnSubmit = document.getElementById("btnSubmit");
    btnSubmit.click();
}

function refreshCityList() {
    var btnBack = document.getElementById("btnBackCity");
    btnBack.click();
    FillCities("lstCountryId", "lstCity");
}

function FillCountries(lstCountryId) {
    var lstCountries = $("#" + lstCountryId);
    lstCountries.empty();

    lstCountries.append($("<option />").val(null).text("Select Country"));

    $.get("/Countries/GetAll", function (data) {
        $.each(data, function (index, country) {
            lstCountries.append($("<option />").val(country.id).text(country.name));
        });
    });
}

function FillCities(countryCtrl, cityCtrl) {
    var lstCountryId = $("#" + countryCtrl).val();
    var lstCities = $("#" + cityCtrl);
    lstCities.empty();

    lstCities.append($("<option />").val(null).text("Select City"));

    $.get("/Cities/GetCities?countryId=" + lstCountryId, function (data) {
        $.each(data, function (index, city) {
            lstCities.append($("<option />").val(city.id).text(city.name));
        });
    });
}
