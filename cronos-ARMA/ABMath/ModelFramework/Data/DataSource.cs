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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Net.Sockets;

namespace ABMath.ModelFramework.Data
{
    /// <summary>
    /// This class provides a mechanism for connection to external sources of data.
    /// The simplest example is a connection to a .csv file, but more interesting examples
    /// could include connections by network to a broker's historical data feed,
    /// a connection to a local database of time series, etc.
    /// </summary>
    [Serializable]
    public abstract class DataSource : IConnectable
    {
        [Browsable(false)]
        public object Data 
        { get; protected set; }

        /// <summary>
        /// returns true if data retrieval is successful
        /// </summary>
        /// <param name="infoMessage">returns any log information about retrieval</param>
        /// <returns></returns>
        public abstract bool RefreshFromSource(out string infoMessage);

        public int NumInputs()
        {
            return 0;
        }

        public int NumOutputs()
        {
            return 1;
        }

        public string GetInputName(int socket)
        {
            throw new ArgumentException("Data source has no inputs.");
        }

        public string GetOutputName(int socket)
        {
            if (socket == 0)
                return "Data";
            throw new SocketException();
        }

        public virtual List<Type> GetAllowedInputTypesFor(int socket)
        {
            return new List<Type>();
        }

        public virtual List<Type> GetOutputTypesFor(int socket)
        {
            return null;
        }

        public bool InputIsFree(int socket)
        {
            return false;
        }

        public bool SetInput(int socket, object item, StringBuilder failMessage)
        {
            throw new ArgumentException("Data source has no inputs.");
        }

        public object GetOutput(int socket)
        {
            if (socket == 0)
                return Data;
            throw new SocketException();
        }

        public string GetDescription()
        {
            return GetShortDescription();
        }

        public string GetShortDescription()
        {
            return "Data source";
        }

        //public Color GetBackgroundColor()
        //{
        //    return Color.GreenYellow;
        //}

        //public Icon GetIcon()
        //{
        //    return null;
        //}

        [Browsable(false)]
        public string ToolTipText
        {
            get; set;
        }
    }
}
