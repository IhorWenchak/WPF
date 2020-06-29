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
	/// Логика взаимодействия для PageResult.xaml
	/// </summary>
	public partial class PageResult : Page
	{
		Path myPath = new Path();
		Rect myRectangle = new Rect();
		RectangleGeometry myRectangleGeometry = new RectangleGeometry();

		public PageResult(double containerWidth, double containerHeight, List<Node> ResBoxes)
		{
			InitializeComponent();

			myRectangle.Location = new Point(0, 0);
			myRectangle.Size = new Size(containerWidth, containerHeight);
			
			myRectangleGeometry.Rect = myRectangle;
			
			myPath.Fill = Brushes.LimeGreen;
			myPath.Stroke = Brushes.Black;
			myPath.StrokeThickness = 1;
			myPath.Data = myRectangleGeometry;

			myGrid.Children.Add(myPath);

			foreach(var box in ResBoxes)
			{
				Path myPath = new Path();
				Rect myRectangle = new Rect();
				RectangleGeometry myRectangleGeometry = new RectangleGeometry();

				myRectangle.Location = new Point(box.Pos_x, box.Pos_y);
				myRectangle.Size = new Size(box.Width, box.Height);

				myRectangleGeometry.Rect = myRectangle;

				myPath.Fill = Brushes.DarkCyan;
				myPath.Stroke = Brushes.Black;
				myPath.StrokeThickness = 1;
				myPath.Data = myRectangleGeometry;

				myGrid.Children.Add(myPath);

			}
		}
	}
}
