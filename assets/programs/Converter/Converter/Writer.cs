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
                        HtmlNode transportNode = HtmlNode.CreateNode("<li><h3>" +
                            HttpUtility.HtmlEncode(transport.DepartStation) + " (" + transport.DepartTime.ToShortTimeString() + ") " +
                            " <span class=\"glyphicon glyphicon-arrow-right\"></span> " +
                            HttpUtility.HtmlEncode(transport.ArriveStation) + " (" + transport.ArriveTime.ToShortTimeString() + ")</h3></li>");
                        HtmlNode noteNode = HtmlNode.CreateNode("<p class=\"notes\">" + HttpUtility.HtmlEncode(transport.Notes) + "</p>");
                        transportNode.AppendChild(noteNode);
                        tripList.AppendChild(transportNode);
                    }
                    tripNode.AppendChild(tripList);
                    trips.AppendChild(tripNode);
                }

                HtmlNode lodgings = document.GetElementbyId("lodging");
                foreach (Lodging lodging in parser.Lodgings)
                {
                    HtmlNode lodgeNode = HtmlNode.CreateNode("<div class=\"lodging\"></div>");
                    lodgeNode.AppendChild(HtmlNode.CreateNode("<h2>" + HttpUtility.HtmlEncode(lodging.Location) + "</h2>"));
                    lodgeNode.AppendChild(HtmlNode.CreateNode("<p>" + HttpUtility.HtmlEncode(lodging.Name) + " (" + 
                        HttpUtility.HtmlEncode(lodging.DepartDate.ToShortDateString()) +
                        " <span class=\"glyphicon glyphicon-arrow-right\"></span> " +
                        HttpUtility.HtmlEncode(lodging.ArriveDate.ToShortDateString()) + ")</p>"));
                    lodgings.AppendChild(lodgeNode);
                }

                string newPath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar + "Summary.html";
                File.WriteAllText(newPath, document.DocumentNode.OuterHtml);
            }
        }
    }
}