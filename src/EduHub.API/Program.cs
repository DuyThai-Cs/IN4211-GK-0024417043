using EduHub.Application.Events;
using EduHub.Application.Notifications;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ── Đăng ký services (Dependency Injection) ──────────────────────────────
// InMemoryEventRepository dùng danh sách static nên dữ liệu vẫn giữ giữa các request dù Scoped.
builder.Services.AddScoped<IEventRepository, InMemoryEventRepository>();
builder.Services.AddScoped<INotificationService, EmailNotificationService>();
builder.Services.AddScoped<INotificationService, SmsNotificationService>();
builder.Services.AddScoped<INotificationServiceFactory, NotificationServiceFactory>();
builder.Services.AddScoped<EventService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EduHub API", Version = "v1" });
});

var app = builder.Build();

// ── Middleware pipeline ───────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
