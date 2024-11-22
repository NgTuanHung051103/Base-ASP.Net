namespace PokemonReviewApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration, builder.Environment);
            
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            startup.Configure(app);

            ServiceDependencies.Configure(app.Services);
            app.UseHttpsRedirection();
            app.Run();
        }
    }
}