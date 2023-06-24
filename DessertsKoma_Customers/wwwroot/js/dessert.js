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
                            var bask = document.getElementById('bask');
                            if (button) {
                                bask.classList.add('added');
                                bask.textContent = "Добавлено! Перейти в корзину.";
                            }
                        });
                    })
                    .catch(error => console.error(error));
            }
        })
        .catch(error => console.error(error));

    $(function () {
        $("#bask").click(function (e) {
            if ($(this).hasClass("added")) {
                e.preventDefault();
                window.location.href = "Basket";
            } else {
                e.preventDefault();
                var dessertId = $('.product-details').data("id");
                var self = $(this);
                $.ajax({
                    url: "/Home/Index",
                    type: "POST",
                    data: { dessertId: dessertId },
                    success: function () {
                        self.addClass("added");
                        self.text("Добавлено! Перейти в корзину.");
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        if (jqXHR.status === 401) {
                            window.location.href = "Login";
                        }
                    }
                });
            }
        });
    });

});


