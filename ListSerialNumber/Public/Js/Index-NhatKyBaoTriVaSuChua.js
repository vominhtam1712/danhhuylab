window.onload = function () {
    const toggleImgNhatKyBaoTriVaSuChua = document.getElementById("toggleImgNhatKyBaoTriVaSuChua");
    const toggleTenThietBiNhatKyBaoTriVaSuChua = document.getElementById("toggleTenThietBiNhatKyBaoTriVaSuChua");
    const toggleHangSXNhatKyBaoTriVaSuChua = document.getElementById("toggleHangSXNhatKyBaoTriVaSuChua");
    const toggleNhatKyBaoTriNhatKyBaoTriVaSuChua = document.getElementById("toggleNhatKyBaoTriNhatKyBaoTriVaSuChua");
    const toggleNgayLapDatNhatKyBaoTriVaSuChua = document.getElementById("toggleNgayLapDatNhatKyBaoTriVaSuChua");

    const isImgNhatKyBaoTriVaSuChuaVisible = localStorage.getItem("ImgNhatKyBaoTriVaSuChuaColumnVisible") === "true";
    const isTenThietBiNhatKyBaoTriVaSuChuaVisible = localStorage.getItem("TenThietBiNhatKyBaoTriVaSuChuaColumnVisible") === "true";
    const isHangSXNhatKyBaoTriVaSuChuaVisible = localStorage.getItem("HangSXNhatKyBaoTriVaSuChuaColumnVisible") === "true";
    const isNhatKyBaoTriNhatKyBaoTriVaSuChuaVisible = localStorage.getItem("NhatKyBaoTriNhatKyBaoTriVaSuChuaColumnVisible") === "true";
    const isNgayLapDatNhatKyBaoTriVaSuChuaVisible = localStorage.getItem("NgayLapDatNhatKyBaoTriVaSuChuaColumnVisible") === "true";

    toggleImgNhatKyBaoTriVaSuChua.checked = isImgNhatKyBaoTriVaSuChuaVisible;
    toggleTenThietBiNhatKyBaoTriVaSuChua.checked = isTenThietBiNhatKyBaoTriVaSuChuaVisible;
    toggleHangSXNhatKyBaoTriVaSuChua.checked = isHangSXNhatKyBaoTriVaSuChuaVisible;
    toggleNhatKyBaoTriNhatKyBaoTriVaSuChua.checked = isNhatKyBaoTriNhatKyBaoTriVaSuChuaVisible;
    toggleNgayLapDatNhatKyBaoTriVaSuChua.checked = isNgayLapDatNhatKyBaoTriVaSuChuaVisible;

    toggleImgNhatKyBaoTriVaSuChua.dispatchEvent(new Event('change'));
    toggleTenThietBiNhatKyBaoTriVaSuChua.dispatchEvent(new Event('change'));
    toggleHangSXNhatKyBaoTriVaSuChua.dispatchEvent(new Event('change'));
    toggleNhatKyBaoTriNhatKyBaoTriVaSuChua.dispatchEvent(new Event('change'));
    toggleNgayLapDatNhatKyBaoTriVaSuChua.dispatchEvent(new Event('change'));
};

document.getElementById("toggleImgNhatKyBaoTriVaSuChua").addEventListener("change", function () {
    localStorage.setItem("ImgNhatKyBaoTriVaSuChuaColumnVisible", this.checked);
    var ImgNhatKyBaoTriVaSuChuaColumns = document.querySelectorAll(".Img-column-NhatKyBaoTriVaSuChua");
    ImgNhatKyBaoTriVaSuChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleTenThietBiNhatKyBaoTriVaSuChua").addEventListener("change", function () {
    localStorage.setItem("TenThietBiNhatKyBaoTriVaSuChuaColumnVisible", this.checked);
    var TenThietBiNhatKyBaoTriVaSuChuaColumns = document.querySelectorAll(".TenThietBi-column-NhatKyBaoTriVaSuChua");
    TenThietBiNhatKyBaoTriVaSuChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleHangSXNhatKyBaoTriVaSuChua").addEventListener("change", function () {
    localStorage.setItem("HangSXNhatKyBaoTriVaSuChuaColumnVisible", this.checked);
    var HangSXNhatKyBaoTriVaSuChuaColumns = document.querySelectorAll(".HangSX-column-NhatKyBaoTriVaSuChua");
    HangSXNhatKyBaoTriVaSuChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleNhatKyBaoTriNhatKyBaoTriVaSuChua").addEventListener("change", function () {
    localStorage.setItem("NhatKyBaoTriNhatKyBaoTriVaSuChuaColumnVisible", this.checked);
    var NhatKyBaoTriNhatKyBaoTriVaSuChuaColumns = document.querySelectorAll(".NhatKyBaoTri-column-NhatKyBaoTriVaSuChua");
    NhatKyBaoTriNhatKyBaoTriVaSuChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleNgayLapDatNhatKyBaoTriVaSuChua").addEventListener("change", function () {
    localStorage.setItem("NgayLapDatNhatKyBaoTriVaSuChuaColumnVisible", this.checked);
    var NgayLapDatNhatKyBaoTriVaSuChuaColumns = document.querySelectorAll(".NgayLapDat-column-NhatKyBaoTriVaSuChua");
    NgayLapDatNhatKyBaoTriVaSuChuaColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

 