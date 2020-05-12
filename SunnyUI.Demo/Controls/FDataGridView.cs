using System.Collections.Generic;

namespace Sunny.UI.Demo
{
    public partial class FDataGridView : UITitlePage
    {
        public FDataGridView()
        {
            InitializeComponent();

            uiDataGridView1.AddColumn("Column1", "Column1", 100);
            uiDataGridView1.AddColumn("Column2", "Column2").SetFixedMode(100);
            uiDataGridView1.AddColumn("Column3", "Column3").SetFixedMode(100);
            //uiDataGridView1.AddColumn("Column4", "Column4", 20);

            uiDataGridView1.ReadOnly = true;
        }

        public override void Init()
        {
            base.Init();

            List<Data> datas = new List<Data>();
            for (int i = 0; i < 200; i++)
            {
                Data data = new Data();
                data.Column1 = "Data" + i.ToString("D2");
                data.Column2 = i.Mod(2) == 0 ? "A" : "B";
                data.Column3 = "编辑";
                data.Column4 = i.Mod(4) == 0;
                datas.Add(data);
            }

            uiDataGridView1.DataSource = datas;
            uiDataGridView1.SelectedIndex = 160;
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
    }
}