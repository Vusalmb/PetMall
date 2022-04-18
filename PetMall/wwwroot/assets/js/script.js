$(document).ready(function(){

    // page up

    $("#page_up").click(function() {
        $("html").animate({scrollTop: 0},500);
    });

    // search

    $("#header_top_setting_icons .search").click(function(){
        $("#search_area").slideToggle();
    });

    // sticky

    const sticky = $(".sticky");

    $(window).scroll(function () {
        let scrollTop = $(window).scrollTop();

        if (scrollTop > 180) {
            sticky.css(
                { "display": "flex" }
            )
        }
        else {
            sticky.css(
                { "display": "none" }
            );
        }

    })

    // basket fixed

    $("#header_top_setting_icons .shop").click(function(){
        $(".basket_fixed_container").toggle("1000").css({"display":"flex"});
        
    })

    $(".sticky_setting_icons li:first-child").click(function(){
        $(".basket_fixed_container").toggle("1000").css({"display":"flex"});
        
    })

    $(".basket_fixed_exit span").click(function(){
        $(".basket_fixed_container").toggle("1000");
    })

    // setting fixed

    $("#header_top_setting_icons .setting").click(function(){
        $(".setting_fixed_container").toggle("1000").css({"display":"flex"});
        
    })

    $(".sticky_setting_icons li:last-child").click(function(){
        $(".setting_fixed_container").toggle("1000").css({"display":"flex"});
        
    })

    $(".setting_fixed_exit span").click(function(){
        $(".setting_fixed_container").toggle("1000");
    })

    // size guide tab

    $(".pet_size_tab_header li").click(function(){
        $(".pet_size_tab_header li").removeClass("active");
        $(this).addClass("active");

        let value = $(this).attr("data-id");
        
        if(value == "women"){
            $(".tab1").show();
        }
        else{
            $(".tab1").not("." + value).hide();
            $(".tab1").filter("." + value).show();
        }

        if(value == "men"){
            $(".tab2").show();
        }
        else{
            $(".tab2").not("." + value).hide();
            $(".tab2").filter("." + value).show();
        }

        if(value == "kid"){
            $(".tab3").show();
        }
        else{
            $(".tab3").not("." + value).hide();
            $(".tab3").filter("." + value).show();
        }
    })

    // FAQs accordion

    $(".accordion_title").click(function(){
        $(this).siblings('.accordion_title').children('i').removeClass('fa-minus').addClass('fa-plus');
        $(this).siblings('.accordion_desc').not($(this).next()).slideUp();
        $(this).children('i').toggleClass('fa-plus').toggleClass('fa-minus');
        $(this).next().slideToggle();
        $(this).siblings('.active').removeClass('active');
        $(this).toggleClass('active');

        //$(".accordion_desc").slideUp();
        //// $(this).next(".accordion_desc").slideToggle("slow");
        //$(this).next(".accordion_desc").slideDown();
        
        
        //$(".accordion_title span i").removeClass("fa-minus").addClass("fa-plus");
        //$(this).find("i").removeClass("fa-plus").addClass("fa-minus");
    })

     // owl carousel

     $(".owl_container").owlCarousel(
        {
            nav: true,
            dots: false,
            loop: true,
            items: 1
        }
    );

    $(".dogs_news_slide").owlCarousel(
        {
            items: 3,
            nav: false,
            dots: false
        }
    )

    $(".join_image_popup").owlCarousel(
        {
            items:1,
            dots: false,
            nav: true
        }
    )

    $(".item span").click(function(){
        $(".join_image_popup_container").toggle();
    })

    $(".join_image_popup_exit span").click(function(){
        $(".join_image_popup_container").css({"display":"none"});
    })

    // blog detail owl-carousel

    $(".related_info").owlCarousel(
        {
            loop: true,
            items: 3,
            nav: false,
            dots: false
        }
    )

    // blog detail social

    $(".blog_detail_info_date .share span:first-child").click(function(){
        $(".share_social").toggle();
    })
    
    // shop filter

    $(".filter_list").click(function(){
        $(".filter_list").removeClass("active");
        $(this).addClass("active");


        let value = $(this).attr("data-filter");

        if(value == "first"){
            $(".card1").show("1000");
        }
        else{
            $(".card1").not("." + value).hide("1000");
            $(".card1").filter("." + value).show("1000");
        }

        if(value == "second"){
            $(".card2").show("1000");
        }
        else{
            $(".card2").not("." + value).hide("1000");
            $(".card2").filter("." + value).show("1000");
        }

        if(value == "third"){
            $(".card3").show("1000");
        }
        else{
            $(".card3").not("." + value).hide("1000");
            $(".card3").filter("." + value).show("1000");
        }
    
        // $(".filter_list").click(function(){
        //     let current = $(".active");
        //     let next = current.next();
    
        //     if(next.length){
        //         current.removeClass("active");
        //         next.addClass("active");
        //     }
        //     else{
        //         current.removeClass("active");
        //         $(".filter_list:first-child").addClass("active");
        //     }
        // });

        // const value = $(this).attr("data-filter");

        // $(".filter_list").removeClass("active");
        // $(this).addClass("active")

        // $(".card").hide();
        // if(value = "first"){
        //     $("." + value).show()
        // }
        // else if(value = "second"){
        //     $("." + value).show()
        // }
        // else if(value = "third"){
        //     $("." + value).show()
        // }
        // else{
        //     $(".card").show();
        // }
    })

    // shop detail size

    $(".size span").click(function(){
        $(".size span").removeClass("active");
        $(this).addClass("active");
    })

    // shop detail tab

    $(".header_container li").click(function(){
        $(".header_container li").removeClass("active");
        $(this).addClass("active");

        let value = $(this).attr("data-id");
        
        if(value == "description"){
            $(".tab1").show();
        }
        else{
            $(".tab1").not("." + value).hide();
            $(".tab1").filter("." + value).show();
        }

        if(value == "delivery"){
            $(".tab2").show();
        }
        else{
            $(".tab2").not("." + value).hide();
            $(".tab2").filter("." + value).show();
        }

        if(value == "exchange"){
            $(".tab3").show();
        }
        else{
            $(".tab3").not("." + value).hide();
            $(".tab3").filter("." + value).show();
        }

        if(value == "review"){
            $(".tab4").show();
        }
        else{
            $(".tab4").not("." + value).hide();
            $(".tab4").filter("." + value).show();
        }
    })

    // shop detail review form

    $(".customer_summary .summary2").click(function(){
        $(".form_container").slideToggle();
    })

    // shop detail owl carousel

    $(".related_product").owlCarousel(
        {
            loop: true,
            items: 4,
            nav: false,
            dots: false
        }
    )

    $(".images").owlCarousel(
        {
            nav: true,
            dots: true,
            loop: true,
            items: 1,
            autoplay: true,
            navText: ['<i class="fa-solid fa-circle-chevron-left"></i>','<i class="fa-solid fa-circle-chevron-right"></i>']
        }
    );

    // remove basket

    //$(".basket_remove").click(function () {
    //    $(".basket_product").css({ "display": "none" });
    //})
})

// home slide

// let i = 0; // current slide
// let j = 3; // total slides

// const dots = document.querySelectorAll(".sliders .slide");
// const images = document.querySelectorAll(".images .dogs");

// function next(){
//     document.getElementById("content" + (i+1)).classList.remove("active_slide");
//     i = (j + i + 1) % j;
//     document.getElementById("content" + (i+1)).classList.add("active_slide");

//     indicator(i+1);
// }

// function previous(){
//     document.getElementById("content" + (i+1)).classList.remove("active_slide");
//     i = (j + i - 1) % j;
//     document.getElementById("content" + (i+1)).classList.add("active_slide");

//     indicator(i+1);
// }

// function indicator(num){
//     dots.forEach(function(dot){
//         dot.style.opacity = "0.5";
//     })
//     document.querySelector(".sliders .slide:nth-child("+ num +")").style.opacity = "1";
// }

// function dot(index){
//     images.forEach(function(image){
//         image.classList.remove("active_slide");
//     });

//     document.getElementById("content" + index).classList.add("active_slide");

//     i = index - 1;
//     indicator(index);
// }