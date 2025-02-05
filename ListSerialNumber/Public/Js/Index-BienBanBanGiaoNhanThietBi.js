window.onload = function () {
    const toggleNguoiTao = document.getElementById("toggleNguoiTao");
    const toggleNgayTao = document.getElementById("toggleNgayTao"); 

    const isNguoiTaoVisible = localStorage.getItem("NguoiTaoColumnVisible") === "true";
    const isNgayTaoVisible = localStorage.getItem("NgayTaoColumnVisible") === "true"; 

    toggleNguoiTao.checked = isNguoiTaoVisible;
    toggleNgayTao.checked = isNgayTaoVisible; 

    toggleNguoiTao.dispatchEvent(new Event('change'));
    toggleNgayTao.dispatchEvent(new Event('change')); 
};

document.getElementById("toggleNguoiTao").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoColumnVisible", this.checked);
    var NguoiTaoColumns = document.querySelectorAll(".Nguoitao-column");
    NguoiTaoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleNgayTao").addEventListener("change", function () {
    localStorage.setItem("NgayTaoColumnVisible", this.checked);
    var NgayTaoColumns = document.querySelectorAll(".Ngaytao-column");
    NgayTaoColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 