document.addEventListener('DOMContentLoaded', function() {
    console.log('JavaScript dosyası yüklendi!');

    const dropdown = document.querySelector('.dropdown');
    const dropdownContent = document.querySelector('.dropdown-content');
    const citySearch = document.getElementById('citySearch');
    const cities = document.querySelectorAll('.cities-list a');
    const dropbtn = document.querySelector('.dropbtn');

    // Sayfalama için gerekli değişkenler
    const state = {
        currentPage: 1,
        itemsPerPage: 10
    };

    // Yukarı çıkma butonu için
    const scrollTop = document.querySelector('.scroll-top');
    if (scrollTop) {
        window.addEventListener('scroll', () => {
            if (window.pageYOffset > 200) {
                scrollTop.classList.add('visible');
            } else {
                scrollTop.classList.remove('visible');
            }
        });

        scrollTop.addEventListener('click', () => {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    }

    // Sayfalama butonlarına tıklama işlemi
    document.querySelectorAll('.page-btn').forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            const pageNumber = parseInt(this.textContent || this.innerText);
            if (!isNaN(pageNumber)) {
                window.location.href = `/Announcement/Index?page=${pageNumber}`;
            }
        });
    });

    // Prev/Next butonları için
    const prevBtn = document.querySelector('.prev-btn');
    const nextBtn = document.querySelector('.next-btn');
    
    if (prevBtn) {
        prevBtn.addEventListener('click', function() {
            const currentPage = parseInt(document.querySelector('.page-btn.active').textContent);
            if (currentPage > 1) {
                window.location.href = `/Announcement/Index?page=${currentPage - 1}`;
            }
        });
    }
    
    if (nextBtn) {
        nextBtn.addEventListener('click', function() {
            const currentPage = parseInt(document.querySelector('.page-btn.active').textContent);
            const totalPages = document.querySelectorAll('.page-btn').length;
            if (currentPage < totalPages) {
                window.location.href = `/Announcement/Index?page=${currentPage + 1}`;
            }
        });
    }
});

