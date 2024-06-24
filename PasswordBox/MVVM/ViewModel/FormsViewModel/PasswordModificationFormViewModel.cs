using PasswordBox.MVVM.Model;
using System.Windows.Input;
using System.Windows;
using PasswordBox.Core;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.Enums;
using PasswordBox.MVVM.Model.Validators;
using PasswordBox.MVVM.Model.Repositories;
using PasswordBox.MVVM.Model.AppContext;

namespace PasswordBox.MVVM.ViewModel.FormsViewModel
{
    internal class PasswordModificationFormViewModel : Core.ViewModel
    {
        private readonly PasswordValidator _passwordValidator;
        private readonly PasswordRepository _passwordRepository;

        // Add
        public PasswordModificationFormViewModel(ActionWithData action)
        {
            if (action != ActionWithData.Add)
                throw new ArgumentException($"Incorrect value of {nameof(ActionWithData)}");

            Action = action;
            _passwordValidator = new();
            _passwordRepository = new(new ApplicationContext());

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            AcceptCommand = new RelayCommand(Accept);
            AddImageCommand = new RelayCommand(AddImage);
        }

        // Update
        public PasswordModificationFormViewModel(ActionWithData action, Password password)
        {
            if (action != ActionWithData.Update)
                throw new ArgumentException($"Incorrect value of {nameof(ActionWithData)}");

            Action = action;
            _passwordValidator = new();
            _passwordRepository = new(new ApplicationContext());

            Id = password.Id;
            Title = password.Title;
            Image = password.Image;
            Login = password.Login;
            PasswordHash = password.PasswordHash;
            Commentary = password.Commentary;

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            AcceptCommand = new RelayCommand(Accept);
            AddImageCommand = new RelayCommand(AddImage);
        }

        #region Properties

        private ActionWithData _action;
        public ActionWithData Action
        {
            get { return _action; }
            set { _action = value; OnPropertyChanged(); }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private string _image = "pack://application:,,,/Resources/IconPassword.png"; // путь по умолчанию
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(); }
        }

        private string _passwordHash;
        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; OnPropertyChanged(); }
        }

        private string _commentary;
        public string Commentary
        {
            get { return _commentary; }
            set { _commentary = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand AcceptCommand { get; set; }

        private void Close(object obj)
        {
            if (obj is not Window form)
                return;

            _passwordRepository.Dispose();
            form.Close();       
        }
        private void Minimize(object obj)
        {
            if (obj is not Window form)
                return;

            form.WindowState = WindowState.Minimized;
        }
        private void AddImage(object obj) => Image = DataWorker.GetImage();
        private async void Accept(object obj)
        {
            if (obj is not Window form)
                return;

            Password password = new(Id, Title, Login, PasswordHash, Commentary, Image);

            var result = await _passwordValidator.ValidateAsync(password);

            if (!result.IsValid)
            {
                ErrorDisplay.ShowValidationErrorMessage(result.Errors);
                return;
            }

            if (Action == ActionWithData.Add)
            {
                try
                {
                    await _passwordRepository.AddAsync(password);
                }
                catch (Exception e)
                {
                    dynamic ex = e;
                    ExceptionsLogger.Handle(ex);
                    MessageBox.Show("Failed to add the password.");
                    return;
                }

                Close(form);
            }
            else if (Action == ActionWithData.Update)
            {
                try 
                {
                    await _passwordRepository.UpdateAsync(password);
                }
                catch (Exception e)
                {
                    dynamic ex = e;
                    ExceptionsLogger.Handle(ex);
                    MessageBox.Show("Failed to update the password.");
                    return;
                }

                Close(form);
            }
        }

        #endregion
    }
}
