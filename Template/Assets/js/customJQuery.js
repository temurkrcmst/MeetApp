$('#Phone', '#FormPhone')

    .keydown(function (e) {
        var key = e.charCode || e.keyCode || 0;
        $phone = $(this);

        // Auto-format- do not expose the mask as the user begins to type
        if (key !== 8 && key !== 9) {       // 8 ve 9. haneye gelene kadar aşağıdakileri yap
            if ($phone.val().length === 4) {
                $phone.val($phone.val() + ')');
            }
            if ($phone.val().length === 5) {
                $phone.val($phone.val() + ' ');
            }
            if ($phone.val().length === 9) {
                $phone.val($phone.val() + '-');
            }
        }

        // Allow numeric (and tab, backspace, delete) keys only
        return (key == 8 ||  // backspace
            key == 9 ||  // tab
            key == 46 ||  // delete
            (key >= 48 && key <= 57) ||    // 0 -9 
            (key >= 96 && key <= 105));    // numpad 0 -9
    })

    .bind('focus click', function () {
        $phone = $(this);

        if ($phone.val().length === 0) {
            $phone.val('(');
        }
        else {
            var val = $phone.val();
            $phone.val('').val(val); // Ensure cursor remains at the end
        }
    })

    .blur(function () {
        $phone = $(this);

        if ($phone.val() === '(') {
            $phone.val('');
        }
    });