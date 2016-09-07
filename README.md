# Introduction

1. This solution uses sitecore 8.1 Update 3 with WFFM, Exm 3.3, and GeoIP 

2. Content is persisted using Unicorn.

3. Sitecore Items are modeled using Glass

# Creating a dev instance of SitecoreExperienceMarketing

1. Install Visual studio 2015 if you don't already have it. Add the following VS extensions:

  * Microsoft ASP.NET and Web Tools

  * Microsoft ASP.NET Web Frameworks and Tools

  * T4 Toolbox for Visual Studio 2015

  * Web Compiler

  * Bundler & Minifier

  * Sitecore Rocks

2. Install Sitecore 8.1 update 3 locally.

3. Create a folder at the solution level called references and copy the sitecore bin folder to the references directory.

4. Update the z.SCEM.user.config.example file to point to the full path to the SCEM data directory "<project-root>\src\data".

5. Copy z.SCEM.user.config.example to the /website/app_config/includes directory and remove the .example extension.

6. Create a file based publishing target to your website as per "local.pubxml".

7. Publish locally.

8. Go to /unicorn.aspx and run an initial sync for your solution.

9. Go to the sitecore client and publish the site

# Regenerating Models using the generator

Sitecore Experience Marketing uses glass as the Object relational layer and t4 templates to generate interfaces from serialized unicorn templates.

## Initial Generator Setup

Make sure that "T4 Toolbox for Visual Studio 2015" plugin is installed.

browse to SCEM.Model/Generator

go to properties for GlassGenerator.tt and GlassMappedClassTemplate.tt and clear the Custom Tool property.

## To Regenerate from Glass

right click on SampleScriptTemplates.tt and click run custom tool. This should regenerate the glass templates from the unicorn serialization folder.

## Using custom return types

To map fields to custom interfaces, update the SampleScriptTemplate.tt file to check for the field id and set the appropriate type
 