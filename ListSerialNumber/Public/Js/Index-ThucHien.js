window.onload = function () {
    const toggleTenThietBiThucHien = document.getElementById("toggleTenThietBiThucHien");
    const toggleModelThucHien = document.getElementById("toggleModelThucHien");
    const toggleSerialNumberThucHien = document.getElementById("toggleSerialNumberThucHien");
    const toggleNameThucHien = document.getElementById("toggleNameThucHien");
    const toggleNgayTaoThucHien = document.getElementById("toggleNgayTaoThucHien");
    const toggleNguoiTaoThucHien = document.getElementById("toggleNguoiTaoThucHien");
    const toggleStatusThucHien = document.getElementById("toggleStatusThucHien"); 

    const isTenThietBiThucHienVisible = localStorage.getItem("TenThietBiThucHienColumnVisible") === "true";
    const isModelThucHienVisible = localStorage.getItem("ModelThucHienColumnVisible") === "true";
    const isSerialNumberThucHienVisible = localStorage.getItem("SerialNumberThucHienColumnVisible") === "true";
    const isNameThucHienVisible = localStorage.getItem("NameThucHienColumnVisible") === "true";
    const isNgayTaoThucHienVisible = localStorage.getItem("NgayTaoThucHienColumnVisible") === "true";
    const isNguoiTaoThucHienVisible = localStorage.getItem("NguoiTaoThucHienColumnVisible") === "true";
    const isStatusThucHienVisible = localStorage.getItem("StatusThucHienColumnVisible") === "true"; 

    toggleTenThietBiThucHien.checked = isTenThietBiThucHienVisible;
    toggleModelThucHien.checked = isModelThucHienVisible;
    toggleSerialNumberThucHien.checked = isSerialNumberThucHienVisible;
    toggleNameThucHien.checked = isNameThucHienVisible;
    toggleNgayTaoThucHien.checked = isNgayTaoThucHienVisible;
    toggleNguoiTaoThucHien.checked = isNguoiTaoThucHienVisible;
    toggleStatusThucHien.checked = isStatusThucHienVisible; 

    toggleTenThietBiThucHien.dispatchEvent(new Event('change'));
    toggleModelThucHien.dispatchEvent(new Event('change'));
    toggleSerialNumberThucHien.dispatchEvent(new Event('change'));
    toggleNameThucHien.dispatchEvent(new Event('change'));
    toggleNgayTaoThucHien.dispatchEvent(new Event('change'));
    toggleNguoiTaoThucHien.dispatchEvent(new Event('change'));
    toggleStatusThucHien.dispatchEvent(new Event('change')); 
}; 
document.getElementById("toggleTenThietBiThucHien").addEventListener("change", function () {
    localStorage.setItem("TenThietBiThucHienColumnVisible", this.checked);
    var TenThietBiThucHienColumns = document.querySelectorAll(".TenThietBi-column-ThucHien");
    TenThietBiThucHienColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleModelThucHien").addEventListener("change", function () {
    localStorage.setItem("ModelThucHienColumnVisible", this.checked);
    var ModelThucHienColumns = document.querySelectorAll(".Model-column-ThucHien");
    ModelThucHienColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleSerialNumberThucHien").addEventListener("change", function () {
    localStorage.setItem("SerialNumberThucHienColumnVisible", this.checked);
    var SerialNumberThucHienColumns = document.querySelectorAll(".SerialNumber-column-ThucHien");
    SerialNumberThucHienColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNameThucHien").addEventListener("change", function () {
    localStorage.setItem("NameThucHienColumnVisible", this.checked);
    var NameThucHienColumns = document.querySelectorAll(".Name-column-ThucHien");
   NameThucHienColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNgayTaoThucHien").addEventListener("change", function () {
    localStorage.setItem("NgayTaoThucHienColumnVisible", this.checked);
    var NgayTaoThucHienColumns = document.querySelectorAll(".NgayTao-column-ThucHien");
    NgayTaoThucHienColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNguoiTaoThucHien").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoThucHienColumnVisible", this.checked);
    var NguoiTaoThucHienColumns = document.querySelectorAll(".NguoiTao-column-ThucHien");
    NguoiTaoThucHienColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleStatusThucHien").addEventListener("change", function () {
    localStorage.setItem("StatusThucHienColumnVisible", this.checked);
    var StatusThucHienColumns = document.querySelectorAll(".Status-column-ThucHien");
    StatusThucHienColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});