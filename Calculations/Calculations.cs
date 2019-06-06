using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;

namespace ID3
{
    public abstract class Calculations
    {
        private static double AttInfo(List<string> catAtt, List<string> nonCategoricalAtt)
        {
            var totalAttributeCount = catAtt.Count(); //total w kolumnie

            List<int> attributesCountDistinct = new List<int>();
            var differentAttributesFromCatAtt = catAtt.Distinct().ToList();

            //ilosc wystapien poszczegolnych atrybutow kolumny kategorycznej
            for (var i = 0; i < differentAttributesFromCatAtt.Count(); i++)
            {
                attributesCountDistinct.Add(catAtt.FindAll(x => x == differentAttributesFromCatAtt[i]).Count());
            }

            List<int> givenAttIndexes;
            int totalPerAttribute;

            var trainingSetPairingAttributes = new List<string>();

            var distinctTrainingSet = nonCategoricalAtt.Distinct().ToList();
            var countedDistinctTrainingSet = new List<int>();

            //licze ile wystąpien mają poszczegolne elementy zbioru ucz
            foreach (var el in distinctTrainingSet)
            {
                countedDistinctTrainingSet.Add(nonCategoricalAtt.FindAll(x => x == el).Count());
            }

            double _info = 0;
            for (var i = 0; i < differentAttributesFromCatAtt.Count; i++)
            {
                //indexy wszystkich elementow takich jak atrybut np. ile play w windy
                givenAttIndexes = Enumerable
                    .Range(0, catAtt.Count)
                    .Where(x => catAtt[x] == differentAttributesFromCatAtt[i]).ToList();

                totalPerAttribute = givenAttIndexes.Count();

                trainingSetPairingAttributes = new List<string>();
                foreach (var index in givenAttIndexes)
                {
                    //if (nonCatAttributes.Count() > index)
                    trainingSetPairingAttributes.Add(nonCategoricalAtt[index]);
                }
                //                        np.sunny / outlook total       * entropia dla atrybutu
                _info += (double)totalPerAttribute / totalAttributeCount * Entropy(trainingSetPairingAttributes);
            }

            return _info;
        }

        private static double Entropy(List<string> list)
        {
            var totalItems = list.Count();
            var distinctList = list.Distinct().ToList();
            var countEachItem = new List<int>();
            double partial_result = 0;
            double result = 0;

            foreach (var el in distinctList)
            {
                var counter = list.FindAll(x => x == el).Count();
                var p = (double)counter / totalItems;
                partial_result = (p) * Math.Log((p), 2);
                result += partial_result;
            }

            result = result * -1;
            return result;
        }

        public static double Entropy(int columnNumber, DataGrid dataGrid)
        {
            var _items = new List<string>();

            foreach (DataRowView dr in dataGrid.ItemsSource)
            {
                _items.Add(dr[columnNumber].ToString());
            }

            var columnTotalItems = _items.Count();

            var distinctList = _items.Distinct().ToList();
            var countEachItem = new List<int>();
            double partial_result = 0;
            double result = 0;

            foreach (var el in distinctList)
            {
                var counter = _items.FindAll(x => x == el).Count();
                var p = (double)counter / columnTotalItems;
                partial_result = (p) * Math.Log((p), 2);
                result += partial_result;
            }

            result = result * -1;
            return result;
        }

        public static double Gain(List<string> categoricalAttribute, List<string> nonCategoricalAtt)
        {
            // entropia klasy(kolumny T)
            var c = Entropy(nonCategoricalAtt) - AttInfo(categoricalAttribute, nonCategoricalAtt);
            return c;
        }
    }
}
