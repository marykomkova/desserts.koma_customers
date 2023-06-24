////$(document).ready(function () {

////	$(function () {
////		$(".ic:not(.added)").click(function (e) {
////			e.preventDefault();
////			var dessertId = $(this).data("id");
////			var self = $(this);
////			$.ajax({
////				url: "/Home/Index",
////				type: "POST",
////				data: { dessertId: dessertId },
////				success: function (result) {
////					if (result) {
////						$(".result").html(result);
////						self.addClass("added");
////					}
////				},
////				error: function (jqXHR, textStatus, errorThrown) {
////					if (jqXHR.status === 401) {
////						window.location.href = "Home/Login";
////					}
////				}
////			});
////		});
////	});


////	$(function () {
////		$(".card-product__title p").click(function (e) {
////			e.preventDefault();
////			var dessertId = $(this).data("id");
////			var newDessertId = dessertId.substring(5);
////			window.location.href = `Home/Dessert?id=${newDessertId}`;
////		});
////	});

////});

function sort() {
    document.getElementById("sec").innerHTML = "";
    var sorting = document.getElementById("sort");
    var selectedValue = sorting.options[sorting.selectedIndex].value;
    if (selectedValue == "rise") {
        fillDessertsSortRise();
    }
    if (selectedValue == "down") {
        fillDessertsSortDown();
    }
    if (selectedValue == "name") {
        fillDessertsSortByName();
    }
}

window.addEventListener('DOMContentLoaded', () => {

    $('select').niceSelect();

    if (document.getElementById("price-range")) {

        var nonLinearSlider = document.getElementById('price-range');
        const firstSpan = document.querySelector('.value-wrapper span:first-of-type');
        const secondSpan = document.querySelector('.value-wrapper span:last-of-type');

        const firstSpanText = firstSpan.textContent.trim();
        const secondSpanText = secondSpan.textContent.trim();

        const firstSpanValue = parseFloat(firstSpanText.slice(0, -4));
        const secondSpanValue = parseFloat(secondSpanText.slice(0, -4));
        var tenPerc = (secondSpanValue - firstSpanValue) / 100 * 10;
        var fiftyPerc = (secondSpanValue - firstSpanValue) / 100 * 50;

        firstSpan.remove();
        secondSpan.remove();

        noUiSlider.create(nonLinearSlider, {
            connect: true,
            behaviour: 'tap',
            start: [firstSpanValue, secondSpanValue],
            range: {
                'min': [firstSpanValue],
                '10%': [tenPerc, 5],
                '50%': [fiftyPerc, 5],
                'max': [secondSpanValue]
            }
        });


        var nodes = [
            document.getElementById('lower-value'), // 0
            document.getElementById('upper-value')  // 1
        ];

        // Display the slider value and how far the handle moved
        // from the left edge of the slider.
        nonLinearSlider.noUiSlider.on('update', function (values, handle, unencoded, isTap, positions) {
            nodes[handle].innerHTML = `${Math.round(values[handle])} руб.`;
        });

    }

    $(function () {
        $(".ic:not(.added)").click(function (e) {
            e.preventDefault();
            var dessertId = $(this).data("id");
            var self = $(this);
            fetch('/Home/GetIsUser')
                .then(response => response.text())
                .then(data => {
                    isUs = data;
                    if (isUs == "true") {
                        $.ajax({
                            url: "/Home/Desserts",
                            type: "POST",
                            data: { dessertId: dessertId },
                            success: function () {
                                $(self).addClass("added");
                            }
                        });
                    } else {
                        window.location.href = "Login";
                    }
                })
                .catch(error => console.error(error));
        });
    });


    $(function () {
        $(".card-product__title p").click(function (e) {
            e.preventDefault();
            var dessertId = $(this).data("id");
            var newDessertId = dessertId.substring(5);
            window.location.href = `Dessert?id=${newDessertId}`;
        });
    });

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