document.addEventListener("DOMContentLoaded", function () {
    const breweryButtons = document.querySelectorAll(".brewery-filter-button");

    breweryButtons.forEach(button => {
        button.addEventListener("click", function () {
            breweryButtons.forEach(btn => btn.classList.remove("active"));
            button.classList.add("active");

            const selectedBrewery = button.getAttribute("data-value");
            const products = document.querySelectorAll('.breweryItem');

            products.forEach(product => {
                // Use data-new attribute to check for "Novinky" filter
                const isNew = product.getAttribute('data-new') === 'true'; // Adjusted to use data-new attribute
                if (selectedBrewery === "Novinky") {
                    if (isNew) {
                        product.style.display = "block";
                    } else {
                        product.style.display = "none";
                    }
                } else {
                    const breweryName = product.getAttribute('data-brewery');
                    if (selectedBrewery === breweryName || selectedBrewery === "") {
                        product.style.display = "block";
                    } else {
                        product.style.display = "none";
                    }
                }
            });
        });
    });
});
