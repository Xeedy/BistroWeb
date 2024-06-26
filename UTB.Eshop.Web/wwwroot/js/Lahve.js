$(document).ready(function () {
    $('.nav-link').click(function (event) {
        var tabId = $(this).attr('id');
        if (tabId) {
            resetInactiveTabs(tabId);
        }
    });
    $('#clearSearchAll').click(function () {
        $('#searchInputAll').val('');
        resetTable('#all-content', '#noResultsAll');
        resetInactiveTabs('all-tab'); // Pøidáme reset i pro aktivní tab s ID 'all-tab'
    });
    $('#clearSearchActive').click(function () {
        $('#searchInputActive').val('');
        resetTable('#active-content', '#noResultsActive');
        resetInactiveTabs('active-tab'); // Reset pro aktivní tab
    });
    $('#clearSearchInactive').click(function () {
        $('#searchInputInactive').val('');
        resetTable('#inactive-content', '#noResultsInactive');
        resetInactiveTabs('inactive-tab'); // Reset pro aktivní tab
    });
    $('#clearSearchNew').click(function () {
        $('#searchInputNew').val('');
        resetTable('#new-content', '#noResultsNew');
        resetInactiveTabs('new-tab'); // Reset pro aktivní tab
    });
    function resetInactiveTabs(currentTabId) {
        var $allTabs = $('.nav-link');

        $allTabs.each(function () {
            var tabId = $(this).attr('id');
            if (tabId) {
                var $searchInput = $('#searchInput' + tabId.charAt(0).toUpperCase() + tabId.slice(1).replace('-tab', ''));
                if ($searchInput.length > 0 && $searchInput.val() !== '') {
                    $searchInput.val('');
                    console.log(`Reset on tab: ${tabId}, search input cleared: yes`);
                } else {
                    console.log(`Reset on tab: ${tabId}, search input cleared: no`);
                }

                var $table = $('#myUniqueTabContent').find('#' + tabId.replace('-tab', '-content')).find('table');
                $table.find('tbody tr').show();

                var $noResults = $('#noResults' + tabId.charAt(0).toUpperCase() + tabId.slice(1));
                if ($noResults.is(':visible')) {
                    $noResults.hide();
                }
            }
        });

        // Zneviditelnit všechny hlášky "Žádné položky nebyly nalezeny" pro všechny taby
        $('.alert.alert-info').hide();
    }

    $('#searchButtonAll').click(function () {
        var searchInput = $('#searchInputAll');
        performSearch(searchInput, '#all-content', '#noResultsAll', searchInput.val());
    });

    $('#searchButtonActive').click(function () {
        var searchInput = $('#searchInputActive');
        performSearch(searchInput, '#active-content', '#noResultsActive', searchInput.val());
    });

    $('#searchButtonInactive').click(function () {
        var searchInput = $('#searchInputInactive');
        performSearch(searchInput, '#inactive-content', '#noResultsInactive', searchInput.val());
    });

    $('#searchButtonNew').click(function () {
        var searchInput = $('#searchInputNew');
        performSearch(searchInput, '#new-content', '#noResultsNew', searchInput.val());
    });

    $('#clearSearchActive').click(function () {
        $('#searchInputActive').val('');
        resetTable('#active-content', '#noResultsActive');
    });

    function performSearch(searchInput, tableId, noResultsId, searchText) {
        searchText = searchText.toLowerCase().trim();
        var found = false;

        console.log(`Performing search in ${tableId} for: "${searchText}"`);

        $(tableId + ' table tbody tr').each(function () {
            var $productNameCell = $(this).find('td:first');
            var $breweryNameCell = $(this).find('td:nth-child(3)'); // Tøetí sloupec pro název pivovaru

            var text = `${$productNameCell.text().toLowerCase()} ${$breweryNameCell.text().trim().toLowerCase()}`;

            var match = text.includes(searchText);

            $(this).toggle(match);

            if (match) {
                found = true;
                console.log(`Match found: Product: "${$productNameCell.text()}", Brewery: "${$breweryNameCell.text()}"`);
            }
        });

        // Zobrazit/zneviditelnit hlášku "Žádné položky nebyly nalezeny" podle nalezených výsledkù
        $(noResultsId).toggle(!found);

        console.log(`Search completed in ${tableId}. Found: ${found ? 'yes' : 'no'}`);
    }

    function resetTable(tableId, noResultsId) {
        $(tableId + ' table tbody tr').show();
        $(noResultsId).hide();
    }
});
