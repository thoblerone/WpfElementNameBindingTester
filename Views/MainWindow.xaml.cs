using System.Diagnostics;
using System.Windows.Input;
using Catel.Windows;

namespace CatelWPFApplication1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : DataWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
            : base(DataWindowMode.Custom)
        {
            InitializeComponent();
            //PersonDataGrid.SelectionChanged += (_, _) => CommandManager.InvalidateRequerySuggested();
        }
    }
}
