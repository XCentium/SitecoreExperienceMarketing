# Creating a dev instance of SitecoreExperienceMarketing


1. Install Visual studio 2015 if you don't already have it. Add the following VS extensions

  *Microsoft ASP.NET and Web Tools
  *Microsoft ASP.NET Web Frameworks and Tools
  *T4 Toolbox for Visual Studio 2015
  *Web Compiler
  *Bundler & Minifier
  *Sitecore Rocks

2. Install Sitecore 8.1 update 3 locally

3. Create a folder at the solution level called references and copy the sitecore bin folder to the references directory

4. Update the z.SCEM.user.config.example file to point to the full path to the SCEM data directory <project-root>\src\data

5. Copy z.SCEM.user.config.example to the /website/app_config/includes directory and remove the .example extensions

6. Create a file based publishing target to your website as per "local.pubxml"