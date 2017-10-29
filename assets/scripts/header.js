$(document).ready(function() {
    var viewers = document.getElementsByClassName('carousel');
    var i;
    for (i = 0; i < viewers.length; i++) {
        var viewer = viewers[i];
        var id = viewer.id;
        var swiper = new Hammer(viewer);
        swiper.on("swipeleft swiperight", function (event) {
            if (event.type === "swipeleft") {
                $('#' + id).carousel("next");
                stopCarousel(id);
            } else if (event.type === "swiperight") {
                $('#' + id).carousel("prev");
                stopCarousel(id);
            }
        });
        $('#togglePlay_' + id).click(function (event) {
            if ($('#playStatus_' + id).hasClass('glyphicon-pause')) {
                stopCarousel(id);
            } else {
                startCarousel(id);
            }
        });
        $('#moveLeft_' + id + ", #moveRight_" + id).click(function (event) {
            stopCarousel(id);
        });
    }
    $('#togglePlay').click(function (event) {
        if ($('#playStatus').hasClass('glyphicon-pause')) {
            stopCarousel("headerCarousel");
        } else {
            startCarousel("headerCarousel");
        }
    });
    $('#moveLeft, #moveRight').click(function (event) {
        stopCarousel("headerCarousel");
    });

    $('img:not(.no-web)').wrap(function () {
        return "<a href='./source/" + $(this).attr("src") + "'></a>";
    });
    $('img.vr').wrap(function () {
        return "<a href='https://360player.io/p/" + $(this).attr("id") + "/'></a>";
    });

    var days = document.getElementsByClassName('day-block');
    var j;
    for (j = 0; j < days.length; j++) {
        var ind = j % 3;
        if (ind == 0) {
            days[j].style.backgroundColor = "#E8A45F";
        } else if (ind == 1) {
            days[j].style.backgroundColor = "#59BE4E";
        } else {
            days[j].style.backgroundColor = "#A5291C";
        }
    }
});

function stopCarousel(id) {
    alert(id);
    $('#' + id).carousel('pause');
    if (id === "headerCarousel") {
        id = "";
    } else {
        id = "_" + id;
    }
    $('#playStatus' + id).removeClass('glyphicon-pause');
    $('#playStatus' + id).removeClass('glyphicon-play');
    $('#playStatus' + id).addClass('glyphicon-play');
}

function startCarousel(id) {
    alert(id);
    $('#' + id).carousel('cycle');
    if (id === "headerCarousel") {
        id = "";
    } else {
        id = "_" + id;
    }
    $('#playStatus' + id).removeClass('glyphicon-pause');
    $('#playStatus' + id).removeClass('glyphicon-play');
    $('#playStatus' + id).addClass('glyphicon-pause');
}
