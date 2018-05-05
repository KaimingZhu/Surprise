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
using Surprise.Library;
using Surprise.UWPMessage;
using Windows.Storage;
using Windows.UI.Composition;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Surprise
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Manage_Page : Page
    {

        public List<SupriseMessageClass_ForUWP> items { get; set; }
        public List<SupriseMessageClass_ForUWP> selected_items { get; set; }

        public Manage_Page()
        {
            ReadMessage();
            this.InitializeComponent();
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            /**
             * 
             *      bug : to get the selected item - refer to the label 
             * 
             * **/
            using (var db = new Model())
            {
                selected_items = new List<SupriseMessageClass_ForUWP>();

                if (MessageList.SelectedItems.Count != 0)
                {
                    int i;
                    for (i = 0; i < MessageList.SelectedItems.Count; i++)
                    {

                        /** get the value of the item **/

                        selected_items.Add((SupriseMessageClass_ForUWP)MessageList.SelectedItems[i]);

                        /** delete it from the folder**/

                        StorageFolder folder = await KnownFolders.MusicLibrary.GetFolderAsync("SurPrise");
                        try
                        {
                            StorageFile file = await folder.GetFileAsync(selected_items[i].ID_data + ".rtf");
                            await file.DeleteAsync();

                            /** delete it from the database **/

                            db.Surprise_MessageDatas.Remove(selected_items[i].Data);
                        }
                        catch
                        {

                        }

                    }
                }
                await db.SaveChangesAsync();
            }
            ReadMessage();
        }
        private async void ReadMessage()
        {
            using (var db = new Model())
            {
                
                items = new List<SupriseMessageClass_ForUWP>();
                if(items.Count > 0)
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
            if(items.Count > 0)
            {
                MessageList.ItemsSource = items;
            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            MessageList.SelectAll();
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            MessageList.SelectedItems.Clear();
        }
    }
}

