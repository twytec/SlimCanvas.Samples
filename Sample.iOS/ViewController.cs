using System;
using System.Collections.Generic;
using UIKit;

namespace Sample.iOS
{
    public partial class ViewController : UIViewController
    {
        SlimCanvas.Canvas slimCanvas;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            var stack = new UIStackView(mainRoot.Frame)
            {
                Axis = UILayoutConstraintAxis.Vertical
            };

            var picker = new UIPickerView();
            SharedSample.SampleConfiguration scenarios = new SharedSample.SampleConfiguration();
            var model = new PickerModel(scenarios.GetAllSamples());
            model.PickerChanged += Model_PickerChanged;
            picker.Model = model;
            stack.AddArrangedSubview(picker);

            SlimCanvas.iOS.SlimCanvasIOS canvas = new SlimCanvas.iOS.SlimCanvasIOS();
            stack.AddArrangedSubview(canvas);

            slimCanvas = canvas.SlimCanvasPCL;

            mainRoot.AddSubview(stack);

            ScenarioLoad(new SharedSample.SamplesModel() { ClassType = typeof(SharedSample.Scenarios.HalloWorld) });
        }

        private void Model_PickerChanged(object sender, PickerChangedEventArgs e)
        {
            ScenarioLoad(e.SelectedValue);
        }

        void ScenarioLoad(SharedSample.SamplesModel model)
        {
            var init = (SharedSample.ISamples)Activator.CreateInstance(model.ClassType);
            init.LoadScenario(slimCanvas);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }

    public class PickerModel : UIPickerViewModel
    {
        private readonly IList<SharedSample.SamplesModel> values;

        public event EventHandler<PickerChangedEventArgs> PickerChanged;

        public PickerModel(IList<SharedSample.SamplesModel> values)
        {
            this.values = values;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return values.Count;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return values[(int)row].Description;
        }

        public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
        {
            return 40f;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            if (this.PickerChanged != null)
            {
                PickerChanged(this, new PickerChangedEventArgs { SelectedValue = values[(int)row] });
            }
        }
    }

    public class PickerChangedEventArgs : EventArgs
    {
        public SharedSample.SamplesModel SelectedValue { get; set; }
    }
}