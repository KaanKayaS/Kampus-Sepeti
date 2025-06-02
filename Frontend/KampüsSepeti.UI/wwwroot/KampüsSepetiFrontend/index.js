document.addEventListener('DOMContentLoaded', function () {
    const navbar = document.querySelector('.navbar');
    const citySearch = document.getElementById('citySearch');
    const citiesList = document.querySelector('.cities-list');
    const cities = citiesList ? citiesList.querySelectorAll('a') : [];
    const dropbtn = document.querySelector('.dropbtn');
    const dropdownContent = document.querySelector('.dropdown-content');
    const dropdown = document.querySelector('.dropdown');
    let prevScrollpos = window.pageYOffset;

    // Sayfa yüklendiğinde seçili şehri kontrol et
    const selectedCity = localStorage.getItem('selectedCity');
    const selectedCityId = localStorage.getItem('selectedCityId');
    if (selectedCity && selectedCity !== 'Lokasyon') {
        dropbtn.innerHTML = `
            <i class="fas fa-map-marker-alt"></i>
            ${selectedCity}
            <i class="fa fa-caret-down"></i>
        `;
    }

    // Navbar scroll işlevi
    if (navbar) {
        window.addEventListener('scroll', function () {
            const currentScrollPos = window.pageYOffset;
            if (prevScrollpos > currentScrollPos) {
                navbar.style.top = "0";
            } else {
                navbar.style.top = "-100px";
            }
            prevScrollpos = currentScrollPos;
        });
    } else {
        console.warn('Navbar element not found.');
    }

    // Şehir seçme işlevi
    if (cities.length > 0) {
        cities.forEach(city => {
            city.addEventListener('click', async function (e) {
                e.preventDefault();
                const cityId = this.getAttribute('data-city-id');
                const cityName = this.querySelector('i')?.nextSibling?.textContent?.trim();

                // Dropdown butonunu güncelle
                if (this.classList.contains('all-cities')) {
                    dropbtn.innerHTML = `
                        <i class="fas fa-map-marker-alt"></i>
                        Lokasyon
                        <i class="fa fa-caret-down"></i>
                    `;
                    // LocalStorage'ı temizle
                    localStorage.removeItem('selectedCity');
                    localStorage.removeItem('selectedCityId');
                    // Tüm ilanları getir
                    window.location.href = '/Main/Index';
                } else {
                    dropbtn.innerHTML = `
                        <i class="fas fa-map-marker-alt"></i>
                        ${cityName}
                        <i class="fa fa-caret-down"></i>
                    `;
                    // Seçilen şehri localStorage'a kaydet
                    localStorage.setItem('selectedCity', cityName);
                    localStorage.setItem('selectedCityId', cityId);
                    // Seçilen şehre göre ilanları getir
                    window.location.href = `/Main/GetPostsByLocation?locationId=${cityId}`;
                }
                
                dropdownContent.classList.remove('show');
            });
        });
    } else {
        console.warn('No cities found in the cities list.');
    }

    // Şehir arama işlevi
    if (citySearch) {
        citySearch.addEventListener('input', function (e) {
            const searchValue = e.target.value.toLowerCase();
            cities.forEach(city => {
                const cityName = city.textContent.toLowerCase();
                city.style.display = cityName.includes(searchValue) ? 'flex' : 'none';
            });
        });
    } else {
        console.warn('City search input not found.');
    }

    // Dropdown açık kalması için
    if (dropdown && dropdownContent) {
        dropdown.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            dropdownContent.classList.toggle('show');
            console.log('Dropdown clicked'); // Debug için
        });

        // Dışarı tıklandığında kapat
        document.addEventListener('click', function (e) {
            if (!dropbtn.contains(e.target) && !dropdownContent.contains(e.target)) {
                dropdownContent.classList.remove('show');
            }
        });
    } else {
        console.warn('Dropdown or dropdown content not found.');
    }

    // Profil dropdown
    const profileTrigger = document.querySelector('.profile-trigger');
    const profileMenu = document.querySelector('.profile-menu');

    if (profileTrigger && profileMenu) {
        profileTrigger.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            profileMenu.classList.toggle('active');
            console.log('Profile clicked'); // Debug için
        });

        // Dışarı tıklandığında kapat
        document.addEventListener('click', function (e) {
            if (!profileTrigger.contains(e.target) && !profileMenu.contains(e.target)) {
                profileMenu.classList.remove('active');
            }
        });
    } else {
        console.warn('Profile trigger or menu not found.');
    }

    // Bildirim ve mesaj ikonları hover efekti
    const navIcons = document.querySelectorAll('.nav-icon-wrapper');
    if (navIcons.length > 0) {
        navIcons.forEach(icon => {
            icon.addEventListener('mouseenter', () => {
                icon.style.transform = 'translateY(-2px)';
            });

            icon.addEventListener('mouseleave', () => {
                icon.style.transform = 'translateY(0)';
            });
        });
    } else {
        console.warn('No navigation icons found.');
    }

    // Debug için
    console.log('Elements found:', {
        dropbtn: !!dropbtn,
        dropdownContent: !!dropdownContent,
        profileTrigger: !!profileTrigger,
        profileMenu: !!profileMenu
    });

    // Filtre işlemleri
    document.addEventListener('DOMContentLoaded', function() {
        // Temizle butonu
        const clearButton = document.querySelector('.clear-filter');
        if (clearButton) {
            clearButton.addEventListener('click', function() {
                const form = document.getElementById('filterForm');
                const inputs = form.querySelectorAll('input');
                const selects = form.querySelectorAll('select');

                // Input'ları temizle
                inputs.forEach(input => {
                    input.value = '';
                });

                // Select'leri ilk seçeneğe döndür
                selects.forEach(select => {
                    select.selectedIndex = 0;
                });

                // Formu submit et
                form.submit();
            });
        }

        // Fiyat input kontrolü
        const priceInputs = document.querySelectorAll('.price-inputs input');
        priceInputs.forEach(input => {
            input.addEventListener('input', function() {
                if (this.value < 0) {
                    this.value = 0;
                }
            });
        });
    });
});
