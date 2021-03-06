﻿using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using ABMath.ModelFramework.Data;

namespace ABMath.ModelFramework.Transforms
{
    [Serializable]
    public class AggregateTransform : TimeSeriesTransformation
    {
        [CategoryAttribute("Parameter"), Description("Period over which to aggregate")]
        public int Period { get; set; }

        public AggregateTransform()
        {
            Period = 4;  // default
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
                return "Time Series";
            throw new SocketException();
        }

        public override string GetOutputName(int index)
        {
            if (index == 0)
                return "Aggregated Time Series";
            throw new SocketException();
        }

        public override string GetDescription()
        {
            return "Aggregates (sums) sub-intervals of univariate time series";
        }

        public override string GetShortDescription()
        {
            return "Aggregator";
        }

        //public override Icon GetIcon()
        //{
        //    return null;
        //}

        public override void Recompute()
        {
            IsValid = false;
            if (GetInputType(-1) != InputType.UnivariateTS)
                return;

            var series = GetInputBundle();
            if (series.Count != 1)
                return;  // something wrong!

            var input = series[0];

            var agg = new TimeSeries();
            double accumulated = 0;
            for (int t = 0; t < input.Count; ++t )
            {
                accumulated += input[t];
                if (t % Period == Period - 1)
                {
                    agg.Add(input.TimeStamp(t), accumulated, false);
                    accumulated = 0;
                }
            }

            outputs = new List<TimeSeries>();
            outputs.Add(agg);
            IsValid = true;
        }

        public override List<Type> GetAllowedInputTypesFor(int socket)
        {
            if (socket >= NumInputs())
                throw new SocketException();
            return new List<Type> { typeof(TimeSeries) };
        }

        public override List<Type> GetOutputTypesFor(int socket)
        {
            if (socket != 0)
                throw new SocketException();
            return new List<Type> { typeof(TimeSeries) };
        }

    }
}
