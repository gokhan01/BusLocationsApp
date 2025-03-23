$(document).ready(function () {
    $('.datepicker').datepicker({
        language: 'tr-TR',
        startDate: '1d',
    });

    // Sayfa yüklendiğinde tüm ürün listesini çek
    $.ajax({
        url: 'BusLocations/Get',
        type: 'GET',
        success: function (data) {
            // Select2'yi tüm liste ile doldur
            //console.log(data)

            $('.origins-select, .destinations-select').select2({
                data: data,
                ajax: {
                    url: 'BusLocations/Get', // Controller'daki action metodu
                    dataType: 'json',
                    delay: 250, // Kullanıcı yazmayı durdurduktan 250ms sonra istek gönder
                    data: function (params) {
                        //console.log('data', data)
                        return {
                            term: params.term // Kullanıcının yazdığı anahtar kelime
                        };
                    },
                    processResults: function (data) {
                        //console.log('processResults', data)
                        return {
                            results: data // Controller'dan dönen sonuçlar
                        };
                    },
                    cache: true // İstekleri önbelleğe al
                },
                minimumInputLength: 2 // Arama için en az 2 karakter gerektir
            });

            //$('.origins-select, .destinations-select').select2({
            //    data: data
            //});
        },
        complete: function () {
            var formValues = localStorage.getItem('formValues');
            if (formValues) {
                var formValuesJson = JSON.parse(formValues);

                $('#originsSelect').val(formValuesJson.originsValue).trigger('change');
                $('#destinationsSelect').val(formValuesJson.destinationsValue).trigger('change');
                $('#departureDateInput').val(formValuesJson.departureDateValue);
            }            

            // Event Listener for Select2
            $('#destinationsSelect').on('change', function (e) {
                var destinationsValue = $(this).val();
                var originsValue = $('#originsSelect').val();

                if (destinationsValue === originsValue) {
                    toastr.warning('Başlangıç ​​ve varış noktası olarak aynı konum seçilemez!');
                    $(this).val('').trigger('change'); // Reset selection
                }
            });
        }
    });

    $('.set-departure-day-btn').on('click', function (e) {
        var $btn = $(this);

        var value = $btn.data('value');

        if (value == 'today') {
            const today = new Date();
            $('#departureDateInput').datepicker('setDate', today);
        } else {
            const tomorrow = new Date();
            tomorrow.setDate(tomorrow.getDate() + 1); // calculate tomorrow's date
            $('#departureDateInput').datepicker('setDate', tomorrow);
        }

        $('.set-departure-day-btn').removeClass('btn-secondary').addClass('btn-outline-secondary');

        $btn.removeClass('btn-outline-secondary').addClass('btn-secondary');// button's style is changed
    });

    $('#swapButton').on('click', function () {
        // Get Values of Both Select2
        var value1 = $('#originsSelect').val();
        var value2 = $('#destinationsSelect').val();

        // Set Swapped Values
        $('#originsSelect').val(value2).trigger('change');
        $('#destinationsSelect').val(value1).trigger('change');
    });

    $('#locationsForm').on('submit', function (e) {
        e.preventDefault();
        var originsValue = $('#originsSelect').val();
        var destinationsValue = $('#destinationsSelect').val();
        var departureDateValue = $('#departureDateInput').val();

        if (destinationsValue === originsValue) {
            toastr.warning('Başlangıç ​​ve varış noktası olarak aynı konum seçilemez!');
            return;
        }

        const formValues = { originsValue, destinationsValue, departureDateValue };

        localStorage.setItem('formValues', JSON.stringify(formValues));

        e.target.submit();
    });
});