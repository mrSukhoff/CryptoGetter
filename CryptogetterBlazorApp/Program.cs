using CryptogetterBlazorApp.Components;
using CryptogetterBlazorApp.CryptoGetter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Регистрируем ServerList как singleton
builder.Services.AddSingleton<ServerList>();

// Регистрируем DataMinerFactory как singleton
builder.Services.AddSingleton<DataMinerFactory>();

// Регистрируем IDataMiner как transient, используя первый сервер из списка или сервер по умолчанию
builder.Services.AddTransient<IDataMiner>(serviceProvider =>
{
	var serverList = serviceProvider.GetRequiredService<ServerList>();
	var factory = serviceProvider.GetRequiredService<DataMinerFactory>();

	// Берем первый сервер из списка (или единственный, если файл server.ini пустой или отсутствует)
	var server = serverList.ListOfServers.FirstOrDefault();
	if (server == null)
	{
		throw new InvalidOperationException("Список серверов пуст. Проверьте server.ini или логику ServerList.");
	}

	return DataMinerFactory.GetDataMiner(server);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();