namespace WeightedDistribution
{
    [System.Serializable]
    public class IntDistributionItem : DistributionItem<int> {}

    public class IntDistribution : Distribution<int, IntDistributionItem> {}
}