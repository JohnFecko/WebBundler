WebBundler
==========

A windows commmand line utility that bundles, minifies, and compresses CSS and Javascript files from a web page.  The files to be bundled are marked in the html with the following comments.


<!--BEGIN CSS PLACEHOLDER -->
  {CSS FILES}
<!--END CSS PLACEHOLDER -->

<!--BEGIN JS PLACEHOLDER -->
  {JAVASCRIPT FILES}
<!--END JS PLACEHOLDER -->


Required Packages
=================
Yahoo! UI Library: YUI Compressor for .Net
http://yuicompressor.codeplex.com/

Command Line Option
===================

WebBundler.exe {file} /script:{script} /stylesheets:{stysheets} /nojs /nocss
  {file}: Full path of file containing CSS and JS Placeholder data.
  {script}: Subdirectory path from {file} location. Optional.  Defaults to scripts.
  {stylesheets}: Subdirectory path from {file} location. Optional.  Defaults to stylesheets.

  /nojs: Do not process javascript.
  /nocss: Do not process css.
