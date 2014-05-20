using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class ImageMap : ClassMap<Image>
    {
        public ImageMap()
        {
            Table("Images");
            LazyLoad();
            Id(x => x.ImageId).Column("ImageId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.ImageData).Column("ImageData").Not.Nullable().Length(int.MaxValue);
            Map(x => x.ContentType).Column("ContentType").Not.Nullable();
        }
    }
}
