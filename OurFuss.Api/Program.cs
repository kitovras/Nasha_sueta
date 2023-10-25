using OurFuss.Api.Infrastructure.Extensions;
using OurFuss.Utils.TelegramBot.Services;
using Wheat.Api.Infrastructure.Extensions;

//�������� �������
var builder = WebApplication.CreateBuilder(args);

//���������� ���������������� ������ �� ��������
OptionExtensions.AddOptions(builder);

//��������� ��
builder.Services.AddDbContext(builder.Configuration);

//��������� �����������
builder.Services.AddControllers();

//���������� ������� �� Core
builder.Services.AddServices();

//��������� ����������� ��� �������
builder.Services.AddSwaggerGen();

//���������� Identity
builder.Services.AddIdentity();

//����������� 
builder.Services.AddAuthorization();

//����������� �� ������
builder.Services.AddJwtAuthorize(builder.Configuration);

//���������� �����
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist";
});

//�������� ������ ������������ ��������
var app = builder.Build();

//���������� ����������� �����
app.UseStaticFiles();

//if (IsDevelopment())
//{
//    app.UseSpaStaticFiles();
//}

//���������� ����� �������� � �������
app.UseDeveloperExceptionPage();

//���������� �������
app.UseRouting();

//����������� Identity 
app.UseAuthentication();
app.UseAuthorization();

//���������� ������
app.UseSwagger();
app.UseSwaggerUI();

//���������� ��������� ����� ����-��������
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

//������������� ��������� �������� �� ��������� ������� ����������
await app.Services.Init();

//��������� ����������
app.Run();
