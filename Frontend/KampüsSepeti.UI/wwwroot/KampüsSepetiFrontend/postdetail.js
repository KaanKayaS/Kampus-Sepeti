document.addEventListener('DOMContentLoaded', function () {
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

    // Diğer mevcut kodlar...
    // Teklif ve sohbet butonları için olaylar
    const chatBtn = document.querySelector('.chat-btn');
    const offerBtn = document.querySelector('.offer-btn');

    if (chatBtn) {
        chatBtn.addEventListener('click', () => {
            console.log('Sohbet başlatılıyor...');
        });
    }

    if (offerBtn) {
        offerBtn.addEventListener('click', () => {
            console.log('Teklif verme modalı açılıyor...');
        });
    }

    // Takip butonu için
    const followBtn = document.querySelector('.follow-btn');

    if (followBtn) {
        followBtn.addEventListener('click', () => {
            followBtn.classList.toggle('following');
            const isFollowing = followBtn.classList.contains('following');
            followBtn.innerHTML = isFollowing ? `
                <svg class="heart-icon" viewBox="0 0 24 24" fill="currentColor" stroke="currentColor" stroke-width="2">
                    <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z"></path>
                </svg>
                <span>Takip Ediliyor</span>
            ` : `
                <svg class="heart-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                    <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z"></path>
                </svg>
                <span>Takip Et</span>
            `;
        });
    }

    // İstek listesi butonu için
    const wishlistBtn = document.querySelector('.wishlist-btn');

    if (wishlistBtn) {
        wishlistBtn.addEventListener('click', () => {
            wishlistBtn.classList.toggle('active');
            const isActive = wishlistBtn.classList.contains('active');

            const tooltip = wishlistBtn.querySelector('.wishlist-tooltip');
            if (tooltip) {
                tooltip.textContent = isActive ? 'İstek Listesinden Çıkar' : 'İstek Listesine Ekle';
            }
        });
    }
});
