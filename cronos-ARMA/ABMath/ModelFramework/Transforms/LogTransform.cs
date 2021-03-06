﻿#region License Info
//Component of Cronos Package, http://www.codeplex.com/cronos
//Copyright (C) 2009 Anthony Brockwell

//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
#endregion


using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ABMath.ModelFramework.Data;

namespace ABMath.ModelFramework.Transforms
{
    [Serializable]
    public class LogTransform : TimeSeriesTransformation
    {
        public override void Recompute()
        {
            var toLog = GetInputBundle();

            var logged = new List<TimeSeries>(toLog.Count);

            bool failed = false;
            foreach (var inp in toLog)
            {
                var ts = new TimeSeries();
                ts.Title = "Log(" + inp.Title + ")";
                for (int t = 0; t < inp.Count; ++t)
                    if (inp[t] > 0)
                        ts.Add(inp.TimeStamp(t), Math.Log(inp[t]), false);
                    else
                        failed = true;
                logged.Add(ts);
            }

            outputs = logged;
            IsValid = true;

            if (failed)
                MessageBox.Show(
                    "One or more values were non-positive.  Logs of these numbers are not included in the output.",
                    "Warning");
        }

        public override string GetDescription()
        {
            return "Log Transform";
        }

        public override string GetShortDescription()
        {
            return "Log";
        }

        //public override Icon GetIcon()
        //{
        //    return null;
        //}

        public override int NumInputs()
        {
            return 1;
        }

        public override int NumOutputs()
        {
            return 1;
        }

        public override string GetInputName(int index)
        {
            return "TimeSeries";
        }

        public override string GetOutputName(int index)
        {
            return "TimeSeries";
        }

        public override List<Type> GetAllowedInputTypesFor(int socket)
        {
            if (socket != 0)
                throw new SocketException();
            return new List<Type> { typeof(TimeSeries), typeof(MVTimeSeries) };
        }

        public override List<Type> GetOutputTypesFor(int socket)
        {
            if (socket != 0)
                throw new SocketException();
            return new List<Type> { typeof(TimeSeries), typeof(MVTimeSeries) };
        }
    }
}
