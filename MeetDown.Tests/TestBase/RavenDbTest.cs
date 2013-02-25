using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Embedded;

namespace MeetDown.Tests.TestBase
{
    /// <summary>
    /// Abstract base class for tests that need access to the in-memory RavenDB document store
    /// </summary>
    public abstract class RavenDbTest
    {
        public IDocumentStore DocumentStore { get; private set; }

        protected RavenDbTest()
        {
            var documentStore = new EmbeddableDocumentStore
                {
                    RunInMemory = true
                };
            documentStore.Initialize();
            documentStore.RegisterListener(new NoStaleQueriesListener());
            DocumentStore = documentStore;
        }
    }
}
