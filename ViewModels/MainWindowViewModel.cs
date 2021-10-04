using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Catel;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using CatelWPFApplication1.Models;

namespace CatelWPFApplication1.ViewModels
{
    /// <summary>
    /// MainWindow view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private readonly IDispatcherService _dispatcherService;
        #endregion

        #region Constructors
        public MainWindowViewModel(IDispatcherService dispatcherService=null)
        {
            ViaVmCommand = new TaskCommand<Person>(OnViaVmCommandExecute, OnViaVmCommandCanExecute);
            ViaElementNameCommand = new TaskCommand<Person>(OnViaElementNameCommandExecute, OnViaElementNameCommandCanExecute);


            PersonList = new List<Person>();
            PersonList.Add(new Person(){FirstName = "Albert", LastName = "Abraham"});
            PersonList.Add(new Person(){FirstName = "Betty", LastName = "Baboa"});
            PersonList.Add(new Person(){FirstName = "Cherry", LastName="Cesar"});

            Argument.IsNotNull(() => dispatcherService);
            _dispatcherService = dispatcherService;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the title of the view model.
        /// </summary>
        /// <value>The title.</value>
        public override string Title { get { return "View model title"; } }

        public List<Person> PersonList { get; }

        public Person SelectedPerson
        {
            get { return GetValue<Person>(SelectedPersonProperty); }
            set
            {
                SetValue(SelectedPersonProperty, value);
            }
        }

        public static readonly PropertyData SelectedPersonProperty = RegisterProperty(nameof(SelectedPerson), typeof(Person), null);

        #endregion

        #region Commands

        public TaskCommand<Person> ViaElementNameCommand { get; }


        private bool OnViaElementNameCommandCanExecute(Person person)
        {
            // on selection change of the datagrid, the parameter is 
            // one click behind, unless explicity triggered in the
            // OnPropertyChanged handler.
            TracePersonUsage(person);
            return person is not null;
        }

        private async Task OnViaElementNameCommandExecute(Person person)
        {
            // changing the SelectedPerson will affect hte datagrid.
            // this change is exposed immediatly to both CanExecute functions
            SelectedPerson = null;
        }
        public TaskCommand<Person> ViaVmCommand { get; }

        private bool OnViaVmCommandCanExecute(Person person)
        {
            // on selection change of the datagrid, the parameter
            // is always one click behind
            TracePersonUsage(person);
            return person is not null;
        }

        private async Task OnViaVmCommandExecute(Person person)
        {
            // changing the SelectedPerson will affect hte datagrid.
            // this change is exposed immediatly to both CanExecute functions
            SelectedPerson = PersonList.FirstOrDefault();
        }
        #endregion

        #region Methods
        protected override void OnPropertyChanged(AdvancedPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            _dispatcherService?.BeginInvoke(async () =>
            {
                // a long delay for easier spotting of the
                // InvalidateCommands result and tracing
                // for workaround purpose, a delay of just 10 ms will do
                await Task.Delay(2000);
                ViewModelCommandManager.InvalidateCommands();
            });
        }

        /// <summary>
        /// formatted output for the On_**_CommandCanExecute functions
        /// </summary>
        /// <param name="person"></param>
        /// <param name="callerMember"></param>
        private void TracePersonUsage(Person person, [CallerMemberName] string callerMember = null)
        {
            var parameterFirstName = person is null ? "null" : person.FirstName;
            var selectedFirstName = SelectedPerson is null ? "null" : SelectedPerson.FirstName;

            Trace.TraceInformation($"{callerMember,35} was called with person = {parameterFirstName,7} while SelectedPersion = {selectedFirstName,7}");
        }

        #endregion
    }
}
