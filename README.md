arcgis-runtime-mapview-export-example

This sample app will export an image of the mapview at the entered dpi.

The app will export the mapview at the entered image resolution. After each export the resoultion is increased by an incrment of 300. To gain the detail for the selected resolution the app expands the size of the mapview. At some point, the mapview will need to expand past a certian limit to achieve the resolution and will crash the application. 

To shortcut to the crash, use the Make it Crash button. This will set the resolution to 2800dpi and attempt to export. 

Resulting Call Stack:
Esri.ArcGISRuntime.ArcGISRuntimeException
  HResult=0x80131500
  Message=Unknown error: DirectX failure CreateTexture2D code = 0x80070057
  Source=Esri.ArcGISRuntime
  StackTrace:
   at Esri.ArcGISRuntime.ArcGISException.HandleCoreError(CoreError error, Boolean throwException)
   at RuntimeCoreNet.GeneratedWrappers.Interop.CheckError(IntPtr errorHandle, Boolean throwOnFailure, GCHandle wrapperHandle)
   at RuntimeCoreNet.GeneratedWrappers.CoreGeoView.Pulse()
   at Esri.ArcGISRuntime.UI.Controls.GeoView.Esri.ArcGISRuntime.Internal.IDxSurfaceSource.Pulse()
   at Esri.ArcGISRuntime.Internal.HostedSurfaceElement.CompositionTarget_Rendering(Object sender, EventArgs e)
   at Esri.ArcGISRuntime.Internal.HostedSurfaceElement.<>c.<AttachRenderingEvent>b__29_0(HostedSurfaceElement instance, Object sender, EventArgs e)
   at Esri.ArcGISRuntime.Internal.WeakEventListener`4.OnEvent(TSender sender, TEventArgs eventArgs)
   at System.Windows.Media.MediaContext.RenderMessageHandlerCore(Object resizedCompositionTarget)
   at System.Windows.Media.MediaContext.AnimatedRenderMessageHandler(Object resizedCompositionTarget)
   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
   at System.Windows.Threading.DispatcherOperation.InvokeImpl()
   at System.Windows.Threading.DispatcherOperation.InvokeInSecurityContext(Object state)
   at MS.Internal.CulturePreservingExecutionContext.CallbackWrapper(Object obj)
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
   at MS.Internal.CulturePreservingExecutionContext.Run(CulturePreservingExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Windows.Threading.DispatcherOperation.Invoke()
   at System.Windows.Threading.Dispatcher.ProcessQueue()
   at System.Windows.Threading.Dispatcher.WndProcHook(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
   at MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
   at MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
   at System.Windows.Threading.Dispatcher.LegacyInvokeImpl(DispatcherPriority priority, TimeSpan timeout, Delegate method, Object args, Int32 numArgs)
   at MS.Win32.HwndSubclass.SubclassWndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam)
   at MS.Win32.UnsafeNativeMethods.DispatchMessage(MSG& msg)
   at System.Windows.Threading.Dispatcher.PushFrameImpl(DispatcherFrame frame)
   at System.Windows.Threading.Dispatcher.PushFrame(DispatcherFrame frame)
   at Esri.ArcGISRuntime.Internal.HostedSurfaceElement.SurfaceBackgroundUiWorker(Object arg)
   at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Threading.ThreadHelper.ThreadStart(Object obj)

  This exception was originally thrown at this call stack:
    [External Code]
