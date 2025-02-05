window.onload = function () {
    const toggleTenThietBiPhanCong = document.getElementById("toggleTenThietBiPhanCong");
    const toggleModelPhanCong = document.getElementById("toggleModelPhanCong");
    const toggleSerialNumberPhanCong = document.getElementById("toggleSerialNumberPhanCong");
    const toggleNamePhanCong = document.getElementById("toggleNamePhanCong"); 

    const isTenThietBiPhanCongVisible = localStorage.getItem("TenThietBiPhanCongColumnVisible") === "true";
    const isModelPhanCongVisible = localStorage.getItem("ModelPhanCongColumnVisible") === "true";
    const isSerialNumberPhanCongVisible = localStorage.getItem("SerialNumberPhanCongColumnVisible") === "true";
    const isNamePhanCongVisible = localStorage.getItem("NamePhanCongColumnVisible") === "true"; 

    toggleTenThietBiPhanCong.checked = isTenThietBiPhanCongVisible;
    toggleModelPhanCong.checked = isModelPhanCongVisible;
    toggleSerialNumberPhanCong.checked = isSerialNumberPhanCongVisible;
    toggleNamePhanCong.checked = isNamePhanCongVisible; 

    toggleTenThietBiPhanCong.dispatchEvent(new Event('change'));
    toggleModelPhanCong.dispatchEvent(new Event('change'));
    toggleSerialNumberPhanCong.dispatchEvent(new Event('change'));
    toggleNamePhanCong.dispatchEvent(new Event('change')); 
};

document.getElementById("toggleTenThietBiPhanCong").addEventListener("change", function () {
    localStorage.setItem("TenThietBiPhanCongColumnVisible", this.checked);
    var TenThietBiPhanCongColumns = document.querySelectorAll(".TenThietBi-column-PhanCong");
    TenThietBiPhanCongColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleModelPhanCong").addEventListener("change", function () {
    localStorage.setItem("ModelPhanCongColumnVisible", this.checked);
    var ModelPhanCongColumns = document.querySelectorAll(".Model-column-PhanCong");
    ModelPhanCongColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleSerialNumberPhanCong").addEventListener("change", function () {
    localStorage.setItem("SerialNumberPhanCongColumnVisible", this.checked);
    var SerialNumberphanCongColumns = document.querySelectorAll(".SerialNumber-column-PhanCong");
    SerialNumberphanCongColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNamePhanCong").addEventListener("change", function () {
    localStorage.setItem("NamePhanCongColumnVisible", this.checked);
    var NamephanCongColumns = document.querySelectorAll(".Name-column-PhanCong");
    NamephanCongColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 