document.addEventListener('DOMContentLoaded', (event) => {
    var today = new Date();
    today.setHours(0, 0, 0, 0); // Reset time to the start of the day for comparison

    var dayCells = document.querySelectorAll('.calendar-table td:not(.empty-day)');
    var lastActiveCell = null; // Keep track of the last active cell
    var assignShiftContainer = document.getElementById("assignShiftContainer");
    var selectedDateInput = document.getElementById('selectedDate'); // The hidden input for the date
    var removeAssignmentButton = document.getElementById('removeAssignmentButton'); // The button to remove assignment

    // Function to clear active state from all cells
    function clearActiveState() {
        dayCells.forEach(function (c) {
            c.classList.remove('active-day');
        });
        assignShiftContainer.classList.remove("active");
        if (lastActiveCell) {
            lastActiveCell.classList.remove('active-day');
            lastActiveCell = null;
            document.getElementById('assignedUserInfo').style.display = 'none';
        }
    }

    dayCells.forEach(function (cell) {
        var cellDate = new Date(cell.getAttribute('data-date'));
        if (cellDate >= today) { // Only add click listener if the cell date is not in the past
            cell.addEventListener('click', function () {
                // Clear active states before any new assignment
                clearActiveState();

                // Logic for managing shift assignment
                var overwriteConfirmed = this.querySelector('.user-name') ? confirm('Již je přiřazena směna pro tento den. Opravdu chcete přepsat stávající směnu?') : true;

                if (overwriteConfirmed) {
                    // Highlight the clicked cell
                    this.classList.add('active-day');
                    lastActiveCell = this;

                    // Update the hidden input with the new date
                    var date = this.getAttribute('data-date');
                    selectedDateInput.value = date;

                    // Format the date and set in the assign shift container
                    var dateObj = new Date(date);
                    var formattedDate = new Intl.DateTimeFormat('cs-CZ', { day: 'numeric', month: 'long', year: 'numeric' }).format(dateObj);
                    assignShiftContainer.querySelector('h2').textContent = `Vyber datum: ${formattedDate}`;

                    // Show or hide the assigned user info based on current assignment
                    var assignedUserNameElement = this.querySelector('.user-name');
                    if (assignedUserNameElement) {
                        document.getElementById('assignedUserName').textContent = assignedUserNameElement.textContent;
                        document.getElementById('assignedUserInfo').style.display = 'block';

                        // Show the delete shift button
                        var assignedShiftId = assignedUserNameElement.getAttribute('data-shift-id');
                        removeAssignmentButton.setAttribute('data-shift-id', assignedShiftId);
                        removeAssignmentButton.style.display = 'inline-block';
                    } else {
                        document.getElementById('assignedUserInfo').style.display = 'none';

                        // Hide the delete shift button
                        removeAssignmentButton.removeAttribute('data-shift-id');
                        removeAssignmentButton.style.display = 'none';
                    }

                    // Show the assign shift container
                    assignShiftContainer.classList.add("active");
                }
            });
        }
    });
    // Listen for clicks outside of calendar cells to clear the active state
    document.addEventListener('click', function (event) {
        if (lastActiveCell && !lastActiveCell.contains(event.target) && !event.target.matches('#assignShiftContainer, #assignShiftContainer *')) {
            clearActiveState();
        }
    }, true); // Use capturing to ensure the document level event runs before the cell click
});
