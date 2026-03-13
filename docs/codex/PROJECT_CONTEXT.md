# Project Context

## Purpose

- Bu repo, Curated Academic Portfolio (CAP) icin .NET tabanli bir akademik portfoy sistemi icerir.
- Cozumde API, WebUI, business, data ve shared katmanlari bulunur.

## Active Branch Policy

- Gunluk gelistirme dali: `semester-project`
- `master` dali stabil kalsin.
- Kullanici acikca istemedikce `master` uzerine commit yapma.

## Solution Map

- `AcademicPortfolio.Shared`: entity siniflari
- `AcademicPortfolio.Data`: `AppDbContext` ve EF Core migration'lari
- `AcademicPortfolio.Business`: is kurallari ve dis servis cagri siniflari
- `AcademicPortfolio.WebAPI`: REST API, Swagger, CORS, PostgreSQL baglantisi
- `AcademicPortfolio.WebUI`: MVC tabanli dashboard arayuzu

## Working Defaults

- Proje dokumanlari `docs/` altinda tutulur.
- Dokumanlar solution'a eklenmez; dogrudan `docs/` klasorunden takip edilir.
- Push oncesi temel dogrulama komutu: `dotnet build AcademicPortfolio.sln`
- Config dosyalarinda gercek sir veya parola tutulmaz.

## Existing Reference Docs

- `docs/INCELEME_NOTLARI.md`
- `docs/codex/PROJECT_CONTEXT.md`
- `docs/codex/SESSION_RESET.md`
- `docs/codex/SESSION_LOG.md`
- `docs/codex/REQUEST_TEMPLATES.md`
- `docs/codex/DECISION_LOG.md`

## First Steps In A Fresh Session

1. `AGENTS.md` dosyasini oku.
2. `docs/codex/PROJECT_CONTEXT.md` dosyasini oku.
3. `git status --short --branch` ile dal ve calisma agacini kontrol et.
4. Gerekliyse ilgili dokuman dosyasini guncelle.
5. Oturum sonuysa `docs/codex/SESSION_LOG.md` dosyasina kisa kayit dus.
