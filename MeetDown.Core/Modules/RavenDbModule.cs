using MeetDown.Core.Indexes;
using Ninject.Modules;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Modules
{
    /// <summary>
    /// A Ninject module that provides anything RavenDB related
    /// </summary>
    public class RavenDbModule : NinjectModule
    {
        private readonly DocumentStore _store;

        public RavenDbModule()
        {
            _store = new DocumentStore
                {
                    ConnectionStringName = "RavenDB",
                    Conventions = new DocumentConvention {IdentityPartsSeparator = "-"}
                };
            _store.Initialize();

            IndexCreation.CreateIndexes(typeof(TagsByPopularity).Assembly, _store);
        }

        public override void Load()
        {
            Bind<IDocumentSession>()
                .ToMethod(x => _store.OpenSession());
        }
    }
}
