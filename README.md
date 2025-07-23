# 📚 BookCatalogApp

_Moderní webová aplikace pro evidenci knih, CD a DVD vytvořená v **ASP.NET Core 8**_

---

## 🎯 Přehled projektu

**BookCatalogApp** je pokročilá evidenční aplikace určená pro správu osobních knihoven a mediálních kolekcí.  
Aplikace byla navržena s důrazem na moderní uživatelské rozhraní, funkcionalitu a škálovatelnost.

---

## ✨ Klíčové funkce

- 📖 **Kompletní evidence knih, CD a DVD**
- 🔍 **Pokročilé vyhledávání a filtrování**
- 👥 **Systém půjčování s evidencí půjčujících**
- 📊 **Analytický dashboard se statistikami**
- 🖼️ **Obrázky obálek s upload funkcionalitou**
- 📱 **QR kódy pro rychlé vyhledání položek**
- 🌙 **Dark mode pro lepší UX**
- 📱 **Responzivní design pro všechna zařízení**

---

## 🛠️ Technologický stack

- **Backend:** ASP.NET Core 8 MVC
- **Databáze:** SQL Server LocalDB / Entity Framework Core
- **Frontend:** HTML5, CSS3, JavaScript, Bootstrap 5

- **Ikony:** Font Awesome 6.4.0  
- **Grafy:** Chart.js  
- **Architektura:** MVC Pattern, Repository Pattern

---

## 🚀 Rychlý start

### Požadavky
- Visual Studio 2022 nebo Visual Studio Code
- .NET 8.0 SDK
- SQL Server LocalDB (součást Visual Studio)

### Instalace

Klonování repositáře:

git clone https://github.com/amonvo/BookCatalogApp.git
cd BookCatalogApp
Obnovení NuGet balíčků:

dotnet restore
Vytvoření databáze:

dotnet ef database update
Spuštění aplikace:

dotnet run
Aplikace bude dostupná na https://localhost:7xxx a http://localhost:5xxx

## 📋 Funkcionalita podle modulů

### 🏠 Dashboard
- **Přehledové statistiky** – celkový počet položek podle typů
- **Grafické analýzy** – půjčené vs. dostupné položky
- **Rychlé akce** – přidání položky, filtrování
- **Real-time data** – aktuální stav evidence

### 📚 Správa položek
- **CRUD operace** – vytváření, čtení, úprava, mazání
- **Validace dat** – povinná pole, formáty
- **Batch operace** – hromadné akce

- **Export/Import** – CSV, Excel formáty

### 🔍 Vyhledávání a filtrování
- **Fulltextové vyhledávání** – název, autor, popis
- **Filtry podle typu** – knihy, CD, DVD
- **Stav půjčení** – dostupné, půjčené
- **Pokročilé filtry** – datum pořízení, kategorie

### 👥 Systém půjčování
- **Evidence půjčujících** – jméno, kontakt
- **Termíny vrácení** – sledování prodlení
- **Historie půjčování** – kompletní audit trail
- **Notifikace** – připomínky vrácení


### 🖼️ Vizuální obsah
- **Upload obrázků** – drag & drop, file browser
- **URL obrázků** – externí odkazy
- **Automatická optimalizace** – komprese, resize
- **Lightbox prohlížení** – zvětšení obrázků

### 📱 QR kódy
- **Automatické generování** – unikátní kódy pro každou položku

- **Mobilní skenování** – rychlé vyhledání
- **Offline funkčnost** – lokální generování
- **Export QR kódů** – tisk, sdílení


## 🏗️ Architektura aplikace

### Struktura projektu

BookCatalogApp/
├── Controllers/ # MVC Controllers
│ ├── ItemsController.cs
│ ├── DashboardController.cs
│ └── HomeController.cs
├── Models/ # Data modely
│ ├── Entities/ # Databázové entity
│ └── ViewModels/ # View modely
├── Views/ # Razor Views
│ ├── Items/
│ ├── Dashboard/
│ └── Shared/
├── Data/ # Databázový kontext
├── Services/ # Business logic
├── wwwroot/ # Statické soubory
│ ├── css/
│ ├── js/
│ ├── uploads/ # Nahrané obrázky
│ └── images/
└── Migrations/ # EF Core migrace

Databázový model
sql
-- MediaTypes (typy médií)
Id, Name, IconClass, ColorClass

-- Items (položky)
Id, Title, Author, MediaTypeId, PurchaseDate, 
Description, CoverImageUrl, CoverImageFileName, 
QRCode, BorrowerName, BorrowDate, CreatedAt

## 🎨 Uživatelské rozhraní

### Design principy
- **Material Design** inspirované komponenty
- **Konzistentní ikonografie** – Font Awesome
- **Barevné kódování** – typy médií
- **Responsivní grid** – Bootstrap 5
- **Přístupnost** – ARIA labels, klávesová navigace

### Klávesové zkratky
### Klávesové zkratky
- `Ctrl + D` – Dashboard
- `Ctrl + L` – Seznam položek
- `Ctrl + N` – Nová položka
- `Ctrl + F` – Vyhledávání
- `Ctrl + M` – Dark mode
- `?` – Nápověda


🔧 Konfigurace
Databázové připojení
json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BookCatalogDb;Trusted_Connection=true"
  }
}


### Upload nastavení
- **Maximální velikost:** 5 MB na soubor
- **Podporované formáty:** JPG, PNG, GIF, WebP
- **Upload složka:** `wwwroot/uploads/covers/`

### QR kód konfigurace
- **Velikost:** 200x200 px
- **Formát:** PNG
- **Generátor:** QR Server API
- **Data:** `BookCatalogApp-Item-{ID}|{Title}|{Author}|{MediaType}`


## 📊 Testovací data

Aplikace obsahuje předpřipravená testovací data:

### 📚 Knihy (3 položky)
- Hobit – J.R.R. Tolkien
- 1984 – George Orwell
- Malý princ – Antoine de Saint-Exupéry *(půjčeno)*

### 💿 CD (3 položky)
- The Wall – Pink Floyd
- Thriller – Michael Jackson *(půjčeno)*
- Bohemian Rhapsody – Queen

### 📀 DVD (4 položky)
- Forrest Gump – Robert Zemeckis
- Matrix – Lana a Lilly Wachowski
- Titanic – James Cameron *(půjčeno)*
- Pán prstenů – Peter Jackson

## 🧪 Testování

### Manuální testování
- **Funkční testy** – všechny CRUD operace
- **UI testy** – responzivní design
- **Integrace** – databáze, upload soubory
- **Bezpečnost** – validace, XSS ochrana

### Testovací scénáře
- Přidání nové knihy s obrázkem
- Půjčení a vrácení položky
- Vyhledávání podle různých kritérií
- Generování a skenování QR kódu
- Export dat a statistik


## 🚨 Známé limitace

- **Offline režim** – aplikace vyžaduje internetové připojení pro QR kódy
- **Concurrent users** – není optimalizována pro vysoký počet současných uživatelů
- **File storage** – lokální úložiště, není vhodné pro distribuované prostředí
- **Authentication** – momentálně bez uživatelského systému


## 🔮 Budoucí vylepšení

**Verze 2.0 – plánované funkce:**
- Multi-tenant architektura
- REST API pro mobilní aplikace
- Real-time notifikace – SignalR
- Pokročilý reporting – PDF generování
- Cloud storage – Azure Blob Storage
- Authentication – ASP.NET Identity
- Internationalization – více jazyků

---

## 👨‍💻 Autor

- **Jméno:** Vojtěch Amon, DiS.
- **Email:** vojtechamon@gmail.com
- **GitHub:** [@amonvo](https://github.com/amonvo)
