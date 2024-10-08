using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace Data_Input_Validation
{
    public class MainViewModel : INotifyDataErrorInfo
    {
        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];

            }
            else
            {
                return Enumerable.Empty<string>();
            }

        }




        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);


            if (results.Any())
            {
                if (Errors.ContainsKey(propertyName)) return;
                Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }



            SubmitCommand.RaiseCanExecuteChanged();

        }



        //private string _name;

        //[Required(ErrorMessage = "Name is Required")]
        //public string Name
        //{
        //    get { return _name; }
        //    set
        //    {
        //        _name = value;

        //        Validate(nameof(Name), value);
        //    }
        //}




        private int _email;

        [Required(ErrorMessage = "Email is Required")]
        public int Email
        {
            get { return _email; }
            set
            {
                _email = value;
                Validate(nameof(Email), value);

            }
        }



        private string _password;

        [Required(ErrorMessage = "Password is Required")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                Validate(nameof(Password), value);
            }
        }

        private DateTime _dateTime1;
        public DateTime DateTime1
        {
            get { return _dateTime1; }
            set
            {
                _dateTime1 = value;
                Validate(nameof(DateTime1), value);
            }
        }


        public ActionCommand SubmitCommand { get; set; }
        public ActionCommand TestCommand { get; set; }

        public MainViewModel()
        {

            SubmitCommand = new ActionCommand(Submit, CanSubmit);
            SubmitCommand = new ActionCommand(Submit, null);
            DateTime1 = DateTime.Now;


        }

        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null);

        }
        private double _currentProgress;
        public double CurrentProgress
        {
            get { return _currentProgress; }
            private set
            {
                _currentProgress = value;
                Validate(nameof(CurrentProgress), value);
                // OnPropertyChanged("CurrentProgress");
            }
        }
        private void Submit(object obj)
        {

            MessageBox.Show("Submitted");
        }

    }
}
