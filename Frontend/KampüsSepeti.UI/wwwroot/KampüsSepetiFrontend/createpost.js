document.addEventListener('DOMContentLoaded', function() {
    const imageInput = document.getElementById('image');
    const imagePreview = document.getElementById('imagePreview');
    const form = document.getElementById('createPostForm');

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
    form.addEventListener('submit', function(e) {
        e.preventDefault();

        // Form verilerini kontrol et
        const title = document.getElementById('title').value;
        const description = document.getElementById('description').value;
        const price = document.getElementById('price').value;
        const brandName = document.getElementById('brandName').value;
        const locationId = document.getElementById('locationId').value;
        const statusId = document.getElementById('statusId').value;
        const categoryId = document.getElementById('categoryId').value;
        const image = imageInput.files[0];

        // Tüm alanların dolu olduğunu kontrol et
        if (!title || !description || !price || !brandName || !locationId || !statusId || !categoryId) {
            alert('Lütfen tüm alanları doldurun.');
            return;
        }

        // FormData oluştur
        const formData = new FormData();
        formData.append('Title', title);
        formData.append('Description', description);
        formData.append('Price', price);
        formData.append('BrandName', brandName);
        formData.append('LocationId', locationId);
        formData.append('StatusId', statusId);
        formData.append('CategoryId', categoryId);
        
        // Eğer resim seçilmişse ekle
        if (image) {
            formData.append('image', image);
        }

        // API'ye gönder
        fetch('/Post/CreatePost', {
            method: 'POST',
            body: formData
        })
        .then(response => {
            if (response.ok) {
                alert('İlan başarıyla oluşturuldu!');
                window.location.href = '/Main/Index';
            } else {
                return response.text().then(text => {
                    throw new Error(text || 'İlan oluşturulurken bir hata oluştu.');
                });
            }
        })
        .catch(error => {
            console.error('Hata:', error);
            alert(error.message);
        });
    });
}); 