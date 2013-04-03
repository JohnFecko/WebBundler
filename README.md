WebBundler
==========

A windows commmand line utility that bundles, minifies, and compresses CSS and Javascript files from a web page.  The files to be bundled are marked in the html with the following comments.


&lt;!--BEGIN CSS PLACEHOLDER --><br/>
  {CSS FILES}<br/>
&lt;!--END CSS PLACEHOLDER --><br/>

&lt;!--BEGIN JS PLACEHOLDER --><br/>
  {JAVASCRIPT FILES}<br/>
&lt;!--END JS PLACEHOLDER --><br/>

Required Packages
=================
Yahoo! UI Library: YUI Compressor for .Net<br/>
http://yuicompressor.codeplex.com/

Command Line Option
===================

WebBundler.exe {file} /script:{script} /stylesheets:{stysheets} /nojs /nocss <br/>
  {file}: Full path of file containing CSS and JS Placeholder data.<br/>
  {script}: Subdirectory path from {file} location. Optional.  Defaults to scripts.<br/>
  {stylesheets}: Subdirectory path from {file} location. Optional.  Defaults to stylesheets.<br/><br/>
  /nojs: Do not process javascript.<br/>
  /nocss: Do not process css.
