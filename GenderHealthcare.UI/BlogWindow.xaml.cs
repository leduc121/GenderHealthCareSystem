using GenderHealthcare.BLL.DTOs;
using System;
using System.Windows;

namespace GenderHealthcare.UI.Views
{
    public partial class BlogWindow : Window
    {
        public BlogDTO Blog { get; set; }

        public BlogWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Blog.Title) || string.IsNullOrWhiteSpace(Blog.Content) || string.IsNullOrWhiteSpace(Blog.AuthorId))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}