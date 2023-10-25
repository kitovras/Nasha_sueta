using OurFuss.Api.Infrastructure.Extensions;
using OurFuss.Utils.TelegramBot.Services;
using Wheat.Api.Infrastructure.Extensions;

//Получаем сборщик
var builder = WebApplication.CreateBuilder(args);

//Подключаем конфигурационные модели из сетингов
OptionExtensions.AddOptions(builder);

//Подрубаем бд
builder.Services.AddDbContext(builder.Configuration);

//Подрубаем контроллеры
builder.Services.AddControllers();

//Подключаем сервисы из Core
builder.Services.AddServices();

//Подрубаем зависимости для свагера
builder.Services.AddSwaggerGen();

//Подключаем Identity
builder.Services.AddIdentity();

//Авторизация 
builder.Services.AddAuthorization();

//Авторизация по токену
builder.Services.AddJwtAuthorize(builder.Configuration);

//Подключаем фронт
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist";
});

//Проводим сборку подключенных сервисов
var app = builder.Build();

//Подключаем статические файлы
app.UseStaticFiles();

//if (IsDevelopment())
//{
//    app.UseSpaStaticFiles();
//}

//Подключаем показ страницы с ошибкой
app.UseDeveloperExceptionPage();

//Подключаем роутинг
app.UseRouting();

//Авторизация Identity 
app.UseAuthentication();
app.UseAuthorization();

//Подключаем свагер
app.UseSwagger();
app.UseSwaggerUI();

//Подключаем дефолтную схему роут-маршрута
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    //if (env.IsDevelopment())
    //{
    //    spa.UseAngularCliServer(npmScript: "start");
    //}
});

//Инициализация некоторых операций до основного запуска приложения
await app.Services.Init();

//Запускаем приложение
app.Run();
