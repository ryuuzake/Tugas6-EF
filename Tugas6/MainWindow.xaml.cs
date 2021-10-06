using System.Windows;

namespace Tugas6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GoToTodoPage();
        }

        private void GoToTodoPage()
        {
            var db = new Model.ApplicationDbContext();
            mainFrame.Navigate(new TodoPage(db));
        }
    }
}
