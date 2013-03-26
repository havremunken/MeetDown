using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetDown.Core.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace MeetDown.Core.Indexes
{
    public class TagsByPopularity : AbstractIndexCreationTask<Group, TagPopularityResult>
    {
        public TagsByPopularity()
        {
            Map = groups => from grp in groups
                            from tag in grp.Tags
                            select new TagPopularityResult
                                {
                                    Tag = tag,
                                    Count = 1
                                };

            Reduce = results => from result in results
                                group result by result.Tag
                                into tag
                                select new TagPopularityResult
                                    {
                                        Tag = tag.Key,
                                        Count = tag.Sum(x => x.Count)
                                    };
        }
    }
}
