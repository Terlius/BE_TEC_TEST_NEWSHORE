using AutoMapper;
using NewshoreAir.Business.Services;
using NewshoreAir.DataAccess.Repositories;
using NewshoreAir.Business.Models;
using NewshoreAir.API.Models;
using NewshoreAir.DataAccess.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your services and repositories
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

// Register HttpClient and Logging services
builder.Services.AddHttpClient();
builder.Services.AddLogging();

// Configure AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<FlightAPI, Flight>()
        .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.DepartureStation))
        .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.ArrivalStation))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
        .ForMember(dest => dest.Transport, opt => opt.MapFrom(src => src.Transport));

    mc.CreateMap<Journey, JourneyDTO>()
        .ForMember(dest => dest.CurrencyTemporal, opt => opt.MapFrom(src => src.Currency));

    mc.CreateMap<Flight, FlightDTO>();
    mc.CreateMap<Transport, TransportDTO>();
    mc.CreateMap<TransportAPI, Transport>();
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
