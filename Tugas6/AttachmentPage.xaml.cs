using System;
using System.Linq;
using System.Windows.Controls;
using Tugas6.Model;

namespace Tugas6
{
    /// <summary>
    /// Interaction logic for AttachmentPage.xaml
    /// </summary>
    public partial class AttachmentPage : Page
    {
        private ApplicationDbContext ApplicationDbContext;
        private int TodoId;
        private int AttachmentId;
        private string AttachmentUrl;

        public AttachmentPage(ApplicationDbContext applicationDbContext, int todoId)
        {
            InitializeComponent();
            ApplicationDbContext = applicationDbContext;
            TodoId = todoId;
            RefreshData();
        }

        private void AttachmentIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            try
            {
                AttachmentId = int.Parse(textBox.Text);
            }
            catch (Exception)
            {
                AttachmentId = 1;
            }
        }

        private void AttachmentUrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            AttachmentUrl = textBox.Text;
        }

        private void Create_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var attachment = new Attachment() { Id = AttachmentId, TodoId = TodoId, Url = AttachmentUrl };
            ApplicationDbContext.Add(attachment);
            ApplicationDbContext.SaveChanges();
            ClearTextBox();
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var attachment = ApplicationDbContext.Attachments.First(t => t.Id == AttachmentId);
            attachment.Url = AttachmentUrl;
            ApplicationDbContext.SaveChanges();
            ClearTextBox();
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var attachment = ApplicationDbContext.Attachments.First(t => t.Id == AttachmentId);
            ApplicationDbContext.Attachments.Remove(attachment);
            ApplicationDbContext.SaveChanges();
            ClearTextBox();
        }

        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            var result = ApplicationDbContext.Attachments.Where(a => a.TodoId == TodoId).ToList();
            AttachmentDataGrid.ItemsSource = result;
        }

        private void ClearTextBox()
        {
            AttachmentIdTextBox.Text = "Attachment Id";
            AttachmentUrlTextBox.Text = "Attachment Url";
        }

        private void AttachmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = sender as DataGrid;
            var rowTodo = row.SelectedItem as Attachment;

            if (rowTodo != null)
            {
                AttachmentIdTextBox.Text = rowTodo.Id.ToString();
                AttachmentUrlTextBox.Text = rowTodo.Url;
            }
        }
    }
}
