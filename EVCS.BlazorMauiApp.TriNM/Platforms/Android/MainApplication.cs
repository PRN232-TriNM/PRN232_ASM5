using Android.App;
using Android.Runtime;
using EVCS.BlazorMauiApp.TriNM;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace EVCS.BlazorMauiApp.TriNM
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
