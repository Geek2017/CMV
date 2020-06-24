$(function() {
    $("#price-range").slider({
        range: true,
        min: 0,
        max: 200000,
        values: [0, 200000],
        slide: function(event, ui) { $("#priceRange").val("$" + ui.values[0] + " - $" + ui.values[1]); }
    });
    $("#priceRange").val("$" + $("#price-range").slider("values", 0) + " - $" + $("#price-range").slider("values", 1));

    $("#mpg-range").slider({
        range: true,
        min: 10,
        max: 10000,
        values: [0, 10000],
        slide: function(event, ui) { $("#MPGRange").val(ui.values[0] + " - " + ui.values[1]); }
    });
    $("#MPGRange").val($("#mpg-range").slider("values", 0) + " - " + $("#mpg-range").slider("values", 1));

    $("#mileage-range").slider({
        range: true,
        min: 0,
        max: 200000,
        values: [0, 200000],
        slide: function(event, ui) { $("#mileageRange").val(ui.values[0] + " - " + ui.values[1]); }
    });
    $("#mileageRange").val($("#mileage-range").slider("values", 0) + " - " + $("#mileage-range").slider("values", 1));

});