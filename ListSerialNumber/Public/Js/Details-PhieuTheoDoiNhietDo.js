window.onload = function () {
    const togglePhongPhieuTheoDoiNhietDo = document.getElementById("togglePhongPhieuTheoDoiNhietDo"); 
    const toggleThangPhieuTheoDoiNhietDo = document.getElementById("toggleThangPhieuTheoDoiNhietDo"); 
    const toggleNamPhieuTheoDoiNhietDo = document.getElementById("toggleNamPhieuTheoDoiNhietDo"); 
    const toggleNhietDoPhieuTheoDoiNhietDo = document.getElementById("toggleNhietDoPhieuTheoDoiNhietDo"); 
    const toggleDoAmPhieuTheoDoiNhietDo = document.getElementById("toggleDoAmPhieuTheoDoiNhietDo"); 
    const toggleNgayTaoPhieuTheoDoiNhietDo = document.getElementById("toggleNgayTaoPhieuTheoDoiNhietDo"); 
    const toggleNguoiTaoPhieuTheoDoiNhietDo = document.getElementById("toggleNguoiTaoPhieuTheoDoiNhietDo"); 

    const isPhongPhieuTheoDoiNhietDoVisible = localStorage.getItem("PhongPhieuTheoDoiNhietDoColumnVisible") === "true"; 
    const isThangPhieuTheoDoiNhietDoVisible = localStorage.getItem("ThangPhieuTheoDoiNhietDoColumnVisible") === "true"; 
    const isNamPhieuTheoDoiNhietDoVisible = localStorage.getItem("NamPhieuTheoDoiNhietDoColumnVisible") === "true"; 
    const isNhietDoPhieuTheoDoiNhietDoVisible = localStorage.getItem("NhietDoPhieuTheoDoiNhietDoColumnVisible") === "true"; 
    const isDoAmPhieuTheoDoiNhietDoVisible = localStorage.getItem("DoAmPhieuTheoDoiNhietDoColumnVisible") === "true"; 
    const isNgayTaoPhieuTheoDoiNhietDoVisible = localStorage.getItem("NgayTaoPhieuTheoDoiNhietDoColumnVisible") === "true"; 
    const isNguoiTaoPhieuTheoDoiNhietDoVisible = localStorage.getItem("NguoiTaoPhieuTheoDoiNhietDoColumnVisible") === "true"; 

    togglePhongPhieuTheoDoiNhietDo.checked = isPhongPhieuTheoDoiNhietDoVisible; 
    toggleThangPhieuTheoDoiNhietDo.checked = isThangPhieuTheoDoiNhietDoVisible; 
    toggleNamPhieuTheoDoiNhietDo.checked = isNamPhieuTheoDoiNhietDoVisible; 
    toggleNhietDoPhieuTheoDoiNhietDo.checked = isNhietDoPhieuTheoDoiNhietDoVisible; 
    toggleDoAmPhieuTheoDoiNhietDo.checked = isDoAmPhieuTheoDoiNhietDoVisible; 
    toggleNgayTaoPhieuTheoDoiNhietDo.checked = isNgayTaoPhieuTheoDoiNhietDoVisible; 
    toggleNguoiTaoPhieuTheoDoiNhietDo.checked = isNguoiTaoPhieuTheoDoiNhietDoVisible; 

    togglePhongPhieuTheoDoiNhietDo.dispatchEvent(new Event('change')); 
    toggleThangPhieuTheoDoiNhietDo.dispatchEvent(new Event('change')); 
    toggleNamPhieuTheoDoiNhietDo.dispatchEvent(new Event('change')); 
    toggleNhietDoPhieuTheoDoiNhietDo.dispatchEvent(new Event('change')); 
    toggleDoAmPhieuTheoDoiNhietDo.dispatchEvent(new Event('change')); 
    toggleNgayTaoPhieuTheoDoiNhietDo.dispatchEvent(new Event('change')); 
    toggleNguoiTaoPhieuTheoDoiNhietDo.dispatchEvent(new Event('change')); 
};

document.getElementById("togglePhongPhieuTheoDoiNhietDo").addEventListener("change", function () {
    localStorage.setItem("PhongPhieuTheoDoiNhietDoColumnVisible", this.checked);
    var PhongPhieuTheoDoiNhietDoColumns = document.querySelectorAll(".Phong-column-PhieuTheoDoiNhietDo");
    PhongPhieuTheoDoiNhietDoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleThangPhieuTheoDoiNhietDo").addEventListener("change", function () {
    localStorage.setItem("ThangPhieuTheoDoiNhietDoColumnVisible", this.checked);
    var ThangPhieuTheoDoiNhietDoColumns = document.querySelectorAll(".Thang-column-PhieuTheoDoiNhietDo");
    ThangPhieuTheoDoiNhietDoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNamPhieuTheoDoiNhietDo").addEventListener("change", function () {
    localStorage.setItem("NamPhieuTheoDoiNhietDoColumnVisible", this.checked);
    var NamPhieuTheoDoiNhietDoColumns = document.querySelectorAll(".Nam-column-PhieuTheoDoiNhietDo");
    NamPhieuTheoDoiNhietDoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNhietDoPhieuTheoDoiNhietDo").addEventListener("change", function () {
    localStorage.setItem("NhietDoPhieuTheoDoiNhietDoColumnVisible", this.checked);
    var NhietDoPhieuTheoDoiNhietDoColumns = document.querySelectorAll(".NhietDo-column-PhieuTheoDoiNhietDo");
    NhietDoPhieuTheoDoiNhietDoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleDoAmPhieuTheoDoiNhietDo").addEventListener("change", function () {
    localStorage.setItem("DoAmPhieuTheoDoiNhietDoColumnVisible", this.checked);
    var DoAmPhieuTheoDoiNhietDoColumns = document.querySelectorAll(".DoAm-column-PhieuTheoDoiNhietDo");
    DoAmPhieuTheoDoiNhietDoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNgayTaoPhieuTheoDoiNhietDo").addEventListener("change", function () {
    localStorage.setItem("NgayTaoPhieuTheoDoiNhietDoColumnVisible", this.checked);
    var NgayTaoPhieuTheoDoiNhietDoColumns = document.querySelectorAll(".NgayTao-column-PhieuTheoDoiNhietDo");
    NgayTaoPhieuTheoDoiNhietDoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNguoiTaoPhieuTheoDoiNhietDo").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoPhieuTheoDoiNhietDoColumnVisible", this.checked);
    var NguoiTaoPhieuTheoDoiNhietDoColumns = document.querySelectorAll(".NguoiTao-column-PhieuTheoDoiNhietDo");
    NguoiTaoPhieuTheoDoiNhietDoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});