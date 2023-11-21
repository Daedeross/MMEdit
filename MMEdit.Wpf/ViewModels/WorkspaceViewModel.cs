namespace MMEdit.Wpf.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Microsoft.Win32;
    using MMEdit.ViewModels;
    using ReactiveUI;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    public class WorkspaceViewModel : ViewModelBase
    {
        private MM2.Character _testData = new MM2.Character
        {
            Name = "Test",
        };

        private ObservableCollection2<MM2CharacterViewModel> m_Characters;
        public ObservableCollection2<MM2CharacterViewModel> Characters
        {
            get => m_Characters;
            set => this.RaiseAndSetIfChanged(ref m_Characters, value);
        }

        private MM2CharacterViewModel? m_CurrentCharacter;
        public MM2CharacterViewModel? CurrentCharacter
        {
            get => m_CurrentCharacter;
            set => this.RaiseAndSetIfChanged(ref m_CurrentCharacter, value);
        }

        public ICommand LoadRosterCommand { get; }

        public WorkspaceViewModel()
        {
            LoadRosterCommand = new RelayCommand(ShowLoadDialog);
        }

        private void ShowLoadDialog()
        {
            var dlg = new OpenFileDialog();
            dlg.InitialDirectory = "F:\\projects\\MMEdit\\MMEdit.MM2\\data";
            dlg.Multiselect = false;
            dlg.Filter = "dat files (*.DAT)|*.DAT|AllFiles (*.*)|*.*";

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (dlg.ShowDialog() == true)
                {
                    LoadMM2Roster(dlg.OpenFile());
                }
            });
        }

        private void LoadMM2Roster(Stream file)
        {
            var allCharacters = MM2.Converter.ReadCharacters(file);
            Characters = new ObservableCollection2<MM2CharacterViewModel>(allCharacters.Select(x => new MM2CharacterViewModel(x)));
            CurrentCharacter = Characters.FirstOrDefault();
        }
    }
}
