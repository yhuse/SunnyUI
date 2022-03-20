using System.Collections.Generic;

namespace Sunny.UI.Demo
{
    public partial class FDataGridView : UIPage
    {
        List<Data> datas = new List<Data>();

        public FDataGridView()
        {
            InitializeComponent();

            //SunnyUI封装的加列函数，也可以和原生的一样，从Columns里面添加列
            uiDataGridView1.AddColumn("Column1", "Column1");
            uiDataGridView1.AddColumn("Column2", "Column2");
            uiDataGridView1.AddColumn("Column3", "Column3");
            uiDataGridView1.AddColumn("Column4", "Column4");

            //SunnyUI常用的初始化配置，看个人喜好用或者不用。
            uiDataGridView1.Init();

            for (int i = 0; i < 3610; i++)
            {
                Data data = new Data();
                data.Column1 = "Data" + i.ToString("D2");
                data.Column2 = i.Mod(2) == 0 ? "A" : "B";
                data.Column3 = "编辑";
                data.Column4 = i.Mod(4) == 0;
                datas.Add(data);
            }

            //设置分页控件总数
            uiPagination1.TotalCount = datas.Count;

            //设置分页控件每页数量
            uiPagination1.PageSize = 50;

            uiDataGridView1.SelectIndexChange += uiDataGridView1_SelectIndexChange;

            uiDataGridView1.ShowGridLine = true;
            //设置统计绑定的表格
            uiDataGridViewFooter1.DataGridView = uiDataGridView1;
        }

        public override void Init()
        {
            base.Init();
            uiPagination1.ActivePage = 1;
        }

        public class Data
        {
            public string Column1 { get; set; }

            public string Column2 { get; set; }

            public string Column3 { get; set; }

            public bool Column4 { get; set; }

            public override string ToString()
            {
                return Column1;
            }
        }

        /// <summary>
        /// 分页控件页面切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pagingSource"></param>
        /// <param name="pageIndex"></param>
        /// <param name="count"></param>
        private void uiPagination1_PageChanged(object sender, object pagingSource, int pageIndex, int count)
        {
            //未连接数据库，通过模拟数据来实现
            //一般通过ORM的分页去取数据来填充
            //pageIndex：第几页，和界面对应，从1开始，取数据可能要用pageIndex - 1
            //count：单页数据量，也就是PageSize值
            List<Data> data = new List<Data>();
            for (int i = (pageIndex - 1) * count; i < (pageIndex - 1) * count + count; i++)
            {
                if (i >= datas.Count) continue;
                data.Add(datas[i]);
            }

            uiDataGridView1.DataSource = data;
            uiDataGridViewFooter1.Clear();
            uiDataGridViewFooter1["Column1"] = "合计：";
            uiDataGridViewFooter1["Column2"] = "Column2_" + pageIndex;
            uiDataGridViewFooter1["Column3"] = "Column3_" + pageIndex;
            uiDataGridViewFooter1["Column4"] = "Column4_" + pageIndex;
        }

        private void uiDataGridView1_SelectIndexChange(object sender, int index)
        {
            index.WriteConsole("SelectedIndex");
        }
    }
}
