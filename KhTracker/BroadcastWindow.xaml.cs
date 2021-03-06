﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for BroadcastWindow.xaml
    /// </summary>
    public partial class BroadcastWindow : Window
    {
        public bool canClose = false;
        Dictionary<string, int> worlds = new Dictionary<string, int>();
        Dictionary<string, int> totals = new Dictionary<string, int>();
        Dictionary<string, int> important = new Dictionary<string, int>();
        List<BitmapImage> Numbers = null;
        Data data;
        public Dictionary<string, bool> toggles = new Dictionary<string, bool>();

        public BroadcastWindow(Data dataIn)
        {
            InitializeComponent();
            //Item.UpdateTotal += new Item.TotalHandler(UpdateTotal);
            Numbers = dataIn.Numbers;
            worlds.Add("SorasHeart",0);
            worlds.Add("DriveForms", 0);
            worlds.Add("SimulatedTwilightTown",0);
            worlds.Add("TwilightTown",0);
            worlds.Add("HollowBastion",0);
            worlds.Add("BeastsCastle",0);
            worlds.Add("OlympusColiseum",0);
            worlds.Add("Agrabah",0);
            worlds.Add("LandofDragons",0);
            worlds.Add("HundredAcreWood",0);
            worlds.Add("PrideLands",0);
            worlds.Add("DisneyCastle",0);
            worlds.Add("HalloweenTown",0);
            worlds.Add("PortRoyal",0);
            worlds.Add("SpaceParanoids",0);
            worlds.Add("TWTNW",0);
            worlds.Add("GoA", 0);
            worlds.Add("Report", 0);
            worlds.Add("TornPage", 0);
            worlds.Add("Fire", 0);
            worlds.Add("Blizzard", 0);
            worlds.Add("Thunder", 0);
            worlds.Add("Cure", 0);
            worlds.Add("Reflect", 0);
            worlds.Add("Magnet", 0);
            worlds.Add("Atlantica", 0);

            totals.Add("SorasHeart", -1);
            totals.Add("DriveForms", -1);
            totals.Add("SimulatedTwilightTown", -1);
            totals.Add("TwilightTown", -1);
            totals.Add("HollowBastion", -1);
            totals.Add("BeastsCastle", -1);
            totals.Add("OlympusColiseum", -1);
            totals.Add("Agrabah", -1);
            totals.Add("LandofDragons", -1);
            totals.Add("HundredAcreWood", -1);
            totals.Add("PrideLands", -1);
            totals.Add("DisneyCastle", -1);
            totals.Add("HalloweenTown", -1);
            totals.Add("PortRoyal", -1);
            totals.Add("SpaceParanoids", -1);
            totals.Add("TWTNW", -1);
            totals.Add("Atlantica", -1);

            important.Add("Fire", 0);
            important.Add("Blizzard", 0);
            important.Add("Thunder", 0);
            important.Add("Cure", 0);
            important.Add("Reflect", 0);
            important.Add("Magnet", 0);
            important.Add("Valor", 0);
            important.Add("Wisdom", 0);
            important.Add("Limit", 0);
            important.Add("Master", 0);
            important.Add("Final", 0);
            important.Add("Nonexistence", 0);
            important.Add("Connection", 0);
            important.Add("Peace", 0);
            important.Add("PromiseCharm", 0);
            important.Add("Feather", 0);
            important.Add("Ukulele", 0);
            important.Add("Baseball", 0);
            important.Add("Lamp", 0);
            important.Add("Report", 0);
            important.Add("TornPage", 0);
            important.Add("SecondChance", 0);
            important.Add("OnceMore", 0);

            data = dataIn;

            foreach (Item item in data.Items)
            {
                item.UpdateTotal += new Item.TotalHandler(UpdateTotal);
                item.UpdateFound += new Item.FoundHandler(UpdateFound);
            }

            Top = Properties.Settings.Default.BroadcastWindowY;
            Left = Properties.Settings.Default.BroadcastWindowX;

            Width = Properties.Settings.Default.BroadcastWindowWidth;
            Height = Properties.Settings.Default.BroadcastWindowHeight;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BroadcastWindowY = Top;
            Properties.Settings.Default.BroadcastWindowX = Left;
        }

        private void Window_SizeChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BroadcastWindowWidth = Width;
            Properties.Settings.Default.BroadcastWindowHeight = Height;
        }

        public void UpdateFound(string item, string world, bool add)
        {
            string worldName = world;
            if (add) worlds[worldName]++; else worlds[worldName]--;
            //Console.WriteLine(worlds[worldName]);

            while (item.Any(char.IsDigit))
            {
                item = item.Remove(item.Length - 1, 1);
            }

            if (add) important[item]++; else important[item]--;
            worlds["Report"] = important["Report"];
            worlds["TornPage"] = important["TornPage"];
            worlds["Fire"] = important["Fire"];
            worlds["Blizzard"] = important["Blizzard"];
            worlds["Thunder"] = important["Thunder"];
            worlds["Cure"] = important["Cure"];
            worlds["Reflect"] = important["Reflect"];
            worlds["Magnet"] = important["Magnet"];
            //Console.WriteLine(item);

            UpdateNumbers();
            
        }

        private void UpdateNumbers()
        {
            foreach(KeyValuePair<string,int> world in worlds)
            {
                if (world.Value < 52)
                {
                    BitmapImage number = Numbers[world.Value + 1];
                    Image worldFound = this.FindName(world.Key + "Found") as Image;
                    worldFound.Source = number;
                    if (world.Key == "Fire" || world.Key == "Blizzard" || world.Key == "Thunder" || world.Key == "Cure" || world.Key == "Reflect" || world.Key == "Magnet")
                    {
                        if (world.Value == 0)
                        {
                            worldFound.Source = null;
                        }
                    }

                    // Format fixing for double digit numbers
                    else if (world.Key != "GoA" && world.Key != "Atlantica" && world.Key != "Report")
                    {
                        if (world.Value >= 10)
                        {
                            (worldFound.Parent as Grid).ColumnDefinitions[0].Width = new GridLength(2, GridUnitType.Star);
                            ((worldFound.Parent as Grid).Parent as Grid).ColumnDefinitions[1].Width = new GridLength(2, GridUnitType.Star);
                        }
                        else
                        {
                            (worldFound.Parent as Grid).ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                            ((worldFound.Parent as Grid).Parent as Grid).ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                        }
                    }
                    else if (world.Key == "Report")
                    {
                        if (world.Value >= 10)
                        {
                            (worldFound.Parent as Grid).ColumnDefinitions[2].Width = new GridLength(.8, GridUnitType.Star);
                        }
                        else
                        {
                            (worldFound.Parent as Grid).ColumnDefinitions[2].Width = new GridLength(.5, GridUnitType.Star);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, int> total in totals)
            {
                Image worldTotal = this.FindName(total.Key + "Total") as Image;
                if (total.Value == -1)
                {
                    worldTotal.Source = new BitmapImage(new Uri("Images\\Numbers\\QuestionMark.png", UriKind.Relative));
                }
                else
                {
                    worldTotal.Source = Numbers[total.Value];
                }

                // Format fixing for double digit numbers
                if (total.Key != "GoA" && total.Key != "Atlantica")
                {
                    if (total.Value >= 11)
                    {
                        (worldTotal.Parent as Grid).ColumnDefinitions[2].Width = new GridLength(2, GridUnitType.Star);
                        double width = ((worldTotal.Parent as Grid).Parent as Grid).ColumnDefinitions[1].Width.Value;
                        ((worldTotal.Parent as Grid).Parent as Grid).ColumnDefinitions[1].Width = new GridLength(width + 1, GridUnitType.Star);
                    }
                    else
                    {
                        (worldTotal.Parent as Grid).ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                    }
                }
            }

            foreach(KeyValuePair<string,int> impCheck in important)
            {
                ContentControl imp = this.FindName(impCheck.Key) as ContentControl;
                if (impCheck.Value > 0)
                {
                    imp.Opacity = 1;
                }
                else
                {
                    if (impCheck.Key != "Report" && impCheck.Key != "TornPage")
                    imp.Opacity = 0.25;
                }
            }
            

            if ((App.Current.MainWindow as MainWindow).collected > 9)
            {
                (Collected.Parent as Grid).ColumnDefinitions[1].Width = new GridLength(.8, GridUnitType.Star);
            }
            else
            {
                (Collected.Parent as Grid).ColumnDefinitions[1].Width = new GridLength(.5, GridUnitType.Star);
            }
        }

        public void UpdateTotal(string world, int checks)
        {
            string worldName = world;
            totals[worldName] = checks+1;

            UpdateNumbers();
        }


        void BroadcastWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            if (!canClose)
            {
                e.Cancel = true;
            }
        }
    }
}
