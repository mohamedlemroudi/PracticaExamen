using JocVaixells.Events;
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

namespace JocVaixells
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class TableControl : UserControl
    {
        #region Private Variables

        private int _rows = 2; // Grid default rowsCount
        private int _cols = 2; // Grid default colsCount

        #endregion

        #region Constructors

        public TableControl()
        {
            InitializeComponent();
            InitTable();
        }

        #endregion

        #region Public events

        public delegate void SelectUIElementHandler(object sender, SelectUIElementEventArgs e);

        // Register SelectItemClickEvent
        public static readonly RoutedEvent SelectItemClickEvent = EventManager.RegisterRoutedEvent("SelectItemClick", RoutingStrategy.Bubble, typeof(SelectUIElementHandler), typeof(TableControl));

        // Register SelectItemRightClickEvent
        public static readonly RoutedEvent SelectItemRightClickEvent = EventManager.RegisterRoutedEvent("SelectItemRightClick", RoutingStrategy.Bubble, typeof(SelectUIElementHandler), typeof(TableControl));

        // Provide CLR accessors for assigning an event handler
        public event SelectUIElementHandler SelectItemClick
        {
            add { AddHandler(SelectItemClickEvent, value); }
            remove { RemoveHandler(SelectItemClickEvent, value); }
        }

        // Provide CLR accessors for assigning an event handler
        public event SelectUIElementHandler SelectItemRightClick
        {
            add { AddHandler(SelectItemRightClickEvent, value); }
            remove { RemoveHandler(SelectItemRightClickEvent, value); }
        }

        #endregion

        #region Public Properties

        // Rows Property
        // Gets or sets grid of rows in grid
        public virtual int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                if (value < 1)
                {
                    var ipe = new Exceptions.InvalidPositionException("Incorrect number of rows");
                    ipe.Min = 0;
                    ipe.Max = int.MaxValue - 1;

                    throw ipe;
                }

                _rows = value;
                InitTable();
            }
        }

        // Cols property
        // Gets or sets number of cols in grid
        public virtual int Cols
        {
            get
            {
                return _cols;
            }
            set
            {
                if (value < 1)
                {
                    var ipe = new Exceptions.InvalidPositionException("Incorrect number of columns");
                    ipe.Min = 0;
                    ipe.Max = int.MaxValue - 1;

                    throw ipe;
                }

                _cols = value;
                InitTable();
            }
        }
        #endregion

        #region Public Methods

        public void SetContent(int row, int col, string text)
        {
            RowColumnValidOrException(row, col);

            var obj = this.mainGrid.Children.Cast<MatrixCell>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
            obj.Text = text;
        }

        public void SetContentValor(int row, int col, string text, bool valor)
        {
            RowColumnValidOrException(row, col);

            var obj = this.mainGrid.Children.Cast<MatrixCell>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
            obj.Text = text;
            obj.Valor = valor;
        }

        public void SetCellProperties(int row, int col, MatrixCellProperties props)
        {
            RowColumnValidOrException(row, col);

            var obj = this.mainGrid.Children.Cast<MatrixCell>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
            obj.Properties = props;
        }

        public string? GetContent(int row, int col)
        {
            string content = string.Empty;

            RowColumnValidOrException(row, col);

            var obj = this.mainGrid.Children.Cast<MatrixCell>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);

            if (obj.Text != null)
                return obj.Text.ToString();
            else
                return content;
        }

        #endregion

        #region Private properties

        public static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;
            else
            {
                T? parent = parentObject as T;
                if (parent != null)
                    return parent;
                else
                    return FindParent<T>(parentObject);
            }
        }

        // Init and redraw de table/matrix
        protected void InitTable()
        {
            // Clear any existing definition or children
            mainGrid.ColumnDefinitions.Clear();
            mainGrid.RowDefinitions.Clear();
            mainGrid.Children.Clear();

            // Define columns 
            for (int c = 0; c < Cols; c++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Define rows
            for (int r = 0; r < Rows; r++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
            }

            // Draw grid 
            for (int c = 0; c < Cols; c++)
            {
                for (int r = 0; r < Rows; r++)
                {

                    MatrixCell m;
                    m = new MatrixCell();

                    Grid.SetColumn(m, c);
                    Grid.SetRow(m, r);
                    mainGrid.Children.Add(m);

                }
            }
        }

        public Cell celdaTocat()
        {
            Random generator = new Random();
            int numFinal = this.Rows * this.Cols;
            int numeroaleatorio = generator.Next(0, numFinal);
            numeroaleatorio -= 1;
            int fila = numeroaleatorio / 3;
            int columna = fila % 3;

            Cell cell = new Cell(fila, columna);

            return cell;
        }

        #endregion

        /*
         * This function cheks if the row and the col is valid
         * If not valids throws and exception
         */
        private bool RowColumnValidOrException(int row, int col)
        {
            Exceptions.InvalidPositionException ipe;

            if ((row < 0) || (row >= Rows))
            {
                ipe = new Exceptions.InvalidPositionException("Incorrect row");
                ipe.Min = 0;
                ipe.Max = this.Rows - 1; ;

                throw ipe;
            }

            if ((col < 0) || (col >= Cols))
            {
                ipe = new Exceptions.InvalidPositionException("Incorrect column");
                ipe.Min = 0;
                ipe.Max = this.Cols - 1; ;

                throw ipe;
            }

            return true;
        }

        private void mainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MatrixCell? mc;
            SelectUIElementEventArgs args = new SelectUIElementEventArgs(SelectItemClickEvent);

            // Assign properties to the event
            mc = FindParent<MatrixCell>((DependencyObject)e.Source);
            if (mc != null && mc.Properties.Enabled)
            {
                args.Row = Grid.GetRow(mc);
                args.Col = Grid.GetColumn(mc);
                args.Cell = mc;

                // Raise the event
                RaiseEvent(args);
            }
        }

        private void mainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MatrixCell? mc;
            SelectUIElementEventArgs args = new SelectUIElementEventArgs(SelectItemRightClickEvent);

            // Assign properties to the event
            mc = FindParent<MatrixCell>((DependencyObject)e.Source);
            args.Row = Grid.GetRow(mc);
            args.Col = Grid.GetColumn(mc);
            args.Cell = mc;

            // Raise the event
            RaiseEvent(args);
        }
    }
}
