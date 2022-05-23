using System.Collections.Generic;
using System.Linq;
using System.IO;
using TC.ViewModel;

namespace TC.Model
{
    class PanelTCModel
    {

        #region Properties
        private bool selected;
        public bool Selected
        {
            get => selected;
            set => selected = value;
        }

        private string filePath;
        public string FilePath
        {
            get => filePath;
            set => filePath = value;
        }

        private List<string> drives;
        public List<string> Drives
        {
            get => drives;
            set => drives = value;
        }

        private List<string> items;
        public List<string> Items
        {
            get => items;
            set => items = value;
        }

        private string activePath;
        public string ActivePath
        {
            get => activePath;
            set => activePath = value;
        }

        private string selectedDrive;
        public string SelectedDrive
        {
            get => selectedDrive;
            set => selectedDrive = value;
        }

        private string selectedItemi;
        public string SelectedItemi
        {
            get => selectedItemi;
            set => selectedItemi = value;
        }
        #endregion

        #region Methods
        public void LoadItems(string tmpPath)
        {
            List<string> tmp = new();
            if (tmpPath != SelectedDrive)
                tmp.Add("..");

            List<string> files = null;


            List<string> directories = Directory.GetDirectories(tmpPath).ToList();


            for (int i = 0; i < directories.Count; i++)
            {
                tmp.Add("<D>" + Path.GetFileName(directories[i]));
            }

            if (Directory.GetFiles(tmpPath) != null)
                files = Directory.GetFiles(tmpPath).ToList();

            for (int i = 0; i < files.Count; i++)
            {
                tmp.Add(Path.GetFileName(files[i]));
            }
            Items = tmp;
        }

        public void UpdatePathFun()
        {

            if (SelectedItemi == "..")
            {
                FilePath = null;
                SelectedItemi = null;

                if (SelectedDrive == ActivePath)
                    return;

                string tmp = Path.GetDirectoryName(ActivePath);
                if (SelectedDrive != tmp)
                    tmp = Path.GetDirectoryName(tmp);

                if (tmp == SelectedDrive)
                    ActivePath = tmp;
                else
                    ActivePath = tmp + "\\";

            }
            else if (File.Exists(ActivePath + SelectedItemi))
            {
                FilePath = ActivePath + SelectedItemi;
            }
            else if (Directory.Exists(ActivePath + SelectedItemi.Remove(0, 3)))
            {
                FilePath = null;
                ActivePath = ActivePath + SelectedItemi.Remove(0, 3) + "\\";
                SelectedItemi = null;
            }
        }

        public void PanelChanged()
        {
            bool left = MainViewModel.Instance.Left.Selected;
            bool right = MainViewModel.Instance.Right.Selected;
            if (left)
                MainViewModel.Instance.ButtonText = "Copy >>";
            if (right)
                MainViewModel.Instance.ButtonText = "<< Copy";
        }
        #endregion

    }
}
