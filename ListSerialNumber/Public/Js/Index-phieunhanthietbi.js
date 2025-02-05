window.onload = function () {
    const toggleTenKHPhieuNhanThietBi = document.getElementById("toggleTenKHPhieuNhanThietBi");
    const toggleTenTBPhieuNhanThietBi = document.getElementById("toggleTenTBPhieuNhanThietBi");
    const toggleSoSNPhieuNhanThietBi = document.getElementById("toggleSoSNPhieuNhanThietBi");
    const toggleNgayTHPhieuNhanThietBi = document.getElementById("toggleNgayTHPhieuNhanThietBi");
    const toggleNguoiTHPhieuNhanThietBi = document.getElementById("toggleNguoiTHPhieuNhanThietBi");
    const toggleHienTrangPhieuNhanThietBi = document.getElementById("toggleHienTrangPhieuNhanThietBi");

    const isTenKHPhieuNhanThietBiVisible = localStorage.getItem("TenKHPhieuNhanThietBiColumnVisible") === "true";
    const isTenTBPhieuNhanThietBiVisible = localStorage.getItem("TenTBPhieuNhanThietBiColumnVisible") === "true";
    const isSoSNPhieuNhanThietBiVisible = localStorage.getItem("SoSNPhieuNhanThietBiColumnVisible") === "true";
    const isNgayTHPhieuNhanThietBiVisible = localStorage.getItem("NgayTHPhieuNhanThietBiColumnVisible") === "true";
    const isNguoiTHPhieuNhanThietBiVisible = localStorage.getItem("NguoiTHPhieuNhanThietBiColumnVisible") === "true";
    const isHienTrangPhieuNhanThietBiVisible = localStorage.getItem("HienTrangPhieuNhanThietBiColumnVisible") === "true";

    toggleTenKHPhieuNhanThietBi.checked = isTenKHPhieuNhanThietBiVisible;
    toggleTenTBPhieuNhanThietBi.checked = isTenTBPhieuNhanThietBiVisible;
    toggleSoSNPhieuNhanThietBi.checked = isSoSNPhieuNhanThietBiVisible;
    toggleNgayTHPhieuNhanThietBi.checked = isNgayTHPhieuNhanThietBiVisible;
    toggleNguoiTHPhieuNhanThietBi.checked = isNguoiTHPhieuNhanThietBiVisible;
    toggleHienTrangPhieuNhanThietBi.checked = isHienTrangPhieuNhanThietBiVisible;

    toggleTenKHPhieuNhanThietBi.dispatchEvent(new Event('change'));
    toggleTenTBPhieuNhanThietBi.dispatchEvent(new Event('change'));
    toggleSoSNPhieuNhanThietBi.dispatchEvent(new Event('change'));
    toggleNgayTHPhieuNhanThietBi.dispatchEvent(new Event('change'));
    toggleNguoiTHPhieuNhanThietBi.dispatchEvent(new Event('change'));
    toggleHienTrangPhieuNhanThietBi.dispatchEvent(new Event('change'));
};
document.getElementById("toggleHienTrangPhieuNhanThietBi").addEventListener("change", function () {
    localStorage.setItem("HienTrangPhieuNhanThietBiColumnVisible", this.checked);
    var HienTrangPhieuNhanThietBi = document.querySelectorAll(".HienTrang-column-phieunhanthietbi");
    HienTrangPhieuNhanThietBi.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNguoiTHPhieuNhanThietBi").addEventListener("change", function () {
    localStorage.setItem("NguoiTHPhieuNhanThietBiColumnVisible", this.checked);
    var NguoiTHPhieuNhanThietBiColumns = document.querySelectorAll(".NguoiThucHien-column-phieunhanthietbi");
    NguoiTHPhieuNhanThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNgayTHPhieuNhanThietBi").addEventListener("change", function () {
    localStorage.setItem("NgayTHPhieuNhanThietBiColumnVisible", this.checked);
    var NgayTHPhieuNhanThietBiColumns = document.querySelectorAll(".NgayThucHien-column-phieunhanthietbi");
    NgayTHPhieuNhanThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleSoSNPhieuNhanThietBi").addEventListener("change", function () {
    localStorage.setItem("SoSNPhieuNhanThietBiColumnVisible", this.checked);
    var SoSNPhieuNhanThietBiColumns = document.querySelectorAll(".SoSN-column-phieunhanthietbi");
    SoSNPhieuNhanThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleTenTBPhieuNhanThietBi").addEventListener("change", function () {
    localStorage.setItem("TenTBPhieuNhanThietBiColumnVisible", this.checked);
    var TenTBPhieuNhanThietBiColumns = document.querySelectorAll(".TenTB-column-phieunhanthietbi");
    TenTBPhieuNhanThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleTenKHPhieuNhanThietBi").addEventListener("change", function () {
    localStorage.setItem("TenKHPhieuNhanThietBiColumnVisible", this.checked);
    var TenKHPhieuNhanThietBiColumns = document.querySelectorAll(".TenKH-column-phieunhanthietbi");
    TenKHPhieuNhanThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});