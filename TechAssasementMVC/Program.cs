using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Spi;
using TechAssasementMVC.Database;
using TechAssasementMVC.Job;
using TechAssasementMVC.Models;
using TechAssasementMVC.Repositories;

namespace TechAssasementMVC
{
	public class Program
	{
		public static void Main()
		{
			InitiateWeatherJob();

			var builder = WebApplication.CreateBuilder();

			builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<WeatherContext>(options =>
				options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:Database")));

			var serviceProvider = builder.Services.BuildServiceProvider();
			var dbContext = serviceProvider.GetService<WeatherContext>();

			dbContext.Database.EnsureCreated();
			serviceProvider.Dispose();

			builder.Services.AddHttpClient();

			builder.Services.AddTransient<WeatherRepository>();


			var app = builder.Build();



			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");

				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();

		}

		public static void InitiateWeatherJob()
		{
			Host.CreateDefaultBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					services.AddDbContext<WeatherContext>(options =>
						options.UseSqlServer(hostContext.Configuration.GetValue<string>("ConnectionStrings:Database")));

					var serviceProvider = services.BuildServiceProvider();
					var dbContext = serviceProvider.GetService<WeatherContext>();

					dbContext.Database.EnsureCreated();
					serviceProvider.Dispose();

					services.AddHttpClient();

					services.AddTransient<WeatherRepository>();
					services.AddTransient<WeatherDataJob>();

					services.AddQuartz(quartz =>
					{
						quartz.UseMicrosoftDependencyInjectionJobFactory();

						quartz.AddJob<WeatherDataJob>(options => options.WithIdentity("WeatherDataJob"));

						quartz.AddTrigger(trigger =>
							trigger.ForJob("WeatherDataJob")
								.WithIdentity("WeatherDataJob")
								.WithSimpleSchedule(schedule =>
									schedule.WithIntervalInMinutes(1)
										.RepeatForever()));
					});

					services.AddSingleton<IJobFactory, JobFactory>();

					services.AddQuartzHostedService(options =>
					{
						options.WaitForJobsToComplete = true;
					});
				}).Build().Start();
		}
	}
}