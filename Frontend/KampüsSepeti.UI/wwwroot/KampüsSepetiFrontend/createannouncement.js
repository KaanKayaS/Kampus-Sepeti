document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('.announcement-form');
    const submitBtn = document.querySelector('.submit-btn');



    // Lokasyonları yükle
    async function loadLocations() {
        try {
            const response = await fetch('http://localhost:5000/api/Location');
            const locations = await response.json();
            
            const locationSelect = document.querySelector('#LocationId');
            const currentValue = locationSelect.value; // Mevcut seçili değeri koru
            
            locationSelect.innerHTML = '<option value="">Konum Seçin</option>';
            locations.forEach(location => {
                const option = document.createElement('option');
                option.value = location.id;
                option.textContent = location.name;
                locationSelect.appendChild(option);
            });

            // Eğer önceden seçili bir değer varsa, onu tekrar seç
            if (currentValue) {
                locationSelect.value = currentValue;
            }
        } catch (error) {
            console.error('Lokasyonlar yüklenirken hata:', error);
        }
    }

    // Sayfa yüklendiğinde lokasyonları yükle
    loadLocations();

    // Form gönderilmeden önce kontrol
    form.addEventListener('submit', async function(e) {
        e.preventDefault();

        const title = document.querySelector('#Title').value;
        const description = document.querySelector('#Description').value;
        const location = document.querySelector('#LocationId').value;

        if (!title || !description || !location) {
            alert('Lütfen tüm alanları doldurunuz.');
            return;
        }

        // Form gönderilirken butonu devre dışı bırak
        submitBtn.disabled = true;
        submitBtn.textContent = 'Gönderiliyor...';

        try {
            const response = await fetch('http://localhost:5000/api/Announcement', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    title: title,
                    description: description,
                    locationId: parseInt(location),
                    userId: document.querySelector('#UserId').value
                })
            });

            if (!response.ok) {
                const data = await response.json();
                // API'den gelen hata mesajını göster
                alert(data.message || 'Bir hata oluştu');
                // Hata durumunda lokasyonları tekrar yükle
                await loadLocations();
            } else {
                // Başarılı ise ana sayfaya yönlendir
                window.location.href = '/Announcement/Index';
            }
        } catch (error) {
            alert('Bir hata oluştu: ' + error.message);
            await loadLocations();
        } finally {
            // Submit butonunu tekrar aktif et
            submitBtn.disabled = false;
            submitBtn.textContent = 'Duyuruyu Yayınla';
        }
    });

    // Textarea otomatik yükseklik ayarı
    const textarea = document.querySelector('#Description');
    if (textarea) {
        textarea.addEventListener('input', function() {
            this.style.height = 'auto';
            this.style.height = (this.scrollHeight) + 'px';
        });
    }
}); 