using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using CrudDynamoDB.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var accessKey = builder.Configuration["AWS:AccessKey"] ?? "Oscar.Arias";
    var secretKey = builder.Configuration["AWS:SecretKey"] ?? "423623826415";

    var dynamoConfig = new AmazonDynamoDBConfig
    {
        RegionEndpoint = Amazon.RegionEndpoint.SAEast1
    };

    return new AmazonDynamoDBClient(accessKey, secretKey, dynamoConfig);
});

builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configuração do pipeline HTTP

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
