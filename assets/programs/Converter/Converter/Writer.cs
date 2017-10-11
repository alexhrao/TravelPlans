using System;
using HtmlAgilityPack;
using Converter.Properties;
using System.IO;
using System.Web;

namespace Converter
{
    namespace Utility
    {
        public class Writer
        {
            public Writer(string filePath, Parser parser)
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(Resources.template);
                // Write trains first:
                HtmlNode trips = document.GetElementbyId("transportation");
                foreach (Trip trip in parser.Transportation)
                {
                    HtmlNode tripNode = HtmlNode.CreateNode("<div class=\"trip\"></div>");
                    tripNode.AppendChild(HtmlNode.CreateNode("<h2>" + HttpUtility.HtmlEncode(trip.Name) + "</h2>"));
                    HtmlNode tripList = HtmlNode.CreateNode("<ol></ol>");
                    foreach (Transport transport in trip.Transports)
                    {
                        HtmlNode transportNode = HtmlNode.CreateNode("<li>" +
                            HttpUtility.HtmlEncode(transport.DepartStation) + " (" + transport.DepartTime.ToShortTimeString() + ") " +
                            " <span class=\"glyphicon glyphicon-arrow-right\"></span> " +
                            HttpUtility.HtmlEncode(transport.ArriveStation) + " (" + transport.ArriveTime.ToShortTimeString() + ")</li>");
                        tripList.AppendChild(transportNode);
                    }
                    tripNode.AppendChild(tripList);
                    trips.AppendChild(tripNode);
                }

                string newPath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar + "Summary.html";
                File.WriteAllText(newPath, document.DocumentNode.OuterHtml);
            }
        }
    }
}