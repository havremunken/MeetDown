using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using MeetDown.Core.Entities;
using MeetDown.Core.Indexes;
using MeetDown.Web.Models;
using Raven.Client;

namespace MeetDown.Web.Controllers
{
    [RoutePrefix("Tags")]
    public class TagsController : Controller
    {
        #region Fields

        private IDocumentSession _session;

        #endregion

        #region Constructor

        public TagsController(IDocumentSession session)
        {
            _session = session;
        }

        #endregion
        
        [GET("Index")]
        public ActionResult Index()
        {
            var popularTags = _session.Query<TagPopularityResult, TagsByPopularity>()
                                      .OrderByDescending(t => t.Count)
                                      .ToList();

            return View(popularTags);
        }

        /// <summary>
        /// Finds all groups that have a certain tag
        /// </summary>
        /// <param name="id">The tag to search for</param>
        /// <returns>A TagSearchModel containing the search results</returns>
        [GET("Find/{id}")]
        public ActionResult Find(string id)
        {
            var model = new TagSearchModel
                {
                    Tag = id,
                    Groups = _session.Query<Group>()
                                     .Where(g => g.Tags.Contains(id))
                                     .ToList()
                };

            return View(model);
        }
    }
}
