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
		public Document d;
		public CSS css;

		int top = 0;
		int scroll = 0;

		public MainWindow() {
			InitializeComponent();
			c = new UtilCanvas(DisplayCanvas);
			d = new Document();
			css = new CSS();

			// generate example document
			{
				{ // head
					ElementNode title = Document.NewNode("title");
					title.AddChild(Document.NewTextNode("my webpage"));
					d.head.AddChild(title);
				}

				{ //body
					ElementNode title1 = Document.NewNode("h1");
					title1.AddAttribute("id", "title");
					title1.AddChild(Document.NewTextNode("welcome"));
					d.body.AddChild(title1);

					ElementNode paragraph1 = Document.NewNode("p");
					paragraph1.AddAttribute("class", "text");
					paragraph1.AddChild(Document.NewTextNode("welcome to my website"));
					d.body.AddChild(paragraph1);

					ElementNode div = Document.NewNode("div");
					div.AddAttribute("id", "content");
					div.AddAttribute("class", "container wrapper");
					d.body.AddChild(div);

					ElementNode title2 = Document.NewNode("h2");
					title2.AddChild(Document.NewTextNode("a title in a div"));
					div.AddChild(title2);

					ElementNode paragraph2 = Document.NewNode("p");
					paragraph2.AddAttribute("class", "text");
					paragraph2.AddChild(Document.NewTextNode("a paragraph in a div"));
					div.AddChild(paragraph2);
				}
			}

			DrawElementTree(d.root);
		}

		public void DrawElementTree(ElementNode node) {
			foreach (Node child in node.Children) {
				if (child.GetType() == typeof(ElementNode)) {
					DrawElementTree((ElementNode)child);
				} else if (child.GetType() == typeof(TextNode)) {
					Dictionary<string, CSSValue> properties = css.GetStyleForNode(child.Parent);

					if (((CSSKeyword)properties["display"]).Keyword != Keyword.none) {
						c.DrawText(
							((TextNode)child).Text,
							0,
							top,
							((CSSLength)properties["font-size"]).Pixels,
							((CSSRgb)properties["color"]).ToColor()
						);

						top += ((CSSLength)properties["font-size"]).Pixels;
					}
				}
			}
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
