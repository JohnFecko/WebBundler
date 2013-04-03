using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahoo.Yui.Compressor;

namespace WebBundler
{
    class Program
    {


        static void Main(string[] args)
        {

            string CssBeginSeperator = "<!--BEGIN CSS PLACEHOLDER -->";
            string CssEndSeperator = "<!--END CSS PLACEHOLDER -->";
            string JsBeginSeperator = "<!--BEGIN JS PLACEHOLDER -->";
            string JsEndSeperator = "<!--END JS PLACEHOLDER -->";

            var jsDirectory = "scripts";
            var cssDirectory = "stylesheets";
            var guid = Guid.NewGuid().ToString();
            var filePath = "";
            var fileName = "index.html";

            bool skipJs = false;
            bool skipCss = false;


            foreach (string arg in args)
            {
                if (arg.Length > 0)
                {
                    string start = arg.Substring(0, 2);
                    switch (start)
                    {
                        case "/?":
                            ShowHelp();
                            return;
                            break;
                        case "/n":
                            if (arg == "/nojs")
                            {
                                skipJs = true;
                            }
                            else if (arg == "/nocss")
                            {
                                skipCss = true;
                            }
                            break;

                        case "/s":
                            if (arg.StartsWith("/script"))
                            {
                                jsDirectory = arg.Replace("/script:", "");
                            }
                            else if (arg.StartsWith("/stylesheets"))
                            {
                                cssDirectory = arg.Replace("/stylesheets:", "");
                            }
                            break;
                        default:
                            if (File.Exists(arg))
                            {
                                if (arg.Contains('\\'))
                                {
                                    int index = arg.LastIndexOf('\\');
                                    filePath = arg.Substring(0, index + 1);
                                    fileName = arg.Substring(index + 1);
                                }
                                else
                                {
                                    filePath = "\\";
                                    fileName = arg;
                                }
                            }
                            else
                            {
                                Console.WriteLine("File does not exist.");
                                return;
                            }
                            break;
                    }
                }
            }

            var html = File.ReadAllText(filePath + fileName);
            if (!skipJs)
            {
                var jsFiles = Directory.EnumerateFiles(filePath + jsDirectory, "*.js");
                var js = ReadFiles(jsFiles);
                var jsTag = "<script src=\"" + jsDirectory + "/" + guid + ".js\"></script>";
                File.WriteAllText(filePath + jsDirectory + @"\" + guid + ".js", CompressJS(js));
                html = ReplaceSection(html, jsTag, JsBeginSeperator, JsEndSeperator);
            }


            if (!skipCss)
            {
                var cssFiles = Directory.EnumerateFiles(filePath + cssDirectory, "*.css");
                var css = ReadFiles(cssFiles);
                var cssTag = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + cssDirectory + "/" + guid + ".css\" />";
                File.WriteAllText(filePath + cssDirectory + @"\" + guid + ".css", CompressCss(css));
                html = ReplaceSection(html, cssTag, CssBeginSeperator, CssEndSeperator);
            }
            File.WriteAllText(filePath + fileName, html);
        }

        private static void ShowHelp()
        {
            Console.WriteLine("WebBundler.exe {file} /script:{script} /stylesheets:{stysheets} /nojs /nocss");
            Console.WriteLine("{file}: Full path of file containing CSS and JS Placeholder data.");
            Console.WriteLine("{script}: Subdirectory path from {file} location. Optional.  Defaults to scripts.");
            Console.WriteLine("{stylesheets}: Subdirectory path from {file} location. Optional.  Defaults to stylesheets.");

            Console.WriteLine("/nojs: Do not process javascript.");
            Console.WriteLine("/nocss: Do not process css.");
        }


        private static string ReplaceSection(string text, string newText, string begin, string end)
        {
            string results = "";
            try
            {
                var indexBegin = text.IndexOf(begin);
                var indexEnd = text.IndexOf(end) + end.Length;
                results = text.Substring(0, indexBegin) + newText + text.Substring(indexEnd);
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
            return results;
        }

        private static string ReadFiles(IEnumerable<string> files)
        {
            string results = "";
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    results += File.ReadAllText(file);
                    File.Delete(file);
                }
            }
            return results;
        }

        private static string CompressJS(string content)
        {
            string results = "";
            try
            {
                var jsCompressor = new JavaScriptCompressor();
                results = jsCompressor.Compress(content);
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
            return results;
        }

        private static string CompressCss(string content)
        {
            string results = "";
            try
            {
                var cssCompressor = new CssCompressor();
                results = cssCompressor.Compress(content);
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }

            return results;
        }

        private static void WriteLog(Exception ex)
        {
            string fileName = "error.log";
            string content = "";

            if (File.Exists(fileName))
            {
                content = File.ReadAllText(fileName) + Environment.NewLine; ;
            }

            content += "Exception: " + ex.Message + Environment.NewLine;
            if (ex.InnerException != null)
            {
                content += "Inner Exception: " + ex.InnerException.Message + Environment.NewLine;
            }
        }
    }
}
