$(document).ready(function() {
    var viewer = document.getElementById('headerCarousel');
    var swiper = new Hammer(viewer);
    swiper.on("swipeleft swiperight", function (event) {
        if (event.type === "swipeleft") {
            $('.carousel').carousel("next");
            stopCarousel();
        } else if (event.type === "swiperight") {
            $('.carousel').carousel("prev");
            stopCarousel();
        }
    });
    $('#togglePlay').click(function (event) {
        if ($('#playStatus').hasClass('glyphicon-pause')) {
            stopCarousel();
        } else {
            startCarousel();
        }
    });
    $('#moveLeft, #moveRight').click(function (event) {
        stopCarousel();
    });
    $('img:not(.no-web)').wrap(function () {
        return "<a href='./source/" + $(this).attr("src") + "'></a>";
    });
    $('img.vr').wrap(function () {
        return "<a href='https://360player.io/p/" + $(this).attr("id") + "/'></a>";
    });
});

function stopCarousel() {
    $('.carousel').carousel('pause');
    $('#playStatus').removeClass('glyphicon-pause');
    $('#playStatus').removeClass('glyphicon-play');
    $('#playStatus').addClass('glyphicon-play');
}

function startCarousel() {
    $('.carousel').carousel('cycle');
    $('#playStatus').removeClass('glyphicon-pause');
    $('#playStatus').removeClass('glyphicon-play');
    $('#playStatus').addClass('glyphicon-pause');
}