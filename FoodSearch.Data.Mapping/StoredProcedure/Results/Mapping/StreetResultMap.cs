﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results.Mapping
{
    public class StreetResultMap : ClassMap<StreetResult>
    {
        public StreetResultMap()
        {
            Table("GetStreets");
            ReadOnly();
            Id(x => x.StreetId).Column("StreetId");
            Map(x => x.Name).Column("Name");
        }
    }
}