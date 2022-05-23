using System.Windows.Controls;

namespace TC.View
{
    /// <summary>
    /// Logika interakcji dla klasy PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        private ViewModel.PanelTCViewModel PanelTCVM;
        public PanelTC()
        {
            InitializeComponent();
            PanelTCVM = new ViewModel.PanelTCViewModel();
            DataContext = PanelTCVM;
        }
    }
}
