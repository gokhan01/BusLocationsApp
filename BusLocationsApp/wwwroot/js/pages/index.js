$(document).ready(function () {
    $('#departureDateInput').datepicker({
        language: 'tr-TR',
        startDate: '1d',//Minimum valid date for departure date is Today.
    });

    $.ajax({
        url: 'BusLocations/Get',
        type: 'GET',
        success: function (data) {

            //Users should be able to perform text-based search on origin and destination fields. The search keyword user
            //enters should be used in order to fetch related bus locations from the obilet.com business API
            //GetBusLocations method.
            $('.origins-select, .destinations-select').select2({
                language: "tr",
                data: data,
                ajax: {
                    url: 'BusLocations/Get', 
                    dataType: 'json',
                    delay: 250, // Send request 250ms after user stops typing
                    data: function (params) {
                        return {
                            term: params.term // Keyword typed by the user
                        };
                    },
                    processResults: function (data) {
                        return {
                            results: data
                        };
                    },
                    cache: true // Cache requests
                },
                minimumInputLength: 2 // Require at least 2 characters for search
            });

            //$('.origins-select, .destinations-select').select2({
            //    data: data
            //});
        },
        complete: function () {
            //Last queried origin, destination and departure date values should be stored on client side. Whenever a user revisits the application, existing origin, destination and departure date values should be set as default values, if available.
            var formValues = localStorage.getItem('formValues');
            if (formValues) {
                var formValuesJson = JSON.parse(formValues);

                $('#originsSelect').val(formValuesJson.originsValue).trigger('change');
                $('#destinationsSelect').val(formValuesJson.destinationsValue).trigger('change');
                $('#departureDateInput').val(formValuesJson.departureDateValue);
            }            

            // Users can not select same location as both origin and destination.
            $('#destinationsSelect').on('change', function (e) {
                var destinationsValue = $(this).val();
                var originsValue = $('#originsSelect').val();

                if (destinationsValue === originsValue) {
                    toastr.warning('Başlangıç ​​ve varış noktası olarak aynı konum seçilemez!');
                    $(this).val('').trigger('change'); // Reset selection
                }
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            toastr.error(jqXHR.responseJSON.Message);
        }
    });

    //Quick selection buttonsfor setting the date to “Today” and “Tomorrow” should setting the value of the departure date field properly.
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

    //Users should be able to swap selected origin and destination locations using the swap button shown in the design
    $('#swapButton').on('click', function () {
        // Get Values of Both Select2
        var value1 = $('#originsSelect').val();
        var value2 = $('#destinationsSelect').val();

        // Set Swapped Values
        $('#originsSelect').val(value2).trigger('change');
        $('#destinationsSelect').val(value1).trigger('change');
    });

    //Search button should redirect user to the journey index page with the specified origin, destination and departure date information
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

        //Last queried origin, destination and departure date values should be stored on client side. Whenever a user revisits the application, existing origin, destination and departure date values should be set as default values, if available.
        localStorage.setItem('formValues', JSON.stringify(formValues));

        e.target.submit();
    });
});