$(document).ready(function() {


    $(".txtMult input, .txtMult2 input, input").change(function() {
        multInputs();
    });


    function multInputs() {
        var mult = 0;


        $("tr.txtMult").each(function() {
            // get the values from this row:
            var $val1 = $('.val1', this).val();
            var $val2 = $('.val2', this).val();
            var $total = ($val1 * 1) * ($val2 * 1)
            $('.expenses_sum', this).val($total);
            mult += $total || 0;
            $('#total_sum_value0').html(parseFloat(mult).toFixed(2))

            var mult1 = 0;

            $("tr.txtMult2").each(function() {
                // get the values from this row:
                var $val11 = $('.val12', this).val();
                var $val22 = $('.val22', this).val();
                var $total1 = ($val11 * 1) * ($val22 * 1)
                $('.expenses_sum2', this).val($total1);
                mult1 += $total1 || 0;
                console.log($total1)
                $('#total_sum_value1').html(parseFloat(mult1).toFixed(2))


                $("#pbc").val(mult - mult1);
                $("#aca").val(mult * ($("#sac").val() / 100));
                $("#gp").val(mult - mult1 - $("#aca").val());

                console.log(mult / $("#gp").val());

                $("#gpm").val(Math.round(($("#gp").val() / mult) * 100));

                $("#rrf").val($("#irrf").val() * 1.5);

                console.log($("#irrf").val() * 1.5);

                $("#bgpm").text($("#gpm").val());

            });




        });
    }




});