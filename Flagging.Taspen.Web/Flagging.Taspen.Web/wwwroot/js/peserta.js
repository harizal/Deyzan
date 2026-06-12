async function loadKota(provinsiId, selectedKota) {
    if (!provinsiId) {
        $('#kota').empty().append('<option value="">-- Pilih Kota --</option>');
        $('#kecamatan').empty().append('<option value="">-- Pilih Kecamatan --</option>');
        $('#kelurahan').empty().append('<option value="">-- Pilih Kelurahan --</option>');
        return;
    }

    const res = await fetch(window.pesertaUrls.getKota + '?provinsiId=' + provinsiId);
    const data = await res.json();
    $('#kota').empty().append('<option value="">-- Pilih Kota --</option>');
    $('#kecamatan').empty().append('<option value="">-- Pilih Kecamatan --</option>');
    $('#kelurahan').empty().append('<option value="">-- Pilih Kelurahan --</option>');
    data.forEach(function (item) {
        $('#kota').append(`<option value="${item.id}">${item.name}</option>`);
    });
    if (selectedKota) {
        $('#kota').val(selectedKota);
    }
}

async function loadKecamatan(kotaId, selectedKecamatan) {
    if (!kotaId) {
        $('#kecamatan').empty().append('<option value="">-- Pilih Kecamatan --</option>');
        $('#kelurahan').empty().append('<option value="">-- Pilih Kelurahan --</option>');
        return;
    }

    const res = await fetch(window.pesertaUrls.getKecamatan + '?kotaId=' + kotaId);
    const data = await res.json();
    $('#kecamatan').empty().append('<option value="">-- Pilih Kecamatan --</option>');
    $('#kelurahan').empty().append('<option value="">-- Pilih Kelurahan --</option>');
    data.forEach(function (item) {
        $('#kecamatan').append(`<option value="${item.id}">${item.name}</option>`);
    });
    if (selectedKecamatan) {
        $('#kecamatan').val(selectedKecamatan);
    }
}

async function loadKelurahan(kecamatanId, selectedKelurahan) {
    if (!kecamatanId) {
        $('#kelurahan').empty().append('<option value="">-- Pilih Kelurahan --</option>');
        return;
    }

    const res = await fetch(window.pesertaUrls.getKelurahan + '?kecamatanId=' + kecamatanId);
    const data = await res.json();
    $('#kelurahan').empty().append('<option value="">-- Pilih Kelurahan --</option>');
    data.forEach(function (item) {
        $('#kelurahan').append(`<option value="${item.id}">${item.name}</option>`);
    });
    if (selectedKelurahan) {
        $('#kelurahan').val(selectedKelurahan);
    }
}

$(function () {
    $('#provinsi').on('change', function () {
        loadKota($(this).val());
    });

    $('#kota').on('change', function () {
        loadKecamatan($(this).val());
    });

    $('#kecamatan').on('change', function () {
        loadKelurahan($(this).val());
    });

    var provVal = $('#provinsi').val();
    var kotaVal = $('#kota').val();
    var kecVal = $('#kecamatan').val();
    var kelVal = $('#kelurahan').val();

    if (provVal) {
        loadKota(provVal, kotaVal).then(function () {
            if (kotaVal) {
                loadKecamatan(kotaVal, kecVal).then(function () {
                    if (kecVal) {
                        loadKelurahan(kecVal, kelVal);
                    }
                });
            }
        });
    }

    $('#pesertaForm').on('submit', function (e) {
        e.preventDefault();

        const form = $(this);
        const actionUrl = form.attr('action');

        LoadingButton(true, 'btnSubmit', 'Simpan');

        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: new FormData(this),
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    ShowAlert('success', 'Success!', response.message + '. Redirecting...');
                    setTimeout(function () {
                        window.location.href = response.redirectUrl;
                    }, 1500);
                } else {
                    ShowAlert('danger', 'Failed!', response.message);
                    LoadingButton(false, 'btnSubmit', 'Simpan');
                }
            },
            error: function () {
                ShowAlert('danger', 'Failed!', 'Terjadi kesalahan saat menyimpan data.');
                LoadingButton(false, 'btnSubmit', 'Simpan');
            }
        });
    });
});
