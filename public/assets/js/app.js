(function($) {


    function ifScroll() {
        var scroll = $(window).scrollTop();
        if (scroll >= 74) {
            $(".navbar").addClass("scrolled");
        } else {
            $(".navbar").removeClass("scrolled");
        }
    }


    $(function() {
        var columnSize = $('#box-queue .col-box');
        $('.close-panel').on('click', function(e) {
            e.preventDefault();
            $($(this).data('target')).remove();
            columnSize.hasClass('col-lg-4') ? columnSize.removeClass('col-lg-4').addClass('col-lg-6') : columnSize.removeClass('col-lg-6').addClass('col-12');
        });
    });


    $(window).on('scroll', function(event) {
        ifScroll();
    });

})(jQuery);