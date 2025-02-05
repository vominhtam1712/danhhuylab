window.onload = function () {
    const toggleTenthietbiBaoCaoSuaChua = document.getElementById("toggleTenthietbiBaoCaoSuaChua"); 
    const toggleSNBaoCaoSuaChua = document.getElementById("toggleSNBaoCaoSuaChua"); 
    const toggleHangBaoCaoSuaChua = document.getElementById("toggleHangBaoCaoSuaChua"); 
    const toggleModelBaoCaoSuaChua = document.getElementById("toggleModelBaoCaoSuaChua"); 
    const toggleTenKHBaoCaoSuaChua = document.getElementById("toggleTenKHBaoCaoSuaChua"); 
    const toggleNguoiTaoBaoCaoSuaChua = document.getElementById("toggleNguoiTaoBaoCaoSuaChua"); 
    const toggleNgayTaoBaoCaoSuaChua = document.getElementById("toggleNgayTaoBaoCaoSuaChua"); 

    const isTenthietbiBaoCaoSuaChuaVisible = localStorage.getItem("TenthietbiBaoCaoSuaChuaColumnVisible") === "true"; 
    const isSNBaoCaoSuaChuaVisible = localStorage.getItem("SNBaoCaoSuaChuaColumnVisible") === "true"; 
    const isHangBaoCaoSuaChuaVisible = localStorage.getItem("HangBaoCaoSuaChuaColumnVisible") === "true"; 
    const isModelBaoCaoSuaChuaVisible = localStorage.getItem("ModelBaoCaoSuaChuaColumnVisible") === "true"; 
    const isTenKHBaoCaoSuaChuaVisible = localStorage.getItem("TenKHBaoCaoSuaChuaColumnVisible") === "true"; 
    const isNguoiTaoBaoCaoSuaChuaVisible = localStorage.getItem("NguoiTaoBaoCaoSuaChuaColumnVisible") === "true"; 
    const isNgayTaoBaoCaoSuaChuaVisible = localStorage.getItem("NgayTaoBaoCaoSuaChuaColumnVisible") === "true"; 

    toggleTenthietbiBaoCaoSuaChua.checked = isTenthietbiBaoCaoSuaChuaVisible; 
    toggleSNBaoCaoSuaChua.checked = isSNBaoCaoSuaChuaVisible; 
    toggleHangBaoCaoSuaChua.checked = isHangBaoCaoSuaChuaVisible; 
    toggleModelBaoCaoSuaChua.checked = isModelBaoCaoSuaChuaVisible; 
    toggleTenKHBaoCaoSuaChua.checked = isTenKHBaoCaoSuaChuaVisible; 
    toggleNguoiTaoBaoCaoSuaChua.checked = isNguoiTaoBaoCaoSuaChuaVisible; 
    toggleNgayTaoBaoCaoSuaChua.checked = isNgayTaoBaoCaoSuaChuaVisible; 

    toggleTenthietbiBaoCaoSuaChua.dispatchEvent(new Event('change')); 
    toggleSNBaoCaoSuaChua.dispatchEvent(new Event('change')); 
    toggleHangBaoCaoSuaChua.dispatchEvent(new Event('change')); 
    toggleModelBaoCaoSuaChua.dispatchEvent(new Event('change')); 
    toggleTenKHBaoCaoSuaChua.dispatchEvent(new Event('change')); 
    toggleNguoiTaoBaoCaoSuaChua.dispatchEvent(new Event('change')); 
    toggleNgayTaoBaoCaoSuaChua.dispatchEvent(new Event('change')); 
};

document.getElementById("toggleTenthietbiBaoCaoSuaChua").addEventListener("change", function () {
    localStorage.setItem("TenthietbiBaoCaoSuaChuaColumnVisible", this.checked);
    var TenthietbiBaoCaoSuaChuaColumns = document.querySelectorAll(".Tenthietbi-column-BaoCaoSuaChua");
    TenthietbiBaoCaoSuaChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleSNBaoCaoSuaChua").addEventListener("change", function () {
    localStorage.setItem("SNBaoCaoSuaChuaColumnVisible", this.checked);
    var SNBaoCaoSuaChuaColumns = document.querySelectorAll(".SN-column-BaoCaoSuaChua");
    SNBaoCaoSuaChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleHangBaoCaoSuaChua").addEventListener("change", function () {
    localStorage.setItem("HangBaoCaoSuaChuaColumnVisible", this.checked);
    var HangBaoCaoSuaChuaColumns = document.querySelectorAll(".Hang-column-BaoCaoSuaChua");
    HangBaoCaoSuaChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleModelBaoCaoSuaChua").addEventListener("change", function () {
    localStorage.setItem("ModelBaoCaoSuaChuaColumnVisible", this.checked);
    var ModelBaoCaoSuaChuaColumns = document.querySelectorAll(".Model-column-BaoCaoSuaChua");
    ModelBaoCaoSuaChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleTenKHBaoCaoSuaChua").addEventListener("change", function () {
    localStorage.setItem("TenKHBaoCaoSuaChuaColumnVisible", this.checked);
    var TenKHBaoCaoSuaChuaColumns = document.querySelectorAll(".TenKH-column-BaoCaoSuaChua");
    TenKHBaoCaoSuaChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleNguoiTaoBaoCaoSuaChua").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoBaoCaoSuaChuaColumnVisible", this.checked);
    var NguoiTaoBaoCaoSuaChuaColumns = document.querySelectorAll(".NguoiTao-column-BaoCaoSuaChua");
    NguoiTaoBaoCaoSuaChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleNgayTaoBaoCaoSuaChua").addEventListener("change", function () {
    localStorage.setItem("NgayTaoBaoCaoSuaChuaColumnVisible", this.checked);
    var NgayTaoBaoCaoSuaChuaColumns = document.querySelectorAll(".NgayTao-column-BaoCaoSuaChua");
    NgayTaoBaoCaoSuaChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 