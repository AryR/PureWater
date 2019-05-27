using AppMobile.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public ConsumptionTabPage(bool daily)
		{
			InitializeComponent();
			BindingContext = ViewModel = new ConsumptionModel(daily);
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
				Shader = SKShader.CreateSweepGradient(new SKPoint(rect.MidX, rect.MidY), new SKColor[] { SKColors.Blue, SKColors.Red }, null, SKShaderTileMode.Repeat, 0, 270)
			};

			using (SKPath path = new SKPath())
			{
				path.AddArc(rect, 0, 270);
				canvas.RotateDegrees(135, rect.MidX, rect.MidY);
				canvas.DrawPath(path, paint);
			}
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
				Color = Color.Red.ToSKColor()
			};

			canvas.DrawRect(0,0, info.Width * tapPercentage, info.Height, paint);

			paint.Color = SKColors.Blue;
			canvas.DrawRect(info.Width * tapPercentage, 0, info.Width * (1 - tapPercentage), args.Info.Height, paint);

			SKPaint textPaint = new SKPaint
			{
				Color = SKColors.Black
			};
			textPaint.TextSize = 0.4f * info.Height;

			canvas.DrawText(ViewModel.TapConsumption + " lts", 0, args.Info.Height * 0.5f, textPaint);
			canvas.DrawText(ViewModel.TreatedConsumption + " lts", info.Width * tapPercentage, args.Info.Height * 0.5f, textPaint);

			textPaint.TextSize = 0.2f * info.Height;

			canvas.DrawText(tapPercentage * 100 + " %", 0, args.Info.Height * 0.75f, textPaint);
			canvas.DrawText((1 - tapPercentage) * 100 + " %", info.Width * tapPercentage, args.Info.Height * 0.75f, textPaint);
		}

		private void History_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new HistoryPage(Daily));
		}
	}
}