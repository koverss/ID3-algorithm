using Microsoft.Win32;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using ID3.DecisionTree;

namespace ID3
{
    public partial class MainWindow : Window
    {
        private int tabColumnCount;
        private List<string> columns;
        private DataTable tbl;
        private int tableWidth;
        private List<double> entropyForEachColumn;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_FindDataFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Data files (*.data)|*.data|Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == true)
            {
                lbl_Path.Content = fileDialog.FileName;
                ParseDataFile(fileDialog.FileName);
            }
            this.Focus();
        }

        private void btn_ID3_Click(object sender, RoutedEventArgs e)
        {
            List<string> catAttItems = new List<string>();
            foreach (DataRow row in tbl.Rows)
                catAttItems.Add((string)row.ItemArray[cB_CatAttColNumb.SelectedIndex]);

            //catAttTotal = catAttItems;
            var catAtt = new KeyValuePair<string, List<string>>(tbl.Columns[cB_CatAttColNumb.SelectedIndex].ColumnName, catAttItems);
            var result = ID3(catAtt, tbl);
            tB_Tree.Text = PrintTree(result);
        }

        private void ParseDataFile(string path)
        {
            tbl = new DataTable();
            List<List<string>> result = new List<List<string>>();
            List<List<string>> l = new List<List<string>>();

            var content = File.ReadAllLines(path);

            for (int col = 0; col < content.First().Count(); col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));

            foreach (var line in content)
            {
                var row = line.Split(',').ToList();
                tabColumnCount = row.Count();
                columns = new List<string>();

                for (var i = 0; i < tabColumnCount; i++)
                {
                    columns.Add((i + 1).ToString());
                }
                cB_CatAttColNumb.IsEnabled = true;
                cB_CatAttColNumb.ItemsSource = columns;

                if (tableWidth == 0)
                    tableWidth = row.Count();

                DataRow dr = tbl.NewRow();

                for (var i = 0; i < row.Count; i++)
                {
                    dr[i] = row[i];
                }

                tbl.Rows.Add(dr);
            }

            while (tbl.Columns.Count > tableWidth)
            {
                tbl.Columns.RemoveAt(tableWidth);
            }

            dataGrid.ItemsSource = tbl.AsDataView();
            var entropyVals = "";
            //entropia
            entropyForEachColumn = new List<double>();
            for (var i = 0; i < tableWidth; i++)
            {
                var en = Calculations.Entropy(i, dataGrid);
                entropyForEachColumn.Add(en);
                entropyVals += en.ToString() + ", ";
            }

            txtBl_entropyVals.Text = entropyVals.Remove(entropyVals.Length - 2);
        }

        private NodeID3 ID3(KeyValuePair<string, List<string>> categoricalAttribute, DataTable trainingSet)
        {
            if (trainingSet.Rows.Count == 0)
                return new NodeID3 { name = "failure", nodes = new Dictionary<string, NodeID3>() };
            if (categoricalAttribute.Value.Distinct().Count() == 1)
                return new NodeID3 { name = categoricalAttribute.Value.Distinct().First(), nodes = new Dictionary<string, NodeID3>() };
            if (trainingSet.Columns.Count == 1)
            {
                return new NodeID3
                {
                    name = categoricalAttribute.Value.GroupBy(x => x)
                        .OrderByDescending(x => x.Count())
                        .Select(x => x.Key)
                        .First(),
                    nodes = new Dictionary<string, NodeID3>()
                };
            }

            var maxGainColumn = "";
            double maxGain = -1;
            List<string> maxGainColElements = new List<string>();
            foreach (DataColumn col in trainingSet.Columns)
            {
                if (col.ColumnName != categoricalAttribute.Key)
                {
                    var _colItems = new List<string>();
                    foreach (DataRow el in trainingSet.Rows)
                        _colItems.Add((string)el[col]);

                    var currGain = Calculations.Gain(categoricalAttribute.Value, _colItems);
                    if (maxGain < currGain)
                    {
                        maxGain = currGain;
                        maxGainColumn = col.ColumnName;
                        maxGainColElements = _colItems;
                    }
                }
            }

            var d = maxGainColElements; //wartosci at kat
            NodeID3 currentNode = new NodeID3() { name = maxGainColumn, nodes = new Dictionary<string, NodeID3>() };
            var distinctD = d.Distinct().ToList();

            var temp = tbl.AsEnumerable().Select(x => x[maxGainColumn]).Distinct().ToList();

            for (var i = 0; i < temp.Count; i++)// distinctD.Count
            {
                DataTable subset = trainingSet.Clone();
                var ind = trainingSet.Columns.IndexOf(maxGainColumn);
                var catAttElementsInSubset = new List<string>();
                foreach (DataRow row in trainingSet.Rows)
                    if ((string)row.ItemArray[ind] == (string)temp[i]) //jesli obiekt w kol kat != d[i]
                    {
                        catAttElementsInSubset.Add((string)row[categoricalAttribute.Key]);
                        subset.Rows.Add(row.ItemArray);
                    }

                subset.Columns.Remove(maxGainColumn); // -D
                //stworzylem subsety, wywoluje rekurencje
                currentNode.nodes.Add((string)temp[i], ID3(new KeyValuePair<string, List<string>>(categoricalAttribute.Key, catAttElementsInSubset), subset));
            }
            return currentNode;
        }

        public static string PrintTree(NodeID3 tree, string strTree, int nInd = 0)
        {
            var ind = string.Join("", Enumerable.Repeat(" ", nInd));
            var str = $"\n{ind}{strTree}";
            foreach (var node in tree.nodes)
                str += PrintTree(node.Value, $"{node.Key} --> {node.Value.name}", nInd + 2);
            return str;
        }

        public static string PrintTree(NodeID3 tree)
        {
            var strTree = PrintTree(tree, tree.name);
            return strTree.StartsWith("\n") ? strTree.Substring(1) : strTree;
        }
    }
}