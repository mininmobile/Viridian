using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace viridian {
	public partial class MainWindow : Window {
		public UtilCanvas c;

		public MainWindow() {
			InitializeComponent();
			c = new UtilCanvas(DisplayCanvas);

			c.DrawText("despacito");
		}
	}

	public class UtilCanvas {
		Canvas target;
	
		public UtilCanvas(Canvas canvas) {
			target = canvas;
		}

		public void DrawText(string text, double x = 0, double y = 0, double size = 16, Color color = default(Color)) {
			color = color == default(Color) ? Color.FromRgb(0, 0, 0) : color;

			TextBlock textBlock = new TextBlock();

			// set content
			textBlock.Text = text;

			// set appearance
			textBlock.FontSize = size;
			textBlock.Foreground = new SolidColorBrush(color);

			// set position
			Canvas.SetLeft(textBlock, x);
			Canvas.SetTop(textBlock, y);

			target.Children.Add(textBlock);
		}
	}
}
