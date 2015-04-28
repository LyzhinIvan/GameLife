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
using System.Drawing;
using System.Timers;
using System.Windows.Threading;

namespace GameLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Rows = 20, Columns = 20;
        private LifeBoard _lifeBoard;
        private Border[,] _borders;
        private Timer timer;
        private Dispatcher dispatcher;

        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Elapsed += TimerOnElapsed;
            timer.Interval = 1000;
            for(int i=0; i<Rows; ++i)
                BoardGrid.RowDefinitions.Add(new RowDefinition());
            for(int i=0; i<Columns; ++i)
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            _borders = new Border[Rows,Columns];
            BoardGrid.Width = Rows*20;
            BoardGrid.Height = Rows*20;
            for(int i=0; i<Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                {
                    Border border = new Border();
                    border.Width = 20;
                    border.Height = 20;
                    border.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    border.BorderThickness = new Thickness(1);
                    border.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    border.MouseLeftButtonDown += BorderOnMouseLeftButtonDown;
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    BoardGrid.Children.Add(border);
                    _borders[i, j] = border;
                }
            _lifeBoard = new LifeBoard(Rows, Columns);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            //timer.Enabled = false;
            _lifeBoard.GoToNext();
            //RefreshBoard();
            Dispatcher.Invoke(RefreshBoard);
        }

        private void BorderOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            Border border = (Border) sender;
            int x = Grid.GetRow(border);
            int y = Grid.GetColumn(border);
            _lifeBoard.Board[x, y] = !_lifeBoard.Board[x, y];
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            try
            {
                foreach (var obj in BoardGrid.Children)
                {
                    Border b = obj as Border;
                    int i = Grid.GetRow(b);
                    int j = Grid.GetColumn(b);
                    if (_lifeBoard.Board[i, j])
                        b.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    else b.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnStart_OnClick(object sender, RoutedEventArgs e)
        {
            timer.Enabled = true;
        }

        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            timer.Enabled = false;
        }
    }
}
