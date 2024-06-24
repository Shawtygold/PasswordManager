using PasswordBox.Core;
using PasswordBox.Enums;
using PasswordBox.MVVM.Model;
using PasswordBox.MVVM.Model.AppContext;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.MVVM.Model.Repositories;
using PasswordBox.MVVM.View.Forms;
using PasswordBox.MVVM.ViewModel.FormsViewModel;
using PasswordBox.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.ViewModel
{
    class PasswordsViewModel : Core.ViewModel
    {
        private readonly PasswordRepository _passwordRepository;
        private IEnumerable<Password> _tempPasswords;

        public PasswordsViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Update);
            DeleteCommand = new RelayCommand(Delete);
            GetPasswordCommand = new RelayCommand(GetPassword);

            _passwordRepository = new PasswordRepository(new ApplicationContext());

            BackgroundWorker backgroundWorker = new();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            _tempPasswords = _passwordRepository.GetAll();
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Passwords = new (_tempPasswords);
        }

        #region Properties

        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Password> _passwords = new();
        public ObservableCollection<Password> Passwords
        {
            get { return _passwords; }
            set { _passwords = value; OnPropertyChanged(); }
        }


        private Password _selectedItem;
        public Password SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }


        #endregion

        #region Commands

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand GetPasswordCommand { get; set; }


        private void Add(object obj)
        {
            PasswordModificationForm form = new();
            form.DataContext = new PasswordModificationFormViewModel(ActionWithData.Add);
            form.ShowDialog();
        }
        private void Update(object obj)
        {
            if(SelectedItem == null)
            {
                MessageBox.Show("The password is not selected.");
                return;
            }

            PasswordModificationForm form = new();
            form.DataContext = new PasswordModificationFormViewModel(ActionWithData.Update, SelectedItem);
            form.ShowDialog();
        }
        private async void Delete(object obj)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("The password is not selected.");
                return;
            }

            if(MessageBox.Show("Do you want to delete this password?", null, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    await _passwordRepository.DeleteAsync(SelectedItem);
                }
                catch (Exception e)
                {
                    dynamic ex = e;
                    ExceptionsLogger.Handle(ex);
                    MessageBox.Show("Failed to delete the password.");
                    return;
                }
            }
        }
        private void GetPassword(object obj)
        {
            if (obj is not Password password)
                return;

            //вызов окна с логином и паролем
            PasswordDialog passwordDialog = new();
            passwordDialog.DataContext = new PasswordDialogViewModel(password.Login, password.PasswordHash, password.Commentary);

            passwordDialog.ShowDialog();
        }

        #endregion
    }
}
