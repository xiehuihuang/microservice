using BrandMicro.WebApi.Register;
using Framework.Common.IOCOptions;
using Framework.Core.ConsulExtend.ServerExtend;
using Framework.Core.ConsulExtend;
using Framework.WebCore.JWTExtend;
using Newtonsoft.Json;
using UserMicro.Model.Models;
using Framework.Core.ConsulExtend.ServerExtend.Register;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ע��log4net��־�ļ�
#region log4net
{
    //Nuget���룺logenet��Microsoft.Extensions.Loggins.Log4Net.AspNetCore
    builder.Logging.AddLog4Net(@"Config/log4net.config");
}
#endregion

#region NLog
{
    //Nuget���룺NLog.Web.AspNetCore
    //builder.Logging.AddNLog(@"Config/NLog.config");
}
#endregion

#region DbEntitys����ע��
builder.Services.AddTransient<DbEntitys>();
#endregion

builder.RegisterSwagger();    // Swagger���÷�װ��ע��
builder.RegisterAutofac();    //ҵ���߼���Services�����ע�� 
builder.RegisterCors("CORS"); //��ӿ�����Ե�ע��

#region �����ļ�ע��
builder.Services.Configure<MySqlConnOptions>(builder.Configuration.GetSection("MysqlConn"));
builder.Services.Configure<RedisClusterOptions>(builder.Configuration.GetSection("RedisConn"));
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
#endregion


#region Consul Server IOCע��
builder.Services.Configure<ConsulRegisterOptions>(builder.Configuration.GetSection("ConsulRegisterOption"));
builder.Services.Configure<ConsulClientOptions>(builder.Configuration.GetSection("ConsulClientOption"));
builder.Services.AddConsulRegister();
#endregion

//֧����Ȩ��
builder.AuthorizationExt();

var app = builder.Build();
app.UseCors("CORS");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Consulע��
app.UseHealthCheckMiddleware("/Health");//����������Ӧ
app.Services.GetService<IConsulRegister>()!.UseConsulRegist().Wait();
#endregion

app.UseHttpsRedirection();
#region ��Ӽ�Ȩ
app.UseAuthentication();
#endregion
app.UseAuthorization();

app.MapControllers();

app.Run();
