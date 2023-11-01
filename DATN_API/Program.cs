using System.Text;
using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_API.Service_IService.Services;
using DATN_Shared.Models;
using DATN_Shared.ViewModel.Momo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme ({bearer token})",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<ISignUpServices, SignUpServices>();
builder.Services.AddScoped<ILoginServices, LoginServices>();

builder.Services.AddScoped<IAddressShipService, AddressShipService>();
builder.Services.AddScoped<IBillItemService, BillItemService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<ICartItemsService, CartItemsService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IFormulaService, FormulaService>();
builder.Services.AddScoped<IHistoryConsumerPointService, HistoryConsumerPointService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddTransient<IProductItemServices, ProductItemServices>();
builder.Services.AddTransient<IProductsServices, ProductServices>();
builder.Services.AddTransient<IPromotionServices, PromotionServices>();
builder.Services.AddTransient<IPromotionItemServices, PromotionItemServices>();
builder.Services.AddTransient<IReviewsService, ReviewsService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IPaymentMethodServices, PaymentMethodServices>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCS"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,  
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins().AllowAnyMethod().AllowAnyHeader();
    }));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
	options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
