document.addEventListener('DOMContentLoaded', () => {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const dayCells = document.querySelectorAll('.calendar-table td:not(.empty-day)');
    let lastActiveCell = null;
    const assignShiftContainer = document.getElementById("assignShiftContainer");
    const selectedDateInput = document.getElementById('selectedDate');

    function clearActiveState() {
        dayCells.forEach((c) => {
            c.classList.remove('active-day');
        });
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

                const overwriteConfirmed = this.querySelector('.user-name') ? confirm('A shift is already assigned for this day. Do you really want to overwrite the existing shift?') : true;

                if (overwriteConfirmed) {
                    this.classList.add('active-day');
                    lastActiveCell = this;

                    const date = this.getAttribute('data-date');
                    selectedDateInput.value = date;

                    const dateObj = new Date(date);
                    const formattedDate = new Intl.DateTimeFormat('cs-CZ', { day: 'numeric', month: 'long', year: 'numeric' }).format(dateObj);
                    assignShiftContainer.querySelector('h2').textContent = `Select date: ${formattedDate}`;

                    const assignedUserNameElement = this.querySelector('.user-name');
                    if (assignedUserNameElement) {
                        document.getElementById('assignedUserName').textContent = assignedUserNameElement.textContent;
                        document.getElementById('assignedUserInfo').style.display = 'block';
                    } else {
                        document.getElementById('assignedUserInfo').style.display = 'none';
                    }

                    assignShiftContainer.classList.add("active");
                }
            });

            cell.addEventListener('contextmenu', function (event) {
                event.preventDefault(); // Prevent default context menu

                const assignedUserNameElement = this.querySelector('.user-name');
                if (assignedUserNameElement) {
                    const assignedShiftId = assignedUserNameElement.getAttribute('data-shift-id');
                    const deleteConfirmed = confirm('Are you sure you want to delete the assigned shift for this day?');
                    if (deleteConfirmed) {
                        // Send a request to delete the shift with the assignedShiftId
                        fetch(`/Calendar/DeleteShift?id=${assignedShiftId}`, {
                            method: 'POST'
                        })
                            .then(response => {
                                if (response.ok) {
                                    // Shift deleted successfully
                                    this.removeChild(assignedUserNameElement); // Remove assigned user name from cell
                                    alert('Shift deleted successfully.');
                                } else {
                                    // Error deleting shift
                                    alert('Error deleting shift.');
                                }
                            })
                            .catch(error => {
                                console.error('Error deleting shift:', error);
                                alert('Error deleting shift.');
                            });
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
