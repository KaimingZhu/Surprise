using Surprise.Library;
using Surprise.UWPMessage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.EntityFrameworkCore;
using Windows.Storage;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Surprise
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StartUp_Page : Page
    {
        private List<SupriseMessageClass_ForUWP> items;
        private string message_pulled;

        public StartUp_Page()
        {
            ReadMessage();
            this.InitializeComponent();
        }
        private async void ReadMessage()
        {
            using (var db = new Model())
            {

                items = new List<SupriseMessageClass_ForUWP>();
                if (items.Count > 0)
                {
                    items.Clear();
                }
                int i;
                List<Surprise_MessageData> db_MessageDatas = await db.Surprise_MessageDatas.ToListAsync();
                for (i = 0; i < db_MessageDatas.Count; i++)
                {

                    /** Initialize the MessageItem - ID **/

                    SupriseMessageClass_ForUWP temp = new SupriseMessageClass_ForUWP();
                    temp.Data = new Surprise_MessageData();
                    temp.getData(db_MessageDatas[i]);
                    temp.ID_data = temp.Data.Date + temp.Data.Time;

                    /** read from the file : Initialize the MessageItem - RTF_String **/

                    StorageFolder folder = await KnownFolders.MusicLibrary.GetFolderAsync("SurPrise");
                    try
                    {

                        // Read the file's text as a string object
                        StorageFile file = await folder.GetFileAsync(temp.ID_data + ".rtf");
                        temp.RTF_String = await FileIO.ReadTextAsync(file);
                        items.Add(temp);

                    }
                    catch
                    {

                    }

                }
            }
            if (items.Count > 0)
            {
                Random seed = new Random(items.Count);
                int number = seed.Next(0, items.Count);
                MessageBox.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, items[number].RTF_String);
                MessageBox.IsReadOnly = true;
            }
        }
    }
}
