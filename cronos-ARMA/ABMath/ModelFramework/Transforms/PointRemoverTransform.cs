﻿using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ABMath.ModelFramework.Data;

namespace ABMath.ModelFramework.Transforms
{
    [Serializable]
    public class PointRemoverTransform : TimeSeriesTransformation
    {
        [Category("Parameter"), DescriptionAttribute("Lower end of range to keep")]
        public double LowerValue { get; set; }
        [CategoryAttribute("Parameter"), DescriptionAttribute("Upper end of range to keep")]
        public double UpperValue { get; set; }

        public PointRemoverTransform()
        {
            LowerValue = 0.01;
            UpperValue = 10000;
        }

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
            if (index == 0)
                return "Input TS";
            throw new ArgumentException("Invalid socket.");
        }

        public override string GetOutputName(int index)
        {
            if (index == 0)
                return "Output TS";
            throw new ArgumentException("Invalid socket.");
        }

        public override string GetDescription()
        {
            return "Removes points whose values lie outside a specified range.";
        }

        public override string GetShortDescription()
        {
            return "Remove";
        }

        //public override Icon GetIcon()
        //{
        //    return null;
        //}

        public override void Recompute()
        {
            IsValid = false;

            var tsList = GetInputBundle();
            if (tsList != null)
            {
                outputs = new List<TimeSeries>(tsList.Count);

                foreach (var ts in tsList)
                {
                    var stripped = new TimeSeries() {Title = ts.Title};
                    for (int t = 0; t < ts.Count; ++t)
                        if (!(ts[t] < LowerValue || ts[t] > UpperValue))
                            stripped.Add(ts.TimeStamp(t), ts[t], true);

                    outputs.Add(stripped);
                }
                if (outputs.Count > 0)
                   IsValid = true;
            }
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
