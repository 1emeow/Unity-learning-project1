using UnityEngine;

public interface CanBePicked
{
    void IsPickedUp();
    void IsReleased();
    bool pickupable { get; set; } // get pour prendre la valeur (true false) set pour la changer (toujours true false)
}
