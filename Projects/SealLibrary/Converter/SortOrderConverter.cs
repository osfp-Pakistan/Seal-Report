﻿//
// Copyright (c) Seal Report, Eric Pfirsch (sealreport@gmail.com), http://www.sealreport.org.
// This code is licensed under GNU General Public License version 3, http://www.gnu.org/licenses/gpl-3.0.en.html.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Seal.Model;

namespace Seal.Converter
{
    public class SortOrderConverter : StringConverter
    {
        public static string kAutomaticAscSortKeyword = "Automatic Ascendant";
        public static string kAutomaticDescSortKeyword = "Automatic Descendant";
        public static string kNoSortKeyword = "Not sorted";
        public static string kAscendantSortKeyword = "Ascendant";
        public static string kDescendantSortKeyword = "Descendant";

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; //true means show a combobox
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; //true will limit to list. false will show the list, but allow free-form entry
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ReportElement element = context.Instance as ReportElement;
            List<string> orders = new List<string>();
            if (element != null)
            {
                orders.Add(kAutomaticAscSortKeyword);
                orders.Add(kAutomaticDescSortKeyword);
                if (element.PivotPosition != PivotPosition.Page) orders.Add(kNoSortKeyword);
                IEnumerable<ReportElement> elements = element.Model.Elements.Where(i => i.PivotPosition == element.PivotPosition);
                for (int i = 1; i <= elements.Count(); i++)
                {
                    orders.Add(string.Format("{0} {1}", i, kAscendantSortKeyword));
                    orders.Add(string.Format("{0} {1}", i, kDescendantSortKeyword));
                }
            }
            return new StandardValuesCollection(orders.ToArray());
        }
    }
}
