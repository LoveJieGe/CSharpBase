using Chapter30_CalculatorUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalculator
{
    public sealed class CalculatureManager : IDisposable
    {
        private AssemblyCatalog catalog;
        private CompositionContainer container;
        private CalculatorImport calcImport;
        private CalculatorExtensionImport calcExtensionImport;
        private CalculatorViewModel viewModel;
        public CalculatureManager(CalculatorViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public async void InitializeContainer()
        {
            catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            container = new CompositionContainer(catalog);
            container.ExportsChanged += (object sender, ExportsChangeEventArgs e) =>
            {
                var sb = new StringBuilder();
                foreach(var item in e.AddedExports)
                {
                    sb.AppendFormat("added export {0}\n", item.ContractName);
                }
                foreach(var item in e.RemovedExports)
                {
                    sb.AppendFormat("removed export {0}\n", item.ContractName);
                }
                viewModel.Status += sb.ToString();
            };
            calcImport = new CalculatorImport();
            calcImport.ImportsSatisfied += (object sender, ImportEventArgs e) =>
              {
                  viewModel.Status += e.StatusMessage;
              };
            await Task.Run(() =>
            {
                container.ComposeParts(calcImport);
            });
            await InitializeOperationAsync();
        }

        public Task InitializeOperationAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
