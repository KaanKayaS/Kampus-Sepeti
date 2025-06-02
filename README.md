# Kampus-Sepeti
.Net 8.0 ve Onion architecture ile geliştirdiğim Üniversite öğrencilerin kampüs hayatına adapte olması ve yardımlaşması için yaptığım bitirme projem

🏗️ Teknoloji Mimarisi

Projeyi modern yazılım geliştirme prensipleri doğrultusunda Onion Architecture ile geliştirdim:



Core Katmanları:
 Domain Layer: Entity modelleri ve iş kuralları
 Application Layer: CQRS pattern ile iş mantığı

Infrastructure Katmanları:
 Persistence: Entity Framework Core ile veri erişimi
 Infrastructure: Dış servis entegrasyonları
 Presentation: RESTful API

🛠️ Kullanılan Teknolojiler
Backend (.NET 8.0):
Authentication & Authorization: JWT Token + Identity Framework
Design Patterns:
   Repository Pattern (Read/Write ayrımı)
   Unit of Work Pattern
   Mediator Pattern (MediatR)
   CQRS Pattern

Validation: FluentValidation ile güçlü doğrulama
Mapping: AutoMapper ile nesne dönüşümleri
Exception Handling: Global Exception Middleware
Real-time Communication: SignalR ile canlı mesajlaşma

Database & ORM:
  Entity Framework Core ile Code-First yaklaşımı
  SQL Server veritabanı
  Migration yönetimi

Frontend:
  ASP.NET Core MVC ile server-side rendering
  AJAX ile dinamik içerik güncellemeleri
  SignalR Client ile gerçek zamanlı bildirimleri
  Modern ve responsive UI tasarımı

🎉 Etkinlik sayfası, Etkinlik.io API’si entegre edilerek canlı ve güncel etkinliklerle zenginleştirilmiştir.
