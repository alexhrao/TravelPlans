$(document).ready(function() {
    var viewers = document.getElementsByClassName('carousel');
    for (var i = 0; i < viewers.length; i++) {
        var viewer = viewers[i];
        var elementId = viewer.id;
        var swiper = new Hammer(viewer);
        attachHammer(swiper, elementId);
        $('#togglePlay_' + elementId).click({elemId: elementId}, function (event) {
            if ($('#playStatus_' + event.data.elemId).hasClass('glyphicon-pause')) {
                stopCarousel(event.data.elemId);
            } else {
                startCarousel(event.data.elemId);
            }
        });
        $('#moveLeft_' + elementId + ", #moveRight_" + elementId).click({elemId: elementId}, function (event) {
            stopCarousel(event.data.elemId);
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

function attachHammer(swiper, id) {
    alert(id);
    swiper.on("swipeleft swiperight", function (event) {
        if (event.type === "swipeleft") {
            alert("swiped " + id);
            $('#' + id).carousel("next");
            stopCarousel(id);
        } else if (event.type === "swiperight") {
            alert("swiped " + id);
            $('#' + id).carousel("prev");
            stopCarousel(id);
        }
    });
}
function stopCarousel(id) {
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
