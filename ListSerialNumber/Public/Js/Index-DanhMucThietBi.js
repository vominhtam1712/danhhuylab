window.onload = function () {
    const toggleImgDanhMucThietBi = document.getElementById("toggleImgDanhMucThietBi"); 
    const toggleTenThietBiDanhMucThietBi = document.getElementById("toggleTenThietBiDanhMucThietBi"); 
    const toggleHangSXDanhMucThietBi = document.getElementById("toggleHangSXDanhMucThietBi"); 
    const toggleNgayDuaVaoSuDungDanhMucThietBi = document.getElementById("toggleNgayDuaVaoSuDungDanhMucThietBi"); 

    const isImgDanhMucThietBiVisible = localStorage.getItem("ImgDanhMucThietBiColumnVisible") === "true"; 
    const isTenThietBiDanhMucThietBiVisible = localStorage.getItem("TenThietBiDanhMucThietBiColumnVisible") === "true"; 
    const isHangSXDanhMucThietBiVisible = localStorage.getItem("HangSXDanhMucThietBiColumnVisible") === "true"; 
    const isNgayDuaVaoSuDungDanhMucThietBiVisible = localStorage.getItem("NgayDuaVaoSuDungDanhMucThietBiColumnVisible") === "true"; 

    toggleImgDanhMucThietBi.checked = isImgDanhMucThietBiVisible; 
    toggleTenThietBiDanhMucThietBi.checked = isTenThietBiDanhMucThietBiVisible; 
    toggleHangSXDanhMucThietBi.checked = isHangSXDanhMucThietBiVisible; 
    toggleNgayDuaVaoSuDungDanhMucThietBi.checked = isNgayDuaVaoSuDungDanhMucThietBiVisible; 

    toggleImgDanhMucThietBi.dispatchEvent(new Event('change')); 
    toggleTenThietBiDanhMucThietBi.dispatchEvent(new Event('change')); 
    toggleHangSXDanhMucThietBi.dispatchEvent(new Event('change')); 
    toggleNgayDuaVaoSuDungDanhMucThietBi.dispatchEvent(new Event('change')); 
};

document.getElementById("toggleImgDanhMucThietBi").addEventListener("change", function () {
    localStorage.setItem("ImgDanhMucThietBiColumnVisible", this.checked);
    var ImgDanhMucThietBiColumns = document.querySelectorAll(".Img-column-DanhMucThietBi");
    ImgDanhMucThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleTenThietBiDanhMucThietBi").addEventListener("change", function () {
    localStorage.setItem("TenThietBiDanhMucThietBiColumnVisible", this.checked);
    var TenThietBiDanhMucThietBiColumns = document.querySelectorAll(".TenThietBi-column-DanhMucThietBi");
    TenThietBiDanhMucThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleHangSXDanhMucThietBi").addEventListener("change", function () {
    localStorage.setItem("HangSXDanhMucThietBiColumnVisible", this.checked);
    var HangSXDanhMucThietBiColumns = document.querySelectorAll(".HangSX-column-DanhMucThietBi");
    HangSXDanhMucThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleNgayDuaVaoSuDungDanhMucThietBi").addEventListener("change", function () {
    localStorage.setItem("NgayDuaVaoSuDungDanhMucThietBiColumnVisible", this.checked);
    var NgayDuaVaoSuDungDanhMucThietBiColumns = document.querySelectorAll(".NgayDuaVaoSuDung-column-DanhMucThietBi");
    NgayDuaVaoSuDungDanhMucThietBiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 