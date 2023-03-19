using OurFuss.Extensions.Configurations;

//Получаем сборщик
var builder = WebApplication.CreateBuilder(args);

//Подрубаем бд
builder.Services.AddDbContext(builder.Configuration);

//Подрубаем контроллеры
builder.Services.AddControllers();

//Подрубаем зависимости для свагера
builder.Services.AddSwaggerGen();

//Проводим сборку подключенных сервисов
var app = builder.Build();

//Подключаем статические файлы
app.UseStaticFiles();

//Подключаем роутинг
app.UseRouting();

//Подключаем дефолтную схему роут-маршрута
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

//Подключаем свагер
app.UseSwagger();
app.UseSwaggerUI();

//Запускаем приложение
app.Run();
