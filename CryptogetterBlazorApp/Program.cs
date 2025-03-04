using CryptogetterBlazorApp.Components;
using CryptogetterBlazorApp.CryptoGetter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// ������������ ServerList ��� singleton
builder.Services.AddSingleton<ServerList>();

// ������������ DataMinerFactory ��� singleton
builder.Services.AddSingleton<DataMinerFactory>();

// ������������ IDataMiner ��� transient, ��������� ������ ������ �� ������ ��� ������ �� ���������
builder.Services.AddTransient<IDataMiner>(serviceProvider =>
{
	var serverList = serviceProvider.GetRequiredService<ServerList>();
	var factory = serviceProvider.GetRequiredService<DataMinerFactory>();

	// ����� ������ ������ �� ������ (��� ������������, ���� ���� server.ini ������ ��� �����������)
	var server = serverList.ListOfServers.FirstOrDefault();
	if (server == null)
	{
		throw new InvalidOperationException("������ �������� ����. ��������� server.ini ��� ������ ServerList.");
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