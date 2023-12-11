using Booking.Shared.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
namespace Booking.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

        /// <summary>
        /// Add Google Application credentials for Admin access. 
        /// This is an environment secret, that should hold the path to the json credentials.
        /// Here it is manually added, however this file should not be exposed.
        ///
        /// Dont use with services.AddFirebaseAuthentication();
        /// </summary>
        string path = AppDomain.CurrentDomain.BaseDirectory + @"Authentication\ServiceAccount.json";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        /// <summary>
        /// Register IConfiguration Appsettings options
        /// </summary>
        services.Configure<AppSettings>(Configuration);
        services.AddScoped(cfg => cfg.GetService<IOptions<AppSettings>>()!.Value);

        /// <summary>
        /// Register CORS -> Allows Client web apps to communicate to this service.
        /// </summary>
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        /// <summary>
        /// Register and configure services
        /// </summary>
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking.Api", Version = "v1" });
        });

        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddAutoMapper(typeof(AccountProfile));

        // Register Firestore configuration
        var firestoreConfig = new FirestoreConfig
        {
            ProjectId = Configuration["Firestore:ProjectId"],
        };

        /// <summary>
        /// Creates singleton FirebaseDb, this allows for 1 connection to the database accross the service lifespan,
        /// you could make this scoped.
        /// </summary>
        services.AddSingleton(sp =>
        {
            string projectId = firestoreConfig.ProjectId;
            FirestoreDb db = FirestoreDb.Create(firestoreConfig.ProjectId);
            return db;
        });

        services.AddScoped<IRepository<BookingEntity>, BookingRepository>();
        services.AddScoped<IRepository<AccountEntity>, AccountRepository<AccountEntity>>();
        services.AddScoped<IDynamicRepository<DynamicEntity>, DynamicRepository>();

        /// <summary>
        /// Add Firebase authentication, incase this service is not running as admin mode and you need to verify Tokens
        /// </summary>
        //services.AddFirebaseAuthentication();

        /// <summary>
        /// Register health checks if you want
        /// </summary>
        services.AddHealthChecks();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionHandleMiddleware>();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking.Api v1"));
            app.UseCors("AllowAllPolicy");
        }
        else
        {
            app.UseHsts();
        }

        app.UseRouting();

        /// <summary>
        /// Enables Authorization checking,
        /// if services.AddFirebaseAuthentication() was added
        /// </summary>
        // app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health")
                     .WithMetadata(new AllowAnonymousAttribute());
        });
    }
}
