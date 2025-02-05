window.onload = function () {
    const toggleTenKHPhieuChuyenMauHieuChuan = document.getElementById("toggleTenKHPhieuChuyenMauHieuChuan");
    const toggleSoSNPhieuChuyenMauHieuChuan = document.getElementById("toggleSoSNPhieuChuyenMauHieuChuan");
    const toggleNguoiNhanPhieuChuyenMauHieuChuan = document.getElementById("toggleNguoiNhanPhieuChuyenMauHieuChuan");
    const toggleNguoiDuocPCPhieuChuyenMauHieuChuan = document.getElementById("toggleNguoiDuocPCPhieuChuyenMauHieuChuan");
    const toggleNgayNhanPhieuChuyenMauHieuChuan = document.getElementById("toggleNgayNhanPhieuChuyenMauHieuChuan");
    const toggleNgayTraPhieuChuyenMauHieuChuan = document.getElementById("toggleNgayTraPhieuChuyenMauHieuChuan");

    const isTenKHPhieuChuyenMauHieuChuanVisible = localStorage.getItem("TenKHPhieuChuyenMauHieuChuanColumnVisible") === "true";
    const isSoSNPhieuChuyenMauHieuChuanVisible = localStorage.getItem("SoSNPhieuChuyenMauHieuChuanColumnVisible") === "true";
    const isNguoiNhanPhieuChuyenMauHieuChuanVisible = localStorage.getItem("NguoiNhanPhieuChuyenMauHieuChuanColumnVisible") === "true";
    const isNguoiDuocPCPhieuChuyenMauHieuChuanVisible = localStorage.getItem("NguoiDuocPCPhieuChuyenMauHieuChuanColumnVisible") === "true";
    const isNgayNhanPhieuChuyenMauHieuChuanVisible = localStorage.getItem("NgayNhanPhieuChuyenMauHieuChuanColumnVisible") === "true";
    const isNgayTraPhieuChuyenMauHieuChuanVisible = localStorage.getItem("NgayTraPhieuChuyenMauHieuChuanColumnVisible") === "true";

    toggleTenKHPhieuChuyenMauHieuChuan.checked = isTenKHPhieuChuyenMauHieuChuanVisible;
    toggleSoSNPhieuChuyenMauHieuChuan.checked = isSoSNPhieuChuyenMauHieuChuanVisible;
    toggleNguoiNhanPhieuChuyenMauHieuChuan.checked = isNguoiNhanPhieuChuyenMauHieuChuanVisible;
    toggleNguoiDuocPCPhieuChuyenMauHieuChuan.checked = isNguoiDuocPCPhieuChuyenMauHieuChuanVisible;
    toggleNgayNhanPhieuChuyenMauHieuChuan.checked = isNgayNhanPhieuChuyenMauHieuChuanVisible;
    toggleNgayTraPhieuChuyenMauHieuChuan.checked = isNgayTraPhieuChuyenMauHieuChuanVisible;

    toggleTenKHPhieuChuyenMauHieuChuan.dispatchEvent(new Event('change'));
    toggleSoSNPhieuChuyenMauHieuChuan.dispatchEvent(new Event('change'));
    toggleNguoiNhanPhieuChuyenMauHieuChuan.dispatchEvent(new Event('change'));
    toggleNguoiDuocPCPhieuChuyenMauHieuChuan.dispatchEvent(new Event('change'));
    toggleNgayNhanPhieuChuyenMauHieuChuan.dispatchEvent(new Event('change'));
    toggleNgayTraPhieuChuyenMauHieuChuan.dispatchEvent(new Event('change'));
};
document.getElementById("toggleNgayTraPhieuChuyenMauHieuChuan").addEventListener("change", function () {
    localStorage.setItem("NgayTraPhieuChuyenMauHieuChuanColumnVisible", this.checked);
    var NgayTraPhieuChuyenMauHieuChuan = document.querySelectorAll(".NgayTra-column-PhieuChuyenMauHieuChuan");
    NgayTraPhieuChuyenMauHieuChuan.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNgayNhanPhieuChuyenMauHieuChuan").addEventListener("change", function () {
    localStorage.setItem("NgayNhanPhieuChuyenMauHieuChuanColumnVisible", this.checked);
    var NgayNhanPhieuChuyenMauHieuChuan = document.querySelectorAll(".NgayNhan-column-PhieuChuyenMauHieuChuan");
    NgayNhanPhieuChuyenMauHieuChuan.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNguoiDuocPCPhieuChuyenMauHieuChuan").addEventListener("change", function () {
    localStorage.setItem("NguoiDuocPCPhieuChuyenMauHieuChuanColumnVisible", this.checked);
    var NguoiDuocPCPhieuChuyenMauHieuChuan = document.querySelectorAll(".NguoiDuocPC-column-PhieuChuyenMauHieuChuan");
    NguoiDuocPCPhieuChuyenMauHieuChuan.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNguoiNhanPhieuChuyenMauHieuChuan").addEventListener("change", function () {
    localStorage.setItem("NguoiNhanPhieuChuyenMauHieuChuanColumnVisible", this.checked);
    var NguoiNhanPhieuChuyenMauHieuChuan = document.querySelectorAll(".NguoiNhan-column-PhieuChuyenMauHieuChuan");
    NguoiNhanPhieuChuyenMauHieuChuan.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleSoSNPhieuChuyenMauHieuChuan").addEventListener("change", function () {
    localStorage.setItem("SoSNPhieuChuyenMauHieuChuanColumnVisible", this.checked);
    var SoSNPhieuChuyenMauHieuChuan = document.querySelectorAll(".SoSN-column-PhieuChuyenMauHieuChuan");
    SoSNPhieuChuyenMauHieuChuan.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleTenKHPhieuChuyenMauHieuChuan").addEventListener("change", function () {
    localStorage.setItem("TenKHPhieuChuyenMauHieuChuanColumnVisible", this.checked);
    var TenKHPhieuChuyenMauHieuChuan = document.querySelectorAll(".TenKH-column-PhieuChuyenMauHieuChuan");
    TenKHPhieuChuyenMauHieuChuan.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});