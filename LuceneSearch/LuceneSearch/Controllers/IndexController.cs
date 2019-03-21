using Lucene.Net.Store;
using LuceneSearch.AzureLucene;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LuceneSearch.Controllers
{
    

    public class IndexController : ApiController
    {
        static LuceneIndexer indexer = null;

        /// <summary>
        /// Constructor for an Index Controller
        /// 
        /// </summary>
        public IndexController() : base()
        {
            Log("Initialization Started");

            if (indexer == null)
            {
                var directory = ConfigurationManager.AppSettings["directory"];

                Log($"Directory : '{directory}'");

                indexer = new LuceneIndexer(directory);

            }

        }

        /// <summary>
        /// Get all entries for the name
        /// </summary>
        /// <remarks>
        /// Get a list of all name's that are similar
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<IndexEntry>))]
        public HttpResponseMessage GetByName(string name)
        {
            var entries = indexer.Search(name);

            return Request.CreateResponse(HttpStatusCode.OK, entries);

        }

        /// <summary>
        /// Get index entry by id
        /// </summary>
        /// <remarks>
        /// Get index entry by id
        /// </remarks>
        /// <param name="id">Id of index entry</param>
        /// <returns></returns>
        /// <response code="200">Index Entry found</response>
        /// <response code="404">Index Entry not found</response>
        [ResponseType(typeof(IndexEntry))]     
        public HttpResponseMessage  GetById(string id)
        {
            var indexEntry = indexer.Get(id);

            return indexEntry == null
                 ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Index Entry not found")
                 : Request.CreateResponse(HttpStatusCode.OK, indexEntry); 

        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Add new Index entry
        /// </summary>
        /// <remarks>
        /// Add a new Index entry
        /// </remarks>
        /// <param name="id">The index identifier</param>
        /// <param name="name">The index name</param>
        /// <returns></returns>
        /// <response code="201">Index Entry created</response>
        [ResponseType(typeof(string))]
        public HttpResponseMessage Put(string id, string name)
        {
            indexer.AddIndexEntry(id, name);

            return Request.CreateResponse(HttpStatusCode.Created, "Index Created");
        }

        // DELETE api/values/5
        public void Delete(string id)
        {
        }

        private void Log(string message)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Temp\debug.txt", true))
            {
                file.WriteLine(DateTime.Now.ToString("d/M/yyyy HH:MM ss") + " " + message);
            }

  
        }
    }

}
