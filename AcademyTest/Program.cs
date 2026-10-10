// Подключаем корневые компоненты интерфейса нашего Blazor-приложения (например, класс App)
using AcademyTest.Components;
// Подключаем методы расширения ядра Entity Framework Core (нужен для метода .UseSqlServer)
using Microsoft.EntityFrameworkCore;
// Подключаем наши созданные вручную C#-классы моделей и контекста БД
using AcademyTest.Models;

// Создаем объект builder (строитель), который собирает конфигурацию, логирование и контейнер DI
var builder = WebApplication.CreateBuilder(args);

// ===================================================================================
// ЧАСТЬ 1: НАСТРОЙКА СЕРВИСОВ ПРИЛОЖЕНИЯ (DI CONTAINER) — РЕГИСТРИРУЕМ ТО, ЧТО НАМ НУЖНО
// ===================================================================================

// Добавляем поддержку Razor-компонентов (базовый движок рендеринга страниц .razor)
builder.Services.AddRazorComponents()
    // Включаем поддержку интерактивного серверного режима (рендеринг через постоянное WebSocket-соединение SignalR)
    .AddInteractiveServerComponents();

// Вытаскиваем текстовое значение адреса нашей БД из appsettings.json по ключу "MiniUniversityConnection"
var connectionString = builder.Configuration.GetConnectionString("MiniUniversityConnection");

// Регистрируем фабрику контекстов. Blazor Server многопоточен, поэтому под каждый запрос страницы 
// фабрика будет налету выдавать изолированный, чистый экземпляр MiniUniversityDbContext
builder.Services.AddDbContextFactory<MiniUniversityDbContext>(options =>
    // Указываем, что в качестве СУБД мы используем MS SQL Server и передаем строку подключения к нему
    options.UseSqlServer(connectionString));

// Скомандовали билдеру скомпилировать все настройки. Объект `app` — это наше готовое запущенное веб-приложение
var app = builder.Build();

// ===================================================================================
// ЧАСТЬ 2: НАСТРОЙКА КОНВЕЙЕРА MIDDLEWARE — ПРАВИЛА ПРОХОЖДЕНИЯ СЕТЕВЫХ ЗАПРОСОВ (PIPELINE)
// ===================================================================================

// Проверяем: если сайт запущен НЕ в режиме разработки (то есть опубликован на реальный сервер / Production)
if (!app.Environment.IsDevelopment())
{
    // Включаем глобальный перехватчик критических ошибок. Если код упадет, пользователя перекинет на страницу /Error
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    // Включаем протокол HSTS (браузеры будут принудительно открывать сайт только по зашифрованному HTTPS)
    app.UseHsts();
}

// Перехватывает ошибки ответов (например, 404 Not Found) и незаметно для адреса делает перенаправление на страницу /not-found
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

// Автоматически перенаправляет пользователя с небезопасного адреса http:// на безопасный https://
app.UseHttpsRedirection();

// Подключает систему защиты от подделки межсайтовых запросов (CSRF-атак). Обязательно для интерактивных форм Blazor
app.UseAntiforgery();

// Разрешает Kestrel эффективно отдавать статические оптимизированные ресурсы приложения (CSS, JS, картинки)
app.MapStaticAssets();

// Назначаем главный Razor-компонент <App> в качестве корневой разметки, куда будут встраиваться все наши страницы
app.MapRazorComponents<App>()
    // Указываем, что по умолчанию наше приложение работает в интерактивном режиме InteractiveServer (SignalR)
    .AddInteractiveServerRenderMode();

// Запускает встроенный веб-сервер (Kestrel). Поток выполнения блокируется, сайт начинает слушать входящие порты
app.Run();
