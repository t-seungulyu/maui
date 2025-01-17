using ElmSharp;

namespace Microsoft.Maui.Controls.Compatibility.Platform.Tizen.Watch
{
	[System.Obsolete(Compatibility.Hosting.MauiAppBuilderExtensions.UseMapperInstead)]
	public class ShellContentRenderer : IShellItemRenderer
	{
		public ShellContentRenderer(ShellContent content)
		{
			ShellContent = content;
			NativeView = GetNativeView(content);
		}

		public ShellContent ShellContent { get; protected set; }

		public BaseShellItem Item => ShellContent;

		public EvasObject NativeView { get; protected set; }

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				NativeView?.Unrealize();
			}
		}

		static EvasObject GetNativeView(ShellContent content)
		{
			var page = (content as IShellContentController).GetOrCreateContent();
			return Platform.GetOrCreateRenderer(page).NativeView;
		}
	}
}
