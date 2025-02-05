window.onload = function () {
    const toggleTenthietbiPhieuyeucauhieuchuan = document.getElementById("toggleTenthietbiPhieuyeucauhieuchuan");
    const toggleSerialNumberPhieuyeucauhieuchuan = document.getElementById("toggleSerialNumberPhieuyeucauhieuchuan");
    const toggleModelPhieuyeucauhieuchuan = document.getElementById("toggleModelPhieuyeucauhieuchuan");
    const toggleTencogntyPhieuyeucauhieuchuan = document.getElementById("toggleTencogntyPhieuyeucauhieuchuan");
    const toggleNguoiTaoPhieuyeucauhieuchuan = document.getElementById("toggleNguoiTaoPhieuyeucauhieuchuan");
    const toggleNgayTaoPhieuyeucauhieuchuan = document.getElementById("toggleNgayTaoPhieuyeucauhieuchuan");

    const isTenthietbiPhieuyeucauhieuchuanVisible = localStorage.getItem("TenthietbiPhieuyeucauhieuchuanColumnVisible") === "true";
    const isSerialNumberPhieuyeucauhieuchuanVisible = localStorage.getItem("SerialNumberPhieuyeucauhieuchuanColumnVisible") === "true";
    const isModelPhieuyeucauhieuchuanVisible = localStorage.getItem("ModelPhieuyeucauhieuchuanColumnVisible") === "true";
    const isTencogntyPhieuyeucauhieuchuanVisible = localStorage.getItem("TencogntyPhieuyeucauhieuchuanColumnVisible") === "true";
    const isNguoiTaoPhieuyeucauhieuchuanVisible = localStorage.getItem("NguoiTaoPhieuyeucauhieuchuanColumnVisible") === "true";
    const isNgayTaoPhieuyeucauhieuchuanVisible = localStorage.getItem("NgayTaoPhieuyeucauhieuchuanColumnVisible") === "true";

    toggleTenthietbiPhieuyeucauhieuchuan.checked = isTenthietbiPhieuyeucauhieuchuanVisible;
    toggleSerialNumberPhieuyeucauhieuchuan.checked = isSerialNumberPhieuyeucauhieuchuanVisible;
    toggleModelPhieuyeucauhieuchuan.checked = isModelPhieuyeucauhieuchuanVisible;
    toggleTencogntyPhieuyeucauhieuchuan.checked = isTencogntyPhieuyeucauhieuchuanVisible;
    toggleNguoiTaoPhieuyeucauhieuchuan.checked = isNguoiTaoPhieuyeucauhieuchuanVisible;
    toggleNgayTaoPhieuyeucauhieuchuan.checked = isNgayTaoPhieuyeucauhieuchuanVisible;

    toggleTenthietbiPhieuyeucauhieuchuan.dispatchEvent(new Event('change'));
    toggleSerialNumberPhieuyeucauhieuchuan.dispatchEvent(new Event('change'));
    toggleModelPhieuyeucauhieuchuan.dispatchEvent(new Event('change'));
    toggleTencogntyPhieuyeucauhieuchuan.dispatchEvent(new Event('change'));
    toggleNguoiTaoPhieuyeucauhieuchuan.dispatchEvent(new Event('change'));
    toggleNgayTaoPhieuyeucauhieuchuan.dispatchEvent(new Event('change'));
};
document.getElementById("toggleTenthietbiPhieuyeucauhieuchuan").addEventListener("change", function () {
    localStorage.setItem("TenthietbiPhieuyeucauhieuchuanColumnVisible", this.checked);
    var TenthietbiPhieuyeucauhieuchuanColumns = document.querySelectorAll(".Tenthietbi-column-Phieuyeucauhieuchuan");
    TenthietbiPhieuyeucauhieuchuanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleSerialNumberPhieuyeucauhieuchuan").addEventListener("change", function () {
    localStorage.setItem("SerialNumberPhieuyeucauhieuchuanColumnVisible", this.checked);
    var SerialNumberPhieuyeucauhieuchuanColumns = document.querySelectorAll(".SerialNumber-column-Phieuyeucauhieuchuan");
    SerialNumberPhieuyeucauhieuchuanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleModelPhieuyeucauhieuchuan").addEventListener("change", function () {
    localStorage.setItem("ModelPhieuyeucauhieuchuanColumnVisible", this.checked);
    var ModelPhieuyeucauhieuchuanColumns = document.querySelectorAll(".Model-column-Phieuyeucauhieuchuan");
    ModelPhieuyeucauhieuchuanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNguoiTaoPhieuyeucauhieuchuan").addEventListener("change", function () {
    localStorage.setItem("NguoiTaoPhieuyeucauhieuchuanColumnVisible", this.checked);
    var NguoiTaoPhieuyeucauhieuchuanColumns = document.querySelectorAll(".Nguoitao-column-Phieuyeucauhieuchuan");
    NguoiTaoPhieuyeucauhieuchuanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleTencogntyPhieuyeucauhieuchuan").addEventListener("change", function () {
    localStorage.setItem("TencogntyPhieuyeucauhieuchuanColumnVisible", this.checked);
    var TencogntyPhieuyeucauhieuchuanColumns = document.querySelectorAll(".Tencognty-column-Phieuyeucauhieuchuan");
    TencogntyPhieuyeucauhieuchuanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleNgayTaoPhieuyeucauhieuchuan").addEventListener("change", function () {
    localStorage.setItem("NgayTaoPhieuyeucauhieuchuanColumnVisible", this.checked);
    var NgayTaoPhieuyeucauhieuchuanColumns = document.querySelectorAll(".Ngaytao-column-Phieuyeucauhieuchuan");
    NgayTaoPhieuyeucauhieuchuanColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 