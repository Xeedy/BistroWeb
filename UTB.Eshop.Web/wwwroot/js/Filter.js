document.addEventListener("DOMContentLoaded", function () {
    const breweryButtons = document.querySelectorAll(".brewery-filter-button");

    breweryButtons.forEach(button => {
        button.addEventListener("click", function () {
            // Remove "active" class from all buttons
            breweryButtons.forEach(btn => btn.classList.remove("active"));

            // Add "active" class to the clicked button
            button.classList.add("active");

            // Get the value of the clicked button
            const selectedBrewery = button.getAttribute("data-value");

            // Filter products based on the selected brewery
            const products = document.querySelectorAll('.breweryItem');
            products.forEach(product => {
                const breweryName = product.getAttribute('data-brewery');
                if (selectedBrewery === breweryName || selectedBrewery === "") {
                    product.style.display = "block";
                } else {
                    product.style.display = "none";
                }
            });
        });
    });
});