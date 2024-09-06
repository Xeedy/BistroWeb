document.getElementById('printButton').addEventListener('click', function () {
    // Otevøení nového okna pro tisk
    var printWindow = window.open('', '_blank');

    // Pokud je printWindow platné, pokraèujeme
    if (printWindow) {
        printWindow.document.open();
        printWindow.document.write('<html><head><title>Tisk seznamu položek</title></head><body>');

        // Zde mùžete vložit specifické styly nebo formátování pro tisk
        printWindow.document.write('<h1>Seznam položek</h1>');

        // Projdeme všechny aktivní produkty a vytiskneme jejich detaily
        @foreach(var product in Model.Where(p => p.Active))
{
    <text>
        var isNew = @product.New.ToString().ToLower();
        var imageURL = '@product.ImageSrc';
        var productName = '@product.Name';
        var price = '@product.Price CZK';
        var breweryName = '@product.Breweries?.Name';
        var typeName = '@product.Typees?.Name';

        printWindow.document.write('<div>');
            if (isNew) {
                printWindow.document.write('<div class="new-badge">Novinka</div>');
                        }
            printWindow.document.write('<img src="' + imageURL + '" alt="..." />');
            printWindow.document.write('<h5>' + productName + '</h5>');
            printWindow.document.write('<p>' + price + '</p>');
            printWindow.document.write('<p>' + breweryName + '</p>');
            printWindow.document.write('<p>' + typeName + '</p>');
            printWindow.document.write('</div>');
    </text>
}

printWindow.document.write('</body></html>');
printWindow.document.close();

// Spuštìní tisku
printWindow.print();
            } else {
    alert('Nepodaøilo se otevøít okno pro tisk. Ujistìte se, že nemáte blokován vyskakovací okna.');
}
        });

