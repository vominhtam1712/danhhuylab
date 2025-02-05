window.onload = function () { 
    const toggleTenThietBiLuuInHoSo = document.getElementById("toggleTenThietBiLuuInHoSo");
    const toggleModelLuuInHoSo = document.getElementById("toggleModelLuuInHoSo");
    const toggleSerialNumberLuuInHoSo = document.getElementById("toggleSerialNumberLuuInHoSo");
    const toggleNameLuuInHoSo = document.getElementById("toggleNameLuuInHoSo");
    const toggleNgayTaoLuuInHoSo = document.getElementById("toggleNgayTaoLuuInHoSo");
    const toggleNguoiTaoLuuInHoSo = document.getElementById("toggleNguoiTaoLuuInHoSo"); 

    const isTenThietBiLuuInHoSoVisible = localStorage.getItem("TenThietBiLuuInHoSoColumnVisible") === "true"; 
    const isModelLuuInHoSoVisible = localStorage.getItem("ModelLuuInHoSoColumnVisible") === "true"; 
    const isSerialNumberLuuInHoSoVisible = localStorage.getItem("SerialNumberLuuInHoSoColumnVisible") === "true"; 
    const isNameLuuInHoSoVisible = localStorage.getItem("NameLuuInHoSoColumnVisible") === "true"; 
    const isNgayTaoLuuInHoSoVisible = localStorage.getItem("NgayTaoLuuInHoSoColumnVisible") === "true"; 
    const isNguoiTaoLuuInHoSoVisible = localStorage.getItem("NguoiTaoLuuInHoSoColumnVisible") === "true"; 

    toggleTenThietBiLuuInHoSo.checked = isTenThietBiLuuInHoSoVisible; 
    toggleModelLuuInHoSo.checked = isModelLuuInHoSoVisible; 
    toggleSerialNumberLuuInHoSo.checked = isSerialNumberLuuInHoSoVisible; 
    toggleNameLuuInHoSo.checked = isNameLuuInHoSoVisible; 
    toggleNgayTaoLuuInHoSo.checked = isNgayTaoLuuInHoSoVisible; 
    toggleNguoiTaoLuuInHoSo.checked = isNguoiTaoLuuInHoSoVisible;  

    toggleTenThietBiLuuInHoSo.dispatchEvent(new Event('change')); 
    toggleModelLuuInHoSo.dispatchEvent(new Event('change')); 
    toggleSerialNumberLuuInHoSo.dispatchEvent(new Event('change')); 
    toggleNameLuuInHoSo.dispatchEvent(new Event('change')); 
    toggleNgayTaoLuuInHoSo.dispatchEvent(new Event('change')); 
    toggleNguoiTaoLuuInHoSo.dispatchEvent(new Event('change'));  
};

document.getElementById("toggleTenThietBiLuuInHoSo").addEventListener("change", function () {
    localStorage.setItem("TenThietBiLuuInHoSoColumnVisible", this.checked);
    var TenThietBiColumns = document.querySelectorAll(".TenThietBi-column-LuuInHoSo");
    TenThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 

document.getElementById("toggleModelLuuInHoSo").addEventListener("change", function () {
    localStorage.setItem("ModelLuuInHoSoColumnVisible", this.checked);
    var ModelColumns = document.querySelectorAll(".Model-column-LuuInHoSo");
    ModelColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 

document.getElementById("toggleSerialNumberLuuInHoSo").addEventListener("change", function () {
    localStorage.setItem("SerialNumberLuuInHoSoColumnVisible", this.checked);
    var SerialNumberColumns = document.querySelectorAll(".SerialNumber-column-LuuInHoSo");
    SerialNumberColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 

document.getElementById("toggleNameLuuInHoSo").addEventListener("change", function () {
    localStorage.setItem("NameLuuInHoSoColumnVisible", this.checked);
    var NameColumns = document.querySelectorAll(".Name-column-LuuInHoSo");
    NameColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 

document.getElementById("toggleNgayTaoLuuInHoSo").addEventListener("change", function () {
    localStorage.setItem("NgayTaoLuuInHoSoColumnVisible", this.checked);
    var NgayTaoColumns = document.querySelectorAll(".NgayTao-column-LuuInHoSo");
    NgayTaoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 

document.getElementById("toggleNguoiTaoLuuInHoSo").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoLuuInHoSoColumnVisible", this.checked);
    var NguoiTaoColumns = document.querySelectorAll(".NguoiTao-column-LuuInHoSo");
    NguoiTaoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});  