using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace JocVaixells
{
    public class MatrixCellProperties
    {
        private Border border;

        public MatrixCellProperties()
        {
            border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.Background = Brushes.Gray;
        }

        public bool Enabled { get; set; } = true;

        public bool ShowImage { get; set; } = false; // By default don't show any image

        public Brush Background { get; set; } = Brushes.Black;
        public Border Border
        {

            get
            {
                return border;
            }
            set
            {
                border = value;
            }
        }
   }
}