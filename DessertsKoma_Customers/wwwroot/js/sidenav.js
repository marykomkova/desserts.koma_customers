$(document).ready(function () {

	$(window).scroll(function () {
		var scroll = $(window).scrollTop();

		if (scroll >= 575) {
			$(".navbar").addClass("navbar-fixed-top dark-bar");
		} else {
			$(".navbar").removeClass("navbar-fixed-top dark-bar");
		}
	});


	// Smooth Scroll

	$('a[href*="#"]:not([href="#"])').click(function () {
		if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '')
			|| location.hostname == this.hostname) {

			var target = $(this.hash);
			target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
			if (target.length) {
				$('html,body').animate({
					scrollTop: (target.offset().top - 530)
				}, 1000);
				return false;
			}
		}
	});


	var contentWayPoint = function () {
		var i = 0;
		$('.ftco-animate').waypoint(function (direction) {

			if (direction === 'down' && !$(this.element).hasClass('ftco-animated')) {

				i++;

				$(this.element).addClass('item-animate');
				setTimeout(function () {

					$('body .ftco-animate.item-animate').each(function (k) {
						var el = $(this);
						setTimeout(function () {
							var effect = el.data('animate-effect');
							if (effect === 'fadeIn') {
								el.addClass('fadeIn ftco-animated');
							} else if (effect === 'fadeInLeft') {
								el.addClass('fadeInLeft ftco-animated');
							} else if (effect === 'fadeInRight') {
								el.addClass('fadeInRight ftco-animated');
							} else {
								el.addClass('fadeInUp ftco-animated');
							}
							el.removeClass('item-animate');
						}, k * 50, 'easeInOutExpo');
					});

				}, 100);

			}

		}, { offset: '95%' });
	};
	contentWayPoint();

	var loader = function () {
		setTimeout(function () {
			if ($('#ftco-loader').length > 0) {
				$('#ftco-loader').removeClass('show');
			}
		}, 1);
	};
	loader();

	// Menu bar
	$(".menu").click(function () {
		$(this).toggleClass('m c');
		$('#mySidenav').toggleClass("show-menu hide-menu");
		$('#main').toggleClass('shadow null_shadow');
		document.body.style.overflow = 'hidden';
	});

	$("#mySidenav .closebtn").click(function () {
		$('.menu').toggleClass('c m');
		$('#mySidenav').toggleClass("show-menu hide-menu");
		$('#main').toggleClass('null_shadow shadow');
		document.body.style.overflow = 'auto';
	});


});