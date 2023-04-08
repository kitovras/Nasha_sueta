using OurFuss.Api.Infrastructure.Extensions;

//�������� �������
var builder = WebApplication.CreateBuilder(args);

//��������� ��
builder.Services.AddDbContext(builder.Configuration);

//��������� �����������
builder.Services.AddControllers();

//���������� ������� �� Core
builder.Services.AddServices();

//��������� ����������� ��� �������
builder.Services.AddSwaggerGen();

//�������� ������ ������������ ��������
var app = builder.Build();

//���������� ����������� �����
app.UseStaticFiles();

//���������� �������
app.UseRouting();

//���������� ������
app.UseSwagger();
app.UseSwaggerUI();

//���������� ��������� ����� ����-��������
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//��������� ����������
app.Run();
