window.onload = function () {
    const toggleDiaChiKH = document.getElementById("toggleDiaChiKH");
    const toggleEmailKH = document.getElementById("toggleEmailKH");
    const toggleSDTKH = document.getElementById("toggleSDTKH");
    const toggleLienHeKH = document.getElementById("toggleLienHeKH"); 
    const toggleNhomNganhKH = document.getElementById("toggleNhomNganhKH");
    const toggleGhiChuKH = document.getElementById("toggleGhiChuKH");

    const isDiaChiKHVisible = localStorage.getItem("DiaChiKHColumnVisible") === "true";
    const isEmailKHVisible = localStorage.getItem("EmailKHColumnVisible") === "true";
    const isSDTKHVisible = localStorage.getItem("SDTKHColumnVisible") === "true";
    const isLienHeKHVisible = localStorage.getItem("LienHeKHColumnVisible") === "true";
    const isNhomNganhKHVisible = localStorage.getItem("NhomNganhKHColumnVisible") === "true";
    const isGhiChuKHVisible = localStorage.getItem("GhiChuKHColumnVisible") === "true";

    toggleDiaChiKH.checked = isDiaChiKHVisible;
    toggleEmailKH.checked = isEmailKHVisible;
    toggleSDTKH.checked = isSDTKHVisible;
    toggleLienHeKH.checked = isLienHeKHVisible;
    toggleNhomNganhKH.checked = isNhomNganhKHVisible;
    toggleGhiChuKH.checked = isGhiChuKHVisible;

    toggleDiaChiKH.dispatchEvent(new Event('change'));
    toggleEmailKH.dispatchEvent(new Event('change'));
    toggleSDTKH.dispatchEvent(new Event('change'));
    toggleLienHeKH.dispatchEvent(new Event('change'));
    toggleNhomNganhKH.dispatchEvent(new Event('change'));
    toggleGhiChuKH.dispatchEvent(new Event('change'));
};

document.getElementById("toggleDiaChiKH").addEventListener("change", function () {
    localStorage.setItem("DiaChiKHColumnVisible", this.checked);
    var DiaChiKHColumns = document.querySelectorAll(".DiaChi-KH-column");
    DiaChiKHColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleEmailKH").addEventListener("change", function () {
    localStorage.setItem("EmailKHColumnVisible", this.checked);
    var EmailKHColumns = document.querySelectorAll(".Email-KH-column");
    EmailKHColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleSDTKH").addEventListener("change", function () {
    localStorage.setItem("SDTKHColumnVisible", this.checked);
    var SDTKHColumns = document.querySelectorAll(".SDT-KH-column");
    SDTKHColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleLienHeKH").addEventListener("change", function () {
    localStorage.setItem("LienHeKHColumnVisible", this.checked);
    var LienHeKHColumns = document.querySelectorAll(".LienHe-KH-column");
    LienHeKHColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNhomNganhKH").addEventListener("change", function () {
    localStorage.setItem("NhomNganhKHColumnVisible", this.checked);
    var NhomNganhKHColumns = document.querySelectorAll(".NhomNganh-KH-column");
    NhomNganhKHColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleGhiChuKH").addEventListener("change", function () {
    localStorage.setItem("GhiChuKHColumnVisible", this.checked);
    var GhiChuKHColumns = document.querySelectorAll(".Ghichu-KH-column");
    GhiChuKHColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});