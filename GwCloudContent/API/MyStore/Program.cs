
using MyStore;

var app = ContainerConfiguration.Configure();

app.MapControllers();

FakeDataGenerator.GenerateFakeData();

app.Run();