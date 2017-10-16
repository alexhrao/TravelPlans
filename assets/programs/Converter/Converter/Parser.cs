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
            public Parser(string filePath)
            {
                Input = new Reader(filePath);
                ParseTransportation();
                ParseLodging();
            }

            private void ParseTransportation()
            {
                // Look for Transportation
                int transInd = Input.Lines.FindIndex(ln => ln.Trim().Equals("Transportation:"));
                int lodgeInd = Input.Lines.FindIndex(ln => ln.Trim().Equals("Lodging:"));
                // For each line after transInd BUT before lodgeInd, switch to right type of transport and create:
                for (int i = transInd + 1; i < lodgeInd;)
                {
                    // Find first trip part, then move on to next
                    string line = Input.Lines[i].Trim();
                    if (line.Length == 0)
                    {
                        return;
                    }
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
                                bool isPlane = false;
                                bool isBus = false;
                                if (line.Contains(" AIRPLANE [")) {
                                    isPlane = true;
                                    line = line.Replace(" AIRPLANE ", " ");
                                }
                                else if (line.Contains(" BUS ["))
                                {
                                    isBus = true;
                                    line = line.Replace(" BUS ", " ");
                                }
                                // Add a new train (will add more later)
                                string departStation = line.Substring(
                                    line.IndexOf('[') + 1,
                                    line.IndexOf(" - ") - line.IndexOf('[') - 1);
                                string arriveStation = line.Substring(
                                    line.IndexOf(" - ") + 3,
                                    line.IndexOf(']') - line.IndexOf(" - ") - 3);
                                string startTime = line.Substring(
                                    line.IndexOf('(') + 1,
                                    line.IndexOf('-', line.IndexOf('(')) - line.IndexOf('(') - 1);
                                string endTime = line.Substring(
                                    line.IndexOf('-', line.IndexOf('(')) + 1,
                                    line.IndexOf(')') - line.IndexOf('-', line.IndexOf('(')) - 1);
                                DateTime depart = new DateTime(2017, 01, 01, Convert.ToInt32(startTime.Substring(0, 2)), Convert.ToInt32(startTime.Substring(3, 2)), 0);
                                DateTime arrive = new DateTime(2017, 01, 01, Convert.ToInt32(endTime.Substring(0, 2)), Convert.ToInt32(endTime.Substring(3, 2)), 0);
                                Boolean isReservation = line.ToUpper().Contains("RESERVATION");
                                line = line.Replace("RESERVATION", "");
                                string notes = "";
                                int noteInd = line.IndexOf('}');
                                if (noteInd <= 0)
                                {
                                    noteInd = line.IndexOf(')');
                                }
                                if (noteInd <= 0)
                                {
                                    notes = "";
                                }
                                else
                                {
                                    notes = line.Substring(noteInd + 1);
                                }
                                Transport transport;
                                if (isPlane)
                                {
                                    transport = new Plane
                                    {
                                        DepartStation = departStation,
                                        ArriveStation = arriveStation,
                                        DepartTime = depart,
                                        ArriveTime = arrive,
                                        IsReservation = isReservation,
                                        Notes = notes
                                    };
                                }
                                else if (isBus)
                                {
                                    transport = new Bus
                                    {
                                        DepartStation = departStation,
                                        ArriveStation = arriveStation,
                                        DepartTime = depart,
                                        ArriveTime = arrive,
                                        IsReservation = isReservation,
                                        Notes = notes
                                    };
                                }
                                else
                                {
                                    transport = new Train
                                    {
                                        DepartStation = departStation,
                                        ArriveStation = arriveStation,
                                        DepartTime = depart,
                                        ArriveTime = arrive,
                                        IsReservation = isReservation,
                                        Notes = notes
                                    };   
                                }
                                i++;
                                line = Input.Lines[i].Trim();
                                trip.Transports.Add(transport);
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
            private void ParseLodging()
            {
                // Look for Lodging
                int lodgeInd = Input.Lines.FindIndex(ln => ln.Trim().Equals("Lodging:"));
                // Look for Entertainment. If not found, then nothing after
                int enterInd = Input.Lines.FindIndex(ln => ln.Trim().Equals("Entertainment:"));
                if (enterInd < 0)
                {
                    enterInd = Input.Lines.Count;
                    Input.Lines.Add("Entertainment:");
                }
                else
                {
                    for (int i = lodgeInd + 1; i < enterInd; i++)
                    {
                        string line = Input.Lines[i].Trim();
                        // Line of form <#>. <LocationName> has this form:
                        // NO space before ., 
                        Regex regex = new Regex("^\\S?[^0-9]+[.]\\s+", RegexOptions.None);
                        MatchCollection matches = regex.Matches(line);
                        if (matches.Count > 0)
                        {
                            // line is name of location
                            // For now, only add the name of the hostel (format is uneven) and dates
                            // Dates will be next line (after ##)
                            // This will be next line;
                            // add the new lodging
                            i++;
                            string dates = Input.Lines[i].Trim();
                            dates = dates.Substring(dates.IndexOf('.') + 1).Trim();
                            string startDate = dates.Substring(0, dates.IndexOf('-'));
                            string endDate = dates.Substring(dates.IndexOf('-') + 1);
                            DateTime start = new DateTime(2017, Convert.ToInt32(startDate.Substring(3)), Convert.ToInt32(startDate.Substring(0, 2)));
                            DateTime end = new DateTime(2017, Convert.ToInt32(endDate.Substring(3)), Convert.ToInt32(endDate.Substring(0, 2)));
                            i++;
                            string name = Input.Lines[i].Trim();
                            bool isAir = false;
                            if (name.Equals("AIRBNB"))
                            {
                                isAir = true;
                                i++;
                                name = Input.Lines[i].Trim();
                            }
                            // Notes will be every line after that has SAME indentation.
                            int numIndent = Input.Lines[i].Length - Input.Lines[i].TrimStart().Length;
                            List<string> notes = new List<string>();
                            while ((Input.Lines[i].Length - Input.Lines[i].TrimStart().Length) == numIndent)
                            {
                                notes.Add(Input.Lines[i].Trim());
                                i++;
                            }
                            i--;
                            Lodging lodging;
                            if (isAir)
                            {
                                lodging = new AirBnb
                                {
                                    Location = line.Substring(line.IndexOf('.') + 2),
                                    Name = "AirBNB",
                                    ArriveDate = start,
                                    DepartDate = end,
                                    Notes = notes
                                };
                            }
                            else
                            {
                                lodging = new Hostel
                                {
                                    Location = line.Substring(line.IndexOf('.') + 2),
                                    Name = name,
                                    ArriveDate = start,
                                    DepartDate = end,
                                    Notes = notes
                                };
                            }
                            Lodgings.Add(lodging);
                        }
                    }
                }
            }
        }
    }
}