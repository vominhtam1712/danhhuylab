$(document).ready(function () {
    $('#idDropdown').select2({
        width: '100%'
    });
    $('#idDropdownB').select2({
        width: '85%'
    });
});  
document.getElementById('selectAll').addEventListener('change', function () {
    var isChecked = this.checked;
    document.querySelectorAll('input[name="selectedIds"]').forEach(function (checkbox) {
        checkbox.checked = isChecked;
    });
});