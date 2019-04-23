using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WeightedDistribution
{
    [System.Serializable]
    public class DistributionItem<T>
    {
        [SerializeField]
        float weight;
        public float Weight { get { return weight; } set { if (weight >= 0) weight = value; } }

        public float CombinedWeight { get; set; }

        [Range(0f, 100f), SerializeField]
        float percentage;
        public float Percentage { get { return percentage; } set { if (value >= 0 && value <= 100) percentage = value; } }

        [SerializeField]
        T value;
        public T Value { get { return value; } set { this.value = value; } }
    }

    public abstract class Distribution<T, T_ITEM> : MonoBehaviour
        where T_ITEM : DistributionItem<T>, new()
    {
        [SerializeField]
        List<T_ITEM> items;
        public List<T_ITEM> Items { get { return items; } }

        int nbItems = 0;
        float combinedWeight;

        void OnItemsChange(bool addedItem = false)
        {
            // On Add Component
            if (items == null)
                return;

            // On Add Item
            if (items.Count > nbItems)
            {
                if (items.Count == 1)
                    items[0].Weight = 1;
                else if (!addedItem)
                    items[items.Count - 1].Weight = 0;
            }

            ComputePercentages();
            nbItems = items.Count;
        }

        void ComputePercentages()
        {
            combinedWeight = 0;

            foreach (T_ITEM item in items)
            {
                combinedWeight += item.Weight;
                item.CombinedWeight = combinedWeight;
            }

            foreach (T_ITEM item in items)
                item.Percentage = item.Weight * 100 / combinedWeight;
        }

        void OnValidate()
        {
            OnItemsChange();
        }

        public T Draw()
        {
            if (items.Count == 0)
                throw new UnityException("Can't draw an item from an empty distribution!");

            float random = Random.Range(0f, combinedWeight);
            foreach (T_ITEM item in items)
            {
                if (random <= item.CombinedWeight)
                    return item.Value;
            }

            throw new UnityException("Error while drawing an item.");
        }

        public void Add(T value, float weight)
        {
            items.Add(new T_ITEM { Value = value, Weight = weight });
            OnItemsChange(true);
        }

        public void RemoveAt(int index)
        {
            if (items.Count - 1 < index || index < 0)
                return;
            items.RemoveAt(index);
            OnItemsChange();
        }
    }
}