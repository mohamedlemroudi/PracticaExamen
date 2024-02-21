using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml;

namespace JocVaixells
{
    public class MatrixCell : Grid
    {
        private Label lbl;
        private Image image;
        private Border border;
        private MatrixCellProperties props;
        private bool valor;

        public MatrixCell()
        {
            props = new MatrixCellProperties();
            this.border = new Border();
            this.lbl = new Label();
            this.image = new Image();
            this.image.Source = new BitmapImage(new Uri("Images/none.png", UriKind.Relative));
            this.valor = false;

            this.RepaintObject();

        }

        public bool Valor
        {
            get
            {
                return this.valor;
            }
            set
            {
                this.valor = value;
                this.RepaintObject();
            }
        }

        public MatrixCellProperties Properties
        {
            get
            {
                return this.props;
            }
            set
            {
                this.props = value;
                this.RepaintObject();
            }
        }

        public string? Text
        {
            get
            {
                if (lbl.Content != null)
                    return lbl.Content.ToString();
                else
                    return String.Empty;
            }

            set
            {
                lbl.Content = value;
            }
        }

        public void RepaintObject()
        {
            this.Background = props.Background;
            var str = XamlWriter.Save(props.Border);
            this.border = (Border)XamlReader.Load(XmlReader.Create(new StringReader(str)));

            if (this.lbl.Parent == null)
            {
                this.border.Child = lbl;
            }
            else
            {
                ((Decorator)this.lbl.Parent).Child = null;
                this.border.Child = lbl;
            }

            this.Children.Add(this.border);
        }
    }
}
