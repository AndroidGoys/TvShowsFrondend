using System.Globalization;

using DamnTv.Api.Client;
using DamnTv.Frontend.Client.Pages.ViewModels;
using DamnTv.Frontend.Components;
using DamnTv.Frontend.PreviewDesign;
using DamnTv.Frontend.PreviewDesign.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    //.AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<MinimalTvApiClient>();
builder.Services.AddTransient<ISharingViewModel, SharingViewModel>();

//��������� ����������� ��� PreviewDistribution
builder.UseSkiaPreviewDesigner();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

//app.MapStaticAssets(); //������� ��� �� �����������
app.UseStaticFiles();

app.MapRazorComponents<App>()
    //.AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DamnTv.Frontend.Client._Imports).Assembly);


app.MapPreviewsDistribution();

app.Run();
