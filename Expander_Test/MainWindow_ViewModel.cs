using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using System.Windows.Data;
using System.Collections;

namespace Expander_Test
{
    public class MainWindow_ViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public List<Info_Bible> m_WholeBible                                { get; set; }

        public Queue<string> Books  { get; set; }
        public bool Busy            { get; set; }

        public string Title         { get; set; }
        public int SelectedIndex    { get; set; }

        public string[] wordList { get; set; }


        #region MainWindow_ViewModel

        public MainWindow_ViewModel()
        {

            // TODO flip dashbards

            //FlipViewTemplateSelector = new RandomDataTemplateSelector();

            //FrameworkElementFactory spFactory = new FrameworkElementFactory(typeof(Image));

            //spFactory.SetBinding(Image.SourceProperty, new Binding("."));
            //spFactory.SetValue(Image.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
            //spFactory.SetValue(Image.StretchProperty, Stretch.Fill);
            //FlipViewTemplateSelector.TemplateOne = new DataTemplate()
            //{
            //    VisualTree = spFactory
            //};
            //FlipViewImages = new string[] { "http://trinities.org/blog/wp-content/uploads/red-ball.jpg", "http://savingwithsisters.files.wordpress.com/2012/05/ball.gif" };

            //RaisePropertyChanged("FlipViewTemplateSelector");


            BrushResources = FindBrushResources();
        }

        #endregion

        private ICommand command_settings;
        public ICommand Settings
        {
            get
            {
                return command_settings
                    ?? (command_settings = new ActionCommand(() =>
                    {
                        MessageBox.Show("SomeCommand");
                    }));
            }
        }

        int? _integerGreater10Property;
        public int? IntegerGreater10Property
        {
            get { return _integerGreater10Property; }
            set
            {
                if (Equals(value, _integerGreater10Property))
                {
                    return;
                }

                _integerGreater10Property = value;
                RaisePropertyChanged("IntegerGreater10Property");
            }
        }

        
        bool m_bOrientation;
        public bool Orientation
        {
            get { return m_bOrientation; }
            set
            {
                m_bOrientation = value;
                RaisePropertyChanged("Orientation");
            }
        }

        bool m_bDockStatus;
        public bool DockStatus
        {
            get { return m_bDockStatus; }
            set
            {
                m_bDockStatus = value;
                RaisePropertyChanged("DockStatus");
            }
        }

        DateTime? _datePickerDate;
        public DateTime? DatePickerDate
        {
            get { return _datePickerDate; }
            set
            {
                if (Equals(value, _datePickerDate))
                {
                    return;
                }

                _datePickerDate = value;
                RaisePropertyChanged("DatePickerDate");
            }
        }

        bool _magicToggleButtonIsChecked = true;
        public bool MagicToggleButtonIsChecked
        {
            get { return this._magicToggleButtonIsChecked; }
            set
            {
                if (Equals(value, _magicToggleButtonIsChecked))
                {
                    return;
                }

                _magicToggleButtonIsChecked = value;
                RaisePropertyChanged("MagicToggleButtonIsChecked");
            }
        }

        private bool _quitConfirmationEnabled;
        public bool TrayApp_Enabled
        {
            get { return _quitConfirmationEnabled; }
            set
            {
                if (value.Equals(_quitConfirmationEnabled)) return;
                _quitConfirmationEnabled = value;
                RaisePropertyChanged("QuitConfirmationEnabled");
            }
        }

        private ICommand textBoxButtonCmd;

        public ICommand TextBoxButtonCmd
        {
            get
            {
                return this.textBoxButtonCmd ?? (this.textBoxButtonCmd = new TextBoxButtonCommand());
            }
        }

        #region TextBoxButtonCommand

        public class TextBoxButtonCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                if (parameter is TextBox)
                {
                    //MessageBox.Show("TextBox Button was clicked!" + Environment.NewLine + "Text: " + ((TextBox)parameter).Text);
                    //((MainWindow)System.Windows.Application.Current.MainWindow).Show_Dashboard();
                }
                else if (parameter is PasswordBox)
                {
                    MessageBox.Show("PasswordBox Button was clicked!" + Environment.NewLine + "Text: " + ((PasswordBox)parameter).Password);
                }
            }
        }

        #endregion

        private ICommand textBoxButtonCmdWithParameter;

        public ICommand TextBoxButtonCmdWithParameter
        {
            get
            {
                return this.textBoxButtonCmdWithParameter ?? (this.textBoxButtonCmdWithParameter = new TextBoxButtonCommandWithIntParameter());
            }
        }

        #region TextBoxButtonCommandWithIntParameter

        public class TextBoxButtonCommandWithIntParameter : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                if (parameter is String)
                {
                    MessageBox.Show("TextBox Button was clicked with parameter!" + Environment.NewLine + "Text: " + parameter);
                }
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region RaisePropertyChanged

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public string this[string columnName]
        {
            get
            {
                if (columnName == "IntegerGreater10Property" && this.IntegerGreater10Property < 10)
                {
                    return "Number is not greater than 10!";
                }

                if (columnName == "DatePickerDate" && this.DatePickerDate == null)
                {
                    return "No date given!";
                }

                return null;
            }
        }

        public string Error { get { return string.Empty; } }

        public ICommand SingleCloseTabCommand { get { return new ExampleSingleTabCloseCommand(); } }

        public class ExampleSingleTabCloseCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                System.Windows.MessageBox.Show("You are now closing the '" + parameter + "' tab!");
            }
        }

        public ICommand NeverCloseTabCommand { get { return new AlwaysInvalidCloseCommand(); } }

        public class AlwaysInvalidCloseCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return false;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }
        }

        public IEnumerable<string> BrushResources { get; private set; }

        private bool _animateOnPositionChange = true;
        public bool AnimateOnPositionChange
        {
            get
            {
                return _animateOnPositionChange;
            }
            set
            {
                if (Equals(_animateOnPositionChange, value)) return;
                _animateOnPositionChange = value;
                RaisePropertyChanged("AnimateOnPositionChange");
            }
        }

        private IEnumerable<string> FindBrushResources()
        {
            var rd = new ResourceDictionary
            {
                Source = new Uri(@"/MahApps.Metro;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute)
            };

            var resources = rd.Keys.Cast<object>()
                    .Where(key => rd[key] is Brush)
                    .Select(key => key.ToString())
                    .OrderBy(s => s)
                    .ToList();

            return resources;
        }

        public RandomDataTemplateSelector FlipViewTemplateSelector
        {
            get;
            set;
        }

        public string[] FlipViewImages
        {
            get;
            set;
        }


        public class RandomDataTemplateSelector : DataTemplateSelector
        {
            public DataTemplate TemplateOne { get; set; }

            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                return TemplateOne;
            }
        }
    }

    #region ActionCommand

    public class ActionCommand : ICommand
    {
        private readonly Action _action;

        public ActionCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }

    #endregion

    public class Info_Bible
    {
        public int Nr       { get; set; } // interne Number from all books inside Info_Bible   1 = Mose, 66 = Offenbarung
        public string Acron { get; set; }  // e.g. Mt
        public string Link  { get; set; } // e.g. Ruth 1.1

        public string Book  { get; set; }
        public string Bund  { get; set; }
        public string Typ   { get; set; }

        public int Vers       { get; set; }
        public int CaptureAct { get; set; }
        public int CaptureMax { get; set; }

        public ArrayList arrVerses  { get; set; }
        public bool bSelectMode     { get; set; }
    }
}