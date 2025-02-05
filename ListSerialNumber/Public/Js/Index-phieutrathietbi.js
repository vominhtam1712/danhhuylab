window.onload = function () {
    const toggleTenKHPhieuTraTB = document.getElementById("toggleTenKHPhieuTraTB");
    const toggleTenTBPhieuTraTB = document.getElementById("toggleTenTBPhieuTraTB");
    const toggleSoSNPhieuTraTB = document.getElementById("toggleSoSNPhieuTraTB");
    const toggleNgayThucHienPhieuTraTB = document.getElementById("toggleNgayThucHienPhieuTraTB");
    const togglePhuongThucGiaoTraPhieuTraTB = document.getElementById("togglePhuongThucGiaoTraPhieuTraTB");
    const toggleTrangThaiThietBiPhieuTraTB = document.getElementById("toggleTrangThaiThietBiPhieuTraTB");

    const isTenKHPhieuTraTBVisible = localStorage.getItem("TenKHPhieuTraTBColumnVisible") === "true";
    const isTenTBPhieuTraTBVisible = localStorage.getItem("TenTBPhieuTraTBColumnVisible") === "true";
    const isSNPhieuTraTBVisible = localStorage.getItem("SNPhieuTraTBColumnVisible") === "true";
    const isNgayThucHienPhieuTraTBVisible = localStorage.getItem("NgayThucHienPhieuTraTBColumnVisible") === "true";
    const isPhuongThucGiaoTraPhieuTraTBVisible = localStorage.getItem("PhuongThucGiaoTraPhieuTraTBColumnVisible") === "true";
    const isTrangThaiThietBiPhieuTraTBVisible = localStorage.getItem("TrangThaiThietBiPhieuTraTBColumnVisible") === "true";

    toggleTenKHPhieuTraTB.checked = isTenKHPhieuTraTBVisible;
    toggleTenTBPhieuTraTB.checked = isTenTBPhieuTraTBVisible;
    toggleSoSNPhieuTraTB.checked = isSNPhieuTraTBVisible;
    toggleNgayThucHienPhieuTraTB.checked = isNgayThucHienPhieuTraTBVisible;
    togglePhuongThucGiaoTraPhieuTraTB.checked = isPhuongThucGiaoTraPhieuTraTBVisible;
    toggleTrangThaiThietBiPhieuTraTB.checked = isTrangThaiThietBiPhieuTraTBVisible;

    toggleTenKHPhieuTraTB.dispatchEvent(new Event('change'));
    toggleTenTBPhieuTraTB.dispatchEvent(new Event('change'));
    toggleSoSNPhieuTraTB.dispatchEvent(new Event('change'));
    toggleNgayThucHienPhieuTraTB.dispatchEvent(new Event('change'));
    togglePhuongThucGiaoTraPhieuTraTB.dispatchEvent(new Event('change'));
    toggleTrangThaiThietBiPhieuTraTB.dispatchEvent(new Event('change'));
};
document.getElementById("toggleTrangThaiThietBiPhieuTraTB").addEventListener("change", function () {
    localStorage.setItem("TrangThaiThietBiPhieuTraTBColumnVisible", this.checked);
    var TrangThaiThietBiPhieuTraTB = document.querySelectorAll(".TrangThaiThietBi-column-phieutrathietbi");
    TrangThaiThietBiPhieuTraTB.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("togglePhuongThucGiaoTraPhieuTraTB").addEventListener("change", function () {
    localStorage.setItem("PhuongThucGiaoTraPhieuTraTBColumnVisible", this.checked);
    var PhuongThucGiaoTraPhieuTraTB = document.querySelectorAll(".PhuongThucGiaoTra-column-phieutrathietbi");
    PhuongThucGiaoTraPhieuTraTB.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNgayThucHienPhieuTraTB").addEventListener("change", function () {
    localStorage.setItem("NgayThucHienPhieuTraTBColumnVisible", this.checked);
    var NgayThucHienPhieuTraTB = document.querySelectorAll(".NgayThucHien-column-phieutrathietbi");
    NgayThucHienPhieuTraTB.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleSoSNPhieuTraTB").addEventListener("change", function () {
    localStorage.setItem("SNPhieuTraTBColumnVisible", this.checked);
    var SNPhieuTraTB = document.querySelectorAll(".SoSN-column-phieutrathietbi");
    SNPhieuTraTB.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleTenTBPhieuTraTB").addEventListener("change", function () {
    localStorage.setItem("TenTBPhieuTraTBColumnVisible", this.checked);
    var TenTBPhieuTraTB = document.querySelectorAll(".TenTB-column-phieutrathietbi");
    TenTBPhieuTraTB.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleTenKHPhieuTraTB").addEventListener("change", function () {
    localStorage.setItem("TenKHPhieuTraTBColumnVisible", this.checked);
    var TenKHPhieuTraTB = document.querySelectorAll(".TenKH-column-phieutrathietbi");
    TenKHPhieuTraTB.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});