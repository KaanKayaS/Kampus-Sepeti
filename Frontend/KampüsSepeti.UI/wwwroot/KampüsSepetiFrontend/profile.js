document.addEventListener('DOMContentLoaded', function() {
    const navbar = document.querySelector('.navbar');
    const citySearch = document.getElementById('citySearch');
    const citiesList = document.querySelector('.cities-list');
    const cities = citiesList.querySelectorAll('a');
    const dropbtn = document.querySelector('.dropbtn');
    let prevScrollpos = window.pageYOffset;
    
    // Şehir seçme işlevi
    cities.forEach(city => {
        city.addEventListener('click', function(e) {
            e.preventDefault();
            const cityName = this.querySelector('i').nextSibling.textContent.trim();
            // "Tüm Şehirler" kontrolü
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

    // Navbar scroll işlevi
    window.onscroll = function() {
        const currentScrollPos = window.pageYOffset;
        if (prevScrollpos > currentScrollPos) {
            navbar.style.top = "0";
        } else {
            navbar.style.top = "-100px";
        }
        prevScrollpos = currentScrollPos;
    }

    // Şehir arama işlevi
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

    // Dropdown açık kalması için
    const dropdown = document.querySelector('.dropdown');
    const dropdownContent = document.querySelector('.dropdown-content');

    dropdown.addEventListener('click', function(e) {
        e.stopPropagation();
        dropdownContent.classList.toggle('show');
        citySearch.focus();
    });

    // Dropdown dışına tıklandığında kapanması
    document.addEventListener('click', function() {
        dropdownContent.classList.remove('show');
    });

    dropdownContent.addEventListener('click', function(e) {
        e.stopPropagation();
    });

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
            if (!profileTrigger.contains(e.target) && !profileMenu.contains(e.target)) {
                profileMenu.classList.remove('active');
            }
        });
    }

    // Bildirim ve mesaj ikonları için hover efekti
    const navIcons = document.querySelectorAll('.nav-icon-wrapper');
    navIcons.forEach(icon => {
        icon.addEventListener('mouseenter', () => {
            icon.style.transform = 'translateY(-2px)';
        });
        
        icon.addEventListener('mouseleave', () => {
            icon.style.transform = 'translateY(0)';
        });
    });
});

