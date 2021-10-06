using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tugas6.Model;

namespace Tugas6
{
    /// <summary>
    /// Interaction logic for TodoPage.xaml
    /// </summary>
    public partial class TodoPage : Page
    {
        private ApplicationDbContext ApplicationDbContext;
        private int TodoId;
        private string TodoTitle;
        private string TodoBody;

        public TodoPage(ApplicationDbContext applicationDbContext)
        {
            InitializeComponent();
            ApplicationDbContext = applicationDbContext;
            RefreshData();
        }

        private void TodoIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            try
            {
                TodoId = int.Parse(textBox.Text);
            }
            catch (Exception)
            {
                TodoId = 1;
            }
        }

        private void TodoTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            TodoTitle = textBox.Text;
        }

        private void TodoBodyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            TodoBody = textBox.Text;
        }

        private void CreateTodo_Click(object sender, RoutedEventArgs e)
        {
            var todo = new Todo() { Id = TodoId, Title = TodoTitle, Body = TodoBody };
            ApplicationDbContext.Add(todo);
            ApplicationDbContext.SaveChanges();
            ClearTextBox();
        }

        private void UpdateTodo_Click(object sender, RoutedEventArgs e)
        {
            var todo = ApplicationDbContext.Todos.First(t => t.Id == TodoId);
            todo.Title = TodoTitle;
            todo.Body = TodoBody;
            ApplicationDbContext.SaveChanges();
            ClearTextBox();
        }

        private void DeleteTodo_Click(object sender, RoutedEventArgs e)
        {
            var todo = ApplicationDbContext.Todos.First(t => t.Id == TodoId);
            ApplicationDbContext.Todos.Remove(todo);
            ApplicationDbContext.SaveChanges();
            ClearTextBox();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            var result = ApplicationDbContext.Todos.ToList();
            TodoDataGrid.ItemsSource = result;
        }

        private void ClearTextBox()
        {
            TodoIdTextBox.Text = "Todo Id";
            TodoTitleTextBox.Text = "Todo Title";
            TodoBodyTextBox.Text = "Todo Body";
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            var rowTodo = row.Item as Todo;

            this.NavigationService.Navigate(new AttachmentPage(ApplicationDbContext, rowTodo.Id));
        }

        private void TodoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = sender as DataGrid;
            var rowTodo = row.SelectedItem as Todo;

            if (rowTodo != null)
            {
                TodoIdTextBox.Text = rowTodo.Id.ToString();
                TodoTitleTextBox.Text = rowTodo.Title;
                TodoBodyTextBox.Text = rowTodo.Body;
            }
        }
    }
}
