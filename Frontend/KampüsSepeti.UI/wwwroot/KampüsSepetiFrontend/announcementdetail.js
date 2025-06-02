// Yorum silme fonksiyonu
async function deleteComment(commentId) {
    if (confirm('Bu yorumu silmek istediğinizden emin misiniz?')) {
        try {
            const response = await fetch(`http://localhost:5000/api/Comment?id=${commentId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok) {
                // Silinen yorumu DOM'dan kaldır
                const commentElement = document.querySelector(`[data-comment-id="${commentId}"]`);
                if (commentElement) {
                    commentElement.remove();
                    
                    // Yorum sayısını güncelle
                    const commentCount = document.querySelector('.comment-count');
                    if (commentCount) {
                        const currentCount = parseInt(commentCount.textContent);
                        commentCount.textContent = currentCount - 1;
                    }
                    
                    // Eğer başka yorum kalmadıysa "Henüz yorum yapılmamış" mesajını göster
                    const remainingComments = document.querySelectorAll('.comment-item');
                    if (remainingComments.length === 0) {
                        const noComments = document.createElement('p');
                        noComments.className = 'no-comments';
                        noComments.textContent = 'Henüz yorum yapılmamış.';
                        document.getElementById('commentsContainer').appendChild(noComments);
                    }
                }
                
                alert('Yorum başarıyla silindi!');
                window.location.reload(); // Sayfayı yenile
            } else {
                throw new Error('Yorum silinirken bir hata oluştu.');
            }
        } catch (error) {
            console.error('Hata:', error);
            alert(error.message || 'Bir hata oluştu. Lütfen tekrar deneyin.');
        }
    }
}

document.addEventListener('DOMContentLoaded', function() {
    const commentsPerPage = 5;
    let currentPage = 1;
    const commentsContainer = document.getElementById('commentsContainer');
    const paginationContainer = document.getElementById('pagination');
    const allComments = Array.from(commentsContainer.querySelectorAll('.comment-item'));
    const totalPages = Math.ceil(allComments.length / commentsPerPage);
    const commentForm = document.getElementById('commentForm');
    const commentContent = document.getElementById('commentContent');
    const submitButton = document.querySelector('.submit-comment-btn');

    // Tarih formatını düzenleme
    const commentDates = document.querySelectorAll('.comment-date span');
    commentDates.forEach(dateElement => {
        const dateText = dateElement.textContent.trim();
        if (dateText) {
            const [datePart, timePart] = dateText.split(' ');
            const [day, month, year] = datePart.split('.');
            const [hours, minutes] = timePart ? timePart.split(':') : ['00', '00'];
            
            const formattedDate = new Date(year, month - 1, day, hours, minutes);
            if (!isNaN(formattedDate)) {
                dateElement.textContent = formattedDate.toLocaleDateString('tr-TR', {
                    day: '2-digit',
                    month: '2-digit',
                    year: 'numeric',
                    hour: '2-digit',
                    minute: '2-digit'
                });
            }
        }
    });

    // Yorum ekleme formu işlemi
    commentForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        const content = commentContent.value.trim();
        const userId = document.getElementById('userId').value;
        const announcementId = document.getElementById('announcementId').value;

        if (!content || !userId) {
            alert('Lütfen yorumunuzu yazın ve giriş yaptığınızdan emin olun.');
            return;
        }

        submitButton.disabled = true;
        submitButton.innerHTML = '<i class="bi bi-arrow-repeat"></i> Gönderiliyor...';

        try {
            // Önce yorumu oluştur
            console.log('Yorum oluşturma isteği gönderiliyor...');
            const createResponse = await fetch('http://localhost:5000/api/Comment', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    content: content,
                    userId: parseInt(userId),
                    announcementId: parseInt(announcementId)
                })
            });

            console.log('Yorum oluşturma yanıtı:', {
                status: createResponse.status,
                statusText: createResponse.statusText,
                headers: Object.fromEntries(createResponse.headers.entries())
            });

            if (!createResponse.ok) {
                throw new Error('Yorum gönderilemedi. Lütfen tekrar deneyin.');
            }

            // Yorum oluşturma yanıtını al
            const createResponseText = await createResponse.text();
            console.log('Yorum oluşturma yanıt içeriği:', createResponseText);

            // Kısa bir bekleme ekle (API'nin veritabanını güncellemesi için)
            await new Promise(resolve => setTimeout(resolve, 500));

            // Yorum başarıyla oluşturuldu mesajı geldiğinde, son yorumu al
            console.log('Son yorumu almak için istek gönderiliyor...');
            const lastCommentResponse = await fetch(`http://localhost:5000/api/Comment/GetLastComment?userId=${userId}&announcementId=${announcementId}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json'
                }
            });

            console.log('Son yorum yanıtı:', {
                status: lastCommentResponse.status,
                statusText: lastCommentResponse.statusText,
                headers: Object.fromEntries(lastCommentResponse.headers.entries())
            });

            if (!lastCommentResponse.ok) {
                const errorText = await lastCommentResponse.text();
                console.error('Son yorum hata yanıtı:', errorText);
                throw new Error('Son yorum alınamadı.');
            }

            const newComment = await lastCommentResponse.json();
            console.log('Yeni yorum detayları:', newComment);
            
            // Yeni yorumu DOM'a ekle
            const commentElement = createCommentElement(newComment);
            
            // No comments mesajını kaldır eğer varsa
            const noComments = commentsContainer.querySelector('.no-comments');
            if (noComments) {
                noComments.remove();
            }
            
            commentsContainer.insertBefore(commentElement, commentsContainer.firstChild);
            
            // Yorum sayısını güncelle ve son sayfaya git
            allComments.unshift(commentElement);
            const newTotalPages = Math.ceil(allComments.length / commentsPerPage);
            if (newTotalPages > totalPages) {
                currentPage = newTotalPages;
            }
            
            // Pagination'ı güncelle
            showComments(currentPage);
            createPaginationButtons();
            
            // Formu temizle
            commentContent.value = '';
            
        } catch (error) {
            console.error('Hata:', error);
            alert(error.message || 'Yorum gönderilirken bir hata oluştu. Lütfen tekrar deneyin.');
        } finally {
            submitButton.disabled = false;
            submitButton.innerHTML = '<i class="bi bi-send"></i> Gönder';
        }
    });

    // Yeni yorum elementi oluştur
    function createCommentElement(comment) {
        const div = document.createElement('div');
        div.className = 'comment-item';
        div.setAttribute('data-comment-id', comment.id);
        
        // Tarihi formatla
        let formattedDate;
        try {
            const date = new Date(comment.createdDate);
            formattedDate = date.toLocaleString('tr-TR', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric',
                hour: '2-digit',
                minute: '2-digit'
            });
        } catch (e) {
            formattedDate = new Date().toLocaleString('tr-TR', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric',
                hour: '2-digit',
                minute: '2-digit'
            });
        }

        // Kullanıcı ID'sini ve duyuru sahibi ID'sini al
        const currentUserId = document.getElementById('userId').value;
        const announcementOwnerId = document.getElementById('announcementOwnerId').value;
        const isCurrentUser = currentUserId && comment.userId && currentUserId === comment.userId.toString();
        const isAnnouncementOwner = currentUserId && announcementOwnerId && currentUserId === announcementOwnerId;

        div.innerHTML = `
            <div class="comment-header">
                <div class="comment-user">
                    <img src="https://picsum.photos/40" alt="User" class="comment-user-image">
                    <span class="comment-username">${comment.userName}</span>
                </div>
                <div class="comment-actions">
                    <div class="comment-date">
                        <i class="bi bi-calendar"></i>
                        <span>${formattedDate}</span>
                    </div>
                    ${(isCurrentUser || isAnnouncementOwner) ? `
                        <button class="delete-comment-btn" onclick="deleteComment(${comment.id})">
                            <i class="bi bi-x-circle"></i>
                        </button>
                    ` : ''}
                </div>
            </div>
            <div class="comment-content">
                <p>${comment.content}</p>
            </div>
        `;
        return div;
    }

    // Yorumları göster
    function showComments(page) {
        const start = (page - 1) * commentsPerPage;
        const end = start + commentsPerPage;
        
        allComments.forEach((comment, index) => {
            if (index >= start && index < end) {
                comment.style.display = 'block';
            } else {
                comment.style.display = 'none';
            }
        });
    }

    // Pagination butonlarını oluştur
    function createPaginationButtons() {
        paginationContainer.innerHTML = '';
        const newTotalPages = Math.ceil(allComments.length / commentsPerPage);
        
        // Önceki sayfa butonu
        const prevButton = document.createElement('button');
        prevButton.innerHTML = '&laquo;';
        prevButton.disabled = currentPage === 1;
        prevButton.addEventListener('click', () => {
            if (currentPage > 1) {
                currentPage--;
                showComments(currentPage);
                updatePaginationButtons();
            }
        });
        paginationContainer.appendChild(prevButton);

        // Sayfa numaraları
        for (let i = 1; i <= newTotalPages; i++) {
            const pageButton = document.createElement('button');
            pageButton.textContent = i;
            if (i === currentPage) {
                pageButton.classList.add('active');
            }
            pageButton.addEventListener('click', () => {
                currentPage = i;
                showComments(currentPage);
                updatePaginationButtons();
            });
            paginationContainer.appendChild(pageButton);
        }

        // Sonraki sayfa butonu
        const nextButton = document.createElement('button');
        nextButton.innerHTML = '&raquo;';
        nextButton.disabled = currentPage === newTotalPages;
        nextButton.addEventListener('click', () => {
            if (currentPage < newTotalPages) {
                currentPage++;
                showComments(currentPage);
                updatePaginationButtons();
            }
        });
        paginationContainer.appendChild(nextButton);
    }

    // Pagination butonlarını güncelle
    function updatePaginationButtons() {
        const buttons = paginationContainer.querySelectorAll('button');
        const newTotalPages = Math.ceil(allComments.length / commentsPerPage);
        
        buttons.forEach((button, index) => {
            if (index === 0) { // Önceki buton
                button.disabled = currentPage === 1;
            } else if (index === buttons.length - 1) { // Sonraki buton
                button.disabled = currentPage === newTotalPages;
            } else { // Sayfa numaraları
                const pageNumber = parseInt(button.textContent);
                button.classList.toggle('active', pageNumber === currentPage);
            }
        });
    }

    // İlk yüklemede pagination'ı başlat
    if (allComments.length > 0) {
        showComments(currentPage);
        createPaginationButtons();
    }
}); 