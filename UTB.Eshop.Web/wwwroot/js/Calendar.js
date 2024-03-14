document.addEventListener('DOMContentLoaded', () => {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const dayCells = document.querySelectorAll('.calendar-table td:not(.empty-day)');
    let lastActiveCell = null;
    const assignShiftContainer = document.getElementById("assignShiftContainer");
    const selectedDateInput = document.getElementById('selectedDate');
    const deleteShiftIdInput = document.getElementById('deleteShiftId');

    function clearActiveState() {
        dayCells.forEach((c) => {
            c.classList.remove('active-day');
        });
        document.getElementById('assignShiftForm').style.display = 'none';
        assignShiftContainer.classList.remove("active");
        if (lastActiveCell) {
            lastActiveCell.classList.remove('active-day');
            lastActiveCell = null;
            document.getElementById('assignedUserInfo').style.display = 'none';
        }
    }

    dayCells.forEach((cell) => {
        const cellDate = new Date(cell.getAttribute('data-date'));
        if (cellDate >= today) {
            cell.addEventListener('click', function () {
                clearActiveState();
                document.getElementById('assignShiftForm').style.display = 'block';
                const overwriteConfirmed = this.querySelector('.user-name') ? confirm('Směna je obsazená jiným uživatelem. Opravdu si přejete změnit směnu?') : true;

                if (overwriteConfirmed) {
                    this.classList.add('active-day');
                    lastActiveCell = this;

                    const date = this.getAttribute('data-date');
                    selectedDateInput.value = date;

                    const dateObj = new Date(date);
                    const formattedDate = new Intl.DateTimeFormat('cs-CZ', { day: 'numeric', month: 'long', year: 'numeric' }).format(dateObj);
                    assignShiftContainer.querySelector('h2').textContent = `${formattedDate}`;

                    const assignedUserNameElement = this.querySelector('.user-name');
                    if (assignedUserNameElement) {
                        const assignedShiftId = assignedUserNameElement.getAttribute('data-shift-id');
                        deleteShiftIdInput.value = assignedShiftId; // Store shift ID for deletion
                        document.getElementById('deleteShiftContainer').style.display = 'block'; // Show the delete button
                    } else {
                        document.getElementById('deleteShiftContainer').style.display = 'none'; // Hide the delete button if no shift is assigned
                    }

                    assignShiftContainer.classList.add("active");
                }
            });

            cell.addEventListener('contextmenu', function (event) {
                event.preventDefault(); // Prevent default context menu

                const assignedUserNameElement = this.querySelector('.user-name');
                if (assignedUserNameElement) {
                    // Retrieve the stored shift ID
                    const assignedShiftId = assignedUserNameElement.getAttribute('data-shift-id');
                    const deleteConfirmed = confirm('Are you sure you want to delete the assigned shift for this day?');
                    if (deleteConfirmed) {
                        // Use the retrieved shift ID to populate the hidden input in the delete form
                        document.getElementById('deleteShiftId').value = assignedShiftId;
                        // Submit the delete form
                        document.getElementById('deleteShiftForm').submit();
                    }
                }
            });
        }
    });

    document.addEventListener('click', function (event) {
        if (lastActiveCell && !lastActiveCell.contains(event.target) && !event.target.matches('#assignShiftContainer, #assignShiftContainer *')) {
            clearActiveState();
        }
    }, true);
});