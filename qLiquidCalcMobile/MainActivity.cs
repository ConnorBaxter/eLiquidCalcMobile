using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace qLiquidCalcMobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText baseVol = FindViewById<EditText>(Resource.Id.editText1);
            EditText baseConc = FindViewById<EditText>(Resource.Id.editText2);
            EditText finalConc = FindViewById<EditText>(Resource.Id.editText3);
            TextView finalVol = FindViewById<TextView>(Resource.Id.textView1);

            Button calcButton = FindViewById<Button>(Resource.Id.button1);
            Button switchModeButton = FindViewById<Button>(Resource.Id.button2);

            baseVol.SetHintTextColor(new Color(255, 255, 255, 128));
            baseConc.SetHintTextColor(new Color(255, 255, 255, 128));
            finalConc.SetHintTextColor(new Color(255, 255, 255, 128));

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

                    finalVol.Text = "Final Volume: " + finalVolStr + " ml";
                }
                catch (Exception exception)
                {
                    Toast.MakeText(this.ApplicationContext, "Please fill in all fields with numbers only", ToastLength.Long).Show();
                }

            };

            switchModeButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(AdvancedActivity));
                this.StartActivity(intent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}