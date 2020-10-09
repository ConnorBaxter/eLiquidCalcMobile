using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace qLiquidCalcMobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class AdvancedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_advanced);

            EditText baseVol = FindViewById<EditText>(Resource.Id.editText1);
            EditText baseConc = FindViewById<EditText>(Resource.Id.editText2);
            EditText finalConc = FindViewById<EditText>(Resource.Id.editText3);
            TextView finalVol = FindViewById<TextView>(Resource.Id.textView1);
            TextView txtRatio = FindViewById<TextView>(Resource.Id.textView2);
            SeekBar seekBar = FindViewById<SeekBar>(Resource.Id.seekBar1);
            Button calcButton = FindViewById<Button>(Resource.Id.button1);
            Button switchModeButton = FindViewById<Button>(Resource.Id.button2);

            baseVol.SetHintTextColor(new Color(255, 255, 255, 128));
            baseConc.SetHintTextColor(new Color(255, 255, 255, 128));
            finalConc.SetHintTextColor(new Color(255, 255, 255, 128));

            seekBar.Progress = 50;
            seekBar.ProgressChanged += (sender, e) =>
            {
                int vgRatio = 100 - seekBar.Progress;
                int pgRatio = seekBar.Progress;

                txtRatio.Text = $"    PG/VG Ratio    {pgRatio}/{vgRatio}";
            };

            calcButton.Click += (sender, e) =>
            {
                string baseVolStr = baseVol.Text;
                string baseConcStr = baseConc.Text;
                string finalConcStr = finalConc.Text;

                try
                {
                    double baseVolNum = double.Parse(baseVolStr);
                    double baseConcNum = double.Parse(baseConcStr);
                    double finalConcNum = double.Parse(finalConcStr);

                    double finalVolNum = Core.Calculate.Calc(baseVolNum, baseConcNum, finalConcNum);
                    string finalVolStr = finalVolNum.ToString();

                    double pgRatio = double.Parse(seekBar.Progress.ToString());
                    double pgPerc = (pgRatio / finalVolNum) * 100;

                    finalVol.Text = $"PG Volume: {pgPerc}\nVG Volume: {100 - pgPerc}\nTotal Volume: {finalVolStr}";
                }
                catch (Exception exception)
                {
                    Toast.MakeText(this.ApplicationContext, "Please fill in all fields with numbers only", ToastLength.Long).Show();
                }

            };

            switchModeButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
            };
        }
    }
}