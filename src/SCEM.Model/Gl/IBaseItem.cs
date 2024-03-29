﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace SCEM.Model.Gl
{
  public interface IBaseItem
  {
    [SitecoreId]
    Guid Id { get; set; }

    [SitecoreChildren]
    IEnumerable<Item> Children { get; set; }

    [SitecoreInfo(SitecoreInfoType.Url, UrlOptions = SitecoreInfoUrlOptions.LanguageEmbeddingNever)]
    string Url { get; set; }

    [SitecoreInfo(SitecoreInfoType.Language, UrlOptions = SitecoreInfoUrlOptions.LanguageEmbeddingNever)]
    Language Language { get; set; }

    [SitecoreInfo(SitecoreInfoType.Version, UrlOptions = SitecoreInfoUrlOptions.LanguageEmbeddingNever)]
    int Version { get; set; }

    [SitecoreInfo(SitecoreInfoType.TemplateId)]
    Guid TemplateId { get; set; }

    [SitecoreInfo(SitecoreInfoType.BaseTemplateIds)]
    IEnumerable<Guid> BaseTemplateIds { get; set; }

    [SitecoreInfo(SitecoreInfoType.TemplateName)]
    string TemplateName { get; set; }

    [SitecoreInfo(SitecoreInfoType.Name)]
    string Name { get; set; }

  }
}
