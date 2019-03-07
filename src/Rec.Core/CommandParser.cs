﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Rec.Core
{
    public class CommandParser
    {
        public CommandParser()
        {

        }

        public Command Parse(string commandString)
        {
            var split = commandString.Split(' ');
            string kind = split[0];
            string topic = split[1];
            string param = string.Empty;
            DateTime applicableDate = default;

            for (int i = 2; i < split.Length; i++)
            {
                if (split[i].StartsWith("@"))
                {
                    var dateString = split[i].Substring(1);
                    applicableDate = DateTime.Parse(dateString);
                    break;
                }
                param += split[i] + " ";
            }
            param = param.TrimEnd();

            return new Command(commandString, kind, applicableDate, topic, param);
        }
    }
}
