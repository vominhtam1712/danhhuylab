window.onload = function () {
    const toggleSerialNumberPhieuyeucauhieuchuanDoi = document.getElementById("toggleSerialNumberPhieuyeucauhieuchuanDoi"); 
    const toggleTenThietBiPhieuyeucauhieuchuanDoi = document.getElementById("toggleTenThietBiPhieuyeucauhieuchuanDoi"); 
    const toggleHangPhieuyeucauhieuchuanDoi = document.getElementById("toggleHangPhieuyeucauhieuchuanDoi"); 
    const toggleModelPhieuyeucauhieuchuanDoi = document.getElementById("toggleModelPhieuyeucauhieuchuanDoi"); 

    const isSerialNumberPhieuyeucauhieuchuanDoiVisible = localStorage.getItem("SerialNumberPhieuyeucauhieuchuanDoiColumnVisible") === "true"; 
    const isTenThietBiPhieuyeucauhieuchuanDoiVisible = localStorage.getItem("TenThietBiPhieuyeucauhieuchuanDoiColumnVisible") === "true"; 
    const isHangPhieuyeucauhieuchuanDoiVisible = localStorage.getItem("HangPhieuyeucauhieuchuanDoiColumnVisible") === "true"; 
    const isModelPhieuyeucauhieuchuanDoiVisible = localStorage.getItem("ModelPhieuyeucauhieuchuanDoiColumnVisible") === "true"; 

    toggleSerialNumberPhieuyeucauhieuchuanDoi.checked = isSerialNumberPhieuyeucauhieuchuanDoiVisible; 
    toggleTenThietBiPhieuyeucauhieuchuanDoi.checked = isTenThietBiPhieuyeucauhieuchuanDoiVisible; 
    toggleHangPhieuyeucauhieuchuanDoi.checked = isHangPhieuyeucauhieuchuanDoiVisible; 
    toggleModelPhieuyeucauhieuchuanDoi.checked = isModelPhieuyeucauhieuchuanDoiVisible; 

    toggleSerialNumberPhieuyeucauhieuchuanDoi.dispatchEvent(new Event('change')); 
    toggleTenThietBiPhieuyeucauhieuchuanDoi.dispatchEvent(new Event('change')); 
    toggleHangPhieuyeucauhieuchuanDoi.dispatchEvent(new Event('change')); 
    toggleModelPhieuyeucauhieuchuanDoi.dispatchEvent(new Event('change')); 
};

document.getElementById("toggleSerialNumberPhieuyeucauhieuchuanDoi").addEventListener("change", function () {
    localStorage.setItem("SerialNumberPhieuyeucauhieuchuanDoiColumnVisible", this.checked);
    var SerialNumberPhieuyeucauhieuchuanDoiColumns = document.querySelectorAll(".SerialNumber-column-PhieuyeucauhieuchuanDoi");
    SerialNumberPhieuyeucauhieuchuanDoiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleTenThietBiPhieuyeucauhieuchuanDoi").addEventListener("change", function () {
    localStorage.setItem("TenThietBiPhieuyeucauhieuchuanDoiColumnVisible", this.checked);
    var TenThietBiPhieuyeucauhieuchuanDoiColumns = document.querySelectorAll(".TenThietBi-column-PhieuyeucauhieuchuanDoi");
    TenThietBiPhieuyeucauhieuchuanDoiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleHangPhieuyeucauhieuchuanDoi").addEventListener("change", function () {
    localStorage.setItem("HangPhieuyeucauhieuchuanDoiColumnVisible", this.checked);
    var HangPhieuyeucauhieuchuanDoiColumns = document.querySelectorAll(".Hang-column-PhieuyeucauhieuchuanDoi");
    HangPhieuyeucauhieuchuanDoiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 
document.getElementById("toggleModelPhieuyeucauhieuchuanDoi").addEventListener("change", function () {
    localStorage.setItem("ModelPhieuyeucauhieuchuanDoiColumnVisible", this.checked);
    var ModelPhieuyeucauhieuchuanDoiColumns = document.querySelectorAll(".Model-column-PhieuyeucauhieuchuanDoi");
    ModelPhieuyeucauhieuchuanDoiColumns.forEach(function (col) {
        col.style.display = this.checked ? "" : "none";
    }, this);
}); 