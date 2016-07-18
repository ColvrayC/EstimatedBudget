using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using EstimatedBudget.POCO;
using System.IO;
using EstimatedBudget.Helpers;

namespace EstimatedBudget.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///COMMAND
        /// </summary>
        /// ChangeNameFrameCommand

        public RelayCommand<string> EditModeCommand { get; set; }

        public RelayCommand<string> SelectedFrameCommand { get; set; }


        /// <summary>
        ///CONSTRUCTEUR
        /// </summary>
        public MainViewModel()
        {
            //Check IF Databse Exist
            var PathDatabase = new ConnectionProvider().GetOpenConnection().ConnectionString.Substring(12);
            if (!File.Exists(PathDatabase))
            {
                DataBaseManager.CreateDatabase();
            }

            PathCurrentFrame = "BankAccountView.xaml";

            SelectedFrameCommand = new RelayCommand<string>(SelectedFrame);
            ShowNav = Visibility.Collapsed;
        }

        /// <summary>
        /// PROPERTY
        /// </summary>
        /// 

        [RaisePropertyChanged]
        public virtual string DisplayUser { get; set; }

        [RaisePropertyChanged]
        public virtual double ActualWidth { get; set; }

        [RaisePropertyChanged]
        public virtual string PathCurrentFrame { get; set; }

        [RaisePropertyChanged]
        public virtual Visibility ShowNav { get; set; }

        [RaisePropertyChanged]
        public virtual string NavigationName { get; set; }

        /// <summary>
        /// METHODES
        /// </summary>
        /// 

        private void SelectedFrame(string Path)
        {
            PathCurrentFrame = Path;
            ShowNav = Visibility.Visible;
        }

        //CleanUp
        public override void Cleanup()
        {

            base.Cleanup();
        }
    }
}

