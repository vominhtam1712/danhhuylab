window.onload = function () {
    const toggleNgayTaoKeHoachHieuChuanForm = document.getElementById("toggleNgayTaoKeHoachHieuChuanForm");
    const toggleNguoiTaoKeHoachHieuChuanForm = document.getElementById("toggleNguoiTaoKeHoachHieuChuanForm");

    const isNgayTaoKeHoachHieuChuanFormVisible = localStorage.getItem("NgayTaoKeHoachHieuChuanFormColumnVisible") === "true";
    const isNguoiTaoKeHoachHieuChuanFormVisible = localStorage.getItem("NguoiTaoKeHoachHieuChuanFormlumnVisible") === "true";

    toggleNgayTaoKeHoachHieuChuanForm.checked = isNgayTaoKeHoachHieuChuanFormVisible;
    toggleNguoiTaoKeHoachHieuChuanForm.checked = isNguoiTaoKeHoachHieuChuanFormVisible;

    toggleNgayTaoKeHoachHieuChuanForm.dispatchEvent(new Event('change'));
    toggleNguoiTaoKeHoachHieuChuanForm.dispatchEvent(new Event('change'));
};

document.getElementById("toggleNgayTaoKeHoachHieuChuanForm").addEventListener("change", function () {
    localStorage.setItem("NgayTaoKeHoachHieuChuanFormColumnVisible", this.checked);
    var NgaytaoKeHoachHieuChuanFormColumns = document.querySelectorAll(".Ngaytao-column-KeHoachHieuChuanForm");
    NgaytaoKeHoachHieuChuanFormColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleNguoiTaoKeHoachHieuChuanForm").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoKeHoachHieuChuanFormlumnVisible", this.checked);
    var NguoiTaoKeHoachHieuChuanFormColumns = document.querySelectorAll(".Nguoitao-column-KeHoachHieuChuanForm");
    NguoiTaoKeHoachHieuChuanFormColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 