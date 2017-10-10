using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Converter.Data;

namespace Converter
{
    namespace Utility
    {
        public class Parser
        {
            private Reader Input;
            public readonly List<Trip> Transportation = new List<Trip>();
            public readonly List<Lodging> Lodgings = new List<Lodging>();
            public Parser(String filePath)
            {
                Input = new Reader(filePath);

                // Look for Transportation
                int transInd = Input.Lines.FindIndex(ln => ln.Trim().Equals("Transportation:"));
                int lodgeInd = Input.Lines.FindIndex(ln => ln.Trim().Equals("Lodging:"));
                // For each line after transInd BUT before lodgeInd, switch to right type of transport and create:
                for (int i = transInd + 1; i < lodgeInd;)
                {
                    // Find first trip part, then move on to next
                    string line = Input.Lines[i].Trim();
                    Regex regex = new Regex("^[^0-9]", RegexOptions.None);
                    MatchCollection matches = regex.Matches(line);
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            // Add a new trip to transportation
                            string tripName = line.Substring(line.IndexOf('.') + 2);
                            Trip trip = new Trip(tripName);
                            // Keep going until we hit a non-number
                            i++;
                            line = Input.Lines[i].Trim();
                            Regex num = new Regex("^[0-9]", RegexOptions.None);
                            while (num.Matches(line).Count > 0)
                            {
                                // Add a new train (will add more later)
                                string departStation = line.Substring(
                                    line.IndexOf('[') + 1, 
                                    line.IndexOf(" - ") - line.IndexOf('[') - 1);
                                string arriveStation = line.Substring(
                                    line.IndexOf(" - ") + 3,
                                    line.IndexOf(']') - line.IndexOf(" - ") - 3);
                                // TODO: Get actual date (not just time??)
                                string startTime = line.Substring(
                                    line.LastIndexOf('(') + 1,
                                    line.LastIndexOf('-') - line.LastIndexOf('(') - 1);
                                string endTime = line.Substring(
                                    line.LastIndexOf('-') + 1,
                                    line.LastIndexOf(')') - line.LastIndexOf('-') - 1);
                                DateTime depart = new DateTime(2017, 01, 01, Convert.ToInt32(startTime.Substring(0, 2)), Convert.ToInt32(startTime.Substring(3, 2)), 0);
                                DateTime arrive = new DateTime(2017, 01, 01, Convert.ToInt32(endTime.Substring(0, 2)), Convert.ToInt32(endTime.Substring(3, 2)), 0);
                                Boolean isReservation = line.ToUpper().Contains("RESERVATION");
                                Train train = new Train
                                {
                                    DepartStation = departStation,
                                    ArriveStation = arriveStation,
                                    DepartTime = depart,
                                    ArriveTime = arrive,
                                    IsReservation = isReservation
                                };
                                i++;
                                line = Input.Lines[i].Trim();
                                trip.Transports.Add(train);
                            }
                            Transportation.Add(trip);
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
    }
}