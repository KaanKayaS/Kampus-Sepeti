document.addEventListener('DOMContentLoaded', function() {
    // Profil dropdown için
    const profileTrigger = document.querySelector('.profile-trigger');
    const profileMenu = document.querySelector('.profile-menu');

    if (profileTrigger && profileMenu) {
        profileTrigger.addEventListener('click', (e) => {
            e.stopPropagation();
            profileMenu.classList.toggle('active');
        });

        // Dışarı tıklandığında dropdown'ı kapat
        document.addEventListener('click', (e) => {
            if (!profileMenu.contains(e.target)) {
                profileMenu.classList.remove('active');
            }
        });
    }

    // Bildirim ve mesaj ikonları için hover efekti
    const navIcons = document.querySelectorAll('.nav-icon-wrapper');
    navIcons.forEach(icon => {
        icon.addEventListener('mouseenter', () => {
            icon.style.transform = 'translateY(-2px)';
            icon.style.opacity = '0.8';
        });
        
        icon.addEventListener('mouseleave', () => {
            icon.style.transform = 'translateY(0)';
            icon.style.opacity = '1';
        });
    });

    // Lokasyon dropdown için
    const dropdown = document.querySelector('.dropdown');
    const dropdownContent = document.querySelector('.dropdown-content');
    const citySearch = document.getElementById('citySearch');
    const cities = document.querySelectorAll('.cities-list a');
    const dropbtn = document.querySelector('.dropbtn');

    // Dropdown açma/kapama
    dropdown.addEventListener('click', function(e) {
        e.stopPropagation();
        dropdownContent.classList.toggle('show');
        if (dropdownContent.classList.contains('show')) {
            citySearch.focus();
        }
    });

    // Dışarı tıklandığında dropdown'ı kapat
    document.addEventListener('click', function() {
        dropdownContent.classList.remove('show');
    });

    // Dropdown içine tıklandığında kapanmasın
    dropdownContent.addEventListener('click', function(e) {
        e.stopPropagation();
    });

    // Şehir arama
    citySearch.addEventListener('input', function(e) {
        const searchValue = e.target.value.toLowerCase();
        cities.forEach(city => {
            const cityName = city.textContent.toLowerCase();
            if (cityName.includes(searchValue)) {
                city.style.display = 'flex';
            } else {
                city.style.display = 'none';
            }
        });
    });

    // Şehir seçme
    cities.forEach(city => {
        city.addEventListener('click', function(e) {
            e.preventDefault();
            const cityName = this.querySelector('i').nextSibling.textContent.trim();
            
            if (this.classList.contains('all-cities')) {
                dropbtn.innerHTML = `
                    <i class="fas fa-map-marker-alt"></i>
                    Lokasyon
                    <i class="fa fa-caret-down"></i>
                `;
            } else {
                dropbtn.innerHTML = `
                    <i class="fas fa-map-marker-alt"></i>
                    ${cityName}
                    <i class="fa fa-caret-down"></i>
                `;
            }
            dropdownContent.classList.remove('show');
        });
    });
});

