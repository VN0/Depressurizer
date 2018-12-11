﻿#region LICENSE

//     This file (AutoCatConfigPanel_Flags.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Flags : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Flags()
        {
            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);

            FillFlagsList();
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Public Methods and Operators

        public void FillFlagsList()
        {
            lstIncluded.Items.Clear();

            foreach (string flag in Database.AllFlags)
            {
                lstIncluded.Items.Add(flag);
            }
        }

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatFlags ac = autoCat as AutoCatFlags;
            if (ac == null)
            {
                return;
            }

            txtPrefix.Text = ac.Prefix;

            foreach (ListViewItem item in lstIncluded.Items)
            {
                item.Checked = ac.IncludedFlags.Contains(item.Text);
            }
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatFlags ac = autoCat as AutoCatFlags;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;

            ac.IncludedFlags.Clear();
            foreach (ListViewItem i in lstIncluded.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedFlags.Add(i.Text);
                }
            }
        }

        #endregion

        #region Methods

        private void cmdCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIncluded, true);
        }

        private void cmdUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIncluded, false);
        }

        private void SetAllListCheckStates(ListView list, bool to)
        {
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = to;
            }
        }

        #endregion
    }
}
