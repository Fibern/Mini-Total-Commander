using System.IO;
using TC.ViewModel;

namespace TC.Model
{
    class MainModel
    {
        #region Properties
        private PanelTCViewModel left;
        public PanelTCViewModel Left
        {
            get => left;
            set => left = value;
        }

        private PanelTCViewModel right;
        public PanelTCViewModel Right
        {
            get => right;
            set => right = value;
        }

        private string buttonText;
        public string ButtonText
        {
            get => buttonText;
            set => buttonText = value;
        }
        #endregion

        #region Methods
        public void Copy()
        {
            if (Left.Selected && Left.FilePath != null)
            {
                if (Right.ActivePath == null)
                {
                    System.Windows.MessageBox.Show("Destination folder not selected", "Copying File Error");
                }
                else
                {
                    string tmp = Right.ActivePath + Left.SelectedItemi;
                    if (!File.Exists(tmp))
                        File.Copy(Left.FilePath, tmp);

                    Right.ActivePath = Right.ActivePath;
                }
            }
            else if (Right.Selected && Right.FilePath != null)
            {

                if (Left.ActivePath == null)
                {
                    System.Windows.MessageBox.Show("Destination folder not selected", "Copying File Error");
                }
                else
                {
                    string tmp = Left.ActivePath + Right.SelectedItemi;
                    if (!File.Exists(tmp))
                        File.Copy(Right.FilePath, tmp);
                    Left.ActivePath = Left.ActivePath;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Source file not selected", "Copying File Error");
            }
        }
        #endregion
    }
}
