using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Sample.Droid
{
    [Activity(Label = "SlimCanvas Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        SlimCanvas.Canvas slimCanvas;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var root = FindViewById<LinearLayout>(Resource.Id.root);

            SlimCanvas.Droid.SlimCanvasDroid can = new SlimCanvas.Droid.SlimCanvasDroid(this);
            root.AddView(can);
            slimCanvas = can.SlimCanvasPCL;

            LoadSpinnerItems();


        }

        List<SharedSample.SamplesModel> samples;

        void LoadSpinnerItems()
        {
            SharedSample.SampleConfiguration scenarios = new SharedSample.SampleConfiguration();
            samples = scenarios.GetAllSamples();

            var sp = FindViewById<Spinner>(Resource.Id.spinner1);

            List<string> spItems = new List<string>();
            for (int i = 0; i < samples.Count; i++)
            {
                var item = samples[i];
                spItems.Add($"{i + 1} {item.Description}");
            }

            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, spItems);
            sp.Adapter = adapter;

            sp.ItemSelected += Sp_ItemSelected;
        }

        private void Sp_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var item = samples[e.Position];
            ScenarioLoad(item);
        }

        void ScenarioLoad(SharedSample.SamplesModel model)
        {
            var init = (SharedSample.ISamples)System.Activator.CreateInstance(model.ClassType);
            init.LoadScenario(slimCanvas);
        }
    }
}

