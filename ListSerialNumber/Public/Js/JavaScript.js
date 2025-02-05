document.getElementById('selectAll1').addEventListener('change', function () {
    var isChecked = this.checked;
    document.querySelectorAll('input[name="selectedIds"]').forEach(function (checkbox) {
        checkbox.checked = isChecked;
    });
});
document.getElementById("openAddModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("Create", "ChuanSuDung", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("modalContent").innerHTML = reponse;
            document.getElementById("addModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAddModal() {
    document.getElementById("addModal").style.display = "none";
}
document.getElementById("openAddDatebeforeModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("ThemNgay", "GiaTriTruocHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("datebeforemodalContent").innerHTML = reponse;
            document.getElementById("adddatebeforeModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closedatebeforeModal() {
    document.getElementById("adddatebeforeModal").style.display = "none";
}
document.getElementById("openEditDatebeforeModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("EditNgay", "GiaTriTruocHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("editdatebeforemodalContent").innerHTML = reponse;
            document.getElementById("editdatebeforeModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeEditdatebeforeModal() {
    document.getElementById("editdatebeforeModal").style.display = "none";
}
document.getElementById("openDeleteDatebeforeModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("XoaNgay", "GiaTriTruocHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("deletedatebeforemodalContent").innerHTML = reponse;
            document.getElementById("deletedatebeforeModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeDeletedatebeforeModal() {
    document.getElementById("deletedatebeforeModal").style.display = "none";
}
document.getElementById("openAdd1kenhbeforeModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("Them1kenh", "GiaTriTruocHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("add1kenhbeforemodalContent").innerHTML = reponse;
            document.getElementById("add1kenhbeforeModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeadd1kenhbeforeModal() {
    document.getElementById("add1kenhbeforeModal").style.display = "none";
}
document.getElementById("openAdd4kenhbeforeModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("Them4kenh", "GiaTriTruocHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("add4kenhbeforemodalContent").innerHTML = reponse;
            document.getElementById("add4kenhbeforeModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeadd4kenhbeforeModal() {
    document.getElementById("add4kenhbeforeModal").style.display = "none";
}
document.getElementById("openAddExcelbeforeModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("ImportExcel", "GiaTriTruocHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("AddExcelbeforemodalContent").innerHTML = reponse;
            document.getElementById("AddExcelbeforeModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAddExcelbeforeModal() {
    document.getElementById("AddExcelbeforeModal").style.display = "none";
}
document.getElementById("openAddDateAffterModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("ThemNgay", "KetQuaHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("dateAffermodalContent").innerHTML = reponse;
            document.getElementById("adddateAfferModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAdddateAffterbeforeModal() {
    document.getElementById("adddateAfferModal").style.display = "none";
}
document.getElementById("openEditDateAffterModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("EditNgay", "KetQuaHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("EditdateAffermodalContent").innerHTML = reponse;
            document.getElementById("EditdateAfferModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeEditdateAffterModal() {
    document.getElementById("EditdateAfferModal").style.display = "none";
}
document.getElementById("openDeleteDateAffterModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("XoaNgay", "KetQuaHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("DeletedateAffermodalContent").innerHTML = reponse;
            document.getElementById("DeletedateAfferModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeDeletedateAffterModal() {
    document.getElementById("DeletedateAfferModal").style.display = "none";
}
document.getElementById("openAdd1kenhAffterModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("Them1kenh", "KetQuaHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("add1kenhAffermodalContent").innerHTML = reponse;
            document.getElementById("add1kenhAfferModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAdd1KenhAffterModal() {
    document.getElementById("add1kenhAfferModal").style.display = "none";
}
document.getElementById("openAdd4kenhAffterModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("Them4kenh", "KetQuaHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("add4kenhAffermodalContent").innerHTML = reponse;
            document.getElementById("add4kenhAfferModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAdd4KenhAffterModal() {
    document.getElementById("add4kenhAfferModal").style.display = "none";
}
document.getElementById("openAddExcelAffterModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("ImportExcel", "KetQuaHieuChinh", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("AddExcelAffermodalContent").innerHTML = reponse;
            document.getElementById("AddExcelAfferModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAddExcelAffterModal() {
    document.getElementById("AddExcelAfferModal").style.display = "none";
}
document.getElementById("openAddGhiChuModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("ThemGhiChu", "PhanCongGhiChu", new { id = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("addGhiChumodalContent").innerHTML = reponse;
            document.getElementById("addGhiChuModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAddGhiChuModal() {
    document.getElementById("addGhiChuModal").style.display = "none";
}
document.querySelector('.editGhuChuButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds5 = [];
    document.querySelectorAll('input[name="selectedIds5"]:checked').forEach(function (checkbox) {
        selectedIds5.push(checkbox.value);
    });

    if (selectedIds5.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để sửa.");
        return;
    }

    var url = '@Url.Action("Chinhsua", "PhanCongGhiChu", new { id = "__ID__" })';
    url = url.replace("__ID__", selectedIds5.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".EditGhiChumodalContent").html(response);
            $(".EditGhiChuModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeEditGhiChuModal() {
    $(".EditGhiChuModal").hide();
}
document.querySelector('.deleteGhuChuButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds5 = [];
    document.querySelectorAll('input[name="selectedIds5"]:checked').forEach(function (checkbox) {
        selectedIds5.push(checkbox.value);
    });

    if (selectedIds5.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để xóa.");
        return;
    }

    var url = '@Url.Action("Xoa", "PhanCongGhiChu", new { id = "__ID__" })';
    url = url.replace("__ID__", selectedIds5.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".DeleteGhiChumodalContent").html(response);
            $(".DeleteGhiChuModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeDeleteGhiChuModal() {
    $(".DeleteGhiChuModal").hide();
}
document.getElementById("openAddBienBanModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("Taobienbanhieuchuan", "BienBanHieuChuan", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("addBienBanmodalContent").innerHTML = reponse;
            document.getElementById("addBienBanModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAddBienBanModal() {
    document.getElementById("addBienBanModal").style.display = "none";
}
document.querySelector('.editBienHanHieuChuanButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds6 = [];
    document.querySelectorAll('input[name="selectedIds6"]:checked').forEach(function (checkbox) {
        selectedIds6.push(checkbox.value);
    });

    if (selectedIds6.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để sửa.");
        return;
    }

    var url = '@Url.Action("Edit", "BienBanHieuChuan", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds6.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".EditBienBanmodalContent").html(response);
            $(".EditBienBanModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeEditBienBanModal() {
    $(".EditBienBanModal").hide();
}
document.querySelector('.deleteBienBanHieuChuanButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds6 = [];
    document.querySelectorAll('input[name="selectedIds6"]:checked').forEach(function (checkbox) {
        selectedIds6.push(checkbox.value);
    });

    if (selectedIds6.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để xóa.");
        return;
    }
    var url = '@Url.Action("Delete", "BienBanHieuChuan", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds6.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".DeletBienBanmodalContent").html(response);
            $(".DeletBienBanModal").css("display", "block");
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeDeleteBienBanModal() {
    $(".DeletBienBanModal").css("display", "none");
}
document.getElementById("openAddSTDModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("Tao_STD_DUT", "STD_DUT", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("addSDTmodalContent").innerHTML = reponse;
            document.getElementById("addSDTModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeaddSDTModal() {
    document.getElementById("addSDTModal").style.display = "none";
}
document.getElementById("openAddExcelSTDModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("ImportExcel", "STD_DUT", new { ids = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("addExcelSTDmodalContent").innerHTML = reponse;
            document.getElementById("addExcelSTDModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeExcelSTDModal() {
    document.getElementById("addExcelSTDModal").style.display = "none";
}
document.getElementById("openAddGCNModalBtn").addEventListener('click', function () {
    var id = $(this).data("id");
    $.ajax({
        url: '@Url.Action("Tao_giaychungnhan", "Giaychungnhan", new { id = "__ID__" })'.replace("__ID__", id),
        type: 'GET',
        success: function (reponse) {
            document.getElementById("addGCNmodalContent").innerHTML = reponse;
            document.getElementById("addGCNModal").style.display = "block";
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeAddGCNModal() {
    document.getElementById("addGCNModal").style.display = "none";
}
document.querySelector('.editButtonChuanSuDung').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds = [];
    document.querySelectorAll('input[name="selectedIds"]:checked').forEach(function (checkbox) {
        selectedIds.push(checkbox.value);
    });

    if (selectedIds.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để sửa.");
        return;
    }

    var url = '@Url.Action("Edit", "ChuanSuDung", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".editmodalChuanSuDungContent").html(response);
            $(".editChuanSuDungModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeEditChuanSuDungModal() {
    $(".editChuanSuDungModal").hide();
}
document.querySelector('.deleteButtonChuanSuDung').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds = [];
    document.querySelectorAll('input[name="selectedIds"]:checked').forEach(function (checkbox) {
        selectedIds.push(checkbox.value);
    });

    if (selectedIds.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để xóa.");
        return;
    }
    var url = '@Url.Action("Delete", "ChuanSuDung", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".deleteChuanSuDungmodalContent").html(response);
            $(".deleteChuanSuDungModal").css("display", "block");
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeDeleteChuanSuDungModal() {
    $(".deleteChuanSuDungModal").css("display", "none");
}
document.querySelector('.editButtonGiaTriTruoc').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds2 = [];
    document.querySelectorAll('input[name="selectedIds2"]:checked').forEach(function (checkbox) {
        selectedIds2.push(checkbox.value);
    });

    if (selectedIds2.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để sửa.");
        return;
    }

    var url = '@Url.Action("Edit", "GiaTriTruocHieuChinh", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds2.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".editGiaTriTruocmodalContent").html(response);
            $(".editGiaTriTruocModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeEditGiaTriTruocModal() {
    $(".editGiaTriTruocModal").hide();
}
document.querySelector('.deleteButtonGiaTriTruoc').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds2 = [];
    document.querySelectorAll('input[name="selectedIds2"]:checked').forEach(function (checkbox) {
        selectedIds2.push(checkbox.value);
    });

    if (selectedIds2.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để xóa.");
        return;
    }
    var url = '@Url.Action("Delete", "GiaTriTruocHieuChinh", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds2.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".deleteGiaTriTruocmodalContent").html(response);
            $(".deleteGiaTriTruocModal").css("display", "block");
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeDeleteGiaTriTruocModal() {
    $(".deleteGiaTriTruocModal").css("display", "none");
}
document.querySelector('.editSTDButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds7 = [];
    document.querySelectorAll('input[name="selectedIds7"]:checked').forEach(function (checkbox) {
        selectedIds7.push(checkbox.value);
    });

    if (selectedIds7.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để sửa.");
        return;
    }

    var url = '@Url.Action("ChinhSua", "STD_DUT", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds7.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".editSTDmodalContent").html(response);
            $(".editSTDModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeEditSTDModal() {
    $(".editSTDModal").hide();
}
document.querySelector('.deleteSTDButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds7 = [];
    document.querySelectorAll('input[name="selectedIds7"]:checked').forEach(function (checkbox) {
        selectedIds7.push(checkbox.value);
    });

    if (selectedIds7.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để xóa.");
        return;
    }
    var url = '@Url.Action("Delete", "STD_DUT", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds7.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".deleteSTDmodalContent").html(response);
            $(".deleteSTDModal").css("display", "block");
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeDeleteSTDModal() {
    $(".deleteSTDModal").css("display", "none");
}
document.querySelector('.editKetQuaHieuChinhButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds4 = [];
    document.querySelectorAll('input[name="selectedIds4"]:checked').forEach(function (checkbox) {
        selectedIds4.push(checkbox.value);
    });

    if (selectedIds4.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để sửa.");
        return;
    }

    var url = '@Url.Action("Edit", "KetQuaHieuChinh", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds4.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".editKetQuaHieuChinhmodalContent").html(response);
            $(".editKetQuaHieuChinhModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeEditKetQuaHieuChinhModal() {
    $(".editKetQuaHieuChinhModal").hide();
}
document.querySelector('.deleteKetQuaHieuChinhButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds4 = [];
    document.querySelectorAll('input[name="selectedIds4"]:checked').forEach(function (checkbox) {
        selectedIds4.push(checkbox.value);
    });

    if (selectedIds4.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để xóa.");
        return;
    }
    var url = '@Url.Action("Delete", "KetQuaHieuChinh", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds4.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".deleteKetQuaHieuChinhmodalContent").html(response);
            $(".deleteKetQuaHieuChinhModal").css("display", "block");
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeDeleteKetQuaHieuChinhModal() {
    $(".deleteKetQuaHieuChinhModal").css("display", "none");
}
document.querySelector('.addDKDBDButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds7 = [];
    document.querySelectorAll('input[name="selectedIds7"]:checked').forEach(function (checkbox) {
        selectedIds7.push(checkbox.value);
    });

    if (selectedIds7.length === 1 || selectedIds7.length === 0) {
        alert("Vui lòng chọn ít nhất 2 mục để tạo độ không đảm bảo đo.");
        return;
    }

    var url = '@Url.Action("Tao_TB_STD", "TB_STD", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds7.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".addDKDBDmodalContent").html(response);
            $(".addDKDBDModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closeaddDKDBDModal() {
    $(".addDKDBDModal").hide();
}
document.querySelector('.deleteDKDBDButton').addEventListener("click", function (event) {
    event.preventDefault();

    var selectedIds8 = [];
    document.querySelectorAll('input[name="selectedIds8"]:checked').forEach(function (checkbox) {
        selectedIds8.push(checkbox.value);
    });

    if (selectedIds8.length === 0) {
        alert("Vui lòng chọn ít nhất một mục để xóa.");
        return;
    }

    var url = '@Url.Action("XoaDKDBD", "BanTinhDKDBD", new { ids = "__ID__" })';
    url = url.replace("__ID__", selectedIds8.join(','));

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            $(".deleteDKDBDmodalContent").html(response);
            $(".deleteDKDBDModal").show();
        },
        error: function () {
            alert('Có lỗi xảy ra khi tải dữ liệu!');
        }
    });
});
function closedeletDKDBDModal() {
    $(".deleteDKDBDModal").hide();
}
document.getElementById('selectAll2').addEventListener('change', function () {
    var isChecked = this.checked;
    document.querySelectorAll('input[name="selectedIds2"]').forEach(function (checkbox) {
        checkbox.checked = isChecked;
    });
});
document.getElementById('selectAll4').addEventListener('change', function () {
    var isChecked = this.checked;
    document.querySelectorAll('input[name="selectedIds4"]').forEach(function (checkbox) {
        checkbox.checked = isChecked;
    });
});
document.getElementById('selectAll5').addEventListener('change', function () {
    var isChecked = this.checked;
    document.querySelectorAll('input[name="selectedIds5"]').forEach(function (checkbox) {
        checkbox.checked = isChecked;
    });
});
document.getElementById('selectAll6').addEventListener('change', function () {
    var isChecked = this.checked;
    document.querySelectorAll('input[name="selectedIds6"]').forEach(function (checkbox) {
        checkbox.checked = isChecked;
    });
});
document.getElementById('selectAll7').addEventListener('change', function () {
    var isChecked = this.checked;
    document.querySelectorAll('input[name="selectedIds7"]').forEach(function (checkbox) {
        checkbox.checked = isChecked;
    });
});
document.getElementById('selectAll8').addEventListener('change', function () {
    var isChecked = this.checked;
    document.querySelectorAll('input[name="selectedIds8"]').forEach(function (checkbox) {
        checkbox.checked = isChecked;
    });
});