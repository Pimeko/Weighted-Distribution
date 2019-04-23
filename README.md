# Weighted distribution

**Weighted distribution** is an inspector tool for Unity3D. 

It allows easy percentage distribution across different values, based on weights. Its generic implementation allows **any type** for the returned value.

![inspector](https://github.com/Pimeko/Weighted-Distribution/blob/master/Assets/WeightedDistribution/Documentation/demo.gif)

# How to use
To begin, simply download the package and add it to your Unity Project.

To create a new Distribution type, you must declare two empty classes :
- the `distribution item`: must be serializable and inherit from DistributionItem\<**T**>
	- **T** is the type of an item's value (int, string ..)
- the `distribution` class: must inherit from Distribution<**T**, **T_ITEM**>
	- **T** is the type of an item's value (int, string ..)
	- **T_ITEM** is the type of the `distribution item` 

Don't forget to use the namespace `WeightedDistribution`.

**Example:**
Let's say you want to create a distribution to get a random string.

You simply have to create a script called `StringDistribution.cs`, as follows :
```C#
using WeightedDistribution;

[System.Serializable]
public class StringDistributionItem : DistributionItem<string> {}

public class StringDistribution: Distribution<string, StringDistributionItem > {}
```

You can now add your component to any gameobject.

Changing the weight automatically affects the probability of apparition. The percentage value is only used as a visual representation and, thus, cannot be changed directly.

You can now reference the distribution component by script and call Draw() to get a random value, as detailed in the API section.

# API
## Draw ()

Calling draw returns a random item from the list.

**Example:**
```c#
StringDistribution distribution;

void Start()
{
	distribution = GetComponent<StringDistribution>();
}

void GetRandom()
{
	string randomString = distribution.Draw();
	// Do stuff with the random string
}
```

## Add (T value, float weight)
Adds an item to the list.

- `Value` must be of the type of an item's value (int, string ..)
- `Weight` is a positive float value

## RemoveAt (int index)
Removes an item from the list at the specified index.