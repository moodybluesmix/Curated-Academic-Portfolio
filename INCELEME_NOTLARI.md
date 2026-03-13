# Proje Inceleme Notlari

Tarih: 2026-03-13

## Genel Ozet

- CAP, akademik yayin verisini merkezilestiren .NET 9 tabanli katmanli bir cozum.
- Cozumde `Shared`, `Data`, `Business`, `WebAPI` ve `WebUI` olmak uzere bes ana proje var.
- Son degisiklikler, varsayilan iskeleti gercek bir akademik portfoy akisina tasiyor.

## Katmanlar

- `AcademicPortfolio.Shared`: `AcademicWork` ve `Journal` entity'lerini tutuyor.
- `AcademicPortfolio.Data`: `AppDbContext` ve EF Core migration dosyalarini iceriyor.
- `AcademicPortfolio.Business`: DOI metadata cekme ve AI sorgu baglami olusturma servislerini barindiriyor.
- `AcademicPortfolio.WebAPI`: REST endpoint'leri, CORS, Swagger ve PostgreSQL baglantisini yurutuyor.
- `AcademicPortfolio.WebUI`: MVC icinde calisan tek sayfa dashboard arayuzunu sagliyor.

## Uygulama Akislari

1. Kullanici WebUI uzerinden DOI giriyor.
2. WebUI, `/api/Works/fetch/{doi}` ile Crossref metadata cekiyor.
3. Kullanici Q kategorisi ve proje kodu verip `/api/Works/save` ile kayit olusturuyor.
4. Dashboard `/api/Works` uzerinden verileri listeleyip istatistikleri hesapliyor.
5. Rapor akisinda `/api/Report/export-excel` endpoint'i CSV ciktisi uretiyor.
6. AI akisinda `/api/Works/ai-query` endpoint'i tum yayinlari context olarak Gemini servisine gonderiyor.

## Mevcut Git Degisikliklerinin Ozeti

- Yeni entity, `DbContext` ve migration dosyalari eklenmis.
- WebAPI tarafina `WorksController` ve `ReportController` eklenmis.
- Varsayilan `WeatherForecast` ornek dosyalari kaldirilmis.
- WebUI ana sayfasi dashboard, yayin ekleme, kutuphane ve AI sorgu ekranlariyla yenilenmis.
- `package.json` ve `package-lock.json` eklenmis.

## Dikkat Ceken Noktalar

- `GeminiAIService` icindeki API key su an bos; config verilmeden AI endpoint'i calismayacak.
- WebUI, API adreslerini dogrudan `https://localhost:7231` olarak hard-code ediyor; ortam bazli config ihtiyaci var.
- `ReportController` CSV uretirken temel temizlik yapiyor ama quote ve newline escape etmedigi icin bozuk satir riski tasiyor.
- `GetAll` endpoint'i siralama yapmadigi icin dashboard'daki "son eklenenler" listesi gercek eklenme sirasini garanti etmiyor.
- `package.json` bagimliliklari mevcut MVC arayuzunde npm pipeline ile aktif kullanilmiyor; ya aktiflestirilmeli ya da sadelestirilmeli.
- Repo icinde gizli bilgi tutulmamali; push oncesi acik veritabani sifresi temizlendi.

## Push Oncesi Temizlik

- `AcademicPortfolio.WebAPI/Properties/launchSettings.json` gecerli JSON formatina getirildi.
- `AcademicPortfolio.WebAPI/appsettings.json` icindeki sabit veritabani sifresi temizlendi.
- `AcademicPortfolio.WebUI/appsettings.json` gereksiz connection string ve comment kalintilarindan arindirildi.

## Dogrulama

- Sandbox icinde yapilan `dotnet build` denemeleri dosya izinlerine takildi.
- Push oncesi son build dogrulamasi yetkili oturumda alinmali.
- Bu repoda otomatik test projesi bulunmuyor.
