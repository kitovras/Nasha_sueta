using OurFuss.Extensions.Configurations;

//�������� �������
var builder = WebApplication.CreateBuilder(args);

//��������� ��
builder.Services.AddDbContext(builder.Configuration);

//��������� �����������
builder.Services.AddControllers();

//��������� ����������� ��� �������
builder.Services.AddSwaggerGen();

//�������� ������ ������������ ��������
var app = builder.Build();

//���������� ����������� �����
app.UseStaticFiles();

//���������� �������
app.UseRouting();

//���������� ��������� ����� ����-��������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

//���������� ������
app.UseSwagger();
app.UseSwaggerUI();

//��������� ����������
app.Run();
