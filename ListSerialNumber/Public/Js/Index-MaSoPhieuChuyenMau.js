window.onload = function () {
    const toggleNguoiTaoMaSoPhieuChuyenMau = document.getElementById("toggleNguoiTaoMaSoPhieuChuyenMau");
    const toggleNgayTaoMaSoPhieuChuyenMau = document.getElementById("toggleNgayTaoMaSoPhieuChuyenMau");

    const isNguoiTaoMaSoPhieuChuyenMauVisible = localStorage.getItem("NguoiTaoMaSoPhieuChuyenMauColumnVisible") === "true";
    const isNgayTaoMaSoPhieuChuyenMauVisible = localStorage.getItem("NgayTaoMaSoPhieuChuyenMauColumnVisible") === "true";

    toggleNguoiTaoMaSoPhieuChuyenMau.checked = isNguoiTaoMaSoPhieuChuyenMauVisible;
    toggleNgayTaoMaSoPhieuChuyenMau.checked = isNgayTaoMaSoPhieuChuyenMauVisible;

    toggleNguoiTaoMaSoPhieuChuyenMau.dispatchEvent(new Event('change'));
    toggleNgayTaoMaSoPhieuChuyenMau.dispatchEvent(new Event('change'));
};

document.getElementById("toggleNguoiTaoMaSoPhieuChuyenMau").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoMaSoPhieuChuyenMauColumnVisible", this.checked);
    var NguoiTaoMaSoPhieuChuyenMauColumns = document.querySelectorAll(".Nguoitao-column-MaSoPhieuChuyenMau");
    NguoiTaoMaSoPhieuChuyenMauColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleNgayTaoMaSoPhieuChuyenMau").addEventListener("change", function () {
    localStorage.setItem("NgayTaoMaSoPhieuChuyenMauColumnVisible", this.checked);
    var NgayTaoMaSoPhieuChuyenMauColumns = document.querySelectorAll(".Ngaytao-column-MaSoPhieuChuyenMau");
    NgayTaoMaSoPhieuChuyenMauColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 