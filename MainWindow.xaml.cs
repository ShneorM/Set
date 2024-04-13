using System.Windows;
using Set.ViewModel;
namespace Set;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    SetViewModel viewModel;
    public MainWindow()
    {
        InitializeComponent();
        viewModel = new SetViewModel();
        this.DataContext = viewModel;
    }
}
