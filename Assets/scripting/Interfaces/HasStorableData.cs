using UnityEngine;

public interface HasStorableData
{
    PickedUpData StoreData();
    void LoadData(PickedUpData data);
}
