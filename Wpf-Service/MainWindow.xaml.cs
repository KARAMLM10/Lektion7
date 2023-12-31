﻿using Iot_Shared.Models;
using Iot_Shared.Services;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Service
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private readonly IOTHubManager _iotHub;

        public ObservableCollection<Twin> DeviceTwinList { get; set; } = new ObservableCollection<Twin>();
        public MainWindow(IOTHubManager iotHub)
        {
            InitializeComponent();
            _iotHub = iotHub;
            DeviceListView.ItemsSource = DeviceTwinList;
            Task.FromResult(GetDevicesTwinAsync());

        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedDeviceId.Text))

                    await _iotHub.SendMethodAsync(new MethodDataRequest
                    {
                        DeviceId = SelectedDeviceId.Text,
                        MethodName = "Start"
                    });
            }

            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }

        }

        private async void StoptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedDeviceId.Text))

                    await _iotHub.SendMethodAsync(new MethodDataRequest
                    {
                        DeviceId = SelectedDeviceId.Text,
                        MethodName = "Stop"
                    });
            }

            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }

        }

        //private async Task GetDeviceDataAsync()
        //{
        //    try
        //    {
        //        var list = await _iotHub.GetDevicesAsJsonAsync();
        //        var device = list.FirstOrDefault();
        //        DeviceInformation.Text = device;

        //    }
        //    catch(Exception ex) 
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}

        private async Task GetDevicesTwinAsync()
        {
            try
            {
                while (true)
                {
                    var twins = await _iotHub.GetDevicesAsTwinsAsync();
                    DeviceTwinList.Clear();

                    foreach (var twin in twins)
                        DeviceTwinList.Add(twin);

                    await Task.Delay(1000);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }


        }

        private async void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                if (button != null)
                {
                    var twin = button.DataContext as Twin;
                    if (twin != null)
                    {
                        string deviceId = twin.DeviceId;
                        if (!string.IsNullOrEmpty(deviceId))
                            await _iotHub.SendMethodAsync(new MethodDataRequest
                            {
                                DeviceId= deviceId,
                                MethodName = "stop"
                            });
                    }
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

        }
        private async void StarttButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                Button? button = sender as Button;
                if (button != null)
                {
                    var twin = button.DataContext as Twin;
                    if (twin != null)
                    {
                        string deviceId = twin.DeviceId;
                        if (!string.IsNullOrEmpty(deviceId))
                            await _iotHub.SendMethodAsync(new MethodDataRequest
                            {
                                DeviceId = deviceId,
                                MethodName = "start"
                            });
                    }
                }
            
            }
            catch(Exception ex) { Debug.WriteLine(ex.Message); }
        }

    }
}
