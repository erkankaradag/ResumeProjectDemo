$(function () {
    var $slider = $("#testimonial .slider");
    var $bgHolder = $("#testimonial .bg-image-holder");
    var $bgImg = $("#testimonialBg"); // yedek (ister gizli kalsın)

    if (!$slider.length || !$bgHolder.length) return;

    function normalizeUrl(url) {
        if (!url) return null;
        return ("" + url).trim(); // ✅ KRİTİK: \r\n ve boşlukları temizler
    }

    function setBg(url) {
        url = normalizeUrl(url);
        if (!url) return;

        // yedek olarak img de güncellensin
        if ($bgImg.length) $bgImg.attr("src", url);

        // asıl görünen: div background-image
        $bgHolder.css("background-image", "url('" + url + "')");
    }

    function getSlideBg($slide) {
        if (!$slide || !$slide.length) return null;

        // data-bg bazen direkt slide'da bazen içeride olur
        var url =
            $slide.attr("data-bg") ||
            $slide.data("bg") ||
            $slide.find("[data-bg]").attr("data-bg") ||
            $slide.find("[data-bg]").data("bg");

        return normalizeUrl(url);
    }

    function applyBgByIndex(idx) {
        try {
            var slick = $slider.slick("getSlick");
            var $slide = $(slick.$slides.get(idx));
            setBg(getSlideBg($slide));
        } catch (e) {
            // slick hazır değilse ilk data-bg
            var $first = $slider.find("[data-bg]").first();
            var url2 = $first.attr("data-bg") || $first.data("bg");
            setBg(url2);
        }
    }

    // 1) init olunca
    $slider.on("init", function (event, slick) {
        applyBgByIndex(slick.currentSlide || 0);
    });

    // 2) değişince
    $slider.on("afterChange", function (event, slick, currentSlide) {
        applyBgByIndex(currentSlide);
    });

    // 3) slick önceden init olduysa
    if ($slider.hasClass("slick-initialized")) {
        applyBgByIndex($slider.slick("slickCurrentSlide"));
    } else {
        // bazı temalarda init gecikir; 500ms sonra bir daha dene
        setTimeout(function () {
            if ($slider.hasClass("slick-initialized")) {
                applyBgByIndex($slider.slick("slickCurrentSlide"));
            } else {
                applyBgByIndex(0);
            }
        }, 500);
    }
});
