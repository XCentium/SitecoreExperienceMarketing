If you do noto already have a working SOLR isntance, follow this guide* with a few tweaks: https://sitecore-community.github.io/docs/search/solr/fast-track-solr-for-lazy-developers/
1) For step 1, Use the Bitnami installer.
2) The clean solr cores are already included in tihs directory for convinience
4 & 5) Thses cha nges area already made in the web project, however, as some of the files have moved for orginisaion reasons, you may wish to delete the following files from your site before deploying the project:
App_Config\Include\Sitecore.ContentSearch.Analytics.config
App_Config\Include\Sitecore.ContentSearch.config
App_Config\Include\Sitecore.ContentSearch.DefaultConfigurations.config
App_Config\Include\Sitecore.ContentSearch.Lucene.*
App_Config\Include\Sitecore.ContentSearch.Solr.*
App_Config\Include\Sitecore.ContentSearch.VerboseLogging.config.example
App_Config\Include\ContentTesting\Sitecore.ContentTesting.Lucene.*
App_Config\Include\ContentTesting\Sitecore.ContentTesting.Solr.*
App_Config\Include\FXM\Sitecore.FXM.Lucene.*
App_Config\Include\FXM\Sitecore.FXM.Solr.*
App_Config\Include\ListManagement\Sitecore.ListManagement.Lucene.*
App_Config\Include\ListManagement\Sitecore.ListManagement.Solr.*
App_Config\Include\Sitecore.Marketing.Definitions.MarketingAssets.Repositories.Lucene.*
App_Config\Include\Sitecore.Marketing.Definitions.MarketingAssets.Repositories.Solr.*
App_Config\Include\Sitecore.Marketing.Lucene.*
App_Config\Include\Sitecore.Marketing.Solr.*
App_Config\Include\Social\Sitecore.Social.Lucene.*
App_Config\Include\Social\Sitecore.Social.Solr.Index.*
App_Config\Include\Sitecore.Speak.ContentSearch.Lucene.*
App_Config\Include\Sitecore.Speak.ContentSearch.Solr.*

6) For step 6, use NuGet to install Unity, ver:2.1.505.2
7) Step 7 is not needed as it's part of the web project

DO: update the value on the node <setting name="ContentSearch.Solr.ServiceBaseAddress" value="http://localhost:8088/solr" /> in Sitecore.ContentSearch.Solr.DefaultIndexConfiguration.config to felect your local setup.

These are clean SOLR cores for SOLR 4.10 for use with Siteore 8.1.
Copy these folders to the location where your SOLR install keeps its cores, restart SOLR, then rebuild indexes.
