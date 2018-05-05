using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Surprise.Library;

namespace Surprise.UWPMessage
{
    public class RtfText
    {
        public static string GetRichText(DependencyObject obj)
        {
            return (string)obj.GetValue(RichTextProperty);
        }

        public static void SetRichText(DependencyObject obj, string value)
        {
            obj.SetValue(RichTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for RichText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RtfText), new PropertyMetadata(string.Empty, callback));

        private static void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var reb = (RichEditBox)d;
            reb.Document.SetText(TextSetOptions.FormatRtf, (string)e.NewValue);
        }
    }

    public class SupriseMessageClass_ForUWP
        {
            public Surprise_MessageData Data { get; set; }
            public string ID_data { get; set; }
            public string RTF_String { get; set; }

            public void getData(Surprise_MessageData temp)
            {
                this.Data.Id = temp.Id;
                this.Data.Date = temp.Date;
                this.Data.Time = temp.Time;
            }
            public SupriseMessageClass_ForUWP()
            {
                ;
            }
        }
}
