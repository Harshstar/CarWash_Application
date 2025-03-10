using carwash.Data;
using carwash.Mappings;
using carwash.Models.Domain;
using carwash.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using carwash.Middlewares;
using Stripe;
using Backend.Models.Domain;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// logger
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/CarWash_Log.txt", rollingInterval : RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddCors(options=>
    {
        options.AddPolicy("corspolicy",policy=>{
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "Car Wash Api" , Version = "v1"});
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme 
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
 


builder.Services.AddDbContext<CarwashDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AuthDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultAuthConnection")));


builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<ICar, CarRepository>();
builder.Services.AddScoped<IPackage, PackageRepository>();
builder.Services.AddScoped<IAddress , AddressRepository>();
builder.Services.AddScoped<IPromoCode , PromoCodeRepository>();
builder.Services.AddScoped<IWashRequest , WashRequestRepository>();
builder.Services.AddScoped<IRatingReview , RatingReviewRepository>();
builder.Services.AddScoped<IPayment , PaymentRepository>();
builder.Services.AddScoped<ITokenRepository , TokenRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


builder.Services.AddIdentityCore<IdentityUser>()
       .AddRoles<IdentityRole>()
       .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CarWash")
       .AddEntityFrameworkStores<AuthDbContext>()
       .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options => 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();