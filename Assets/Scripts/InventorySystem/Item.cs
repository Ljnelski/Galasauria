using System;

[Serializable]
public class Item
{
    public ItemData data { get; private set; }
    public int stackSize;

    public Item(ItemData source)
    {
        data = source;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }

    public int GetWeight()
    {
        return data.itemWeight * stackSize;
    }
}
