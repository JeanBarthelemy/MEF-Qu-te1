using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace MEF_Quête1
{
    class Program
    {
        private MefHost _host;

        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();

            Console.ReadKey();
        }

        private void Run()
        {
            _host = new MefHost();
            HelloService service = _host.Container.GetExportedValue<HelloService>();
        }
    }

    internal class MefHost
    {
        public CompositionContainer Container
        {
            get
            {
                if (_container == null)
                {
                    AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                    _container = new CompositionContainer(catalog);
                }
                return _container;
            }
        }
        private CompositionContainer _container = null;
    }

    [Export]
    internal class HelloSayer
    {
        public HelloSayer()
        {
            SayHello();
        }

        public void SayHello()
        {
            Console.WriteLine("Bonjour !");
        }
    }

    [Export]
    internal class HelloService
    {
        [Import]
        private HelloSayer helloSayer;
    }
}
