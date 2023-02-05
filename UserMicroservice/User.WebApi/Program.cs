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

// 注册log4net日志文件
#region log4net
{
    //Nuget引入：logenet、Microsoft.Extensions.Loggins.Log4Net.AspNetCore
    builder.Logging.AddLog4Net(@"Config/log4net.config");
}
#endregion

#region NLog
{
    //Nuget引入：NLog.Web.AspNetCore
    //builder.Logging.AddNLog(@"Config/NLog.config");
}
#endregion

#region DbEntitys服务注入
builder.Services.AddTransient<DbEntitys>();
#endregion

builder.RegisterSwagger();    // Swagger配置封装的注册
builder.RegisterAutofac();    //业务逻辑层Services服务的注册 
builder.RegisterCors("CORS"); //添加跨域策略的注册

#region 配置文件注入
builder.Services.Configure<MySqlConnOptions>(builder.Configuration.GetSection("MysqlConn"));
builder.Services.Configure<RedisClusterOptions>(builder.Configuration.GetSection("RedisConn"));
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
#endregion


#region Consul Server IOC注册
builder.Services.Configure<ConsulRegisterOptions>(builder.Configuration.GetSection("ConsulRegisterOption"));
builder.Services.Configure<ConsulClientOptions>(builder.Configuration.GetSection("ConsulClientOption"));
builder.Services.AddConsulRegister();
#endregion

//支持授权的
builder.AuthorizationExt();

var app = builder.Build();
app.UseCors("CORS");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Consul注册
app.UseHealthCheckMiddleware("/Health");//心跳请求响应
app.Services.GetService<IConsulRegister>()!.UseConsulRegist().Wait();
#endregion

app.UseHttpsRedirection();
#region 添加鉴权
app.UseAuthentication();
#endregion
app.UseAuthorization();

app.MapControllers();

app.Run();
