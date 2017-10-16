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
                        HtmlNode transportNode;
                        if (transport is Train)
                        {
                            transportNode = HtmlNode.CreateNode("<li><h3><i class=\"fa fa-train\"></i> " +
                                HttpUtility.HtmlEncode(transport.DepartStation) + " (" + transport.DepartTime.ToShortTimeString() + ") " +
                                " <span class=\"glyphicon glyphicon-arrow-right\"></span> " +
                                HttpUtility.HtmlEncode(transport.ArriveStation) + " (" + transport.ArriveTime.ToShortTimeString() + ")</h3></li>");   
                        }
                        else if (transport is Plane)
                        {
                            transportNode = HtmlNode.CreateNode("<li><h3><i class=\"fa fa-plane\"></i> " +
                                HttpUtility.HtmlEncode(transport.DepartStation) + " (" + transport.DepartTime.ToShortTimeString() + ") " +
                                " <span class=\"glyphicon glyphicon-arrow-right\"></span> " +
                                HttpUtility.HtmlEncode(transport.ArriveStation) + " (" + transport.ArriveTime.ToShortTimeString() + ")</h3></li>");
                        }
                        else if (transport is Bus)
                        {
                            transportNode = HtmlNode.CreateNode("<li><h3><i class=\"fa fa-bus\"></i> " +
                                HttpUtility.HtmlEncode(transport.DepartStation) + " (" + transport.DepartTime.ToShortTimeString() + ") " +
                                " <span class=\"glyphicon glyphicon-arrow-right\"></span> " +
                                HttpUtility.HtmlEncode(transport.ArriveStation) + " (" + transport.ArriveTime.ToShortTimeString() + ")</h3></li>");
                        }
                        else
                        {
                            transportNode = HtmlNode.CreateNode("<li><h3>" +
                                HttpUtility.HtmlEncode(transport.DepartStation) + " (" + transport.DepartTime.ToShortTimeString() + ") " +
                                " <span class=\"glyphicon glyphicon-arrow-right\"></span> " +
                                HttpUtility.HtmlEncode(transport.ArriveStation) + " (" + transport.ArriveTime.ToShortTimeString() + ")</h3></li>");
                        }
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
                    if (lodging is AirBnb)
                    {
                        lodgeNode.AppendChild(HtmlNode.CreateNode("<h2>AirBnB</h2>"));   
                    }
                    else
                    {
                        lodgeNode.AppendChild(HtmlNode.CreateNode("<h2>Hostel</h2>"));
                    }
                    lodgeNode.AppendChild(HtmlNode.CreateNode("<h2>" + HttpUtility.HtmlEncode(lodging.Location) + "</h2>"));
                    lodgeNode.AppendChild(HtmlNode.CreateNode("<h3>" + HttpUtility.HtmlEncode(lodging.Name) + " (" + 
                        HttpUtility.HtmlEncode(lodging.DepartDate.ToShortDateString()) +
                        " <span class=\"glyphicon glyphicon-arrow-right\"></span> " +
                        HttpUtility.HtmlEncode(lodging.ArriveDate.ToShortDateString()) + ")</h3>"));
                    for (int i = 0; i < lodging.Notes.Count; i++)
                    {
                        lodging.Notes[i] = HttpUtility.HtmlEncode(lodging.Notes[i]);
                    }
                    string Notes = String.Join("<br />", lodging.Notes.ToArray());
                    lodgeNode.AppendChild("<p>" + Notes + "</p>");
                    lodgings.AppendChild(lodgeNode);
                }
                string newPath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar + "Summary.html";
                File.WriteAllText(newPath, document.DocumentNode.OuterHtml);
            }
        }
    }
}