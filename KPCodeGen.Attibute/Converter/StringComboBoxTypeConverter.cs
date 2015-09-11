using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KPCodeGen.Attibute.Converter
{
    public delegate string[] TypeConverterItems(ITypeDescriptorContext context);

    public class StringComboBoxTypeConverter : StringConverter
    {
        public static event TypeConverterItems OnSelectColumn;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext
        context)
        {
            //true means show a combobox
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext
        context)
        {
            //true will limit to list. false will show the list, but allow free-form entry
            return true;
        }

        public override
        StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (OnSelectColumn != null)
            {
                var arrayObj = OnSelectColumn(context);
                if (arrayObj != null)
                    return new StandardValuesCollection(arrayObj);
            }
            return new StandardValuesCollection(new string[] { });
        }

        // Colocar em cima da propriedade
        // [TypeConverter(typeof(StringComboBoxTypeConverter))]
    }
}
