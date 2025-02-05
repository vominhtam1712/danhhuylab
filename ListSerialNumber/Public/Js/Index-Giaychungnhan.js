window.onload = function () {
    const toggleSerialNumberGiaychungnhan = document.getElementById("toggleSerialNumberGiaychungnhan");
    const toggleTenthietbiGiaychungnhan = document.getElementById("toggleTenthietbiGiaychungnhan");
    const toggleModelGiaychungnhan = document.getElementById("toggleModelGiaychungnhan");
    const toggleNguoiTaoGiaychungnhan = document.getElementById("toggleNguoiTaoGiaychungnhan");
    const toggleNgayTaoGiaychungnhan = document.getElementById("toggleNgayTaoGiaychungnhan");

    const isSerialNumberGiaychungnhanVisible = localStorage.getItem("SerialNumberGiaychungnhanColumnVisible") === "true";
    const isTenthietbiGiaychungnhanVisible = localStorage.getItem("TenthietbiGiaychungnhanColumnVisible") === "true";
    const isModelGiaychungnhanVisible = localStorage.getItem("ModelGiaychungnhanColumnVisible") === "true";
    const isNguoiTaoGiaychungnhanVisible = localStorage.getItem("NguoiTaoGiaychungnhanColumnVisible") === "true";
    const isNgayTaoGiaychungnhanVisible = localStorage.getItem("NgayTaoGiaychungnhanColumnVisible") === "true";

    toggleSerialNumberGiaychungnhan.checked = isSerialNumberGiaychungnhanVisible; 
    toggleTenthietbiGiaychungnhan.checked = isTenthietbiGiaychungnhanVisible; 
    toggleModelGiaychungnhan.checked = isModelGiaychungnhanVisible; 
    toggleNguoiTaoGiaychungnhan.checked = isNguoiTaoGiaychungnhanVisible; 
    toggleNgayTaoGiaychungnhan.checked = isNgayTaoGiaychungnhanVisible; 

    toggleSerialNumberGiaychungnhan.dispatchEvent(new Event('change')); 
    toggleTenthietbiGiaychungnhan.dispatchEvent(new Event('change')); 
    toggleModelGiaychungnhan.dispatchEvent(new Event('change')); 
    toggleNguoiTaoGiaychungnhan.dispatchEvent(new Event('change')); 
    toggleNgayTaoGiaychungnhan.dispatchEvent(new Event('change')); 
};

document.getElementById("toggleSerialNumberGiaychungnhan").addEventListener("change", function () {
    localStorage.setItem("SerialNumberGiaychungnhanColumnVisible", this.checked);
    var SerialNumberGiaychungnhanColumns = document.querySelectorAll(".SerialNumber-column-Giaychungnhan");
    SerialNumberGiaychungnhanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleTenthietbiGiaychungnhan").addEventListener("change", function () {
    localStorage.setItem("TenthietbiGiaychungnhanColumnVisible", this.checked);
    var TenthietbiGiaychungnhanColumns = document.querySelectorAll(".Tenthietbi-column-Giaychungnhan");
   TenthietbiGiaychungnhanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleModelGiaychungnhan").addEventListener("change", function () {
    localStorage.setItem("ModelGiaychungnhanColumnVisible", this.checked);
    var ModelGiaychungnhanColumns = document.querySelectorAll(".Model-column-Giaychungnhan");
    ModelGiaychungnhanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleNguoiTaoGiaychungnhan").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoGiaychungnhanColumnVisible", this.checked);
    var NguoiTaoGiaychungnhanColumns = document.querySelectorAll(".NguoiTao-column-Giaychungnhan");
    NguoiTaoGiaychungnhanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleNgayTaoGiaychungnhan").addEventListener("change", function () {
    localStorage.setItem("NgayTaoGiaychungnhanColumnVisible", this.checked);
    var NgayTaoGiaychungnhanColumns = document.querySelectorAll(".NgayTao-column-Giaychungnhan");
    NgayTaoGiaychungnhanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 