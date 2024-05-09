using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")  // добавление сервисов аутентификации
    .AddJwtBearer();      // подключение аутентификации с помощью jwt-токенов
builder.Services.AddAuthorization();            // добавление сервисов авторизации


var app = builder.Build();


/*
//обработка ошибок
app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    var path = statusCodeContext.HttpContext.Request.Path;

    response.ContentType = "text/html";
    if(response.StatusCode == 401 || response.StatusCode == 403)
    {
        await response.SendFileAsync("wwwroot/401.html");
    }
    else if(response.StatusCode == 404)
    {
        await response.SendFileAsync("wwwroot/404.html");
    }
});
*/

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/account", [Authorize] () => "Hi");

/*
app.UseStatusCodePagesWithRedirects("/error");
*/
/*
app.UseStatusCodePagesWithReExecute("/error");


app.Map("/error", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot/404.html");
});
*/


app.Environment.EnvironmentName = "Production"; // меняем имя окружения

// если приложение не находится в процессе разработки
// перенаправляем по адресу "/error"
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(app => app.Run(async context =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("wwwroot/404.html");
    }));
 };


app.Run(async (context) =>
{
    int a = 5;
    int b = 0;
    int c = a / b;
    await context.Response.WriteAsync($"c = {c}");
});


app.Run();
