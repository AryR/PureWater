﻿using AppMobile.Helpers;
using AppMobile.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConsumptionTabPage : ContentPage
	{
		private ConsumptionModel ViewModel;
		private bool Daily;

		private SKColor MinColor = new SKColor(32, 250, 79);
		private SKColor MaxColor = new SKColor(250, 32, 32);
		private SKColor TapColor = new SKColor(56, 159, 255);
		private SKColor TreatedColor = new SKColor(40, 191, 58);
		private SKColor PointColor = new SKColor(255, 226, 5);

		public ConsumptionTabPage(bool daily)
		{
			InitializeComponent();
			Daily = daily;
			BindingContext = ViewModel = new ConsumptionModel(daily);

			Device.StartTimer(TimeSpan.FromSeconds(5), () =>
			{
				Device.BeginInvokeOnMainThread(() => RefreshViewModel());
				return true;
			});
		}

		private void RefreshViewModel()
		{
			ViewModel.UpdateModel();
			PrincipalCanvas.InvalidateSurface();
			SecondaryCanvas.InvalidateSurface();
		}

		void OnConsumptioCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
		{
			SKImageInfo info = args.Info;
			SKSurface surface = args.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			int size = Math.Min(info.Width-50, info.Height);
			SKRect rect = new SKRect(50, 30, size, size);
			
			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = 50,
				Shader = SKShader.CreateSweepGradient(new SKPoint(rect.MidX, rect.MidY), new SKColor[] { MinColor, MaxColor }, null, SKShaderTileMode.Repeat, 0, 270)
			};

			using (SKPath path = new SKPath())
			{
				path.AddArc(rect, 0, 270);

				double consumptionPercentage = ((ViewModel.TotalConsumption - ViewModel.MinConsumption) * 100) / (ViewModel.MaxConsumption - ViewModel.MinConsumption);
				double degree = consumptionPercentage * 270 / 100;


				var angle = Math.PI * (degree) / 180.0;

				//calculate the radius and the center point of the circle
				var radius = (rect.Right - rect.Left) / 2;
				var middlePoint = new SKPoint();
				middlePoint.X = (rect.Left + radius);
				middlePoint.Y = rect.Top + radius; //top of current circle plus radius

				SKPaint circlePaint = new SKPaint
				{
					Style = SKPaintStyle.StrokeAndFill,
					StrokeWidth = 10,
					Color = PointColor
				};
				
				canvas.RotateDegrees(135, rect.MidX, rect.MidY);
				canvas.DrawPath(path, paint);
				canvas.DrawCircle(middlePoint.X + (float)(radius * Math.Cos(angle)), middlePoint.Y + (float)(radius * Math.Sin(angle)), 20, circlePaint);
				canvas.RotateDegrees(-135, rect.MidX, rect.MidY);
			}

			string resourceID = "AppMobile.Images.water_icon.png";
			Assembly assembly = GetType().GetTypeInfo().Assembly;
			SKBitmap resourceBitmap;

			using (Stream stream = assembly.GetManifestResourceStream(resourceID))
			{
				resourceBitmap = SKBitmap.Decode(stream);
			}

			if (resourceBitmap != null)
			{
				var resizedBitmap = resourceBitmap.Resize(new SKImageInfo((int)(info.Width * 0.2), (int)(info.Height * 0.2)), SKBitmapResizeMethod.Box);

				canvas.DrawBitmap(resizedBitmap, info.Width * 0.5f - resizedBitmap.Width * 0.5f, info.Height * 0.2f - resizedBitmap.Height * 0.5f);
			}


			string resourceID2 = "AppMobile.Images.happyworld.png";
			SKBitmap resourceBitmap2;

			using (Stream stream = assembly.GetManifestResourceStream(resourceID2))
			{
				resourceBitmap2 = SKBitmap.Decode(stream);
			}

			if (resourceBitmap != null)
			{
				var resizedBitmap = resourceBitmap2.Resize(new SKImageInfo((int)(info.Width * 0.2), (int)(info.Height * 0.2)), SKBitmapResizeMethod.Box);

				canvas.DrawBitmap(resizedBitmap, 0, info.Height - resizedBitmap.Height);
			}

			string resourceID3 = "AppMobile.Images.sadworld.png";
			SKBitmap resourceBitmap3;

			using (Stream stream = assembly.GetManifestResourceStream(resourceID3))
			{
				resourceBitmap3 = SKBitmap.Decode(stream);
			}

			if (resourceBitmap != null)
			{
				var resizedBitmap = resourceBitmap3.Resize(new SKImageInfo((int)(info.Width * 0.2), (int)(info.Height * 0.2)), SKBitmapResizeMethod.Box);

				canvas.DrawBitmap(resizedBitmap, info.Width - resizedBitmap.Width, info.Height - resizedBitmap.Height);
			}

			SKPaint textPaint = new SKPaint
			{
				Color = SKColors.Black,
				TextAlign = SKTextAlign.Center,
				TextSize = 0.1f * rect.Height
			};
			canvas.DrawText("Consumo", rect.Width * 0.5f, rect.Height * 0.5f, textPaint);

			textPaint.TextSize = 0.15f * rect.Height;
			canvas.DrawText(ViewModel.TotalConsumption.ToString("F2") + " lts", rect.Width * 0.5f, rect.Height * 0.7f, textPaint);

			textPaint.TextSize = 0.07f * rect.Height;
			canvas.DrawText("Estado", rect.Width * 0.5f, rect.Height * 0.8f, textPaint);
			textPaint.TextSize = 0.05f * rect.Height;
			canvas.DrawText(ViewModel.PhValue.ToString("F2") + " ph", rect.Width * 0.5f, rect.Height * 0.85f, textPaint);
			canvas.DrawText(ViewModel.TurbidityValue.ToString("F2") + " NTU", rect.Width * 0.5f, rect.Height * 0.9f, textPaint);
		}

		void OnComparasionCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
		{
			SKImageInfo info = args.Info;
			SKSurface surface = args.Surface;
			SKCanvas canvas = surface.Canvas;
			
			canvas.Clear();

			float tapPercentage = (float) (ViewModel.TapConsumption / (ViewModel.TapConsumption + ViewModel.TreatedConsumption));

			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = TapColor
			};

			canvas.DrawRect(0,0, info.Width * tapPercentage, info.Height, paint);

			paint.Color = TreatedColor;
			canvas.DrawRect(info.Width * tapPercentage, 0, info.Width * (1 - tapPercentage), args.Info.Height, paint);

			SKPaint textPaint = new SKPaint
			{
				Color = SKColors.Black
			};
			textPaint.TextSize = 0.4f * info.Height;

			canvas.DrawText(ViewModel.TapConsumption.ToString("F2") + " lts", 0, args.Info.Height * 0.5f, textPaint);
			canvas.DrawText(ViewModel.TreatedConsumption.ToString("F2") + " lts", info.Width * tapPercentage, args.Info.Height * 0.5f, textPaint);

			textPaint.TextSize = 0.2f * info.Height;

			canvas.DrawText(Math.Round(tapPercentage * 100, 2) + " %", 0, args.Info.Height * 0.75f, textPaint);
			canvas.DrawText(Math.Round((1 - tapPercentage) * 100, 2) + " %", info.Width * tapPercentage, args.Info.Height * 0.75f, textPaint);
		}

		private void History_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new HistoryPage(Daily));
		}
	}
}