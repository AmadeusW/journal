﻿using System;
using Rec.Core.Commands;

namespace Rec.Core
{
    public class CommandParser
    {
        public CommandParser()
        {

        }

        public Command Parse(string commandString, DateTime recordedDate)
        {
            var split = commandString.Split(new[] { ' ', '-'});
            string verb = split[0];
            string param = string.Empty;
            double paramNumber = 0;
            string date = string.Empty;
            string date2 = string.Empty;
            string place = string.Empty;
            string place2 = string.Empty;
            DateTime applicableDate = default;
            DateTime applicableDate2 = default;

            bool parsingData = true;
            bool parsingDate = false;
            bool parsingPlace = false;
            bool parsingSecond = true;

            for (int i = 1; i < split.Length; i++)
            {
                var trimmed = split[i].Trim();
                if (string.IsNullOrEmpty(trimmed))
                    continue;

                if (trimmed.StartsWith("@"))
                {
                    parsingData = false;
                    parsingDate = true;
                    parsingPlace = false;
                    parsingSecond = false;
                }
                else if (trimmed.StartsWith("#"))
                {
                    parsingData = false;
                    parsingDate = false;
                    parsingPlace = true;
                    parsingSecond = false;
                }
                else if (trimmed.StartsWith("-"))
                {
                    parsingSecond = true;
                }

                if (parsingData)
                {
                    param += trimmed + " ";
                }
                else if (parsingDate)
                {
                    if (trimmed.StartsWith("@"))
                        trimmed = trimmed.Substring(1);

                    if (parsingSecond)
                        date2 += trimmed + " ";
                    else
                        date += trimmed + " ";
                }
                else if (parsingPlace)
                {
                    if (parsingSecond)
                        place2 += trimmed + " ";
                    else
                        place += trimmed + " ";
                }
            }

            param = param.TrimEnd();
            Double.TryParse(param, out paramNumber);
            DateTime.TryParse(date, out applicableDate);
            DateTime.TryParse(date2, out applicableDate2);
            place = place.TrimEnd();
            place2 = place2.TrimEnd();

            switch (verb)
            {
                case "plan":
                case "show":
                case "list":
                    // TODO: use a factory which creates a command based on the verb
                    return new GenericAction(commandString, verb, param, paramNumber, applicableDate, applicableDate2, place, place2);
                default:
                    return new Recording(commandString, verb, param, paramNumber, applicableDate, applicableDate2, place, place2);
            }
            
        }
    }
}
