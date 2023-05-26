
using MyStoreApi;

var builder = WebApplication.CreateBuilder(args);
var app = ContainerConfiguration.Configure();

app.MapControllers();

app.Run();