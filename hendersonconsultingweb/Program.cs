using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// builder.Services.AddHsts(options =>
// {
//     options.Preload = true;
//     options.IncludeSubDomains = true;
//     options.MaxAge = TimeSpan.FromDays(60);
//     options.ExcludedHosts.Add("hendersonculsting.dev");
//     options.ExcludedHosts.Add("www.hendersonculsting.dev");
// });

// builder.Services.AddHttpsRedirection(options =>
// {
//     options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
//     options.HttpsPort = 5001;
// });

// if (!builder.Environment.IsDevelopment())
// {
//     builder.Services.AddHttpsRedirection(options =>
//     {
//         options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
//         options.HttpsPort = 443;
//     });
// }


var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
