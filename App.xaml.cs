
using Catel.Windows;
using System.Windows;

namespace CatelWPFApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            Catel.Logging.LogManager.AddDebugListener();
#endif

            // TODO: Using a custom IoC container like Unity? Register it here:
            // Catel.IoC.ServiceLocator.Instance.RegisterExternalContainer(MyUnityContainer);

            base.OnStartup(e);
        }
    }
}