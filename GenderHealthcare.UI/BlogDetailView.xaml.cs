using GenderHealthcare.BLL.DTOs;
using System.ComponentModel;
using System.Windows.Controls;

namespace GenderHealthcare.UI.Views
{
    public partial class BlogDetailView : UserControl
    {
        private BlogDTO _blog;

        public BlogDTO Blog
        {
            get => _blog;
            set
            {
                _blog = value;
                DataContext = this;
            }
        }

        public BlogDetailView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Blog = new BlogDTO();
            }
        }
    }
}