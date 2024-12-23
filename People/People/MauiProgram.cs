﻿namespace People;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// TODO: Add statements for adding PersonRepository as a singleton
		string andresjimenezdbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "andresjimenez_people.db");
		builder.Services.AddSingleton<PersonRepository>(s =>
			ActivatorUtilities.CreateInstance<PersonRepository>(s, andresjimenezdbPath));
		
        return builder.Build();
	}
}
