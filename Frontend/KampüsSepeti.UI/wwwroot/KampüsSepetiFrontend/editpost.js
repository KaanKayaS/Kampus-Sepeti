document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('editPostForm');
    const imageInput = document.getElementById('image');
    const imagePreview = document.getElementById('imagePreview');

    // Resim önizleme
    imageInput.addEventListener('change', function(e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(e) {
                imagePreview.src = e.target.result;
                imagePreview.style.display = 'block';
            }
            reader.readAsDataURL(file);
        }
    });

    // Form gönderimi
    form.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        try {
            // Form verilerini JSON formatında hazırla
            const formData = new FormData(form);
            const jsonData = {
                id: parseInt(formData.get('id')),
                title: formData.get('title'),
                description: formData.get('description'),
                price: parseFloat(formData.get('price')),
                brandName: formData.get('brandName'),
                locationId: parseInt(formData.get('locationId')),
                statusId: parseInt(formData.get('statusId')),
                categoryId: parseInt(formData.get('categoryId')),
                image: imagePreview.src, // Mevcut resim yolunu imagePreview'dan al
                userId: parseInt(document.querySelector('input[name="userId"]').value) // Kullanıcı ID'sini ekle
            };

            // Eğer yeni bir resim seçildiyse
            if (imageInput.files[0]) {
                jsonData.image = await getBase64(imageInput.files[0]);
            }

            console.log('Gönderilecek veriler:', jsonData);

            // API'ye direkt gönder
            const response = await fetch('http://localhost:5000/api/Posts/UpdatePost', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(jsonData)
            });

            if (!response.ok) {
                const errorData = await response.text();
                console.error('API Hata Yanıtı:', errorData);
                throw new Error(errorData || 'Güncelleme başarısız');
            }

            const result = await response.json();
            console.log('Güncelleme başarılı:', result);
            alert('İlan başarıyla güncellendi!');
            window.location.href = '/MyPosts/Index';
        } catch (error) {
            console.error('Güncelleme hatası:', error);
            alert('İlan güncellenirken bir hata oluştu: ' + error.message);
        }
    });

    // Resmi Base64 formatına çevir
    function getBase64(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = error => reject(error);
        });
    }
}); 