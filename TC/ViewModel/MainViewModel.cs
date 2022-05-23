using System.ComponentModel;
using System.Windows.Input;
using TC.Model;

namespace TC.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {

        #region Constructor
        public event PropertyChangedEventHandler PropertyChanged;

        private MainModel mainModel;
        private MainViewModel() {
            mainModel = new MainModel();
            ButtonText = "Copy";
            Left = new PanelTCViewModel();
            Right = new PanelTCViewModel();
        }
        #endregion

        #region Properties
        private static MainViewModel instance = null;
        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainViewModel();
                return instance;
            }
        }

        public PanelTCViewModel Left
        {
            get => mainModel.Left;
            set
            {
                mainModel.Left = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Left)));
            }
        }

        public PanelTCViewModel Right
        {
            get => mainModel.Right;
            set
            {
                mainModel.Right = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Right)));
            }
        }

        public string ButtonText
        {
            get => mainModel.ButtonText;
            set {
                mainModel.ButtonText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonText)));
            } 
        }
        #endregion

        #region Commands
        private ICommand copy;
        public ICommand Copy => copy ?? (copy = new RelayCommand(
            o => {
                try
                {
                    mainModel.Copy();
                }
                catch
                {
                    System.Windows.MessageBox.Show("File cannot be copied", "Copying File Error");
                }
            },
            o => true
            ));
        #endregion
    }
}
