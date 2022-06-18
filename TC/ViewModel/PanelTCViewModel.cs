using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using TC.Model;

namespace TC.ViewModel
{
    class PanelTCViewModel : INotifyPropertyChanged
    {
        #region Constructor
        public event PropertyChangedEventHandler PropertyChanged;
        private PanelTCModel tCModel;
        public PanelTCViewModel()
        {
            tCModel = new PanelTCModel();
            tCModel.Selected = false;
        }
        #endregion

        #region Properties
        public bool Selected { 
            get => tCModel.Selected;
            set => tCModel.Selected = value;
        }

        public string FilePath{
            get => tCModel.FilePath;
            set => tCModel.FilePath = value;
        }

        public List<string> Drives
        {
            get => tCModel.Drives;
            set
            {
                tCModel.Drives = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drives)));
            }
        }

        public List<string> Items
        {
            get => tCModel.Items;
            set
            {
                tCModel.Items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }

        public string ActivePath {
            get => tCModel.ActivePath;
            set {
                try
                {
                    tCModel.LoadItems(value);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("Access denied", "Directory Access Error");
                    return;
                }
                tCModel.ActivePath = value;
                Items = tCModel.Items;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActivePath)));
            }
        }

        public string SelectedDrive {
            get => tCModel.SelectedDrive;
            set
            {
                tCModel.SelectedDrive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDrive)));
            }
        }

        public string SelectedItemi
        {
            get => tCModel.SelectedItemi;
            set
            {
                tCModel.SelectedItemi = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItemi)));
            }
        }
        #endregion

        #region Commands

        private ICommand getDrives;
        public ICommand GetDrives => getDrives ?? (getDrives = new RelayCommand(
            o => Drives = tCModel.GetDirectories(),
            o => true
            ));

        private ICommand updateDrive;
        public ICommand UpdateDrive => updateDrive ?? (updateDrive = new RelayCommand(
            o => ActivePath = SelectedDrive,
            o => true
            ));

        private ICommand updatePath;
        public ICommand UpdatePath => updatePath ?? (updatePath = new RelayCommand(
            o => 
            {
                tCModel.UpdatePathFun();
                FilePath = tCModel.FilePath;
                ActivePath = tCModel.ActivePath;
                SelectedItemi = tCModel.SelectedItemi;
            },
            o => SelectedItemi != null
            ));

        private ICommand changePanel;
        public ICommand ChangePanel => changePanel ?? (changePanel = new RelayCommand(
            o => 
            {
                if (!Selected) {
                    MainViewModel.Instance.Left.Selected = false;
                    MainViewModel.Instance.Right.Selected = false;
                    Selected = true;
                    tCModel.PanelChanged();
                }
            },
            o => true
            ));

        #endregion
    }
}
