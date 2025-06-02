document.addeventlistener('domcontentloaded', () => {
    console.log('dom loaded'); // sayfa yüklendiğinde kontrol

    const form = document.getelementbyıd('loginform');
    const errormessage = document.getelementbyıd('errormessage');

    if (!form) {
        console.error('form elementi bulunamadı!');
        return;
    }

    if (!errormessage) {
        console.error('hata mesaj elementi bulunamadı!');
        return;
    }

    console.log('form elementi bulundu:', form);

    form.addeventlistener('submit', async (e) => {
        e.preventdefault();

        const email = document.getelementbyıd('email').value;
        const password = document.getelementbyıd('password').value;

        try {
            const response = await fetch('http://localhost:5000/api/auth/login', {
                method: 'post',
                headers: {
                    'content-type': 'application/json'
                },
                body: json.stringify({
                    email: email,
                    password: password
                })
            });

            if (response.ok) {
                const data = await response.json();

                // normal form submit
                form.submit();
            } else {
                const error = await response.text();
                const errordiv = document.queryselector('.validation-summary-errors');
                if (!errordiv) {
                    const newerrordiv = document.createelement('div');
                    newerrordiv.classname = 'validation-summary-errors text-danger';
                    newerrordiv.textcontent = 'kullanıcı adı veya şifre hatalı';
                    form.insertbefore(newerrordiv, form.firstchild);
                }
            }
        } catch (error) {
            console.error('hata:', error);
            const errordiv = document.queryselector('.validation-summary-errors');
            if (!errordiv) {
                const newerrordiv = document.createelement('div');
                newerrordiv.classname = 'validation-summary-errors text-danger';
                newerrordiv.textcontent = 'bir hata oluştu: ' + error.message;
                form.insertbefore(newerrordiv, form.firstchild);
            }
        }
    });

    // tema değiştirme fonksiyonları
    const themetoggle = document.getelementbyıd('themetoggle');

    // sayfa yüklendiğinde kaydedilmiş temayı kontrol et
    const savedtheme = localstorage.getıtem('theme') || 'light';
    document.documentelement.setattribute('data-theme', savedtheme);

    themetoggle.addeventlistener('click', () => {
        const currenttheme = document.documentelement.getattribute('data-theme');
        const newtheme = currenttheme === 'light' ? 'dark' : 'light';

        document.documentelement.setattribute('data-theme', newtheme);
        localstorage.setıtem('theme', newtheme);
    });

    // ınput animasyonları
    const inputs = document.queryselectorall('.input-group input');

    inputs.foreach(input => {
        input.addeventlistener('focus', function() {
            this.parentelement.classlist.add('focused');
        });

        input.addeventlistener('blur', function() {
            if (this.value === '') {
                this.parentelement.classlist.remove('focused');
            }
        });

        // sayfa yüklendiğinde değer varsa focused class'ı ekle
        if (input.value !== '') {
            input.parentelement.classlist.add('focused');
        }
    });
});

