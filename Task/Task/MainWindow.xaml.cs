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
using Task.Models;

namespace Task
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private double height = 0;
		private double width = 0;
		private double heightMain = 0;
		private double widthMain = 0;
		private int quantity = 1;
		private double sumSqr = 0;
		private int caunter = 0;
		Packer packer;


		public MainWindow()
		{
			InitializeComponent();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!double.TryParse(mainHeight.Text, out heightMain))
			{
				MessageBox.Show("The height value will not be allowed!");
				mainHeight.Text = "0";
				heightMain = 0;
			}
			else
			{
				mainHeight.Text = heightMain.ToString();
			}
		}

		private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
		{
			if (!double.TryParse(mainWidth.Text, out widthMain))
			{
				MessageBox.Show("The width value will not be allowed!");
				mainWidth.Text = "0";
				widthMain = 0;
			}
			else
			{
				mainWidth.Text = widthMain.ToString();
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Surface surf = new Surface();
			surf.ContainerHeight = heightMain;
			surf.ContainerWidth = widthMain;

			foreach (var box in MyBoxes)
			{
				sumSqr += (box.Width * box.Height);				
			}

			if (sumSqr > surf.MainSquare)
			{
				MessageBox.Show("The area of the pieces is larger than the surface area !");
				MyBoxes.Clear();

				this.mainHeight.Text = "0";
				this.mainWidth.Text = "0";

				return;
			}

			caunter = MyBoxes.Count();

			for (int i = 0; i < Math.Pow(2, caunter); i++)
			{
				Boxes.Clear();
				string str = Convert.ToString(i, 2).PadLeft(caunter, '0');

				for (int j = 0; j < caunter; j++)
				{
					Boxes.Add(new Surface { Height = MyBoxes[j].Height, Width = MyBoxes[j].Width });

					if (str[j] == '1')
					{
						double width = Boxes[j].Width;
						Boxes[j].Width = Boxes[j].Height;
						Boxes[j].Height = width;						
					}					
				}
				
				packer = new Packer(Boxes, surf.ContainerHeight, surf.ContainerWidth);

				if (packer.ResBoxes.Count() == caunter)
				{
					NavigationWindow win = new NavigationWindow();
					win.Content = new PageResult(surf.ContainerWidth, surf.ContainerHeight, packer.ResBoxes);
					win.Show();
				}
			}
			
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			for (int count = 1; count <= quantity; count++)
			{
				if (((height > heightMain) && (height > widthMain)) || ((width > heightMain) && (width > widthMain)))
				{
					string large = height > width ? height.ToString() : width.ToString();
					MessageBox.Show(String.Format("One side of the rectangle == {0} is too large !", large));

					this.heightField.Text = "0";
					this.widthField.Text = "0";
					this.quantityField.Text = "1";
					break;
				}

				Surface rectangle = new Surface();
				rectangle.Height = height;
				rectangle.Width = width;
				MyBoxes.Add(rectangle);
			}

			this.heightField.Text = "0";
			this.widthField.Text = "0";
			this.quantityField.Text = "1";

		}

		private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
		{
			if ((!int.TryParse(quantityField.Text, out quantity)))
			{
				MessageBox.Show("The quantity value will not be allowed!");
				quantityField.Text = "1";
				quantity = 1;
			}
			else
			{
				quantityField.Text = quantity.ToString();
			}

		}

		private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
		{
			if ((!double.TryParse(heightField.Text, out height)))
			{
				MessageBox.Show("The height value will not be allowed!");
				heightField.Text = "0";
				height = 0;
			}
			else
			{
				heightField.Text = height.ToString();
			}
		}

		private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
		{
			if ((!double.TryParse(widthField.Text, out width)))
			{
				MessageBox.Show("The width value will not be allowed!");
				widthField.Text = "0";
				width = 0;
			}
			else
			{
				widthField.Text = width.ToString();
			}
		}

		public List<Surface> MyBoxes { get; set; } = new List<Surface>();
		public List<Surface> Boxes { get; set; } = new List<Surface>();		
	}
}
