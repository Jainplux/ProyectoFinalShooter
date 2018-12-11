using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Idamage
{
    int health { get; set; }

    void UpdateHealth(int totalDamage);
}
