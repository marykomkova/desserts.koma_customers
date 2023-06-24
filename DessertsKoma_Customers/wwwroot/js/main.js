$(document).ready(function() {
 	
 	$(window).scroll(function() {    
	    var scroll = $(window).scrollTop();

	    if (scroll >= 575) {
	        $(".navbar").addClass("navbar-fixed-top dark-bar");
	    } else {
	        $(".navbar").removeClass("navbar-fixed-top dark-bar");
	    }
	});


	// Smooth Scroll

		$('a[href*="#"]:not([href="#"])').click(function() {
		    if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') 
		        || location.hostname == this.hostname) {

		        var target = $(this.hash);
		        target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
		           if (target.length) {
		             $('html,body').animate({
		                 scrollTop: (target.offset().top - 530)
		            }, 1000);
		            return false;
		        }
		    }
		});


 	// Popup

 	$('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
        disableOn: 700,
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        fixedContentPos: false
    });

	var contentWayPoint = function() {
		var i = 0;
		$('.ftco-animate').waypoint( function( direction ) {

			if( direction === 'down' && !$(this.element).hasClass('ftco-animated') ) {
				
				i++;

				$(this.element).addClass('item-animate');
				setTimeout(function(){

					$('body .ftco-animate.item-animate').each(function(k){
						var el = $(this);
						setTimeout( function () {
							var effect = el.data('animate-effect');
							if ( effect === 'fadeIn') {
								el.addClass('fadeIn ftco-animated');
							} else if ( effect === 'fadeInLeft') {
								el.addClass('fadeInLeft ftco-animated');
							} else if ( effect === 'fadeInRight') {
								el.addClass('fadeInRight ftco-animated');
							} else {
								el.addClass('fadeInUp ftco-animated');
							}
							el.removeClass('item-animate');
						},  k * 50, 'easeInOutExpo' );
					});
					
				}, 100);
				
			}

		} , { offset: '95%' } );
	};
	contentWayPoint();

	var loader = function() {
		setTimeout(function() { 
			if($('#ftco-loader').length > 0) {
				$('#ftco-loader').removeClass('show');
			}
		}, 1);
	};
	loader();


	var carousel = function() {
		$('.carousel-testimony').owlCarousel({
			autoplay: true,
			loop: true,
			items:1,
			margin: 0,
			stagePadding: 0,
			nav: false,
			navText: ['<span class="ion-ios-arrow-back">', '<span class="ion-ios-arrow-forward">'],
			responsive:{
				0:{
					items: 1
				},
				600:{
					items: 1
				},
				1000:{
					items: 1
				}
			}
		});

	};
	carousel();

 	// Slider

 	$('#workstation-slider').owlCarousel({
	    loop:true,
	    margin:30,
	    responsive:{
	        0:{
	            items:1
	        },
	        600:{
	            items:2
	        },
	        1000:{
	            items:3
	        }
	    }
	})
	$('#expert-slider').owlCarousel({
	    loop:true,
	    items: 1
	})

 	// height

 	var h = $('.expert').height();
 	$('.expert .col-sm-6 div').height(function (index, height) {
	    return (h);
	});

 	// Menu bar
	$( ".menu" ).click(function() {
		$(this).toggleClass('m c');
		$('#mySidenav').toggleClass( "show-menu hide-menu" );
		$('#main').toggleClass('shadow null_shadow');
		document.body.style.overflow = 'hidden';
	});

	$( "#mySidenav .closebtn" ).click(function() {
		$('.menu').toggleClass('c m');
		$('#mySidenav').toggleClass( "show-menu hide-menu" );
		$('#main').toggleClass('null_shadow shadow');
		document.body.style.overflow = 'auto';
	});


});

$(function () {
	$(".ic:not(.added)").click(function (e) {
		e.preventDefault();
		var dessertId = $(this).data("id");
		var self = $(this);
		$.ajax({
			url: "/Home/Index",
			type: "POST",
			data: { dessertId: dessertId },
			success: function (result) {
				if (result) {
					$(".result").html(result);
					self.addClass("added");
				}
			},
			error: function (jqXHR, textStatus, errorThrown) {
				if (jqXHR.status === 401) {
					window.location.href = "Home/Login";
				}
			}
		});
	});
});



function logout() {
	$.ajax({
		url: "/Home/Logout",
		type: "POST",
		success: function (result) {
			if (result) {
				$(".result").html(result);
				window.location.href = `Home/Index`;
			}
		}
	});
}


$(function () {
	$(".card-product__title p").click(function (e) {
		e.preventDefault();
		var dessertId = $(this).data("id");
		var newDessertId = dessertId.substring(5);
		window.location.href = `Home/Dessert?id=${newDessertId}`;
	});
});

document.addEventListener('DOMContentLoaded', function () {

	let isUs;
	fetch('/Home/GetIsUser')
		.then(response => response.text())
		.then(data => {
			isUs = data;
			if (isUs == "true") {
				fetch('/Home/GetDesserts')
					.then(response => response.json())
					.then(data => {
						data.forEach(function (dessertInBasket) {
							var dessertNumber = dessertInBasket;
							var button = document.querySelector('[data-id="' + dessertNumber + '"]');
							if (button) {
								button.classList.add('added');
							}
						});
					})
					.catch(error => console.error(error));
			}
		})
		.catch(error => console.error(error));
});
