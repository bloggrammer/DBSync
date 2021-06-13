using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace DBSync.WPFClient.Controls
{
    public class HyperlinkExtension : Hyperlink
    {
        public HyperlinkExtension()
        {
            RequestNavigate += OnRequestNavigate;
        }
        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

    }
}
