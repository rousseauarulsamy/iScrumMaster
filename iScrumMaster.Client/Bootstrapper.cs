using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using iScrumMaster.Infrastructure.Models;
using iScrumMaster.Infrastructure;

namespace iScrumMaster.Client
{
    public class Bootstrapper : MefBootstrapper    
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="DependencyObject"/>, the
        /// <see cref="Bootstrapper"/> will attach the default <seealso cref="Microsoft.Practices.Composite.Regions.IRegionManager"/> of
        /// the application in its <see cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionManagerProperty"/> attached property
        /// in order to be able to add regions by using the <seealso cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionNameProperty"/>
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return new EmptyUserControl();
        }
              

        /// <summary>
        /// Configures the <see cref="AggregateCatalog"/> used by MEF.
        /// </summary>
        /// <remarks>
        /// The base implementation does nothing.
        /// </remarks>
        ///         
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // Add this assembly to export ModuleTracker
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ScrumModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(InfrasModule).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Project).Assembly));

            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ScrumModule).Assembly));
        }        
    }
}
