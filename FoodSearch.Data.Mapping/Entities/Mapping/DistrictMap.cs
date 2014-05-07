﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class DistrictMap : ClassMap<District>
    {
        public DistrictMap()
        {
            Table("Districts");
            ReadOnly();
            LazyLoad();
            Id(x => x.DistrictId).Column("DistrictId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
