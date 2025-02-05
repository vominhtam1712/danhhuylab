<script>
    document.getElementById('copyButton').addEventListener('click', function() {
        // Lấy tất cả các checkbox đã chọn
        const selectedCheckboxes = document.querySelectorAll('input[name="selectedIds"]:checked');

    // Kiểm tra có ít nhất một hàng được chọn
    if (selectedCheckboxes.length === 0) {
        alert('Vui lòng chọn một hàng để sao chép!');
    return;
        }

    // Lặp qua từng checkbox đã chọn
    selectedCheckboxes.forEach(function(checkbox) {
            const row = checkbox.closest('tr'); // Lấy hàng tương ứng
    const newRow = row.cloneNode(true); // Sao chép hàng

    // Xóa các giá trị không cần thiết trong hàng mới
    const inputs = newRow.querySelectorAll('input[type="checkbox"]');
            inputs.forEach(input => input.checked = false); // Bỏ chọn checkbox
    const detailLink = newRow.querySelector('a'); // Lấy liên kết chi tiết
    if (detailLink) detailLink.innerText = 'Xem'; // Reset lại tên liên kết

    // Thêm hàng mới vào bảng
    document.querySelector('tbody').appendChild(newRow);
        });
    });
</script>
