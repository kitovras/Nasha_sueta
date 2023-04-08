using OurFuss.Api.Infrastructure.Extensions;

//Получаем сборщик
var builder = WebApplication.CreateBuilder(args);

//Подрубаем бд
builder.Services.AddDbContext(builder.Configuration);

//Подрубаем контроллеры
builder.Services.AddControllers();

//Подключаем сервисы из Core
builder.Services.AddServices();

//Подрубаем зависимости для свагера
builder.Services.AddSwaggerGen();

//Проводим сборку подключенных сервисов
var app = builder.Build();

//Подключаем статические файлы
app.UseStaticFiles();

//Подключаем роутинг
app.UseRouting();

//Подключаем свагер
app.UseSwagger();
app.UseSwaggerUI();

//Подключаем дефолтную схему роут-маршрута
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//Запускаем приложение
app.Run();
