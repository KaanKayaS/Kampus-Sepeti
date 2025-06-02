$(document).ready(function () {
    // jQuery validation için custom mesajlar
    $.validator.messages.required = "Bu alan zorunludur";
    $.validator.messages.email = "Geçerli bir e-posta adresi giriniz";
    $.validator.messages.minlength = "En az {0} karakter girmelisiniz";

    // Form validasyonu
    $("#registerForm").validate({
        rules: {
            FullName: {
                required: true,
                minlength: 2
            },
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 6
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#password"
            },
            PhoneNumber: {
                required: true
            },
            LocationId: {
                required: true
            },
            UniversityId: {
                required: true
            }
        },
        messages: {
            Password: {
                required: "Şifre alanı boş bırakılamaz",
                minlength: "Şifre en az 6 karakter olmalıdır"
            },
            ConfirmPassword: {
                required: "Şifre tekrarı gereklidir",
                equalTo: "Şifreler eşleşmiyor"
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('validation-message');
            error.insertAfter(element.closest('.input-group'));
        },
        highlight: function (element) {
            $(element).closest('.input-group').addClass('error');
        },
        unhighlight: function (element) {
            $(element).closest('.input-group').removeClass('error');
        }
    });

    // Şifre eşleşme kontrolü
    $('#confirmPassword').on('keyup', function() {
        var password = $('#password').val();
        var confirmPassword = $(this).val();
        
        if (password !== confirmPassword) {
            $(this).closest('.form-group').find('.validation-message span').text('Şifreler eşleşmiyor');
            $(this).closest('.input-group').addClass('error');
        } else {
            $(this).closest('.form-group').find('.validation-message span').text('');
            $(this).closest('.input-group').removeClass('error');
        }
    });

    // Şifre göster/gizle
    $('.toggle-password').click(function () {
        const input = $(this).prev('input');
        const type = input.attr('type') === 'password' ? 'text' : 'password';
        input.attr('type', type);
        $(this).toggleClass('fa-eye fa-eye-slash');
    });

    // Telefon numarası formatı
    const phoneInput = document.querySelector('input[name="PhoneNumber"]');
    if (phoneInput) {
        phoneInput.addEventListener('input', function(e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length > 0) {
                if (value.length <= 3) {
                    value = `(${value}`;
                } else if (value.length <= 6) {
                    value = `(${value.slice(0, 3)}) ${value.slice(3)}`;
                } else if (value.length <= 10) {
                    value = `(${value.slice(0, 3)}) ${value.slice(3, 6)}-${value.slice(6)}`;
                }
            }
            e.target.value = value;
        });
    }
});
