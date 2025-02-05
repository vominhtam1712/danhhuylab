window.onload = function () {
    const toggleModel = document.getElementById("toggleModel");
    const toggleNhomNganh = document.getElementById("toggleNhomNganh");
    const toggleGhiChu = document.getElementById("toggleGhiChu");
    const toggleImg = document.getElementById("toggleImg");
    const toggleTenthietbi = document.getElementById("toggleTenthietbi");

    const isModelVisible = localStorage.getItem("modelColumnVisible") === "true";
    const isNhomNganhVisible = localStorage.getItem("nhomNganhColumnVisible") === "true";
    const isGhiChuVisible = localStorage.getItem("ghiChuColumnVisible") === "true";
    const isImgVisible = localStorage.getItem("ImgColumnVisible") === "true";
    const isTenthietbiVisible = localStorage.getItem("TenthietbiColumnVisible") === "true";

    toggleModel.checked = isModelVisible;
    toggleNhomNganh.checked = isNhomNganhVisible;
    toggleGhiChu.checked = isGhiChuVisible;
    toggleImg.checked = isImgVisible;
    toggleTenthietbi.checked = isTenthietbiVisible;

    toggleModel.dispatchEvent(new Event('change'));
    toggleNhomNganh.dispatchEvent(new Event('change'));
    toggleGhiChu.dispatchEvent(new Event('change'));
    toggleImg.dispatchEvent(new Event('change'));
    toggleTenthietbi.dispatchEvent(new Event('change'));
};

document.getElementById("toggleModel").addEventListener("change", function () {
    localStorage.setItem("modelColumnVisible", this.checked);
    var modelColumns = document.querySelectorAll(".model-column");
    modelColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleNhomNganh").addEventListener("change", function () {
    localStorage.setItem("nhomNganhColumnVisible", this.checked);
    var nhomNganhColumns = document.querySelectorAll(".nhomnganh-column");
    nhomNganhColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleGhiChu").addEventListener("change", function () {
    localStorage.setItem("ghiChuColumnVisible", this.checked);
    var ghiChuColumns = document.querySelectorAll(".ghichu-column");
    ghiChuColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});

document.getElementById("toggleImg").addEventListener("change", function () {
    localStorage.setItem("ImgColumnVisible", this.checked);
    var imgColumns = document.querySelectorAll(".img-column");
    imgColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
document.getElementById("toggleTenthietbi").addEventListener("change", function () {
    localStorage.setItem("TenthietbiColumnVisible", this.checked);
    var TenthietbiColumns = document.querySelectorAll(".Tenthietbi-column");
    TenthietbiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
});
