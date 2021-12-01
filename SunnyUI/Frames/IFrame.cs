using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunny.UI
{
    public interface IFrame
    {
        UITabControl MainTabControl { get; }

        UIPage AddPage(UIPage page, int index);

        UIPage AddPage(UIPage page, Guid guid);

        UIPage AddPage(UIPage page);

        void SelectPage(int pageIndex);

        void SelectPage(Guid guid);

        UIPage GetPage(int pageIndex);

        UIPage GetPage(Guid guid);

        bool TopMost { get; set; }

        bool RemovePage(int pageIndex);

        bool RemovePage(Guid guid);

        void Feedback(object sender, int pageIndex, params object[] objects);
    }
}
